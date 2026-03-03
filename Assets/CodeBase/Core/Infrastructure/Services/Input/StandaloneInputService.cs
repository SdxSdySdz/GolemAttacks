using UnityEngine;

namespace CodeBase.Core.Infrastructure.Services.Input
{
    public class StandaloneInputService : IInputService
    {
        public bool IsClicked()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Space) ||
                   UnityEngine.Input.GetKeyDown(KeyCode.KeypadEnter);
        }
    }
}