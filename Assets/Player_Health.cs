using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public float health = 100f;
    public GameObject gameover;
    public void damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        gameover.SetActive(true);
    }
}
