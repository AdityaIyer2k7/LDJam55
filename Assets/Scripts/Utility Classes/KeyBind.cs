using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[CreateAssetMenu(fileName="keybind", menuName="New KeyBind")]
public class KeyBind : ScriptableObject
{
    public string identifier;
    public List<KeyCode> keyCodes;
}