using UnityEngine;

namespace CodeBase.Core.Infrastructure.Services.Input
{
    public class MobileInputService : IInputService
    {
        public bool IsClicked()
        {
            return UnityEngine.Input.touchCount > 0 && 
                   UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began;
        }
    }
}