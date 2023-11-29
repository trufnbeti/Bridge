using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnFinish() {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Character character = CacheComponent.GetCharacter(other);
        if (character != null) {
            EventManager.EmitEvent(EventID.Finish);

            if (character is Player) {
                EventManager.EmitEvent(EventID.Win);
            } else {
                EventManager.EmitEvent(EventID.Lose);
            }
        }
    }
    
}
