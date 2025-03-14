using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    public int x;
    public int y;

    private Item items;
    public Item Item
    { get => items;
        set
        {
            if (items == value) return;
            items = value;
            icon.sprite = items.sprite;
        } 
    }


    public Image icon;
    public Button button;
    
    public Cube Left => x > 0 ? Field.Instance.Cub[x - 1, y] : null;
    public Cube Right => x < Field.Instance.Width - 1 ? Field.Instance.Cub[x + 1, y] : null;
    public Cube Top => y > 0 ? Field.Instance.Cub[x, y - 1] : null;
    public Cube Low => y < Field.Instance.Height - 1 ? Field.Instance.Cub[x, y + 1] : null;

    public Cube[] Nears => new[]
    {
        Left, Right, Top, Low
    };

    private void Start()
    {
        button.onClick.AddListener(() => Field.Instance.Choose(this));
    }

    public List<Cube> GetConnectedCub(List<Cube> expel = null)
    {
        var result = new List<Cube> { this, };
        if (expel == null) 
        { 
            expel = new List<Cube> { this, };
        }
        else
        {
            expel.Add(this);
        }
        foreach (var Near in Nears) 
        {
            if (Near == null || expel.Contains(Near) || Near.Item != Item) continue;
            result.AddRange(Near.GetConnectedCub(expel));
        }


        return result;
    }
}
