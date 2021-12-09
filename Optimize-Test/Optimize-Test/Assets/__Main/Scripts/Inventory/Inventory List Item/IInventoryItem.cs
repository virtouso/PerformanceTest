using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInventoryItem
{
   void Init(Sprite icon, string nameText,Transform parent,UnityAction buttonAction);

   void UpdateColor(Color color);

}
