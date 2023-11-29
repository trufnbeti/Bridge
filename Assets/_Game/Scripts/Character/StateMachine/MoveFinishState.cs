using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFinishState : IState<Enemy> {
    public void OnEnter(Enemy enemy) {
        enemy.SetDestination(LevelManager.Ins.FinishPoint);
    }

    public void OnExcute(Enemy enemy) {
        if (enemy.BricksCount == 0) {
            enemy.ChangeState(new CollectState());
        }
    }

    public void OnExit(Enemy enemy) { }
}
