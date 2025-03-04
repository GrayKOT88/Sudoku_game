using UnityEngine;

namespace NewScripts
{
    public class GameStateManager : MonoBehaviour
    {
        private GridManager gridManager;
        private LevelManager levelManager;

        public bool HasGameFinished { get; private set; }

        public void Initialize(GridManager gridManager, LevelManager levelManager)
        {
            this.gridManager = gridManager;
            this.levelManager = levelManager;
            HasGameFinished = false;
        }

        public void CheckWin()
        {
            for (int i = 0; i < GridManager.GRID_SIZE; i++)
            {
                for (int j = 0; j < GridManager.GRID_SIZE; j++)
                {
                    if (gridManager.Cells[i, j].Value == 0 || gridManager.Cells[i, j].IsIncorrect)
                    {
                        return;
                    }
                }
            }
            HasGameFinished = true;

            for (int i = 0; i < GridManager.GRID_SIZE; i++)
            {
                for (int j = 0; j < GridManager.GRID_SIZE; j++)
                {
                    gridManager.Cells[i, j].UpdateWin();
                }
            }
            Invoke("GoToNextLevel", 2f);
        }

        private void GoToNextLevel()
        {
            int level = PlayerPrefs.GetInt("Level", 0);
            levelManager.SaveLevel(levelManager.GenerateNewLevel(level + 1), level + 1);
            RestartGame();
        }

        public void RestartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}