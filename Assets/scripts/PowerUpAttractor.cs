using UnityEngine;

public class PowerUpAttractor : MonoBehaviour
{
    public float attractionRadius = 100f;
    public float attractionForce = 100f;
    public float powerUpDuration = 5f;
    private bool isAttracting = false;
    private bool collidedPlayerIt;
    private int collidedPlayerItInt;
    private int collidedPlayerNumber;
    ItGameManager itGameManager;
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {      
            Debug.Log("Collided with Pull Powerup");
            itGameManager = GameObject.Find("ItGameManager").GetComponent<ItGameManager>();     
            //collidedPlayerNumber = other.gameObject.GetComponent<CollisionManager>().playerNumber;
            collidedPlayerNumber = other.gameObject.GetComponent<CollisionManager>().GetPlayerNumber();
            collidedPlayerIt = other.gameObject.GetComponent<CollisionManager>().isIt;
            collidedPlayerItInt = other.gameObject.GetComponent<CollisionManager>().IsIt();
            

            Debug.Log(other.gameObject.GetComponent<CollisionManager>().playerNumber);
            isAttracting = true;
            GetComponent<Renderer>().enabled = false; // Hide the power-up when collected
            Invoke("StopAttracting", powerUpDuration); // Stop attracting after the duration
        }
    }

    private void FixedUpdate()
    {
        if (isAttracting)
        {
            Debug.Log(transform.position.x + ", " + transform.position.y + ", " + transform.position.z);
            Collider[] colliders = Physics.OverlapSphere(transform.position, attractionRadius);
            int numberOfColliders = colliders.Length;
            Debug.Log("Number of colliders detected: " + numberOfColliders);
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Player"))
                {
                    CollisionManager colCollisionManager = col.GetComponent<CollisionManager>();

                    // // If player is the one who hit the thing, don't affect them
                    //if (colCollisionManager.playerNumber == collidedPlayerNumber)
                    if (colCollisionManager.GetPlayerNumber() == collidedPlayerNumber)
                    {
                        Debug.Log("Skipping player");
                        continue;
                    }

                    bool colPlayerIt = colCollisionManager.isIt;
                    int colPlayerItInt = colCollisionManager.IsIt();
                    Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
                    Vector3 direction = transform.position - col.transform.position;
                    Debug.Log("Scan for Players");
                    //If the it player tags it, everybody else gets dragged closer
                    // if (collidedPlayerItInt == 1)
                    // {
                    //     Debug.Log("Adding Pulling Force1");
                    //     if (rb != null)
                    //     {
                    //         Debug.Log("Adding Pulling Force2");
                    //         rb.AddForce(direction.normalized * attractionForce, ForceMode.Force);
                    //     }
                    // }

                    if (rb != null)
                    {
                        Debug.Log("Adding Force");
                        rb.AddForce(direction.normalized * attractionForce, ForceMode.Force);
                    }

                    // If a chaser tags the power up and a player is close, they get pushed away
                    else if (collidedPlayerItInt == 0 && colPlayerItInt == 1)
                    {
                        Debug.Log("Adding Pushing Force1");
                        if (rb != null)
                        {
                            Debug.Log("Adding Pushing Force2");
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