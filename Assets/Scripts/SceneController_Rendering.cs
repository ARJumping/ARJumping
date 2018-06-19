using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController_Rendering : MonoBehaviour, ISceneController//, IUserAction_Start
{
    public SSDirector director;
    //public GameObject target;

    void Awake()
    {
        director = SSDirector.getInstance();
        director.currentSceneController = this;
        director.currentSceneController.LoadResources();
    }
    //载入游戏相关资源
    public void LoadResources()
    {
        //初始化预制资源
        //target = GameObject.Instantiate(target, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    }

}
