using System.Linq;
using TowerOfLondon.Configs;
using UnityEngine;

namespace TowerOfLondon.Common
{
    /// <summary>
    /// Класс, хранящий и выдающий конфиги по запросу
    /// </summary>
    public class ConfigManager : MonoBehaviour
    {
        public static ConfigManager Instance;

        [SerializeField]
        private LevelConfigRepository _levelConfigRepository;

        [SerializeField]
        private LevelConfig _defaultConfig;

        private void Awake()
        {
            if (Instance != null)
                Destroy(this);
            else
                Instance = this;
        }

        public LevelConfig GetConfig(int levelIndex)
        {
            var levelConfig = _levelConfigRepository.levels.FirstOrDefault(a => a.levelIndex == levelIndex);
            if (levelConfig == null) levelConfig = _defaultConfig;
            return levelConfig;
        }
    }
}
