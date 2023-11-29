using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject door;
    
    private void OnTriggerEnter(Collider other) {
        Character character = CacheComponent.GetCharacter(other);

        if (character != null && character.IsOnBridge) {
            Destroy(door);
        }
    }
}
