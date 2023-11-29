using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILose : UICanvas
{
    public void OnBtnRetryClick() {
        EventManager.EmitEvent(EventID.Reset);
        CloseDirectly();
    }
}
