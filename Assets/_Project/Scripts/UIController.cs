using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
  [Header("Start Game Settings")] 
  [SerializeField] private GameObject startGamePanel;
  [SerializeField] private GameObject errorTextGameObject;

  [Header("BotVsPlayer")] 
  [SerializeField] private GameObject botVsPlayerPanel;
  [SerializeField] private GameObject winPanelBvP;
  [SerializeField] private GameObject gameOverPanelBvP;
  [SerializeField] private TextMeshProUGUI botNumberText;
  [SerializeField] private TextMeshProUGUI playerNumberText;
  [SerializeField] private GameObject gamePlayButton;
  
  private void Awake()
  {
    GameManager.Instance.startGame.AddListener(SetStartGameModeOnUI);
    GameManager.Instance.startGame.AddListener(SetPlayerNumber);
    GameManager.Instance.printError.AddListener(DisplayError);
    GameManager.Instance.displayBotValue.AddListener(DisplayBotNumberOnText);
    GameManager.Instance.botWin.AddListener(SetGameOverPanel);
    GameManager.Instance.playerWin.AddListener(SetWingGamePanel);
    GameManager.Instance.returnToMenu.AddListener(PlayAgainBvP);
    //GameManager.Instance.onlyDisplayBotValue.AddListener(Dis);
  }

  private void SetStartGameModeOnUI()
  {
    startGamePanel.SetActive(false);
    botVsPlayerPanel.SetActive(true);
    GameManager.Instance.GenerateNumber();
  }

  private void SetPlayerNumber()
  {
    playerNumberText.text = "Your Number: " + GameManager.Instance.playerNumber;
  }

  private void DisplayError()
  {
    StartCoroutine(WaitToDisplayError());
  }

  private void DisplayBotNumberOnText()
  {
    botNumberText.text = "Bot Value: " + GameManager.Instance.botNumber;
  }

  private void EnableWinPanel()
  {
    
  }
  
  

  public void PlayAgainBvP()
  {
    startGamePanel.SetActive(true);
    botVsPlayerPanel.SetActive(false);
    gamePlayButton.SetActive(true);
    winPanelBvP.SetActive(false);
    gameOverPanelBvP.SetActive(false);
  }

  private void SetWingGamePanel()
  {
    gamePlayButton.SetActive(false);
    winPanelBvP.SetActive(true);
  }

  private void SetGameOverPanel()
  {
    gamePlayButton.SetActive(false);
    gameOverPanelBvP.SetActive(true);
  }
  
  private IEnumerator WaitToDisplayError()
  {
    errorTextGameObject.SetActive(true);
    yield return new WaitForSeconds(3);
    errorTextGameObject.SetActive(false);
  }

}
