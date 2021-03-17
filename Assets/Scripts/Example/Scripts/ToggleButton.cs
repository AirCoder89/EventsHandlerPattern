using System;
using EventsHandler;
using UnityEngine;
using UnityEngine.UI;

namespace Example.Scripts
{
    [System.Serializable]
    public struct ToggleButtonData
    {
        public Color background;
        public Color label;
    }
    
    public enum ToggleBtnEvents
    {
        OnClick, OnSelect, OnUnselect
    }
    
    [RequireComponent(typeof(Button))]
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] private PlayerControl targetControl;
        
        [Header("Internal References")] 
        [SerializeField] private Text label;
        [SerializeField] private Image icon;
        
        [Header("Colors")] 
        public ToggleButtonData selectedColor;
        public ToggleButtonData unselectedColor;
        
        private Button _btn;
        private Button button
        {
            get
            {
                if (_btn == null) _btn = GetComponent<Button>();
                return _btn;
            }
        }

        private Image _bg;
        private Image _background
        {
            get
            {
                if (_bg == null) _bg = GetComponent<Image>();
                return _bg;
            }
        }
        
        private EventsManager _manager;
        private EventsManager _eventsManager 
            => _manager ?? (_manager = new EventsManager(this));

        public void Initialize()
        {
            button.onClick.AddListener(() =>
            {
                _eventsManager.InvokeEvent(ToggleBtnEvents.OnClick);
            });
            Unselect();
        }

        public void Select()
        {
            button.interactable = false;
            _background.color = selectedColor.background;
            label.color = icon.color = selectedColor.label;
            _eventsManager.InvokeEvent(ToggleBtnEvents.OnSelect, targetControl);
        }

        public void Unselect()
        {
            button.interactable = true;
            _background.color = unselectedColor.background;
            label.color = icon.color = unselectedColor.label;
            _eventsManager.InvokeEvent(ToggleBtnEvents.OnUnselect, targetControl);
        }
    }
}