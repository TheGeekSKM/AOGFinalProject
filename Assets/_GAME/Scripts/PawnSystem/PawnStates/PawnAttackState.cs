using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnAttackState : PawnBaseState
{
    PawnAttackController attackController;
    public PawnAttackState(PawnController controller, PawnAttackController attackController) : base(controller)
    {
        this.attackController = attackController;
    }
}
