using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProximity : MonoBehaviour
{
    public TurretScript turretScript;
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        turretScript = transform.parent.gameObject.GetComponent<TurretScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (turretScript.enemiesInProx.Count > 0)
        {
            turretScript.targeting = true;
        }else
        {
            turretScript.targeting = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            turretScript.enemiesInProx.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            turretScript.enemiesInProx.Remove(other.gameObject);
        }
    }
}
