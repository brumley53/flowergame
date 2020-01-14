using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteSheet
{
    public Sprite[] frames;
    public float framesPerSecond;
}

[CreateAssetMenu(fileName = "New Flower Data", menuName = "ScriptableObjects/Flowers/FlowerData")]
public class FlowerAsset : ScriptableObject
{
    public string flowerName;
    public SpriteSheet[] flowerStageSpriteSheetArray;
    public Sprite teaLeavesSprite;
    public int timeToGrow;
    public AnimationCurve growthCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public SpriteSheet GetCurrentSpriteSheet(float timeSincePlanted)
    {
        //get percentage grown on a 0 to 1 scale by dividing time since planted by full growth time 
        float time = timeSincePlanted / timeToGrow;
        //adjust time based on the curve to get adjusted growth time then multiply by the number of stages to get growth stage
        float growthStage = growthCurve.Evaluate(time) * flowerStageSpriteSheetArray.GetUpperBound(0);
        //Round down to remove decimals and then get the appropriate sprite
        return flowerStageSpriteSheetArray[Mathf.Min(Mathf.FloorToInt(growthStage), flowerStageSpriteSheetArray.GetUpperBound(0))];
    }

    public SpriteSheet GetSeed()
    {
        return flowerStageSpriteSheetArray[0];
    }

    public SpriteSheet GetFullBloom()
    {
        return flowerStageSpriteSheetArray[flowerStageSpriteSheetArray.GetUpperBound(0)];
    }
}
