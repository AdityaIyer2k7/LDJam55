using System;
using UnityEngine;

/*
 * Using keynames FWD, BCK, LSTRF, RSTRF
 */

public class Movement : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public new ParticleSystem particleSystem;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.inSpellMode) { animator.SetBool("isWalking", false); return; }
        Vector3 delPos = (
            transform.forward * ((InputManager.Instance.GetKey("FWD") ? 1 : 0) - (InputManager.Instance.GetKey("BCK") ? 1 : 0)) +
            transform.right * ((InputManager.Instance.GetKey("RSTRF") ? 1 : 0) - (InputManager.Instance.GetKey("LSTRF") ? 1 : 0))
        );
        if (delPos.sqrMagnitude >= 0.5)
        {
            transform.position += Time.deltaTime*speed*delPos.normalized;
            animator.SetBool("isWalking", true);
        }
        else animator.SetBool("isWalking", false);
        float delta = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        transform.RotateAround(transform.position, Vector3.up, delta);
        ParticleSystem.ShapeModule shape = particleSystem.shape;
        shape.position = new Vector3(transform.position.x, -transform.position.z, 0);
        GameManager.Instance.playerPos = transform.position;
    }
}
