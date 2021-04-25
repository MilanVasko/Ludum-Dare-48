using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {
	const string API_URL = "http://tic-tac-run.herokuapp.com";

	public InputField yourNameInput;
	public Text yourTimeText;
	public Button yourNameSubmitButton;
	public Text yourNameSubmitFailedText;
	bool submitLoading = false;
	string submitError = "";
	bool submitForbidden = false;

	public ScoreEntryUI scoreEntryPrefab;
	public RectTransform scoreEntriesContainer;
	public Text scoreLoadingFailedText;
	bool scoresLoading = false;

	List<ScoreOutput> currentScores = new List<ScoreOutput>();

	void Awake() {
		yourNameInput.text = PlayerSettings.playerName;
		SetUIActive(false);
		PlayerHealth.onPlayerDied += OnPlayerDied;
	}

	void OnDestroy() {
		PlayerHealth.onPlayerDied -= OnPlayerDied;
	}

	void Start() {
		yourNameSubmitFailedText.gameObject.SetActive(false);
		scoreLoadingFailedText.gameObject.SetActive(false);

		RefreshSubmitButtonInteractivity();
	}

	void RefreshSubmitButtonInteractivity() {
		yourNameSubmitButton.interactable = !submitForbidden && (PlayerSettings.playerName.Trim() != "" && (!submitLoading || submitError != ""));
	}

	void OnPlayerDied(PlayerHealth playerHealth) {
		SetUIActive(true);

		yourTimeText.text = Util.FormatElapsedTime(FindObjectOfType<GameDirector>().elapsedTime);
		StopAllCoroutines();

		if (!scoresLoading) {
			StartCoroutine(GetScores());
		}
	}

	public void OnTryAgainPressed() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

	public void OnPlayerNameChanged(string newPlayerName) {
		PlayerSettings.playerName = newPlayerName;
		RefreshSubmitButtonInteractivity();
	}

	public void OnSubmitPressed() {
		if (submitLoading || submitForbidden) {
			return;
		}
		submitLoading = true;
		StartCoroutine(SendScore(PlayerSettings.playerName, FindObjectOfType<GameDirector>().elapsedTime));
	}

	IEnumerator SendScore(string name, float elapsedTime) {
		ScoreInput scoreInput = new ScoreInput {
			name = name,
			elapsedTime = elapsedTime
		};

		string rawInput = JsonUtility.ToJson(scoreInput);
		byte[] jsonBytes = Encoding.UTF8.GetBytes(rawInput);

		DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

		UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBytes);
		uploadHandlerRaw.contentType = "application/json";

		using (UnityWebRequest www = new UnityWebRequest(API_URL + "/scores", "POST", downloadHandlerBuffer, uploadHandlerRaw)) {
			yield return www.SendWebRequest();

			submitLoading = false;

			if (www.result != UnityWebRequest.Result.Success) {
				OnSubmitError(www.error);
				Debug.Log(www.error);
			} else {
				OnSubmitError("");
				ScoreOutput myScoreOutput = new ScoreOutput {
					name = name,
					date = "",
					elapsedTime = elapsedTime
				};
				currentScores.Add(myScoreOutput);
				currentScores.Sort((a, b) => -a.elapsedTime.CompareTo(b.elapsedTime));
				RefreshScores(currentScores);

				submitForbidden = true;
				RefreshSubmitButtonInteractivity();
				Debug.Log("Done uploading score");
			}
		}
	}

	void OnSubmitError(string error) {
		submitError = error;
		yourNameSubmitFailedText.gameObject.SetActive(error != "");
	}

	void SetUIActive(bool active) {
		gameObject.SetActive(active);
		Time.timeScale = active ? 0 : 1;
	}

	IEnumerator GetScores() {
		scoresLoading = true;

		UnityWebRequest www = UnityWebRequest.Get(API_URL + "/scores");
		yield return www.SendWebRequest();

		scoresLoading = false;
		if (www.result != UnityWebRequest.Result.Success) {
			OnScoresError(www.error);
		} else {
			OnScoresError("");

			ScoreResponse scoreResponse = JsonUtility.FromJson<ScoreResponse>(www.downloadHandler.text);

			if (scoreResponse != null && scoreResponse.values != null) {
				currentScores = new List<ScoreOutput>(scoreResponse.values);
				currentScores.Sort((a, b) => -a.elapsedTime.CompareTo(b.elapsedTime));
			} else {
				currentScores = new List<ScoreOutput>();
			}

			RefreshScores(currentScores);
		}
	}

	void OnScoresError(string error) {
		scoreLoadingFailedText.gameObject.SetActive(error != "");
	}

	void RefreshScores(List<ScoreOutput> sortedCurrentScores) {
		foreach (RectTransform child in scoreEntriesContainer) {
			Destroy(child.gameObject);
		}

		foreach (ScoreOutput scoreOutput in sortedCurrentScores) {
			ScoreEntryUI scoreEntry = Instantiate(scoreEntryPrefab, scoreEntriesContainer);
			scoreEntry.SetData(scoreOutput.name, scoreOutput.elapsedTime);
		}
	}
}

[Serializable]
public class ScoreInput {
	public string name;
	public float elapsedTime;
}

[Serializable]
public class ScoreResponse {
	public ScoreOutput[] values;
}

[Serializable]
public class ScoreOutput {
	public string name;
	public string date;
	public float elapsedTime;
}
