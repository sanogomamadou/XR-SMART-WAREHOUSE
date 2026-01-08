using UnityEngine;

public class WarehouseItem : MonoBehaviour
{
    [Header("Données")]
    public string itemName;
    public int quantity;

    [Header("Paramètres")]
    public int warningThreshold = 10;

    [Header("Statut")]
    public StockStatus status;

    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponentInChildren<Renderer>();
        UpdateStatus();
    }

    public void UpdateStatus()
    {
        if (quantity <= 0)
            status = StockStatus.Error;
        else if (quantity <= warningThreshold)
            status = StockStatus.Warning;
        else
            status = StockStatus.OK;

        UpdateColor();
    }

    void UpdateColor()
    {
        // Si Start n'est pas passé OU si le Renderer est sur un enfant
        if (objectRenderer == null)
            objectRenderer = GetComponentInChildren<Renderer>();

        if (objectRenderer == null)
        {
            Debug.LogWarning($"WarehouseItem: Aucun Renderer trouvé sur {gameObject.name} (ni enfant).");
            return;
        }

        switch (status)
        {
            case StockStatus.OK:
                objectRenderer.material.color = Color.green;
                break;

            case StockStatus.Warning:
                objectRenderer.material.color = Color.yellow;
                break;

            case StockStatus.Error:
                objectRenderer.material.color = Color.red;
                break;
        }
    }

    public void AddStock(int amount)
    {
        quantity += amount;
        UpdateStatus();
    }
}
