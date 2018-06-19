using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//与场记的通信接口
public interface IUserAction_Playing
{
    //void startGame();
    //void gameOver();
    //void reStart();    //重新开始-重新建图
    //void playAgain();  //再玩一次-沿用旧图
    int getScore();
}

public class UserGUI_Playing : MonoBehaviour {
    //与场记通信的动作对象实例
    private IUserAction_Playing action;
    private SSDirector director;
    //private float nextFireTime;
    //public Camera cam;
    public Text scoreText;

    void Start() {
        director = SSDirector.getInstance();
        action = director.currentSceneController as IUserAction_Playing;
    }

    void Update() {
        scoreText.text = "Score: " + action.getScore();
        //if (Input.GetMouseButtonDown(0) && Time.time > nextFireTime) {
        //    nextFireTime = Time.time + action.getFireRate();
        //    Camera ca = cam.GetComponent<Camera>();
        //    Ray mouseRay = ca.ScreenPointToRay(Input.mousePosition);
        //    action.fire(cam.transform.position, mouseRay.direction);
        //}
    }

    void OnGUI() {
        //if (GUI.Button(new Rect(0, 60, 120, 40), "Start")) {
        //    //换场景：开始页-渲染页-游戏页
        //    director.render();
        //}
        //if (GUI.Button(new Rect(0, 120, 120, 40), "Render"))
        //{
        //    director.play();
        //}
        //if (GUI.Button(new Rect(0, 120, 120, 40), "Again")) {
        //    director.playAgain();
        //}
        //if (GUI.Button(new Rect(0, 120, 120, 40), "Restart"))
        //{
        //    director.render();
        //}
    }
}
