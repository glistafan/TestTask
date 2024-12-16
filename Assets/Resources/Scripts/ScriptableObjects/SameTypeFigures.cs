using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SameTypeFigures", order = 3)]
    public class SameTypeFigures : ScriptableObject
    {
        [SerializeField] FigureProperties[] figureProperties;
        public FigureProperties[] FigureProperties => figureProperties;
    }

}