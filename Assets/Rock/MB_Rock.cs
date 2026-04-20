using UnityEngine;

public class MB_Rock : MonoBehaviour
{
    [SerializeField] Data_Rock data;
    [SerializeField] SpriteRenderer sprite;
    private float[] layerHP = new float[3];
    private int layer;

    void Start()
    {
        layer = 0;
        data.LAYERHP.CopyTo(layerHP, 0);
    }


    void Update()
    {
        TakeDamage(.1f);
    }

    void TakeDamage(float damage)
    {
        if (layer == 3)
            return;

        layerHP[layer] -= damage;

        if (layerHP[layer] < 0)
        {
            if (layer < 3)
            {
                layer++;
                ShiftLayer(layer);
            }
        }
    }

    void ShiftLayer(int layer)
    {
        sprite.color = data.LAYERCOLOR[layer];
    }
}
