using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : MonoBehaviour, IObjectFactory {
    private static ObjectFactory _instance;

    public GameObject RocketPrefab;
    public GameObject ShieldPrefab;
    public GameObject TrapPrefab;
    public GameObject ArmsPrefab;
    private List<GameObject> usingRocket = new List<GameObject>();
    private List<GameObject> freeRocket = new List<GameObject>();
    private List<GameObject> usingShield = new List<GameObject>();
    private List<GameObject> freeShield = new List<GameObject>();
    private List<GameObject> usingTrap = new List<GameObject>();
    private List<GameObject> freeTrap = new List<GameObject>();
    private List<GameObject> usingArms = new List<GameObject>();
    private List<GameObject> freeArms = new List<GameObject>();

    void Awake()
    {
        if (_instance == null)
        {
            _instance = Singleton<ObjectFactory>.Instance;
        }
    }

	// Use this for initialization
	void Start () {
        SceneController_Playing sceneController = (SceneController_Playing)SSDirector.getInstance().currentSceneController;
        sceneController.currentObjectFactory = this;
        GameObject[] platform = GameObject.FindGameObjectsWithTag("Platform");
        foreach(GameObject p in platform)
        {
            p.GetComponent<BuildingScript>().enabled = true;
        }
    }

    public GameObject getItem(string itemType)
    {
        List<GameObject> freeList = getObjectList(itemType, "free");
        GameObject prefab = getObjectPrefab(itemType);
        GameObject item = new GameObject();
        if(freeList.Count == 0)
        {
            item = GameObject.Instantiate (prefab) as GameObject;
        }
        else
        {
            List<GameObject> usingList = getObjectList(itemType, "using");
            item = freeList[0];
            freeList.RemoveAt(0);
            usingList.Add(item);
        }
        return item;
    }

    public void freeObject(GameObject obj)
    {
        List<GameObject> usingList = getObjectList(obj.tag, "using");
        List<GameObject> freeList = getObjectList(obj.tag, "free");
        obj.SetActive(false);
        usingList.Remove(obj);
        freeList.Add(obj);
    }

    public List<GameObject> getObjectList(string itemType, string listType)
    {
        List<GameObject> list = new List<GameObject>();
        if (itemType == "Rocket") { 
            if (listType == "using") list = usingRocket;
            if (listType == "free") list = freeRocket;
        }
        else if (itemType == "Shield")
        {
            if (listType == "using") list = usingShield;
            if (listType == "free") list = freeShield;
        }
        else if (itemType == "Trap")
        {
            if (listType == "using") list = usingTrap;
            if (listType == "free") list = freeTrap;
        }
        else if (itemType == "Arms")
        {
            if (listType == "using") list = usingArms;
            if (listType == "free") list = freeArms;
        }
        return list;
    }

    public GameObject getObjectPrefab(string itemType)
    {
        GameObject item = new GameObject();
        if (itemType == "Rocket")
        {
            item = RocketPrefab;
        }
        else if (itemType == "Shield")
        {
            item = ShieldPrefab;
        }
        else if (itemType == "Trap")
        {
            item = TrapPrefab;
        }
        else if (itemType == "Arms")
        {
            item = ArmsPrefab;
        }
        return item;
    }
}
