using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public string name;
    public int gold;
    public int num;

    public Item(string name = "", int gold = 0, int num = 0)
    {
        this.name = name;
        this.gold = gold;
        this.num = num;
    }
}

public class Recipe
{
    public string name;
    public int fullness;
    public int spirit;
    public Sprite recipe_img;
    public Dictionary<string, int> ingredients;


    public Recipe(string name = "", int fullness = 0, int spirit = 0, Dictionary<string, int> ingredients = null, Sprite sprite = null)
    {
        this.name = name;
        this.fullness = fullness;
        this.spirit = spirit;
        this.ingredients = ingredients;
        this.recipe_img = sprite;
    }
}

public abstract class Info
{
    public int min, max, cur;
    public Info(int min = 0, int max = 100, int cur = 0)
    {
        this.min = min;
        this.max = max;
        this.cur = cur;
    }

    public bool increase(int val)
    {
        this.cur = Math.Min(this.cur + val, this.max);
        return true;
    }
    public bool decrease(int val)
    {
        if (this.cur - val < 0) { return false; }
        this.cur -= val;
        return true;
    }

}

public class FullnessInfo : Info
{
    public FullnessInfo(int min = 0, int max = 100, int cur = 0) : base(min, max, cur) { }
}

public class SpiritInfo : Info
{
    public SpiritInfo(int min = 0, int max = 100, int cur = 0) : base(min, max, cur) { }
}

public class ActionInfo : Info
{
    public ActionInfo(int min = 0, int max = 10, int cur = 0) : base(min, max, cur) { }
}

public class FoodBsaketBlank
{
    public Vector3 position = new Vector3();
    public Quaternion rotation = new Quaternion();
    public Vector2 scale = new Vector2();
}