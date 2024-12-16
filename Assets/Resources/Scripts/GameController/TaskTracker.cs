using Assets.Resources.Scripts.GameCreator;
using Assets.Resources.Scripts.ScriptableObjects;
using Assets.Resources.Scripts.Ui;
using DG.Tweening;
using UnityEngine;

namespace Assets.Resources.Scripts.GameController
{
    public class TaskTracker : MonoBehaviour
    {
        [SerializeField] private UiController uiController;
        [SerializeField] private GameStarter gameStarter;
        [SerializeField] private ParticleSystem starParticle;
        private FigureProperties winFigure;
        private float maxDistance = 2f;
        private float maxScale = 1.5f;
        private float duration = 0.1f;
        private bool taskActive;
        public void SetTask(FigureProperties winFigureProperties)
        {
            winFigure = winFigureProperties;
            uiController.SetTaskText(winFigureProperties.Value);
            taskActive = true;
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && taskActive)
            {
                CheckFigure();
            }
        }

        private void CheckFigure()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                var cell = hit.collider.gameObject.GetComponent<Cell>();
                if (cell.Value == winFigure.Value)
                {
                    starParticle.transform.position = cell.transform.position;
                    starParticle.Play();

                    taskActive = false;
                    BounceMove(cell);
                }
                else
                {
                    LeftRightMove(cell);
                }
            }
        }

        private void BounceMove(Cell cell)
        {
            if (cell.AnimationSequence.active) return;
            var startScale = cell.ImageTransform.localScale;
            var bounceSequence = DOTween.Sequence()
                .Append(cell.ImageTransform.DOScale(maxScale, duration)
                .SetEase(Ease.InBounce)
                .SetLoops(4, LoopType.Yoyo))
                .Append(cell.ImageTransform.DOScale(startScale, duration));

            bounceSequence.OnKill(() => gameStarter.StartNewLevel());
        }

        private void LeftRightMove(Cell cell)
        {
            if (cell.AnimationSequence.active) return;

            var figureTransform = cell.ImageTransform;
            var startPosition = figureTransform.position;
            var leftFirstPosition = startPosition.x - maxDistance / 4;
            var rightFirstPosition = startPosition.x + maxDistance / 3;
            var leftSecondPosition = startPosition.x - maxDistance / 2;
            var rightSecondPosition = startPosition.x + maxDistance;

            cell.AnimationSequence = DOTween.Sequence();

            AddMove(cell.AnimationSequence, leftFirstPosition, figureTransform);
            AddMove(cell.AnimationSequence, rightFirstPosition, figureTransform);
            AddMove(cell.AnimationSequence, leftSecondPosition, figureTransform);
            AddMove(cell.AnimationSequence, rightSecondPosition, figureTransform);
            AddMove(cell.AnimationSequence, leftSecondPosition, figureTransform);
            AddMove(cell.AnimationSequence, rightFirstPosition, figureTransform);
            AddMove(cell.AnimationSequence, leftFirstPosition, figureTransform);
            AddMove(cell.AnimationSequence, startPosition.x, figureTransform);
        }
        private void AddMove(Sequence moveSequence, float targetPosition, Transform figureTransform)
        {
            moveSequence.Append(figureTransform.DOMoveX(targetPosition, duration).SetEase(Ease.InOutQuad));
        }
    }
}