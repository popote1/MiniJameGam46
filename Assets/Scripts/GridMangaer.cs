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

    //[SerializeField] List<string> buildingNames;

    //[SerializeField] TileBase ground;
    //[SerializeField] TileBase bigHouse;
    //[SerializeField] TileBase littleHouse;
    //[SerializeField] TileBase warehouse;
    //[SerializeField] TileBase sawmill;
    //[SerializeField] TileBase farm;
    //[SerializeField] TileBase fishDocks;
    //[SerializeField] TileBase church;
    //[SerializeField] TileBase merchantDock;
    //[SerializeField] TileBase infirmary;

    [SerializeField] TileBase placeableSquare;

    [SerializeField] List<Building> buildings;
    List<Building> buildableBuildíngs = new List<Building>();
    List<Cell.TileType> noDemolish = new List<Cell.TileType> { Cell.TileType.Air, Cell.TileType.Ground, Cell.TileType.Church };

    [SerializeField] Tilemap tilemap;

    int selectedBuilding = 0;

    Cell[,] cellGrid;
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
                foreach (Building building in buildings)
                {
                    if (currentTile == building.tile)
                    {
                        newCell.type = building.type;
                        newCell.canBuildAbove = building.canBuildAbove;
                    }
                }
                newCell.position[0] = i;
                newCell.position[1] = j;
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
        ReadMap();
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
                    ReadMap();
                    DetectPlaceableSquares();
                    StaticData.ChangeFoodValue(-toBuild.foodCost);
                    StaticData.ChangeWoodValue(-toBuild.woodCost);
                    StaticData.ChangeGoldValue(-toBuild.goldCost);
                    cellGrid[mousePos.x, mousePos.y].currentBuildingObj = Instantiate(toBuild.obj, tilemap.CellToWorld(mousePos), transform.rotation);
                }
                else if (!noDemolish.Contains(cellGrid[mousePos.x, mousePos.y].type) && toBuild.tile == null && (cellGrid[mousePos.x, Mathf.Clamp(mousePos.y + 1, 0, cellGrid.GetLength(1) -   1)].type == Cell.TileType.Air || mousePos.y == cellGrid.GetLength(1) - 1) ) {
                    ReplaceTile(toBuild.tile, mousePos);
                    Destroy(cellGrid[mousePos.x, mousePos.y].currentBuildingObj.gameObject);
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
