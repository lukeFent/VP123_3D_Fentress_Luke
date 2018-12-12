using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {

        animator.gameObject.transform.parent.GetComponent<AIcontol>().alert.SetActive(false);
        animator.gameObject.transform.parent.GetComponent<AIcontol>().patroling = true;
        animator.gameObject.transform.parent.GetComponent<AIcontol>().enemyMaterial.color = Color.green;
        animator.gameObject.transform.parent.GetComponent<AIcontol>().SetDesination();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {

        if (animator.gameObject.transform.parent.GetComponent<AIcontol>().isTravelling && animator.gameObject.transform.parent.GetComponent<AIcontol>().navMeshAgent.remainingDistance <= 2.0f)
        {
            animator.gameObject.transform.parent.GetComponent<AIcontol>().isTravelling = false;
            animator.gameObject.transform.parent.GetComponent<AIcontol>().wayPointsVisited++;

            if (animator.gameObject.transform.parent.GetComponent<AIcontol>().canWait)
            {
                animator.gameObject.transform.parent.GetComponent<AIcontol>().isWaiting = true;
                animator.gameObject.transform.parent.GetComponent<AIcontol>().waitTimer = 0f;
            }
            else
            {

                //patroling = true; 
                animator.gameObject.transform.parent.GetComponent<AIcontol>().SetDesination();
            }

        }

        if (animator.gameObject.transform.parent.GetComponent<AIcontol>().isWaiting)
        {
            animator.gameObject.transform.parent.GetComponent<AIcontol>().waitTimer += Time.deltaTime;
            if (animator.gameObject.transform.parent.GetComponent<AIcontol>().waitTimer >= animator.gameObject.transform.parent.GetComponent<AIcontol>().waitTime)
            {
                animator.gameObject.transform.parent.GetComponent<AIcontol>().isWaiting = false;
                animator.gameObject.transform.parent.GetComponent<AIcontol>().SetDesination();
            }
        }





    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.parent.GetComponent<AIcontol>().patroling = false;


    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
