using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    int nQueued = 0;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.GetKeyDown("SUMMON") & nQueued < GameManager.Instance.playerLvl+1)
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
                if (GameManager.Instance.playerLvl <= CalcPow(levelData) &
                    CalcPow(levelData) <= 2*GameManager.Instance.playerLvl) valid.Add(levelData);
            }
        }
        List<Block> openBlocks =  BlockManager.Instance.GetBorderBlocksOfType(new Block(){hasEnemy=false,hasTrail=false});
        List<Vector3Int> openSquares = new();
        foreach (Block openBlock in openBlocks) openSquares.Add(openBlock.position);
        LevelData enemyToSpawn;
        Vector3Int openSquare;
        GameObject enemy;
        EnemyScript enemyScript;
        for (int i = 0; i < nQueued; i++)
        {
            enemyToSpawn = valid[UnityEngine.Random.Range(0, valid.Count)];
            openSquare = openSquares[UnityEngine.Random.Range(0, openSquares.Count)];
            openSquares.Remove(openSquare);
            enemy = Instantiate(enemyToSpawn.prefab, openSquare, Quaternion.identity, EnemyManager.Instance.transform);
            enemyScript = enemy.AddComponent<EnemyScript>();
            enemyScript.Init(enemyToSpawn, openSquare);
        }
        nQueued = 0;
    }
}
