using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class DishSelection : MonoBehaviour
{
    public Button cook_btn;
    public GameObject dish_ref;
    public Transform dish_bg;
    public Vector2 dish_pos_gap;
    public Vector2 max_pos;

    string csv_file_path = Application.streamingAssetsPath + "\\cook.csv";

    // Start is called before the first frame update
    void Start()
    {
        // initialize btns
        cook_btn.onClick.AddListener(Cook);
        // read csv
        GlobalFunctions.ReadRecipeCSV(csv_file_path);
        // DisplayRecipes();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Cook()
    {
        Debug.Log("start cooking");
    }

    void DisplayRecipes()
    {
        DataTable dt = GlobalFunctions.ReadCSV(csv_file_path);
        int name = dt.Columns["name"].Ordinal;
        int fullness = dt.Columns["fullness"].Ordinal;
        int spirit = dt.Columns["spirit"].Ordinal;
        int cooking_skill = dt.Columns["cooking_skill"].Ordinal;
        int ingredient1 = dt.Columns["ingredient1"].Ordinal;
        int num1 = dt.Columns["num1"].Ordinal;
        int ingredient2 = dt.Columns["ingredient2"].Ordinal;
        int num2 = dt.Columns["num2"].Ordinal;

        Vector2 ref_pos = dish_ref.GetComponent<RectTransform>().anchoredPosition;
        Vector2 dish_pos = ref_pos;
        foreach (DataRow r in dt.Rows)
        {
            GameObject dish = Instantiate(dish_ref, dish_bg);
            dish_pos += new Vector2(dish_pos_gap.x, 0);
            if (dish_pos.x > max_pos.x)
            {
                dish_pos = new Vector2(ref_pos.x, dish_pos.y + dish_pos_gap.y); // new row
            }
            dish.GetComponent<RectTransform>().anchoredPosition = dish_pos;
        }
    }
}
