using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{
    public new string name;
    public int index;
    public int levelNeeded;
    public Vector3Int[] blocksNeeded;
    public GameObject displayPrefab;
    public GameObject spellPrefab;
}