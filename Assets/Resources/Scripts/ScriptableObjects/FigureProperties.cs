using UnityEngine;

namespace Assets.Resources.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FigureProperties", order = 2)]
    public class FigureProperties : ScriptableObject
    {
        [SerializeField] Sprite sprite;
        [SerializeField] string value;
        [SerializeField] int degreeRotation;

        public Sprite Sprite => sprite;
        public string Value => value;
        public int DegreeRotation => degreeRotation;
    }
}