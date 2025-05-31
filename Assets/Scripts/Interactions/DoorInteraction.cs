using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    private bool isOpen = false;

    public void Interact()
    {
        isOpen = !isOpen;
        Debug.Log("Pintu " + (isOpen ? "dibuka" : "ditutup"));

        // Contoh animasi rotasi pintu
        float targetAngle = isOpen ? -90f : 0f;
        transform.localRotation = Quaternion.Euler(0, targetAngle, 0);
    }
}
