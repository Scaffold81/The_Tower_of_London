using Game.Common;
using System;
using System.Linq;
using TowerOfLondon.Common;
using TowerOfLondon.Configs;
using TowerOfLondon.Structures;
using TowerOfLondon.UI;
using UnityEngine;

namespace TowerOfLondon.Puzzle
{
    public class LevelManager : MonoBehaviour
    {
        private int _levelIndex = 0;
        private int _turns;
        private int _turn;

        private LevelConfig _levelConfig;

        [SerializeField] private UITextField _turnCounter;
        [SerializeField] private UIEndLevelPanel _puzzleStatePanel;
        [SerializeField] private BoardController _target;
        [SerializeField] private BoardController _player;

        public int Turn
        {
            get { return _turn; }
            set
            {
                _turn = value;
                _turnCounter.UpdateText(_turn + "/" + _turns);
                CheckWin();
                if (value >= _turns)
                    Lose();
            }
        }

        private void Start()
        {
            NewLevel();
        }

        private void InitBoard(BoardController board, Board config)
        {
            board.InitializeBoard(config);
        }

        private void GetLevelConfig(int value)
        {
            _levelConfig = ConfigManager.Instance.GetConfig(value);
        }

        private void OnEnable()
        {
            _player.TurnOn += SetTurn;
        }

        private void OnDisable()
        {
            _player.TurnOn -= SetTurn;
        }

        private void SetTurn()
        {
            Turn++;
        }

        private void CheckWin()
        {
            bool allRingsMatch = false;

            for (int i = 0; i < _player.Pins.Count; i++)
            {
                if (_player.Pins[i].Rings.Count == _levelConfig.targetBoard.pin[i].rings.Count && _player.Pins[i].Rings.Count > 0)
                {
                    var playerRingTypes = _player.Pins[i].Rings.Select(ring => ring.RingType);
                    var targetRingTypes = _levelConfig.targetBoard.pin[i].rings.Select(ring => ring.RingType);

                    if (playerRingTypes.SequenceEqual(targetRingTypes))
                    {
                        allRingsMatch = true;
                        break;
                    }
                }
            }

            if (allRingsMatch)
            {
                Win();
            }
        }

        private void Win()
        {
            var header = "Победа";
            var info = $"{DateTime.UtcNow}\nПользователь: Иванов\nУровень: {_levelConfig.levelIndex}\nХодов: {_turn}";

            _puzzleStatePanel.PanelOn(header, info);
            SaveSystem.SaveData(header, info);
            Debug.Log("All rings match. Player wins!");
        }

        private void Lose()
        {
            var header = "Поражение";
            var info = $"Пользователь: Иванов\nУровень: {_levelConfig.levelIndex}\nХодов: {_turn}";

            _puzzleStatePanel.PanelOn(header, info);
            SaveSystem.SaveData(header, info);
            Debug.Log("Player loses!");
        }

        public void NewLevel()
        {
            _levelIndex++;
            GetLevelConfig(_levelIndex);

            _turns = _levelConfig.numberOfMoves;
            Turn = 0;
            InitBoard(_target, _levelConfig.targetBoard);
            InitBoard(_player, _levelConfig.gameBoard);
        }

        private void CloseLevel()
        {
            _target.ClearRings();
            _player.ClearRings();
        }
    }
}