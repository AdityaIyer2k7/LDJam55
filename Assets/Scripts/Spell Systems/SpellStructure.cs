using UnityEngine;

public class SpellStructure : MonoBehaviour
{
    public int health = 0;

    public void Harm(int damage) {
        health -= damage;
        if (health <= 0) DestroySelf();
    }

    public void DestroySelf() { Destroy(gameObject); }
}