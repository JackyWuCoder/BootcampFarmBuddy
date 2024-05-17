using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateSeedsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _txtSeedAmount;

    private string _seedName;

    private void OnEnable()
    {
        Planter._instance.OnSeedsChanged += UpdateValue;
    }

    private void OnDisable()
    {
        Planter._instance.OnSeedsChanged -= UpdateValue;
    }

    private void OnDestroy()
    {
        Planter._instance.OnSeedsChanged -= UpdateValue;
    }

    public void SetSeedName(string name)
    {
        _seedName = name;
    }
        

    void UpdateValue(string seedName, int value)
    {
        if (seedName == _seedName)
        {
            _txtSeedAmount.SetText(value.ToString());
        }
    }
}
