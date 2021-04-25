using System;
using UnityEngine;

public class PlayerSettings : MonoBehaviour {
	public static event Action<float> onSoundsVolumeChanged;

	public static float soundsVolume {
		get {
			return PlayerPrefs.GetFloat("sounds-volume", 0.7f);
		}
		set {
			PlayerPrefs.SetFloat("sounds-volume", value);
			onSoundsVolumeChanged?.Invoke(value);
		}
	}
}
