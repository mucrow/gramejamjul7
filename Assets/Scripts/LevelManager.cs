using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager: MonoBehaviour {
  [SerializeField] Image _banner;
  [SerializeField] Sprite _bannerBlank;
  [SerializeField] Sprite[] _bannerCountdown;
  [SerializeField] Sprite _bannerArrowStarved;
  [SerializeField] Sprite _bannerWin;
  [SerializeField] Curtains _curtains;
  [SerializeField] int _numArrows = 16;

  enum State {
    InitialWait,
    OpeningCurtains,
    Countdown,
    Gameplay,
    ArrowStarving,
    ArrowStarved,
    ClosingCurtainsForRetry,
    YouWonBanner,
    ClosingCurtainsForNextLevel,
  }

  int _countdownNumber;
  float _bannerDuration = 3f;
  float _countdownRate = 1f;
  float _curtainPullRate = 1f / 3f;
  float _initialWait = 1f;
  State _state;
  float _timer;

  void Start() {
    Time.timeScale = 0f;
    _state = State.InitialWait;
    _timer = _initialWait;
  }

  void Update() {
    _timer -= Time.unscaledDeltaTime;

    if (_state == State.InitialWait) {
      // this state exists cause unscaledDeltaTime is jank when a game first starts
      if (_timer <= 0f) {
        _state = State.OpeningCurtains;
        Debug.Log("opening curtains...");
        _timer = _curtainPullRate;
      }
    }
    else if (_state == State.OpeningCurtains) {
      if (_timer <= 0f) {
        bool curtainsAreFullyOpen = _curtains.PullOpen();
        if (curtainsAreFullyOpen) {
          _state = State.Countdown;
          // TODO show countdown display
          _countdownNumber = 3;
          Debug.Log(_countdownNumber);
          _timer += _countdownRate;
        }
        else {
          _timer += _curtainPullRate;
        }
      }
    }
    else if (_state == State.Countdown) {
      if (_timer <= 0f) {
        _countdownNumber -= 1;
        if (_countdownNumber <= 0) {
          Debug.Log("GO!");
          // TODO hide countdown display
          _state = State.Gameplay;
          Time.timeScale = 1f;
        }
        else {
          Debug.Log(_countdownNumber);
          // TODO update countdown display
          _timer += _countdownRate;
        }
      }
    }
    else if (_state == State.Gameplay) {
      if (_numArrows <= 0) {
        _state = State.ArrowStarving;
        Debug.Log("omg am i actually arrow starved rn ???");
      }
      else if (GetNumEnemiesAlive() <= 0) {
        Debug.Log("birdie or smth LMAO");
        SetYouWonState();
      }
    }
    else if (_state == State.ArrowStarving) {
      // this state exists to give enemies a frame to DIE
      if (GetNumEnemiesAlive() <= 0) {
        Debug.Log("nope i won lol");
        SetYouWonState();
      }
      else {
        // TODO show "arrow starved" banner
        _state = State.ArrowStarved;
        Time.timeScale = 0f;
        Debug.Log("yep i fuckign died - showing \"arrow starved\" banner");
        _timer = _bannerDuration;
      }
    }
    else if (_state == State.ArrowStarved) {
      if (_timer <= 0f) {
        _state = State.ClosingCurtainsForRetry;
        Debug.Log("closing curtains...");
        _timer += _curtainPullRate;
      }
    }
    else if (_state == State.ClosingCurtainsForRetry) {
      if (_timer <= 0f) {
        bool curtainsAreFullyClosed = _curtains.PullClosed();
        if (curtainsAreFullyClosed) {
          Debug.Log("reloading level");
          var scene = SceneManager.GetActiveScene();
          SceneManager.LoadScene(scene.buildIndex);
        }
        else {
          _timer += _curtainPullRate;
        }
      }
    }
    else if (_state == State.YouWonBanner) {
      if (_timer <= 0f) {
        _state = State.ClosingCurtainsForNextLevel;
        Debug.Log("closing curtains...");
        _timer = _curtainPullRate;
      }
    }
    else if (_state == State.ClosingCurtainsForNextLevel) {
      if (_timer <= 0f) {
        bool curtainsAreFullyClosed = _curtains.PullClosed();
        if (curtainsAreFullyClosed) {
          Debug.Log("loading next level");
          var scene = SceneManager.GetActiveScene();
          SceneManager.LoadScene(scene.buildIndex + 1);
        }
        else {
          _timer += _curtainPullRate;
        }
      }
    }
    else {
      Debug.LogError("Unhandled LevelManager state: " + _state);
      _state = State.Gameplay;
    }
  }

  public void NotifyArrowFired() {
    _numArrows -= 1;
  }

  void SetYouWonState() {
    // TODO show "you won" banner
    _state = State.YouWonBanner;
    _timer = _bannerDuration;
  }

  int GetNumEnemiesAlive() {
    var enemies = GameObject.FindGameObjectsWithTag("Enemy");
    return enemies.Length;
  }
}
