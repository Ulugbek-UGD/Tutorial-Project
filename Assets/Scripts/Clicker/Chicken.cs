using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chicken : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ChickenData _data;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _eggText;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        _data.AddGold();
        DisplayInfo();
    }

    private void DisplayInfo()
    {
        _goldText.SetText("Gold " +_data.gold);
        _eggText.SetText("Egg " +_data.egg);
    }
}