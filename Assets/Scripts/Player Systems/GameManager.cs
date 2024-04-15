using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {get{
        if (instance != null) return instance;
        GameManager im = FindObjectOfType<GameManager>();
        if (im != null) { instance = im; return im; }
        GameObject go = new();
        go.name =  "GameManager";
        DontDestroyOnLoad(go);
        instance = go.AddComponent<GameManager>();
        return instance;
    }}

    public bool inSpellMode = false;
    public float waveTime = 15;
    public float manaTime = 0.2f; // Time to refil 1 mana point
    public int mana = 0;
    public int xp = 0;

    public int playerLvl { get {
        return Mathf.FloorToInt(xp/10);
    } }
    
    public Vector3 playerPos = Vector3.zero;

    [SerializeField]
    int health = 5;
    float lastWaveTime = 0;
    float lastManaTime = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null & instance != this) Destroy(gameObject);
    }

    void Update()
    {
        if (inSpellMode) { lastWaveTime = lastManaTime = Time.time; }
        if (Time.time - lastManaTime > manaTime)
        {
            mana += 1;
            lastManaTime = Time.time;
        }
        if (Time.time - lastWaveTime > waveTime)
        {
            EnemyManager.Instance.Wave();
            lastWaveTime = Time.time;
        }
    }

    public void Harm(int damage)
    {
        health -= damage;
        if (health <= 0) Debug.Log("git gud lol");
    }
}
