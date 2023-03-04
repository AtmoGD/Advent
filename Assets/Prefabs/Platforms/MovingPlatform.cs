using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private List<Vector2> waypoints = new List<Vector2>();
    [SerializeField] private float speed = 1f;

    private int waypointIndex = 0;

    private void Update()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex], speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, waypoints[waypointIndex]) < 0.1f)
            {
                waypointIndex++;
            }
        }
        else
        {
            waypointIndex = 0;
        }
    }
}
