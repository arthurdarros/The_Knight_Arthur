using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;

    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header ("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

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

    void Update()
    {
        if (PlayerInSight())
        {
            Attack();
        }
        if(enemyPatrol != null)
        enemyPatrol.enabled = !PlayerInSight();
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

    void Attack()
    {
        if(cooldownTimer >= attackCooldown)
        {
            anim.SetTrigger("meleeAttack");
            cooldownTimer = 0;
        }
        else
        {
            cooldownTimer += Time.deltaTime;
        }

    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = 
        Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0, Vector2.left, 0, playerLayer);

        if(hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if(PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
