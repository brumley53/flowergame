using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerData : MonoBehaviour
{
    public FlowerAsset flowerAsset;
    [HideInInspector]
    public DateTime timePlanted = DateTime.MaxValue;
}
