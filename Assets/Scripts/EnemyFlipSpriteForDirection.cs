using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlipSpriteForDirection : MonoBehaviour
{
    Camera _mainCamera;
    SpriteRenderer _spriteRenderer;
    Transform _rigidbody;

    void Awake() 
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Dot(Vector3.Cross(_mainCamera.transform.forward, _rigidbody.forward), _mainCamera.transform.right) > 0) {
             _spriteRenderer.flipX = true;
        } else {
            _spriteRenderer.flipX = false;
        }
    }
}
