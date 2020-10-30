using UnityEngine;

public class HippieBehaviour : CharacterDifferentiationBase
{
    public HippieBehaviour(MovementBase movementBase)
    {
        movement = movementBase;
    }

    public override void UseSpecialAbility()
    {
        Debug.Log("Hippie Ability");
    }
}