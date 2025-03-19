using System.Collections.Generic;
using UnityEngine;

public class PedVehicleManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints;

    [SerializeField]
    private GameObject[] pedVehicles;

    public int numberOfRandomTransforms = 50;

    private void Awake()
    {
        List<Transform> randomTransforms = GetRandomTransforms(wayPoints, numberOfRandomTransforms);

        for (int i = 0; i < pedVehicles.Length; i++)
        {
            pedVehicles[i].transform.position = wayPoints[i].position;
            pedVehicles[i].GetComponent<PedVehicle>().initialWayPointObject = wayPoints[i].gameObject;
        }
    }

    List<Transform> GetRandomTransforms(Transform[] array, int count)
    {
        List<Transform> transformList = new List<Transform>(array);

        // Ensure that the number of random transforms does not exceed the available transforms
        if (count > transformList.Count)
        {
            count = transformList.Count;
        }

        // Shuffle the list to randomize the order
        for (int i = 0; i < transformList.Count; i++)
        {
            Transform temp = transformList[i];
            int randomIndex = Random.Range(i, transformList.Count);
            transformList[i] = transformList[randomIndex];
            transformList[randomIndex] = temp;
        }

        // Take the first 'count' number of elements
        List<Transform> randomSelection = transformList.GetRange(0, count);

        return randomSelection;
    }
}
