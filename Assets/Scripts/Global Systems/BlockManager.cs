using System;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public Vector3Int position;
    public bool hasFire;
    public GameObject fire;
}

public class BlockManager : MonoBehaviour
{
    private static BlockManager instance;
    public static BlockManager Instance
    {get{
        if (instance != null) return instance;
        BlockManager im = FindObjectOfType<BlockManager>();
        if (im != null) { instance = im; return instance; }
        GameObject go = new();
        go.name = "BlockManager";
        DontDestroyOnLoad(go);
        instance = go.AddComponent<BlockManager>();
        return instance;
    }}

    public Block[,] blockGrid;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null & instance != this) Destroy(gameObject);
        blockGrid = new Block[GameData.gridDims+GameData.visibleSz,GameData.gridDims+GameData.visibleSz];
        for (int i=0; i<GameData.gridDims+GameData.visibleSz; i++)
        {
            for (int j=0; j<GameData.gridDims+GameData.visibleSz; j++)
            {
                blockGrid[i,j] = new Block() {position = new Vector3Int(
                    i-(GameData.gridDims+GameData.visibleSz)/2,
                    0,
                    j-(GameData.gridDims+GameData.visibleSz)/2
                )};
            }
        }
    }

    public Block GetBlockAt(Vector3 pos) { return GetBlockAt(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z)); }

    public Block GetBlockAt(Vector3Int pos) { return GetBlockAt(pos.x, pos.z); }

    public Block GetBlockAt(int x, int z)
    {
        return blockGrid[
            x+(GameData.gridDims+GameData.visibleSz)/2,
            z+(GameData.gridDims+GameData.visibleSz)/2
        ];
    }

    public List<Block> GetBorderBlocks()
    {
        List<Block> borderBlocks = new();
        int playerIntPosX = Mathf.RoundToInt(GameManager.Instance.playerPos.x);
        int playerIntPosZ = Mathf.RoundToInt(GameManager.Instance.playerPos.z);
        for (int x = playerIntPosX-GameData.visibleSz/2; x < 1+playerIntPosX+GameData.visibleSz/2; x++)
        {
            borderBlocks.Add(GetBlockAt(x, playerIntPosZ-GameData.visibleSz/2));
            borderBlocks.Add(GetBlockAt(x, playerIntPosZ+GameData.visibleSz/2));
        }
        for (int z = 1+playerIntPosZ-GameData.visibleSz/2; z < playerIntPosZ+GameData.visibleSz/2; z++)
        {
            borderBlocks.Add(GetBlockAt(playerIntPosX-GameData.visibleSz/2, z));
            borderBlocks.Add(GetBlockAt(playerIntPosX+GameData.visibleSz/2, z));
        }
        return borderBlocks;
    }
}
