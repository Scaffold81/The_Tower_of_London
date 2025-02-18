using System.Linq;
using TowerOfLondon.Configs;
using UnityEngine;

namespace TowerOfLondon.Common
{
    /// <summary>
    /// Клас храняший и выдюший конфиги по запросу
    /// </summary>
    public class LevelConfigsManager : MonoBehaviour
    {
        public static LevelConfigsManager Instance;
        
        [SerializeField]
        private LevelConfigReposytory _levelConfigReposytory;

        [SerializeField]
        private LevelConfig _defaultConfig;

        private void Awake()
        {
            Instance = this;
        }

        public LevelConfig GetConfig(int levelIndex)
        {
            var levelConfig = _levelConfigReposytory.levels.FirstOrDefault(a => a.levelIndex == levelIndex);
            if (levelConfig == null) levelConfig = _defaultConfig;
            return levelConfig;
        }

    }
}
