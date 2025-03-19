using UnityEngine;
using UnityEditor;
public class WayPoint : MonoBehaviour
{
    public Transform[] nextWayPoints;

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // Set the Gizmo color to something noticeable
        Gizmos.color = Color.green;

        // Iterate through each target in the array and draw an arrow from this object to the target
        foreach (Transform target in nextWayPoints)
        {
            if (target != null)
            {
                // Draw an arrow from the current object's position to the target's position
                Gizmos.DrawLine(transform.position, target.position);
                // Optionally, you can use DrawArrow handles to create a more noticeable arrow shape:
                DrawArrow(transform.position, target.position);
            }
        }

        // Check if the current GameObject is selected
        if (Selection.activeGameObject == gameObject)
        {
            // Set the Gizmo color to the highlight color
            Gizmos.color = Color.red;

            foreach (Transform target in nextWayPoints)
                Gizmos.DrawSphere(target.position, 2f);
        }
    }

    // This is a helper function to draw an arrow with a custom length
    private void DrawArrow(Vector3 from, Vector3 to)
    {
        // You can use Gizmos.DrawLine to draw an arrow manually or use a built-in arrow shape
        Vector3 direction = (to - from).normalized;
        float arrowHeadLength = 0f;  // Adjust this for arrow size
        float arrowHeadAngle = 20f;    // Adjust this for the angle of the arrowhead

        // Draw the main line of the arrow
        Gizmos.DrawLine(from, to);

        // Draw the arrowhead
        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * Vector3.forward;
        Gizmos.DrawLine(to, to - right * arrowHeadLength);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, arrowHeadAngle, 0) * Vector3.forward;
        Gizmos.DrawLine(to, to - left * arrowHeadLength);
    }
#endif
}
