using System;
using System.Collections.Generic;

namespace TowerOfLondon.Structures
{
    /// <summary>
    /// ƒанные доски и соответственно стержней на ней
    /// </summary>
    [Serializable]
    public class Board
    {
        public List<Pin> pin = new ();
    }
}
