using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text progressText;
    public TMP_Text finalStatusText;

    private WarehouseItem[] allItems;

    void Start()
    {
        allItems = FindObjectsOfType<WarehouseItem>();
        UpdateGlobalStatus();
    }

    void Update()
    {
        UpdateGlobalStatus();
    }

    void UpdateGlobalStatus()
    {
        int okCount = 0;
        int warningCount = 0;
        int errorCount = 0;

        foreach (var item in allItems)
        {
            switch (item.status)
            {
                case StockStatus.OK:
                    okCount++;
                    break;
                case StockStatus.Warning:
                    warningCount++;
                    break;
                case StockStatus.Error:
                    errorCount++;
                    break;
            }
        }

        int total = allItems.Length;

        // PROGRESSION
        if (progressText != null)
        {
            progressText.text =
                $"Stabilisation : {okCount}/{total}\n" +
                $"Warning : {warningCount} | Error : {errorCount}";
        }

        // VALIDATION FINALE
        if (okCount == total)
        {
            if (finalStatusText != null)
            {
                finalStatusText.text = "ENTREPÔT STABILISÉ";
                finalStatusText.color = Color.green;
            }
        }
        else
        {
            if (finalStatusText != null)
            {
                finalStatusText.text = "ACTION REQUISE";
                finalStatusText.color = Color.red;
            }
        }
    }
}
