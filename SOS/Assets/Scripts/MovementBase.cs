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
    public Inventory inventory;
    public Inventory winItems;
    public int playerId;
    public float interactionCooldown = 0.5f;
    public float interactionCooldownRemaining = 0;

    public override bool Equals(object obj)
    {
        return playerId == ((MovementBase)obj).playerId;
    }

    public override int GetHashCode()
    {
        return playerId;
    }
}