using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public int gunDamage = 1;
    public float fireRate = 0.25f;
    public float range = 100f;

    public Camera playerCamera;

    public GameObject projectile;
    private Vector3 randomRotation;
    public AnimationClip Fire;
    public Animator gunSprite;

    public bool canShoot = true;
    private GameManager gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && gameManagerScript.isGameActive && !gameManagerScript.isGamePaused)
        {
            Shoot();
            canShoot = false;
            gunSprite.SetTrigger("Fire");
            //Fire.
        }
    }

    void Shoot()
    {
        for (int i = 0; i < 8; i++)
        {
            randomRotation = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
            Instantiate(projectile, transform.position, playerCamera.transform.rotation * Quaternion.Euler(randomRotation));
        }
        StartCoroutine(Cooldown());
    }

    

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
}
