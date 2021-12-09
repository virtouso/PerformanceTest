using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventoryManager : MonoBehaviour
{
    [Inject] private IGeneralSettings _generalSettings;
    [Inject] private IInventoryPool _inventoryPool;
    [SerializeField] private InventoryInfoPanel InfoPanel;
    [SerializeField] private InventoryItem InventoryItemPrefab;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private GameObject Container;


    [Multiline] [SerializeField] private string ItemJson;
    [SerializeField] private int ItemGenerateScale = 10;
    [SerializeField] private Sprite[] Icons;

   
    private InventoryItemData[] ItemDatas;
    
    
    private InventoryItemData[] GenerateItemDatas(string json, int scale)
    {
        var itemDatas = JsonUtility.FromJson<InventoryItemDatas>(json).ItemDatas;
        var finalItemDatas = new InventoryItemData[itemDatas.Length * scale];
        for (var i = 0; i < itemDatas.Length; i++)
        {
            for (var j = 0; j < scale; j++)
            {
                finalItemDatas[i + j * itemDatas.Length] = itemDatas[i];
            }
        }

        return finalItemDatas;
    }

    private IInventoryItem _selectedItem;
    private void InventoryItemOnClick(IInventoryItem itemClicked, InventoryItemData itemData)
    {
        if (_selectedItem != null)
            _selectedItem.UpdateColor(_generalSettings.UnselectedColor);

        itemClicked.UpdateColor(_generalSettings.SelectedColor);
        _selectedItem = itemClicked;
    }
    
    private void GetDataAndGenerateList()
    {
        ItemDatas = GenerateItemDatas(ItemJson, ItemGenerateScale);
        foreach (InventoryItemData itemData in ItemDatas)
        {
            var newItem = _inventoryPool.GetInventoryItem();
            newItem.Init(Icons[itemData.IconIndex], itemData.Name, Container.transform,
                () => { InventoryItemOnClick(newItem, itemData); });
        }
    }
    
    private void OnPlayerScroll(Vector2 position)
    {
       Debug.Log(position);
    }
    

    #region Unity Callbacks

    void Start()
    {
       GetDataAndGenerateList();
       _scrollRect.onValueChanged.AddListener(OnPlayerScroll);
    }
    
    #endregion
    
}