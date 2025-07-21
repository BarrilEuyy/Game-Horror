using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saltinteraction : MonoBehaviour, IInteractable
{
    public Item itemData;
    GhostAI ghost;

    public void Interact(GameObject interactor)
    {
        InventoryManager.Instance.AddItem(itemData);
        if(GameObject.Find("GhostManager") != null)
            ghost.saltIsTaken = true;
    }

    void Update()
    {
        ghost = GameObject.Find("GhostManager")?.GetComponent<GhostAI>();

        if (ghost == null)
            return;

        if (InventoryManager.Instance.inventoryItems.Contains(itemData))
            ghost.saltIsTaken = true;
        else
            ghost.saltIsTaken = false;
    }
}
