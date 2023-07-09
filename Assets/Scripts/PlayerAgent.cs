using UnityEngine;

public class PlayerAgent: MonoBehaviour {
  [SerializeField] GameObject _arrowPrefab;
  [SerializeField] float _slowMotionTimeScale = 0.2f;

  Camera _mainCamera;

  void Start() {
    _mainCamera = Camera.main;
  }

  void Update() {
    bool shootArrowButtonPressed = Input.GetMouseButtonDown(0);
    bool slowTimeButtonPressed = Input.GetMouseButtonDown(1);
    bool slowTimeButtonReleased = Input.GetMouseButtonUp(1);

    if (shootArrowButtonPressed) {
      var newArrow = Utils.FireProjectile(_mainCamera.transform, _arrowPrefab, Vector3.zero);

      var arrowRotation = newArrow.transform.eulerAngles;
      arrowRotation.x = 0f;
      arrowRotation.z = 0f;
      newArrow.transform.eulerAngles = arrowRotation;

      var arrowPosition = newArrow.transform.position;
      // TODO choose Y-level based on MAXIMUM KILL POTENTIAL
      arrowPosition.y = 0.5f;
      newArrow.transform.position = arrowPosition;
    }

    if (slowTimeButtonPressed) {
      Time.timeScale = _slowMotionTimeScale;
    }
    if (slowTimeButtonReleased) {
      Time.timeScale = 1f;
    }
  }
}
