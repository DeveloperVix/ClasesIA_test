using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStaticAgent : FunctionsFSM
{
    public override void EnterState(AgenteEstatico agent)
    {
        Debug.Log("Entro a estado rotar");
        agent.agentStatus = AgentState.OnMovement;
    }

    public override void UpdateState(AgenteEstatico agent)
    {
        if(agent.targetDetected)
        {
            agent.TransitionToState(agent.attackState);
        }
        else
        {
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, Quaternion.Euler(agent.angles[agent.angleIndex]), Time.deltaTime * agent.speedMovement);
            

            Debug.Log(agent.transform.eulerAngles.y);

            if(agent.transform.eulerAngles.y >= (agent.angles[agent.angleIndex].y - 1))
            {
                agent.angleIndex = (agent.angleIndex + 1) % agent.angles.Length;
                agent.TransitionToState(agent.idleState);
            }
        }
    }
}
