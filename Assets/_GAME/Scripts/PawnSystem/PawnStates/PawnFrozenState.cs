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
        Controller.SetPawnSpeed(0f);
    }
}
