using UnityEngine;
using UnityEngine.UI;

public class Curtains: MonoBehaviour {
  // public static Curtains Instance;

  [SerializeField] Sprite[] _frames;

  int _frameIndex;
  // Camera _mainCamera;
  // SpriteRenderer _spriteRenderer;
  Image _image;

  void Awake() {
    // if (Instance != null) {
    //   Destroy(gameObject);
    //   return;
    // }
    // Instance = this;
    // DontDestroyOnLoad(gameObject);
    LegitAwake();
  }

  // runs only if we're the legit curtains
  void LegitAwake() {
    _image = GetComponent<Image>();
    _frameIndex = _frames.Length - 1;
    UpdateSpriteRenderer();
  }

  void Start() {
    // _mainCamera = Camera.main;
  }

  void Update() {
    // var cameraTransform = _mainCamera.transform;
    // var forwardVector = cameraTransform.rotation * Vector3.forward * 5f;
    // transform.position = cameraTransform.position + forwardVector;
  }

  /** Returns true if the curtains are now fully closed. */
  public bool PullClosed() {
    if (_frameIndex >= _frames.Length - 1) {
      return true;
    }
    _frameIndex = Mathf.Min(_frameIndex + 1, _frames.Length - 1);
    UpdateSpriteRenderer();
    return false;
  }

  /** Returns true if the curtains are now fully open. */
  public bool PullOpen() {
    if (_frameIndex <= 0) {
      return true;
    }
    _frameIndex = Mathf.Max(_frameIndex - 1, 0);
    UpdateSpriteRenderer();
    return false;
  }

  void UpdateSpriteRenderer() {
    _image.sprite = _frames[_frameIndex];
  }
}
