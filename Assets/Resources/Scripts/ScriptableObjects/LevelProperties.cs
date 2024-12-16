using UnityEngine;

namespace Assets.Resources.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Level", order = 1)]
    public class LevelProperties : ScriptableObject
    {
        [SerializeField] private int height;
        [SerializeField] private int width;

        public int Height => height;
        public int Width => width;
    }
}