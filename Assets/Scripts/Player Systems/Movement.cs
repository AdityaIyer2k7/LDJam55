using System;
using UnityEngine;

/*
 * Using keynames FWD, BCK, LSTRF, RSTRF
 */

public class Movement : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public Transform particleSystems;

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime*speed*(
            transform.forward * ((InputManager.Instance.GetKey("FWD") ? 1 : 0) - (InputManager.Instance.GetKey("BCK") ? 1 : 0)) +
            transform.right * ((InputManager.Instance.GetKey("RSTRF") ? 1 : 0) - (InputManager.Instance.GetKey("LSTRF") ? 1 : 0))
        ).normalized;
        float delta = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        transform.RotateAround(transform.position, Vector3.up, delta);
        particleSystems.position = transform.position;
        GameManager.Instance.playerPos = transform.position;
    }
}
