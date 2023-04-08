using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody enemyRB;
    Vector3 knockbackdir;
    public float health = 50;
    public bool hitcooldown = true;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(gameObject);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile" && hitcooldown)
        {
            knockbackdir = other.transform.position - transform.position;
            enemyRB.AddForce(knockbackdir * 3f, ForceMode.Impulse);
            health -= 25;
            hitcooldown = false;
            StartCoroutine(ResetMovement());
        }

        if (other.tag == "Mine" && hitcooldown)
        {
            knockbackdir = other.transform.position - transform.position;
            enemyRB.AddForce(knockbackdir * 3f, ForceMode.Impulse);
            health -= 45;
            hitcooldown = false;
            StartCoroutine(ResetMovement());
        }
    }

    IEnumerator ResetMovement()
    {
        yield return new WaitForSeconds(0.5f);
        enemyRB.velocity = Vector3.zero;
        hitcooldown = true;
    }
}
