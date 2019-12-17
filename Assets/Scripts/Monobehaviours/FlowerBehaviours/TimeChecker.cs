using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeChecker : MonoBehaviour
{
    [HideInInspector]
    public DateTime timePlanted = DateTime.MaxValue;

    public UnityEventFloat updateTimeEvent;

    void Start()
    {
        timePlanted = DateTime.UtcNow;
        TimeSpan timeSpan = DateTime.UtcNow - timePlanted;
        UpdateTime(System.Convert.ToSingle(timeSpan.TotalSeconds));
    }

    void OnCloseApp()
    {

    }

    [ContextMenu("testOpenAppUpdate")]
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && timePlanted != DateTime.MaxValue)
        {
            TimeSpan timeSpan = DateTime.UtcNow - timePlanted;
            UpdateTime(System.Convert.ToSingle(timeSpan.TotalSeconds));
        }
    }

    void UpdateTime(float timeCheck)
    {
        updateTimeEvent.Invoke(timeCheck);
    }
}
