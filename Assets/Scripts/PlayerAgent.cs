using System;
using UnityEngine;

public class PlayerAgent: MonoBehaviour {
  [SerializeField] GameObject _arrowPrefab;
  [SerializeField] float _slowMotionTimeScale = 0.2f;

  LineRenderer _lineRenderer;
  Camera _mainCamera;

  void Start() {
    _mainCamera = Camera.main;
    _lineRenderer = GetComponentInChildren<LineRenderer>();
  }

  void Update() {
    var arrowRayStart = _mainCamera.transform.position;
    arrowRayStart.y = 1f;
    var arrowRayEulerAngles = new Vector3(0f, _mainCamera.transform.eulerAngles.y, 0f);
    var arrowRayDirection = Quaternion.Euler(arrowRayEulerAngles);
    var arrowRayPositionDelta = arrowRayDirection * Vector3.forward * 1000f;

    var arrowRayEnd = arrowRayStart + arrowRayPositionDelta;
    _lineRenderer.SetPositions(new Vector3[] { arrowRayStart, arrowRayEnd });

    bool strafeArrowButtonPressed = Input.GetMouseButtonDown(0);
    bool slowTimeButtonPressed = Input.GetMouseButtonDown(1);
    bool slowTimeButtonReleased = Input.GetMouseButtonUp(1);

    if (strafeArrowButtonPressed) {
      FireArrow(arrowRayStart, arrowRayDirection);
    }

    if (slowTimeButtonPressed) {
      Time.timeScale = _slowMotionTimeScale;
    }
    if (slowTimeButtonReleased) {
      Time.timeScale = 1f;
    }
  }

  void FireArrow(Vector3 arrowStart, Quaternion arrowDirection) {
    var raycastHits = Physics.RaycastAll(arrowStart, arrowDirection * Vector3.forward);
    // sort raycast hits by distance
    Array.Sort(raycastHits, new RaycastHitAscendingDistanceComparer());

    foreach (var raycastHit in raycastHits) {
      var hitCollider = raycastHit.collider;
      if (!hitCollider.isTrigger) {
        break;
      }
      if (hitCollider.CompareTag("Enemy")) {
        Destroy(hitCollider.gameObject);
      }
    }
  }
}
