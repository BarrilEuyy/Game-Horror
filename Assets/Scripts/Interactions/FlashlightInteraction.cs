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

    public void Interact(GameObject interactor)
    {
        // Tambahkan ke inventory
        InventoryManager.Instance.AddItem(itemData);
        playerCam = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>().gameObject;

        if(playerLight == null)
            playerLight = playerCam.GetComponentInChildren<Light>();

        // Buat tombol aksi
        if (btnActive == null)
            btnActive = Instantiate(itemData.actionButton, InventoryManager.Instance.actionButtonPos);
        btnActive.onClick.RemoveAllListeners();
        btnActive.onClick.AddListener(() =>
        {
            bool state = playerLight.gameObject.activeSelf;
            lightInWorld.SetActive(!state);
            playerLight.gameObject.SetActive(!state);
        });
        btnActive.gameObject.SetActive(false);
        itemData.actionButton = btnActive;
    }

    void Update()
    {

        if (playerLight != null)
        {
            if (!InventoryManager.Instance.inventoryItems.Contains(item: itemData))
            {
                playerLight.intensity = 0;
                btnActive.gameObject.SetActive(false);
            }
            else
            {

                playerLight.intensity = 1;
                btnActive.gameObject.SetActive(true);
            }
        }
    }
}
