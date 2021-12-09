using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGeneralSettings 
{
 Color SelectedColor { get; }
 Color UnselectedColor { get; }
 
 int InventoryPoolSize { get; }
 
}
