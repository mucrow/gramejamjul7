using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour {
    [SerializeField] float jumpInterval;
    [SerializeField] float jumpStrength;
    [SerializeField] float gravityStrength;
    Rigidbody _rigidbody;

    float _timeSinceLastJump;
    float _startingY;

    // Start is called before the first frame update
    void Start() {
        _timeSinceLastJump = 0;
        _rigidbody = GetComponent<Rigidbody>();
        _startingY = transform.position.y;
    }

    void FixedUpdate() {
        _timeSinceLastJump += Time.fixedDeltaTime;
        if (_timeSinceLastJump > jumpInterval) {
            _timeSinceLastJump = 0;
            _rigidbody.AddForce(_rigidbody.transform.up * jumpStrength);
            //transform.position += Vector3.up * 1;
        }

        if (transform.position.y < _startingY) {
            _rigidbody.velocity += Vector3.up * -1 * _rigidbody.velocity.y;
            var newPosition = transform.position + (Vector3.up * (_startingY - transform.position.y));
            _rigidbody.Move(newPosition, transform.rotation);
        } else {
            _rigidbody.velocity += Vector3.down * gravityStrength;
        }
    }
}
