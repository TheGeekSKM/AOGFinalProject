using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnIdleState : PawnBaseState
{
    public PawnIdleState(PawnController pawn) : base(pawn) { 
        
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Controller.PawnState = PawnState.Idle;
        Controller.ResetPawnSpeed();
    }
}
