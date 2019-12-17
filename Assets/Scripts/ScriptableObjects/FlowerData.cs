using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Flower Data", menuName = "ScriptableObjects/Flowers/FlowerData")]
public class FlowerData : ScriptableObject
{
    public string flowerName;
    public Sprite[] flowerStageSpriteArray;
    public Sprite teaLeavesSprite;
    public int timeToGrow;
    public AnimationCurve growthCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public Sprite GetCurrentSprite(float timeSincePlanted)
    {
        //get percentage grown on a 0 to 1 scale by dividing time since planted by full growth time 
        float time = timeSincePlanted / timeToGrow;
        //adjust time based on the curve to get adjusted growth time then multiply by the number of stages to get growth stage
        float growthStage = growthCurve.Evaluate(time) * flowerStageSpriteArray.GetUpperBound(0);
        //Round down to remove decimals and then get the appropriate sprite
        return flowerStageSpriteArray[Mathf.Min(Mathf.FloorToInt(growthStage),flowerStageSpriteArray.GetUpperBound(0))];
    }

    public Sprite GetSeed()
    {
        return flowerStageSpriteArray[0];
    }

    public Sprite GetFullBloom()
    {
        return flowerStageSpriteArray[flowerStageSpriteArray.GetUpperBound(0)];
    }
}
