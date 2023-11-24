using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {
	[SerializeField] private Transform bricksParent;
	private List<Vector3> brickPoints = new List<Vector3>();
	private List<Vector3> emptyPoint = new List<Vector3>();
	private List<PlatformBrick> bricks = new List<PlatformBrick>();

	private void Awake() {
		for (float i = -Constant.BRICK_ROWS  + 1; i  < Constant.BRICK_ROWS; i += Constant.SPACE_BETWEEN_PLATFORM_BRICK) {
			for (float j = -Constant.BRICK_COLS  + 1; j  < Constant.BRICK_COLS; j += Constant.SPACE_BETWEEN_PLATFORM_BRICK) {
				brickPoints.Add(bricksParent.position + new Vector3(i, 0, j));
			}
		}
	}

	public void OnInit() {
		foreach (var item in brickPoints) {
			emptyPoint.Add(item);	
		}
	}

	public void InitColor(ColorType colorType) {
		int amount = brickPoints.Count / LevelManager.Ins.NumCharacter;

		for (int i = 0; i < amount; ++i) {
			SpawnBrick(colorType);
		}
	}
	public void SpawnBrick(ColorType colorType)
	{
		if (emptyPoint.Count > 0)
		{
			int rand = Random.Range(0, emptyPoint.Count);
			PlatformBrick brick = SimplePool.Spawn<PlatformBrick>(PoolType.PlatformBrick, emptyPoint[rand], Quaternion.identity);
			brick.stage = this;
			brick.ChangeColor(colorType);
			brick.transform.SetParent(bricksParent);
			emptyPoint.RemoveAt(rand);
			bricks.Add(brick);
		}
	}
	
	public void RemoveBrick(PlatformBrick brick)
	{
		emptyPoint.Add(brick.tf.position);
		bricks.Remove(brick);
	}

	public PlatformBrick FindBrick(ColorType colorType) {
		foreach (var brick in bricks) {
			if (brick.colorType == colorType) return brick;
		}

		return null;
	}

}
