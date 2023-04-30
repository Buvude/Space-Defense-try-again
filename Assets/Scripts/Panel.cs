using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Repair()
    {
        if (gameManager.isShipDamaged == true)
        {
            gameManager.shipStatusText.text = "Ship Status:\n Ship has been repaired";
            gameManager.isShipDamaged = false;
            StartCoroutine(gameManager.BreakShip());
        }
    }
}
