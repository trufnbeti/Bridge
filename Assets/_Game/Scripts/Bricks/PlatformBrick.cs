using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBrick : ColorObject
{
    private T RandomColor<T>() {
        System.Array enumValues = System.Enum.GetValues(typeof(T));
        T randomEnumValue = (T)enumValues.GetValue(Random.Range(3, enumValues.Length));
        return randomEnumValue;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(GameTag.Player.ToString())) {
            if (GameManager.Ins.player.colorType == this.colorType) {
                GameManager.Ins.player.AddBrick();
                Destroy(gameObject);
            }
        }
    }
}
