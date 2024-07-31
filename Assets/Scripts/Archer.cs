using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Damageable))]
public class Archer : Knight
{
    public float waypointReachDistance = 0.1f;
    public List<Transform> waypoints;
    Transform nextWaypoint;
    int waypointNum = 0;

    Damageable damageable;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();

        GameObject[] waypointObjects = GameObject.FindGameObjectsWithTag("WayPoint");
        foreach (GameObject waypointObject in waypointObjects)
        {
            waypoints.Add(waypointObject.transform);
        }

        if (waypoints.Count > 0)
        {
            nextWaypoint = waypoints[waypointNum];
        }
        else
        {
            Debug.LogWarning("No waypoints set for Archer.");
        }
    }

    void Update()
    {
        this.HasTarget = attackZone.detectedColliders.Count > 0;

        if (this.AttackCooldown > 0)
        {
            this.AttackCooldown -= Time.deltaTime;
        }

        if (damageable.IsAlive)
        {
            if (this.CanMove && !this.HasTarget)
            {
                MoveTowardsWaypoint();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    private void MoveTowardsWaypoint()
    {
        if (nextWaypoint == null) return;

        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.velocity = directionToWaypoint * maxSpeed;

        if (distance <= waypointReachDistance)
        {
            waypointNum++;

            if (waypointNum >= waypoints.Count)
            {
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];
        }
        UpdateDirection();
    }



    private void UpdateDirection()
    {
        Vector3 currentLocalScale = transform.localScale;
        if (transform.localScale.x > 0)
        {
            if (rb.velocity.x < 0)
            {
                FlipDirection();
            }
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                FlipDirection();
            }
        }
    }
}
