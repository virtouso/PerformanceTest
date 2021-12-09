using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// used for serialization, its cleaner to not to be nested. as its possible to be used outside of class scope
[Serializable]
public class InventoryItemDatas
{
    public InventoryItemData[] ItemDatas;
}
