using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class PlantingMenu : MonoBehaviour
{
    public FlowerLibrary flowerLibrary;
    public Button seedButtonPrefab;
    public RectTransform menuContentTransform;

    public void FillMenu(FlowerBehaviour potToFill)
    {
        foreach (FlowerAsset flowerAsset in flowerLibrary.flowerTypes)
        {
            Button tempButton = Instantiate(seedButtonPrefab, menuContentTransform);
            tempButton.image.sprite = flowerAsset.GetSeed().frames[0];
            tempButton.onClick.AddListener(delegate { potToFill.Plant(flowerAsset); CloseMenu(); });
        }
    }

    void CloseMenu()
    {
        Destroy(gameObject);
    }

    public void OnEnable()
    {
        PopUpMenuManager.CloseMenus += CloseMenu;
    }

    public void OnDisable()
    {
        PopUpMenuManager.CloseMenus -= CloseMenu;
    }
}
