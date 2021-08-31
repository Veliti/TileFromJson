[System.Serializable]
public class Tile
{
    public enum TileType { Texture }

    public string Id;

    public string Type;

    public float Width;

    public float Height;

    public float X;

    public float Y;
}

[System.Serializable]
public class TileData
{
    public Tile[] List;
}
