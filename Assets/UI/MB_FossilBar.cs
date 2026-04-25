using UnityEngine;
using UnityEngine.UI;

public class MB_FossilBar : MonoBehaviour
{
    [SerializeField] Slider CleanBar;
    [SerializeField] Slider DamageBar;
    [SerializeField] MB_Timer Timer;
    public Data_Rock rockData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rockData.Event_ShiftFossilBar.AddListener(ShiftFossilBar);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }


    private void ShiftFossilBar(float value, bool damageOrClean)
    {
        Slider bar = damageOrClean ? CleanBar : DamageBar;

        bar.value = value;

        if (CleanBar.value + DamageBar.value > .98)
            Timer.StopAllCoroutines();
    }

    public void ResetBar()
    {
        rockData.Event_ShiftFossilBar.RemoveListener(ShiftFossilBar);
        DamageBar.value = 0;
        CleanBar.value = 0;

    }

    public void RearmBar()
    {
        rockData.Event_ShiftFossilBar.AddListener(ShiftFossilBar);
    }
}
