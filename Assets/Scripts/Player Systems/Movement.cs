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

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.inSpellMode) return;
        transform.position += Time.deltaTime*speed*(
            transform.forward * ((InputManager.Instance.GetKey("FWD") ? 1 : 0) - (InputManager.Instance.GetKey("BCK") ? 1 : 0)) +
            transform.right * ((InputManager.Instance.GetKey("RSTRF") ? 1 : 0) - (InputManager.Instance.GetKey("LSTRF") ? 1 : 0))
        ).normalized;
        float delta = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        transform.RotateAround(transform.position, Vector3.up, delta);
        ParticleSystem.ShapeModule shape = particleSystem.shape;
        shape.position = new Vector3(transform.position.x, -transform.position.z, 1);
        GameManager.Instance.playerPos = transform.position;
    }
}
