using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserGUI_Rendering : MonoBehaviour {
    private SSDirector director;
    private Button btn_build;

    // Use this for initialization
    void Start () {
        btn_build = GameObject.Find("Canvas/Build").GetComponent<Button>();
        btn_build.onClick.AddListener(buildScene);
    }
	
	public void buildScene()
    {
        //建立模型
        director.play();
    }
}
