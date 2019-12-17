using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class inputDetection : MonoBehaviour
{
    public UnityEvent inputEvent;

    void OnMouseUpAsButton()
    {
        Debug.Log("WOMP");
        inputEvent.Invoke();
    }
}
