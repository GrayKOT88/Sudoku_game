using System.Collections.Generic;
using UnityEngine;

namespace NewScripts
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Vector3 _startPos;
        [SerializeField] private float _offsetX, _offsetY;
        [SerializeField] private SubGrid _subGridPrefab;

        private Cell[,] cells;
        public const int GRID_SIZE = 9;
        private const int SUBGRID_SIZE = 3;

        public Cell[,] Cells => cells;

        public void InitializeGrid(int[,] puzzleGrid)
        {
            cells = new Cell[GRID_SIZE, GRID_SIZE];

            for (int i = 0; i < GRID_SIZE; i++)
            {
                Vector3 spawnPos = _startPos + i % 3 * _offsetX * Vector3.right + i / 3 * _offsetY * Vector3.up;
                SubGrid subGrid = Instantiate(_subGridPrefab, spawnPos, Quaternion.identity);
                List<Cell> subgridCells = subGrid.cells;
                int startRow = (i / 3) * 3;
                int startCol = (i % 3) * 3;
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    subgridCells[j].Row = startRow + j / 3;
                    subgridCells[j].Col = startCol + j % 3;
                    int cellValue = puzzleGrid[subgridCells[j].Row, subgridCells[j].Col];
                    subgridCells[j].Init(cellValue);
                    cells[subgridCells[j].Row, subgridCells[j].Col] = subgridCells[j];
                }
            }
        }

        public void ResetGrid()
        {
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    cells[i, j].Reset();
                }
            }
        }

        public void HighlightCell(Cell selectedCell)
        {
            int currentRow = selectedCell.Row;
            int currentCol = selectedCell.Col;
            int subGridRow = currentRow - currentRow % SUBGRID_SIZE;
            int subGridCol = currentCol - currentCol % SUBGRID_SIZE;

            for (int i = 0; i < GRID_SIZE; i++)
            {
                cells[i, currentCol].HighLight();
                cells[currentRow, i].HighLight();
                cells[subGridRow + i % 3, subGridCol + i / 3].HighLight();
            }
            cells[currentRow, currentCol].Select();
        }

        public bool IsValid(Cell cell, int value)
        {
            int row = cell.Row;
            int col = cell.Col;
                        
            for (int i = 0; i < GRID_SIZE; i++)
            {
                if (cells[row, i].Value == value || cells[i, col].Value == value)
                {
                    return false;
                }
            }
            
            int subGridRow = row - row % SUBGRID_SIZE;
            int subGridCol = col - col % SUBGRID_SIZE;

            for (int r = subGridRow; r < subGridRow + SUBGRID_SIZE; r++)
            {
                for (int c = subGridCol; c < subGridCol + SUBGRID_SIZE; c++)
                {
                    if (cells[r, c].Value == value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}