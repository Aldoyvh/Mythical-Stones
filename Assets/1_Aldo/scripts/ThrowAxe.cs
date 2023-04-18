using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAxe : MonoBehaviour
{
    [Header("TeacherThrower")]
    public GameObject Projectile;
    public float nextShot;
    public Transform parent;
    public float fireRate;
    public IEnumerator ieInstatiate;

    [Header("MyThrower")]
    public SpriteRenderer sprRenderer;
    public float Throwingduration;
    public Animator anmtr;
    public bool IsThrowing;

    void OnEnable()
    {
        InvokeRepeating("InstatiateFireball", fireRate, nextShot);
        //ieInstatiate= IEInstatiateFireballs(nextShot);
        //StartCoroutine(ieInstatiate);
    }

    private void OnDisable()
    {
        CancelInvoke();
        StopAllCoroutines();
    }

    IEnumerator IEInstatiateFireballs(float _nextshot)
    {
        yield return new WaitForSeconds(_nextshot);
        InstatiateFireball();
        StartCoroutine(IEInstatiateFireballs(_nextshot));
    }

    void InstatiateFireball()
    {
        GameObject go = Instantiate(Projectile, parent);
        go.transform.rotation = Quaternion.identity;
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<Fireball>().speedMovement *= (Random.Range(0, 2) * 2) - 1;
        go.GetComponent<Fireball>().SetUp();
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Time.time >= nextShot)
        {
            for (var i = 0; i < 1; i++)
            {
                Throw();
                Fireball.speedMovement = Random.Range(-5, 5);
                Projectile = Instantiate(Projectile, transform.position, transform.rotation);
                nextShot = Time.time + fireRate;

            }
        }
        if (Fireball.speedMovement < 0)
            sprRenderer.flipX = true;
        else
            sprRenderer.flipX = false;
    }

    void Throw()
    {
        anmtr.SetBool("IsThrowing", true);
        IsThrowing = true;
        Invoke("StopThrowing", Throwingduration);
    }

    void StopThrowing()
    {
        anmtr.SetBool("IsThrowing", false);
            IsThrowing = false;
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            Destroy(gameObject);
        }

    }
}
