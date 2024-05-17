using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : MonoBehaviour
{
    [SerializeField] public Sprite[] _sprites;

    private int _harvestAmount;

    public void SetHarvest(string plantName, int harvestAmount)
    {
        // Set harvest sprite and amount
        GetComponent<SpriteRenderer>().sprite = Planter._instance.GetPlantResourseByName(plantName)._harvestSprite;
        _harvestAmount = harvestAmount;

        GetComponent<ClickableObject>().OnClicked.AddListener(() => { CollectHarvest(plantName); });
    }

    public void CollectHarvest(string plantName)
    {
        // Assignment 2
        // Call the harvester to harvest this element

        Destroy(gameObject);
    }
}
