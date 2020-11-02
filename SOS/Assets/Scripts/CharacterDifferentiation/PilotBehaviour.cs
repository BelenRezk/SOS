using UnityEngine;

public class PilotBehaviour : CharacterDifferentiationBase
{
    public PilotBehaviour(MovementBase movementBase, bool playMusic, AudioManager manager)
    {
        movement = movementBase;
        shouldPlayMusic = playMusic;
        audioManager = manager;
        movement.abilityCooldown = 15f;
        movement.abilityDuration = 3f;
    }

    public override void UseSpecialAbility()
    {
        Debug.Log("Pilot Ability");
        ToggleWaypoint("WaypointOar", true);
        ToggleWaypoint("WaypointFlareGun", true);
        if (shouldPlayMusic)
        {
            audioManager.Stop("MainMusic");
            audioManager.Stop("BananaMusic");
            audioManager.Play("RadarBlip");
            audioManager.Play("TargetAcquired");
        }
    }

    public override void FinishSpecialAbility()
    {
        ToggleWaypoint("WaypointOar", false);
        ToggleWaypoint("WaypointFlareGun", false);
        if (shouldPlayMusic)
        {
            audioManager.Stop("RadarBlip");
            if (movement.isUsingBanana)
                audioManager.Play("BananaMusic");
            else
                audioManager.Play("MainMusic");
        }
        Debug.Log("Finish Pilot Ability");
    }

    private void ToggleWaypoint(string gameObjectName, bool value)
    {
        GameObject waypoint = GameObject.Find(gameObjectName);
        if (waypoint != null)
        {
            Waypoint waypointScript = waypoint.GetComponent<Waypoint>();
            if (waypointScript != null)
                waypointScript.isAbilityActive = value;
        }
    }
}