using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Splines;

public class GridMangaer : MonoBehaviour
{
    public static GridMangaer Instance;
    [SerializeField] Camera _mainCamera;
    [SerializeField] int _mapHeight;
    [SerializeField] int _mapWidth;
    [SerializeField] int _waterLevel;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap otherMap;
    [SerializeField] private TileBase placeableSquare;
    [SerializeField] private TileBase demolishTile;
    [SerializeField] private List<Building> buildings;

    private List<Building> buildableBuildíngs = new List<Building>();

    private List<Cell.TileType> noDemolish = new List<Cell.TileType>
        { Cell.TileType.Air, Cell.TileType.Ground, Cell.TileType.Church };



    private Cell[,] _cellGrid;
    private Building _selectedBuilding;
    private List<Vector3Int> _demolishTileLocations = new List<Vector3Int>();
    private Cell _currenHoverCell;
    private List<Cell> _currentInteractibleCells = new List<Cell>();

    private InputAction _actionLeftClick;
    private InputAction _actionRightClick;

    private bool _inBuildingMode;
    private bool _inDemolitionMode;
    

    public bool InBuildingMode => _inBuildingMode;

    private void Awake()
    {
        StaticEvent.OnOrderBuilding += StaticEventOnOnOrderBuilding;
        Instance = this;
    }

    void Start()
    {
        _actionLeftClick = InputSystem.actions.FindAction("Attack");
        _actionRightClick = InputSystem.actions.FindAction("RightClic");
        _actionLeftClick.started += ActionLeftClickOnstarted;
        _actionRightClick.started += ActionRightClickOnstarted;

        StaticData.ChangeFoodStockValue(50);
        StaticData.ChangeWoodStockValue(50);
        StaticData.ChangeFoodValue(50);
        StaticData.ChangeWoodValue(50);
        StaticData.ChangeGoldValue(50);

        foreach (Building building in buildings)
        {
            if (building.canBuild)
            {
                buildableBuildíngs.Add(building);
            }
        }

        InitializeGrid();
    }

