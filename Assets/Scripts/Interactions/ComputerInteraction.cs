using UnityEngine;

public class ComputerInteraction : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
