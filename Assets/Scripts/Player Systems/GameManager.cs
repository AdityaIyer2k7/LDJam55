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

    public int playerLvl;
    public Vector3Int playerPos;

    [SerializeField]
    float lastTickTime = 0;
    [SerializeField]
    float tickTime = 1;
    [SerializeField]
    int health = 5;

    Movement movement;
    SummonEnemy summonEnemy;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null & instance != this) Destroy(gameObject);
        lastTickTime = Time.time;
        movement = GetComponent<Movement>();
        summonEnemy = GetComponent<SummonEnemy>();
    }

    void Update()
    {
        if (Time.time - lastTickTime > tickTime) {
            BlockManager.Instance.GetBlockAt(playerPos).hasPlayer = true;
            EnemyManager.Instance.Tick();
            summonEnemy.Tick();
            movement.Tick();
            lastTickTime = Time.time;
        }
    }

    public void Harm(int damage)
    {
        health -= damage;
        if (health <= 0) Debug.Log("git gud lol");
    }
}
