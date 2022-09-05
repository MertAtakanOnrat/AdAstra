using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System.Threading;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField emailInputField = default;

    [SerializeField]
    TMP_InputField usernameInputField = default;

    [SerializeField]
    TMP_InputField passwordInputField = default;

    [SerializeField]
    GameObject rowPrefab;

    [SerializeField]
    Transform rowsParent;

    UIControl uIControl;

    int highScore = 12;

    public int HighScore { get { return highScore; } }
    // Start is called before the first frame update
    void Start()
    {
        uIControl = Camera.main.GetComponent<UIControl>();
        
        //Login();
        //RegisterClick();
        //LoginClick();

    }
    public void UpdateDisplayName()
    {
        var request = new UpdateUserTitleDisplayNameRequest();
        request.DisplayName = usernameInputField.text;
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateDisplayName, OnError);
    }
    void OnUpdateDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log(result.DisplayName + " heloooooo");
    }
    public void RegisterClick()
    {
        var register = new RegisterPlayFabUserRequest {
            Username = usernameInputField.text,
            Email = emailInputField.text,
            Password = passwordInputField.text };
        PlayFabClientAPI.RegisterPlayFabUser(register, OnRegisterSuccess, OnRegisterFailure);

        
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        uIControl.RegisterSuccessful();
        UpdateDisplayName();
        Debug.Log("Successful account create!");
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        uIControl.RegisterFailure(error.GenerateErrorReport());
        Debug.Log("Error while creating account!");
        Debug.Log(error.GenerateErrorReport());
    }

    public void LoginClick()
    {
        var login = new LoginWithPlayFabRequest { Username = usernameInputField.text, Password = passwordInputField.text };
        
        PlayFabClientAPI.LoginWithPlayFab(login, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Successful login!" + result.PlayFabId);
        //////////////////////////////////
        GetHighScore();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        uIControl.LoginFailure(error.GenerateErrorReport());
        Debug.Log("Error while loggining in!");
        Debug.Log(error.GenerateErrorReport());
    }
   
    public void GetAccountInfo()
    {
        var request = new GetAccountInfoRequest();
        
        PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInfoSuccess, OnGetAccountInfoError);
    }

    private void OnGetAccountInfoError(PlayFabError obj)
    {
        print("Anfrage der Account Daten hat nicht funktioniert");
    }

    private void OnGetAccountInfoSuccess(GetAccountInfoResult resultData)
    {
        Debug.Log("saaaaa" + resultData.AccountInfo.Username);
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "HighScore",
                    Value = score
                    
    }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Succesful leaderboard sent");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "HighScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }
        
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(item.Position + " " + item.DisplayName+ " " + item.StatValue);
        }
        uIControl.DisplayLeaderboard();
    }

    public void GetHighScore()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "HighScore",
            MaxResultsCount = 1
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundPlayer, OnError);
    }
    void OnGetLeaderboardAroundPlayer(GetLeaderboardAroundPlayerResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            highScore = item.StatValue;
           
        }
        uIControl.LoginSuccessful();
    }

    public void GetHighScoreGameOver()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "HighScore",
            MaxResultsCount = 1
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundPlayerGameOver, OnError);
    }
    void OnGetLeaderboardAroundPlayerGameOver(GetLeaderboardAroundPlayerResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            highScore = item.StatValue;

        }
        
    }
}
