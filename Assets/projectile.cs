using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public GameObject projectille;
    public float launchVelocity = 700f;

    public void Fire()
    {
        GameObject ball = Instantiate(projectille, transform.position, transform.rotation);
        ball.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * launchVelocity);
    }
}
