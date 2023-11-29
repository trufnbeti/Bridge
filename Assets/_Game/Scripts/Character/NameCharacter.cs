using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NameCharacter : MonoBehaviour
{
	[SerializeField] private Text nameTxt;
	[SerializeField] private bool isPlayer;
	
	private string[] names = { "Alice", "Bob", "Charlie", "David", "Eva", "Frank", "Grace", "Henry", "Ivy", "Jack" };

	private void Start() {
		if (!isPlayer) {
			string randomName = GetRandomName();
			nameTxt.text = randomName;
		}
	}

	private string GetRandomName() {
		int rand = Random.Range(0, names.Length);
		return names[rand];
	}

	private void Update() {
		transform.rotation = Quaternion.LookRotation(transform.position - GameManager.Ins.camera.transform.position);
	}
}
