using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public bool targeting;
    public List<GameObject> enemiesInProx;
    public float shortestDistance;
    public float distance;

    // Update is called once per frame
    void Update()
    {
        if (targeting)
        {
            for (int i = 0; i < enemiesInProx.Count; i++)
            {
                distance = Vector3.Distance(enemiesInProx[i].transform.position, transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    transform.LookAt(enemiesInProx[i].transform);
                }
            }
        }
    }
}
