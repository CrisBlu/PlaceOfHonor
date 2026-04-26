using System;
using UnityEngine;
using UnityEngine.UI;

public class MB_FossilBar : MonoBehaviour
{
    [SerializeField] Slider CleanBar;
    [SerializeField] Slider DamageBar;
    [SerializeField] MB_GameManager GameManager;
    public Data_Rock rockData;

    [NonSerialized] public float score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        rockData.Event_ShiftFossilBar.AddListener(ShiftFossilBar);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }


    private void ShiftFossilBar(float value, bool damageOrClean)
    {
        if (!GameManager.GameActive)
            return;


        Slider bar = damageOrClean ? CleanBar : DamageBar;

        bar.value = value;

        score = CleanBar.value;
        if (CleanBar.value + DamageBar.value > .98)
        {
            score += 1 - (CleanBar.value + DamageBar.value);
            GameManager.StopGame();
        }
           
    }

    public void ResetBar()
    {
        rockData.Event_ShiftFossilBar.RemoveListener(ShiftFossilBar);
        DamageBar.value = 0;
        CleanBar.value = 0;
        score = 0;

    }

    public void RearmBar()
    {
        
        rockData.Event_ShiftFossilBar.AddListener(ShiftFossilBar);
    }
}
