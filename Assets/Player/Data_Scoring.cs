
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_Scoring", menuName = "Scriptable Objects/Data_Scoring")]
public class Data_Scoring : ScriptableObject
{
    public List<float> scores;
    private void OnEnable()
    {
        scores = new List<float>();
    }
}
