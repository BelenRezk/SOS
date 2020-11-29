using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector_Script : MonoBehaviour
{
    public Image Businesswoman;
    public Image OldLady;
    public Image Pilot;
    public Image Hippie;
    private Vector3 CharacterPosition;
    private Vector3 OffScreen;
    [HideInInspector]
    public static int CharacterInt = 1;

    private void Awake()
    {
        CharacterPosition = Businesswoman.transform.position;
        OffScreen = Pilot.transform.position;
    }

    void Update()
    {
        if(Input.GetKey("left"))
        {
            PreviousCharacter();
        }
        if(Input.GetKey("right"))
        {
            NextCharacter();
        }
    }

    public void NextCharacter()
    {
        switch(CharacterInt)
        {
            case 1:
                Businesswoman.enabled = false;
                Businesswoman.transform.position = OffScreen;
                Pilot.transform.position = CharacterPosition;
                Pilot.enabled = true;
                CharacterInt++;
                break;
            case 2:
                Pilot.enabled = false;
                Pilot.transform.position = OffScreen;
                OldLady.transform.position = CharacterPosition;
                OldLady.enabled = true;
                CharacterInt++;
                break;
            case 3:
                OldLady.enabled = false;
                OldLady.transform.position = OffScreen;
                Hippie.transform.position = CharacterPosition;
                Hippie.enabled = true;
                CharacterInt++;
                break;
            case 4:
                Hippie.enabled = false;
                Hippie.transform.position = OffScreen;
                Businesswoman.transform.position = CharacterPosition;
                Businesswoman.enabled = true;
                CharacterInt++;
                ResetInt();
                break;
            default:
                ResetInt();
                break;
        }
    }

    public void PreviousCharacter()
    {
        switch(CharacterInt)
        {
            case 1:
                Businesswoman.enabled = false;
                Businesswoman.transform.position = OffScreen;
                Hippie.transform.position = CharacterPosition;
                Hippie.enabled = true;
                ResetInt();
                break;
            case 2:
                Pilot.enabled = false;
                Pilot.transform.position = OffScreen;
                Businesswoman.transform.position = CharacterPosition;
                Businesswoman.enabled = true;
                CharacterInt--;
                break;
            case 3:
                OldLady.enabled = false;
                OldLady.transform.position = OffScreen;
                Pilot.transform.position = CharacterPosition;
                Pilot.enabled = true;
                CharacterInt--;
                break;
            case 4:
                Hippie.enabled = false;
                Hippie.transform.position = OffScreen;
                OldLady.transform.position = CharacterPosition;
                OldLady.enabled = true;
                CharacterInt--;
                break;
            default:
                ResetInt();
                break;
        }
    }

    private void ResetInt()
    {
        if(CharacterInt >= 4)
        {
            CharacterInt = 1;
        }
        else
        {
            CharacterInt = 4;
        }
    }
}
