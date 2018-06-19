//using EasyAR;
//using System;
//using UnityEngine;

//public class MyImageTargetBehaviour : ImageTargetBehaviour
//{
//    private float lastX;
//    private float lastY;
//    private float lastZ;
//    private float lastRX;
//    private float lastRY;
//    private float lastRZ;
//    protected override void Awake()
//    {
//        base.Awake();
//        TargetFound += OnTargetFound;
//        TargetLost += OnTargetLost;
//        TargetLoad += OnTargetLoad;
//        TargetUnload += OnTargetUnload;
//    }
//    protected override void Start()
//    {
//        base.Start();
//        HideObjects(transform);
//    }

//    protected override void Update()
//    {
//        base.Update();
//        float myrx = 0;
//        myrx = this.transform.localEulerAngles.x;
//        while (myrx >= 360)//为了让判断条件时方便，强制把所有不在1~270以内的数字，转换为-270~270  
//        {
//            myrx -= 360;
//        }
//        while (myrx <= -360)
//        {
//            myrx += 360;
//        }
//        while (myrx > 270 && 360 - myrx >= 0)
//            myrx = -(360 - myrx);
//        float myry = 0;
//        myry = this.transform.localEulerAngles.y;
//        while (myry >= 360)
//        {
//            myry -= 360;
//        }
//        while (myry <= -360)
//        {
//            myry += 360;
//        }

//        while (myry > 270 && 360 - myry >= 0)
//            myry = -(360 - myry);

//        float myrz = 0;
//        myrz = this.transform.localEulerAngles.z;
//        while (myrz >= 360)
//        {
//            myrz -= 360;
//        }

//        while (myrz <= -360)
//        {
//            myrz += 360;
//        }

//        while (myrz > 270 && 360 - myrz >= 0)
//            myrz = -(360 - myrz);
//        //关键，当模型抖动超过一定范围时，不修正模型的坐标角度，记录坐标和角度  
//        if (((Math.Abs(this.transform.position.x - lastX) > 0.06 || Math.Abs(this.transform.position.y - lastY) > 0.06 || Math.Abs(this.transform.position.z - lastZ) > 0.06) &&
//     (Math.Abs(this.transform.position.x - lastX) > 0.13 || Math.Abs(this.transform.position.y - lastY) > 0.13 || Math.Abs(this.transform.position.z - lastZ) > 0.13)) ||
//     ((Math.Abs(myrx - lastRX) > 3 && Math.Abs(myry - lastRY) > 3 && Math.Abs(myrz - lastRZ) > 3) && (Math.Abs(myrx - lastRX) > 6 || Math.Abs(myry - lastRY) > 6 || Math.Abs(myrz - lastRZ) > 6)))
//                {
//            lastX = this.transform.position.x;
//            lastY = this.transform.position.y;
//            lastZ = this.transform.position.z;
//            lastRX = myrx;
//            lastRY = myry;
//            lastRZ = myrz;
//            this.transform.rotation = Quaternion.Euler(lastRX, lastRY, lastRZ);
//            this.transform.position = new Vector3(lastX, lastY, lastZ);
//            Debug.Log("change");
//        }
//        else//模型抖动范围过小时，修正模型坐标为上一次正确的坐标  
//        {

//            this.transform.rotation = Quaternion.Euler(lastRX, lastRY, lastRZ);
//            this.transform.position = new Vector3(lastX, lastY, lastZ);
//            Debug.Log("fix");
//        }

//    }
//    void HideObjects(Transform trans)
//    {
//        for (int i = 0; i < trans.childCount; i++)
//            HideObjects(trans.GetChild(i)); if (transform != trans)
//            gameObject.SetActive(false);
//    }
//    void ShowObjects(Transform trans)
//    {
//        for (int i = 0; i < trans.childCount; i++)
//            ShowObjects(trans.GetChild(i)); if (transform != trans)
//            gameObject.SetActive(true);
//    }
//    void OnTargetFound(TargetAbstractBehaviour behaviour)
//    {
//        ShowObjects(transform);
//        Debug.Log("Found: " + Target.Id);
//    }
//    void OnTargetLost(TargetAbstractBehaviour behaviour)
//    {
        
//        HideObjects(transform);
//        Debug.Log("Lost: " + Target.Id);
//    }
//    void OnTargetLoad(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
//    {
//        Debug.Log("Load target (" + status + "): " + Target.Id + " (" + Target.Name + ")  -> " + tracker);
//    }
//    void OnTargetUnload(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
//    {
//        Debug.Log("Unload target (" + status + "): " + Target.Id + " (" + Target.Name + ")  -> " + tracker);
//    }
//}
