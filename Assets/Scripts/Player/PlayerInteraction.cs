using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Transform dreamWorldSpawn; // drag posisi tujuan di Inspector

    [Header("Settings")]
    public float interactDistance = 3f;
    public Button interactButton;
    public Button dropButton;

    public LayerMask interactableLayer;

    [Header("Item")]
    public Transform DropPos;

    public Camera playerCam;

    [Header("Ghost Detection")]
    public string ghostTag = "Ghost";  // Pastikan ghost kamu pakai tag ini
    private float ghostLookTimer = 0f;
    public float timeToTriggerDream = 3f;
    private bool triggered = false;

    void Update()
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            // Interaksi biasa
            IInteractable interactable = hit.collider.GetComponent<IInteractable>()
                ?? hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                interactButton.gameObject.SetActive(true);
                interactButton.onClick.RemoveAllListeners();
                interactButton.onClick.AddListener(() => interactable.Interact(gameObject));
            }

            // Deteksi Ghost
            if (!triggered && hit.collider.CompareTag(ghostTag))
            {
                ghostLookTimer += Time.deltaTime;

                if (ghostLookTimer >= timeToTriggerDream)
                {
                    triggered = true;
                    StartCoroutine(EnterDreamWorld());
                }
            }
            else
            {
                ghostLookTimer = 0f; // Reset timer jika tidak melihat ghost
            }
        }
        else
        {
            interactButton.gameObject.SetActive(false);
            ghostLookTimer = 0f; // reset juga kalau tidak melihat apa-apa
        }
    }

    IEnumerator EnterDreamWorld()
    {
  Debug.Log("Masuk ke dunia mimpi dari raycast 👀😴");

    // Efek transisi
    Time.timeScale = 0.2f;
    yield return new WaitForSecondsRealtime(1f);
    Time.timeScale = 1f;

    // Teleport player
    CharacterController controller = GetComponent<CharacterController>();
    if (controller != null)
    {
        controller.enabled = false; // matikan dulu biar gak konflik
        transform.position = dreamWorldSpawn.position;
        controller.enabled = true;  // hidupkan lagi
    }
    else
    {
        transform.position = dreamWorldSpawn.position; // fallback
    }

    Debug.Log("Teleport ke dunia mimpi selesai.");
    }
}
