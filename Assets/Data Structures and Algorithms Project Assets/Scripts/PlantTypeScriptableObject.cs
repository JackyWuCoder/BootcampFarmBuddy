using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantType_Untitled", menuName = "ScriptableObjects/Plant Type", order =1)]
public class PlantTypeScriptableObject : ScriptableObject
{
    public string _plantTypeName;
    
    public Sprite _harvestSprite;
    public Sprite _seedSprite;

    public float _timeToGrow;

    public int _minHarvest;
    public int _maxHarvest;
    public int _minSeeds;
    public int _maxSeeds;

    public float _pricePerSeed;
    public float _pricePerHarvest;

    public int GenerateRandomHarvestAmount()
    {
        return Random.Range(_minHarvest, _maxHarvest);
    }

    public int GenerateRandomSeedAmount()
    {
        return Random.Range(_minSeeds, _maxSeeds);
    }
}
