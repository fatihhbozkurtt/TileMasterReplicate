using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace EssentialManagers.Scripts
{
    public class CanvasManager : MonoBehaviour
    {
        private enum PanelType
        {
            MainMenu,
            Game,
            Success,
            Fail
        }
        

        [Header("Canvas Groups")] public CanvasGroup mainMenuCanvasGroup;
        public CanvasGroup gameCanvasGroup;
        public CanvasGroup successCanvasGroup;
        public CanvasGroup failCanvasGroup;

        [Header("Standard Objects")] public Image screenFader;
        public TextMeshProUGUI levelText;

        CanvasGroup[] canvasArray;
        [Inject] private GameManager _gameManager;
        private void Awake()
        {
            canvasArray = new CanvasGroup[System.Enum.GetNames(typeof(PanelType)).Length];

            canvasArray[(int)PanelType.MainMenu] = mainMenuCanvasGroup;
            canvasArray[(int)PanelType.Game] = gameCanvasGroup;
            canvasArray[(int)PanelType.Success] = successCanvasGroup;
            canvasArray[(int)PanelType.Fail] = failCanvasGroup;

            foreach (CanvasGroup canvas in canvasArray)
            {
                canvas.gameObject.SetActive(true);
                canvas.alpha = 0;
            }

            FadeInScreen(1f);
            ShowPanel(PanelType.MainMenu);


            // HACK: Workaround for FBSDK
            // FBSDK spawns a persistent EventSystem object. Since Unity 2020.2 there must be only one EventSystem objects at a given time.
            // So we must dispose our own EventSystem object if it exists.
            UnityEngine.EventSystems.EventSystem[] eventSystems =
                FindObjectsOfType<UnityEngine.EventSystems.EventSystem>();
            if (eventSystems.Length > 1)
            {
                Destroy(GetComponentInChildren<UnityEngine.EventSystems.EventSystem>().gameObject);
                Debug.LogWarning("There are multiple live EventSystem components. Destroying ours.");
            }
        }

        private void Start()
        {
            levelText.text = "LEVEL " + _gameManager.GetTotalStagePlayed().ToString();

            _gameManager.LevelStartedEvent += (() => ShowPanel(PanelType.Game));
            _gameManager.LevelSuccessEvent += (() => ShowPanel(PanelType.Success));
            _gameManager.LevelFailedEvent += (() => ShowPanel(PanelType.Fail));
        }

        private void ShowPanel(PanelType panelId)
        {
            int panelIndex = (int)panelId;

            for (int i = 0; i < canvasArray.Length; i++)
            {
                if (i == panelIndex)
                {
                    FadePanelIn(canvasArray[i]);
                }

                else
                {
                    FadePanelOut(canvasArray[i]);
                }
            }
        }

        #region ButtonEvents

        public void OnTapRestart()
        {
            FadeOutScreen(_gameManager.RestartStage, 1);
        }

        public void OnTapContinue()
        {
            FadeOutScreen(_gameManager.NextStage, 1);
        }

        #endregion

        #region FadeInOut

        private void FadePanelOut(CanvasGroup panel)
        {
            panel.DOFade(0, 0.75f);
            panel.blocksRaycasts = false;
        }

        private void FadePanelIn(CanvasGroup panel)
        {
            panel.DOFade(1, 0.75f);
            panel.blocksRaycasts = true;
        }

        public void FadeOutScreen(TweenCallback callback, float duration)
        {
            screenFader.DOFade(1, duration).From(0).OnComplete(callback);
        }

        public void FadeOutScreen(float duration)
        {
            screenFader.DOFade(1, duration).From(0);
        }

        public void FadeInScreen(TweenCallback callback, float duration)
        {
            screenFader.DOFade(0, duration).From(1).OnComplete(callback);
        }

        public void FadeInScreen(float duration)
        {
            screenFader.DOFade(0, duration).From(1);
        }

        #endregion
    }
}