    private void InitializeGrid()
    {
        _cellGrid = new Cell[_mapWidth, _mapHeight];
        for (int x = 0; x < _mapWidth; x++)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                _cellGrid[x, y] = new Cell(x, y);
                TileBase currentTile = tilemap.GetTile(new Vector3Int(x, y));

                foreach (Building building in buildings)
                {
                    if (currentTile == building.tile)
                    {
                        building.SetUpCell(_cellGrid[x, y]);
                    }
                }
            }
        }
    }

    private void StaticEventOnOnOrderBuilding(object sender, BuildingCost e)
    {
        SelectBuilding(e);
    }

    private Cell GetCell(int x, int y)
    {
        if (x < 0 || x >= _mapWidth || y < 0 || y >= _mapHeight) return null;
        return _cellGrid[x, y];
    }

    private bool IsCellAboveWater(Cell cell)
    {
        if (cell == null) return false;
        for (int i = cell.position.y; i >= 0; i--)
        {
            if (GetCell(cell.position.x, i) == null) return true;
            if (GetCell(cell.position.x, i).type != Cell.TileType.Air) return false;
            if (i <= _waterLevel) return true;
        }

        return true;
    }

    public List<Cell> GetAdjacentCells(Cell origin)
    {
        List<Cell> cells = new List<Cell>();
        cells.Add(_cellGrid[origin.position.x, Mathf.Min(origin.position.y + 1, _cellGrid.GetLength(1) - 1)]);
        cells.Add(_cellGrid[origin.position.x, Mathf.Max(origin.position.y - 1, 0)]);
        cells.Add(_cellGrid[Mathf.Max(origin.position.x - 1, 0), origin.position.y]);
        cells.Add(_cellGrid[Mathf.Min(origin.position.x + 1, _cellGrid.GetLength(0) - 1), origin.position.y]);
        return cells;
    }

    public void SelectBuilding(BuildingCost cost)
    {
        foreach (Building building in buildableBuildíngs)
        {
            if (building.type == cost._type)
            {
                _selectedBuilding = building;
            }
        }

        if (_selectedBuilding.tile == null)
        {
            ClearInteractablecells();
            DetectDestroyableCells();
            DisplayDemolishInteractibleCells();
            StaticEvent.DoStartBuilding(null);
            _inDemolitionMode = true;
            _inBuildingMode = false;
        }
        else
        {
            ClearInteractablecells();
            DetectBuildableCells(_selectedBuilding);
            DisplayBuildableInteractibleCells();
            StaticEvent.DoStartBuilding(_selectedBuilding);
            _inDemolitionMode = false;
            _inBuildingMode = true;
        }

        _selectedBuilding.woodCost = cost.WoodCost;
        _selectedBuilding.goldCost = cost.GoldCost;
    }

    private void DetectBuildableCells(Building building)
    {
        for (int x = 0; x < _mapWidth; x++)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                if (_cellGrid[x, y].type != Cell.TileType.Air) continue;
                if (GetCell(x, y - 1) == null || !GetCell(x, y - 1).CanBeBuildOnTop()) continue;
                if (building.requiresWater)
                {
                    if (!IsCellAboveWater(GetCell(x - 1, y)) &&
                        !IsCellAboveWater(GetCell(x + 1, y)))
                        continue;
                }

                _currentInteractibleCells.Add(_cellGrid[x, y]);
            }
        }
        
    }

    private void DetectDestroyableCells() {
        for (int x = 0; x < _mapWidth; x++) {
            for (int y = 0; y < _mapHeight; y++) {
                if (_cellGrid[x, y].type == Cell.TileType.Air) continue;
                if (_cellGrid[x, y].type == Cell.TileType.Ground) continue;
                if (_cellGrid[x, y].type == Cell.TileType.Church) continue;
                if (GetCell(x, y + 1) != null && GetCell(x, y + 1).type != Cell.TileType.Air) continue;
                _currentInteractibleCells.Add(_cellGrid[x, y]);
            }
        }
    }

    private void DisplayBuildableInteractibleCells() {
        foreach (var cell in _currentInteractibleCells) {
            otherMap.SetTile(cell.position, placeableSquare);
        }
    }

    private void DisplayDemolishInteractibleCells() {
        foreach (var cell in _currentInteractibleCells) {
            otherMap.SetTile(cell.position, demolishTile);
        }
    }

    private void ClearInteractablecells() {
        _currentInteractibleCells.Clear();
        otherMap.ClearAllTiles();
    }

 
    private void ManagerNewBuilding(Cell cell)
    {
        if (_selectedBuilding != null) {
            if( _currentInteractibleCells.Contains(cell)) {
                ReplaceTile(_selectedBuilding.tile, cell.position);
                StaticData.ChangeGoldValue(-_selectedBuilding.goldCost);
                StaticData.ChangeWoodValue(-_selectedBuilding.woodCost);
                _selectedBuilding.SetUpCell(cell); 
                
                StructCueInformation cue = new StructCueInformation();
                cue.Type = StructCueInformation.CueType.Building;
                cue.TargetPosition = (Vector3)cell.position;
                StaticEvent.DoPlayCue(cue);
                
                ClearInteractablecells();
                StaticEvent.DoStartBuilding(null);
                _selectedBuilding = null;
                _inBuildingMode = false;
            }
        }
    }

    private void ManageDestroyBuilding(Cell cell) {
        if (_currentInteractibleCells.Contains(cell)) {
            ReplaceTile(null, cell.position);
            cell.DestroyBuilding();
            
            StructCueInformation cue = new StructCueInformation();
            cue.Type = StructCueInformation.CueType.Destroy;
            cue.TargetPosition = (Vector3)cell.position;
            StaticEvent.DoPlayCue(cue);
            
            ClearInteractablecells();
            StaticEvent.DoStartBuilding(null);
            _selectedBuilding =null;
            _inDemolitionMode = false;
        }
    }
    private void ActionRightClickOnstarted(InputAction.CallbackContext obj) {
        Debug.Log("ActionRightClickOnstarted");
        if (_inDemolitionMode || _inBuildingMode) {
            ClearInteractablecells();
            StaticEvent.DoStartBuilding(null);
            _selectedBuilding = null;
            _inDemolitionMode = false;
            _inBuildingMode = false;
        }
    }
    
    private void ActionLeftClickOnstarted(InputAction.CallbackContext obj) {
        Debug.Log("ActionLeftClickOnstarted");
        Vector3Int mousePos = tilemap.WorldToCell(_mainCamera.ScreenToWorldPoint(Mouse.current.position.value));
        Cell selectedCell = GetCell(mousePos.x, mousePos.y);
        if (selectedCell == null) return;
        
        if( _inBuildingMode) ManagerNewBuilding(selectedCell);
        if( _inDemolitionMode) ManageDestroyBuilding(selectedCell);

        if (selectedCell.type == Cell.TileType.MerchantDock) {
            StaticEvent.DoOpenMerchant(_cellGrid[mousePos.x, mousePos.y].currentBuilding);
        }
    }

    private void Update() {
        ManagerOnHover();
    }

    public void ReplaceTile(TileBase newTile, Vector3Int coordinates) {
        tilemap.SetTile(coordinates, newTile);
    }

    private void OnDestroy() {
        StaticEvent.OnOrderBuilding -= StaticEventOnOnOrderBuilding;
        _actionLeftClick.started-= ActionLeftClickOnstarted;
        _actionRightClick.started-= ActionRightClickOnstarted;
        for (int i = 0; i < _cellGrid.GetLength(0); i++) {
            for (int j = 1; j < _cellGrid.GetLength(1); j++) {
                if (_cellGrid[i, j].currentHouse != null) {
                    _cellGrid[i, j].currentHouse.OnRemove();
                }
                if (_cellGrid[i, j].currentBuilding != null) {
                    _cellGrid[i, j].currentBuilding.OnRemove();
                }
            }
        }
    }

    private void ManagerOnHover() {
        Vector3Int mousePos = tilemap.WorldToCell(_mainCamera.ScreenToWorldPoint(Mouse.current.position.value));
        Cell selectedCell = GetCell(mousePos.x, mousePos.y);
        if (selectedCell !=null && _selectedBuilding==null) {
            if (_cellGrid[mousePos.x, mousePos.y] == _currenHoverCell) return;
            _currenHoverCell = _cellGrid[mousePos.x, mousePos.y];
            StaticEvent.DoHoverCell(_currenHoverCell);
        }
        else {
            StaticEvent.DoHoverCell(_currenHoverCell);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLineStrip(new Vector3[]{
            new (0,0,0),
            new (_mapWidth,0,0),
            new (_mapWidth,_mapHeight,0),
            new (0,_mapHeight,0)}, true);
        Gizmos.color = Color.deepSkyBlue;
        Gizmos.DrawLine(new Vector3(-1, _waterLevel, 0), new Vector3(_mapWidth+1, _waterLevel, 0));
        
    }
}
