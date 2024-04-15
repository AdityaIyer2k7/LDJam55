using System;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    GameObject deathParticles;
    int hitpoints;
    int damage;
    float speed;
    LevelData levelData;

    void Start()
    {
        deathParticles = Resources.Load<GameObject>("Prefabs/Death/DeathParticles");
    }

    void Update()
    {
        if (GameManager.Instance.inSpellMode) return;
        transform.forward = GameManager.Instance.transform.position - transform.position;
        transform.position += transform.forward * speed * Time.deltaTime;
        Block thisBlock = BlockManager.Instance.GetBlockAt(transform.position);
        if (levelData.canDestoryFire & thisBlock.hasFire) {
            thisBlock.hasFire = false;
            Destroy(thisBlock.fire);
        }
    }

    public void Harm(int damage)
    {
        hitpoints -= damage;
        if (hitpoints <= 0) DestroySelf();
    }

    void DestroySelf()
    {
        GameManager.Instance.mana = Math.Min(GameManager.Instance.mana+20, 100);
        GameManager.Instance.xp += 1;
        EnemyManager.Instance.enemyScripts.Remove(this);
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.Harm(damage);
            DestroySelf();
        }
        if (other.gameObject.tag == "Structure")
        {
            SpellStructure structure = other.gameObject.GetComponent<SpellStructure>();
            bool toDestroySelf = structure.health >= damage;
            structure.Harm(damage);
            if (toDestroySelf) DestroySelf();
        }
    }

    public void Init(LevelData levelData)
    {
        this.levelData = levelData;
        hitpoints = levelData.hitpoints;
        damage = levelData.damage;
        speed = levelData.speed;
        EnemyManager.Instance.enemyScripts.Add(this);
    }
}
