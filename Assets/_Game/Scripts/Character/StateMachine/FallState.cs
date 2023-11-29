using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : IState<Enemy> {
    private float timer;
    public void OnEnter(Enemy enemy) {
        timer = 0;
        enemy.IsFalling = true;
        enemy.StopMove();
    }

    public void OnExcute(Enemy enemy) {
        timer += Time.deltaTime;
        if (timer >= Constant.STUN_TIME) {
            enemy.ChangeState(new CollectState());
        }
    }

    public void OnExit(Enemy enemy) {
        enemy.IsFalling = false;
    }
}
