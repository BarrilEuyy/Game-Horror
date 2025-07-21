using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookInteraction : MonoBehaviour, IInteractable
{
    public Item itemData;

    public GameObject buttonUI;
    private AutoFlip book;

    public void Interact(GameObject interactor)
    {
        InventoryManager.Instance.AddItem(itemData);

        buttonUI = Instantiate(itemData.actionButton, InventoryManager.Instance.actionButtonPos);

        book = GetComponentInChildren<AutoFlip>();

        for (int i = 0; i < buttonUI.transform.childCount; i++)
        {
            if (i == 0)
                buttonUI.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(book.FlipRightPage);
            else
                buttonUI.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(book.FlipLeftPage);
        }

    }

    void Update()
    {
        if (InventoryManager.Instance.inventoryItems.Contains(itemData))
            buttonUI.SetActive(true);
        else
            Destroy(buttonUI);
    }
}
