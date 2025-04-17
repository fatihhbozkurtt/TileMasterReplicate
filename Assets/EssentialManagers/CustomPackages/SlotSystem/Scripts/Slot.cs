using UnityEngine;

namespace EssentialManagers.CustomPackages.SlotSystem.Scripts
{
    public class Slot : MonoBehaviour
    {
        [Header("Debug")] public bool IsOccupied;
        [SerializeField] private SlotUser OccupierObject;
        
        public void SetOccupied(SlotUser newOccupier)
        {
            OccupierObject = newOccupier;
            IsOccupied = true;
        }
        
        public void SetFree()
        {
            OccupierObject = null;
            IsOccupied = false;
        }
        
        public SlotUser GetOccupierObject()
        {
            return OccupierObject;
        }
    }
}