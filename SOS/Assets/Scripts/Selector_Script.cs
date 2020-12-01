using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector_Script : MonoBehaviour
{
    public GameObject Businesswoman;
    public GameObject OldLady;
    public GameObject Pilot;
    public GameObject Hippie;
    [HideInInspector]
    public static int CharacterInt = 1;

    void Start()
    {
        Businesswoman.SetActive(true);
        OldLady.SetActive(false);
        Pilot.SetActive(false);
        Hippie.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown("left"))
        {
            PreviousCharacter();
        }
        if(Input.GetKeyDown("right"))
        {
            NextCharacter();
        }
    }

    public void NextCharacter()
    {
        switch(CharacterInt)
        {
            case 1:
                Businesswoman.SetActive(false);
                Pilot.SetActive(true);
                CharacterInt++;
                break;
            case 2:
                Pilot.SetActive(false);
                OldLady.SetActive(true);
                CharacterInt++;
                break;
            case 3:
                OldLady.SetActive(false);
                Hippie.SetActive(true);
                CharacterInt++;
                break;
            case 4:
                Hippie.SetActive(false);
                Businesswoman.SetActive(true);
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
                Businesswoman.SetActive(false);
                Hippie.SetActive(true);
                ResetInt();
                break;
            case 2:
                Pilot.SetActive(false);
                Businesswoman.SetActive(true);
                CharacterInt--;
                break;
            case 3:
                OldLady.SetActive(false);
                Pilot.SetActive(true);
                CharacterInt--;
                break;
            case 4:
                Hippie.SetActive(false);
                OldLady.SetActive(true);
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
