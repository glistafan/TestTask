using Assets.Resources.Scripts.GameCreator;
using DG.Tweening;
using UnityEngine;

namespace Assets.Resources.Scripts.GameController
{
    public class LevelAnimationManager
    {
        public void ShowFirstLevel(Cell[,] cells)
        {
            var bounceSequence = DOTween.Sequence();
            foreach (var cell in cells)
            {
                if (cell.gameObject.activeSelf)
                {
                    cell.AnimationSequence = DOTween.Sequence()
                        .Append(cell.transform.DOScale(1, 2)
                        .SetEase(Ease.InBounce))
                        .OnKill(() => cell.SetColliderSize());
                }
            }
        }
        public void ShowLevel(Cell[,] cells)
        {
            foreach (var cell in cells)
            {
                if (cell.gameObject.activeSelf)
                {
                    cell.transform.localScale = new Vector3(1, 1);
                    cell.SetColliderSize();
                }
            }
        }
    }
}