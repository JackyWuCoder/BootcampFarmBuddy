using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _seedButtonUI;
    [SerializeField] private Transform _seedsUIHolder;
    [SerializeField] private TMP_Text _txtStatus;

    [Header("Shop-BUY")]
    [SerializeField] private Transform _buySeedsHolder;
    [SerializeField] private SeedsBuyUIElement _buySeedsUIElement;

    [Header("Shop-SELL")]
    [SerializeField] private Transform _sellHarvestHolder;
    [SerializeField] private SellHarvestUIElement _sellHarvestUIElement;

    public static UIManager _instance { get; private set; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;
    }

    public void UpdateStatus(string text)
    {
        _txtStatus.SetText(text);
    }

    public void InitializePlantUIs(PlantTypeScriptableObject[] _plantTypes)
    {
        foreach (var item in _plantTypes)
        {
            GameObject seedButton = Instantiate(_seedButtonUI, _seedsUIHolder);

            seedButton.GetComponent<Image>().sprite = item._seedSprite;

            seedButton.GetComponent<Button>().onClick.AddListener(() => { 
                Planter._instance.ChoosePlant(item._plantTypeName); 
            });

            seedButton.GetComponent<UpdateSeedsUI>().SetSeedName(item._plantTypeName);



            SeedsBuyUIElement buySeedUIElement = Instantiate(_buySeedsUIElement, _buySeedsHolder);
            buySeedUIElement.SetElement(item._plantTypeName, item._pricePerSeed, item._seedSprite);
            buySeedUIElement.GetButton().onClick.AddListener(() =>
            {
                GameManager._instance.GetShop().BuySeed(item._plantTypeName, item._pricePerSeed);
            });

        }
    }

    public void ShowTotalHarvest()
    {
        //Assignment 2
        
    }

    
}
