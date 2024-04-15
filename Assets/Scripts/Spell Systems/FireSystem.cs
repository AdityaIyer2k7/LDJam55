using UnityEngine;

public class FireSystem : MonoBehaviour
{
    public GameObject firePrefab;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.GetKey("TRAIL") & GameManager.Instance.mana >= 25)
        {
            Block thisBlock = BlockManager.Instance.GetBlockAt(transform.position);
            if (thisBlock.hasFire) return;
            GameManager.Instance.mana -= 25;
            GameObject fire = Instantiate(firePrefab, thisBlock.position, Quaternion.identity);
            thisBlock.hasFire = true;
            thisBlock.fire = fire;
        };
    }
}
