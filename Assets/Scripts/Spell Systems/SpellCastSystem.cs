using System.Collections.Generic;
using UnityEngine;

public class SpellCastSystem : MonoBehaviour
{
    public new Camera camera;
    public int H = 5;
    Spell[] spells;
    Dictionary<int, Spell> indexedSpells = new();
    Spell activeSpell;
    GameObject activeSelectPrefab;

    void Start()
    {
        spells = Resources.LoadAll<Spell>("Spells");
        foreach (Spell spell in spells)
        {
            indexedSpells.Add(spell.index, spell);
        }
    }

    bool TestCast(Vector3 pos)
    {
        Vector3Int gridPos = new Vector3Int(Mathf.RoundToInt(pos.x), 0, Mathf.RoundToInt(pos.z));
        bool canCast = true;
        foreach (Vector3Int blockNeeded in activeSpell.blocksNeeded)
        {
            if (!BlockManager.Instance.GetBlockAt(gridPos+blockNeeded).hasFire) {
                canCast = false;
                break;
            }
        }
        return canCast;
    }

    void BreakFires(Vector3 pos)
    {
        Vector3Int gridPos = new Vector3Int(Mathf.RoundToInt(pos.x), 0, Mathf.RoundToInt(pos.z));
        foreach (Vector3Int blockNeeded in activeSpell.blocksNeeded)
        {
            BlockManager.Instance.GetBlockAt(gridPos+blockNeeded).hasFire = false;
            Destroy(BlockManager.Instance.GetBlockAt(gridPos+blockNeeded).fire);
            BlockManager.Instance.GetBlockAt(gridPos+blockNeeded).fire = null;
        }
    }

    void TryCast(Vector3 pos)
    {
        if (!TestCast(pos)) return;
        BreakFires(pos);
        Instantiate(activeSpell.spellPrefab, activeSelectPrefab.transform.position, Quaternion.identity);
        Destroy(activeSelectPrefab);
        activeSelectPrefab = null;
        activeSpell = null;
        GameManager.Instance.inSpellMode = false;
        camera.transform.localPosition = Vector3.up*0.5f;
        camera.transform.localRotation = Quaternion.Euler(15,0,0);
        camera.orthographic = false;
    }

    void Update()
    {
        if (!GameManager.Instance.inSpellMode)
        {
            if (InputManager.Instance.GetKeyDown("SPELL1"))
            {
                activeSpell = indexedSpells[1];
                activeSelectPrefab = Instantiate(activeSpell.displayPrefab);
                GameManager.Instance.inSpellMode = true;
                camera.transform.localPosition = Vector3.up*H;
                camera.transform.localRotation = Quaternion.Euler(90,0,0);
                camera.orthographic = true;
                
            }
        }
        else
        {
            Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);
            activeSelectPrefab.transform.position = new Vector3(Mathf.RoundToInt(pos.x), 0, Mathf.RoundToInt(pos.z));
            if (InputManager.Instance.GetKeyDown("CAST")) TryCast(pos);
        }
    }
}