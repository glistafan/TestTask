using Assets.Resources.Scripts.GameController;
using Assets.Resources.Scripts.ScriptableObjects;
using Assets.Resources.Scripts.Ui;
using Assets.Resources.Scripts.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Scripts.GameCreator
{
    public class LevelCreator : MonoBehaviour
    {
        [SerializeField] private LevelProperties[] levelPropertieList;
        [SerializeField] private SameTypeFigures[] sameTypeFiguresArrays;
        [SerializeField] private UiController uiController;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private TaskTracker taskTracker;

        private Queue<LevelProperties> levelPropertieQueue = new Queue<LevelProperties>();
        private List<FigureProperties> victoriousFigures = new List<FigureProperties>();

        public LevelProperties[] LevelPropertieList => levelPropertieList;

        public void CreateLevelPropertiesQueue()
        {
            foreach (var levelPropertie in levelPropertieList)
            {
                levelPropertieQueue.Enqueue(levelPropertie);
            }
        }

        public void StartNewLevel(Cell[,] cells)
        {
            if (levelPropertieQueue.Count == 0)
            {
                uiController.EnableRestartUi();
                return;
            }
            var levelConfigurator = SetupLevel();
            CreateLevel(levelConfigurator, cells);
            StartAnimation(cells);
            StartTask(levelConfigurator.WinFigure);
        }
        private LevelConfigurator SetupLevel()
        {
            var levelProperties = levelPropertieQueue.Dequeue();
            cameraController.SetCameraPosition(levelProperties);
            var levelConfigurator = new LevelConfigurator(sameTypeFiguresArrays, levelProperties, victoriousFigures);
            return levelConfigurator;
        }

        private void CreateLevel(LevelConfigurator levelConfigurator, Cell[,] cells)
        {
            var height = levelConfigurator.Height;
            var width = levelConfigurator.Width;
            var selectedFiguresProperties = levelConfigurator.SelectedFiguresPropertiesList;

            foreach (var cell in cells) cell.DisableCell();

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var figureProperties = selectedFiguresProperties.GetRandomValue();
                    selectedFiguresProperties.Remove(figureProperties);
                    cells[x, y].CellInitialization(figureProperties);
                }
            }
        }

        private void StartAnimation(Cell[,] cells)
        {
            var animator = new LevelAnimationManager();

            if (levelPropertieQueue.Count == levelPropertieList.Length - 1)
            {
                animator.ShowFirstLevel(cells);
            }
            else
            {
                animator.ShowLevel(cells);
            }
        }

        private void StartTask(FigureProperties winFigure)
        {
            victoriousFigures.Add(winFigure);
            taskTracker.SetTask(winFigure);
        }
    }
}