using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/IconData", order = 1)]
    public class IconSo : ScriptableObject
    {
        public List<IconDataWrapper> iconDataWrappers;
    }
}