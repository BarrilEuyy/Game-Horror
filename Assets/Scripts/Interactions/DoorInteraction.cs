using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorInteraction : MonoBehaviour, IInteractable
{
    [Header("Door Settings")]
    [SerializeField] private bool isLocked = true;
    [SerializeField] private string requiredKeyName = "Key";

    private Animator animator;
    private bool isOpen = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact(GameObject interactor)
    {
        if (isLocked)
        {
            Item keyItem = InventoryManager.Instance.inventoryItems
                .Find(item => item.itemName == requiredKeyName);

            if (keyItem != null)
            {
                UnlockDoor(keyItem);
            }
            else
            {
                Debug.Log("Pintu terkunci. Kamu butuh kunci.");
            }
        }
        else
        {
            ToggleDoor();
        }
    }

    private void UnlockDoor(Item keyItem)
    {
        isLocked = false;
        InventoryManager.Instance.RemoveItem(keyItem);
        Debug.Log("Pintu berhasil dibuka.");

        OpenDoor();
    }

    private void ToggleDoor()
    {
        if (isOpen)
            CloseDoor();
        else
            OpenDoor();
    }

    private void OpenDoor()
    {
        isOpen = true;
        animator.SetBool("isOpen", true);
        Debug.Log("Pintu dibuka.");
    }

    private void CloseDoor()
    {
        isOpen = false;
        animator.SetBool("isOpen", false);
        Debug.Log("Pintu ditutup.");
    }
}
