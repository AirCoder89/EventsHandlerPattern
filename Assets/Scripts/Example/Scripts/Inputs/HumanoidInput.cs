using UnityEngine;

namespace Example.Scripts.Inputs
{
    public enum HumanoidInputEvents //events group
    {
        OnMove, OnJump, OnAttack
    }
    
    public class HumanoidInput : BaseInput
    {
        public override void Tick()
        {
            var move = Input.GetAxis("Horizontal");
            if(move > 0f || move < 0f) eManager.InvokeEvent(HumanoidInputEvents.OnMove, move);
            
            if(Input.GetKeyDown(KeyCode.Space)) eManager.InvokeEvent(HumanoidInputEvents.OnJump);
            if(Input.GetKeyDown(KeyCode.N)) eManager.InvokeEvent(HumanoidInputEvents.OnAttack);
        }
    }
}