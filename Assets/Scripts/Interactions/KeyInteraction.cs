using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteraction : MonoBehaviour, IInteractable
{
    public Item itemData;
    public void Interact(GameObject interactor)
    {
        InventoryManager.Instance.AddItem(itemData);
    }
}
