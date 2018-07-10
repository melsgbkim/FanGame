using UnityEngine;
using System.Collections;

[System.Serializable]
public class UIOrderInfo
{
    public string name;
    public Material On;
    public Material Off;
    public Material Empty;
}



[System.Serializable] 
public class UIOrderKeyInfo
{
    public string name = "Empty";
    public KeyCode key = KeyCode.None;
}