using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : IState<Enemy> {

    private int brick;
    public void OnEnter(Enemy enemy) {
        enemy.ChangeAnim(Anim.idle);
        int currentLevel = LevelManager.Ins.CurrentLevelIndex;
        brick = Random.Range(Constant.MIN_BRICK_TO_BUILD[currentLevel], 9);
    }

    public void OnExcute(Enemy enemy) {
        throw new System.NotImplementedException();
    }

    public void OnExit(Enemy enemy) {
        throw new System.NotImplementedException();
    }

    private void FindBrick(Enemy enemy) {
        if (enemy.stage != null) {
            PlatformBrick platformBrick = enemy.stage.FindBrick(enemy.colorType);

            if (platformBrick != null) {
                //hết brick, đi đến đích
                enemy.ChangeState(new MoveFinishState());
            } else {
                enemy.SetDestination(platformBrick.tf.position);
            }
        }
    }
}
