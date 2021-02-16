using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStaticState : FunctionsFSM
{
    public override void EnterState(AgenteEstatico agent)
    {
        Debug.Log("Entro a estado Idle");
        agent.agentStatus = AgentState.Idle;
        agent.Coroutine(Wait(agent));
    }

    IEnumerator Wait(AgenteEstatico agent)
    {
        yield return new WaitForSeconds(agent.speedMovement);
        agent.TransitionToState(agent.rotateState); //Cambia al estado de rotación
    }

    public override void UpdateState(AgenteEstatico agent)
    {
        //necesario preguntar si se ha detectado al jugador
            //agent.StopAllCoroutines();//Esto detiene la corutina de Wait que esta en este código
            //Si es verdad, cambiar al estado de atacar
    }
}
