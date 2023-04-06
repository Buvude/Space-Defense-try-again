using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NMA : MonoBehaviour
{
    public int enemyFireCooldown;
    public GameObject Fireball;
    public EnemyLineOfSightTest eLOS;
    public Animator ani;
    public float NMAspeed;
    public EnemyGoalPoin CurrentHome, nextTarget;//This will make the NMA "immune" to the hitbox on the EGP, so it can leave
    public enum EnemyState { Searching, Paused, Patrolling, Chasing, Attacking, Dead}; //depending on the state the enemy is in, it will act differently
    public Vector3 CurrentTarget; 
    private NavMeshAgent agent;
    public EnemyState CurrentState;
    // Start is called before the first frame update
    private void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        agent.speed = NMAspeed;
        CurrentTarget = new Vector3();
    }
    void Start()
    { 
        //TEMPORARY TEST FOR NAVMESH
        //make it so the spawn point is instatnly a "home" so that they don't search when spawned in

        /*CurrentTarget.Set(0f, 0f, 0f);*/
        /*UpdateTarget();*///putting this as a method so it can be called whenever, but not constantly every frame. This might optimize the game a bit more
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateTarget()
    {
        CurrentTarget = nextTarget.gameObject.GetComponent<Transform>().position;
        agent.destination = CurrentTarget;
    }
    /* public void UpdateTargetWthEGP()
     {
         CurrentTarget = nextTarget.gameObject.GetComponent<Transform>().position;
         UpdateTarget();
         *//*agent.destination = newPoint;*//*
         CurrentTarget = NextTargetPossibly;
         UpdateTarget();*//*
     }*/
    public void NewHome(EnemyGoalPoin EGP2)
    {
        CurrentHome = EGP2;
        /*CurrentState = EnemyState.Searching;*/
        /*StateSwitch();*/
        
        //play animation then switch state to patrolling (after choosing new path)
    }
    public void postAnimation()
    {
        Debug.Log("got to post animation");
        if (CurrentState == EnemyState.Searching)
        {
            CurrentState = EnemyState.Patrolling;
        }
        
        StateSwitch();
    }
    public void StateSwitch()//paused is an inbetween so stuff doesn't keep getting updated
    {
        switch (CurrentState)
        {
            case EnemyState.Searching:
                {
                    agent.speed = 0;
                    /*CurrentState = EnemyState.Paused;*/
                    ani.SetTrigger("SearchingTime");
                    break;
                }
            case EnemyState.Patrolling:
                {
                    /*UpdateTarget();*/
                    agent.speed = NMAspeed;
                    break;
                }
            case EnemyState.Chasing:
                {
                    agent.speed = NMAspeed;
                    StopCoroutine(TimeToShoot());
                    StartCoroutine(TimeToChase());
                    //set destination to player
                    /*UpdateTarget();*/
                    break;
                }
                
            case EnemyState.Attacking:
                {
                    agent.speed = 0;
                    StopCoroutine(TimeToChase());
                    StartCoroutine(TimeToShoot());
                    //attack player, and swith back to chasing after attack animation finishes if they are now out of range or maybe for when the game is paused
                    break;
                }
                
            case EnemyState.Dead:
                agent.speed = 0;
                //destroy enemy and such.
                break;
            case EnemyState.Paused:
                {
                    break;
                }
            default:
                break;
        }
        IEnumerator TimeToShoot()
        {
            while (true)
            {
                this.transform.LookAt(eLOS.Target);
                Quaternion rotationForAttack=new Quaternion();
                rotationForAttack.x = Vector3.Angle(transform.position, eLOS.Target.transform.position);
                rotationForAttack.y = transform.rotation.y;
                rotationForAttack.z = transform.rotation.z;
                Instantiate(Fireball, transform.position, transform.rotation);
                yield return new WaitForSeconds(enemyFireCooldown);
                if (!eLOS.WithinRange())
                {
                    CurrentState = EnemyState.Chasing;
                    StateSwitch();
                    break;
                }
            }
        
        }
    }
    IEnumerator TimeToChase()
    {/*
            print(Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, eLOS.gameObject.transform.position));
            yield return new WaitForEndOfFrame();*/
        while (CurrentState == EnemyState.Chasing)
        {
            //print("made it to corutine");
            //CurrentTarget = eLOS.Target.position;
            //UpdateTarget();
            agent.destination = eLOS.Target.position;
            /*if (eLOS.WithinRange())
            {
                CurrentState = EnemyState.Attacking;
                StateSwitch();
                StopCoroutine(TimeToChase());
                break;
            }*/
            yield return new WaitForSeconds(1f);
        }
    }
    public void OnPlayerAttack()
    {

    }

        //for searching play ~5 second animation of searching, then change state to patrolling (after setting new goal point)
        //For patrolling move towards new goal point, switch to searching once arrive in trigger for goal point
        //at any point if the enemy sees the player, or is attacked, or see's another enemy that is chasing/attacking the player
        //they will exit whatever state they are in, and enter chasing, in which they move towards the player until within range.
        //once within range they will enter attacking mode, when they die they enter death mode.
        //Chasing and attacking mode will swap between one another, but never go back to searching or patrolling.
}
