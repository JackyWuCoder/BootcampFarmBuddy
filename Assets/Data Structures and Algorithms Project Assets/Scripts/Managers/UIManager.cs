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
        // Loop through all child objects of the parent
        foreach (Transform child in _sellHarvestHolder)
        {
            // Destroy each child object
            Destroy(child.gameObject);
        }

        Sprite[] _sprites =  Harvester._instance._harvest.GetComponent<Harvest>()._sprites;
        Sprite tempSprite;
        // Populate the shop with collected harvests
        foreach (CollectedHarvest collectedHarvest in Harvester._instance.GetCollectedHarvest()) {

            if (collectedHarvest._name == "Pumpkins")
            {
                tempSprite = _sprites[0];
            }
            else if (collectedHarvest._name == "Carrots")
            {
                tempSprite = _sprites[1];
            }
            else if (collectedHarvest._name == "Potatoes")
            {
                tempSprite = _sprites[2];
            }
            else if (collectedHarvest._name == "Tomatoes")
            {
                tempSprite = _sprites[3];
            }
            else if (collectedHarvest._name == "Beans")
            {
                tempSprite = _sprites[4];
            }
            else
            {
                tempSprite = null;
            }

            SellHarvestUIElement _sellHarvestUIElementInstance = Instantiate(_sellHarvestUIElement, _sellHarvestHolder);

            _sellHarvestUIElementInstance.SetElement(collectedHarvest, collectedHarvest._name, collectedHarvest._time, 2,
                collectedHarvest._amount, tempSprite);
        }
    }

    
}
