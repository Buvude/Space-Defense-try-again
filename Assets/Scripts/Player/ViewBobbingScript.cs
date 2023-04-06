using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBobbingScript : MonoBehaviour
{

    public float amplitude;
    public float frequency;

    public new Transform camera;
    public Transform cameraHolder;

    private Rigidbody playerRB;

    private Vector3 startPos;
    private Vector3 pos;

    public Transform center;

    public PlayerMovementScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerScript = GetComponent<PlayerMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMotion();
        Stabilize();
    }

    private void CheckMotion()
    {
        float speed = new Vector3(playerRB.velocity.x, playerRB.velocity.y, playerRB.velocity.z).magnitude;

        if (speed >= 0.5 && playerScript.grounded || speed >= 0.5 && playerScript.groundedslope)
        {
            camera.localPosition = Vector3.Lerp(camera.localPosition, FootStep(), 1 * Time.deltaTime);
        }else
        {
            ResetPosition();
        }
    }

    private Vector3 FootStep()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitude;
        return pos;
    }

    private void ResetPosition()
    {
        if (camera.localPosition == startPos) return;
        camera.localPosition = Vector3.Lerp(camera.localPosition, startPos, 8 * Time.deltaTime);
    }
    
    private void Stabilize()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraHolder.localPosition.y, transform.position.z);
        pos += cameraHolder.forward * 15.0f;
        camera.LookAt(pos);
    }
}
