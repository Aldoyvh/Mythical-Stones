using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemmyRun : MonoBehaviour
{
    [Header("Stats")]
    public int hitPoints;
    public float speed;
    public float HP=2;

    [Header("Raycast")]
    public float offset;
    public float raycastLemght;
    public LayerMask groundLayer;
    [Header("Components")]
    public Rigidbody2D rbody;
    public SpriteRenderer spriteRenderer;
    [Header("MovementLimits")]
    public float allowedDistance;
    public Vector2 startingPoint;
    // Start is called before the first frame update
    void Start()
    {
        startingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ChechForLimits();
    }

    private void FixedUpdate()
    {
        rbody.velocity = Vector2.right * speed;
        if (HP <= 0)
        {
            Destroy(this);
        }
    }
    void ChechForLimits()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.right * offset, Vector2.down, raycastLemght, groundLayer);
        if(hit.collider== null)
        {
            offset *= -1;
            speed *= -1;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    void CheckForDisdtance()
    {
        if(transform.position.x > startingPoint.x + allowedDistance || transform.position.x < startingPoint.x - allowedDistance)
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
            Destroy(gameObject);
        }
            
    }
}
