using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class InventoryPool : MonoBehaviour, IInventoryPool
{
    [Inject] private IGeneralSettings _generalSettings;
    [Inject] private InventoryItem.Factory _inventoryFactory;

    public IInventoryItem GetInventoryItem()
    {
        var item = _inventoryListItems.Dequeue();
        _inventoryListItems.Enqueue(item);
        return item;
    }

    private Queue<IInventoryItem> _inventoryListItems;

    private void Init()
    {
        _inventoryListItems = new Queue<IInventoryItem>(_generalSettings.InventoryPoolSize);
        for (int i = 0; i < _generalSettings.InventoryPoolSize; i++)
        {
            _inventoryListItems.Enqueue(_inventoryFactory.Create());
        }
    }


    private void Start()
    {
        Init();
    }
}