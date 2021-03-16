using EventsHandler;
using Example.Scripts.Inputs;
using UnityEngine;
using UnityEngine.UI;

namespace Example.Scripts
{
   public enum PlayerControl { Vehicle, Humanoid }
   
    public class Example1 : MonoBehaviour
    {
       [Header("Internal References")] 
       [SerializeField] private Text logTxt;
       [SerializeField] private ToggleButton humanoidBtn;
       [SerializeField] private ToggleButton vehicleBtn;
       [SerializeField] private GameObject vehicleControl;
       [SerializeField] private GameObject humanoidControl;
      
      //- private
      private BaseInput _currentInput;
      private VehicleInput _vehicleInput;
      private HumanoidInput _humanoidInput;
      private PlayerControl _targetPlayer;
      private ToggleButton _selectedButton;
      
      private void Start()
      {
         _vehicleInput = new VehicleInput();
         _humanoidInput = new HumanoidInput();
      
         EventSystem.AddEventListener<ToggleBtnEvents>((int)ToggleBtnEvents.OnClick, OnClickToggleButton);
         EventSystem.AddEventListener<ToggleBtnEvents>((int)ToggleBtnEvents.OnSelect, OnSelectControl);
         
         humanoidBtn.Initialize();
         vehicleBtn.Initialize();
         
         //Set Humanoid control as default
         _selectedButton = humanoidBtn;
         humanoidBtn.Select(); 
         
         //subscribe To events
         _vehicleInput.AddEventListener(VehicleInputInputEvents.OnSteering, SteeringHandler);
         _vehicleInput.AddEventListener(VehicleInputInputEvents.OnAccelerate, AccelerationHandler);
         _vehicleInput.AddEventListener(VehicleInputInputEvents.OnBrake, BrakeHandler);
         _vehicleInput.AddEventListener(VehicleInputInputEvents.OnNitro, NitroHandler);
      
         _humanoidInput.AddEventListener(HumanoidInputEvents.OnMove, MoveHandler);
         _humanoidInput.AddEventListener(HumanoidInputEvents.OnAttack, AttackHandler);
         _humanoidInput.AddEventListener(HumanoidInputEvents.OnJump, JumpHandler);
      }

      private void OnSelectControl(CallbackEvent callback)
      {
         _targetPlayer = (PlayerControl) callback.Params[0];
         vehicleControl.SetActive(_targetPlayer == PlayerControl.Vehicle);
         humanoidControl.SetActive(_targetPlayer == PlayerControl.Humanoid);
      }

      private void OnClickToggleButton(CallbackEvent callback)
      {
         if(_selectedButton != null) _selectedButton.Unselect();
         _selectedButton = callback.target as ToggleButton;
         _selectedButton.Select();
      }

      #region Handeling Car Events
      private void SteeringHandler(CallbackEvent callback)
      {
         Log($"Car Steer {(float)callback.Params[0]}");
      }
      
      private void AccelerationHandler(CallbackEvent callback)
      {
         Log($"Car Accelerate {(float)callback.Params[0]}");
      }
      
      private void BrakeHandler(CallbackEvent callback)
      {
         Log($"Car Brake");
      }
   
      private void NitroHandler(CallbackEvent callback)
      {
         Log($"Car Nitro");
      }
      #endregion
   
      #region Handeling Humanoid Events
      private void MoveHandler(CallbackEvent callback)
      {
         Log($"Move {(float)callback.Params[0]}");
      }
         
      private void AttackHandler(CallbackEvent callback)
      {
         Log($"Attack !");
      }
         
      private void JumpHandler(CallbackEvent callback)
      {
         Log($"Jump !");
      }
      #endregion
   
      private void Update()
      {
         //Set input
         if (_targetPlayer == PlayerControl.Vehicle && !_vehicleInput.isEnabled) SetInput(_vehicleInput);
         else if (_targetPlayer == PlayerControl.Humanoid && !_humanoidInput.isEnabled) SetInput(_humanoidInput);
      
         _currentInput?.Tick();
      }

      private void SetInput(BaseInput inInput)
      {
         if(_currentInput == inInput) return;
         _currentInput?.Disable();
         _currentInput = inInput;
         _currentInput.Enable();
      }

      private void Log(string inTxt)
      {
         logTxt.text = $"{inTxt}\n{logTxt.text}";
      }
    }
}
