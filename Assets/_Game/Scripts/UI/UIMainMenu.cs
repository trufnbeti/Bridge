using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    public void OnClickPlayButton() {
        EventManager.EmitEvent(EventID.Start);
        CloseDirectly();
    }
}
