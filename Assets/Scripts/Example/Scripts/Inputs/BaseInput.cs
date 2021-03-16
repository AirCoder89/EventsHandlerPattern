using System;
using EventsHandler;

namespace Example.Scripts.Inputs
{
    public enum BaseInputEvents //events group
    {
        OnEnabled, OnDisabled
    }
    
    public abstract class BaseInput
    {
        protected EventsManager eManager;
        public bool isEnabled { get; private set; }
        
        public BaseInput()
        {
            eManager = new EventsManager(this);
            isEnabled = false;
        }
        
        public abstract void Tick();

        public void Enable()
        {
            if(isEnabled) return;
            isEnabled = true;
            eManager.InvokeEvent(BaseInputEvents.OnEnabled);
        }

        public void Disable()
        {
            if(!isEnabled) return;
            isEnabled = false;
            eManager.InvokeEvent(BaseInputEvents.OnDisabled);
        }
        
        //- Generic methods
        public void AddEventListener<T>(T inEvent, EventSystem.EventHandler inHandler) where  T : Enum
            => eManager?.AddEventListener(inEvent, inHandler);

        public  void RemoveEventListener<T>(T inEvent, EventSystem.EventHandler inHandler) where  T : Enum
            => eManager?.RemoveEventListener(inEvent, inHandler);
        
        public  void RemoveEventListener<T>(T inEvent) where  T : Enum
            =>  eManager?.RemoveEventListener(inEvent);
    }
}