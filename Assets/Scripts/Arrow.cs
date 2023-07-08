using System;
using UnityEngine;

public class Arrow: MonoBehaviour {
  [Header("This vector will be normalized.")]
  [SerializeField] Vector3 _direction = new Vector3(0f, 0, 1f);

  [SerializeField] float _speed = 50f;

  Rigidbody _rigidbody;

  void Awake() {
    _rigidbody = GetComponent<Rigidbody>();
  }

  void Start() {
    _rigidbody.velocity = transform.localRotation * Vector3.forward * _speed;
  }

  void OnCollisionEnter(Collision other) {
    // TODO probably want handling for game logic and particle effects here
    Destroy(gameObject);
  }
}
