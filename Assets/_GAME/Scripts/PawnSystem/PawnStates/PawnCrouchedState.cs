using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnCrouchedState : PawnBaseState
{
    public PawnCrouchedState(PawnController pawn) : base(pawn) {
        
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Controller.PawnState = PawnState.Crouched;
        Controller.SetPawnSpeed(1.5f);
    }
}
