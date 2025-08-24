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

    [SerializeField] TileBase placeableSquare;

    [SerializeField] List<Building> buildings;
    List<Building> buildableBuildíngs = new List<Building>();
    List<Cell.TileType> noDemolish = new List<Cell.TileType> { Cell.TileType.Air, Cell.TileType.Ground, Cell.TileType.Church };

    [SerializeField] Tilemap tilemap;

    int selectedBuilding = 0;

    public List<Cell> GetAdjacentCells(Cell origin)
    {
        List<Cell> cells = new List<Cell>();
        //Debug.Log(Mathf.Min(origin.position.y + 1, cellGrid.GetLength(1) - 1));
        cells.Add(cellGrid[origin.position.x, Mathf.Min(origin.position.y + 1, cellGrid.GetLength(1) - 1)]);
        cells.Add(cellGrid[origin.position.x, Mathf.Max(origin.position.y - 1, 0)]);
        cells.Add(cellGrid[Mathf.Max(origin.position.x - 1, 0), origin.position.y]);
        cells.Add(cellGrid[Mathf.Min(origin.position.x + 1, cellGrid.GetLength(0) - 1), origin.position.y]);
        return cells;
    }

    public Cell[,] cellGrid;
    void Start()
    {
        StaticData.ChangeFoodStockValue(50);
        StaticData.ChangeWoodStockValue(50);
        StaticData.ChangeFoodValue(50);
        StaticData.ChangeWoodValue(50);
        StaticData.ChangeGoldValue(50);
        foreach (Building building in buildings)
        {
            //Debug.Log(building.name);
            if (building.canBuild)
            {
                
                buildableBuildíngs.Add(building);
            }
        }
        cellGrid = new Cell[mapWidth, mapHeight];
        ReadMap(true);
    }

    private void ReadMap(bool initialRead)
    {
        for (int i = 0; i < cellGrid.GetLength(0); i++)
        {
            for (int j = 0; j < cellGrid.GetLength(1); j++)
            {
                Cell newCell = new Cell();
                if (!initialRead)
                {
                    newCell.currentBuilding = cellGrid[i, j].currentBuilding;
                    newCell.currentHouse = cellGrid[i, j].currentHouse;
                }

                TileBase currentTile = tilemap.GetTile(new Vector3Int(i, j));

                foreach (Building building in buildings)
                {
                    if (currentTile == building.tile)
                    {
                        newCell.type = building.type;
                        newCell.canBuildAbove = building.canBuildAbove;
                    }
                }

                if(initialRead)
                {
                    switch (newCell.type)
                    {
                        case Cell.TileType.Warehouse:
                            cellGrid[i, j].currentBuilding = new Warehouse();
                            break;
                        case Cell.TileType.Farm:
                            cellGrid[i, j].currentBuilding = new Farme();
                            break;
                        case Cell.TileType.Sawmill:
                            cellGrid[i, j].currentBuilding = new Sawmill();
                            break;
                        case Cell.TileType.Church:
                            cellGrid[i, j].currentBuilding = new Church();
                            break;
                        case Cell.TileType.BigHouse:
                            cellGrid[i, j].currentHouse = new House { CitizenCount = 4, _taxeByCitizens = 3 };
                            break;
                        case Cell.TileType.LittleHouse:
                            cellGrid[i, j].currentHouse = new House();
                            break;
                        default:
                            break;
                    }
                }

                newCell.position[0] = i;
                newCell.position[1] = j;
                newCell.gridManager = this;
                cellGrid[i, j] = newCell;
                //Debug.Log(newCell.position[0] + "," + newCell.position[1] + " | " + newCell.type);
            }
        }
    }

    public void CycleBuilding(int direction)
    {
        if (selectedBuilding == 0 && direction == -1)
        {
            selectedBuilding = buildableBuildíngs.Count - 1;
        }
        else if (selectedBuilding == buildableBuildíngs.Count - 1 && direction == 1)
        {
            selectedBuilding = 0;
        }
        else
        {
            selectedBuilding += direction;
        }
        text.text = buildableBuildíngs[selectedBuilding].name;
        DetectPlaceableSquares();
    }
    
    void DetectPlaceableSquares()
    {
        bool requiresWater = buildableBuildíngs[selectedBuilding].requiresWater;
        if (buildableBuildíngs[selectedBuilding].tile == null)
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
        else if (!requiresWater)
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
        ReadMap(false);
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3Int mousePos = tilemap.WorldToCell(mainCamera.ScreenToWorldPoint(Mouse.current.position.value));
            if (0 <= mousePos.x && mousePos.x < cellGrid.GetLength(0) && 0 <= mousePos.y && mousePos.y < cellGrid.GetLength(1))
            {
                Building toBuild = buildableBuildíngs[selectedBuilding];
                //Debug.Log(mousePos);
                if (cellGrid[mousePos.x, mousePos.y].type == Cell.TileType.PlaceableSquare && toBuild.woodCost <= StaticData.WoodStock && toBuild.foodCost <= StaticData.FoodStock && toBuild.goldCost <= StaticData.Gold)
                {
                    ReplaceTile(toBuild.tile, mousePos);
                    ReadMap(false);
                    DetectPlaceableSquares();
                    StaticData.ChangeFoodValue(-toBuild.foodCost);
                    StaticData.ChangeWoodValue(-toBuild.woodCost);
                    StaticData.ChangeGoldValue(-toBuild.goldCost);


                    switch (toBuild.type)
                    {
                        case Cell.TileType.Warehouse:
                            cellGrid[mousePos.x, mousePos.y].currentBuilding = new Warehouse();
                            break;
                        case Cell.TileType.Farm:
                            cellGrid[mousePos.x, mousePos.y].currentBuilding = new Farme();
                            break;
                        case Cell.TileType.Sawmill:
                            cellGrid[mousePos.x, mousePos.y].currentBuilding = new Sawmill();
                            break;
                        case Cell.TileType.BigHouse:
                            cellGrid[mousePos.x, mousePos.y].currentHouse = new House { CitizenCount = 4, _taxeByCitizens = 3 };
                            break;
                        case Cell.TileType.LittleHouse:
                            cellGrid[mousePos.x, mousePos.y].currentHouse = new House();
                            break;
                        default:
                            break;
                        
                    }
                    if (cellGrid[mousePos.x, mousePos.y].currentBuilding != null)
                    {
                        cellGrid[mousePos.x, mousePos.y].currentBuilding.cell = cellGrid[mousePos.x, mousePos.y];
                        cellGrid[mousePos.x, mousePos.y].currentBuilding.OnCreate();
                        //Debug.Log(cellGrid[mousePos.x, mousePos.y].currentBuilding.cell.position);
                    }
                    else if (cellGrid[mousePos.x, mousePos.y].currentHouse != null)
                    {
                        cellGrid[mousePos.x, mousePos.y].currentHouse.cell = cellGrid[mousePos.x, mousePos.y];
                        cellGrid[mousePos.x, mousePos.y].currentHouse.OnCreate();
                    }
                }
                else if (!noDemolish.Contains(cellGrid[mousePos.x, mousePos.y].type) && toBuild.tile == null && (cellGrid[mousePos.x, Mathf.Clamp(mousePos.y + 1, 0, cellGrid.GetLength(1) -   1)].type == Cell.TileType.Air || mousePos.y == cellGrid.GetLength(1) - 1) ) {
                    ReplaceTile(toBuild.tile, mousePos);
                    if (cellGrid[mousePos.x, mousePos.y].currentBuilding != null)
                    {
                        //Debug.Log(mousePos);
                        //Debug.Log(cellGrid[mousePos.x, mousePos.y].currentBuilding);
                        cellGrid[mousePos.x, mousePos.y].currentBuilding.OnRemove();
                        cellGrid[mousePos.x, mousePos.y].currentBuilding = null;
                    }
                    if (cellGrid[mousePos.x, mousePos.y].currentHouse != null)
                    {
                        cellGrid[mousePos.x, mousePos.y].currentHouse.OnRemove();
                        cellGrid[mousePos.x, mousePos.y].currentHouse = null;
                    }
                    ReadMap(false);
                }
            }
        }
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            for (int i = 0; i < cellGrid.GetLength(0); i++)
            {
                for (int j = 1; j < cellGrid.GetLength(1); j++)
                {
                    if (cellGrid[i, j].currentBuilding != null)
                    {
                        Debug.Log(i + " " + j);
                        Debug.Log(cellGrid[i, j].currentBuilding);
                    }
                }
            }
            Debug.Log(StaticData.GetCitizenCount);
            Debug.Log("done");
        }
    }

    public void ReplaceTile(TileBase newTile, Vector3Int coordinates)
    {
        tilemap.SetTile(coordinates, newTile);
    }
}
