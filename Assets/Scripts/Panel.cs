using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Repair()
    {
        if (gameManager.isShipDamaged == true)
        {
            gameManager.isShipDamaged = false;
            StartCoroutine(gameManager.BreakShip());
        }
    }
}
