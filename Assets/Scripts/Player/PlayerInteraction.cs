using UnityEngine;
using UnityEngine.UI;


public class PlayerInteraction : MonoBehaviour
{
    [Header("Settings")]
    public float interactDistance = 3f;
    public Button interactButton;
    public Button dropButton;

    public LayerMask interactableLayer;

    [Header("Item")]
    public bool hasKey;
    public Transform handTransform;
    public GameObject item;


    public Camera playerCam;

    void Update()
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Raycast hanya ke layer "Interactable"
        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>()
            ?? hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                interactButton.gameObject.SetActive(true);
                interactButton.onClick.RemoveAllListeners();
                interactButton.onClick.AddListener(() => interactable.Interact(gameObject));
            }
        }
        else
        {
            // currentTarget = null;
            interactButton.gameObject.SetActive(false);
        }

        dropButton.gameObject.SetActive((item != null) ? true : false);

    }

    public void UnpickItem()
    {
        if (item == null) return;


        item.transform.SetParent(null);
        item.transform.position = transform.position + transform.forward * 1.5f;

        if (item.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = false;
            rb.AddForce(transform.forward * 2f, ForceMode.Impulse);
        }

        if (item != null && hasKey)
            hasKey = false;

        item = null;
    }

}