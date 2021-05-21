using UnityEngine;

public class WayPoints : MonoBehaviour
{
    // Array for the waypoints that enemy move
    public static Transform[] positions;

    void Awake()
    {
        // Give the array a size the same as how many waypoint we put under the gameobject
        positions = new Transform[transform.childCount];

        // Load all the child waypoints in the array within the loop
        for (int i = 0; i < positions.Length; i++)
        {
            // Put the waypoint that is the child waypoint of this gameobject to the array
            positions[i] = transform.GetChild(i);
        }
    }
}