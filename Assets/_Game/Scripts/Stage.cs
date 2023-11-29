using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {
	[SerializeField] private Transform bricksParent;
	
	private List<Vector3> brickPoints = new List<Vector3>();
	private List<Vector3> emptyPoint = new List<Vector3>();
	private List<PlatformBrick> bricks = new List<PlatformBrick>();

	private void Awake() {
		int levelIndex = Pref.Level;
		for (int i = -Constant.COLS[levelIndex - 1] + 1; i  < Constant.COLS[levelIndex - 1]; i += 2) {
			for (int j = -Constant.ROWS[levelIndex - 1] + 1; j  < Constant.ROWS[levelIndex - 1]; j += 2) {
				brickPoints.Add(bricksParent.position + new Vector3(i, 0, j));
			}
		}
	}

	public void OnInit() {
		for (int i = 0; i < brickPoints.Count; ++i) {
			emptyPoint.Add(brickPoints[i]);
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
		PlatformBrick res = null;
		for (int i = 0; i < bricks.Count; ++i) {
			if (bricks[i].colorType == colorType) {
				res = bricks[i];
			}
		}

		return res;
	}

}
