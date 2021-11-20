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

        GoLobbyScene();
    }

    void Update()
    {
    }

    public void GoLobbyScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void GoStageScene()
    {
        SceneManager.LoadScene("StageScene");
    }

    public void GoBonFireScene()
    {
        SceneManager.LoadScene("BonFireScene");
    }

    public void GoTutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void GoStoryScene()
    {
        SceneManager.LoadScene("StoryScene");
    }

    public void GoStageSelectScene()
    {
        SceneManager.LoadScene("StageSelectScene");
    }

    public void GoResultScene()
    {

    }

    public void GoCharacterMakingScene()
    {

    }

}
