using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRepair : MonoBehaviour
{
    public Camera playerCamera;
    public float range = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Repair();
        }
    }



    void Repair()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward);

            Panel panel = hit.transform.GetComponent<Panel>();
            if (panel != null)
            {
                panel.Repair();
            }
        }
    }
}
