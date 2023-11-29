using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameTag {
	Player
}

public enum Anim {
	idle = 0,
	run = 1,
	dance = 2,
	fall = 3
}

public enum GameState {
	MainMenu = 0,
	GamePlay = 1,
	Pause = 2
}

public enum ColorType {
	None = 0,
	Yellow = 1,
	Grey = 2,
	Blue = 3,
	Green = 4,
	Orange = 5,
	Red = 6
}

public enum PoolType {
	None = 0,
	PlatformBrick = 1,
	BridgeBrick = 2,
	DropBrick = 3,
	PlayerBrick = 4,
	Enemy
}

public enum EventID {
	Start = 0,
	Finish = 1,
	Reset = 2,
	NextLevel = 3,
	Win = 4,
	Lose = 5
}

public enum PrefKey {
	Level = 1
}

public enum ParticleType
{
	BloodExplosionRound = 0,
	SingleThunder = 10,
}