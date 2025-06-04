using UnityEngine;

public class ComputerInteraction : MonoBehaviour, IInteractable
{
    public void Interact(GameObject interactor)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
