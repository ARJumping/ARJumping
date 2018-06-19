using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//与场记的通信接口
//public interface IUserAction_Start
//{

//}

public class UserGUI_Start : MonoBehaviour
{
    //与场记通信的动作对象实例
    //private IUserAction_Start action;
    private SSDirector director;
    private Button btn_start;

    void Start()
    {
        director = SSDirector.getInstance();
        //action = director.currentSceneController as IUserAction_Start;
        btn_start = GameObject.Find("Canvas/NewGame/Start").GetComponent<Button>();
        btn_start.onClick.AddListener(scanScene);
    }

    public void scanScene()
    {
        director.play();
    }
}
