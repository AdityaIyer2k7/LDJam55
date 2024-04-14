using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    int hitpoints;
    int damage;
    int speed;
    LevelData levelData;

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

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.Harm(damage);
            DestroySelf();
        }
    }

    public void Init(LevelData levelData)
    {
        this.levelData = levelData;
        hitpoints = levelData.hitpoints;
        damage = levelData.damage;
        speed = levelData.speed;
    }
}
