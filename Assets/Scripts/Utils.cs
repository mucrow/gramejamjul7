using UnityEngine;

public class Utils {
  public static GameObject FireProjectile(Transform cannon, GameObject projectilePrefab, Vector3 offsetFromCannon) {
    var newProjectile = GameObject.Instantiate(projectilePrefab, cannon.position, cannon.rotation, cannon);
    newProjectile.transform.localPosition = offsetFromCannon;
    newProjectile.transform.parent = null;
    return newProjectile;
  }
}
