using UnityEngine;

namespace Example.Scripts.Inputs
{
    public enum VehicleInputInputEvents  //events group
    {
        OnSteering, OnAccelerate, OnNitro, OnBrake
    }
    
    public class VehicleInput : BaseInput
    {
        public override void Tick()
        {
            var acceleration = Input.GetAxis("Vertical");
            var steering = Input.GetAxis("Horizontal");
            
            if(acceleration > 0f) eManager.InvokeEvent(VehicleInputInputEvents.OnAccelerate, acceleration);
            if(steering > 0f || steering < 0f) eManager.InvokeEvent(VehicleInputInputEvents.OnSteering, steering);
            if(Input.GetKeyDown(KeyCode.Space)) eManager.InvokeEvent(VehicleInputInputEvents.OnNitro);
            if(Input.GetKey(KeyCode.B)) eManager.InvokeEvent(VehicleInputInputEvents.OnBrake);
        }
    }
}