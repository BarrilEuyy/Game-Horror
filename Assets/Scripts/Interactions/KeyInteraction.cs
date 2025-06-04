using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteraction : MonoBehaviour, IInteractable
{

    public void Interact(GameObject interactor)
    {
        PlayerInteraction player = interactor.GetComponent<PlayerInteraction>();
        Rigidbody rb = GetComponent<Rigidbody>();

        player.hasKey = true;
        player.item = gameObject;
        transform.SetParent(player.handTransform);
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
        rb.isKinematic = true;
    }
}
