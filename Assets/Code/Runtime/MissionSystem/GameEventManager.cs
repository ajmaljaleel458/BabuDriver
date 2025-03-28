using System;
using System.Collections.Generic;
using UnityEngine;

namespace BabuDriver.GameEvents
{
    public static class GameEventManager
    {
        private static Dictionary<string, Action<object>> eventDictionary = new Dictionary<string, Action<object>>();

        public static void Subscribe(string eventName, Action<object> listener)
        {
            if (!eventDictionary.ContainsKey(eventName))
                eventDictionary[eventName] = delegate { };

            eventDictionary[eventName] += listener;
        }


        public static void Unsubscribe(string eventName, Action<object> listener)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] -= listener;
                if (eventDictionary[eventName] == null)
                    eventDictionary.Remove(eventName);
            }
        }

        public static void TriggerEvent(string eventName, object eventData = null)
        {
            if (eventDictionary.ContainsKey(eventName))
                eventDictionary[eventName]?.Invoke(eventData);
        }
    }
}
