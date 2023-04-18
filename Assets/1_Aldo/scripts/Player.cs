using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    #region variables
    [Header("Stats")]
    public int HP;
    public int HPLimit=10;
    public int Hp
    {
        get
        {
            return HP;
        }
        set
        {
            HP = Mathf.Clamp(value,0, HPLimit);
            UIManager.instance.slHP.value = Hp;
            CheckForDeath();
        }
    }
    [Header("Components")]
    public Animator anmtr;
    public Rigidbody2D rbody;
    public SpriteRenderer sprRenderer;
    public CheckpointManager checkManager;
    [Header("Movement")]
    public float movementSpeed;
    float currentSpeed;
    [Header("Jump")]
    public Transform feet;
    public float jumpForce;
    public float raycastLenght;
    public LayerMask groundLayer;
    bool willJump;
    [Header("Attack")]
    public GameObject swordCollider;
    bool isAttacking;
    [Header("Dash")]
    public bool isDashing;
    public float dashingSpeed;
    public float dashingDuration;

    public UIManager uimanager;
    #endregion
    void Start()
    {
        Hp = HPLimit;
        UIManager.instance.slHP.maxValue = HPLimit;
        UIManager.instance.slHP.value = Hp;
        if (checkManager.GetActiveCheckpoint())
        {
            transform.position = checkManager.GetActiveCheckpoint().transform.position;
        }
    }

    void Update()
    {
        if(!isAttacking&&!isDashing)
            Inputs();
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
    }
    void Inputs()
    {
        currentSpeed = Input.GetAxis("Horizontal");
        anmtr.SetFloat("walkspeed", currentSpeed);
        if (currentSpeed != 0)
        {
            if (currentSpeed < 0)
                sprRenderer.flipX = true;
            else
                sprRenderer.flipX = false;
        }

        //jump
        RaycastHit2D hit = Physics2D.Raycast(feet.position, Vector2.down, raycastLenght, groundLayer);
        if (hit.collider)
        {
            anmtr.SetBool("isJumping", false);
            if (Input.GetButtonDown("Jump"))
            {
                willJump = true;
            }
        }
        else
        {
            anmtr.SetBool("isJumping", true);
            willJump = false;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Dash();
        }
    }

    #region movement
    void Movement()
    {
        if (!isDashing)
        {
            rbody.velocity = currentSpeed * Vector2.right * movementSpeed + rbody.velocity.y * Vector2.up;
        }
        else
        {
            float direction = 0;
            if (!sprRenderer.flipX)
            {
                direction = 1;

            }
            else
            {
                direction = -1;
            }
            rbody.velocity = direction * Vector2.right * dashingSpeed; //+ rbody.velocity.y * Vector2.up;
        }

    }

    void Dash()
    {
        anmtr.SetBool("isJumping", false);
        anmtr.SetBool("IsDashing", true);
        isDashing = true;
        Invoke("StopDashing", dashingDuration);
    }

    void StopDashing()
    {
        anmtr.SetBool("IsDashing", false);
        isDashing = false;
    }
    void Jump()
    {
        if (willJump)
        {
            rbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            willJump = false;
        }
        
    }
    #endregion
    #region attack
    void Attack()
    {
        anmtr.SetBool("isJumping", false);
        anmtr.SetTrigger("Attack");
        isAttacking = true;
        currentSpeed = 0;
        anmtr.SetFloat("walkspeed", currentSpeed);
        if (sprRenderer.flipX)
            swordCollider.transform.localScale = new Vector3(-1, 1, 1);
        else
            swordCollider.transform.localScale = Vector3.one;
    }
    public void EnableSwordCollider()
    {
        swordCollider.SetActive(true);
    }
    public void DisableSwordCollider()
    {
        swordCollider.SetActive(false);
        isAttacking = false;
    }
    #endregion

    #region Collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fireball"))
        {
            Hp--;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Potion"))
        {
            if (collision.GetComponent<Potion>())
            {
                Hp += collision.GetComponent<Potion>().healthRecovered;
                Destroy(collision.gameObject);
            }
        }
        else if (collision.CompareTag("Hazard"))
        {
            Hp-=20;
            rbody.velocity = Vector2.right * rbody.velocity.x;
            rbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anmtr.SetBool("isJumping", true);
        }
        else if (collision.CompareTag("Enemy"))
        {
            Hp--;
            rbody.velocity = Vector2.right * rbody.velocity.x;

        }
        else if (collision.CompareTag("Finish"))
        {
            checkManager.CurrentCheckpoint = -1;

            if (SaveManager.HasHighScore(SceneManager.GetActiveScene().buildIndex))
            {
                if (SaveManager.LoadHighScore(SceneManager.GetActiveScene().buildIndex) > Time.timeSinceLevelLoad)
                {
                    SaveManager.SaveHighScore(SceneManager.GetActiveScene().buildIndex, Time.timeSinceLevelLoad);
                }
            }
            else
            {
                SaveManager.SaveHighScore(SceneManager.GetActiveScene().buildIndex, Time.timeSinceLevelLoad);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (collision.CompareTag("Stone"))
        {
            sCORE.points++;
            Destroy(collision.gameObject);
        }
    }
    #endregion
    void CheckForDeath()
    {
        if (Hp <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //
        }

    }
}
