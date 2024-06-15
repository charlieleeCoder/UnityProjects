using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // Navigation
    public Transform patrolRoute;
    public List<Transform> locations;

    // Private nav
    private int _locationIndex = 0;
    private NavMeshAgent _agent;
    private bool _patrolling = true;

    // Player awareness
    public Transform playerLocation;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        playerLocation = GameObject.Find("Player").transform;
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
        // Do nothing
        if (locations.Count == 0) 
        {
            return;
        }

        // Next location
        _agent.destination = locations[_locationIndex].position;

        // Increment index and loop
        _locationIndex = (_locationIndex + 1) % locations.Count;
    }

    // When in larger trigger collider range
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            // Stop patrol
            _patrolling = false;

            // Now attacking
            Debug.Log("Player detected - attack!");
            _agent.destination = playerLocation.position;
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

    // Health & death
    private int _lives;
    public int enemyLives
    {
        get { return _lives; }

        private set 
        { 
            _lives = value; 
            
            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("This cruel world...");
            }
        
        } 
    }

    // Remove health
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            enemyLives -= 1;
            Debug.Log("Hit!");
        }
    }
}
