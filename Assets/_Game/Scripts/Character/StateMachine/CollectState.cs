using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : IState<Enemy> {

    private int brick;
    public void OnEnter(Enemy enemy) {
        enemy.StopMove();
        int currentLevel = LevelManager.Ins.CurrentLevelIndex;
        brick = Random.Range(Constant.MIN_BRICK_TO_BUILD[currentLevel - 1], 9);
        FindBrick(enemy);
    }

    public void OnExcute(Enemy enemy) {
        if (enemy.CanDestination) {
            enemy.StopMove();
            if (enemy.BricksCount >= brick)
            {
                enemy.ChangeState(new MoveFinishState());
            }
            else
            {
                FindBrick(enemy);
            }
            
        }
    }

    public void OnExit(Enemy enemy) { }

    private void FindBrick(Enemy enemy) {
        if (enemy.stage != null) {
            PlatformBrick platformBrick = enemy.stage.FindBrick(enemy.colorType);

            if (platformBrick == null) {
                //hết brick, đi đến đích
                enemy.ChangeState(new MoveFinishState());
            } else {
                enemy.SetDestination(platformBrick.tf.position);
            }
        }
    }
}
