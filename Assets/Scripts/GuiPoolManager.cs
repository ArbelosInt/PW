// Programmer : Agnel Blaise
//
// Script that manages the pool of GUI Indicators. Non-modular.
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuiPoolManager : MonoBehaviour {

    public GuiIndicator poolPrefab;
    public int poolSize = 8;
    public List<GuiIndicator> activePool;
    public List<GuiIndicator> pool;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < poolSize; i++) // Populating the pool
        {
            GuiIndicator indicator = Instantiate(poolPrefab);
            indicator.transform.SetParent(this.transform);
            indicator.gameObject.SetActive(false);
            pool.Add(indicator);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddVehicle(GameObject vehicle)
    {
        GuiIndicator indicator = GetFromPool();
        indicator.SetTarget(vehicle, this);
        Debug.Log("GUI Pooler adding Alert");
    }

    // Takes an indicator from pool and add it to active pool
    public GuiIndicator GetFromPool()
    {
        GuiIndicator indic;

        if (pool.Count > 0) // If objects exists in pool
        {
            indic = pool[0];   // Get the first
            pool.Remove(indic); // Remove it from pool           
        }
        else 
        {
            indic = Instantiate(poolPrefab); // Create  a new one
            indic.transform.parent = this.transform;
        }

        activePool.Add(indic);
        return indic; 
    }

    // Put it back to pool 
    public void AddToInactivePool(GuiIndicator obj)
    {
        activePool.Remove(obj);
        pool.Add(obj);
    }

}
