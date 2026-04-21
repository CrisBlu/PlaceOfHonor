using System;
using UnityEngine;

public class MB_Rock : MonoBehaviour
{
    [SerializeField] Data_Rock data;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] SpriteMask mask;
    [SerializeField] bool OverFossil;

    private float[] layerHP = new float[4];
    private int layer;

    void Start()
    {
        layer = 0;
        data.LAYERHP.CopyTo(layerHP, 0);
    }


    void Update()
    {

    }

    public void TakeDamage(float damage)
    {

        layerHP[layer] -= damage;

        if (layerHP[layer] < 0)
        {
            if (layer < 3)
            {
                layer++;
                ShiftLayer(layer);
            }
            else if(OverFossil)
            {
                mask.enabled = true;
                Debug.Log("Fossil broken!");
            }
        }
    }

    void ShiftLayer(int layer)
    {
        sprite.color = data.LAYERCOLOR[layer];
    }
}
