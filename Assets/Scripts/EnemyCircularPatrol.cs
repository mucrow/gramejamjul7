using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyCircularPatrol: MonoBehaviour {
  [SerializeField] float _speed = 2f;

  [SerializeField] Transform _pivotTransform;

  Vector3 _pivot;
  Rigidbody _rigidbody;

  void Awake() {
    _pivot = _pivotTransform.position;
    _rigidbody = GetComponent<Rigidbody>();
  }

  void FixedUpdate() {
    var currentPosition = transform.position;
    Vector3 direction = Vector3.Cross(_pivot - currentPosition, Vector3.up).normalized;
    _rigidbody.velocity = direction * _speed;
  }
}
