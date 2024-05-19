using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : MonoBehaviour
{
    [SerializeField] public Harvest _harvest;
    [SerializeField] public Seed _seed;

    // Harvest Analytics
    private Dictionary<string, int> _harvests = new Dictionary<string, int>();

    // Harvest to sell
    // Assignment 2 - Data structure to hold collected harvests
    [SerializeField] private List<CollectedHarvest> collectedHarvests = new List<CollectedHarvest>();

    public static Harvester _instance;
       
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;
    }

    // Assignment 2
    public List<CollectedHarvest> GetCollectedHarvest()
    {
        return collectedHarvests;
    }

    // Assignment 2
    public void RemoveHarvest(CollectedHarvest harvest)
    {
        collectedHarvests.Remove(harvest);
    }

    // Assignment 2 - CollectHarvest method to collect the harvest when picked up
    public void CollectHarvest(string plantName, int amount)
    {
        // Add harvested item to the list
        DateTime currentDateTime = DateTime.Now;
        string dateTimeString = currentDateTime.ToString("yyyy/MM/dd hh:mm: tt");
        collectedHarvests.Add(new CollectedHarvest(plantName, dateTimeString, amount));
        // Update UI to display collected harvest
        UIManager._instance.ShowTotalHarvest();
    }

    public void ShowHarvest(string plantName, int harvestAmount, int seedAmount, Vector2 position)
    {
        // initiate a harvest with random amount
        Harvest harvest = Instantiate(_harvest, position + Vector2.up + Vector2.right, Quaternion.identity);
        harvest.SetHarvest(plantName, harvestAmount);
        
        // initiate one seed object
        Seed seed = Instantiate(_seed, position + Vector2.up + Vector2.left, Quaternion.identity);
        seed.SetSeed(plantName, seedAmount);
    }

    //Assignment 3
    public void SortHarvestByAmount()
    {
        // Sort the collected harvest using Quick sort
        // Convert collectedHarvests to array for sorting
        CollectedHarvest[] harvestArray = collectedHarvests.ToArray();

        // Quick Sort algorithm
        void QuickSortByAmount(CollectedHarvest[] arr, int low, int high)
        {
            if (low < high)
            {
                // Partition the array, and get the pivot index
                int pivotIndex = PartitionByAmount(arr, low, high);

                // Recursively sort elements before and after the pivot index
                QuickSortByAmount(arr, low, pivotIndex - 1);
                QuickSortByAmount(arr, pivotIndex + 1, high);
            }
        }

        // Helper method to partition the array
        int PartitionByAmount(CollectedHarvest[] arr, int low, int high)
        {
            // Choose the pivot element (last element)
            int pivot = arr[high]._amount;

            int i = low - 1; // Index of smaller element

            for (int j = low; j < high; j++)
            {
                // If current element is smaller than or equal to the pivot
                if (arr[j]._amount <= pivot)
                {
                    i++;

                    // Swap arr[i] and arr[j]
                    CollectedHarvest temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            // Swap arr[i+1] and arr[high] (or pivot)
            CollectedHarvest temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;

            return i + 1;
        }

        // Call the Quick Sort algorithm
        QuickSortByAmount(harvestArray, 0, harvestArray.Length - 1);

        // Update collectedHarvests with sorted harvests
        collectedHarvests.Clear();
        collectedHarvests.AddRange(harvestArray);
    }

}

// For Assignment 2, this holds a collected harvest object
[System.Serializable]
public struct CollectedHarvest
{
    public string _name;
    public string _time;
    public int _amount;

    public CollectedHarvest(string name, string time, int amount)
    {
        _name = name;
        _time = time;
        _amount = amount;
    }
}
