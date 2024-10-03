using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnAttackState : PawnBaseState
{
    public PawnAttackState(PawnController controller, PawnAttackController attackController) : base(controller)
    {
        
    }

    public override void OnEnter()
    {
        Controller.PawnState = PawnState.Attack;
        Controller.SetPawnSpeed(0f);
    }
}
