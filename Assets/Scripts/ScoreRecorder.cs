using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour, IRecorder
{
    private static ScoreRecorder _instance;
    private int score = 0;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = Singleton<ScoreRecorder>.Instance;
        }
    }

    void Start()
    {
        SceneController sceneController = (SceneController)SSDirector.getInstance().currentSceneController;
        sceneController.currentScoreRecorder = this;
    }

    //加分
    public void addScore(string ring)
    {
        switch (ring)
        {
            case "1":
                score += 5;
                break;
            case "2":
                score += 4;
                break;
            case "3":
                score += 3;
                break;
            case "4":
                score += 2;
                break;
            case "5":
                score += 1;
                break;
        }
    }

    //获取当前分数
    public int getScore()
    {
        return score;
    }

    //清除记录
    public void clear()
    {
        score = 0;
    }
}
