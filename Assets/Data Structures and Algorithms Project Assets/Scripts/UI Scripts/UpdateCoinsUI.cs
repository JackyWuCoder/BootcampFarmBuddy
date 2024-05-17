using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateCoinsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _txtCoins;

    private void Start()
    {
        UpdateValue(0);
        GameManager._instance.GetShop().OnCoinsChanged += UpdateValue;
    }

    private void OnDisable()
    {
        GameManager._instance.GetShop().OnCoinsChanged -= UpdateValue;
    }

    private void OnDestroy()
    {
        GameManager._instance.GetShop().OnCoinsChanged -= UpdateValue;
    }

    void UpdateValue(float coins)
    {
        _txtCoins.SetText(coins.ToString());
    }
}
