using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossRun : MonoBehaviour
{
    [Header("Stats")]
    public int hitPoints;
    public float speed;
    public float HP = 10;
    public Slider Life;

    [Header("Raycast")]
    public float offset;
    public float raycastLemght;
    public LayerMask groundLayer;
    [Header("Components")]
    public Rigidbody2D rbody;
    public SpriteRenderer spriteRenderer;
    public GameObject draco;
    [Header("MovementLimits")]
    public float allowedDistance;
    public Vector2 startingPoint;
    public CheckpointManager checkManager;
    // Start is called before the first frame update
    void Start()
    {
        startingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ChechForLimits();
        Life.value = HP;
    }

    private void FixedUpdate()
    {
        rbody.velocity = Vector2.right * speed;
        
    }
    void ChechForLimits()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.right * offset, Vector2.down, raycastLemght, groundLayer);
        if (hit.collider == null)
        {
            offset *= -1;
            speed *= -1;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    void CheckForDisdtance()
    {
        if (transform.position.x > startingPoint.x + allowedDistance || transform.position.x < startingPoint.x - allowedDistance)
        {
            offset *= -1;
            speed *= 1;
            spriteRenderer.flipX = !spriteRenderer.flipX;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            HP--;
            
        }
        if (HP <= 0)
        {
            Destroy(draco);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

   
}
