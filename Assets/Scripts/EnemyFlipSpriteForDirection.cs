using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlipSpriteForDirection : MonoBehaviour
{
    Camera _mainCamera;
    SpriteRenderer _spriteRenderer;
    Rigidbody _rigidbody;

    void Awake() 
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 comparisonVector;
        if (_rigidbody.velocity.magnitude > 0.1f) {
            comparisonVector = _rigidbody.velocity.normalized;
        } else {
            comparisonVector = _rigidbody.transform.forward;
        }

        if (Vector3.Dot(Vector3.Cross(_mainCamera.transform.forward, comparisonVector), _mainCamera.transform.right) > 0) {
             _spriteRenderer.flipX = true;
        } else {
            _spriteRenderer.flipX = false;
        }
    }
}
