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
    public enum spriteState { idle, walking };//what animation should be playing
    SpriteFacingDirection CurrentDirection;
    public spriteState thisSpriteState;
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>().colorChangerTest.Add(this.GetComponent<Animator>());
    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempV3 = new Vector3(Player.position.x, Player.position.y, Player.position.z);
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
            //sR.sprite = frontFacing;
            CurrentDirection = SpriteFacingDirection.Forward;
        }
        else if (angleToPlayer < 135f&&angleToPlayer>45f)
        {
            //print("Right Facing Sprite");
            //sR.sprite = rightFacing;
            CurrentDirection = SpriteFacingDirection.Right;
        }
        else if ((angleToPlayer < 180f &&angleToPlayer>135)|| (angleToPlayer > -180f && angleToPlayer < -135f))
        {
            //print("Back Facing Sprite");
            //sR.sprite = awayFacing;
            CurrentDirection = SpriteFacingDirection.Back;
        }
        else if (angleToPlayer >-135f)
        {
            //print("Left Facing Sprite");
            //sR.sprite = leftFacing;
            CurrentDirection = SpriteFacingDirection.Left;
        }
        //print(angleToPlayer.ToString());
        switch (thisSpriteState)
        {
            case spriteState.idle:
                switch (CurrentDirection)
                {
                    case SpriteFacingDirection.Forward:
                        sR.sprite = frontFacing;
                        break;
                    case SpriteFacingDirection.Left:
                        sR.sprite = leftFacing;
                        break;
                    case SpriteFacingDirection.Right:
                        sR.sprite = rightFacing;
                        break;
                    case SpriteFacingDirection.Back:
                        sR.sprite = awayFacing;
                        break;
                    default:
                        break;
                }
                break;
            case spriteState.walking:
                switch (CurrentDirection)
                {
                    case SpriteFacingDirection.Forward:
                        break;
                    case SpriteFacingDirection.Left:
                        break;
                    case SpriteFacingDirection.Right:
                        break;
                    case SpriteFacingDirection.Back:
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
}
