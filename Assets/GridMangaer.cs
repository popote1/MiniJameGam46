using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
public class GridMangaer: MonoBehaviour
{
    [SerializeField] int mapHeight;
    [SerializeField] int mapWidth;

    [SerializeField] TileBase ground;
    [SerializeField] TileBase BigHouse;
    [SerializeField] TileBase LittleHouse;
    [SerializeField] TileBase warehouse;
    [SerializeField] TileBase sawmill;
    [SerializeField] TileBase farm;
    [SerializeField] TileBase fishDocks;
    [SerializeField] TileBase church;
    [SerializeField] TileBase merchantDock;
    [SerializeField] TileBase infirmary;

    [SerializeField] Tilemap tilemap;

    TileBase selectedBuilding;

    Cell[,] cellGrid;
    void Start()
    {
        cellGrid = new Cell[mapWidth, mapHeight];
        ReadMap();
    }

    private void ReadMap()
    {
        for (int i = 0; i < cellGrid.GetLength(0); i++)
        {
            for (int j = 0; j < cellGrid.GetLength(1); j++)
            {
                TileBase currentTile = tilemap.GetTile(new Vector3Int(i, j));
                Cell newCell = new Cell();
                if (currentTile == ground)
                {
                    newCell.type = Cell.TileType.Ground;
                }
                else if (currentTile == BigHouse)
                {
                    newCell.type = Cell.TileType.BigHouse;
                }
                else if (currentTile == LittleHouse)
                {
                    newCell.type = Cell.TileType.LittleHouse;
                    newCell.canBuildAbove = false;
                }
                else if (currentTile == warehouse)
                {
                    newCell.type = Cell.TileType.Warehouse;
                }
                else if (currentTile == sawmill)
                {
                    newCell.type = Cell.TileType.Sawmill;
                }
                else if (currentTile == farm)
                {
                    newCell.type = Cell.TileType.Farm;
                    newCell.canBuildAbove = false;
                }
                else if (currentTile == fishDocks)
                {
                    newCell.type = Cell.TileType.FishDocks;
                    newCell.canBuildAbove = false;
                    newCell.canBuildInland = false;
                }
                else if (currentTile == fishDocks)
                {
                    newCell.type = Cell.TileType.Church;
                    newCell.canBuildAbove = false;
                }
                else if (currentTile == infirmary)
                {
                    newCell.type = Cell.TileType.Infirmary;
                    newCell.canBuildAbove = false;
                }
                else
                {
                    newCell.type = Cell.TileType.Air;
                    newCell.canBuildAbove = false;
                }
                newCell.position[0] = i;
                newCell.position[1] = j;
                cellGrid[i, j] = newCell;
                Debug.Log(newCell.position[0] + "," + newCell.position[1] + " | " + newCell.type);
            }
        }
    }

    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            selectedBuilding = BigHouse;
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            selectedBuilding = LittleHouse;
        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            //selectedBuilding = 
        }
    }

    public void ReplaceTile(TileBase newTile, Vector3Int coordinates)
    {
        tilemap.SetTile(coordinates, newTile);
        ReadMap();
    }
}
