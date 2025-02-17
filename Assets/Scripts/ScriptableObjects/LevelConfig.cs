using TowerOfLondon.Structures;
using UnityEngine;

namespace TowerOfLondon.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Scriptable Objects/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public int numberOfMoves = 10;

        public Board targetBoard = new Board();
        public Board gameBoard = new Board();
    }
}
