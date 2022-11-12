using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class animationcontroll : MonoBehaviour
{
    public Animator anim;
    float horizontalInput;
    float verticalInput;
    public GameObject gun;
    public GameObject knife;
    private AnimatorStateInfo PlayerLayer0;
    public GameObject ShootButton;
    public GameObject PunchButton;

    [SerializeField] private Rig animrig;
    void Start()
    {
        anim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = SimpleInput.GetAxis("Horizontal");
        verticalInput = SimpleInput.GetAxis("Vertical");
        PlayerLayer0 = anim.GetCurrentAnimatorStateInfo(0);
        if (verticalInput > 0.01)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
        if (knife.activeInHierarchy == true)
        {
            PunchButton.SetActive(true);
            ShootButton.SetActive(false);
        }
        else if (gun.activeInHierarchy == true)
        {
            PunchButton.SetActive(false);
            ShootButton.SetActive(true);
        }
    }
    public void Jump()
    {
        anim.SetTrigger("Jump");
    }
    public void slide()
    {
        anim.SetTrigger("Slide");
    }
    public void Punch()
    {
        anim.SetTrigger("Punch");
    }
    public void Change_Weapon()
    {
       if(gun.activeInHierarchy==true)
       {
            gun.SetActive(false);
            knife.SetActive(true);
       }
       else
       {
            gun.SetActive(true);
            knife.SetActive(false);
        }
    }    
}
