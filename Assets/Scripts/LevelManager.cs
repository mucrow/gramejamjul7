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
  [SerializeField] Text _winBannerArrowCount;

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
  int _startingArrowCount;

  void Start() {
    _startingArrowCount = _numArrows;
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
        _timer = _curtainPullRate;
      }
    }
    else if (_state == State.OpeningCurtains) {
      if (_timer <= 0f) {
        bool curtainsAreFullyOpen = _curtains.PullOpen();
        if (curtainsAreFullyOpen) {
          _state = State.Countdown;
          _countdownNumber = 3;
          _banner.sprite = _bannerCountdown[_countdownNumber];
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
          _banner.sprite = _bannerBlank;
          _state = State.Gameplay;
          Time.timeScale = 1f;
        }
        else {
          _banner.sprite = _bannerCountdown[_countdownNumber];
          _timer += _countdownRate;
        }
      }
    }
    else if (_state == State.Gameplay) {
      if (_numArrows <= 0) {
        _state = State.ArrowStarving;
      }
      else if (GetNumEnemiesAlive() <= 0) {
        SetYouWonState();
      }
    }
    else if (_state == State.ArrowStarving) {
      // this state exists to give enemies a frame to DIE
      if (GetNumEnemiesAlive() <= 0) {
        SetYouWonState();
      }
      else {
        _banner.sprite = _bannerArrowStarved;
        _state = State.ArrowStarved;
        Time.timeScale = 0f;
        _timer = _bannerDuration;
      }
    }
    else if (_state == State.ArrowStarved) {
      if (_timer <= 0f) {
        _banner.sprite = _bannerBlank;
        _state = State.ClosingCurtainsForRetry;
        _timer += _curtainPullRate;
      }
    }
    else if (_state == State.ClosingCurtainsForRetry) {
      if (_timer <= 0f) {
        bool curtainsAreFullyClosed = _curtains.PullClosed();
        if (curtainsAreFullyClosed) {
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
        _banner.sprite = _bannerBlank;
        _winBannerArrowCount.text = "";
        _state = State.ClosingCurtainsForNextLevel;
        _timer = _curtainPullRate;
      }
    }
    else if (_state == State.ClosingCurtainsForNextLevel) {
      if (_timer <= 0f) {
        bool curtainsAreFullyClosed = _curtains.PullClosed();
        if (curtainsAreFullyClosed) {
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
    _banner.sprite = _bannerWin;
    _winBannerArrowCount.text = (_startingArrowCount - _numArrows) + " arrows";
    _state = State.YouWonBanner;
    _timer = _bannerDuration;
  }

  int GetNumEnemiesAlive() {
    var enemies = GameObject.FindGameObjectsWithTag("Enemy");
    return enemies.Length;
  }
}
