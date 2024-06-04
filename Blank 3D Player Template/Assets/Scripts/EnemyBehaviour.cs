using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // Navigation
    public Transform patrolRoute;
    public List<Transform> locations;

    private int _locationIndex = 0;
    private NavMeshAgent _agent;
    private bool _patrolling = true;

    // Player awareness
    public Transform playerLocation;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        InitialisePatrolRoute();

        MoveToNextPatrolLocation();
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.remainingDistance < 0.2f && !_agent.pathPending && _patrolling)
        {
            MoveToNextPatrolLocation();
        }

    }

    // Establish locations to travel
    void InitialisePatrolRoute()
    {
        foreach(Transform t in patrolRoute)
        {
            locations.Add(t);
            Debug.Log(t);
        }
    }

    // Swap location
    void MoveToNextPatrolLocation()
    {
        _agent.destination = locations[_locationIndex].position;
    }

    // When in larger trigger collider range
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            // Now attacking
            _patrolling = false;
            Debug.Log("Player detected - attack!");
        }
    }

    // When player moves outside of trigger collider
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            // Now attacking
            _patrolling = true;
            Debug.Log("Player lost - back to patrol...");
        }
    }
}
