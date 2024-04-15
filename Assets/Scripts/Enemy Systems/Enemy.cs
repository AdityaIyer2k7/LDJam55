using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int hitpoints = 1;
    public int damage = 1;
    public float speed = 1;
    public bool canDestoryFire = false;
    public GameObject prefab;
}

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy Type")]
public class Enemy : ScriptableObject
{
    public new string name;
    [TextArea]
    public string description;
    public List<LevelData> levelData;
}
