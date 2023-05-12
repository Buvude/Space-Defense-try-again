using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody enemyRB;
    Vector3 knockbackdir;
    public float health = 50;
    public bool hitcooldown = true;
    public bool isAlive = true;
    internal GameManager gameManager;
    public int scrapToChange;
    private SpawnManager spawner;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        spawner = GameObject.FindGameObjectWithTag("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(gameObject);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        isAlive = false;
        this.GetComponent<NMA>().CurrentState = NMA.EnemyState.Dead;
        gameManager.UpdateScrap();
        spawner.enemyCount -= 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAlive)
        {
            if (other.tag == "Projectile" && hitcooldown)
            {
                knockbackdir = other.transform.position - transform.position;
                enemyRB.AddForce(knockbackdir * 3f, ForceMode.Impulse);
                health -= 25;
                if (health <= 0)
                {
                    Die();
                }
                hitcooldown = false;
                StartCoroutine(ResetMovement());
            }

            if (other.tag == "Mine" && hitcooldown)
            {
                knockbackdir = other.transform.position - transform.position;
                enemyRB.AddForce(knockbackdir * 3f, ForceMode.Impulse);
                health -= 25;
                if (health <= 0)
                {
                    Die();
                }
                hitcooldown = false;
                StartCoroutine(ResetMovement());
            }
        }
    }

    IEnumerator ResetMovement()
    {
        yield return new WaitForSeconds(0.5f);
        enemyRB.velocity = Vector3.zero;
        hitcooldown = true;
    }
}
