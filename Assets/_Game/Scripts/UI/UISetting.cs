using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : UICanvas
{
    public override void Open() {
        base.Open();
        Time.timeScale = 0;
    }

    public override void CloseDirectly() {
        base.CloseDirectly();
        Time.timeScale = 1;
    }

    public void OnBtnContinueClick() {
        GameManager.Ins.ChangeState(GameState.GamePlay);
        CloseDirectly();
    }

    public void OnBtnRetryClick() {
        EventManager.EmitEvent(EventID.Reset);
        CloseDirectly();
    }
}
