using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

public class Field : MonoBehaviour
{
    public static Field Instance { get; private set; }
    [SerializeField] private AudioClip Sound;
    [SerializeField] private AudioSource audioSource;

    public Row[] rows;
    public Cube[,] Cub { get; private set; }

    public int Width => Cub.GetLength(0);
    public int Height => Cub.GetLength(1);

    private readonly List<Cube> chooses = new List<Cube>();

    private const float Duration = 0.25f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Cub = new Cube[rows.Max(row => row.cube.Length), rows.Length];

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var Cube = rows[y].cube[x];
                Cube.x = x;
                Cube.y = y;
                Cube.Item = ItemBase.items[Random.Range(0, ItemBase.items.Length)];

                Cub[x, y] = Cube;
            }
        }
        Plop();
    }

    //}
    //private void Update()
    //{
    //    if (!Input.GetKeyDown(KeyCode.A)) return;
    //    foreach (var connectedCube in Cub[0, 0].GetConnectedCub()) connectedCube.icon.transform.DOScale(1.25f, Duration).Play();
    //}

    public async void Choose(Cube cube)
    {
        if (!chooses.Contains(cube))
        {
            if (chooses.Count > 0)
            {
                if (Array.IndexOf(chooses[0].Nears, cube) != -1) chooses.Add(cube);
            }
            else
            {
                chooses.Add(cube);
            }
        }

        if (chooses.Count < 2) return;

        Debug.Log($"Choose Cub at ({chooses[0].x},{chooses[0].y}) and ({chooses[1].x},{chooses[1].y}");
        await Swap(chooses[0], chooses[1]);

        if (OnPlop())
        {
            Plop();
        }
        else
        {
            await Swap(chooses[0], chooses[1]);
        }

        chooses.Clear();
    }

    public async Task Swap(Cube cube1, Cube cube2)
    {
        var icon1 = cube1.icon;
        var icon2 = cube2.icon;

        var icon1Transform = icon1.transform;
        var icon2Transform = icon2.transform;

        var follower = DOTween.Sequence();

        follower.Join(icon1Transform.DOMove(icon2Transform.position, Duration));
        follower.Join(icon2Transform.DOMove(icon1Transform.position, Duration));

        await follower.Play().AsyncWaitForCompletion();

        icon1Transform.SetParent(cube2.transform);
        icon2Transform.SetParent(cube1.transform);

        cube1.icon = icon2;
        cube2.icon = icon1;

        var cube1Item = cube1.Item;

        cube1.Item = cube2.Item;
        cube2.Item = cube1Item;
    }

    private bool OnPlop()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (Cub[x, y].GetConnectedCub().Skip(1).Count() >= 2) return true;
            }
        }
        return false;
    }
    private async void Plop()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var Cube = Cub[x, y];
                var connectedCub = Cube.GetConnectedCub();

                if (connectedCub.Skip(1).Count() < 2) continue;

                var sink = DOTween.Sequence();
                foreach (var connectedCube in connectedCub)
                {
                    sink.Join(connectedCube.icon.transform.DOScale(Vector3.zero, Duration));
                }

                audioSource.PlayOneShot(Sound);

                Score.Instance._Score += Cube.Item.value * connectedCub.Count;

                await sink.Play().AsyncWaitForCompletion();

                var insink = DOTween.Sequence();
                foreach (var connectedCube in connectedCub)
                {
                    connectedCube.Item = ItemBase.items[Random.Range(0, ItemBase.items.Length)];
                    insink.Join(connectedCube.icon.transform.DOScale(Vector3.one, Duration));
                }
                await insink.Play().AsyncWaitForCompletion();

                x = 0;
                y = 0;
            }
        }
    }
}
