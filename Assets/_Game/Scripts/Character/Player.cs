using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
	[SerializeField] private DynamicJoystick joystick;
	[SerializeField] private float moveSpeed;
	

	protected override void OnFalling() {
		base.OnFalling();
		StartCoroutine(WaitForStand(Constant.STUN_TIME));
	}

	private IEnumerator WaitForStand(float delayTime) {
		yield return CacheComponent.GetWFS(delayTime);
		isFalling = false;
		ChangeAnim(Anim.idle);
	}

	private void Move() {
		if (GameManager.Ins.IsState(GameState.GamePlay) && !isFalling) {
			Vector3 direct = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;
			Vector3 nextPos = direct * moveSpeed * Time.deltaTime + tf.position;

			if (CanMove(nextPos)) {
				tf.position = CheckGround(nextPos);
			}

			if (Vector3.Distance(direct, Vector3.zero) > 0) {
				playerModel.forward = direct;
				ChangeAnim(Anim.run);
			} else {
				ChangeAnim(Anim.idle);
			}
		}
	}

	private void Update() {
		Move();
	}
}