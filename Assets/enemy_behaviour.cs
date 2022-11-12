using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_behaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform TargetTransform;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float turn_speed = 30f;
    [SerializeField] private GameObject Player;

    private AnimatorStateInfo PlayerLayer0;
    public Animator anim;

    private Vector3 direction;

    private float dist;
    public float health = 100f;

    public GameObject muzzleFlash;
    public AudioSource bang;

    float time;
    float timeDelay;

    public GameObject self;

    /****************************/
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    private void Start()
    {
        anim.GetComponent<Animator>();
        bang = GetComponent<AudioSource>();
        time = 0f;
        timeDelay = 3f;
        /************************/
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }
    private void Update()
    {
        dist = Vector3.Distance(this.transform.position, TargetTransform.position);
        direction = Player.transform.position - this.transform.position;
        if (dist > 4 && canSeePlayer == true)
        {
            var rotation = Quaternion.LookRotation(TargetTransform.position - transform.position);
            rb.MoveRotation(rotation);
        }
        if (dist < 10 && dist > 4 && canSeePlayer == true)
        {
            time = time + 1 * Time.deltaTime;
            anim.SetBool("run", false);
            anim.SetBool("aiming", true);
            if (time >= timeDelay)
            {
                time = 0f;
                StartCoroutine(Shoot());
            }
        }
        else if (dist > 10 && canSeePlayer == true)
        {
            anim.SetBool("run", true);
        }
        else if (dist > 10 && canSeePlayer == false)
        {
            anim.SetBool("aiming", false);
        }
    }

    private void FixedUpdate()
    {

        if (dist > 10 && canSeePlayer == true)
        {

            rb.velocity = transform.forward * 7;

        }
        else
        {
            rb.velocity = transform.forward * 0;
        }
    }
    IEnumerator Death()
    {
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(3f);
        self.SetActive(false);
    }
    IEnumerator Shoot()
    {

        anim.SetTrigger("Shoot");
        bang.Play();
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, direction, out hit, 20))
        {
            Player_Health player = hit.transform.GetComponent<Player_Health>();
            if (player != null)
            {
                muzzleFlash.SetActive(true);
                yield return new WaitForSeconds(0.3f);
                muzzleFlash.SetActive(false);
                player.damage(10f);
            }
        }
        yield return new WaitForSeconds(2f);
    }
    public void damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(Death());
        }
    }
    /********************************/
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Knife")
        {
            StartCoroutine(Death());
        }
    }
}
