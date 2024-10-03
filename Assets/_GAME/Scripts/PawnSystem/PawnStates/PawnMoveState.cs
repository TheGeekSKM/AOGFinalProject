using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMoveState : PawnBaseState
{
    public PawnMoveState(PawnController controller) : base(controller)
    {
        
    }

    public override void OnEnter()
    {
        Controller.PawnState = PawnState.Move;
    }
}
