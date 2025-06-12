using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory Settings")]
    public int inventoryCapacity;
    public List<Item> inventoryItems = new List<Item>();

    [Header("UI References")]
    public Transform inventoryItemsTrans;
    public Transform inventoryArea;
    public GameObject inventoryUIPrefab;
    public Transform itemSlot;
    public float targetSizeUI;
    public Button dropItemButton;
    public Transform actionButtonPos;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(Item item)
    {
        if (inventoryItems.Count >= inventoryCapacity) return;

        item.inspectItem.gameObject.SetActive(false);
        item.inspectItem.transform.SetParent(inventoryArea);
        item.inspectItem.transform.localPosition = Vector3.zero;
        item.worldScale = item.inspectItem.transform.localScale;
        inventoryItems.Add(item);
        RefreshUI();
    }

    public void RemoveItem(Item item)
    {
        inventoryItems.Remove(item);
        RefreshUI();
    }

    void RefreshUI()
    {
        // Bersihkan UI lama
        foreach (Transform child in itemSlot)
            Destroy(child.gameObject);

        // Buat ulang UI untuk setiap item
        foreach (Item item in inventoryItems)
        {
            GameObject itemUI = Instantiate(inventoryUIPrefab, itemSlot);
            itemUI.GetComponent<Image>().sprite = item.itemSprite;

            Button btn = itemUI.GetComponent<Button>();
            btn.onClick.AddListener(() => OnItemSelected(item, itemUI));
        }
    }

    void OnItemSelected(Item item, GameObject itemUI)
    {

        foreach (Transform child in actionButtonPos)
        {
            if (item.actionButton != null)
            {
                item.actionButton.gameObject.SetActive(true);

                if (child.gameObject.activeSelf != item.actionButton.gameObject)
                    child.gameObject.SetActive(false);
            }
            // else
            // {
            //     child.gameObject.SetActive(false);
            // }
        }

        dropItemButton.gameObject.SetActive(true);
        dropItemButton.onClick.RemoveAllListeners(); // Hindari listener ganda
        dropItemButton.onClick.AddListener(() => DropItem(itemUI, item));

        // Sembunyikan model lama jika ada
        if (inventoryItemsTrans.childCount > 0)
        {
            GameObject currentItem = inventoryItemsTrans.GetChild(0).gameObject;
            ResetInspectedItem(currentItem);
        }

        // Tampilkan model baru
        GameObject model = item.inspectItem;
        model.transform.SetParent(inventoryItemsTrans);
        model.transform.localPosition = Vector3.zero;
        model.SetActive(true);

        // Atur skala
        AutoScaleModel(model);
        if (item.inspectScale != Vector3.zero)
            model.transform.localScale = item.inspectScale;
        else
            item.inspectScale = model.transform.localScale;

        // Atur layer
        LayerChanger(model, 7);

        // Nonaktifkan physics sementara
        Rigidbody rb = model.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;
    }

    void ResetInspectedItem(GameObject model)
    {
        // Kembalikan model sebelumnya
        IInteractable interactable = model.GetComponent<IInteractable>();
        if (interactable != null)
        {
            MonoBehaviour script = interactable as MonoBehaviour;
            var field = script.GetType().GetField("itemData");
            if (field?.GetValue(script) is Item previousItem)
            {
                model.transform.localScale = previousItem.worldScale;
            }
        }

        model.transform.SetParent(inventoryArea);
        model.transform.localPosition = Vector3.zero;
        model.SetActive(false);
    }

    void DropItem(GameObject itemUI, Item item)
    {
        Transform dropPos = GameObject.FindGameObjectWithTag("Player")
                                       .GetComponent<PlayerInteraction>().DropPos;

        GameObject model = item.inspectItem;
        model.transform.SetParent(null);
        model.transform.position = dropPos.position;
        model.transform.localScale = item.worldScale;
        model.SetActive(true);

        Rigidbody rb = model.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;

        LayerChanger(model, 3);

        Destroy(itemUI);
        RemoveItem(item);
    }

    void AutoScaleModel(GameObject model)
    {
        Renderer[] renderers = model.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return;

        Bounds bounds = renderers[0].bounds;
        foreach (Renderer rend in renderers)
            bounds.Encapsulate(rend.bounds);

        float maxSize = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
        if (maxSize <= 0) return;

        float scale = targetSizeUI / maxSize;
        model.transform.localScale *= scale;
    }

    void LayerChanger(GameObject obj, int layer)
    {
        if (obj == null) return;
        obj.layer = layer;
        foreach (Transform child in obj.transform)
            LayerChanger(child.gameObject, layer);
    }
}
