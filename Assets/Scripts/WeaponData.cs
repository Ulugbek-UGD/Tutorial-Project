using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Weapon Data", fileName = "New Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _price;
    [SerializeField] private float _damage;
    
    public string Name => _name;
    public Sprite Icon => _icon;
    public int Price => _price;
    public float Damage => _damage;
}