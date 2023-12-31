using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [Header ("Attack Parameters")]
    private float attackRange = 0.5f;
    private int attackDamage = 25;
    private float attackRate = 2f;
    private float nextAttackTime = 0f;

    [Header ("Health Components")]
    private int maxHealth = 100;
    int currentHealth;

    [Header ("Attack Sound")]
    [SerializeField] private AudioClip attackSound;

    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        
    }

    void Attack()
    {
        //Play an attack animation
        animator.SetTrigger("Attack");
        SoundManager.instance.PlaySound(attackSound);
        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;
        //Draw the line from the range of the attack
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
