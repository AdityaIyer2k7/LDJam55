using UnityEngine;

public class FireSystem : MonoBehaviour
{
    public GameObject firePrefab;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.GetKeyDown("TRAIL"))
        {
            Block thisBlock = BlockManager.Instance.GetBlockAt(transform.position+transform.forward);
            if (thisBlock.hasFire) return;
            GameObject fire = Instantiate(firePrefab, thisBlock.position, Quaternion.identity);
            thisBlock.hasFire = true;
            thisBlock.fire = fire;
        };
    }
}
