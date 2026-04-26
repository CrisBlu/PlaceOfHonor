using UnityEngine;

[CreateAssetMenu(fileName = "Data_Tool", menuName = "Scriptable Objects/Data_Tool")]
public class Data_Tool : ScriptableObject
{

    public Tool tool;
}


public enum Tool
{
    hammer,
    drill,
    none
}