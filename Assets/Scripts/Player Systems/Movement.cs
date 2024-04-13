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
    }

    public void Tick()
    {
        GameManager.Instance.playerPos += queuedMove;
        if (GameManager.Instance.playerPos.x > GameData.gridDims/2) GameManager.Instance.playerPos.x--;
        if (GameManager.Instance.playerPos.x < -GameData.gridDims/2) GameManager.Instance.playerPos.x++;
        if (GameManager.Instance.playerPos.z > GameData.gridDims/2) GameManager.Instance.playerPos.z--;
        if (GameManager.Instance.playerPos.z < -GameData.gridDims/2) GameManager.Instance.playerPos.z++;
        transform.position = GameManager.Instance.playerPos;
        queuedMove = Vector3Int.zero;
    }
}
