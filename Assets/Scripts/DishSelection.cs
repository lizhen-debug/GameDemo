using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DishSelection : MonoBehaviour
{
    public GameObject recipe_bar;
    public Button cook_btn;
    public GameObject dish_ref;
    public Transform dish_bg;
    public Vector2 dish_pos_gap;
    public Vector2 max_pos;


    // Start is called before the first frame update
    void Start()
    {
        // initialize btns
        cook_btn.onClick.AddListener(Cook);
        DisplayRecipes();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Cook()
    {
        Debug.Log("start cooking");
    }

    void SetRecipeBarInfo(Recipe recipe)
    {
        GameObject recipe_name = recipe_bar.transform.Find("recipe_name").gameObject;
        GameObject dish_img = recipe_bar.transform.Find("dish_img").gameObject;
        GameObject ingredients = recipe_bar.transform.Find("ingredients").gameObject;
        recipe_name.GetComponent<TextMeshProUGUI>().text = recipe.name;
        dish_img.GetComponent<Image>().sprite = recipe.recipe_img;
        int i = 0;
        foreach(var ingredient in recipe.ingredients)
        {
            ingredients.transform.GetChild(i++).gameObject.GetComponent<TextMeshProUGUI>().text = ingredient.Key;
        }
        for (; i < ingredients.transform.childCount; i++)
        {
            ingredients.transform.GetChild(i++).gameObject.GetComponent<TextMeshProUGUI>().text = null;
        }
    }

    void DisplayRecipes()
    {
        Vector2 ref_pos = dish_ref.GetComponent<RectTransform>().anchoredPosition;
        Vector2 dish_pos = ref_pos;
        foreach (Recipe recipe in GlobalFunctions.recipe_arr)
        {
            GameObject dish = Instantiate(dish_ref, dish_bg);
            // set image and text
            dish.GetComponent<RectTransform>().anchoredPosition = dish_pos;
            dish.GetComponent<Image>().sprite = recipe.recipe_img;
            GameObject dish_name_text = dish.transform.GetChild(0).gameObject;
            dish_name_text.GetComponent<TextMeshProUGUI>().text = recipe.name;
            // set right side info bar
            dish.GetComponent<Button>().onClick.AddListener(() => SetRecipeBarInfo(recipe));

            // update pos for next recipe
            dish_pos += new Vector2(dish_pos_gap.x, 0);
            if (dish_pos.x > max_pos.x)
            {
                dish_pos = new Vector2(ref_pos.x, dish_pos.y + dish_pos_gap.y); // new row
            }
        }
        dish_ref.SetActive(false);
    }
}
