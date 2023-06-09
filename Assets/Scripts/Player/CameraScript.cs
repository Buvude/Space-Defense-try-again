using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private float sensitivityX = 200;
    private float sensitivityY = 200;

    public new Camera camera;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    public float xRotation;
    public float yRotation;
    private GameManager gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameManagerScript = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.isGameActive && !gameManagerScript.isGamePaused)
        {
            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");

            yRotation += mouseX * sensitivityX * multiplier;
            xRotation -= mouseY * sensitivityY * multiplier;

            camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

            transform.rotation = Quaternion.Euler(0, yRotation, 0);

            xRotation = Mathf.Clamp(xRotation, -90, 90);
        }
        
    }
}
