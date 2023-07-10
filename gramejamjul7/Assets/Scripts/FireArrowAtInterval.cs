using UnityEngine;

// Wrote this as a test script so I could see more clearly that the arrow firing logic was working
public class FireArrowAtInterval: MonoBehaviour {
  [SerializeField] GameObject _arrowPrefab;
  [SerializeField] float _timeBetweenShots = 1f;

  float _timeToNextShot;

  void Start() {
    _timeToNextShot = _timeBetweenShots;
  }

  void Update() {
    _timeToNextShot -= Time.deltaTime;
    if (_timeToNextShot <= 0f) {
      Utils.FireProjectile(transform, _arrowPrefab, Vector3.forward * 0.75f);
      _timeToNextShot += _timeBetweenShots;
    }
  }
}
