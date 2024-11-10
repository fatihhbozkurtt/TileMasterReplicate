using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace EssentialManagers.Scripts
{
    public class GameStarter : MonoBehaviour, IPointerDownHandler
    {
        bool ready = false;
        [Inject] private GameManager _gameManager;

        private void Start()
        {
            ready = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (ready)
            {
                ready = false;
                _gameManager.StartGame();
            }
        }
    }
}