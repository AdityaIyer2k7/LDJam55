using UnityEngine;

public class TrailSystem : MonoBehaviour
{
    public GameObject trailPrefab;

    bool toTrail = false;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.GetKeyDown("TRAIL")) toTrail = true;
    }

    public void Tick()
    {
        if (!toTrail || BlockManager.Instance.GetBlockAt(GameManager.Instance.playerPos).hasTrail) return;
        Instantiate(trailPrefab, GameManager.Instance.playerPos, Quaternion.identity, BlockManager.Instance.transform);
        BlockManager.Instance.GetBlockAt(GameManager.Instance.playerPos).hasTrail = true;
        toTrail = false;
    }
}
