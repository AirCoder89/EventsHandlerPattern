using System;
using System.Collections.Generic;

namespace EventsHandler
{
    public static class EventSystem
    {
        public delegate void EventHandler(CallbackEvent callback);

        private static Dictionary<Type, Dictionary<int, EventHandler>> _eventMap;

        public static bool HasEvent<T>(int inEventIndex) where T : Enum
        {
            if (_eventMap == null || !_eventMap.ContainsKey(typeof(T))) return false;
            return _eventMap[typeof(T)].ContainsKey(inEventIndex);
        }
        
        public static void AddEventListener<T>(int inEventIndex, EventHandler inEventHandler) where T : Enum
        {
            if(_eventMap == null) _eventMap = new Dictionary<Type, Dictionary<int, EventHandler>>();
            if(!_eventMap.ContainsKey(typeof(T)))
                _eventMap.Add(typeof(T), new Dictionary<int, EventHandler>());
            if (_eventMap[typeof(T)].ContainsKey(inEventIndex))
                _eventMap[typeof(T)][inEventIndex] += inEventHandler;
            else _eventMap[typeof(T)].Add(inEventIndex, inEventHandler);
        }

        public static void RemoveEventListener<T>(int inEventIndex) where T : Enum
        {
            if(!HasEvent<T>(inEventIndex)) return;
            _eventMap[typeof(T)].Remove(inEventIndex);
        }

        public static void RemoveEventListener<T>(int inEventIndex , EventHandler inEventhandler) where T : Enum
        {
            if(!HasEvent<T>(inEventIndex) || inEventhandler == null) return;
            if (_eventMap[typeof(T)][inEventIndex] != null)
                if (_eventMap[typeof(T)].ContainsKey(inEventIndex))
                    _eventMap[typeof(T)][inEventIndex] -= inEventhandler;
        }

        public static void InvokeEvent<T>(int inEventIndex, object inTarget, Type inTargetType, params object[] inParams)
            where T : Enum
        {
            if(!HasEvent<T>(inEventIndex)) return;
            var msg = new CallbackEvent(inTarget, inParams);
            _eventMap[typeof(T)][inEventIndex]?.Invoke(msg);
        }
    }
}
