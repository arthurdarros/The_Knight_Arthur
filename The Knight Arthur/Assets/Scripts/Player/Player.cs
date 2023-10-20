using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header ("Movement Parameters")]
    [SerializeField]private float speed;
    [SerializeField]private float jumpForce;
    
    [Header("Jumping")]
    [SerializeField] private bool isJumping;


    [Header("Dashing Parameters")]
    [SerializeField]private float dashingVelocity = 5f;
    [SerializeField]private float dashingTime = 0.2f;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;

    [Header("Iframes Dashings")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header ("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D rig;
    private Animator anim;
    private TrailRenderer trailRenderer;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

   
    void Update()
    {
        Move();
        Jump();
        Dash();
    }
    
    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;

        if(Input.GetAxis("Horizontal") > 0)
        {
            anim.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if(Input.GetAxis("Horizontal") < 0)
        {
            anim.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        if(Input.GetAxis("Horizontal") == 0)
        {
            anim.SetBool("run", false);
        }

    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isJumping)
            {
                rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                anim.SetBool("jump", true);
                SoundManager.instance.PlaySound(jumpSound);
            }
        }
    }

    void Dash()
    {
        var dashInput = Input.GetButtonDown("Dash");

        if(dashInput && canDash)
        {
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            StartCoroutine(DashIframe());

            if(dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(transform.localScale.x, 0);
            }

            //Add stopping dash

            StartCoroutine(StopDashing());

        }

        anim.SetBool("isDashing", isDashing);

        if (isDashing)
        {
            rig.velocity = dashingDir.normalized * dashingVelocity;
            return;
        }

        if (isJumping == false)
        {
            canDash = true;
        }
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        isDashing = false;
    }

    private IEnumerator DashIframe()
    {
        Physics2D.IgnoreLayerCollision(8, 10, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 10, false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            isJumping = true;
        }
    }
}
