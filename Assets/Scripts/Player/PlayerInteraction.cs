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
    public Transform DropPos;


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
            interactButton.gameObject.SetActive(false);
        }


    }



}