using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour {
    private SceneController_Playing sceneController;
    private Vector3[] itemPositions = {
        new Vector3(4, 1, 4),
        new Vector3(0, 1, 4),
        new Vector3(-4, 1, 4),
        new Vector3(4, 1, 0),
        new Vector3(0, 1, 0),
        new Vector3(-4, 1, 0),
        new Vector3(4, 1, -4),
        new Vector3(0, 1, -4),
        new Vector3(-4, 1,-4),
    };
    private bool[] hasItem =
    {
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false
    };
    private int currentItemNumber = 0;

    public float ItemProbability = 0.5f;
    public float RocketProbability = 0.25f;
    public float ShieldProbability = 0.25f;
    public float TrapProbability = 0.25f;
    public float ArmsProbability = 0.25f;
    public float DecreasingProbability = 0.1f;  //根据已有的道具数动态调节产生道具的概率

    void Start () {
        sceneController = (SceneController_Playing)SSDirector.getInstance().currentSceneController;

        for(int i = 0; i < 5; i++)
        {
            float sieve = Random.Range(0f, 1f);  //筛子决定是否生成道具
            Debug.Log(sieve);
            if (sieve < ItemProbability)
            {
                sieve = Random.Range(0f, 1f);    //重新掷筛子决定生成道具的类型
                if (sieve < RocketProbability)
                {
                    createItem("Rocket");
                }
                else if (sieve < ShieldProbability + RocketProbability)
                {
                    createItem("Shield");
                }
                else if (sieve < TrapProbability + ShieldProbability + RocketProbability)
                {
                    createItem("Trap");
                }
                else if (sieve < ArmsProbability + TrapProbability + ShieldProbability + RocketProbability)
                {
                    createItem("Arms");
                }
            }
            if (ItemProbability == 0f) break;
        }
    }
	
    public void createItem(string type)
    {
        int sieve = Mathf.FloorToInt(Random.Range(0f, 8.999999f));  //随机选择一个位置
        if(hasItem[sieve])   //如果该位置已有道具，则重新选择
        {
            createItem(type);
            return;
        }
        //获取道具，并调整好位置、大小
        GameObject item = sceneController.currentObjectFactory.getItem(type);
        item.transform.SetParent(this.gameObject.transform);
        item.transform.localPosition = itemPositions[sieve];
        item.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        hasItem[sieve] = true;
        //调整产生道具的概率
        currentItemNumber += 1;
        ItemProbability -= DecreasingProbability;
        if (ItemProbability < 0f)
        {
            ItemProbability = 0f;
        }
    }
}
