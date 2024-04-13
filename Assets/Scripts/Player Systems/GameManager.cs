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
    public bool inSpellMode = false;

    public float lastWaveTime = 0;
    public float waveTime = 15;

    [SerializeField]
    int health = 5;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null & instance != this) Destroy(gameObject);
    }

    void Update()
    {
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
