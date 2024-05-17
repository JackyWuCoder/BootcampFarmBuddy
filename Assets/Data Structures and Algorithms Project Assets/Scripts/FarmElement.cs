using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmElement : MonoBehaviour
{
    public enum PlantState
    {
        grass,
        farm,
        planted
    }

    private PlantState _state;

    private void Start()
    {
        _state = PlantState.grass;
    }

    public void SetFarmState(PlantState state)
    { 
        _state = state; 
    }

    public void OnClicked()
    {
        switch(_state)
        {
            case PlantState.grass:
                _state = PlantState.farm;
                Planter._instance.SetFarm();
                break;
            case PlantState.farm:
                if(Planter._instance.Plant(transform.position, this))
                {
                    _state = PlantState.planted;
                }
                break;
            default:
                break;

        }
    }
}
