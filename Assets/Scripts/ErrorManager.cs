using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class ErrorManager : MonoBehaviour
{
    public GameObject ErrorMessageObject;
    public Text ErrorMessageText;

    void ExceptionHandlerInit()
    {

    }

    public void ReturnError(string message)
    {
        ErrorMessageObject.SetActive(true);
        ErrorMessageText.text = message;
    }

    public void CloseErrorMessage()
    {
        ErrorMessageObject.SetActive(false);
        GameManagerNew.SaveGameData();
        Application.Quit();
    }
}