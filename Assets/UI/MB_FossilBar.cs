using UnityEngine;
using UnityEngine.UI;

public class MB_FossilBar : MonoBehaviour
{
    [SerializeField] Slider CleanBar;
    [SerializeField] Slider DamageBar;
    [SerializeField] Data_Rock rockData;
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
    }
}
