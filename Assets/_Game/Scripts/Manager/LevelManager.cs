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
        currentLevelIndex = Pref.Level;
    }

    private void Start() {
        OnReset();
    }
    
    private void OnEnable() {
        EventManager.OnEventEmitted += OnEventEmitted;
    }
    private void OnDisable() {
        EventManager.OnEventEmitted -= OnEventEmitted;
    }

    public void OnInit() {
        //init character pos
        List<Vector3> startPoints = new List<Vector3>();
        for (int i = -NumCharacter + 1; i < NumCharacter; i += 2) {
            startPoints.Add(currentLevel.startPoint.position + new Vector3(i, 0, 0));
        }
        //udpate navmesh
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(currentLevel.navMeshData);
        
        //init color for character
        List<ColorType> colors = new List<ColorType>();
        List<ColorType> exceptColors = new List<ColorType>() { ColorType.Grey, ColorType.None};
        
        while (colors.Count < NumCharacter) {
            ColorType colorType = RandomColor();
            if (!colors.Contains(colorType) && !exceptColors.Contains(colorType)) {
                colors.Add(colorType);
            }
        }
        
        //random pos & color player
        int rand = UnityEngine.Random.Range(0, NumCharacter);
        GameManager.Ins.player.tf.position = startPoints[rand];
        GameManager.Ins.player.tf.rotation = Quaternion.identity;
        GameManager.Ins.player.ChangeColor(colors[rand]);
        startPoints.RemoveAt(rand);
        colors.RemoveAt(rand);
        
        GameManager.Ins.player.OnInit();
        
        //init enemy
        for (int i = 0; i < currentLevel.numBot; ++i) {
            Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.Enemy, startPoints[i], Quaternion.identity);
            enemy.ChangeColor(colors[i]);
            enemy.OnInit();
            GameManager.Ins.AddEnemy(enemy);
        }
        

    }

    public void OnReset() {
        LoadLevel(currentLevelIndex);
        OnInit();
    }

    private void OnNextLevel() {
        currentLevelIndex = currentLevelIndex % levels.Count + 1;
        PlayerPrefs.SetInt(PrefKey.Level.ToString(), currentLevelIndex);
        EventManager.EmitEvent(EventID.Reset);
    }

    private ColorType RandomColor() {
        Array enumValues = Enum.GetValues(typeof(ColorType));
        System.Random random = new System.Random();
        ColorType randomEnumValue = (ColorType)enumValues.GetValue(random.Next(enumValues.Length));
        return randomEnumValue;
    }

    private void LoadLevel(int index) {
        if (currentLevel != null) {
            Destroy(currentLevel.gameObject);
        }
        
        currentLevel = Instantiate(levels[index - 1]);
        currentLevel.OnInit();
    }
    
    private void OnEventEmitted(EventID eventID) {
        switch (eventID) {
            case EventID.Reset: 
                OnReset();
                break;
            case EventID.NextLevel: //next level = tang level + event reset
                OnNextLevel();
                break;
        }
    }

}
