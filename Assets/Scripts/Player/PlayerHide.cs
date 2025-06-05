using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    public bool isHiding = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HideSpot"))
        {
            isHiding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HideSpot"))
        {
            isHiding = false;
        }
    }
}
