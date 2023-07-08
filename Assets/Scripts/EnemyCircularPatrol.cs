using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyCircularPatrol: MonoBehaviour {
  [SerializeField] float _speed = 2f;

  [SerializeField] Transform _pivotTransform;

  Vector3 _pivot;
  Rigidbody _rigidbody;
  Vector3 _startPosition;
  bool _hasSnappedThisCycle;

  void Awake() {
    _pivot = _pivotTransform.position;
    _rigidbody = GetComponent<Rigidbody>();
    _startPosition = transform.position;
    _hasSnappedThisCycle = false;
  }

  void FixedUpdate() {
    var currentPosition = transform.position;

    if ((currentPosition - _startPosition).magnitude < 1f) { //arbitrary limit. when we're close to start, time to home in on start position

      if (!_hasSnappedThisCycle) {
        var positionAfterMove = Vector3.MoveTowards(currentPosition, _startPosition, _speed);

        // check: are we now on top of start position?
        if ((positionAfterMove - currentPosition).magnitude < .1f) {
          // set _hasSnappedThisCycle, we will start moving on normal circle path again
          _hasSnappedThisCycle = true;
        } else {
          // move in straight line directly towards start
          Vector3 direction = (_startPosition - currentPosition).normalized;
          _rigidbody.velocity = direction * _speed;
        }
      } else {
        Vector3 direction = Vector3.Cross(_pivot - currentPosition, Vector3.up).normalized;
        _rigidbody.velocity = direction * _speed;
      }
      
    } else {
      if (_hasSnappedThisCycle) {
        // if we're outside the snap range, reset this var so we can snap again once we go around the loop
        _hasSnappedThisCycle = false;
      }

      Vector3 direction = Vector3.Cross(_pivot - currentPosition, Vector3.up).normalized;
      _rigidbody.velocity = direction * _speed;
    }
  }


}
