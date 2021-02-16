using System.Collections;
using UnityEngine;

//Los estados en que puede estar el agente
public enum AgentState {Idle, OnMovement, Attacking, Dead}

public class AgenteEstatico : MonoBehaviour
{
    //Una variable para saber el estado del agente
    public AgentState agentStatus;

    //Una variable para saber que estado se esta ejecutando
    public FunctionsFSM curState;

    [Header("Elementos del sensor")]
    public LayerMask targetMask;
    public float radiusDetection = 2f;
    public Transform sensorPosition;
    public bool targetDetected = false;

    [Header("Elementos del agente")]
    public float speedMovement = 5f;
    public float timeIdle = 10f;

    public Vector3[] angles;
    public int angleIndex;


    public readonly IdleStaticState idleState = new IdleStaticState();
    public readonly RotateStaticAgent rotateState = new RotateStaticAgent();
    public readonly AttackingState attackState = new AttackingState();

    // Start is called before the first frame update
    void Start()
    {
        TransitionToState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        curState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        TargetDetected();
    }

    public virtual void TargetDetected()
    {
        Collider[] colliders = Physics.OverlapSphere(sensorPosition.position, radiusDetection, targetMask);
        int i = 0;
        while(i < colliders.Length)
        {
            targetDetected = true;
            i++;
        }
        if(colliders.Length == 0)
        {
            targetDetected = false;
        }
    }

    public void TransitionToState(FunctionsFSM state)
    {
        curState = state;
        curState.EnterState(this);
    }

    public void Coroutine(IEnumerator thisCoroutine)
    {
        StartCoroutine(thisCoroutine);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sensorPosition.position, radiusDetection);
    }
}
