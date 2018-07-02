using UnityEngine;
using Vuforia;

public class MyTrackableEventHandler : DefaultTrackableEventHandler
{
    protected override void OnTrackingFound()
    {
        var children = GetComponentsInChildren<Transform>(true);
        foreach (var child in children)
        {
            if (child.tag != "ImageTarget")
            {
                child.gameObject.SetActive(true);
            }
            //找到Player时激活摇杆
            if (child.tag == "Player")
            {
                GameObject.Find("EasyTouchControlsCanvas/DirectionController").GetComponent<ETCJoystick>().visible = true;
                GameObject.Find("EasyTouchControlsCanvas/Item").GetComponent<ETCButton>().visible = true;
                GameObject.Find("EasyTouchControlsCanvas/JumpB").GetComponent<ETCButton>().visible = true;
            }
        }
    }


    protected override void OnTrackingLost()
    {
        var children = GetComponentsInChildren<Transform>(true);
        foreach (var child in children)
        {
            if(child.tag != "ImageTarget")
            {
                child.gameObject.SetActive(false);
            }
            if(child.tag == "Player")
            {
                GameObject.Find("EasyTouchControlsCanvas/DirectionController").GetComponent<ETCJoystick>().visible = false;
                GameObject.Find("EasyTouchControlsCanvas/Item").GetComponent<ETCButton>().visible = false;
                GameObject.Find("EasyTouchControlsCanvas/JumpB").GetComponent<ETCButton>().visible = false;
            }
        }
    }
}