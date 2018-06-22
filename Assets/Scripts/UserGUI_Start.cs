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
    private Button btn_pageUp;
    private Button btn_pageDown;
    private int pageState;

    void Start()
    {
        pageState = 0;
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
        pageState = 1;

        btn_quit = GameObject.Find("Canvas/NewGame/GuideContent/Quit").GetComponent<Button>();
        btn_pageUp = GameObject.Find("Canvas/NewGame/GuideContent/PageUp").GetComponent<Button>();
        btn_pageDown = GameObject.Find("Canvas/NewGame/GuideContent/PageDown").GetComponent<Button>();

        btn_quit.onClick.AddListener(quitGuide);
        btn_pageUp.onClick.AddListener(changePage);
        btn_pageDown.onClick.AddListener(changePage);
        
    }

    public void quitGuide()
    {
        pageState = 0;
        GameObject.Find("Canvas/NewGame/GuideContent").SetActive(false);
    }

    public void changePage()
    {
        if(pageState == 1)
        {
            Image img = GameObject.Find("Canvas/NewGame/GuideContent").GetComponent<Image>() as Image;
            img.sprite = Resources.Load("Images/secondPage", typeof(Sprite)) as Sprite;
            pageState = 2;
        }else if(pageState == 2)
        {
            Image img = GameObject.Find("Canvas/NewGame/GuideContent").GetComponent<Image>() as Image;
            img.sprite = Resources.Load("Images/firstPage", typeof(Sprite)) as Sprite;
            pageState = 1;
        }
    }
}

