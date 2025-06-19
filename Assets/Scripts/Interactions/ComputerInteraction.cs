using System.Collections;
using UnityEngine;

public class ComputerInteraction : MonoBehaviour, IInteractable
{
    public Transform playerCamera;         // Drag MainCamera
    public Transform computerCameraPos;    // Drag Camera kosong di depan komputer
    public GameObject emailUI;             // Drag UI Email

    private Vector3 defaultPos;

    public GameObject playerUi;
    private Quaternion defaultRot;

    private bool isUsingComputer = false;
    private bool isTransitioning = false;
    public float smoothSpeed = 5f;
    public float emailUIDelay = 1f; // detik sebelum email UI muncul

    public void Interact(GameObject interactor)
    {
        // Simpan posisi awal
        if (!isUsingComputer)
        {
            defaultPos = playerCamera.position;
            defaultRot = playerCamera.rotation;
        }

        isUsingComputer = !isUsingComputer;
        isTransitioning = true;

        if (!isUsingComputer)
        {
            emailUI.SetActive(false);
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    void Update()
    {
        if (!isTransitioning || playerCamera == null) return;

        Vector3 targetPos = isUsingComputer ? computerCameraPos.position : defaultPos;
        Quaternion targetRot = isUsingComputer ? computerCameraPos.rotation : defaultRot;

        playerCamera.position = Vector3.Lerp(playerCamera.position, targetPos, Time.deltaTime * smoothSpeed);
        playerCamera.rotation = Quaternion.Lerp(playerCamera.rotation, targetRot, Time.deltaTime * smoothSpeed);

        if (Vector3.Distance(playerCamera.position, targetPos) < 0.01f)
        {
            isTransitioning = false;

            if (isUsingComputer)
            {
                playerUi.SetActive(false);
                 StartCoroutine(ShowEmailUIAfterDelay());
            }
        }
    }

    IEnumerator ShowEmailUIAfterDelay()
    {
        yield return new WaitForSeconds(emailUIDelay);
        emailUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

