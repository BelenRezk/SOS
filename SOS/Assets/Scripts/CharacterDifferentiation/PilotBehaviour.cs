using UnityEngine;

public class PilotBehaviour : CharacterDifferentiationBase
{
    public PilotBehaviour(MovementBase movementBase)
    {
        movement = movementBase;
    }

    public override void UseSpecialAbility()
    {
        Debug.Log("Pilot Ability");

    }
}