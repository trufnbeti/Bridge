using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<Level> levels;

    public Vector3 FinishPoint => currentLevel.finishPoint.position;
    private int currentLevelIndex;

    private Level currentLevel;
    public int NumCharacter => currentLevel.numBot + 1;

    public int CurrentLevelIndex => currentLevelIndex;

    private void Awake() {
        currentLevelIndex = 1;
    }

    private void Start() {
        LoadLevel(currentLevelIndex);
    }

    public void OnInit() {
        //init character pos
        List<Vector3> startPoints = new List<Vector3>();
        for (float i = -NumCharacter + 1; i < NumCharacter; i += Constant.SPACE_BETWEEN_CHARACTER) {
            startPoints.Add(currentLevel.startPoint.position + new Vector3(i, 0, 0));
        }
        //udpate navmesh
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(currentLevel.navMeshData);
        
        //random color
        List<ColorType> colors = new List<ColorType>();
        List<ColorType> exceptColors = new List<ColorType>();
        exceptColors.Add(ColorType.Grey);
        exceptColors.Add(ColorType.None);
        exceptColors.Add(ColorType.White);
        ;
        while (colors.Count < 4) {
            ColorType colorType = RandomColor();
            if (!colors.Contains(colorType) && !exceptColors.Contains(colorType)) {
                colors.Add(colorType);
            }
        };
    }

    private ColorType RandomColor() {
        Array enumValues = Enum.GetValues(typeof(ColorType));
        System.Random random = new System.Random();
        ColorType randomEnumValue = (ColorType)enumValues.GetValue(random.Next(enumValues.Length));
        return randomEnumValue;
    }

    private void LoadLevel(int index) {
        if (currentLevel != null) {
            Destroy(currentLevel);
        }

        currentLevel = Instantiate(levels[index - 1]);
        currentLevel.OnInit();
    }
}
