using UnityEngine;

[CreateAssetMenu(fileName = "Data_Rock", menuName = "Scriptable Objects/Data_Rock")]
public class Data_Rock : ScriptableObject
{
    [SerializeField] public float[] LAYERHP;
    [SerializeField] public Color[] LAYERCOLOR;
}
