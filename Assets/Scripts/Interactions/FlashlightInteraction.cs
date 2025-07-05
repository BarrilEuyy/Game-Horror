using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class FlashlightInteraction : MonoBehaviour, IInteractable
{
    public Item itemData;

    public GameObject lightInWorld;
    Light playerLight;
    private Button btnActive;
    private GameObject playerCam;
    public GameObject btn;

    public void Interact(GameObject interactor)
    {
        // Tambahkan ke inventory
        InventoryManager.Instance.AddItem(itemData);
        playerCam = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>().gameObject;

        if (playerLight == null)
            playerLight = playerCam.transform.Find("Light").GetComponent<Light>();
        // Buat tombol aksi
        if (btnActive == null)
            btn = Instantiate(itemData.actionButton, InventoryManager.Instance.actionButtonPos);

        btnActive = btn.GetComponent<Button>();
        btnActive.onClick.RemoveAllListeners();
        btnActive.onClick.AddListener(() =>
        {
            bool state = playerLight.gameObject.activeSelf;
            lightInWorld.SetActive(!state);
            playerLight.gameObject.SetActive(!state);
        });
    }

    void Update()
    {

        if (playerLight != null)
        {
            if (InventoryManager.Instance.inventoryItems.Contains(item: itemData))
                btn.SetActive(true);
            else
                Destroy(btn);
        }

    }
}
