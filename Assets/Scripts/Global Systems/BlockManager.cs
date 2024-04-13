using System;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public Vector3Int position;
    public bool hasPlayer;
    public bool hasEnemy;
    public EnemyScript enemy;
    public bool hasTrail;
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
            for (int j=0; j<GameData.gridDims+GameData.visibleSz; j++)
                blockGrid[i,j] = new Block() {position = new Vector3Int(
                    i-(GameData.gridDims+GameData.visibleSz)/2,
                    0,
                    j-(GameData.gridDims+GameData.visibleSz)/2
                )};
    }

    public Block GetBlockAt(Vector3Int pos)
    {
        return blockGrid[
            pos.x+(GameData.gridDims+GameData.visibleSz)/2,
            pos.z+(GameData.gridDims+GameData.visibleSz)/2
        ];
    }

    public Block GetBlockAt(int x, int z)
    {
        return blockGrid[
            x+(GameData.gridDims+GameData.visibleSz)/2,
            z+(GameData.gridDims+GameData.visibleSz)/2
        ];
    }

    public List<Block> GetBlocksOfType(Block reference)
    {
        List<Block> blocks = new();
        Block block;
        for (int i = -GameData.visibleSz/2; i < 1+GameData.visibleSz/2; i++)
        {
            for (int j = -GameData.visibleSz/2; j < 1+GameData.visibleSz/2; j++)
            {
                block = GetBlockAt(
                    i+GameManager.Instance.playerPos.x,
                    j+GameManager.Instance.playerPos.z
                );
                if (block.hasEnemy==reference.hasEnemy & block.hasPlayer==reference.hasPlayer & block.hasTrail==reference.hasTrail)
                {
                    if (!block.hasEnemy || (block.hasEnemy & block.enemy==reference.enemy)) blocks.Add(block);
                }
            }
        }
        return blocks;
    }

    public List<Block> GetBorderBlocksOfType(Block reference)
    {
        List<Block> blocks = new();
        List<Block> allBlocks = GetBlocksOfType(reference);
        foreach (Block block in allBlocks)
        {
            if (Math.Abs(block.position.x-GameManager.Instance.playerPos.x)==GameData.visibleSz/2 ||
                Math.Abs(block.position.z-GameManager.Instance.playerPos.z)==GameData.visibleSz/2) blocks.Add(block);
        }
        return blocks;
    }
}
