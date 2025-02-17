using System;

namespace TowerOfLondon.Structures
{
    /// <summary>
    /// ƒанные доски и соответственно стержней на ней
    /// </summary>
    [Serializable]
    public class Board
    {
        public Pin pin1 = new Pin();
        public Pin pin2 = new Pin();
        public Pin pin3 = new Pin();
    }
}
