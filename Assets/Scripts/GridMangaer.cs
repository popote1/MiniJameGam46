using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Splines;
public class GridMangaer: MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    [SerializeField] TileBase demolishTile;

    [SerializeField] int mapHeight;
    [SerializeField] int mapWidth;

    [SerializeField] int waterLevel;

    [SerializeField] TileBase placeableSquare;

    [SerializeField] List<Building> buildings;
    List<Building> buildableBuildíngs = new List<Building>();
    List<Cell.TileType> noDemolish = new List<Cell.TileType> { Cell.TileType.Air, Cell.TileType.Ground, Cell.TileType.Church };

    [SerializeField] Tilemap tilemap;
    [SerializeField] Tilemap otherMap;

    Building selectedBuilding;

    List<Vector3Int> demolishTileLocations = new List<Vector3Int>();
    private Cell _currenHoverCell;



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
                            newCell.currentBuilding = new Warehouse();
                            break;
                        case Cell.TileType.Farm:
                            newCell.currentBuilding = new Farme();
                            break;
                        case Cell.TileType.Sawmill:
                            newCell.currentBuilding = new Sawmill();
                            break;
                        case Cell.TileType.Church:
                            newCell.currentBuilding = new Church();
                            break;
                        case Cell.TileType.MerchantDock:
                            newCell.currentBuilding = new MerchantDocks();
                            break;
                        case Cell.TileType.FishDocks:
                            newCell.currentBuilding = new FishDocks();
                            break;
                        case Cell.TileType.Infirmary:
                            newCell.currentBuilding = new Infirmary();
                            break;
                        case Cell.TileType.BigHouse:
                            newCell.currentHouse = new House { CitizenCount = 4, _taxeByCitizens = 3 };
                            break;
                        case Cell.TileType.LittleHouse:
                            newCell.currentHouse = new House();
                            break;
                        default:
                            break;
                    }
                    if (newCell.currentBuilding != null)
                    {
                        newCell.currentBuilding.cell = newCell;
                        newCell.currentBuilding.OnCreate();
                        //Debug.Log(cellGrid[mousePos.x, mousePos.y].currentBuilding.cell.position);
                    }
                    else if (newCell.currentHouse != null)
                    {
                        newCell.currentHouse.cell = newCell;
                        newCell.currentHouse.OnCreate();
                    }
                    //Debug.Log(newCell.currentBuilding);
                }

                newCell.position[0] = i;
                newCell.position[1] = j;
                newCell.gridManager = this;
                cellGrid[i, j] = newCell;
                //Debug.Log(newCell.position[0] + "," + newCell.position[1] + " | " + newCell.type);
            }
        }
    }

    //public void CycleBuilding(int direction)
    //{
    //    if (OLDSelectedBuilding == 0 && direction == -1)
    //    {
    //        OLDSelectedBuilding = buildableBuildíngs.Count - 1;
    //    }
    //    else if (OLDSelectedBuilding == buildableBuildíngs.Count - 1 && direction == 1)
    //    {
    //        OLDSelectedBuilding = 0;
    //    }
    //    else
    //    {
    //        OLDSelectedBuilding += direction;
    //    }
    //    text.text = buildableBuildíngs[OLDSelectedBuilding].name;
    //    DetectPlaceableSquares();
    //}

    public void SelectBuilding()
    {
        StaticEvent.DoStartBuilding(null);
        selectedBuilding = null;
        DetectPlaceableSquares();
    }

    public void SelectBuilding(Cell.TileType toBuild, BuildingCost cost)
    {
        foreach(Building building in buildableBuildíngs)
        {
            if(building.type == toBuild)
            {
                selectedBuilding = building;
            }
        }
        if (selectedBuilding.tile == null)
        {
            StaticEvent.DoStartBuilding(null);
        }
        else
        {
            StaticEvent.DoStartBuilding(selectedBuilding);
        }
        selectedBuilding.woodCost = cost.WoodCost;
        selectedBuilding.goldCost = cost.GoldCost;
        DetectPlaceableSquares();
    }
    
    void DetectPlaceableSquares()
    {
        
        if (selectedBuilding == null)
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
            foreach (Vector3Int tile in demolishTileLocations)
            {
                otherMap.SetTile(tile, null);
            }
        }
        
        else if (selectedBuilding.tile == null)
        {
            for (int i = 0; i < cellGrid.GetLength(0); i++)
            {
                for (int j = 1; j < cellGrid.GetLength(1); j++)
                {
                    if (cellGrid[i, j].type == Cell.TileType.PlaceableSquare)
                    {
                        ReplaceTile(null, new Vector3Int(i, j));
                    }
                    else if ((cellGrid[i, j].currentBuilding != null && cellGrid[i, j].type != Cell.TileType.Church))
                    {
                        otherMap.SetTile(new Vector3Int(i, j), demolishTile);
                        demolishTileLocations.Add(new Vector3Int(i, j));
                    }
                    else if (cellGrid[i, j].currentHouse != null)
                    {
                        otherMap.SetTile(new Vector3Int(i, j), demolishTile);
                        demolishTileLocations.Add(new Vector3Int(i, j));
                    }
                }
            }
        }
        else if (!selectedBuilding.requiresWater)
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
            foreach (Vector3Int tile in demolishTileLocations)
            {
                otherMap.SetTile(tile, null);
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
            foreach (Vector3Int tile in demolishTileLocations)
            {
                otherMap.SetTile(tile, null);
            }
        }
        ReadMap(false);
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3Int mousePos = tilemap.WorldToCell(mainCamera.ScreenToWorldPoint(Mouse.current.position.value));
            if (0 <= mousePos.x && mousePos.x < cellGrid.GetLength(0) && 0 <= mousePos.y && mousePos.y < cellGrid.GetLength(1)) {
                
                if (selectedBuilding != null)
                {
                    Building toBuild = selectedBuilding;
                    //Debug.Log(mousePos);
                    if (cellGrid[mousePos.x, mousePos.y].type == Cell.TileType.PlaceableSquare)
                    {
                        ReplaceTile(toBuild.tile, mousePos);
                        ReadMap(false);
                        StaticData.ChangeGoldValue(-selectedBuilding.goldCost);
                        StaticData.ChangeWoodValue(-selectedBuilding.woodCost);
                        StaticEvent.DoPlayCue(new StructCueInformation(new Vector2(mousePos.x, mousePos.y), StructCueInformation.CueType.Building, toBuild.type));
                        SelectBuilding();
                        DetectPlaceableSquares();

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
                            case Cell.TileType.FishDocks:
                                cellGrid[mousePos.x, mousePos.y].currentBuilding = new FishDocks();
                                break;
                            case Cell.TileType.Infirmary:
                                cellGrid[mousePos.x, mousePos.y].currentBuilding = new Infirmary();
                                break;
                            case Cell.TileType.MerchantDock:
                                cellGrid[mousePos.x, mousePos.y].currentBuilding = new MerchantDocks();
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
                    else if (!noDemolish.Contains(cellGrid[mousePos.x, mousePos.y].type) && toBuild.tile == null && (cellGrid[mousePos.x, Mathf.Clamp(mousePos.y + 1, 0, cellGrid.GetLength(1) - 1)].type == Cell.TileType.Air || mousePos.y == cellGrid.GetLength(1) - 1))
                    {
                        ReplaceTile(toBuild.tile, mousePos);
                        StaticEvent.DoPlayCue(new StructCueInformation(new Vector2(mousePos.x, mousePos.y), StructCueInformation.CueType.Destroy, toBuild.type));
                        SelectBuilding();
                        if (cellGrid[mousePos.x, mousePos.y].currentBuilding != null)
                        {
                            //Debug.Log(mousePos);
                            Debug.Log(cellGrid[mousePos.x, mousePos.y].currentBuilding);
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
                else if (cellGrid[mousePos.x, mousePos.y].type == Cell.TileType.MerchantDock)
                {
                    StaticEvent.DoOpenMerchant(cellGrid[mousePos.x, mousePos.y].currentBuilding);
                }
            }
        }
        if(Mouse.current.rightButton.wasPressedThisFrame)
        {
            SelectBuilding();
        }
        ManagerOnHover();
    }

    public void ReplaceTile(TileBase newTile, Vector3Int coordinates)
    {
        tilemap.SetTile(coordinates, newTile);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < cellGrid.GetLength(0); i++)
        {
            for (int j = 1; j < cellGrid.GetLength(1); j++)
            {
                if (cellGrid[i, j].currentHouse != null)
                {
                    cellGrid[i, j].currentHouse.OnRemove();
                }
                if (cellGrid[i, j].currentBuilding != null)
                {
                    cellGrid[i, j].currentBuilding.OnRemove();
                }
            }
        }
    }

    private void ManagerOnHover()
    {
        Vector3Int mousePos = tilemap.WorldToCell(mainCamera.ScreenToWorldPoint(Mouse.current.position.value));
        if (0 <= mousePos.x 
            && mousePos.x < cellGrid.GetLength(0) 
            && 0 <= mousePos.y 
            && mousePos.y < cellGrid.GetLength(1)
            && selectedBuilding==null)
            
        {
            if (cellGrid[mousePos.x, mousePos.y] == _currenHoverCell) return;
            _currenHoverCell = cellGrid[mousePos.x, mousePos.y];
            StaticEvent.DoHoverCell(_currenHoverCell);
        }
        else {
            StaticEvent.DoHoverCell(_currenHoverCell);
        }
           
    }
    
    
}
