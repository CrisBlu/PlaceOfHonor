using System;
using TMPro;
using UnityEngine;

public class MB_Rock : MonoBehaviour
{
    [SerializeField] Data_Rock data;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] SpriteMask mask;
    [SerializeField] TextMeshPro text;
    [SerializeField] bool OverFossil;

    private float[] layerHP = new float[4];
    [NonSerialized] public int layer;

    void Start()
    {
        layer = 0;
        data.LAYERHP.CopyTo(layerHP, 0);

        if (OverFossil)
            data.totalRocksOverFossil++;
        
        text.text = layerHP[layer].ToString();
    }


    void Update()
    {
       
    }

    public void TakeDamage(float damage, int layerStruck)
    {
        //Rocks take less damage if they are on a different layer from the rock that was hit
        int layerDiff = Mathf.Abs(layer - layerStruck) + 1;

        layerHP[layer] -= damage/layerDiff;

        if (layerHP[layer] < 0)
        {
            if (layer < 3)
            {
                layer++;
                ShiftLayer(layer);

                if (layer == 3 && OverFossil)
                {
                    data.OnFossilReveal();
                }
            }
            else if(OverFossil && mask.enabled == false)
            {
                mask.enabled = true;
                data.OnFossilDamaged();
            }
        }

        

        text.text = layerHP[layer].ToString();
    }

    void ShiftLayer(int layer)
    {
        sprite.color = data.LAYERCOLOR[layer];
    }
}
