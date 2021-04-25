using UnityEngine;

public class Util : MonoBehaviour {
	public static string FormatElapsedTime(float elapsedTime) {
		int totalSeconds = (int)elapsedTime;
		int totalMinutes = totalSeconds / 60;
		int totalHours = totalMinutes / 60;

		int milliseconds = ((int)(elapsedTime * 1000)) % 1000;
		int seconds = totalSeconds % 60;
		int minutes = totalMinutes % 60;
		int hours = totalHours % 60;

		return hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00") + "." + milliseconds.ToString("000");
	}
}
