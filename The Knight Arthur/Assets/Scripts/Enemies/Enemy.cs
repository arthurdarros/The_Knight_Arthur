using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        //decrease damage from the life
        currentHealth -= damage;

        //Play hurt animation
        animator.SetTrigger("hurt");
        //Checking if life is empty
        if(currentHealth <=0)
        {
            Die();
        }
    }

    void Die()
    {
        //Die animation
        animator.SetBool("isDead", true);

        //Disable the enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
