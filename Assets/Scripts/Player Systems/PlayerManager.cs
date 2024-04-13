using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance
    {get{
        if (instance != null) return instance;
        PlayerManager im = FindObjectOfType<PlayerManager>();
        if (im != null) { instance = im; return im; }
        GameObject go = new() {name = "PlayerManager"};
        DontDestroyOnLoad(go);
        instance = go.AddComponent<PlayerManager>();
        return instance;
    }}

    public int gridDims = 45;

    public int playerLvl;
    public Vector3Int playerPos;

    [SerializeField]
    float lastTickTime = 0;
    [SerializeField]
    float tickTime = 1;

    Movement movement;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null & instance != this) Destroy(gameObject);
        lastTickTime = Time.time;
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        if (Time.time - lastTickTime > tickTime) {
            movement.Tick();
            lastTickTime = Time.time;
        }
    }
}
