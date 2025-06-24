using UnityEngine;
using System.Collections;

public class GhostAI : MonoBehaviour
{
    public GameObject ghostPrefab;
    public Transform player;
    public float appearDuration = 2f;
    public float interval = 5f;
    public float distanceInFront = 3f;

    private GameObject ghostInstance;
    private Renderer ghostRenderer;
    private Collider ghostCollider;

    void Start()
    {
        ghostInstance = Instantiate(ghostPrefab);
        ghostRenderer = ghostInstance.GetComponentInChildren<Renderer>();
        ghostCollider = ghostInstance.GetComponent<Collider>();

        StartCoroutine(GhostAppearRoutine());
    }

    IEnumerator GhostAppearRoutine()
    {
        while (true)
        {
            // Tampilkan ghost
            Vector3 frontPos = player.position + player.forward * distanceInFront;
            ghostInstance.transform.position = frontPos;
            ghostInstance.transform.LookAt(player);

            ghostRenderer.enabled = true;
            ghostCollider.enabled = true;

            Debug.Log("👻 Ghost muncul");

            yield return new WaitForSeconds(appearDuration);

            // Sembunyikan ghost
            ghostRenderer.enabled = false;
            ghostCollider.enabled = false;

            Debug.Log("💨 Ghost hilang");

            yield return new WaitForSeconds(interval);
        }
    }
}
