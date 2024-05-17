using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager _instance;

    [SerializeField] private Shop _shop;

    public GameSate _currentState;

    public enum GameSate
    {
        MapMove,
        Menu,
        SeedPlant
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;
    }

    public Shop GetShop()
    {
        return _shop;
    }
    
    public void SetState(GameSate state)
    {
        _currentState = state;
    }

    public GameSate GetState()
    {
        return _currentState;
    }

}


