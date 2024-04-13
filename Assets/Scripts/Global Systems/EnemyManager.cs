using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;
    public static EnemyManager Instance
    {get{
        if (instance != null) return instance;
        EnemyManager im = FindObjectOfType<EnemyManager>();
        if (im != null) { instance = im; return im; }
        GameObject go = new() {name = "EnemyManager"};
        DontDestroyOnLoad(go);
        instance = go.AddComponent<EnemyManager>();
        return instance;
    }}

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null & instance != this) Destroy(gameObject);
    }
}
