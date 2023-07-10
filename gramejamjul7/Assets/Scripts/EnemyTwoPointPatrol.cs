using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyPathPatrol))]
public class EnemyTwoPointPatrol: MonoBehaviour {
  [SerializeField] GameObject _waypointPrefab;
  [SerializeField] Transform _secondWaypointPosition;

  EnemyPathPatrol _enemyPathPatrol;

  void Awake() {
    _enemyPathPatrol = GetComponent<EnemyPathPatrol>();
  }

  void Start() {
    // these both need to be created as new objects so their positions do not change as the enemy
    // moves around.
    var firstWaypoint = MakeWaypointAtPosition(transform.position);
    var secondWaypoint = MakeWaypointAtPosition(_secondWaypointPosition.position);
    var path = new List<Collider>() { firstWaypoint, secondWaypoint };
    _enemyPathPatrol.SetPath(path);
  }

  Collider MakeWaypointAtPosition(Vector3 position) {
    var waypointGameObject = Instantiate(_waypointPrefab, position, Quaternion.identity);
    return waypointGameObject.GetComponent<Collider>();
  }
}
