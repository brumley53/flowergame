using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlowerData))]
public class FlowerBehaviour : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [HideInInspector]
    public FlowerData flowerData;

    public UnityEventFloat updateTimeEvent;
    void Start()
    {
        flowerData = GetComponent<FlowerData>();
        Plant();
    }

    [ContextMenu("testOpenAppUpdate")]
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && flowerData != null)
        {
            TimeSpan timeSpan = DateTime.UtcNow - flowerData.timePlanted;
            UpdateTimeNow();
        }
    }

    public void Plant()
    {
        flowerData.timePlanted = DateTime.UtcNow;
        UpdateTimeNow();
    }

    void UpdateTime(float timeSincePlanted)
    {
        UpdateSprite(timeSincePlanted);
        updateTimeEvent.Invoke(timeSincePlanted);
    }

    void UpdateTimeNow()
    {
        TimeSpan timeSpan = DateTime.UtcNow - flowerData.timePlanted;
        UpdateTime(System.Convert.ToSingle(timeSpan.TotalSeconds));
    }

    public void UpdateSprite(float timeSincePlanted)
    {
        spriteRenderer.sprite = flowerData.flowerAsset.GetCurrentSprite(timeSincePlanted);
    }
}
