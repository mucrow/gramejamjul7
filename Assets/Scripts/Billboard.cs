using UnityEngine;

public class Billboard: MonoBehaviour {
  [SerializeField] bool _lockX = true;
  [SerializeField] bool _lockZ = true;

  Camera _mainCamera;
  Vector3 _cameraRotation;
  Vector3 _newRotation;

  void Start() {
    _mainCamera = Camera.main;
  }

  void LateUpdate() {
    _cameraRotation = _mainCamera.transform.eulerAngles;

    _newRotation = transform.eulerAngles;
    _newRotation.y = _cameraRotation.y;

    if (!_lockX) {
      _newRotation.x = _cameraRotation.x;
    }
    if (!_lockZ) {
      _newRotation.z = _cameraRotation.z;
    }

    transform.eulerAngles = _newRotation;
  }
}
