using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager: MonoBehaviour {
  enum State {
    OpeningCurtains,
    Countdown,
    Gameplay,
    ArrowStarved,
    YouWonBanner,
    ClosingCurtains,
  }

  State _state = State.OpeningCurtains;

  void Update() {
    //
  }
}
