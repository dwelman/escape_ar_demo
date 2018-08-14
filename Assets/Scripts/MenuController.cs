using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private GameObject warningPanel;
    [SerializeField]
    private GameObject tutorialPanel;

    public enum MenuState
    {
        MainMenuState,
        InGameState,
        QuitState,
        TutorialState
    }

    public List<MenuState> stateStack = new List<MenuState>();

    private int starCount;

    public void Start()
    {
        GoToState(0);
    }
    public void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            GoBack();
            //GameManager.instance.AddStar("star_" + ++starCount);
        }
        if (Input.GetKeyDown("k"))
        {
            ResetGame();
        }
    }

    public void GoToState(int newStateNum)
    {
        Debug.Log(newStateNum);
        MenuState newState = (MenuState)newStateNum;

        if (stateStack.Count >= 1)
            DeactivateStateMenu(stateStack[stateStack.Count - 1]);

        stateStack.Add(newState);
        ActivateStateMenu(newState);
    }

    public void GoBack()
    {
        if (stateStack.Count > 1)
        {
            DeactivateStateMenu(stateStack[stateStack.Count - 1]);
            stateStack.Remove(stateStack[stateStack.Count - 1]);
			ActivateStateMenu(stateStack[stateStack.Count - 1]);
        }
		else
		{
			GoToState(1);
		}
    }

    public void ActivateStateMenu(MenuState newState)
    {
        switch (newState)
        {
            case MenuState.MainMenuState:
                {
                    menuPanel.SetActive(true);
                    Time.timeScale = 0.0001f;
                    break;
                }
            case MenuState.InGameState:
                {
                    Time.timeScale = 1f;
                    break;
                }
            case MenuState.QuitState:
                {
                    warningPanel.SetActive(true);
                    break;
                }
            case MenuState.TutorialState:
                {
                    tutorialPanel.SetActive(true);
                    break;
                }
        }
    }

    public void DeactivateStateMenu(MenuState oldStateNum)
    {
        MenuState oldState = (MenuState)oldStateNum;

        switch (oldState)
        {
            case MenuState.MainMenuState:
                {
                    menuPanel.SetActive(false);
                    break;
                }
            case MenuState.InGameState:
                {
                    break;
                }
            case MenuState.QuitState:
                {
                    warningPanel.SetActive(false);
                    break;
                }
            case MenuState.TutorialState:
                {
                    tutorialPanel.SetActive(false);
                    break;
                }
        }
    }

    public void QuitGame()
    {

        Application.Quit();
    }
    public void ResetGame()
    {
        GameManager.instance.ClearStars();
    }
}
