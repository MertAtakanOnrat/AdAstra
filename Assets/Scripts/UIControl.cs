using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UIControl : MonoBehaviour
{
    [SerializeField]
    GameObject gameNameText = default;

    [SerializeField]
    GameObject gameOverText = default;

    [SerializeField]
    GameObject playButton = default;

    [SerializeField]
    GameObject playAgainButton = default;

    [SerializeField]
    GameObject storeButton = default;

    [SerializeField]
    GameObject leaderboardButton = default;

    [SerializeField]
    Text scoreText = default;

    [SerializeField]
    Text highScoreText = default;

    [SerializeField]
    TMP_InputField emailInputField = default;

    [SerializeField]
    TMP_InputField usernameInputField = default;

    [SerializeField]
    TMP_InputField passwordInputField = default;

    [SerializeField]
    GameObject loginButton = default;

    [SerializeField]
    GameObject registerButton = default;

    [SerializeField]
    GameObject wrongInputText = default;

    [SerializeField]
    GameObject userNotFoundText = default;

    [SerializeField]
    GameObject alreadyExistsText = default;

    [SerializeField]
    GameObject loginInfoText = default;

    [SerializeField]
    GameObject registerSuccessfulText = default;

    [SerializeField]
    GameObject invalidInputRegisterText = default;

    [SerializeField]
    GameObject homeButton = default;

    [SerializeField]
    GameObject leaderboard = default;

    [SerializeField]
    GameObject returnHome = default;

    int score;
    int highScore;

    PlayFabManager playFabManager;


    // Start is called before the first frame update
    void Start()
    {
        ///
        playFabManager = Camera.main.GetComponent<PlayFabManager>();
        ///
        gameOverText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        storeButton.gameObject.SetActive(false);
        leaderboardButton.gameObject.SetActive(false);
        emailInputField.gameObject.SetActive(true);
        usernameInputField.gameObject.SetActive(true);
        passwordInputField.gameObject.SetActive(true);
        loginButton.gameObject.SetActive(true);
        registerButton.gameObject.SetActive(true);
        wrongInputText.gameObject.SetActive(false);
        alreadyExistsText.gameObject.SetActive(false);
        userNotFoundText.gameObject.SetActive(false);
        loginInfoText.gameObject.SetActive(true);
        registerSuccessfulText.gameObject.SetActive(false);
        invalidInputRegisterText.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
        leaderboard.gameObject.SetActive(false);
        returnHome.gameObject.SetActive(false);
    }
    public void DisplayLeaderboard()
    {
        leaderboard.gameObject.SetActive(true);
        gameNameText.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        storeButton.gameObject.SetActive(false);
        leaderboardButton.gameObject.SetActive(false);
        returnHome.gameObject.SetActive(true);
    }
    public void LoginSuccessful()
    {
        playButton.gameObject.SetActive(true);
        storeButton.gameObject.SetActive(true);
        leaderboardButton.gameObject.SetActive(true);

        highScoreText.gameObject.SetActive(true);
        SetAndGetHighScore();
        highScoreText.text = "High Score: " + highScore;

        emailInputField.gameObject.SetActive(false);
        usernameInputField.gameObject.SetActive(false);
        passwordInputField.gameObject.SetActive(false);
        loginButton.gameObject.SetActive(false);
        registerButton.gameObject.SetActive(false);
        loginInfoText.gameObject.SetActive(false);
        registerSuccessfulText.gameObject.SetActive(false);
        userNotFoundText.gameObject.SetActive(false);
        wrongInputText.gameObject.SetActive(false);
        invalidInputRegisterText.gameObject.SetActive(false);
    }

    public void LoginFailure(string error)
    {
        userNotFoundText.gameObject.SetActive(false);
        wrongInputText.gameObject.SetActive(false);
        
        if (error == "/Client/LoginWithPlayFab: User not found")
        {
            userNotFoundText.gameObject.SetActive(true);
        }
        else if(error == "/Client/LoginWithPlayFab: Invalid username or password")
        {
            wrongInputText.gameObject.SetActive(true);
        }
        else
        {
            wrongInputText.gameObject.SetActive(true);
        }
        
    }

    public void RegisterSuccessful()
    {
        invalidInputRegisterText.gameObject.SetActive(false);
        alreadyExistsText.gameObject.SetActive(false);
        registerSuccessfulText.gameObject.SetActive(true);
    }

    public void RegisterFailure(string error)
    {
        invalidInputRegisterText.gameObject.SetActive(false);
        alreadyExistsText.gameObject.SetActive(false);
        if (error.Substring(0,53) == "/Client/RegisterPlayFabUser: Invalid input parameters")
        {
            invalidInputRegisterText.gameObject.SetActive(true);
        }
        else if(error.Substring(0,56) == "/Client/RegisterPlayFabUser: Email address not available" ||
            error.Substring(0,51) == "/Client/RegisterPlayFabUser: Username not available")
        {
            alreadyExistsText.gameObject.SetActive(true);
        }
        else
        {
            invalidInputRegisterText.gameObject.SetActive(true);
        }
        
    }

    public void ReturnHomePage()
    {
        gameNameText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
        storeButton.gameObject.SetActive(true);
        leaderboardButton.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
        playAgainButton.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
        leaderboard.gameObject.SetActive(false);
        returnHome.gameObject.SetActive(false);
    }

    public void GameIsStarted()
    {
        score = 0;
        gameNameText.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        storeButton.gameObject.SetActive(false);
        leaderboardButton.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        homeButton.gameObject.SetActive(false);
        UpdateScore();

    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        playAgainButton.gameObject.SetActive(true);
        //SetAndGetHighScore();
        playFabManager.GetHighScoreGameOver();
        //Debug.Log("score = " + score);
        //Debug.Log("highscore = " + highScore);

        if (score >= highScore && score >= playFabManager.HighScore)
        {
            
            highScore = score;
            Debug.Log("highscoreeeeeeeee = " + highScore);
        }

        highScoreText.text = "High Score: " + highScore;
        playFabManager.SendLeaderboard(highScore);
        homeButton.gameObject.SetActive(true);
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    int SetAndGetHighScore()
    {
        playFabManager.GetHighScoreGameOver();
        highScore = playFabManager.HighScore;
        Debug.Log("setandget1 = " + playFabManager.HighScore);
        Debug.Log("setandget2 = " + highScore);
        return highScore;

        //if (highScore == 0)
        //{
            
        //    highScore = score;
        //    return highScore;
        //}
        //else
        //{
        //    highScore = playFabManager.HighScore;
        //    return highScore;
        //}
        
    }
    

    public void AsteroidDestroyed(GameObject asteroid)
    {
        switch (asteroid.gameObject.name[8])
        {
            case '1':
                score += 20;
                UpdateScore();
                break;
            case '2':
                score += 15;
                UpdateScore();
                break;
            case '3':
                score += 10;
                UpdateScore();
                break;
            case '4':
                score += 5;
                UpdateScore();
                break;
            default:
                break;
        }
    }
}
