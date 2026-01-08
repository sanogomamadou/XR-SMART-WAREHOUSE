using UnityEngine;
using TMPro;
using System.Collections;

public class ActionManager : MonoBehaviour
{
    [Header("Références")]
    public WarehouseItemSelector selector;

    [Header("UI Feedback (optionnel mais recommandé)")]
    public TMP_Text feedbackText;
    public float feedbackDuration = 1.5f;

    [Header("Paramètres actions")]
    public int restockAmount = 10;

    Coroutine feedbackRoutine;

    void Update()
    {
        if (selector == null) return;

        WarehouseItem item = selector.SelectedItem;
        if (item == null) return;

        // R = Restock
        if (Input.GetKeyDown(KeyCode.R))
        {
            item.AddStock(restockAmount);
            ShowFeedback($"Restock: +{restockAmount} sur {item.itemName}");
        }

        // F = Fix (si Error)
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (item.status == StockStatus.Error)
            {
                item.quantity = 1;
                item.UpdateStatus();
                ShowFeedback($"Fix appliqué sur {item.itemName}");
            }
            else
            {
                ShowFeedback($"Fix impossible: {item.itemName} n'est pas en Error");
            }
        }
    }

    void ShowFeedback(string msg)
    {
        if (feedbackText == null)
        {
            Debug.Log(msg);
            return;
        }

        feedbackText.text = msg;

        if (feedbackRoutine != null)
            StopCoroutine(feedbackRoutine);

        feedbackRoutine = StartCoroutine(ClearFeedbackAfterDelay());
    }

    IEnumerator ClearFeedbackAfterDelay()
    {
        yield return new WaitForSeconds(feedbackDuration);
        feedbackText.text = "";
    }
}
