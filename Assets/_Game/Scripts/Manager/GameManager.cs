using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    public Camera camera;
    public Player player;

    [SerializeField] private GameState state = GameState.MainMenu;
    private List<Enemy> enemies = new List<Enemy>();

    private void Awake() {
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
    }

    private void Start() {
        UIManager.Ins.OpenUI<UIMainMenu>();
    }

    private void OnEnable() {
        EventManager.OnEventEmitted += OnEventEmitted;
    }
    private void OnDisable() {
        EventManager.OnEventEmitted -= OnEventEmitted;
    }

    public void ChangeState(GameState state) {
        this.state = state;
    }

    public bool IsState(GameState state) {
        return this.state == state;
    }

    public void AddEnemy(Enemy e) {
        enemies.Add(e);
    }
    
    public void OnStartGame() {
        UIManager.Ins.OpenUI<UIGamePlay>();
        StartCoroutine(WaitForPlay(Constant.TIME_TO_PLAY));
    }

    private void OnFinish() {
        UIManager.Ins.CloseUI<UIGamePlay>();
        ChangeState(GameState.Pause);
        for (int i = 0; i < enemies.Count; ++i) {
            enemies[i].ChangeState(null);
            enemies[i].StopMove();
        }
    }

    private void OnReset() {
        SimplePool.CollectAll();
        enemies.Clear();
        EventManager.EmitEvent(EventID.Start);
    }

    private IEnumerator WaitForPlay(float delayTime) {
        yield return CacheComponent.GetWFS(delayTime);
        ChangeState(GameState.GamePlay);
        for (int i = 0; i < enemies.Count; ++i) {
            enemies[i].ChangeState(new CollectState());
        }
    }
    
    private void OnEventEmitted(EventID eventID) {
        switch (eventID) {
            case EventID.Start:
                OnStartGame();
                break;
            case EventID.Reset:
                OnReset();
                break;
            case EventID.Finish:
                OnFinish();
                break;
            case EventID.Win:
                UIManager.Ins.OpenUI<UIWin>();
                break;
            case EventID.Lose:
                UIManager.Ins.OpenUI<UILose>();
                break;
        }
    }
}
