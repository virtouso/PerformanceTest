using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private DisplayData _displayData;

    [Multiline] [SerializeField] private string ItemJson;
    [SerializeField] private int ItemGenerateScale = 10;
    [SerializeField] private Sprite[] Icons;


    private InventoryItemData[] ItemDatas;


    // i know you just needed to multiplay data to fake lots of data to make me use paginations.
    // i mean logically its bad but its for faking. just change the name to...
    private InventoryItemData[] GenerateFakeItemDatas(string json, int scale)
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

        _displayData._descriptionText.text = itemData.Description;
        _displayData._statsText.text = itemData.Stat.ToString();
        _displayData._iconImage.sprite = Icons[itemData.IconIndex];

    }

    private void GetDataAndGenerateList()
    {
        ItemDatas = GenerateFakeItemDatas(ItemJson, ItemGenerateScale);
        UpdateUiList(0);
    }

    private bool UpdateUiList(int startIndex)
    {
        Debug.Log("start is::" + startIndex);
        if (startIndex < 0) return false;

        int endIndex = startIndex + _generalSettings.InventoryPoolSize;
        Debug.Log("end is::" + endIndex);


        if (endIndex > ItemDatas.Length - 1)
            return false;

        for (int i = startIndex; i < endIndex; i++)
        {
            var newItem = _inventoryPool.GetInventoryItem();
            int index = i;
            newItem.Init(Icons[ItemDatas[index].IconIndex], ItemDatas[index].Name, Container.transform,
                delegate { InventoryItemOnClick(newItem, ItemDatas[index]); });
        }

        return true;
    }

    private int _listIndexReference = 0;

    private void OnPlayerScroll(Vector2 position)
    {
        if (position.y >= 0.99f)
        {
            Debug.Log("scroller start");
            UpdateIndex(-_generalSettings.InventoryPoolSize);
            bool updated = UpdateUiList(_listIndexReference);
            if (updated)
                _scrollRect.verticalNormalizedPosition = 0.02f;
        }
        else if (position.y <= 0.01f)
        {
            Debug.Log("scroller end");
            UpdateIndex(_generalSettings.InventoryPoolSize);
            ;
            bool updated = UpdateUiList(_listIndexReference);
            if (updated)
                _scrollRect.verticalNormalizedPosition = 0.98f;
        }
    }

    private bool _canUpdate = true;

    private void UpdateIndex(int value)
    {
        if (!_canUpdate) return;
        _listIndexReference += value;
        StartCoroutine(ResetCanUpdate());
    }

    private IEnumerator ResetCanUpdate()
    {
        _canUpdate = false;
        yield return new WaitForSeconds(1f);
        _canUpdate = true;
    }


    #region Unity Callbacks

    void Start()
    {
        GetDataAndGenerateList();
        _scrollRect.onValueChanged.AddListener(OnPlayerScroll);
    }

    #endregion

    #region Nested Models

    [System.Serializable]
    private class DisplayData
    {
        [SerializeField] public TextMeshProUGUI _statsText;
        [SerializeField] public TextMeshProUGUI _descriptionText;
        [SerializeField] public Image _iconImage;
    }

    #endregion
}