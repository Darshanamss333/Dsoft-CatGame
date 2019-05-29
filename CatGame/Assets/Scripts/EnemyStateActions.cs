using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateActions : StateMachineBehaviour 
{
    Enemybehaviour _EnemyBehaviourScript;
    Rigidbody2D _Rb;
    Animator _Animator;

    [SerializeField]
    Vector2 _StateTime = new Vector2(1, 3);
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        _EnemyBehaviourScript = animator.gameObject.GetComponent<Enemybehaviour>();
        _Rb = animator.gameObject.GetComponent<Rigidbody2D>();
        _Animator = animator;


        //GetTowardPos
        if(_IsMoveTowerdToPlayer)
        {
            _MoveTowersPos = new Vector3(GameManager.Instance._Player.transform.position.x, GameManager.Instance._Player.transform.position.y, 0) + _MoveTowardOffset;
        }
        else
        {
            _MoveTowersPos = animator.gameObject.transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
        }

        //GetNext State
        int nextState = 0;
        if(_RandomNextState)
        {
            nextState = Random.Range(1, _MaxRandomState + 1);
        }
        else
        {
            nextState = 1;
        }
        _Animator.SetInteger("NextState", 0);
        _EnemyBehaviourScript._WaitStateTime(UnityEngine.Random.Range(_StateTime.x,_StateTime.y),nextState);

        _EnemyBehaviourScript.BehaviourStart(_IsDieCheck,_IsTurnToPlayer);
	}

    [SerializeField]
    string START;
    [SerializeField]
    bool _IsDieCheck;
    [SerializeField]
    bool _IsTurnToPlayer;

    [SerializeField]
    string UPDATE;
    [SerializeField]
    bool _IsMoveTowerd;
    [SerializeField]
    bool _IsMoveTowerdToPlayer;
    [SerializeField]
    Vector3 _MoveTowersPos;
    [SerializeField]
    Vector3 _MoveTowardOffset;
    [SerializeField]
    bool _IsVelocity;
    [SerializeField]
    Vector2 _VelocityValue;
    [SerializeField]
    bool _IsTurnFrontNoGround;
    [SerializeField]
    bool _IsTurnFrontWallDetect;
    [SerializeField]
    bool _IsPlayerFrontCheck;
    [SerializeField]
    bool _IsPlayerDistanceCheck;
    [SerializeField]
    float _MaxDistance;
    [SerializeField]
    bool _RandomNextState;
    [SerializeField]
    int _MaxRandomState;

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _EnemyBehaviourScript.BehaviourUpdates(_IsMoveTowerd,_MoveTowersPos,_IsVelocity, _VelocityValue, _IsTurnFrontNoGround,_IsTurnFrontWallDetect,_IsPlayerFrontCheck,
                                               _IsPlayerDistanceCheck,_MaxDistance);
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        _EnemyBehaviourScript._CurrentVelocity = Vector2.zero;
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
