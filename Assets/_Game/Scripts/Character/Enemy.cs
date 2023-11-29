using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Enemy : Character
{
	[SerializeField] private NavMeshAgent nav;
	
	private IState<Enemy> currentState;
	private Vector3 destinationPos;

	public override void OnInit() {
		base.OnInit();
		nav.speed = Constant.SPEED[LevelManager.Ins.CurrentLevelIndex - 1];
	}

	public bool CanDestination => Vector3.Distance(destinationPos, tf.position.x * Vector3.right + tf.position.z * Vector3.forward) < 0.1f;

	public void ChangeState(IState<Enemy> state) {
		if (currentState != null) {
			currentState.OnExit(this);
		}

		currentState = state;

		if (currentState != null) {
			currentState.OnEnter(this);
		}
	}

	public void SetDestination(Vector3 target) {
		ChangeAnim(Anim.run);
		nav.enabled = true;
		destinationPos = target;
		destinationPos.y = 0;
		nav.SetDestination(target);
	}

	public void StopMove() {
		nav.enabled = false;
	}

	protected override void OnFalling() {
		base.OnFalling();
		ChangeState(new FallState());
	}
	
	private void Update() {
		if (GameManager.Ins.IsState(GameState.GamePlay) && currentState != null)
		{
			currentState.OnExcute(this);
			CanMove(tf.position); //check stair
		}
	}
}
