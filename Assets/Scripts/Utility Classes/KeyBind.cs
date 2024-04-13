using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[CreateAssetMenu(fileName="New Keybind", menuName="KeyBind")]
public class KeyBind : ScriptableObject
{
    public string identifier;
    public List<KeyCode> keyCodes;
}