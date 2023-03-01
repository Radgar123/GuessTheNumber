using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    #region Variable

    public int minValueToGenerate = 0;
    public int maxValueToGenerate = 1;
    
    public int botNumber;
    public int playerNumber;
    
    public int minValue;
    public int maxValue;
    
    public int actualValue;
    private bool isLess;
    
    #endregion


    private void Start()
    {
        startGame.AddListener(SetStartValue);
    }

    #region GenerateNumber

    public void GenerateNumber()
    {
        botNumber = Random.Range(minValueToGenerate, maxValueToGenerate);
    }

    private void GenerateNumber(bool isLess)
    {
        if (isLess)
        {
            maxValue = botNumber;
            botNumber = Random.Range(minValue, actualValue);
        }
        else
        {
            minValue = botNumber;
            botNumber = Random.Range(actualValue, maxValue);
        }
        
        DisplayBotNumber();
    }

    #endregion
    
    private void SetActualValue()
    {
        actualValue = botNumber;
    }

    private void SetMinValue() => minValue = actualValue;
    private void SetMaxValue() => maxValue = actualValue;

    private void SetStartValue()
    {
        minValue = minValueToGenerate;
        maxValue = maxValueToGenerate;
    }

    #region BaseGameValueSetters

    public void SetMinRange(string value)
    {
        if (int.TryParse(value, out int result))
        {
            minValueToGenerate = result;
        }
    }

    public void SetMaxRange(string value)
    {
        if (int.TryParse(value, out int result))
        {
            maxValueToGenerate = result;
        }
    }

    public void SetValueToCheck(string value)
    {
        if (int.TryParse(value, out int result))
        {
            playerNumber = result;
        }
    }

    #endregion

    public void CheckCorrectValueToSubmit()
    {
        if (playerNumber > maxValueToGenerate 
            || playerNumber < minValueToGenerate || minValueToGenerate > maxValueToGenerate || 
            maxValueToGenerate < minValueToGenerate)
        {
            Debug.Log("Nie działa");
            PrintError();
        }
        else
        {
            Debug.Log("Działa");
            StartGame();
        }
    }

    private void ParsePlayerWithBotValue()
    {
        if (botNumber == playerNumber)
        {
            playerWin.Invoke();
        }
        else if (minValue == maxValue)
        {
            botWin.Invoke();
        }
    }


    #region Events

    [HideInInspector] public UnityEvent startGame;
    [HideInInspector] public UnityEvent printError;
    [HideInInspector] public UnityEvent displayBotValue;
    [HideInInspector] public UnityEvent botWin;
    [HideInInspector] public UnityEvent playerWin;
    [HideInInspector] public UnityEvent returnToMenu;

    #endregion

    #region InvokeEvents

    public void StartGame()
    {
        startGame.Invoke();
        DisplayBotNumber();
    }

    public void PrintError()
    {
        printError.Invoke();
    }

    public void DisplayBotNumber()
    {
        displayBotValue.Invoke();
    }

    public void ReturnToMenu()
    {
        returnToMenu.Invoke();
    }
    
    #endregion

    #region ButtonFunction

    public void LessButton()
    {
        SetActualValue();
        GenerateNumber(true);
        ParsePlayerWithBotValue();
    }

    public void MoreButton()
    {
        SetActualValue();
        GenerateNumber(false);
        ParsePlayerWithBotValue();
    }

    #endregion


}
