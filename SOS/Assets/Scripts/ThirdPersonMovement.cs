using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MovementBase
{
    public CharacterController controller;
    public Transform cam;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    Vector3 velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    private float remainingBananaTime = 0f;

    public float bananaSpeedMultiplier = 2.0f;
    private bool hasShield = false;
    public AudioClip getHitSound;
    public AudioClip shieldSound;
    private CharacterDifferentiationBase characterBehaviour;

    void Start()
    {
        AudioManager manager = FindObjectOfType<AudioManager>();
        FindObjectOfType<AudioManager>().Stop("Jungle");
        FindObjectOfType<AudioManager>().Play("MainMusic");
        FindObjectOfType<AudioManager>().Play("Waves");
        switch (Selector_Script.CharacterInt)
        {
            case 1:
                characterBehaviour = new BusinessWomanBehaviour(this, true, manager);
                break;
            case 2:
                characterBehaviour = new PilotBehaviour(this, true, manager);
                break;
            case 3:
                characterBehaviour = new OldLadyBehaviour(this, true, manager);
                break;
            case 4:
                characterBehaviour = new HippieBehaviour(this, true, manager);
                break;
            default:
                characterBehaviour = new PilotBehaviour(this, true, manager);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        //gravity
        velocity.y += gravity * Time.deltaTime * 2;
        controller.Move(velocity * Time.deltaTime);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        for (int i = 1; i <= 9; i++)
        {
            string action = "Position" + i;
            if (Input.GetButtonDown(action))
                inventory.ChangeSelectedPosition(i - 1);
        }
        if (Input.GetButtonDown("DropItem"))
            inventory.RemoveSelectedItem();
        if (Input.GetButtonDown("UseItem"))
            inventory.UseSelectedItem();
        CheckBananaUsage();
        if (currentInvincibility > 0)
            currentInvincibility -= Time.deltaTime;
        if (Input.GetButtonDown("UseSpecialAbility") && abilityCooldownRemaining <= 0 && !abilityActive)
        {
            characterBehaviour.UseSpecialAbility();
            abilityDurationRemaining = abilityDuration;
            abilityActive = true;
        }
        if (abilityCooldownRemaining > 0)
            abilityCooldownRemaining -= Time.deltaTime;
        if (abilityActive && abilityDurationRemaining <= 0)
        {
            characterBehaviour.FinishSpecialAbility();
            abilityActive = false;
            abilityCooldownRemaining = abilityCooldown;
        }
        if (abilityDurationRemaining > 0)
            abilityDurationRemaining -= Time.deltaTime;
    }

    private void CheckBananaUsage()
    {
        if (isUsingBanana)
        {
            if (remainingBananaTime > 0f)
                remainingBananaTime -= Time.deltaTime;
            else
            {
                isUsingBanana = false;
                speed = speed / bananaSpeedMultiplier;
                FindObjectOfType<AudioManager>().Stop("BananaMusic");
                FindObjectOfType<AudioManager>().Play("MainMusic");
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
        AddToInventory(item);
    }

    private void AddToInventory(IInventoryItem item)
    {
        if (item != null && !item.HasOwner)
        {
            if (item.WinItem)
            {
                winItems.AddItem(item);
            }
            else
            {
                inventory.AddItem(item);
            }
            item.HasOwner = true;
        }
        else if (item != null && item.HasOwner && currentInvincibility <= 0)
        {
            if (!hasShield)
            {
                PlayGetHitSound();
                inventory.DropAllItems();
                winItems.DropAllItems();
                item.DestroyObject();
            }
            else
            {
                PlayShieldSound();
                hasShield = false;
            }
            currentInvincibility = afterHitInvincibility;
        }
    }

    private void PlayGetHitSound()
    {
        try
        {
            AudioSource.PlayClipAtPoint(getHitSound, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
        }
        catch (Exception)
        {
            Debug.Log("No audio clip");
        }
    }

    private void PlayShieldSound()
    {
        try
        {
            AudioSource.PlayClipAtPoint(shieldSound, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
        }
        catch (Exception)
        {
            Debug.Log("No shield audio clip");
        }
    }

    public void UseBanana(float duration)
    {
        isUsingBanana = true;
        speed = speed * bananaSpeedMultiplier;
        remainingBananaTime = duration;
    }

    public bool UseShield()
    {
        if (!hasShield)
        {
            hasShield = true;
            return true;
        }
        else
            return false;
    }
}
