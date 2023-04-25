using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    public NMA Parent;
    public float speed;
    private Vector3 targetSpace;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        print("spawned projectile");
        targetSpace = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.LookAt(targetSpace);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                break;
            default:
                Destroy(this.gameObject);
                break;
        }
        //print("Trigger entered");
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.UpdateHealth(-10);
        }
    }

    
}
