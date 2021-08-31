using System;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    [SerializeField]
    private bool _loadHard;
    [SerializeField]
    private TextAsset NORMAL_JSON;
    [SerializeField]
    private TextAsset HARD_JSON;
    readonly string SPRITES_PATH = "Sprites/TileSprites/";

    public void GetTileNameClosestToLeftUp(Action<string> action)
    {
        action.Invoke(GetTileNameClosestTo(new Vector2(0f, 1f)));
    }

    private void LoadMap(string json)
    {
        var tileData = JsonUtility.FromJson<TileData>(json);
        foreach (var tile in tileData.List)
        {
            switch (Enum.Parse(typeof(Tile.TileType), tile.Type))
            {
                case Tile.TileType.Texture:
                    CreateTextureTile(tile);
                    break;
            }
        }
    }

    private void CreateTextureTile(Tile tile)
    {
        var spritePath = SPRITES_PATH + tile.Id;
        var sprite = Resources.Load<Sprite>(spritePath);

        var newgameobject = new GameObject(tile.Id);
        newgameobject.transform.position = new Vector2(tile.X, tile.Y);
        newgameobject.transform.SetParent(transform);

        var spriteRenderer = newgameobject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.size = new Vector2(tile.Width, tile.Height);
    }

    private string GetTileNameClosestTo(Vector2 viewportTarget)
    {
        var cam = Camera.main;
        var children = GetComponentsInChildren<SpriteRenderer>();
        var smallestDistance = float.MaxValue;
        SpriteRenderer closest = null;
        foreach (var tile in children)
        {
            var distance = Vector2.Distance(cam.ViewportToWorldPoint(viewportTarget), (Vector2)tile.transform.position);
            if(distance < smallestDistance)
            {
                smallestDistance = distance;
                closest = tile;
            }
        }
        return closest?.name;
    }

    private void Awake()
    {
        if(_loadHard)
            LoadMap(HARD_JSON.text);
        else
            LoadMap(NORMAL_JSON.text);
    }
}
