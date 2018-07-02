using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
//与记录员通信的接口
public interface IRecorder
{
    //ScoreRecord
    float getScore();
    void clear();
    void showScoreBoard(string state);
}

//与动作管理者通信的接口
public interface IActionManager
{
    void jump(float time);
}

//与对象工厂通信的接口
public interface IObjectFactory
{
    GameObject getItem(string type);
    void freeObject(GameObject obj);
}

public class SceneController_Playing : MonoBehaviour, ISceneController, IUserAction_Playing
{
    public IRecorder currentScoreRecorder { get; set; }
    public IActionManager currentActionManager { get; set; }
    public IObjectFactory currentObjectFactory { get; set; }
    public SSDirector director;
    private WebCamTexture camTexture;

    private int FPS = 60;
    private int FPScount = 0;

    void Awake()
    {
        director = SSDirector.getInstance();
        director.setFPS(FPS);
        director.currentSceneController = this;
        director.currentSceneController.LoadResources();
    }
    //自动对焦
    void Start()
    {
        //WebCamDevice[] devices = WebCamTexture.devices;
        //if (devices.Length > 0)
        //{
        //    Debug.Log("DevicesName" + devices[0].name);
        //    Text Text = GameObject.Find("Canvas/Text").GetComponent<Text>();
        //    Text.text = "DevicesName: " + devices[0].name;
        //}
        bool focusModeSet = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        if (!focusModeSet)
        {
            Debug.Log("Failed to set focus mode (unsupported mode).");
        }
    }

    void Update()
    {
        if(FPScount++ >= FPS)
        {
            bool focusModeSet = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
            if (!focusModeSet)
            {
                Debug.Log("Failed to set focus mode (unsupported mode).");
            }
            FPScount = 0;
        }
    }

    //载入游戏相关资源
    public void LoadResources()
    {
        //初始化预制资源
        //target = GameObject.Instantiate(target, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    }

    public void setGameState(string state)
    {
        director.gameOver();
        //关闭识别引擎
        ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        tracker.Stop();
        //显示分数
        currentScoreRecorder.showScoreBoard(state);
    }

    public void backToStart()
    {
        currentScoreRecorder.clear();
        director.backToStart();
    }

    //获取当前分数
    public float getScore()
    {
        return currentScoreRecorder.getScore();
    }

    //跳跃
    public void jump(float time)
    {
        currentActionManager.jump(time);
    }
    //添加道具图标
    public void setItem(string name)
    {
        string path = "Images/" + name;
        GameObject.Find("EasyTouchControlsCanvas/Item").transform.GetComponent<UnityEngine.UI.Image>().overrideSprite = Resources.Load(path, typeof(Sprite)) as Sprite;
    }
    //删除道具图标
    public void freeItem()
    {
        string path = "Images/ItemButton";
        GameObject.Find("EasyTouchControlsCanvas/Item").transform.GetComponent<UnityEngine.UI.Image>().overrideSprite = Resources.Load(path, typeof(Sprite)) as Sprite;
    }
}
