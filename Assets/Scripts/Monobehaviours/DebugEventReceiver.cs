using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEventReceiver : MonoBehaviour
{
    public bool debugCurrentGameTime;

    void Update()
    {
        if (debugCurrentGameTime)
        {
            Debug.Log("Game Time: " + Time.realtimeSinceStartup);
        }
    }

    public void DebugFloat(float debugMessage)
    {
        Debug.Log("Debug Float: " + debugMessage);
    }
}
