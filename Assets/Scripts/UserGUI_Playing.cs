using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//与场记的通信接口
public interface IUserAction_Playing
{
    float getScore();
    void jump(float slidingDistance);
}

public class UserGUI_Playing : MonoBehaviour {
    //与场记通信的动作对象实例
    private IUserAction_Playing action;
    private Vector2 touchFirst;   //手指按下的位置
    private Vector2 touchSecond;   //手指拖动过程中的位置
    private float accumulationPower;   //蓄力力度，随时间增长
    private bool accumulation;   //判断是否处于蓄力中
    private ETCButton JumpB;
    //private float endTime;
    //private float slidingDistance;
    //玩家
    public GameObject Player;
    public Scrollbar AccumulationBar;
    //public float MinimumSlidingDistance = 80f;
    //public float MaxDistance = 1000000f;
    //public float minTime = 0.5f;
    public Text Score;
    public float MinPower = 0f;
    public float MaxPower = 1000f;
    public float AddRate = 500f;
    public float DecreaseRate = 1000f;

    void Start() {
        action = SSDirector.getInstance().currentSceneController as IUserAction_Playing;
        accumulationPower = 0f;
        accumulation = false;
        JumpB = GameObject.Find("EasyTouchControlsCanvas/JumpB").GetComponent<ETCButton>();
        JumpB.onPressed.AddListener(ToJump);
        JumpB.onUp.AddListener(Jump);
    }

    void Update() {
        if (accumulation)
        {
            accumulationPower += Time.deltaTime * AddRate;
            if (accumulationPower > MaxPower)
            {
                accumulationPower = MaxPower;
            }
            AccumulationBar.size = accumulationPower / MaxPower;
        }
        if (!accumulation)
        {
            accumulationPower -= Time.deltaTime * DecreaseRate;
            if (accumulationPower < MinPower)
            {
                accumulationPower = MinPower;
            }
            AccumulationBar.size = accumulationPower / MaxPower;
        }
        //玩家出现则显示分数
        Score.gameObject.SetActive(Player.activeSelf);
        AccumulationBar.gameObject.SetActive(Player.activeSelf);
        Score.text = "Score: " + action.getScore().ToString("f1");
    }

//    void OnGUI() {
//        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
//        {
//            //Debug.Log(Input.GetTouch(0).position);
//#if IPHONE || ANDROID
//			if (IsPointerOverGameObject())
//#else
//            if (EventSystem.current.IsPointerOverGameObject())
//#endif
//            {
//                Debug.Log("当前触摸在UI上");
//            }
//            else
//            {
//                Debug.Log("当前没有触摸在UI上");
//                accumulation = true;
//            }
//        }
//        if(Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
//        {
//            accumulation = false;
//            action.jump(accumulationPower);
//        }
        //    //开始滑动
        //    if (Event.current.type == EventType.MouseDown && !EventSystem.current.IsPointerOverGameObject() && accumulationPower < MaxPower)
        //    {
        //        //touchFirst = Event.current.mousePosition;
        //        //slidingDistance = 0f;
        //        accumulation = true;
        //    }
        //    //滑动中计算滑动距离
        //    //if (Event.current.type == EventType.MouseDrag && slidingDistance < MaxDistance)
        //    //{
        //    //    touchSecond = Event.current.mousePosition;
        //    //    slidingDistance += (touchSecond - touchFirst).sqrMagnitude;
        //    //    touchFirst = touchSecond;
        //    //    Debug.Log("length " + slidingDistance);
        //    //    if(slidingDistance > MaxDistance)
        //    //    {
        //    //        slidingDistance = MaxDistance;
        //    //    }
        //    //    Debug.Log("size: " + slidingDistance / MaxDistance);
        //    //    AccumulationBar.size = slidingDistance / MaxDistance;
        //    //}
        //    //结束滑动
        //    //if (Event.current.type == EventType.MouseUp && slidingDistance > MinimumSlidingDistance && !EventSystem.current.IsPointerOverGameObject())
        //    //{
        //    //    action.jump(slidingDistance);
        //    //}
        //    if (Event.current.type == EventType.MouseUp)
        //    {
        //        accumulation = false;
        //        action.jump(accumulationPower);
        //    }
   // }

    //public static bool IsPointerOverGameObject()
    //{
    //    PointerEventData eventData = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
    //    eventData.pressPosition = Input.mousePosition;
    //    eventData.position = Input.mousePosition;

    //    List<RaycastResult> list = new List<RaycastResult>();
    //    UnityEngine.EventSystems.EventSystem.current.RaycastAll(eventData, list);
    //    return list.Count > 0;
    //}
    void ToJump()
    {
        accumulation = true;
    }

    void Jump()
    {
        accumulation = false;
        action.jump(accumulationPower);
    }
}
