using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
	[SerializeField] private DynamicJoystick joystick;
	
	private void Update() {
		Vector3 direct = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;
		Vector3 nextPos = direct * moveSpeed * Time.deltaTime + tf.position;

		if (CanMove(nextPos)) {
			tf.position = CheckGround(nextPos);
		}

		if (direct != Vector3.zero) {
			playerModel.forward = direct;
			ChangeAnim(Anim.run);
		} else {
			ChangeAnim(Anim.idle);
		}
		
	}
}