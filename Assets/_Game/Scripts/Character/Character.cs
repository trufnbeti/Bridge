using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ColorObject {
    [SerializeField] private LayerMask groungLayer;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private Transform bricksParent;
    [SerializeField] private Animator anim;
    [SerializeField] protected Transform playerModel;
    [SerializeField] protected float moveSpeed;
    

    private string currentAnim = Anim.idle.ToString();
    private Stack<PlayerBrick> playerBricks = new Stack<PlayerBrick>();

    public int BricksCount => playerBricks.Count;

    public override void OnInit() {
        playerModel.rotation = Quaternion.identity;
    }

    public void AddBrick() {
        PlayerBrick playerBrick = Pool.Ins.Spawn<PlayerBrick>(PoolType.PlayerBrick, bricksParent.position + Vector3.up * BricksCount * Constant.PLAYER_BRICK_HEIGHT, transform.rotation);
        playerBrick.ChangeColor(colorType);
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

    protected bool CanMove(Vector3 nextpos) {
        RaycastHit hit;
        if (Physics.Raycast(nextpos, Vector3.down, out hit, Mathf.Infinity, stairLayer)) {
            BridgeBrick bridgeBrick = CacheComponent.GetBridgeBrick(hit.collider);

            if (bridgeBrick.colorType != colorType && BricksCount == 0 && playerModel.forward.z > 0) return false;
        }

        return true;
    }

    protected void ChangeAnim(Anim ani) {
        string animName = ani.ToString();
        if (currentAnim != animName) {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }


}
