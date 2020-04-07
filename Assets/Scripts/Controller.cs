using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed, jumpForce, wallJumpForce;
    public Transform groundCheck,ceilingCheck, wallCheckL;
    public bool grounded, walled,ceiled;
    public Animator anim;
    public AudioClip jumpClip, wallJump, hitGround;
    private AudioSource source;
    private bool forceRight;
    private Rigidbody2D rb;
    private Vector3 smoothVel = Vector3.zero;
    private float stickTimer, lastDir,canJump,runTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    public void Move(float dir, bool jump)
    {
        Vector2 targetVel = new Vector2(dir * speed * (runTimer > 1.2f ? 1.7f:1), rb.velocity.y);
        if (walled && !grounded && !ceiled)
        {
            if (stickTimer > 0)
            {
                targetVel.x = 0;
            }
            targetVel.y *= 0.7f;
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && -Physics2D.gravity.y >= 0)
            {
                source.clip = wallJump;
                source.Play();
                targetVel = new Vector2(rb.velocity.x + wallJumpForce * (forceRight ? 1 : -1), jumpForce);
            }else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) && -Physics2D.gravity.y <= 0))
            {
                source.clip = wallJump;
                source.Play();
                targetVel = new Vector2(rb.velocity.x + wallJumpForce * (forceRight ? 1 : -1), -jumpForce);
            }
        }
        if (-Physics2D.gravity.y == 0 && !grounded && !ceiled && !walled)
        {
            targetVel = rb.velocity;
        }
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVel, ref smoothVel, (grounded || walled || ceiled ? 0 : 0.5f));
        if (jump)
        {
            if (canJump > 0.1f)
            {
                source.clip = jumpClip;
                source.Play();
                if (Physics2D.gravity.y < 0)
                {
                    Debug.Log("AAAAAA");
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce * 1);
                }
                else if (Physics2D.gravity.y > 0)
                {
                    Debug.Log("BBBB");
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce * -1);
                }
                else
                {
                    Debug.Log("CCCC");
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce * (Input.GetAxisRaw("Vertical") > 0 ? 1 : -1));
                }
            }
            else
            {
                rb.AddForce(new Vector2(0, -6.5f * -Physics2D.gravity.y/3.3f));
            }
        }
        if (!grounded && !jump)
        {
            rb.AddForce(new Vector2(0, -16.5f * -Physics2D.gravity.y/3.3f));
        }
    }

    public void FixedUpdate()
    {
        Distort();
        if (grounded || ceiled)
        {
            canJump += Time.deltaTime;
        }
        else
        {
            canJump = 0;
        }
        float dir = Input.GetAxisRaw("Horizontal");
        bool jump = (Input.GetAxisRaw("Vertical") != 0);
        if (dir == lastDir)
        {
            if (walled)
            {
                stickTimer -= Time.deltaTime;
            }
            else
            {
                stickTimer = 0.2f;
            }
            if(grounded || ceiled)
            {
                runTimer += Time.deltaTime;
            }
            else{
                runTimer = 0;
            }
        }
        else
        {
            stickTimer = 0.2f;
            runTimer = 0;
        }
        lastDir = dir;
        Move(dir, jump);
    }

    public void TriggerCheck(Transform c, bool enter)
    {
        if (c == groundCheck || c == ceilingCheck)
        {
            if (enter)
            {
                if (c == groundCheck && -Physics2D.gravity.y >= 0)
                {
                    grounded = true;
                    if (Mathf.Abs(rb.velocity.y) > 10)
                    {
                        source.clip = hitGround;
                        source.Play();
                        anim.SetTrigger("Squatch");
                    }
                }else if (c == ceilingCheck && -Physics2D.gravity.y <= 0)
                {
                    ceiled = true;
                    if (Mathf.Abs(rb.velocity.y) > 10)
                    {
                        anim.SetTrigger("Squatch");
                        source.clip = hitGround;
                        source.Play();
                    }
                }
            }
            else
            {
                if (c == groundCheck)
                {
                    grounded = false;
                }
                else
                {
                    ceiled = false;
                }
            }
        }
        else
        {
            if (enter)
            {
                Debug.Log(rb.velocity.y * Physics2D.gravity.y + " " + walled);
                if (rb.velocity.y * Physics2D.gravity.y > 0 || Physics2D.gravity.y == 0) {
                    forceRight = (c == wallCheckL);
                    walled = true;
                }
            }
            else
            {
                walled = false;
            }
        }
    }

    public void Distort()
    {
        float vel = Mathf.Abs(rb.velocity.y);
        if(vel > 65)
        {
            vel = 65;
        }
        float x = 1 - (vel * 0.005f);
        float y = 1 + (vel * 0.005f);
        transform.localScale = new Vector3(x,y,1);
    }
}
