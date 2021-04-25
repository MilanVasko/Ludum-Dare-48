using System;
using System.Collections;
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

	public ScoreEntryUI scoreEntryPrefab;
	public RectTransform scoreEntriesContainer;

	bool scoresLoading = false;
	string scoresError = "";

	bool submitLoading = false;
	string submitError = "";

	void Awake() {
		yourNameInput.text = PlayerSettings.playerName;
		SetUIActive(false);
		PlayerHealth.onPlayerDied += OnPlayerDied;
	}

	void OnDestroy() {
		PlayerHealth.onPlayerDied -= OnPlayerDied;
	}

	void Start() {
		yourNameSubmitButton.interactable = PlayerSettings.playerName.Trim() != "";
	}

	void OnPlayerDied(PlayerHealth playerHealth) {
		SetUIActive(true);

		yourTimeText.text = Util.FormatElapsedTime(FindObjectOfType<GameDirector>().elapsedTime);
		StopAllCoroutines();
		StartCoroutine(GetScores());
	}

	public void OnTryAgainPressed() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

	public void OnPlayerNameChanged(string newPlayerName) {
		PlayerSettings.playerName = newPlayerName;
		yourNameSubmitButton.interactable = newPlayerName.Trim() != "";
	}

	public void OnSubmitPressed() {
		if (submitLoading) {
			return;
		}
		submitLoading = true;
		StartCoroutine(SendScore(PlayerSettings.playerName, FindObjectOfType<GameDirector>().elapsedTime));
	}

	void SetUIActive(bool active) {
		gameObject.SetActive(active);
		Time.timeScale = active ? 0 : 1;
	}

	IEnumerator GetScores() {
		UnityWebRequest www = UnityWebRequest.Get(API_URL + "/scores");
		yield return www.SendWebRequest();

		scoresLoading = false;
		if (www.result != UnityWebRequest.Result.Success) {
			scoresError = www.error;
		} else {
			scoresError = "";

			ScoreResponse scoreResponse = JsonUtility.FromJson<ScoreResponse>(www.downloadHandler.text);

			foreach (RectTransform child in scoreEntriesContainer) {
				Destroy(child.gameObject);
			}

			Array.Sort(scoreResponse.values, (a, b) => -a.elapsedTime.CompareTo(b.elapsedTime));

			foreach (ScoreOutput scoreOutput in scoreResponse.values) {
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
				submitError = www.error;
				Debug.Log(submitError);
			} else {
				submitError = "";
				Debug.Log("Done uploading score");
			}
		}
	}
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
