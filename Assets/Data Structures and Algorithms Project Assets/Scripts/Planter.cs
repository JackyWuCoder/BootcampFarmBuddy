using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Planter : MonoBehaviour
{
    public static Planter _instance { get; private set; }

    public System.Action<string, int> OnSeedsChanged;

    [Header("Plants")]
    [SerializeField] private Plant[] _plantPrefabs;
    [SerializeField] private Transform _plantParent;

    [Header("Farm")]
    [SerializeField] private Tilemap _farmMap;
    [SerializeField] private RuleTile _farmTile;

    // Holds all the resource files of the plants
    private PlantTypeScriptableObject[] _plants;

    // Mainly to retrieve the plant type by name
    private Dictionary<string, Plant> _availablePlantTypes = new Dictionary<string, Plant>();
    
    // Store the available seeds to plant
    private Dictionary<Plant, int> _availableSeeds = new Dictionary<Plant, int>();


    private Plant _selectedPlantType;

    // Store all the plants for analytics
    private List<Plant> _plantedPlants = new List<Plant>();

    // Store plant position information.
    private Dictionary<Plant, FarmElement> _farmElementOfPlant = new Dictionary<Plant, FarmElement>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;
    }

    private void Start()
    {
        Initialize();
    }

    public List<Plant> GetPlantedPlants()
    {
        return _plantedPlants;
    }

    public PlantTypeScriptableObject GetPlantResourseByName(string name)
    {
        if(_availablePlantTypes.TryGetValue(name, out Plant obj))
        {
            return obj.GetplantType();
        }
        return null;
    }

    void Initialize()
    {
        // Assign the scriptable object resource
        _plants = new PlantTypeScriptableObject[_plantPrefabs.Length];

        for (int i = 0; i < _plantPrefabs.Length; i++)
        {
            _plants[i] = _plantPrefabs[i].GetplantType();
        }

        // Initialize Seeds UI
        UIManager._instance.InitializePlantUIs(_plants);

        for (int i = 0; i < _plants.Length; i++)
        {
            // try adding the value
            if (_availablePlantTypes.TryAdd(_plants[i]._plantTypeName, _plantPrefabs[i]))
            {
                //add available seeds (nothing at starts)
                _availableSeeds.Add(_plantPrefabs[i], Utils.START_SEEDS);

                //Update listeners
                OnSeedsChanged(_plants[i]._plantTypeName, _availableSeeds[_plantPrefabs[i]]);

            }
            else
            {
                Debug.Log($"Plant with the same name exists. Cannot add {_plants[i]._plantTypeName}");
            }

        }

    }

    
    public void ChoosePlant(string plantName)
    {
        // Check if plant type is available
        if(_availablePlantTypes.TryGetValue(plantName, out Plant plantType))
        {
            _selectedPlantType = plantType;

            UIManager._instance.UpdateStatus($"Selected : {plantName}");
        }
        else
        {
            Debug.Log("Plant type cannot be found");
        }
    }

    public bool Plant(Vector2 position, FarmElement farmElement)
    {
        

        //check if seeds are available
        if (_selectedPlantType != null && _availableSeeds.TryGetValue(_selectedPlantType, out int availableSeeds))
        {
            if (availableSeeds > 0)
            {
                // reduce one seed
                _availableSeeds[_selectedPlantType] = availableSeeds - 1;
                OnSeedsChanged(_selectedPlantType.GetplantType()._plantTypeName, _availableSeeds[_selectedPlantType]);

                // plant the seed
                Plant plant = Instantiate(_selectedPlantType, position, Quaternion.identity);
                plant.transform.SetParent(_plantParent);

                // add this to the planted plants
                _plantedPlants.Add(plant);

                //assign the plant with the farm element so that it knows where its planted
                _farmElementOfPlant.TryAdd(plant, farmElement);

                return true;
            }
            else
            {
                Debug.Log($"No seeds of {_selectedPlantType.GetplantType()._plantTypeName} left to plant!");
                
                return false;
            }
        }
        else
        {
            UIManager._instance.UpdateStatus("Select a seed to plant!");
        }
        return false;
    }

    public void AddSeeds(string plantName, int seeds)
    {
        // Check if plant type is available
        if (_availablePlantTypes.TryGetValue(plantName, out Plant plantType))
        {
            // if available
            if(_availableSeeds.ContainsKey(plantType))
            {
                // add seeds to that plant
                _availableSeeds[_selectedPlantType] += seeds;

                // Update listeners
                OnSeedsChanged(plantName, _availableSeeds[_selectedPlantType]);

            }
        }
    }

    public void SetFarm()
    {
        _farmMap.SetTile(GetTilePosition(), _farmTile);
    }

    public void RemoveFarm()
    {
        _farmMap.SetTile(GetTilePosition(), null);
    }

    public void RemovePlant(Plant plant)
    {
        if(_farmElementOfPlant.TryGetValue(plant, out FarmElement farmElement))
        {
            //change the state of the farm
            farmElement.SetFarmState(FarmElement.PlantState.grass);
            RemoveFarm();

            // remove from planted plants
            _plantedPlants.Remove(plant);

            // remove the associated farm element
            _farmElementOfPlant.Remove(plant);
            Destroy(plant.gameObject);

        }
    }

    Vector3Int GetTilePosition()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePosition = _farmMap.WorldToCell(point);

        return tilePosition;
    }
}
