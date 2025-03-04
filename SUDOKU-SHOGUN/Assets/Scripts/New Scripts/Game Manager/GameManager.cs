using UnityEngine;

namespace NewScripts
{
    public class GameManager : MonoBehaviour
    {
        private GridManager gridManager;
        private LevelManager levelManager;
        private UIManager uiManager;
        private InputManager inputManager;
        private GameStateManager gameStateManager;

        private void Start()
        {
            gridManager = GetComponent<GridManager>();
            levelManager = GetComponent<LevelManager>();
            uiManager = GetComponent<UIManager>();
            inputManager = GetComponent<InputManager>();
            gameStateManager = GetComponent<GameStateManager>();

            int[,] puzzleGrid = new int[GridManager.GRID_SIZE, GridManager.GRID_SIZE];
            int level = PlayerPrefs.GetInt("Level", 0);

            if (level == 0)
            {
                puzzleGrid = levelManager.GenerateNewLevel(1);
                levelManager.SaveLevel(puzzleGrid, 1);
                level = 1;
            }
            else
            {
                puzzleGrid = levelManager.LoadCurrentLevel();
            }

            uiManager.InitializeUI(level);
            gridManager.InitializeGrid(puzzleGrid);

            inputManager.Initialize(gridManager, gameStateManager);
            gameStateManager.Initialize(gridManager, levelManager);
        }

        public void RestartCurrentLevel()
        {
            int level = PlayerPrefs.GetInt("Level", 0);
            levelManager.SaveLevel(levelManager.GenerateNewLevel(level), level);
            gameStateManager.RestartGame();
        }

        public void StartGameWith1Level()
        {
            levelManager.SaveLevel(levelManager.GenerateNewLevel(1), 1);
            gameStateManager.RestartGame();
        }        
    }
}