using Assets.Resources.Scripts.ScriptableObjects;
using Assets.Resources.Scripts.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Resources.Scripts.GameCreator
{
    public class LevelConfigurator
    {
        private List<FigureProperties> figurePropertiesList;
        public List<FigureProperties> SelectedFiguresPropertiesList { get; private set; }
        public FigureProperties WinFigure { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        public LevelConfigurator(SameTypeFigures[] sameTypeFiguresArrays, LevelProperties levelProperties, List<FigureProperties> victoriousFiguresProperties)
        {
            Height = levelProperties.Height;
            Width = levelProperties.Width;

            figurePropertiesList = sameTypeFiguresArrays.GetRandomValue().FigureProperties.ToList();
            WinFigure = GetWinFigure(victoriousFiguresProperties);
            SelectedFiguresPropertiesList = GetSelectedPropertiesList();
        }

        private FigureProperties GetWinFigure(List<FigureProperties> victoriousFiguresProperties)
        {
            var winFigure = figurePropertiesList.Except(victoriousFiguresProperties).ToList().GetRandomValue();

            if (winFigure == null)
            {
                //Обновляем список если фигуры закончились
                winFigure = figurePropertiesList.GetRandomValue();
                var intersection = victoriousFiguresProperties.Intersect(figurePropertiesList).ToList();
                foreach(var f in intersection)
                {
                    victoriousFiguresProperties.Remove(f);
                }


            }
            figurePropertiesList.Remove(winFigure);
            return winFigure;
        }

        private List<FigureProperties> GetSelectedPropertiesList()
        {
            var selectedFiguresPropertiesList = figurePropertiesList.GetRandomValuesList(Height * Width - 1);
            selectedFiguresPropertiesList.Add(WinFigure);
            return selectedFiguresPropertiesList;
        }
    }
}