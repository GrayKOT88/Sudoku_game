using UnityEngine;

namespace NewScripts
{
    public class LevelManager : MonoBehaviour
    {
        private const int GRID_SIZE = 9;        

        public int[,] GenerateNewLevel(int level)
        {
            int[,] grid = new int[GRID_SIZE, GRID_SIZE];
            int[,] generatedGrid = Generator.GeneratePuzzle((Generator.DifficultyLevel)(level / 100));
            
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    grid[i, j] = generatedGrid[i, j];
                }
            }

            return grid;
        }

        public void SaveLevel(int[,] grid, int level)
        {
            string arrayString = "";
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    arrayString += grid[i, j].ToString() + ",";
                }
            }
            arrayString = arrayString.TrimEnd(',');
            PlayerPrefs.SetInt("Level", level);
            PlayerPrefs.SetString("Grid", arrayString);
        }

        public int[,] LoadCurrentLevel()
        {
            int[,] grid = new int[GRID_SIZE, GRID_SIZE];
            string arrayString = PlayerPrefs.GetString("Grid");
            string[] arrayValue = arrayString.Split(',');
            int index = 0;
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    grid[i, j] = int.Parse(arrayValue[index]);
                    index++;
                }
            }
            return grid;
        }
    }
}