using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SummonEnemy : MonoBehaviour
{
    int nQueued = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.GetKeyDown("SUMMON") & nQueued < PlayerManager.Instance.playerLvl+1)
        {
            nQueued += 1;
        }
    }

    float CalcPow(LevelData levelData)
    {
        // Power = Damage by enemy + Crystals needed to defeat * Speed + 1 if canDestroyTrail - 1
        return levelData.damage + levelData.hitpoints*levelData.speed + (levelData.canDestoryTrail ? 1 : 0) - 1;
    }

    public void Tick()
    {
        if (nQueued==0) return;
        Enemy[] enemyTypes = Resources.LoadAll<Enemy>("Enemies");
        List<LevelData> valid = new();
        foreach (Enemy enemyType in enemyTypes)
        {
            foreach (LevelData levelData in enemyType.levelData)
            {
                if (PlayerManager.Instance.playerLvl <= CalcPow(levelData) &
                    CalcPow(levelData) <= 2*PlayerManager.Instance.playerLvl) valid.Add(levelData);
            }
        }
        LevelData enemyToSpawn;
        List<Block> openSquares; // Improve code
        Vector3Int openSquare;
        GameObject enemy;
        EnemyScript enemyScript;
        for (int i = 0; i < nQueued; i++)
        {
            enemyToSpawn = valid[UnityEngine.Random.Range(0, valid.Count)];
            openSquares = BlockManager.Instance.GetBorderBlocksOfType(new Block(){hasEnemy=false,hasTrail=false});
            openSquare = openSquares[UnityEngine.Random.Range(0, openSquares.Count)].position;
            enemy = Instantiate(enemyToSpawn.prefab, openSquare, Quaternion.identity, EnemyManager.Instance.transform);
            enemyScript = enemy.AddComponent<EnemyScript>();
            enemyScript.Init(enemyToSpawn);
        }
        nQueued = 0;
    }
}
