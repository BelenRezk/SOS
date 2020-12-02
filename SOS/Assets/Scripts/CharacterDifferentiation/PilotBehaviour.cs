using UnityEngine;
using System;

public class PilotBehaviour : CharacterDifferentiationBase
{
    public AIView aiPlayerView;
    public PilotBehaviour(MovementBase movementBase, bool playMusic, AudioManager manager, AIView ai)
    {
        movement = movementBase;
        shouldPlayMusic = playMusic;
        audioManager = manager;
        movement.abilityCooldown = 15f;
        movement.abilityDuration = 3f;
        aiPlayerView = ai;
    }

    public override void UseSpecialAbility()
    {
        Debug.Log("Pilot Ability");
        if (shouldPlayMusic)
        {
            //Human player
            ToggleWaypoint("WaypointOar", true);
            ToggleWaypoint("WaypointFlareGun", true);
            ToggleWaypoint("WaypointOar (1)", true);
            ToggleWaypoint("WaypointEmergencyKit", true);
            ToggleWaypoint("WaypointParachute", true);
            try{
                audioManager.Stop("MainMusic");
                audioManager.Stop("BananaMusic");
                audioManager.Play("RadarBlip");
                audioManager.Play("TargetAcquired");
            }
            catch(Exception){}
        }
        else
        {
            //AI Player
            GameObject [] collectibles = GameObject.FindGameObjectsWithTag("Item");
            bool foundObject = false;
            for(int x = 0; (x < collectibles.Length && !foundObject) ; x ++)
            {
                IInventoryItem item = collectibles[x].GetComponent<IInventoryItem>(); 
                if(!item.HasOwner && item.WinItem){
                    foundObject = true;
                    aiPlayerView.ItemsPositions.Add((collectibles[x].name, collectibles[x].transform.position));
                }
            }
        }
    }

    public override void FinishSpecialAbility()
    {
        if (shouldPlayMusic)
        {
            ToggleWaypoint("WaypointOar", false);
            ToggleWaypoint("WaypointFlareGun", false);
            ToggleWaypoint("WaypointOar (1)", false);
            ToggleWaypoint("WaypointEmergencyKit", false);
            ToggleWaypoint("WaypointParachute", false);
            try
            {
                audioManager.Stop("RadarBlip");
            }
            catch(Exception){}
            if (movement.isUsingBanana)
            {
                try{
                audioManager.Play("BananaMusic");
                }
                catch(Exception){}
            }
            else
            {
                try
                {
                    audioManager.Play("MainMusic");
                }
                catch(Exception){}
            }
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