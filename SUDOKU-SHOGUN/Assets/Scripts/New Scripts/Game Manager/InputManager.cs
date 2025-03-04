using UnityEngine;

namespace NewScripts
{
    public class InputManager : MonoBehaviour
    {
        private GridManager gridManager;
        private GameStateManager gameStateManager;

        private Cell selectedCell;

        public void Initialize(GridManager gridManager, GameStateManager gameStateManager)
        {
            this.gridManager = gridManager;
            this.gameStateManager = gameStateManager;
        }

        private void Update()
        {
            if (gameStateManager.HasGameFinished || !Input.GetMouseButton(0)) return;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            Cell tempCell;

            if (hit
                && hit.collider.gameObject.TryGetComponent(out tempCell)
                && tempCell != selectedCell
                && !tempCell.IsLocked
                )
            {
                gridManager.ResetGrid();
                selectedCell = tempCell;
                gridManager.HighlightCell(selectedCell);
            }
        }

        public void UpdateCellValue(int value)
        {
            if (gameStateManager.HasGameFinished || selectedCell == null) return;
            // Проверяем, можно ли поставить это число в ячейку
            if (!gridManager.IsValid(selectedCell, value))
            {
                selectedCell.IsIncorrect = true; // Помечаем ячейку как некорректную
            }
            else
            {
                selectedCell.IsIncorrect = false; // Число подходит
            }
            selectedCell.UpdateValue(value);
            gridManager.HighlightCell(selectedCell);
            gameStateManager.CheckWin();
        }
    }
}