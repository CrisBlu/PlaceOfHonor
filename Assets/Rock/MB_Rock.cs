using System;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MB_Rock : MonoBehaviour
{
    [SerializeField] Data_Rock data;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Renderer mask;
    //[SerializeField] TextMeshPro text;
    [SerializeField] bool OverFossil;

    private float[] layerHP = new float[4];
    [NonSerialized] public int layer;
    LayerMask fossilMask;

    void Start()
    {
        layer = 0;
        data.LAYERHP.CopyTo(layerHP, 0);

        
        
        //text.text = layerHP[layer].ToString();

        fossilMask = LayerMask.GetMask("Hidden");
        CheckIfInFrontOfFossil();

        if (OverFossil)
            data.totalRocksOverFossil++;


    }

    void CheckIfInFrontOfFossil()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 100, fossilMask))
        {
            OverFossil = true;
            Debug.DrawRay(transform.position, Vector3.forward, Color.white, 10000f, true);
        }
        else
        {
            OverFossil = false;
        }

        
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

        

        //text.text = layerHP[layer].ToString();
    }

    void ShiftLayer(int layer)
    {
        sprite.color = data.LAYERCOLOR[layer];
    }
}
