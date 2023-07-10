using UnityEngine;

public class Curtains: MonoBehaviour {
  public static Curtains Instance;

  void Awake() {
    if (Instance != null) {
      Destroy(gameObject);
      return;
    }
    Instance = this;
  }
}
