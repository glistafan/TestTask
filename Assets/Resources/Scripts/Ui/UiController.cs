using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Ui
{
    public class UiController : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Text taskTextUI;

        public void EnableRestartUi()
        {
            var sequence = backgroundImage.DOFade(0.8f, 1);
            sequence.OnComplete(() => restartButton.gameObject.SetActive(true));
        }

        public Tween UseLoadUi(float endValue, float duration)
        {
            var animation = backgroundImage.DOFade(endValue, duration);
            return animation;
        }
        public void SetTaskText(string taskText)
        {
            taskTextUI.text = "Find " + taskText;
            taskTextUI.DOFade(1, 3);
        }
        public void SetDefaultTaskText()
        {
            taskTextUI.color = new Color(0, 0, 0, 0);
        }
    }
}