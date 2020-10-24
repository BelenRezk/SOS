﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Inventory inventory;
    Vector3 velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    private float remainingBananaTime = 0f;
    private bool isUsingBanana = false;
    public float bananaSpeedMultiplier = 2.0f;
    private bool hasShield = false;
    public float afterHitInvincibility = 1f;
    private float currentInvincibility = 0f;

    void Start()
    {
        FindObjectOfType<AudioManager>().Stop("Jungle");
        FindObjectOfType<AudioManager>().Play("MainMusic");
        FindObjectOfType<AudioManager>().Play("Waves");
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
            Debug.Log(inventory.gameObject.name);
            inventory.AddItem(item);
            item.HasOwner = true;
        }
        else if (item != null && item.HasOwner && currentInvincibility <= 0)
        {
            if (!hasShield)
                inventory.DropAllItems();
            else
                hasShield = false;
            currentInvincibility = afterHitInvincibility;
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
