using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "Shield";
        }
    }

    public bool _hasOwner;
    public override bool HasOwner
    {
        get
        {
            return _hasOwner;
        }
        set
        {
            _hasOwner = value;
        }
    }

    public override bool WinItem
    {
        get
        {
            return false;
        }
    }

    public AudioClip shieldActivationSound;

    public override bool OnUse()
    {
        Transform player = this.transform.parent;
        GameObject playerGO = player.gameObject;
        bool shieldUsed = false;
        try
        {
            ThirdPersonMovement thirdPersonMovement = playerGO.GetComponent<ThirdPersonMovement>();
            shieldUsed = thirdPersonMovement.UseShield();
            PlayShieldActivationSound();
        }
        catch (Exception)
        {

        }
        if (shieldUsed)
            this.transform.parent = null;
        return true;
    }

    private void PlayShieldActivationSound()
    {
        try
        {
            AudioSource.PlayClipAtPoint(shieldActivationSound, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
        }
        catch (Exception)
        {
            Debug.Log("No audio clip");
        }
    }
}