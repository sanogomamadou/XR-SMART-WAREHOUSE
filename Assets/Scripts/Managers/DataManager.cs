using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WarehouseJsonRoot
{
    public WarehouseJsonItem[] items;
}

[Serializable]
public class WarehouseJsonItem
{
    public string id;
    public string name;
    public int quantity;
    public int warningThreshold;
    public string category; 
}

public class DataManager : MonoBehaviour
{
    [Header("JSON (TextAsset)")]
    public TextAsset warehouseJson;

    private Dictionary<string, WarehouseJsonItem> dataById = new Dictionary<string, WarehouseJsonItem>();

    void Start()
    {
        LoadJson();
        ApplyDataToScene();
    }

    void LoadJson()
    {
        if (warehouseJson == null)
        {
            Debug.LogError("DataManager: warehouseJson is not assigned!");
            return;
        }

        var root = JsonUtility.FromJson<WarehouseJsonRoot>(warehouseJson.text);

        if (root == null || root.items == null)
        {
            Debug.LogError("DataManager: JSON invalid or empty.");
            return;
        }

        dataById.Clear();
        foreach (var it in root.items)
        {
            if (!string.IsNullOrEmpty(it.id))
                dataById[it.id] = it;
        }

        Debug.Log($"DataManager: Loaded {dataById.Count} items from JSON.");
    }

    void ApplyDataToScene()
    {
        var sceneItems = FindObjectsOfType<WarehouseItem>();

        foreach (var sceneItem in sceneItems)
        {
            string id = sceneItem.gameObject.name;

            if (dataById.TryGetValue(id, out var jsonItem))
            {
                sceneItem.itemName = jsonItem.name;
                sceneItem.quantity = jsonItem.quantity;
                sceneItem.warningThreshold = jsonItem.warningThreshold;
                sceneItem.UpdateStatus();
            }
            else
            {
                Debug.LogWarning($"DataManager: No JSON entry for '{id}'");
            }
        }

        Debug.Log("DataManager: Applied JSON data to scene items.");
    }
}