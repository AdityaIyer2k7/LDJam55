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

    public int playerLvl = 1;
    public Vector3Int playerPos = Vector3Int.zero;
    public bool inSpellMode = false;

    [SerializeField]
    float lastTickTime = 0;
    [SerializeField]
    float tickTime = 1;
    [SerializeField]
    int health = 5;

    Movement movement;
    TrailSystem trailSystem;
    EnemySystem enemySystem;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null & instance != this) Destroy(gameObject);
        lastTickTime = Time.time;
        movement = GetComponent<Movement>();
        trailSystem = GetComponent<TrailSystem>();
        enemySystem = GetComponent<EnemySystem>();
    }

    void Update()
    {
        if (Time.time - lastTickTime > tickTime) {
            /* 1) Player moves
             * 2) Trails are summoned
             * 3) Enemies move
             * 4) Enemies are summoned
             */
            BlockManager.Instance.GetBlockAt(playerPos).hasPlayer = false;
            movement.Tick();
            BlockManager.Instance.GetBlockAt(playerPos).hasPlayer = true;
            trailSystem.Tick();
            EnemyManager.Instance.Tick();
            enemySystem.Tick();
            lastTickTime = Time.time;
        }
    }

    public void Harm(int damage)
    {
        health -= damage;
        if (health <= 0) Debug.Log("git gud lol");
    }
}
