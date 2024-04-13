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

    public List<EnemyScript> enemies = new();
    public List<EnemyScript> toKill = new();

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null & instance != this) Destroy(gameObject);
    }

    public void Tick()
    {
        foreach (EnemyScript enemy in enemies) enemy.MoveToPlayer();
        foreach (EnemyScript enemy in toKill) enemies.Remove(enemy);
        toKill.Clear();
    }

    public void QueueToKill(EnemyScript enemy) { toKill.Add(enemy); }
}
