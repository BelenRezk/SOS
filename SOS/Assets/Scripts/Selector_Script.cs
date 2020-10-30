using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector_Script : MonoBehaviour
{
    public Image Blue;
    public Image Green;
    public Image Red;
    public Image Yellow;
    private Vector3 CharacterPosition;
    private Vector3 OffScreen;
    [HideInInspector]
    public static int CharacterInt = 1;

    private void Awake()
    {
        CharacterPosition = Blue.transform.position;
        OffScreen = Red.transform.position;
    }

    public void NextCharacter()
    {
        switch(CharacterInt)
        {
            case 1:
                Blue.enabled = false;
                Blue.transform.position = OffScreen;
                Red.transform.position = CharacterPosition;
                Red.enabled = true;
                CharacterInt++;
                break;
            case 2:
                Red.enabled = false;
                Red.transform.position = OffScreen;
                Green.transform.position = CharacterPosition;
                Green.enabled = true;
                CharacterInt++;
                break;
            case 3:
                Green.enabled = false;
                Green.transform.position = OffScreen;
                Yellow.transform.position = CharacterPosition;
                Yellow.enabled = true;
                CharacterInt++;
                break;
            case 4:
                Yellow.enabled = false;
                Yellow.transform.position = OffScreen;
                Blue.transform.position = CharacterPosition;
                Blue.enabled = true;
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
                Blue.enabled = false;
                Blue.transform.position = OffScreen;
                Yellow.transform.position = CharacterPosition;
                Yellow.enabled = true;
                ResetInt();
                break;
            case 2:
                Red.enabled = false;
                Red.transform.position = OffScreen;
                Blue.transform.position = CharacterPosition;
                Blue.enabled = true;
                CharacterInt--;
                break;
            case 3:
                Green.enabled = false;
                Green.transform.position = OffScreen;
                Red.transform.position = CharacterPosition;
                Red.enabled = true;
                CharacterInt--;
                break;
            case 4:
                Yellow.enabled = false;
                Yellow.transform.position = OffScreen;
                Green.transform.position = CharacterPosition;
                Green.enabled = true;
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
