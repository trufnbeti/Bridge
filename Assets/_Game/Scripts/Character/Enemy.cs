using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
	[SerializeField] private NavMeshAgent nav;
	private IState<Enemy> currentState;

	public override void OnInit() {
		base.OnInit();
		ChangeAnim(Anim.idle);
	}

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
		nav.enabled = true;
		nav.SetDestination(target);
	}

	public void StopMove() {
		nav.enabled = false;
	}
}
