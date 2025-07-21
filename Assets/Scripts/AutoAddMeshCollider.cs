using UnityEngine;

public class AutoAddMeshCollider : MonoBehaviour
{
    void Start()
    {
        foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
        {
            var go = meshFilter.gameObject;
            if (go.GetComponent<Collider>() == null)
            {
                go.AddComponent<MeshCollider>();
                go.GetComponent<MeshCollider>().convex = true;
            }
        }
    }
}
