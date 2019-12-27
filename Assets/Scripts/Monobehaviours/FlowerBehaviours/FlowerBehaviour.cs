using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FlowerData
{
    public FlowerAsset flowerAsset;
    [HideInInspector]
    public DateTime timePlanted = DateTime.MaxValue;
}

public class FlowerBehaviour : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public FlowerData flowerData;

    public UnityEventFloat updateTimeEvent;

    void Start()
    {
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
