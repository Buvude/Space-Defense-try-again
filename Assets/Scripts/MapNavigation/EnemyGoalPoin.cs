using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoalPoin : MonoBehaviour
{
    public List<EnemyGoalPoin> EGP = new List<EnemyGoalPoin>();
    // Start is called before the first frame update
    void Start()
    {
        //have a list of EGPs, for the enemy to select where to move randomly. At each EGP, the enemy will select from a valid EGP to go to next. 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Transform NextPoint()
    {
        return EGP[((int)Random.Range(0f, EGP.Count))].gameObject.transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<NMA>().NewHome(this);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy")&&collision.gameObject.GetComponent<NMA>().CurrentHome!=this)
        {
            if (collision.gameObject.GetComponent<NMA>().CurrentState != NMA.EnemyState.Chasing && collision.gameObject.GetComponent<NMA>().CurrentState != NMA.EnemyState.Attacking)
            {
                collision.gameObject.GetComponent<NMA>().NewHome(this);//setting the home to the spawn (so they don't get caught in an endless loop)
                collision.gameObject.GetComponent<NMA>().CurrentState = NMA.EnemyState.Searching;
                /*//temp testing for navmesh
                collision.gameObject.GetComponent<NMA>().CurrentState = NMA.EnemyState.Patrolling;
                //end temp test*/
                collision.gameObject.GetComponent<NMA>().StateSwitch();
                collision.gameObject.GetComponent<NMA>().nextTarget = NextPoint().gameObject.GetComponent<EnemyGoalPoin>();
                collision.gameObject.GetComponent<NMA>().UpdateTarget();
            }
        }
    }
    //add trigger area to trigger searching mode, and then set this EGP as their home.
}
