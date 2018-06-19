//using UnityEngine;

//public class NoImageTargetShow : MonoBehaviour
//{
//    public GameObject ModelOfAugmenter;
//    public GameObject ImageTarget;
//    private bool HasFound = false;

//    void Start ()
//    {
//        ModelOfAugmenter.SetActive(false);
//    }
//    void Update ()
//    {
//        if (ImageTarget.activeSelf == true)
//        {
//            HasFound = true;
//            ModelOfAugmenter.SetActive(false);
//        }
//        if (ImageTarget.activeSelf == false && HasFound == true)
//        {
//            ModelOfAugmenter.SetActive(true);
//        }
//    }
//}