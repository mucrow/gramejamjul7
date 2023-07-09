using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanBackgroundUpAndDown : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float a;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * Mathf.Sin(Time.time * speed) * a;
        transform.position += Vector3.right * Mathf.Cos(Time.time * speed * 1.2f) * a;
    }
}
