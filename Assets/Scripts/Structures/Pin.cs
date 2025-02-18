using System;
using System.Collections.Generic;
using TowerOfLondon.Enums;
using TowerOfLondon.Puzzle;

namespace TowerOfLondon.Structures
{
    /// <summary>
    /// данные стержня. Вместимость и какие кольца на нем находяться
    /// </summary>
    [Serializable]
    public class Pin
    {
        public int pinCapacity = 1;
        public List<RingController> rings = new List<RingController>();
    }
}
