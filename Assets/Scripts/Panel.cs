using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public Material thisMaterial;
    public Texture Broken, unbroken;
    public bool broken = false;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        thisMaterial.mainTexture= Broken;
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        gameManager.panelsInScene.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (broken)
        {
            thisMaterial.mainTexture = Broken;
        }
        else
        {
            thisMaterial.mainTexture = unbroken;
        }
    }

    public void Repair()
    {
        if (gameManager.isShipDamaged == true && broken)
        {
            broken = false;
            gameManager.shipStatusText.text = "Ship Status:\n Ship has been repaired";
            gameManager.isShipDamaged = false;
            StartCoroutine(gameManager.BreakShip());
        }
    }
}
