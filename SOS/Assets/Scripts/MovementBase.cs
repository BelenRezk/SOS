using UnityEngine;

public abstract class MovementBase : MonoBehaviour
{
    public float speed = 6f;
    public float abilityCooldown = 40f;
    public float abilityCooldownRemaining = 0f;

    public float abilityDuration = 10f;
    public float abilityDurationRemaining = 0f;
    public bool abilityActive = false;

    public float afterHitInvincibility = 1f;
    public float currentInvincibility = 0f;
    public bool isUsingBanana = false;
}