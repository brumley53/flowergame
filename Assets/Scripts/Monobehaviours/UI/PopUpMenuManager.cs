using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PopUpMenuManager: MonoBehaviour
{
    public static event Action CloseMenus = delegate { };
}
