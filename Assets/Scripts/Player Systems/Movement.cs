using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

/*
 * Using keynames FWD, BCK, LSTRF, RSTRF, JMP, SNK
 */

public enum MOVE_STATE {
    IDLE,
    RUNNING,
    CROUCHING
}

public class Movement : MonoBehaviour
{
    [Header("Settings")]
    public bool instaMove = true;
    public bool forceMove = false;
    public bool canStrafe = true;
    public bool canAirMove = true;
    public bool canAirJump = false;
    public bool forceTurn = false;

    [Header("Speeds")]
    [Tooltip("m/s\nOnly for instaMove")] public float maxSpeed = 10;
    [Tooltip("N\nOnly for forceMove")] public float moveForce = 10;
    [Tooltip("m/s2")] public float accl;
    [Tooltip("rad/s")] public float turnSpeed;
    [Tooltip("s")] public float maxTurnTime;
    [Tooltip("N m")] public float turnForce;

    Rigidbody rb;
    bool onGround = false;
    List<GameObject> groundsTouching = new();
    MOVE_STATE state = MOVE_STATE.IDLE;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canAirMove || onGround)
        {
            Vector3 net = Vector3.Normalize(
                transform.forward*(InputManager.Instance.GetKey("FWD")-InputManager.Instance.GetKey("BCK")) + 
                transform.right*(canStrafe ? (InputManager.Instance.GetKey("RSTRF")-InputManager.Instance.GetKey("LSTRF")) : 0)
            );
            if (instaMove) rb.velocity = maxSpeed*net;
            else if (forceMove) rb.AddForce(moveForce*net*MathF.Sin(0.5f*MathF.PI*(maxSpeed-rb.velocity.magnitude)/maxSpeed));
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground") {
            onGround = true;
            groundsTouching.Add(other.gameObject);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground") {
            groundsTouching.Remove(other.gameObject);
            if (groundsTouching.Count == 0) onGround = false;
        }
    }
}
