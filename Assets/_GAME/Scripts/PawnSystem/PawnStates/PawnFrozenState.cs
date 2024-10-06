using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnFrozenState : PawnBaseState
{
    public PawnFrozenState(PawnController pawn) : base(pawn) {
        
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Controller.PawnState = PawnState.Frozen;
    }

    public override void OnExit()
    {
        base.OnExit();
        Controller.IsResting = false;
    }
}
