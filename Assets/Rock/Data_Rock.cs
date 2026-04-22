using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Data_Rock", menuName = "Scriptable Objects/Data_Rock")]
public class Data_Rock : ScriptableObject
{
    [SerializeField] public float[] LAYERHP;
    [SerializeField] public Color[] LAYERCOLOR;
    [System.NonSerialized] public UnityEvent<float, bool> Event_ShiftFossilBar;

    public int totalRocksOverFossil;
    public int fossilReveal;
    public int fossilDamaged;



    private void OnEnable()
    {
        Event_ShiftFossilBar = new UnityEvent<float, bool>();

        totalRocksOverFossil = 0;
        fossilReveal = 0;
        fossilDamaged = 0;
    }

    private void OnDisable()
    {
        Event_ShiftFossilBar.RemoveAllListeners();
        Event_ShiftFossilBar = null;
    }


    public void OnFossilReveal()
    {
        fossilReveal++;
        Event_ShiftFossilBar.Invoke((float)fossilReveal/ (float)totalRocksOverFossil, true);

    }

    public void OnFossilDamaged()
    {
        fossilReveal--;
        fossilDamaged++;
        Event_ShiftFossilBar.Invoke((float)fossilReveal / (float)totalRocksOverFossil, true);
        Event_ShiftFossilBar.Invoke((float)fossilDamaged / (float)totalRocksOverFossil, false);

    }

    


}
