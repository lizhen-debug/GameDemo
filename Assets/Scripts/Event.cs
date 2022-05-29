using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Event
{
    /* UI buttons events */
    public static void EatFood(Player player)
    {
        Debug.Log("Eat Food");
    }
    public static void FeedCat(Player player)
    {
        Debug.Log("Feed Cat");
    }
    public static void CovidTest(Player player)
    {
        Debug.Log("Covid Test");
    }
    public static void ExchangeFood(Player player)
    {
        Debug.Log("Exchange Food");
    }
    public static void Cook(Player player)
    {
        Debug.Log("Cook");
    }
    public static void Chat(Player player)
    {
        Debug.Log("Chat");
    }
    public static void ViewNews(Player player)
    {
        Debug.Log("View News");
    }
}
