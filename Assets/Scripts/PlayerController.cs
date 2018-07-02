using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IActionManager {
    //单实例
    private static PlayerController _instance;
    public float jumpGroundAngle = 45.0f;

    void Awake()
    {
        if(_instance == null)
        {
            _instance = Singleton<PlayerController>.Instance;
        }
    }

	void Start () {
        //将当前的动作管理者设为自身
        SceneController_Playing sceneController = (SceneController_Playing)SSDirector.getInstance().currentSceneController;
        sceneController.currentActionManager = this;
	}

    void Update()
    {
        //限制跳跃的角度
        if(jumpGroundAngle > 90.0f)
        {
            jumpGroundAngle = 90.0f;
        }
        if(jumpGroundAngle < 10.0f)
        {
            jumpGroundAngle = 10.0f;
        }
    }
	
	public void jump(float time)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            //float force = slidingDistance / 750.0f;
            float force = time;
            Vector3 forward = player.transform.forward;
           // Debug.Log("Jump forward " + forward);
            Vector3 up = new Vector3(0, 1, 0) * forward.sqrMagnitude * Mathf.Tan(jumpGroundAngle);
            Vector3 jumpDir = (up + forward).normalized;
            player.GetComponent<Rigidbody>().AddForce(jumpDir * force, ForceMode.Impulse);
        }
    }
}
