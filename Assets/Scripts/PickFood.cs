using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PickFood : MonoBehaviour
{
    // Start is called before the first frame update
    List<GameObject> pickfood_add_gameobject = new List<GameObject>();//这个类新增的游戏对象list，用于销毁
    public bool is_active;
    //public DragUI dragUI;
    void Start()
    {
        is_active = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButtonDown(1))
        //{
        //    if (!GlobalFunctions.PrepareFoodQTE.is_active)
        //        GlobalFunctions.PrepareFoodQTE.ActiveQTE("PotatoQTE", 30);
        //    else
        //        GlobalFunctions.PrepareFoodQTE.DeactiveQTE();
        //}
    }

    public void ActivePickFood(List<Item> inventory)
    {
        pickfood_add_gameobject.Clear();
        gameObject.SetActive(true);
        is_active = true;
        int i = 1;
        foreach (Item item in inventory)
        {
            string name = item.name;
            int num = item.num;
            print(name);
            print(num);
            
            GameObject food_sprite = new GameObject();
            food_sprite.layer = 5;
            food_sprite.name = name;
            food_sprite.AddComponent<DragUI>();
            food_sprite.AddComponent<Image>();
            food_sprite.GetComponent<Image>().sprite = Resources.Load<Sprite>(name);
            
            food_sprite.transform.parent = gameObject.transform;

            int x = i / 3;
            int y = i % 3 - 1;
            food_sprite.transform.SetPositionAndRotation(GlobalFunctions.foodBsaketBlanks[x,y].position, GlobalFunctions.foodBsaketBlanks[x, y].rotation);
            food_sprite.GetComponent<RectTransform>().sizeDelta = GlobalFunctions.foodBsaketBlanks[x, y].scale;
            
            pickfood_add_gameobject.Add(food_sprite);
           
            i++;
         
        }
    }
    public void DeactivePickFood()
    {
        gameObject.SetActive(false);
        is_active = false;

        foreach(GameObject gameObject in pickfood_add_gameobject)
        {
            Destroy(gameObject);
        }
        
    }

}
