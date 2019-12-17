using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(FlowerDataHolder))]
public class SpriteChanger : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public FlowerDataHolder flowerDataHolder;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flowerDataHolder = GetComponent<FlowerDataHolder>();
    }

    public void UpdateSprite(float timeSincePlanted)
    {
        spriteRenderer.sprite = flowerDataHolder.flowerData.GetCurrentSprite(timeSincePlanted);
    }
}
