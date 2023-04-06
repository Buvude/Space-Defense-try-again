using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : EnemyGoalPoin
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*public void EnemySpawned()
    {

    }*/
    public Transform FirstPoint()
    {
        return EGP[((int)Random.Range(0f, EGP.Count ))].gameObject.transform;
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        print("Collision entered");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<NMA>().NewHome(this);//setting the home to the spawn (so they don't get caught in an endless loop)
            collision.gameObject.GetComponent<NMA>().UpdateTargetWthEGP(NextPoint().position);
        }
    }*/

    private void OnTriggerEnter(Collider collision)
    {
        print("Collision entered");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<NMA>().NewHome(this);//setting the home to the spawn (so they don't get caught in an endless loop)
            collision.gameObject.GetComponent<NMA>().CurrentState = NMA.EnemyState.Patrolling;
            collision.gameObject.GetComponent<NMA>().StateSwitch();
            collision.gameObject.GetComponent<NMA>().nextTarget = FirstPoint().gameObject.GetComponent<EnemyGoalPoin>();
            collision.gameObject.GetComponent<NMA>().UpdateTarget();


        }
    }
}
