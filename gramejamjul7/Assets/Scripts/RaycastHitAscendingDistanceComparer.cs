using System.Collections.Generic;
using UnityEngine;

public class RaycastHitAscendingDistanceComparer: IComparer<RaycastHit> {
  public int Compare(RaycastHit a, RaycastHit b) {
    // ReSharper disable once CompareOfFloatsByEqualityOperator
    if (a.distance == b.distance) {
      return 0;
    }
    if (a.distance > b.distance) {
      return 1;
    }
    return -1;
  }
}
