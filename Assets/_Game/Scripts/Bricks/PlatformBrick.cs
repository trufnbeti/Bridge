using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBrick : ColorObject {
	[HideInInspector] public Stage stage;

	public override void OnDespawn() {
		base.OnDespawn();
		stage.RemoveBrick(this);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(GameTag.Player.ToString())) {
			Character character = CacheComponent.GetCharacter(other);
			if (colorType == character.colorType) {
				character.AddBrick();
				OnDespawn();
			}
		}
	}
}
