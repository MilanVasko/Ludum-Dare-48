using UnityEngine;
using UnityEngine.UI;

public class ScoreEntryUI : MonoBehaviour {
	public Text text;

	public void SetData(string name, float elapsedTime) {
		text.text = name + ": " + Util.FormatElapsedTime(elapsedTime);
	}
}
