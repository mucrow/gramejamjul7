using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyPathPatrol: MonoBehaviour {
  [SerializeField] float _speed = 2f;

  [SerializeField] List<Collider> _path;

  Collider _currentWaypoint => _path[_currentWaypointIndex];
  int _currentWaypointIndex = 0;
  Rigidbody _rigidbody;

  void Awake() {
    _rigidbody = GetComponent<Rigidbody>();
  }

  void Start() {
    if (_path != null && _path.Count > 0) {
      SetVelocityTowardCurrentWaypoint();
    }
  }

  void OnTriggerEnter(Collider other) {
    if (other == _currentWaypoint) {
      _currentWaypointIndex = (_currentWaypointIndex + 1) % _path.Count;
      SetVelocityTowardCurrentWaypoint();
    }
  }

  public void SetPath(List<Collider> path) {
    _path = path;
    _currentWaypointIndex = 0;
    SetVelocityTowardCurrentWaypoint();
  }

  void SetVelocityTowardCurrentWaypoint() {
    var currentPosition = transform.position;
    var waypointPosition = _currentWaypoint.transform.position;

    // note that we don't actually move to this position. i'm just leveraging the MoveTowards
    // function for simplicity.
    var positionAfterMove = Vector3.MoveTowards(currentPosition, waypointPosition, _speed);

    _rigidbody.velocity = positionAfterMove - currentPosition;
  }
}
