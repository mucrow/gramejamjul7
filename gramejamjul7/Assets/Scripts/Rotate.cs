using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate: MonoBehaviour {
  enum Dimension { X, Y, Z };

  [SerializeField] Dimension _dimension = Dimension.Y;
  [SerializeField] float _rate = 90f;

  void Update() {
    var rotation = transform.localRotation.eulerAngles;
    if (_dimension == Dimension.X) {
      rotation.x += _rate * Time.deltaTime;
    }
    else if (_dimension == Dimension.Y) {
      rotation.y += _rate * Time.deltaTime;
    }
    else {
      rotation.z += _rate * Time.deltaTime;
    }
    transform.localRotation = Quaternion.Euler(rotation);
  }
}
