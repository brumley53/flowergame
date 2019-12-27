using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Flower Library", menuName = "ScriptableObjects/Flowers/FlowerLibrary")]
public class FlowerLibrary : ScriptableObject
{
    [Tooltip("Make sure there's only one of each flowerdata in this array, you don't need to put all of them in either just the ones you want in the game.")]
    public FlowerAsset[] flowerTypes;
    public Dictionary<FlowerAsset, int> seedDictionary;
}
