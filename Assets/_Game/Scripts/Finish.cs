using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Character character = CacheComponent.GetCharacter(other);
        if (character != null)
        {
            Debug.Log("WIN");
            // LevelManager.Instance.OnFinishGame();
            // if (character is Player)
            // {
            //     UIManager.Instance.OpenUI<Victory>();
            // }
            // else
            // {
            //     UIManager.Instance.OpenUI<Fail>();
            // }
            //
            // UIManager.Instance.CloseUI<Gameplay>();

            // GameManager.Instance.ChangeState(GameState.Pause);

            // character.ChangeAnim(Constants.ANIM_DANCE);

            // character.tf.eulerAngles = Vector3.up * 180;
            // character.OnInit();
        }
    }
}
