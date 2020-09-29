using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadWinner : MonoBehaviour
{
    public static string winner;
    void Start()
    {
        GetComponent<Text>().text = winner;
    }
}
