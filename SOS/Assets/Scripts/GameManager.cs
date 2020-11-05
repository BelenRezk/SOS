using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int ItemsId;
    #region Singleton
    public static GameManager instance;
    void Awake()
    {
        instance = this;
        ItemsId = 0;
        Application.targetFrameRate = 60; //Para que anden bien en el unity remote temporalmente
    }
    #endregion

}
