using Assets.Resources.Scripts.GameCreator;
using Assets.Resources.Scripts.Ui;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Resources.Scripts.GameController
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private FieldCreator fieldCreator;
        [SerializeField] private UiController uiController;
        [SerializeField] private LevelCreator levelCreator;

        private Cell[,] cells;

        private void Start()
        {
            StartCoroutine(StartGame());
        }
        public void RestartGame()
        {
            StartCoroutine(StartGame());
        }

        public IEnumerator StartGame()
        {
            yield return uiController.UseLoadUi(1, 1).WaitForCompletion();
            levelCreator.CreateLevelPropertiesQueue();
            CreateGameField();
            uiController.SetDefaultTaskText();
            yield return uiController.UseLoadUi(0, 1).WaitForCompletion();
            StartNewLevel();
        }
        public void StartNewLevel()
        {
            levelCreator.StartNewLevel(cells);
        }
        public void CreateGameField()
        {
            fieldCreator.ClearGame(cells);
            cells = fieldCreator.CreateCells();
        }
    }
}