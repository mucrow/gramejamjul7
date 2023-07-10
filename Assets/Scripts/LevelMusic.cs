
using UnityEngine;

public class LevelMusic: MonoBehaviour {
  [SerializeField] AudioClip _song;

  void Start() {
    AudioManager.Instance.PlaySong(_song);
  }
}
