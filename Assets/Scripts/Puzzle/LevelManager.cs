using System;
using TowerOfLondon.Common;
using TowerOfLondon.Configs;
using TowerOfLondon.Structures;
using UnityEngine;

namespace TowerOfLondon.Puzzle
{
    public class LevelManager : MonoBehaviour
    {
        private int _levelIndex = 0;
        private int _turns;

        private LevelConfig _levelConfig;
        [SerializeField]
        private BoardController _target;
        [SerializeField]
        private BoardController _player;

        private void Start()
        {
            NewLevel();
        }

        private void InitBoard(BoardController board, Board config)
        {
            board.InitializeBoard(config);
        }

        private LevelConfig GetLevelConfig(int value)
        {
            return ConfigManager.Instance.GetConfig(value);
        }

        public void NewLevel()
        {
            _levelIndex += 1;
            _levelConfig = GetLevelConfig(_levelIndex);

            _turns = _levelConfig.numberOfMoves;

            InitBoard(_target, _levelConfig.targetBoard);
            InitBoard(_player, _levelConfig.gameBoard);
        }
    }
}
