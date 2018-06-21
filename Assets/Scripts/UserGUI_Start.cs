
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
    private Button btn_info;
    private Button btn_quit;

    void Start()
    {
        director = SSDirector.getInstance();
        //action = director.currentSceneController as IUserAction_Start;
        btn_start = GameObject.Find("Canvas/NewGame/Start").GetComponent<Button>();
        btn_info = GameObject.Find("Canvas/NewGame/Guide").GetComponent<Button>();

        btn_start.onClick.AddListener(scanScene);
        btn_info.onClick.AddListener(readGuide);
    }

    public void scanScene()
    {
        director.play();
    }

    public void readGuide()
    {
        GameObject.Find("Canvas/NewGame/GuideContent").SetActive(true);

        btn_quit = GameObject.Find("Canvas/NewGame/GuideContent/Quit").GetComponent<Button>();
        btn_quit.onClick.AddListener(quitGuide);
        
    }

    public void quitGuide()
    {
        GameObject.Find("Canvas/NewGame/GuideContent").SetActive(false);
    }
}
