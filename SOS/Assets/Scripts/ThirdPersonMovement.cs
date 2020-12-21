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
    public Transform GameCamera;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    public float remainingBananaTime = 0f;
    public float bananaSpeedMultiplier = 2.0f;
    public bool hasShield = false;
    public AudioClip getHitSound;
    public AudioClip shieldSound;
    private CharacterDifferentiationBase characterBehaviour;
    public Animator animator;
    private int timer;
    public bool gameJustResumed;
    public Transform messagePanel;

    void Start()
    {
        gameJustResumed = false;
        timer = 0;
        AudioManager manager = FindObjectOfType<AudioManager>();
        try{    
            manager.Stop("Jungle");
            manager.PlayMainMusic();
            manager.Play("Waves");
        }
        catch(Exception){
            //there's no music to play or stop
        }
        switch (Selector_Script.CharacterInt)
        {
            case 1:
                characterBehaviour = new BusinessWomanBehaviour(this, true, manager);
                abilityCooldown = 35f;
                GameObject businessWoman = this.transform.Find("Empresaria").gameObject;
                businessWoman.SetActive(true);
                this.animator = businessWoman.GetComponent<Animator>();
                this.gameObject.name = "Businesswoman";
                break;
            case 2:
                characterBehaviour = new PilotBehaviour(this, true, manager, null);
                abilityCooldown =  15f;
                GameObject pilot = this.transform.Find("Piloto").gameObject;
                pilot.SetActive(true);
                this.animator = pilot.GetComponent<Animator>();
                this.gameObject.name = "Pilot";
                break;
            case 3:
                characterBehaviour = new OldLadyBehaviour(this, true, manager);
                abilityCooldown = 35f;
                GameObject oldLady = this.transform.Find("anciana").gameObject;
                oldLady.SetActive(true);
                this.animator = oldLady.GetComponent<Animator>();
                this.gameObject.name = "Old Lady";
                break;
            case 4:
                characterBehaviour = new HippieBehaviour(this, true, manager);
                abilityCooldown = 40f;
                GameObject hippie = this.transform.Find("hippie").gameObject;
                hippie.SetActive(true);
                this.animator = hippie.GetComponent<Animator>();
                this.gameObject.name = "Hippie";
                break;
            default:
                characterBehaviour = new PilotBehaviour(this, true, manager, null);
                abilityCooldown =  15f;
                pilot = this.transform.Find("Piloto").gameObject;
                pilot.SetActive(true);
                this.animator = pilot.GetComponent<Animator>();
                this.gameObject.name = "Pilot";
                break;
        }
        abilityCooldownRemaining = 3f;
    }
    // Update is called once per frame
    void Update()
    {
        timer++;
        if (animator.GetBool("Jumping"))
            animator.SetBool("Jumping", false);
        if (animator.GetBool("ThrowingCoconut"))
            animator.SetBool("ThrowingCoconut", false);
        if (animator.GetBool("IsWalking") && timer == 5)
            animator.SetBool("IsWalking", false);
        var CharacterRotation = GameCamera.transform.rotation;
        CharacterRotation.x = 0;
        CharacterRotation.z = 0;
        transform.rotation = CharacterRotation;
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            timer = 0;
            animator.SetBool("Jumping", true);
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
            
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            animator.SetBool("IsWalking", true);
            timer = 0;
        }
        if (Input.GetButtonDown("UseCoconut") && coconutInventory.currentNumberOfItems > 0)
        {
            if(!gameJustResumed)
            {
                animator.SetBool("ThrowingCoconut", true);
                timer = 0;
            }
            else
            {
                gameJustResumed = false;
            }
        }
        if (Input.GetButtonDown("UsePowerUp") && powerUpInventory.currentNumberOfItems > 0)
            powerUpInventory.UseItem();
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
        if (interactionCooldownRemaining > 0)
            interactionCooldownRemaining -= Time.deltaTime;
    }
    public void ThrowCoconut()
    {
        if (coconutInventory.currentNumberOfItems > 0)
            coconutInventory.UseItem();
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
                FindObjectOfType<AudioManager>().PlayMainMusic();
            }
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
        if (currentInvincibility <= 0 || abilityActive)
        {
            AddToInventory(item);
        }   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("LavaPlane"))
        {
            if(other.gameObject.name.Contains("9")|| other.gameObject.name.Contains("11") || other.gameObject.name.Contains("12") || other.gameObject.name.Contains("16")
            || other.gameObject.name.Contains("19") || other.gameObject.name.Contains("21") || other.gameObject.name.Contains("22") )
            {
                controller.Move(this.transform.right*30.0f);
            }
            else
            {
                controller.Move(this.transform.forward*-30.0f);
            }
            coconutInventory.DropAllItems();
            powerUpInventory.DropAllItems();
            winItems.DropAllItems();
        }
        else
        {
            IInventoryItem item = other.GetComponent<Collider>().GetComponent<IInventoryItem>();  
            if (currentInvincibility <= 0 || abilityActive)
            {
                AddToInventory(item);
            }
        }   
    }
    private void AddToInventory(IInventoryItem item)
    {
        if(item != null && interactionCooldownRemaining <= 0)
        {
            interactionCooldownRemaining = interactionCooldown;
            if(!item.HasOwner)
            {
                bool wasItemAdded = false;
                if (item.WinItem)
                {
                    if(winItems.currentNumberOfItems < winItems.SLOTS)
                    {
                        winItems.AddItem(item);
                        wasItemAdded = true;
                    }
                }
                else if(item.Name == "Coconut")
                {
                    if(coconutInventory.currentNumberOfItems < coconutInventory.SLOTS)
                    {
                        coconutInventory.AddItem(item);
                        wasItemAdded = true;
                    }
                }
                else
                    if(powerUpInventory.currentNumberOfItems < powerUpInventory.SLOTS)
                    {
                        powerUpInventory.AddItem(item);
                        wasItemAdded = true;
                    }
                if(wasItemAdded)
                    item.HasOwner = true;
            }
            else
            {
                if(currentInvincibility <= 0)
                {
                    timer = 0;
                    if (!hasShield)
                    {
                        PlayGetHitSound();
                        coconutInventory.DropAllItems();
                        powerUpInventory.DropAllItems();
                        winItems.DropAllItems();
                    }
                    else
                    {
                        PlayShieldSound();
                        hasShield = false;
                        Transform player = this.transform;
                        GameObject shieldBubble = player.Find("ShieldBubble").gameObject;
                        shieldBubble.SetActive(false);
                        FindObjectOfType<PositionRandomizer>().SpawnShield();
                    }
                    currentInvincibility = afterHitInvincibility;
                }
            item.HasOwner = false;
            item.DestroyObject();
            FindObjectOfType<PositionRandomizer>().SpawnCoconut();
            }
        }
    }
    private void PlayGetHitSound()
    {
        try
        {
            animator.SetBool("WasHit", true);
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
        FindObjectOfType<PositionRandomizer>().SpawnBanana();
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

    public IEnumerator EnableMessagePanel()
    {
        Debug.Log("Called Enable message panel");
        messagePanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        messagePanel.gameObject.SetActive(false);
    }

    public void RemoveShield()
    {
        hasShield = false;
        Transform player = this.transform;
        GameObject shieldBubble = player.Find("ShieldBubble").gameObject;
        shieldBubble.SetActive(false);
        FindObjectOfType<PositionRandomizer>().SpawnShield();
    }
}
