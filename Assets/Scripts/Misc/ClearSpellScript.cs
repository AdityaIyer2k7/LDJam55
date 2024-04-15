using UnityEngine;

public class ClearSpellScript : MonoBehaviour
{
    public float aliveTime;

    float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        EnemyScript[] enemyScripts = new EnemyScript[EnemyManager.Instance.enemyScripts.Count];
        EnemyManager.Instance.enemyScripts.CopyTo(enemyScripts);
        EnemyManager.Instance.enemyScripts.Clear();
        foreach (EnemyScript enemyScript in enemyScripts) enemyScript.Harm(1000000);
        if (Time.time - startTime > aliveTime)
        {
            Destroy(gameObject);
        }
    }
}
