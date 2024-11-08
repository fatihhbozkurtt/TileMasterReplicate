using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace EssentialManagers.Scripts
{
    public class TouchSurface : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public InputManager inputManager;

        [Inject]
        private void ZenjectSetup(InputManager im)
        {
            inputManager = im;
        }

        public void OnPointerDown(PointerEventData _)
        {
            Debug.Log("Pointer Down");
            inputManager.OnPointerDown();
        }

        public void OnPointerUp(PointerEventData _)
        {
            Debug.Log("Pointer Up");
            inputManager.OnPointerUp();
        }
    }
}