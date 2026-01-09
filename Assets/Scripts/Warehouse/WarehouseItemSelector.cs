using UnityEngine;
using TMPro;

public class WarehouseItemSelector : MonoBehaviour
{
    public Camera playerCamera;
    public float maxDistance = 5f;

    [Header("UI")]
    public TMP_Text itemNameText;
    public TMP_Text quantityText;
    public TMP_Text statusText;

    [Header("Feedback sélection")]
    public float liftHeight = 0.3f;

    private WarehouseItem selectedItem;
    private WarehouseItem lastItem;

    private Vector3 lastItemOriginalPos;

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
        // RESET ancien item
        if (lastItem != null)
        {
            lastItem.transform.position = lastItemOriginalPos;

            Transform oldOutline = lastItem.transform.Find("Outline");
            if (oldOutline != null)
                oldOutline.gameObject.SetActive(false);
        }

        // NOUVEL item
        selectedItem = item;
        lastItem = selectedItem;

        lastItemOriginalPos = selectedItem.transform.position;

        // Surélévation
        selectedItem.transform.position += Vector3.up * liftHeight;

        // Activer outline
        Transform outline = selectedItem.transform.Find("Outline");
        if (outline != null)
            outline.gameObject.SetActive(true);

        UpdateUI();
    }

    void UpdateUI()
    {
        if (selectedItem != null)
        {
            itemNameText.text = "Nom : " + selectedItem.itemName;
            quantityText.text = "Quantité : " + selectedItem.quantity;
            statusText.text = "Statut : " + selectedItem.status;
        }
    }
}
