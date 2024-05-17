using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plant : MonoBehaviour, IPointerClickHandler
{
    private GameObject[] _growStages;
    private float _growTimer = 0;
    private int _currentStage = 0;

    [SerializeField] private PlantTypeScriptableObject _plantType;

    void Start()
    {
        _growStages = new GameObject[transform.childCount];
        for(int i =0; i< transform.childCount; i++)
        {
            _growStages[i] = transform.GetChild(i).gameObject;
        }

        _growTimer = 0;
        _currentStage = 0;
        _growStages[_currentStage].SetActive(true);

    }

    public void SetPlantType(PlantTypeScriptableObject plantType)
    { 
        _plantType = plantType;
    }

    public PlantTypeScriptableObject GetplantType()
    {
        return _plantType;
    }

    void Update()
    {
        UpdateCropStage();
    }

    void UpdateCropStage()
    {
        if (_currentStage >= _growStages.Length - 1)
            return;

        _growTimer += Time.deltaTime;

        if(_growTimer >= _plantType._timeToGrow)
        {
            _growTimer = 0;
            _growStages[_currentStage].SetActive(false);
            _currentStage++;
            _growStages[_currentStage].SetActive(true);
        }
    }

    void Harvest()
    {
        //show harvest
        Harvester._instance.ShowHarvest(_plantType._plantTypeName, _plantType.GenerateRandomHarvestAmount(), _plantType.GenerateRandomSeedAmount(), transform.position);

        //remove plant
        Planter._instance.RemovePlant(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_currentStage == _growStages.Length-1)
        {
            Harvest();
        }
    }
}
