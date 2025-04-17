using Data;
using EssentialManagers.CustomPackages.SlotSystem.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour
{
    [Header("Debug")] [SerializeField] private IconDataWrapper data;

    public void SetIconData(IconDataWrapper idw)
    {
        data = idw;
        GetComponentInChildren<Image>().sprite = data.iconSprite;
        GetComponent<SlotUser>().SetEnum(data.iconEnum);
    }
}