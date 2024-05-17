using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SellHarvestUIElement : MonoBehaviour
{
    [SerializeField] TMP_Text _txtPlantName;
    [SerializeField] TMP_Text _txtDateTime;
    [SerializeField] TMP_Text _txtHarvestAmount;
    [SerializeField] TMP_Text _txtValuePerItem;

    [SerializeField] Image _imgPlant;

    [SerializeField] private Button _btnSell;

    public CollectedHarvest _harvestElement { get; private set; }
    public void SetElement(CollectedHarvest harvestElement, string plantName, string timeStamp, float valuePerItem, int amount, Sprite sprite)
    {
        //Remove listeners if any.
        _btnSell.onClick.RemoveAllListeners();

        _harvestElement= harvestElement;
        _txtPlantName.text = plantName;
        _txtDateTime.text = timeStamp;
        _txtValuePerItem.text = $"${valuePerItem} per item";
        _txtHarvestAmount.text = $"{amount}";
        _imgPlant.sprite = sprite;

        _btnSell.onClick.AddListener(() =>
        {
            // Assignment 2
            // Call the shop to sell the harvest

            Destroy(gameObject);
        });
    }

    public Button GetButton() { return _btnSell; }

}
