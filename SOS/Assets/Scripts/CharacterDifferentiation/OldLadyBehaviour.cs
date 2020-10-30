using UnityEngine;

public class OldLadyBehaviour : CharacterDifferentiationBase
{
    public OldLadyBehaviour(MovementBase movementBase)
    {
        movement = movementBase;
    }

    public override void UseSpecialAbility()
    {
        Debug.Log("Old Lady Ability");
    }
}