using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBrick : ColorObject {
    private bool isTakeable;

    private void OnEnable() {
        isTakeable = false;
        StartCoroutine(WaitForTakeable(Constant.TIME_TAKEABLE));
    }

    private IEnumerator WaitForTakeable(float delayTime) {
        yield return CacheComponent.GetWFS(delayTime);
        isTakeable = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (!isTakeable) {
            return;
        }

        if (other.CompareTag(GameTag.Player.ToString())) {
            Character character = CacheComponent.GetCharacter(other);

            if (!character.IsFalling) {
                character.AddBrick();
                OnDespawn();
            }
        }
    }
}
