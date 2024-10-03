using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnAmbushState : PawnBaseState
{
    public PawnAmbushState(PawnController pawn) : base(pawn) 
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        Controller.PawnState = PawnState.Ambush;
    }
}
