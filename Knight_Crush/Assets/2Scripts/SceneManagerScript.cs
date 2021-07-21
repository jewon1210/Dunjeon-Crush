using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    static SceneManagerScript UniqueInstance;
    public static SceneManagerScript _instance
    {
        get { return UniqueInstance; }
    }

    public enum CurScene
    {
        EmptyS = 0,
        LobbyS,
        
    }

    void Start()
    {
        UniqueInstance = this;
    }

    void Update()
    {
    }

    public void GoLobbyScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void GoInGameScene()
    {
        SceneManager.LoadScene("InGameScene");
    }

    public void GoStageSelectScene()
    {
        
    }

    public void GoResultScene()
    {

    }

    public void GoCharacterMakingScene()
    {

    }

}
