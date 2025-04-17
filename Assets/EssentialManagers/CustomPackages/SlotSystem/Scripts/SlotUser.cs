using System;
using Data;
using DG.Tweening;
using UnityEngine;

namespace EssentialManagers.CustomPackages.SlotSystem.Scripts
{
    public class SlotUser : MonoBehaviour
    {
        [Header("Debug")] public Slot MySlot; 
        public IconEnum slotUserEnum;

     

        public void SetEnum(IconEnum ie)
        {
            slotUserEnum = ie;
            gameObject.name = slotUserEnum + "_Tile";
        }

        private IconEnum GetRandomColor()
        {
            Array values = Enum.GetValues(typeof(IconEnum));
            return (IconEnum)values.GetValue(UnityEngine.Random.Range(1,
                values.Length));
        }

        public void OnPicked()
        {
            if (SlotManager.instance.HasEmptySlots(out Slot emptySlot))
            {
                MySlot = emptySlot;
                emptySlot.SetOccupied(this);
                SlotManager.instance.AddToColorDictionary(this);
                transform.DOMove(emptySlot.transform.position, 0.5f)
                    .OnComplete(() => SlotManager.instance.OnNewOccupierArrives(this));
            }
            else
            {
                Debug.LogWarning(SlotManager.instance.WillMergeHappen()
                    ? "Don't worry, merge is happening"
                    : "No empty slots exist!");
            }
        }

        public void PerformMergeAnimation()
        {
            float duration = 0.3f;

            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOMoveY(transform.position.y + 1f, duration).SetEase(Ease.OutQuad))
                .Join(transform.DOScale(1.25f, duration))
                .Append(transform.DOScale(Vector3.zero, duration).SetEase(Ease.InQuad))
                .OnComplete(() => Destroy(gameObject));
        }
    }
}
