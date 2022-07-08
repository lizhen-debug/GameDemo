using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public string name;
    public int gold;
    public int num;

    public Inventory(string name = "", int gold = 0, int num = 0)
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
    public Dictionary<string, int> ingredients;

    public Recipe(string name = "", int fullness = 0, int spirit = 0, Dictionary<string, int> ingredients = null)
    {
        this.name = name;
        this.fullness = fullness;
        this.spirit = spirit;
        this.ingredients = ingredients;
    }
}
