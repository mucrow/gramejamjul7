using System;
using System.Collections.Generic;
using System.Linq;
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
    // var arrowRayStart = _mainCamera.transform.position;
    // arrowRayStart.y = 1f;
    // var arrowRayEulerAngles = new Vector3(0f, _mainCamera.transform.eulerAngles.y, 0f);
    // var arrowRayDirection = Quaternion.Euler(arrowRayEulerAngles);
    // var arrowRayPositionDelta = arrowRayDirection * Vector3.forward * 1000f;
    //
    // var arrowRayEnd = arrowRayStart + arrowRayPositionDelta;
    // _lineRenderer.SetPositions(new Vector3[] { arrowRayStart, arrowRayEnd });

    bool strafeArrowButtonPressed = Input.GetMouseButtonDown(0);
    bool slowTimeButtonPressed = Input.GetMouseButtonDown(1);
    bool slowTimeButtonReleased = Input.GetMouseButtonUp(1);

    if (strafeArrowButtonPressed) {
      // FireArrow(arrowRayStart, arrowRayDirection);
      FireArrow(1f, 14f, 0.5f);
    }

    if (slowTimeButtonPressed) {
      Time.timeScale = _slowMotionTimeScale;
    }
    if (slowTimeButtonReleased) {
      Time.timeScale = 1f;
    }
  }

  void FireArrow(float minYLevel, float maxYLevel, float yStep) {
    var arrowRayStart = _mainCamera.transform.position;
    var arrowRayEulerAngles = new Vector3(0f, _mainCamera.transform.eulerAngles.y, 0f);
    var arrowRayDirection = Quaternion.Euler(arrowRayEulerAngles);

    var raycastHits = new RaycastHit[256];
    var enemiesFromCurrentRay = new List<GameObject>();
    var enemiesFromBestRay = new List<GameObject>();
    for (arrowRayStart.y = minYLevel; arrowRayStart.y <= (maxYLevel + 0.001f); arrowRayStart.y += yStep) {
      var numRaycastHits = Physics.RaycastNonAlloc(arrowRayStart, arrowRayDirection * Vector3.forward, raycastHits);
      if (numRaycastHits <= 0) {
        continue;
      }

      // sort raycast hits by distance
      Array.Sort(raycastHits, 0, numRaycastHits, new RaycastHitAscendingDistanceComparer());

      for (int i = 0; i < numRaycastHits; ++i) {
        var raycastHit = raycastHits[i];
        var hitCollider = raycastHit.collider;
        // var hitGameObject = raycastHit.collider.gameObject;

        if (!hitCollider.isTrigger) {
          break;
        }
        if (hitCollider.CompareTag("Enemy")) {
          enemiesFromCurrentRay.Add(hitCollider.gameObject);
        }
      }

      if (enemiesFromCurrentRay.Count > enemiesFromBestRay.Count) {
        enemiesFromBestRay = new List<GameObject>(enemiesFromCurrentRay);
      }
      enemiesFromCurrentRay.Clear();
    }

    foreach (var enemy in enemiesFromBestRay) {
      // TODO death effects
      Destroy(enemy);
    }
  }
}
