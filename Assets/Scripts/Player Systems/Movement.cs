using System;
using UnityEngine;

/*
 * Using keynames FWD, BCK, LSTRF, RSTRF
 */

public class Movement : MonoBehaviour
{
    public Vector3Int queuedMove = Vector3Int.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.GetKey("FWD")) queuedMove = Vector3Int.forward;
        else if (InputManager.Instance.GetKey("BCK")) queuedMove = Vector3Int.back;
        else if (InputManager.Instance.GetKey("LSTRF")) queuedMove = Vector3Int.left;
        else if (InputManager.Instance.GetKey("RSTRF")) queuedMove = Vector3Int.right;
        else queuedMove = Vector3Int.zero;
    }

    public void Tick()
    {
        PlayerManager.Instance.playerPos = new Vector3Int(
            Math.Clamp(PlayerManager.Instance.playerPos.x+queuedMove.x, -PlayerManager.Instance.gridDims/2, PlayerManager.Instance.gridDims/2),
            0,
            Math.Clamp(PlayerManager.Instance.playerPos.y+queuedMove.y, -PlayerManager.Instance.gridDims/2, PlayerManager.Instance.gridDims/2)
        );
        transform.position = PlayerManager.Instance.playerPos;
    }
}
