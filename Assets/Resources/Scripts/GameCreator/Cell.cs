using Assets.Resources.Scripts.ScriptableObjects;
using DG.Tweening;
using UnityEngine;

namespace Assets.Resources.Scripts.GameCreator
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRendererImage;
        [SerializeField] private SpriteRenderer spriteRendererBackground;
        [SerializeField] private BoxCollider2D boxCollider;
        public string Value { get; private set; }
        public Sequence AnimationSequence { get; set; }
        public Transform ImageTransform => spriteRendererImage.transform;

        public void CellInitialization(FigureProperties figureProperties)
        {
            AnimationSequence = DOTween.Sequence();
            spriteRendererImage.sprite = figureProperties.Sprite;
            Value = figureProperties.Value;
            spriteRendererImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, figureProperties.DegreeRotation));
            gameObject.SetActive(true);
        }

        public void DisableCell()
        {
            gameObject.SetActive(false);
        }

        public void SetColliderSize()
        {
            boxCollider.size = spriteRendererImage.bounds.size;
        }

        public void DestroyCell()
        {
            Destroy(gameObject);
        }
    }
}