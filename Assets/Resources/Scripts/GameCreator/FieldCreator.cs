using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Resources.Scripts.GameCreator
{
    public class FieldCreator : MonoBehaviour
    {
        [SerializeField] private Cell cellTemplate;
        [SerializeField] private Grid grid;
        [SerializeField] private LevelCreator levelCreator;

        public Cell[,] CreateCells()
        {
            var levelPropertieList = levelCreator.LevelPropertieList;
            var height = levelPropertieList.Max(x => x.Height);
            var width = levelPropertieList.Max(x => x.Width);
            var cells = new Cell[width, height];

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var worldPosition = grid.GetCellCenterWorld(new Vector3Int(x, y, 0));
                    var newCell = Instantiate(cellTemplate, worldPosition, Quaternion.identity);
                    cells[x, y] = newCell;
                }
            }
            return cells;
        }

        public void ClearGame(Cell[,] cells)
        {
            if (cells != null)
            {
                foreach (var cell in cells)
                {
                    cell.DestroyCell();
                }
            }
        }
    }

}
