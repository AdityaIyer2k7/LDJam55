using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;
    public static EnemyManager Instance
    {get{
        if (instance != null) return instance;
        EnemyManager im = FindObjectOfType<EnemyManager>();
        if (im != null) { instance = im; return im; }
        GameObject go = new();
        go.name = "EnemyManager";
        DontDestroyOnLoad(go);
        instance = go.AddComponent<EnemyManager>();
        return instance;
    }}

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null & instance != this) Destroy(gameObject);
    }

    float CalcPow(LevelData levelData)
    {
        // Power = Damage by enemy + Crystals needed to defeat * 0.2*Speed + 1 if canDestroyFire - 1
        return levelData.damage + 0.2f*levelData.hitpoints*levelData.speed + (levelData.canDestoryFire ? 1 : 0) - 1;
    }

    public void Wave()
    {
        int nQueued = GameManager.Instance.playerLvl*Random.Range(0,2);
        if (nQueued <= 0) return;
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
        List<Block> openBlocks =  BlockManager.Instance.GetBorderBlocks();
        List<Vector3Int> openSquares = new();
        foreach (Block openBlock in openBlocks) openSquares.Add(openBlock.position);
        LevelData enemyToSpawn;
        Vector3Int openSquare;
        GameObject enemy;
        EnemyScript enemyScript;
        for (int i = 0; i < nQueued; i++)
        {
            enemyToSpawn = valid[Random.Range(0, valid.Count)];
            openSquare = openSquares[Random.Range(0, openSquares.Count)];
            openSquares.Remove(openSquare);
            enemy = Instantiate(enemyToSpawn.prefab, openSquare, Quaternion.identity, transform);
            enemyScript = enemy.AddComponent<EnemyScript>();
            enemyScript.Init(enemyToSpawn);
        }
    }
}
