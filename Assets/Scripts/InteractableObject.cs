using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public void Interact()
    {
        // Logika ketika objek diinteraksi
        Debug.Log("Objek diambil: " + gameObject.name);

        // Contoh: menghilangkan objek
        Destroy(gameObject);

        // Atau bisa juga memindahkan ke inventory
        // InventoryManager.Instance.AddItem(gameObject);
    }

    public void InteractComputer()
    {
        Debug.Log("interaksi dengan komputer");
    }
}