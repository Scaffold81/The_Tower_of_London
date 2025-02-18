using System.Collections.Generic;
using UnityEngine;

namespace TowerOfLondon.Configs
{
    [CreateAssetMenu(fileName = "LevelConfigReposytory", menuName = "Scriptable Objects/LevelConfigReposytory")]
    public class LevelConfigReposytory : ScriptableObject
    {
       public List<LevelConfig> levels;
    }
}
