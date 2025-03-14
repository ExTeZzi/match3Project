using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Match3/Item")]
public class Item : ScriptableObject
{
    public Sprite sprite;

    public int value;
}
