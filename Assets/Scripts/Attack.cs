using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.parent.GetComponent<AIcontol>().alert.SetActive(true);
        animator.gameObject.transform.parent.GetComponent<AIcontol>().enemyWeapon.fireControl(true);
        //animator.gameObject.transform.parent.GetComponent<AIcontol>().patroling = false;
        animator.gameObject.transform.parent.GetComponent<AIcontol>().enemyMaterial.color = Color.red;
        //animator.gameObject.transform.parent.GetComponent<AIcontol>().SetDesination();
        animator.gameObject.transform.parent.GetComponent<AIcontol>().navMeshAgent.isStopped = true; 



    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //float speed = animator.gameObject.transform.parent.GetComponent<AIcontol>().looking;
        Transform AItrans = animator.gameObject.transform.parent;

        Transform pTransform = animator.gameObject.transform.parent.GetComponent<AIcontol>().player.transform;

       // Quaternion rotation = Quaternion.LookRotation(pTransform.position);

       // Vector3 direction = pTransform.position - AItrans.position;

       // Quaternion toRotation = Quaternion.FromToRotation(AItrans.forward, direction);
        //Quaternion rot = Quaternion.LookRotation(pTransform.forward, Vector3.forward);


      //  AItrans.rotation = Quaternion.Lerp(AItrans.rotation, toRotation, speed * Time.deltaTime);

       AItrans.LookAt(pTransform);
        //animator.gameObject.transform.parent.GetComponent<AIcontol>().navMeshAgent.SetDestination(pTransform);



    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.parent.GetComponent<AIcontol>().alert.SetActive(false);
        animator.gameObject.transform.parent.GetComponent<AIcontol>().enemyWeapon.fireControl(false);
        //animator.gameObject.transform.parent.GetComponent<AIcontol>().patroling = true;
        animator.gameObject.transform.parent.GetComponent<AIcontol>().navMeshAgent.isStopped = false;


        //


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
