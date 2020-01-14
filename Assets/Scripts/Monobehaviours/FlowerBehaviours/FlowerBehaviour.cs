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

    public FlowerData(FlowerAsset flowerToPlant)
    {
        flowerAsset = flowerToPlant;
        timePlanted = DateTime.UtcNow;
    }
}

public class FlowerBehaviour : MonoBehaviour
{
    public SpriteSheetAnimation spriteSheetAnimation;
    
    private FlowerData flowerData = null;

    public UnityEventFloat updateTimeEvent;

    [ContextMenu("testOpenAppUpdate")]
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && flowerData != null)
        {
            TimeSpan timeSpan = DateTime.UtcNow - flowerData.timePlanted;
            UpdateTimeNow();
        }
    }

    public void Plant(FlowerAsset flowerToPlant)
    {
        flowerData = new FlowerData(flowerToPlant);
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
        spriteSheetAnimation.setSpriteSheet(flowerData.flowerAsset.GetCurrentSpriteSheet(timeSincePlanted));
    }
}
