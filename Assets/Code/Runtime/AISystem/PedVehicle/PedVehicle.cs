using UnityEngine;
using UnityEngine.AI;

public class PedVehicle : MonoBehaviour
{
    public GameObject initialWayPointObject; // The initial waypoint the car will start moving to

    private Transform currentWayPoint; // Current waypoint the car is moving towards
    private NavMeshAgent vehicleAgent; // NavMeshAgent to move the car

    private void Start()
    {
        // Ensure that the initial waypoint is valid and get its transform
        if (initialWayPointObject != null)
        {
            currentWayPoint = initialWayPointObject.transform;
        }
        else
        {
            Debug.LogError("Initial waypoint object is not assigned!");
            return;
        }

        // Initialize the NavMeshAgent
        vehicleAgent = GetComponent<NavMeshAgent>();

        // Ensure the agent exists before moving to the first waypoint
        if (vehicleAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing!");
            return;
        }
    }

    private void Update()
    {
        if (currentWayPoint != null)
        {
            // Set the vehicle's destination to the current waypoint
            vehicleAgent.destination = currentWayPoint.position;

            // Check if the vehicle has reached the current waypoint
            if (Vector3.Distance(transform.position, currentWayPoint.position) < 0.5f)
            {
                // Move to the next waypoint once the current one is reached
                MoveToNextWaypoint();
            }
        }
    }

    void MoveToNextWaypoint()
    {
        if (currentWayPoint != null)
        {
            // Ensure the waypoint has the WayPoint component
            WayPoint wayPoint = currentWayPoint.GetComponent<WayPoint>();
            if (wayPoint != null && wayPoint.nextWayPoints.Length > 0)
            {
                // Move to a random waypoint from the next waypoints
                currentWayPoint = GetRandomWaypoint(wayPoint);
            }
            else
            {
                Debug.LogWarning("No next waypoints available or WayPoint script is missing!");
            }
        }
    }

    Transform GetRandomWaypoint(WayPoint wayPoint)
    {
        if (wayPoint.nextWayPoints.Length == 0)
        {
            Debug.LogWarning("Waypoint array is empty!");
            return null; // Return null if no waypoints exist
        }

        // Get a random index from the waypoints array
        int randomIndex = Random.Range(0, wayPoint.nextWayPoints.Length);

        // Return the randomly selected Transform
        return wayPoint.nextWayPoints[randomIndex];
    }
}
