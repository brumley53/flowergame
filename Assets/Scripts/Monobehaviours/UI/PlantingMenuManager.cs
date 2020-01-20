using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class PlantingMenuManager : PopUpMenuManager
{
    public PlantingMenu plantingMenuPrefab;
    [HideInInspector]
    public RectTransform rectTransform;
    public CanvasScaler canvasScaler;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OpenPlantingMenu(FlowerBehaviour potToFill)
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, potToFill.transform.position);
        PlantingMenu tempPlantingMenu = Instantiate(plantingMenuPrefab, screenPoint, plantingMenuPrefab.transform.rotation, transform);
        tempPlantingMenu.FillMenu(potToFill);
    }
}
