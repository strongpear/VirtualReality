using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPointManager : MonoBehaviour
{        
    public Transform worldSphereTransform;
    private float sphereRadius;

    // Start is called before the first frame update
    void Start()
    {
        sphereRadius = worldSphereTransform.localScale.x / 2;
    }

    // Helper function for transferring it-ness and respawning
    public Vector3 RandomPointOnSphere()
    {
        Vector3 randomUnitPoint = Random.insideUnitSphere;
        Vector3 randomPoint = worldSphereTransform.position + randomUnitPoint * (sphereRadius + 0.1f);
        return randomPoint;
    }


}
