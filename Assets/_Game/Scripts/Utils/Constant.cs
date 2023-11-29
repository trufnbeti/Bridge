using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant {
   public const float PLAYER_BRICK_HEIGHT = 0.5f;
   public static readonly int[] MIN_BRICK_TO_BUILD = {4, 6, 8}; // brick to build each level
   public static readonly int[] ROWS = {6, 6, 8}; // row each level
   public static readonly int[] COLS = {6, 8, 8}; // col each level
   public static readonly int[] SPEED = {7, 10, 15}; // speed each level
   public const float STUN_TIME = 1.5f; //falling time
   public const float TIME_TO_PLAY = 2f; //
   public const float TIME_TAKEABLE = 1f; //drop brick
   public const float TIME_TO_HIDE_COUNTDOWN = 0.1f;
}
