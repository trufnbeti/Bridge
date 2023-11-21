using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ColorObject {
    [SerializeField] private LayerMask groungLayer;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private Transform bricksParent;
    [SerializeField] private Transform playerModel;
    [SerializeField] private Animator anim;
    [SerializeField] private float moveSpeed;
    

    private string currentAnim;
    private Stack<PlayerBrick> playerBricks = new Stack<PlayerBrick>();

    public int BricksCount => playerBricks.Count;

    public override void OnInit() {
        playerModel.rotation = Quaternion.identity;
    }

    public void AddBrick() {
        PlayerBrick playerBrick = Pool.Ins.Spawn<PlayerBrick>(PoolType.PlayerBrick, bricksParent.position + Vector3.up * BricksCount * Constant.PLAYER_BRICK_HEIGHT, transform.rotation);
        playerBrick.transform.SetParent(bricksParent);
        playerBricks.Push(playerBrick);
    }

    protected Vector3 CheckGround(Vector3 nextPos) {
        RaycastHit hit;

        if (Physics.Raycast(nextPos, Vector3.down, out hit, Mathf.Infinity, groungLayer)) {
            return hit.point + Vector3.up;
        }

        return tf.position;
    }

    // protected void ChangeAnim(Anim ani) {
    //     string animName = ani.ToString();
    //     if (currentAnim != animName) {
    //         anim.ResetTrigger(animName);
    //         if(currentAnim != null) {
    //             anim.ResetTrigger(currentAnim);
    //         }
    //         
    //         currentAnim = animName;
    //         anim.SetTrigger(currentAnim);
    //     }
    // }


}
