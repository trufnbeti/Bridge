using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameTag {
	Player
}

public enum Anim {
	idle = 0,
	run = 1
}

public enum ColorType {
	None = 0,
	White = 1,
	Grey = 2,
	Blue = 3,
	Green = 4,
	Orange = 5,
	Red = 6,
	Yellow = 7
}

public enum PoolType {
	None = 0,
	PlatformBrick = 1,
	BridgeBrick = 2,
	DropBrick = 3,
	PlayerBrick = 4,
	Enemy
}

public enum ParticleType
{
	BloodExplosionRound = 0,
	SingleThunder = 10,
}