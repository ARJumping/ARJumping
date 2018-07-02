using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    private SceneController_Playing sceneController;
    private ScoreRecorder recorder;
    private ETCButton Item;
    private string currentPlatform;
    private float deathHeight = -5.0f;
    private string currentHoldingItem = "";
    private bool hasShield = false;
    //private bool usingItem = false;
    //private float rocketTimer = 0f;

    //public float RocketTime = 5f;
    public float RocketPower = 10f;


    private void Start()
    {
        sceneController = (SceneController_Playing)SSDirector.getInstance().currentSceneController;
        recorder = (ScoreRecorder)FindObjectOfType(typeof(ScoreRecorder));
        currentPlatform = "StartPlatform";
        Item = GameObject.Find("EasyTouchControlsCanvas/Item").GetComponent<ETCButton>();
        Item.onPressed.AddListener(useItem);
        Item.onUp.AddListener(freeItem);
    }

    private void Update()
    {
        //玩家高度低于一定值判定死亡
        if (this.gameObject.transform.position.y <= deathHeight)
        {
            gameOver();
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        //同一平台不加分
        if (other.gameObject.name != currentPlatform)
        {
            //撞到平台时加分
            if (other.transform.tag == "EndPlatform" || other.transform.tag == "Platform")
            {
                //将Player附属到下一个ImageTarget
                currentPlatform = other.gameObject.name;
                this.gameObject.transform.parent = other.gameObject.transform.parent.parent;
                //获取碰撞点坐标以计算距离中心点距离
                ContactPoint contact = other.contacts[0];
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 collisionPoint = contact.point;
                Vector3 centerPoint = other.transform.position;
                float offsetDistance = (collisionPoint - centerPoint).magnitude;
                recorder.addScoreByDistance(offsetDistance);
                if (other.transform.tag == "EndPlatform")
                {
                    sceneController.setGameState("win");
                }
            }
            //撞到道具
            else if(other.transform.tag == "Item")
            {
                bool isRocket = other.transform.name.Contains("Rocket");
                bool isArms = other.transform.name.Contains("Arms");
                bool isShield = other.transform.name.Contains("Shield");
                bool isTrap = other.transform.name.Contains("Trap");
                if (isRocket)
                {
                    currentHoldingItem = "Rocket";
                    sceneController.setItem("Rocket");
                    //待实现切换按钮图片
                }
                else if (isShield)
                {
                    hasShield = true;
                    //加护盾图
                }
                else if (isArms)
                {
                    recorder.addScoreByItem("Arms");
                }
                else if (isTrap)
                {
                    //无护盾死亡
                    if (!hasShield)
                    {
                        gameOver();
                    }
                    else
                    {
                        hasShield = false;
                        //去掉护盾图
                    }
                }
                //释放道具对象
                sceneController.currentObjectFactory.freeObject(other.gameObject);
            }
        }
    }

    public void useItem()
    {
        //无道具不处理
        if(currentHoldingItem == "")
        {
            return;
        }
        else
        {
            if (currentHoldingItem == "Rocket")
            {
                Vector3 forward = this.gameObject.transform.forward;
                forward.y = 0f;   //水平飞行
                Vector3 up = new Vector3(0, 1, 0) * forward.sqrMagnitude * Mathf.Tan(45);
                Vector3 dir = (up + forward).normalized;
                this.GetComponent<Rigidbody>().AddForce(dir * RocketPower, ForceMode.Impulse);
            }
        }
    }

    public void freeItem()
    {
        //无道具不处理
        if (currentHoldingItem == "")
        {
            return;
        }
        else
        {
            //usingItem = false;
            currentHoldingItem = "";
            sceneController.freeItem();
        }
    }

    public void gameOver()
    {
        sceneController.setGameState("failed");
        this.gameObject.SetActive(false);
        GameObject.Find("EasyTouchControlsCanvas/DirectionController").GetComponent<ETCJoystick>().visible = false;
        Item.visible = false;
    }
}
