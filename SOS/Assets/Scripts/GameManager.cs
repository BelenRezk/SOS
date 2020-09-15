using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    void Awake()
    {
        instance = this;

        Application.targetFrameRate = 60; //Para que anden bien en el unity remote temporalmente
    }
    #endregion

}
