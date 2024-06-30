using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class Archer : MonoBehaviour
{
    public float walkAcceleration = 30f;
    public float maxSpeed = 3f;
    public float walkStopRate = 0.05f;    
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    public float waypointReachDistance = 0.1f;
    public List<Transform> waypoints;

    public float detectionRange; // Düþmanýn oyuncuyu algýlama mesafesi

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    Damageable damageable;
    Transform target; // Oyuncu karakterin Transform'u

    public enum WalkableDirection
    {
        Right,
        Left
    }

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get
        {
            return _walkDirection;
        }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * (-1), gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
                else
                {
                    Debug.LogError("Current walkable direction is not set to legal values of right or left");
                }
            }
            _walkDirection = value;
        }
    }

    public bool _hasTarget;

    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();

        // Oyuncu karakterin Transform'unu bul
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("Player with tag 'Player' not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }

        // Karakterin algýlanmasý ve yön deðiþtirmesi
        //DetectAndChaseTarget();
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
            Debug.Log("On wall");
        }
        if (!damageable.LockVelocity)
        {
            if (CanMove && touchingDirections.IsGrounded && !HasTarget) // Sadece hedef yokken hareket et
            {
                float xVelocity = Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime),
                    -maxSpeed, maxSpeed);
                rb.velocity = new Vector2(xVelocity, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y); //animasyon durdurulmalý
            }
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnCliffDetected()
    {
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
            Debug.Log("Cliff Detected");
        }
    }


    //private void DetectAndChaseTarget()
    //{
    //    if (target != null)
    //    {
    //        float distanceToTarget = Vector2.Distance(transform.position, target.position);

    //        if (distanceToTarget <= detectionRange)
    //        {
    //            Vector3 directionToTarget = (target.position - transform.position).normalized;

    //            if (directionToTarget.x > 0 && WalkDirection != WalkableDirection.Right)
    //            {
    //                WalkDirection = WalkableDirection.Right;
    //            }
    //            else if (directionToTarget.x < 0 && WalkDirection != WalkableDirection.Left)
    //            {
    //                WalkDirection = WalkableDirection.Left;
    //            }

    //            HasTarget = true;
    //        }
    //        else
    //        {
    //            HasTarget = false;
    //        }
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {

    }
}
