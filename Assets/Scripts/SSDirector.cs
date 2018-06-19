using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//与场记通信的接口
public interface ISceneController
{
    void LoadResources();
}

public class SSDirector : System.Object
{
    //单实例导演对象
    private static SSDirector _instance;
    private static string _state = "NewGame";
    //当前的场记
    public ISceneController currentSceneController { get; set; }

    //获取导演实例
    public static SSDirector getInstance()
    {
        if (_instance == null)
        {
            _instance = new SSDirector();
        }
        return _instance;
    }

    //获取程序运行的帧率
    public int getFPS()
    {
        return Application.targetFrameRate;
    }

    //设置程序运行的帧率
    public void setFPS(int fps)
    {
        Application.targetFrameRate = fps;
    }
    //获取当前状态
    public string getState()
    {
        return _state;
    }
    //切换场景
    public void switchScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(_state);
    }

    public void play()
    {
        _state = "Playing";
        switchScene();
    }

    public void gameOver()
    {
        _state = "GameOver";
        //currentSceneController.gameOver();
    }

    //再玩一次-沿用旧图
    public void playAgain()
    {
        _state = "Playing";
        //重新加载当前场景
        //clearAllData();
        switchScene();
    }

    public void backToStart()
    {
        _state = "NewGame";
        //重新加载当前场景
        //clearAllData();
        switchScene();
    }
}
