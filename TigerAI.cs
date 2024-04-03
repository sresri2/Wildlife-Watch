using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerAI : MonoBehaviour
{
    public float scanningRadius = 15f;
    public LayerMask deerLayer;
    public LayerMask restingSpotLayer;
    public float moveSpeed = 3f;
    public float wanderRadius = 5f;

    private Vector3 targetPosition;

    void Start()
    {
        InvokeRepeating("MakeDecision", 0f, 0.75f);
    }

    void MakeDecision()
    {
        Collider[] deers = Physics.OverlapSphere(transform.position, scanningRadius, deerLayer);
        if (deers.Length > 0)
        {
            targetPosition = new Vector3(deers[0].transform.position.x, transform.position.y, deers[0].transform.position.z);
        }
        else
        {
            Collider[] restingSpots = Physics.OverlapSphere(transform.position, scanningRadius, restingSpotLayer);
            if (restingSpots.Length > 0)
            {
                targetPosition = new Vector3(restingSpots[0].transform.position.x, transform.position.y, restingSpots[0].transform.position.z);
            }
            else
            {
                targetPosition = transform.position + new Vector3(Random.insideUnitSphere.x, 0f, Random.insideUnitSphere.z) * wanderRadius;
            }
        }
        transform.LookAt(targetPosition);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z), moveSpeed * Time.deltaTime);
    }
}
