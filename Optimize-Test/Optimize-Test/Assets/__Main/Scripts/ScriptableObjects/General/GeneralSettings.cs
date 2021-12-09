using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "General Settings",menuName = "Config/General Settings")]
public class GeneralSettings : ScriptableObject, IGeneralSettings
{

    [SerializeField] private Color _selectedColor;
    public Color SelectedColor => _selectedColor;

    [SerializeField] private Color _unSelectedColor;
    public Color UnselectedColor => _unSelectedColor;

    [SerializeField] private int _inventoryPoolSize;
    public int InventoryPoolSize => _inventoryPoolSize;
}
