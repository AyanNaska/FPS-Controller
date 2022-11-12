using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RaycastShootComplete : MonoBehaviour
{
    public float damabge = 10f;
    public float impactforce = 100f;
    public float range = 100f;
    public float fireRate = 15f;

    public Camera FPScam;
    public GameObject muzzleFlash;
    public AudioSource bang;

    public Button shootButton;
    public void Start()
    {
        bang= GetComponent<AudioSource>();
    }
    public void Shoot()
    {
        StartCoroutine(fire());
    }
    IEnumerator fire()
    {
        bang.Play();
        RaycastHit hit;
        if (Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            enemy_behaviour enemy = hit.transform.GetComponent<enemy_behaviour>();
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactforce);
            }
            if(enemy != null)
            {
                enemy.damage(10f);
            }
        }
        shootButton.interactable = false;
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        muzzleFlash.SetActive(false);
        shootButton.interactable = true;
    }
}
