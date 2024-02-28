using UnityEngine;

public class PowerUpAttractor : MonoBehaviour
{
    public float attractionRadius = 10f;
    public float attractionForce = 5f;
    public float powerUpDuration = 5f;
    private bool isAttracting = false;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided with Pull Powerup");
        if (other.gameObject.CompareTag("Player"))
        {
            
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

                if (col.CompareTag("Attractable"))
                {
                    Rigidbody rb = col.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        Vector3 direction = transform.position - col.transform.position;
                        rb.AddForce(direction.normalized * attractionForce, ForceMode.Force);
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