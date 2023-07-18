using Game.Models;
using System;

public static class EventManager
{
    //Put your events here.

    public static readonly Event<int> IntegerEvent = new Event<int>();
    public static readonly Event<CreamData> OnCreamItemSelected = new();
}