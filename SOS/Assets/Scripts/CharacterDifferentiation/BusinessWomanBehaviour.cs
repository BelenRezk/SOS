using UnityEngine;

public class BusinessWomanBehaviour : CharacterDifferentiationBase
{
    public BusinessWomanBehaviour(MovementBase movementBase)
    {
        movement = movementBase;
    }

    public override void UseSpecialAbility()
    {
        Debug.Log("Business Woman Ability");
    }
}