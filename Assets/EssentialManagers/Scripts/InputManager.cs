using UnityEngine;

namespace EssentialManagers.Scripts
{
    public class InputManager : MonoBehaviour
    {
        public event System.Action TouchStartEvent;
        public event System.Action TouchEndEvent;

        public void OnPointerDown()
        {
            TouchStartEvent?.Invoke();
        }

        public void OnPointerUp()
        {
            TouchEndEvent?.Invoke();
        }
    }
}
