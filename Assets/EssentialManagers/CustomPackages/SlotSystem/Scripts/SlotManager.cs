using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace EssentialManagers.CustomPackages.SlotSystem.Scripts
{
    public class SlotManager : MonoSingleton<SlotManager>
    {
        [Header("Debug")] [SerializeField] private int mergeCount = 3;
        [SerializeField] private List<Slot> slots;
        private Dictionary<IconEnum, List<SlotUser>> colorOccupiers = new();

        protected override void Awake()
        {
            base.Awake();

            slots = transform.GetComponentsInChildren<Slot>().ToList();
        }
        
        /// <summary>
        /// ExampleSlotUser nesnesini renk tabanlı sözlüğe ekler ve güncellenmiş Dictionary döndürür.
        /// </summary>
        public void AddToColorDictionary(SlotUser occupier)
        {
            if (!colorOccupiers.ContainsKey(occupier.slotUserEnum))
            {
                colorOccupiers[occupier.slotUserEnum] = new List<SlotUser>();
            }

            colorOccupiers[occupier.slotUserEnum].Add(occupier);
        }
        
        /// <summary>
        /// Belirtilen kullanıcıyı sözlükten kaldırır.
        /// </summary>
        public void RemoveFromDictionary( SlotUser occupier)
        {
            if (colorOccupiers.ContainsKey(occupier.slotUserEnum))
            {
                colorOccupiers[occupier.slotUserEnum].Remove(occupier);

                if (colorOccupiers[occupier.slotUserEnum].Count == 0)
                {
                    colorOccupiers.Remove(occupier.slotUserEnum);
                }
            }
        }

        public void OnNewOccupierArrives( SlotUser occupier)
        {
            List< SlotUser> sameColoredOccupiers = GetSameColoredOccupiers(occupier.slotUserEnum);

            if (sameColoredOccupiers.Count < mergeCount) return;

            for (int i = 0; i < mergeCount; i++)
            {
                 SlotUser slotUser = sameColoredOccupiers[i];
                RemoveFromDictionary(slotUser);
                slotUser.MySlot.SetFree();
                slotUser.PerformMergeAnimation();
            }
        }

        #region BOOLEANS

        public bool HasEmptySlots(out Slot emptySlot)
        {
            emptySlot = null;
            bool hasSlots = false;
            foreach (var slot in slots)
            {
                if (slot.IsOccupied) continue;

                hasSlots = true;
                emptySlot = slot;
                break;
            }

            return hasSlots;
        }

        public bool WillMergeHappen()
        {
            foreach (var pair in colorOccupiers)
            {
                if (pair.Value.Count >= 3)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
        #region GETTERS

        private List<SlotUser> GetSameColoredOccupiers(IconEnum targetEnum)
        {
            List<SlotUser> sameColoredOccupiers = new List<SlotUser>();

            foreach (var slot in slots)
            {
                if (!slot.IsOccupied) continue;
                if (slot.GetOccupierObject().slotUserEnum == targetEnum)
                    sameColoredOccupiers.Add(slot.GetOccupierObject());
            }

            return sameColoredOccupiers;
        }

        public List<Slot> GetSlots()
        {
            return slots;
        }

        #endregion
    }
}