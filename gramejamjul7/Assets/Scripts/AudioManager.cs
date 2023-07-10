using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager: MonoBehaviour {
  public static AudioManager Instance;

  [SerializeField] AudioSource _musicSource;

  void Awake() {
    if (Instance == null) {
      Instance = this;
      DontDestroyOnLoad(gameObject);
      _musicSource = GetComponent<AudioSource>();
    }
    else {
      Destroy(gameObject);
    }
  }

  public void PlaySong(AudioClip song) {
    if (_musicSource.clip == song && _musicSource.isPlaying) {
      return;
    }
    _musicSource.clip = song;
    _musicSource.Play();
  }
}
