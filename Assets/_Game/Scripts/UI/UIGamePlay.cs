using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : UICanvas
{
    [SerializeField] private Text levelTxt;
    [SerializeField] private Countdown count;
    
    private void OnEnable() {
        OnShowCountdown();
        levelTxt.text = "Level: " + LevelManager.Ins.CurrentLevelIndex;
        EventManager.OnEventEmitted += OnEventEmitted;
    }
    private void OnDisable() {
        EventManager.OnEventEmitted -= OnEventEmitted;
    }

    private void OnShowCountdown() {
        count.gameObject.SetActive(true);
    }

    private void OnEventEmitted(EventID eventID) {
        switch (eventID) {
            case EventID.Reset:
                OnShowCountdown();
                break;
        }
    }

    public void OnBtnSettingClick() {
        GameManager.Ins.ChangeState(GameState.Pause);
        UIManager.Ins.OpenUI<UISetting>();
    }
    
    
}
