using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField] public Sprite[] _sprites;

    private int _seedAmount;

    public void SetSeed(string plantName, int seeds)
    {
        _seedAmount = seeds;

        GetComponent<SpriteRenderer>().sprite = Planter._instance.GetPlantResourseByName(plantName)._seedSprite;
        GetComponent<ClickableObject>().OnClicked.AddListener(() => { CollectSeed(plantName); });
    }

    private void CollectSeed(string plantName)
    {
        Planter._instance.AddSeeds(plantName, _seedAmount);
        Destroy(gameObject);
    }
}
