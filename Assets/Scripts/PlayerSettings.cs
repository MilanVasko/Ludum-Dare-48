using System;
using UnityEngine;

public class PlayerSettings : MonoBehaviour {
	public static event Action<float> onSoundsVolumeChanged;
	public static event Action<float> onMusicVolumeChanged;

	public static float soundsVolume {
		get {
			return PlayerPrefs.GetFloat("sounds-volume", 0.7f);
		}
		set {
			PlayerPrefs.SetFloat("sounds-volume", value);
			onSoundsVolumeChanged?.Invoke(value);
		}
	}

	public static float musicVolume {
		get {
			return PlayerPrefs.GetFloat("music-volume", 0.7f);
		}
		set {
			PlayerPrefs.SetFloat("music-volume", value);
			onMusicVolumeChanged?.Invoke(value);
		}
	}
}
