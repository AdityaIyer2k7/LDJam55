using UnityEngine;

public class FireballSpellScript : MonoBehaviour
{
    public int damage;
    public float aliveTime;

    float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time - startTime > aliveTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyScript>().Harm(damage);
        }
    }
}
