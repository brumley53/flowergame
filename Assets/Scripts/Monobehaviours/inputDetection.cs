using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class inputDetection : MonoBehaviour
{
    public UnityEvent inputEvent;

    void OnMouseUpAsButton()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            inputEvent.Invoke();
        }
    }
}
