using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectiles : MonoBehaviour
{
    public GameObject projectile;
    public float launchVelocity = 700f;

    public void Fire()
    {
        GameObject ball = Instantiate(projectile, transform.position,
                                                      transform.rotation);
        ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3
                                             (0, launchVelocity, 0));
    }
}
