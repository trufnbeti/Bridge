using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Character : ColorObject {
    [SerializeField] private LayerMask groungLayer;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private Transform bricksParent;
    [SerializeField] private Animator anim;
    [SerializeField] protected Transform playerModel;
    [SerializeField] protected float moveSpeed;
    [HideInInspector] public Stage stage;
    

    private string currentAnim = Anim.idle.ToString();
    private Stack<PlayerBrick> playerBricks = new Stack<PlayerBrick>();
    private bool isOnBridge = false;

    public int BricksCount => playerBricks.Count;

    public override void OnInit() {
        ClearBrick();
        playerModel.rotation = Quaternion.identity;
    }

    public void AddBrick() {
        PlayerBrick playerBrick = SimplePool.Spawn<PlayerBrick>(PoolType.PlayerBrick, bricksParent);
        playerBrick.ChangeColor(colorType);
        playerBrick.transform.localPosition = Vector3.up * BricksCount * Constant.PLAYER_BRICK_HEIGHT;
        playerBrick.transform.SetParent(bricksParent, false);
        playerBricks.Push(playerBrick);
    }

    public void RemoveBrick() {
        if (BricksCount > 0) {
            PlayerBrick playerBrick = playerBricks.Pop();
            SimplePool.Despawn(playerBrick);
        }
    }
    
    public void ChangeAnim(Anim ani) {
        string animName = ani.ToString();
        if (currentAnim != animName) {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    protected Vector3 CheckGround(Vector3 nextPos) {
        Vector3 res = tf.position;
        RaycastHit hit;


        if (Physics.Raycast(nextPos, Vector3.down, out hit, Mathf.Infinity, groungLayer)) {
            res = hit.point + Vector3.up;
            // return hit.point + Vector3.up;
        }

        isOnBridge = Physics.Raycast(tf.position, Vector3.down, out hit, Mathf.Infinity, stairLayer) ? true : false;

        if (Physics.Raycast(nextPos, Vector3.down, Mathf.Infinity, stairLayer) && playerModel.forward.z < 0 && !isOnBridge) {
            res = tf.position;
        }

        return res;
    }

    protected bool CanMove(Vector3 nextpos) {
        RaycastHit hit;
        if (Physics.Raycast(nextpos, Vector3.down, out hit, Mathf.Infinity, stairLayer)) {
            
            BridgeBrick bridgeBrick = CacheComponent.GetBridgeBrick(hit.collider);
            
            if (bridgeBrick.colorType != colorType && playerBricks.Count > 0 && playerModel.forward.z > 0) {
                bridgeBrick.ChangeColor(colorType);
                RemoveBrick();
                stage.SpawnBrick(colorType);
            }

            if (bridgeBrick.colorType != colorType && playerBricks.Count == 0 && playerModel.forward.z > 0) return false;
        }

        return true;
    }

    private void ClearBrick() {
        while (playerBricks.Count > 0) {
            PlayerBrick playerBrick = playerBricks.Pop();
            SimplePool.Despawn(playerBrick);
        }
    }
}
