using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteFacePlayer : MonoBehaviour
{
    public GameObject ParentObject;
    public SpriteRenderer sR;
    public Sprite frontFacing, leftFacing, rightFacing, awayFacing; 
    public Transform Player;
    public enum SpriteFacingDirection {Forward, Left, Right, Back };//made for sprite animation/implimentation to make it easier. 
    SpriteFacingDirection CurrentDirection;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempV3 = new Vector3(Player.position.x, 0f, Player.position.z);
        Vector3 directionToPlayer = ParentObject.transform.position - Player.position;
        transform.LookAt(tempV3);
        float angleToPlayer;
        angleToPlayer = Vector3.SignedAngle(ParentObject.transform.forward, directionToPlayer, Vector3.up);
        /*if (angleToPlayer > 360f)
        {
            print("Angle to hight");
        }
        else*/ if (angleToPlayer < 45f && angleToPlayer > -45f)
        {
            //print("Front Facing Sprite");
            sR.sprite = frontFacing;
            CurrentDirection = SpriteFacingDirection.Forward;
        }
        else if (angleToPlayer < 135f&&angleToPlayer>45f)
        {
            //print("Right Facing Sprite");
            sR.sprite = rightFacing;
            CurrentDirection = SpriteFacingDirection.Right;
        }
        else if ((angleToPlayer < 180f &&angleToPlayer>135)|| (angleToPlayer > -180f && angleToPlayer < -135f))
        {
            //print("Back Facing Sprite");
            sR.sprite = awayFacing;
            CurrentDirection = SpriteFacingDirection.Back;
        }
        else if (angleToPlayer >-135f)
        {
            //print("Left Facing Sprite");
            sR.sprite = leftFacing;
            CurrentDirection = SpriteFacingDirection.Left;
        }
        //print(angleToPlayer.ToString());
    }
}
