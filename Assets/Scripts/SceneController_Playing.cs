using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//与记录员通信的接口
public interface IRecorder
{
    //ScoreRecord
    void addScore(string ring);
    int getScore();
    void clear();
}

//与动作管理者通信的接口
public interface IActionManager
{
    //ShootManager
    //void fire(GameObject arrow, Vector3 emitPos, Vector3 emitDir);
    //float getFireRate();
}

//与对象工厂通信的接口
public interface IObjectFactory
{
    GameObject getBuilding();
    GameObject getPlayer();
    GameObject getItems();
    void freeObject(GameObject obj);
}

public class SceneController : MonoBehaviour, ISceneController, IUserAction_Playing
{
    public IRecorder currentScoreRecorder { get; set; }
    public IActionManager currentActionManager { get; set; }
    public IObjectFactory currentObjectFactory { get; set; }
    public SSDirector director;

    void Awake()
    {
        director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentSceneController = this;
        director.currentSceneController.LoadResources();
    }
    //载入游戏相关资源
    public void LoadResources()
    {
        //初始化预制资源
        //target = GameObject.Instantiate(target, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    }

    //获取当前分数
    public int getScore()
    {
        return currentScoreRecorder.getScore();
    }

    //获取发射间隔
    //public float getFireRate() {
    //    return currentActionManager.getFireRate();
    //}
}
