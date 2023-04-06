using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDefenseScript : MonoBehaviour
{
    public GameObject turret;
    public GameObject mine;
    public float scrap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scrap >= 50 && Input.GetKeyDown(KeyCode.T))
        {
            Instantiate(turret, transform.position, transform.rotation);
            scrap -= 50;
        }else if (scrap >= 25 && Input.GetKeyDown(KeyCode.G))
        {
            Instantiate(mine, transform.position, transform.rotation);
            scrap -= 25;
        }
    }
}