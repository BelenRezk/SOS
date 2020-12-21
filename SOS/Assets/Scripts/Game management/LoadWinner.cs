using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadWinner : MonoBehaviour
{
    public static string winner;
    public GameObject businessWomanWin;
    public GameObject businessWomanLose;
    public GameObject pilotWin;
    public GameObject pilotLose;
    public GameObject oldLadyWin;
    public GameObject oldLadyLose;
    public GameObject hippieWin;
    public GameObject hippieLose;

    void Start()
    {
        switch(winner)
        {
            case "Businesswoman":
                businessWomanWin.SetActive(true);
                pilotLose.SetActive(true);
                oldLadyLose.SetActive(true);
                hippieLose.SetActive(true);
                break;
            case "Pilot":
                businessWomanLose.SetActive(true);
                pilotWin.SetActive(true);
                oldLadyLose.SetActive(true);
                hippieLose.SetActive(true);
                break;
            case "Old Lady":
                businessWomanLose.SetActive(true);
                pilotLose.SetActive(true);
                oldLadyWin.SetActive(true);
                hippieLose.SetActive(true);
                break;
            case "Hippie":
                businessWomanLose.SetActive(true);
                pilotLose.SetActive(true);
                oldLadyLose.SetActive(true);
                hippieWin.SetActive(true);
                break;
        }
    }
}
