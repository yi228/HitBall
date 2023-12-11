using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;
using TMPro;

public class LogInScene : MonoBehaviour
{
    public TextMeshProUGUI logintext;
    public static LogInScene instance;
    public string userName = "1";
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        SignIn();
    }
    public void SignIn()
    {
        logintext.text = "SignInFunc";
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }
    internal void ProcessAuthentication(SignInStatus status)
    {
        logintext.text = "ProcessAuthentication";
        if (status == SignInStatus.Success)
        {
            userName = PlayGamesPlatform.Instance.GetUserDisplayName();
            logintext.text = userName;
            LoadInGameScene();
        }
        else
        {
            Debug.Log("LoginFailed");
            logintext.text = "로그인 실패";
            userName = "Guest";
        }
    }
    private void LoadInGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
