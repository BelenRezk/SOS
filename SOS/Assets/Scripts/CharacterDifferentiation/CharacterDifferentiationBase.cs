using UnityEngine;

public abstract class CharacterDifferentiationBase
{
    public MovementBase movement;
    public bool shouldPlayMusic;
    public AudioManager audioManager;

    public abstract void UseSpecialAbility();

    public abstract void FinishSpecialAbility();
}