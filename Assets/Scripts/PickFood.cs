using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PickFood : MonoBehaviour
{
    // Start is called before the first frame update
    List<GameObject> current_recipe_used_food = new List<GameObject>();
    public bool is_active;
    public DragUI dragUI;
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

    public void ActivePickFood(Recipe recipe)
    {
        gameObject.SetActive(true);
        is_active = true;
        int i = 0;
        foreach (KeyValuePair<string, int> keyValue in recipe.ingredients)
        {
            string name = keyValue.Key;
            int num = keyValue.Value;
            print(name);
            print(num);

            GameObject food_sprite = new GameObject();
            food_sprite.layer = 5;
            food_sprite.name = name;
            food_sprite.AddComponent<DragUI>();
            food_sprite.AddComponent<Image>();
            food_sprite.GetComponent<Image>().sprite = Resources.Load<Sprite>(name);

            food_sprite.transform.parent = gameObject.transform;
            food_sprite.transform.SetPositionAndRotation(new Vector3(160+i*200, -90+540, 0), new Quaternion(0, 0, 0, 0));
            food_sprite.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);

            current_recipe_used_food.Add(food_sprite);
            i++;
            
            
        }
    }
    public void DeactivePickFood()
    {
        gameObject.SetActive(false);
        is_active = false;

        foreach(GameObject gameObject in current_recipe_used_food)
        {
            Destroy(gameObject);
        }
    }

}
