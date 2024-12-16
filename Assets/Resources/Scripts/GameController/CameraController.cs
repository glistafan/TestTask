using Assets.Resources.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Resources.Scripts.GameController
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Grid _levelGrid;
        public void SetCameraPosition(LevelProperties levelProperties)
        {
            int centerX = levelProperties.Width / 2;
            int centerY = levelProperties.Height / 2;

            var worldPositionX = levelProperties.Width % 2 == 1 ? _levelGrid.GetCellCenterWorld(new Vector3Int(centerX, centerY, -10)).x
            : _levelGrid.CellToWorld(new Vector3Int(centerX, centerY, -10)).x;

            var worldPositionY = levelProperties.Height % 2 == 1 ? _levelGrid.GetCellCenterWorld(new Vector3Int(centerX, centerY, -10)).y
            : _levelGrid.CellToWorld(new Vector3Int(centerX, centerY, -10)).y;

            Camera.main.transform.position = new Vector3(worldPositionX, worldPositionY, -10);
        }
    }
}