using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerAI : MonoBehaviour
{
    public float scanningRadius = 10f;
    public LayerMask predatorLayer;
    public LayerMask grassLayer;
    public float moveSpeed = 2f;
    public float wanderRadius = 5f;

    private Vector3 targetPosition;

    void Start()
    {
        InvokeRepeating("MakeDecision", 0f, 0.75f);
    }

    void MakeDecision()
    {
        Collider[] predators = Physics.OverlapSphere(transform.position, scanningRadius, predatorLayer);
        if (predators.Length > 0)
        {
            Vector3 predatorDirection = (transform.position - predators[0].transform.position).normalized;
            targetPosition = transform.position + new Vector3(predatorDirection.x, 0f, predatorDirection.z) * scanningRadius;
        }
        else
        {
            Collider[] grasses = Physics.OverlapSphere(transform.position, scanningRadius, grassLayer);
            if (grasses.Length > 0)
            {
                targetPosition = new Vector3(grasses[0].transform.position.x, transform.position.y, grasses[0].transform.position.z);
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
