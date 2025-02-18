using System.Collections.Generic;
using UnityEngine;

namespace TowerOfLondon.Configs
{
    [CreateAssetMenu(fileName = "LevelConfigReposytory", menuName = "Scriptable Objects/LevelConfigReposytory")]
    public class LevelConfigRepository : ScriptableObject
    {
        public List<LevelConfig> levels;
    }
}
