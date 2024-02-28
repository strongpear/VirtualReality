using UnityEngine;

public class PowerUpAttractor : MonoBehaviour
{
    public float attractionRadius = 10f;
    public float attractionForce = 5f;
    public float powerUpDuration = 5f;
    private bool isAttracting = false;
    private bool collidedPlayerIt;
    private int collidedPlayerNumber;
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided with Pull Powerup");
        if (other.gameObject.CompareTag("Player"))
        {           
            collidedPlayerNumber = other.gameObject.GetComponent<CollisionManager>().playerNumber;
            collidedPlayerIt = other.gameObject.GetComponent<CollisionManager>().isIt;
            isAttracting = true;
            GetComponent<Renderer>().enabled = false; // Hide the power-up when collected
            Invoke("StopAttracting", powerUpDuration); // Stop attracting after the duration
        }
    }

    private void FixedUpdate()
    {
        if (isAttracting)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, attractionRadius);

            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Player"))
                {
                    CollisionManager colCollisionManager = col.GetComponent<CollisionManager>();

                    // If player is the one who hit the thing, don't affect them
                    if (colCollisionManager.playerNumber == collidedPlayerNumber)
                    {
                        continue;
                    }

                    bool colPlayerIt = colCollisionManager.isIt;
                    Rigidbody rb = col.GetComponent<Rigidbody>();
                    Vector3 direction = transform.position - col.transform.position;

                    // If the it player tags it, everybody else gets dragged closer
                    if (collidedPlayerIt)
                    {
                        if (rb != null)
                        {
                            rb.AddForce(direction.normalized * attractionForce, ForceMode.Force);
                        }
                    }

                    // If a chaser tags the power up and a player is close, they get pushed away
                    else if (!collidedPlayerIt && colPlayerIt)
                    {
                        if (rb != null)
                        {
                            rb.AddForce(-(direction.normalized) * attractionForce, ForceMode.Force);
                        }
                    }

                }
            }
        }
    }

    private void StopAttracting()
    {
        isAttracting = false;
        Destroy(gameObject); // Destroy the power-up after its effect expires
    }
}