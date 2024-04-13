using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Vector3Int position;
    int hitpoints;
    int damage;
    int speed;
    LevelData levelData;

    public void MoveToPlayer()
    {
        int delX = GameManager.Instance.playerPos.x - position.x;
        int delZ = GameManager.Instance.playerPos.z - position.z;
        Block oldBlock = BlockManager.Instance.GetBlockAt(position), newBlock;
        if (Math.Abs(delX) > Math.Abs(delZ) & !BlockManager.Instance.GetBlockAt(position+Vector3Int.right*Math.Sign(delX)).hasEnemy)
        {
            oldBlock.hasEnemy = false;
            oldBlock.enemy = null;
            position = position+Vector3Int.right*Math.Sign(delX);
            newBlock = BlockManager.Instance.GetBlockAt(position);
            newBlock.hasEnemy = true;
            newBlock.enemy = this;
            if (newBlock.hasPlayer) {
                GameManager.Instance.Harm(damage);
                DestroySelf();
            }
        }
        else if (!BlockManager.Instance.GetBlockAt(position+Vector3Int.forward*Math.Sign(delZ)).hasEnemy)
        {
            oldBlock.hasEnemy = false;
            oldBlock.enemy = null;
            position = position+Vector3Int.forward*Math.Sign(delZ);
            newBlock = BlockManager.Instance.GetBlockAt(position);
            newBlock.hasEnemy = true;
            newBlock.enemy = this;
            if (newBlock.hasPlayer) {
                GameManager.Instance.Harm(damage);
                DestroySelf();
            }
        }
        transform.position = position;
    }

    public void DestroySelf()
    {
        EnemyManager.Instance.QueueToKill(this);
        Block block = BlockManager.Instance.GetBlockAt(position);
        block.hasEnemy = false;
        block.enemy = null;
        Destroy(gameObject);
    }

    public void Init(LevelData newLevelData, Vector3Int pos)
    {
        position = pos;
        hitpoints = newLevelData.hitpoints;
        damage = newLevelData.damage;
        speed = newLevelData.speed;
        levelData = newLevelData;
        EnemyManager.Instance.enemies.Add(this);
        Block blockAt = BlockManager.Instance.GetBlockAt(pos);
        blockAt.hasEnemy = true;
        blockAt.enemy = this;
    }
}
