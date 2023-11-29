using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWin : UICanvas
{
    public void OnBtnRetryClick() {
        EventManager.EmitEvent(EventID.Reset);
        CloseDirectly();
    }

    public void OnBtnNextClick() {
        EventManager.EmitEvent(EventID.NextLevel);
        CloseDirectly();
    }
}
