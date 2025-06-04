using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    public bool isLocked;

    public Animation doorAnim;
    public AnimationClip openAnim;
    public AnimationClip closeAnim;

    private bool isOpen = false;
    public float speed;

    public void Interact(GameObject interactor)
    {
        if (isLocked)
        {
            PlayerInteraction player = interactor.GetComponent<PlayerInteraction>();
            if (player != null && player.hasKey)
            {
                UnlockDoor(player);
            }
            else
            {
                Debug.Log("Pintu terkunci. Kamu butuh kunci.");
                return;
            }
        }
        else
        {
            ToogleDoor();
        }

    }

    void UnlockDoor(PlayerInteraction player)
    {
        isOpen = true;
        player.hasKey = false;
        isLocked = false;
        Destroy(player.item);
        Debug.Log("Pintu terbuka!");
    }

    void ToogleDoor()
    {
        isOpen = !isOpen;
        Debug.Log("Pintu " + (isOpen ? "dibuka" : "ditutup"));

        Animator anim = GetComponent<Animator>() ?? GetComponentInChildren<Animator>();
        anim.SetBool("isOpen", isOpen);

    }

    // void RotateDoor()
    // {
    //     // Contoh animasi rotasi pintu
    //     float targetAngle = isOpen ? Mathf.SmoothDampAngle(transform.eulerAngles.y, -90f, ref r, 0.2f) : 0f;
    //     transform.localRotation = Quaternion.Euler(0, targetAngle, 0);
    // }
}
