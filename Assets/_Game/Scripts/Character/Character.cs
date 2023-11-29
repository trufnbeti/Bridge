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
    [HideInInspector] public Stage stage;
    
    protected bool isFalling = false;

    private string currentAnim = Anim.idle.ToString();
    private Stack<PlayerBrick> playerBricks = new Stack<PlayerBrick>();
    private bool  isOnBridge = false;

    public bool IsOnBridge => isOnBridge;
    public bool IsFalling {
        get => isFalling;
        set => isFalling = value;
    }

    public int BricksCount => playerBricks.Count;

    public override void OnInit() {
        ClearBrick();
        playerModel.rotation = Quaternion.identity;
        ChangeAnim(Anim.idle);
    }
    
    private void OnEnable() {
        EventManager.OnEventEmitted += OnEventEmitted;
    }
    private void OnDisable() {
        EventManager.OnEventEmitted -= OnEventEmitted;
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
        }

        if (Physics.Raycast(nextPos, Vector3.down, Mathf.Infinity, stairLayer) && playerModel.forward.z < 0 && !isOnBridge) {
            res = tf.position;
        }

        return res;
    }

    protected bool CanMove(Vector3 pos) {
        bool res = true;
        isOnBridge = Physics.Raycast(tf.position, Vector3.down, Mathf.Infinity, stairLayer) ? true : false;
        RaycastHit hit;
        if (Physics.Raycast(pos, Vector3.down, out hit, Mathf.Infinity, stairLayer)) {
            
            BridgeBrick bridgeBrick = CacheComponent.GetBridgeBrick(hit.collider);
            
            if (bridgeBrick.colorType != colorType && playerBricks.Count > 0 && playerModel.forward.z > 0) {
                bridgeBrick.ChangeColor(colorType);
                RemoveBrick();
                stage.SpawnBrick(colorType);
            }

            if (bridgeBrick.colorType != colorType && playerBricks.Count == 0 && playerModel.forward.z > 0) {
                res = false;
            }
        }

        return res;
    }

    protected virtual void OnFalling() {
        while (BricksCount > 0) {
            SimplePool.Spawn<DropBrick>(PoolType.DropBrick, transform.position, Quaternion.identity);
            RemoveBrick();
        }
        isFalling = true;
        ChangeAnim(Anim.fall);
    }

    private void ClearBrick() {
        while (BricksCount > 0) {
            PlayerBrick playerBrick = playerBricks.Pop();
            SimplePool.Despawn(playerBrick);
        }
    }

    private void OnFinish() {
        OnInit();
        ChangeAnim(Anim.dance);
    }
    
    private void OnEventEmitted(EventID eventID) {
        switch (eventID) {
            case EventID.Finish:
                OnFinish();
                break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(GameTag.Player.ToString())) {
            Character character = CacheComponent.GetCharacter(other);
            if (BricksCount < character.BricksCount && !isOnBridge) {
                OnFalling();
                ClearBrick();
            }
        }
    }
}
