using UnityEngine;
using TMPro; // très important !

public class WarehouseItemSelector : MonoBehaviour
{
    public Camera playerCamera;
    public float maxDistance = 5f;

    [Header("UI")]
    public TMP_Text itemNameText;
    public TMP_Text quantityText;
    public TMP_Text statusText;

    private WarehouseItem selectedItem;

    public WarehouseItem SelectedItem => selectedItem;

    void Update()
    {
        HandleSelection();
    }

    void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                WarehouseItem item = hit.collider.GetComponent<WarehouseItem>();
                if (item != null)
                {
                    SelectItem(item);
                }
            }
        }
    }

    void SelectItem(WarehouseItem item)
    {
        selectedItem = item;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (selectedItem != null)
        {
            itemNameText.text = "Nom : " + selectedItem.itemName;
            quantityText.text = "Quantité : " + selectedItem.quantity;
            statusText.text = "Statut : " + selectedItem.status.ToString();
        }
    }
}
