using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
public class GridMangaer: MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    [SerializeField] int mapHeight;
    [SerializeField] int mapWidth;

    [SerializeField] int waterLevel;

    [SerializeField] TMP_Text text;

    [SerializeField] List<string> buildingNames;

    [SerializeField] TileBase ground;
    [SerializeField] TileBase bigHouse;
    [SerializeField] TileBase littleHouse;
    [SerializeField] TileBase warehouse;
    [SerializeField] TileBase sawmill;
    [SerializeField] TileBase farm;
    [SerializeField] TileBase fishDocks;
    [SerializeField] TileBase church;
    [SerializeField] TileBase merchantDock;
    [SerializeField] TileBase infirmary;

    

    [SerializeField] TileBase placeableSquare;

    List<TileBase> buildings = new List<TileBase>();
    List<Cell.TileType> noDemolish = new List<Cell.TileType> { Cell.TileType.Air, Cell.TileType.Ground, Cell.TileType.Church };

    [SerializeField] Tilemap tilemap;

    int selectedBuilding = 0;

    Cell[,] cellGrid;
    void Start()
    {
        buildings = new List<TileBase> { bigHouse, littleHouse, warehouse, sawmill, farm, fishDocks, merchantDock, infirmary, null };
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
                if (ground == currentTile)
                {
                    newCell.type = Cell.TileType.Ground;
                }
                else if (currentTile == bigHouse)
                {
                    newCell.type = Cell.TileType.BigHouse;
                }
                else if (currentTile == littleHouse)
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
                }
                else if (currentTile == church)
                {
                    newCell.type = Cell.TileType.Church;
                    newCell.canBuildAbove = false;
                }
                else if (currentTile == infirmary)
                {
                    newCell.type = Cell.TileType.Infirmary;
                    newCell.canBuildAbove = false;
                }
                else if (currentTile == merchantDock)
                {
                    newCell.type = Cell.TileType.MerchantDock;
                    newCell.canBuildAbove = false;
                }
                else if (currentTile == placeableSquare)
                {
                    newCell.type = Cell.TileType.PlaceableSquare;
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

    public void CycleBuilding(int direction)
    {
        if (selectedBuilding == 0 && direction == -1)
        {
            selectedBuilding = buildings.Count - 1;
        }
        else if (selectedBuilding == buildings.Count - 1 && direction == 1)
        {
            selectedBuilding = 0;
        }
        else
        {
            selectedBuilding += direction;
        }
        text.text = buildingNames[selectedBuilding];
        DetectPlaceableSquares();
    }
    
    void DetectPlaceableSquares()
    {
        bool canBePlacedInland = buildingNames[selectedBuilding] != "Fish docks";
        Debug.Log(canBePlacedInland);
        if (buildings[selectedBuilding] == null)
        {
            for (int i = 0; i < cellGrid.GetLength(0); i++)
            {
                for (int j = 1; j < cellGrid.GetLength(1); j++)
                {
                    if (cellGrid[i, j].type == Cell.TileType.PlaceableSquare)
                    {
                        ReplaceTile(null, new Vector3Int(i, j));
                    }

                }
            }
        }
        else if (canBePlacedInland)
        {
            for (int i = 0; i < cellGrid.GetLength(0); i++)
            {
                for (int j = 1; j < cellGrid.GetLength(1); j++)
                {
                    if (cellGrid[i, j].type == Cell.TileType.Air && cellGrid[i, j - 1].canBuildAbove)
                    {
                        ReplaceTile(placeableSquare, new Vector3Int(i, j));
                    }

                }
            }
        }
        else
        {
            for (int i = 0; i < cellGrid.GetLength(0); i++)
            {
                for (int j = 1; j < cellGrid.GetLength(1); j++)
                {
                    if (cellGrid[i, j].type == Cell.TileType.Air && cellGrid[i, j - 1].canBuildAbove && j <= waterLevel)
                    {
                        ReplaceTile(placeableSquare, new Vector3Int(i, j));
                    }
                    else if (cellGrid[i, j].type == Cell.TileType.PlaceableSquare && j > waterLevel )
                    {
                        ReplaceTile(null, new Vector3Int(i, j));
                    }
                    
                }
            }
        }
        ReadMap();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3Int mousePos = tilemap.WorldToCell(mainCamera.ScreenToWorldPoint(Mouse.current.position.value));
            if (0 <= mousePos.x && mousePos.x < cellGrid.GetLength(0) && 0 <= mousePos.y && mousePos.y < cellGrid.GetLength(1))
            {
                Debug.Log(mousePos);
                if (cellGrid[mousePos.x, mousePos.y].type == Cell.TileType.PlaceableSquare)
                {
                    ReplaceTile(buildings[selectedBuilding], mousePos);
                    ReadMap();
                    DetectPlaceableSquares();
                }
                else if (!noDemolish.Contains(cellGrid[mousePos.x, mousePos.y].type) && buildings[selectedBuilding] == null && (cellGrid[mousePos.x, Mathf.Clamp(mousePos.y + 1, 0, cellGrid.GetLength(1) -   1)].type == Cell.TileType.Air || mousePos.y == cellGrid.GetLength(1) - 1) ) {
                    ReplaceTile(buildings[selectedBuilding], mousePos);
                    ReadMap();
                }
            }
        }
    }

    public void ReplaceTile(TileBase newTile, Vector3Int coordinates)
    {
        tilemap.SetTile(coordinates, newTile);
    }
}
