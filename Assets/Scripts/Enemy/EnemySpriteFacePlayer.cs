 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteFacePlayer : MonoBehaviour
{
    public List<Animator> AnimationManagers = new List<Animator>(); 
    public GameObject ParentObject;
    //public SpriteRenderer sR;
    public SpriteRenderer frontFacing, leftFacing, rightFacing, awayFacing; 
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
        if (angleToPlayer < 45f && angleToPlayer > -45f)//determining with sprite renderer to have active
        {
            //print("Front Facing Sprite");
            //sR.sprite = frontFacing;
            CurrentDirection = SpriteFacingDirection.Back;
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
            CurrentDirection = SpriteFacingDirection.Forward;
        }
        else if (angleToPlayer >-135f)
        {
            //print("Left Facing Sprite");
            //sR.sprite = leftFacing;
            CurrentDirection = SpriteFacingDirection.Left;
        }
        switch (CurrentDirection)
        {
            case SpriteFacingDirection.Forward:
                frontFacing.enabled = true;
                awayFacing.enabled = false;
                rightFacing.enabled = false;
                leftFacing.enabled = false;
                break;
            case SpriteFacingDirection.Left:
                frontFacing.enabled = false;
                awayFacing.enabled = false;
                rightFacing.enabled = false;
                leftFacing.enabled = true;
                break;
            case SpriteFacingDirection.Right:
                frontFacing.enabled = false;
                awayFacing.enabled = false;
                rightFacing.enabled = true;
                leftFacing.enabled = false;
                break;
            case SpriteFacingDirection.Back:
                frontFacing.enabled = false;
                awayFacing.enabled = true;
                rightFacing.enabled = false;
                leftFacing.enabled = false;
                break;
            default:
                break;
        }
        //print(angleToPlayer.ToString());
        switch (thisSpriteState)//activating the correct spriete renderer
        {
            case spriteState.idle:
                {
                    foreach (Animator ani in AnimationManagers)
                    {
                        if (!ani.GetBool("Idle"))
                        {
                            ani.SetTrigger("IdleTrigger");
                            ani.SetBool("Walking", false);
                            ani.SetBool("Idle", true);
                        }
                        
                    }
                }
                break;
            case spriteState.walking:
                {
                    foreach (Animator ani in AnimationManagers)
                    {
                        if (!ani.GetBool("Walking"))
                        {
                            ani.SetTrigger("WalkingTrigger");
                            ani.SetBool("Walking", true);
                            ani.SetBool("Idle", false);
                        }
                        
                    }
                }
                break;
            default:
                break;
        }
    }
}
