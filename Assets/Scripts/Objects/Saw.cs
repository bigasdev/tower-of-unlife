using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : DamagePlayer
{
    public GameObject[] waypoints;
    public float speed;
    private int current = 0;
    [SerializeField] private float wRadius = .5f;
    private void Start() {
        foreach(var w in waypoints){
            w.transform.parent = null;
        }
    }
    private void Update()
    {
        if(Vector2.Distance(waypoints[current].transform.position, transform.position) < wRadius)
        {
            current++;
            if(current >= waypoints.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
    }

}
