using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header ("Health Parameters")]
    [SerializeField]private int maxHealth = 100;
    int currentHealth;

    [Header ("Death Sound")]
    [SerializeField] private AudioClip deathSound;

    private Animator anim;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        //decrease damage from the life
        currentHealth -= damage;

        //Play hurt animation
        anim.SetTrigger("hurt");
        //Checking if life is empty
        if(currentHealth <=0)
        {
            Die();
        }
    }

    void Die()
    {
        //Die animation
        anim.SetBool("isDead", true);
        SoundManager.instance.PlaySound(deathSound);

        //Disable the enemy
        if(GetComponentInParent<EnemyPatrol>()!= null)
            GetComponentInParent<EnemyPatrol>().enabled = false;

        if(GetComponent<EnemyMelee>() != null)
            GetComponent<EnemyMelee>().enabled = false;
        
        GetComponent<Collider2D>().enabled = false;

        this.enabled = false;
    }
}