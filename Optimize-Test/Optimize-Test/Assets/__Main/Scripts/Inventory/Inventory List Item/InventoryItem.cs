using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class InventoryItem : MonoBehaviour, IInventoryItem
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Button _button;


    public void Init(Sprite icon, string nameText,Transform parent, UnityAction buttonAction)
    {
        _icon.sprite = icon;
        _name.text = nameText;
        transform.SetParent(parent,false);
        _button.onClick.AddListener(buttonAction);
        gameObject.SetActive(true);
    }

    public void UpdateColor(Color color)
    {
        _background.color = color;
    }



    public class Factory : PlaceholderFactory<InventoryItem>
    {
        
    }
}