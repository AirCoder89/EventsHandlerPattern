using System;
using UnityEngine;

namespace EventsHandler
{
    public class EventsManager
    {
        private readonly object _target;

        public EventsManager(object inTarget)
        {
            this._target = inTarget;
        }
        
        public void AddEventListener<T>(T inEvent, EventSystem.EventHandler inHandler) where T : Enum
            =>  EventSystem.AddEventListener<T>((int)(object)inEvent, inHandler);
        
        public void RemoveEventListener<T>(T inEvent, EventSystem.EventHandler inHandler) where T : Enum
            =>  EventSystem.RemoveEventListener<T>((int)(object)inEvent,inHandler);
        
        public void RemoveEventListener<T>(T inEvent) where T : Enum
            =>  EventSystem.RemoveEventListener<T>((int)(object)inEvent);

        public void InvokeEvent<T>(T inEvent, params object[] inParams) where T : Enum
            => EventSystem.InvokeEvent<T>((int)(object)inEvent, _target, _target.GetType(), inParams);
    }
}
