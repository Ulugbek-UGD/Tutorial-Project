using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCard : MonoBehaviour
{
    [SerializeField] private WeaponData _data;
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private Button _buyButton;
    
    private void OnEnable()
    {
        _buyButton.onClick.AddListener(BuyWeapon);
    }
    private void OnDisable()
    {
        _buyButton.onClick.AddListener(BuyWeapon);
    }
    
    private void Start()
    {
        DisplayWeaponData();
    }
    
    private void DisplayWeaponData()
    {
        _nameText.SetText(_data.Name);
        _iconImage.sprite = _data.Icon;
        _damageText.SetText("Damage = " +_data.Damage);
    }
    
    private void BuyWeapon()
    {
        // Write buy logic here...
        Debug.Log("We can buy "+_data.Name);
    }
}