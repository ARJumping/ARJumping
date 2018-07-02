using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRecorder : MonoBehaviour, IRecorder
{
    SceneController_Playing sceneController;
    private static ScoreRecorder _instance;
    private Button btn_again;
    private Text scoreText;
    private bool showScore;
    private Vector3 boardScale;
    private float score;

    //分数板
    public GameObject ScoreBoard;
    //每偏离一定距离一档分数
    public float LayeringDistance = 0.5f;
    //各项基础分
    public float BaseScore = 10f;
    public float ArmsScore = 5f;
    public float TimeScore = 100f;
    //计分的指数函数底数(0-1)
    public float BaseNumber = 0.5f;
    //各项得分系数
    public float DitanceCoefficient = 0.4f;
    public float ItemCoefficient = 0.3f;
    public float TimeCoefficient = 0.3f;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = Singleton<ScoreRecorder>.Instance;
        }
    }

    void Start()
    {
        sceneController = (SceneController_Playing)SSDirector.getInstance().currentSceneController;
        sceneController.currentScoreRecorder = this;
        showScore = false;
        boardScale = new Vector3(0f, 0f, 1f);
        score = 0f;
    }

    void Update()
    {
        //显示分数版
        if(showScore)
        {
            boardScale.x += Time.deltaTime * 10;
            boardScale.y += Time.deltaTime * 7;
            if (boardScale.x <= 10.0f)
            {
                ScoreBoard.GetComponent<RectTransform>().localScale = boardScale;
            }
            else
            {
                showScore = false;
            }
        }
    }

    //偏离距离加分
    public void addScoreByDistance(float distance)
    {
        score += DitanceCoefficient * BaseScore * Mathf.Pow(BaseNumber, Mathf.FloorToInt(distance / LayeringDistance));
    }
    //道具加分
    public void addScoreByItem(string itemName)
    {
        if (itemName == "Arms")
        {
            score += ItemCoefficient * ArmsScore;
        }
    }

    //获取当前分数
    public float getScore()
    {
        return score;
    }

    //清除记录
    public void clear()
    {
        score = 0;
    }

    public void backToStart()
    {
        sceneController.backToStart();
    }

    public void showScoreBoard(string state)
    {
        showScore = true;
        btn_again = GameObject.Find("Canvas/ScoreBoard/Again").GetComponent<Button>();
        btn_again.onClick.AddListener(backToStart);
        scoreText = GameObject.Find("Canvas/ScoreBoard/Score").GetComponent<Text>();
        scoreText.text = "Score: " + score.ToString("f1");
        if (state == "win")
        {
            Text info = GameObject.Find("Canvas/ScoreBoard/GameOver").GetComponent<Text>();
            info.text = "You Win !!!";
        }
    }
}
