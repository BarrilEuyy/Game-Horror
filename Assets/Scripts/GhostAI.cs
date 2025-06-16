using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 15f;
    public float giveUpTime = 30f;

    private NavMeshAgent agent;
    private float timeSinceLastSeen;
    private bool isChasing = false;

    private PlayerHide playerHide;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerHide = player.GetComponent<PlayerHide>();
          GameObject playerObj = GameObject.FindWithTag("Player");
    if (playerObj != null)
    {
        player = playerObj.transform;
        playerHide = playerObj.GetComponent<PlayerHide>();
    }
    else
    {
        Debug.LogWarning("Player not found! Pastikan tag 'Player' sudah dipasang.");
    }
    }   
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
// jika ingin hantu nya tidak mengejar saat player bersembunyi, tambahkan ini !playerHide.isHiding
        if (CanSeePlayer() && distance <= chaseRange)
        {
            isChasing = true;
            timeSinceLastSeen = 0f;
            agent.SetDestination(player.position);
        }
        else if (isChasing)
        {
            timeSinceLastSeen += Time.deltaTime;

            if (timeSinceLastSeen >= giveUpTime)
            {
                isChasing = false;
                Patrol();
            }
            else
            {
                agent.SetDestination(player.position);
            }
        }
        else
        {
            Patrol();
        }
    }

    bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 direction = (player.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, direction, out hit, chaseRange))
        {
            if (hit.transform == player)
                return true;
        }
        return false;
    }

    void Patrol()
    {
        // Untuk saat ini hantu diam jika tidak mengejar
        agent.SetDestination(transform.position);
    }
}
