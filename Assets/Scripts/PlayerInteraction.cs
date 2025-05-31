using UnityEngine;
using UnityEngine.UI;


public class PlayerInteraction : MonoBehaviour
{
    [Header("Settings")]
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;
    public Button interactButton;

    public LayerMask interactableLayer;


    private Camera playerCam;

    void Start()
    {
        playerCam = GetComponent<Camera>();
    }

    void Update()
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Raycast hanya ke layer "Interactable"
        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();

            if (interactable != null)
            {
                interactButton.gameObject.SetActive(true);
                interactButton.onClick.RemoveAllListeners();
                interactButton.onClick.AddListener(() => HandleInteraction(interactable));
            }
        }
        else
        {
            // currentTarget = null;
            interactButton.gameObject.SetActive(false);
        }
    }
    void HandleInteraction(InteractableObject ObjectName)

    {
        // Debug.Log(ObjectName);
        switch (ObjectName.name)
        {
            case "computer":
                ObjectName.InteractComputer();
                break;


        }
    }
}