using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public Vector3Int position;
    public bool hasPlayer;
    public bool hasEnemy;
    public LevelData enemy;
    public bool hasTrail;
}

public class BlockManager : MonoBehaviour
{
    private static BlockManager instance;
    public static BlockManager Instance
    {get{
        if (instance != null) return instance;
        BlockManager im = FindObjectOfType<BlockManager>();
        if (im != null) { instance = im; return im; }
        GameObject go = new() {name = "BlockManager"};
        DontDestroyOnLoad(go);
        instance = go.AddComponent<BlockManager>();
        return instance;
    }}

    public Block[,] blockGrid;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null & instance != this) Destroy(gameObject);
        blockGrid = new Block[53,53];
        for (int i = -4-PlayerManager.Instance.gridDims/2;
             i <= 4+PlayerManager.Instance.gridDims/2; i++)
            for (int j = -4-PlayerManager.Instance.gridDims/2;
                 j <= 4+PlayerManager.Instance.gridDims/2; j++)
                 blockGrid[i,j] = new Block() {position = new Vector3Int(i,0,j)};
    }

    public List<Block> GetBlocksOfType(Block reference)
    {
        List<Block> blocks = new();
        Block block;
        for (int i = -4; i < 5; i++)
        {
            for (int j = -4; j < 5; j++)
            {
                block = blockGrid[
                    i+PlayerManager.Instance.playerPos.x+PlayerManager.Instance.gridDims/2+4,
                    j+PlayerManager.Instance.playerPos.y+PlayerManager.Instance.gridDims/2+4
                ];
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
            if (System.Math.Abs(block.position.x-PlayerManager.Instance.playerPos.x)==4 ||
                System.Math.Abs(block.position.y-PlayerManager.Instance.playerPos.y)==4) blocks.Add(block);
        }
        return blocks;
    }
}
