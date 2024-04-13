using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance
    {get{
        if (instance != null) return instance;
        InputManager im = FindObjectOfType<InputManager>();
        if (im != null) { instance = im; return im; }
        GameObject go = new();
        go.name = "InputManager";
        DontDestroyOnLoad(go);
        instance = go.AddComponent<InputManager>();
        return instance;
    }}
    private Dictionary<string, List<KeyCode>> keyCodes = new();

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null & instance != this) Destroy(gameObject);
        KeyBind[] keyBinds = Resources.LoadAll<KeyBind>("KeyBinds");
        foreach (KeyBind keyBind in keyBinds)
        {
            keyCodes.Add(keyBind.identifier, keyBind.keyCodes);
        }
    }

    public bool GetKeyDown(string keyName)
    {
        foreach (KeyCode keyCode in keyCodes[keyName]) if (Input.GetKeyDown(keyCode)) return true;
        return false;
    }
    public bool GetKeyUp(string keyName)
    {
        foreach (KeyCode keyCode in keyCodes[keyName]) if (Input.GetKeyUp(keyCode)) return true;
        return false;
    }
    public bool GetKey(string keyName)
    {
        foreach (KeyCode keyCode in keyCodes[keyName]) if (Input.GetKey(keyCode)) return true;
        return false;
    }
}
