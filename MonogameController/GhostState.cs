using System;
using System.Collections.Generic;

namespace MonogameController
{

    public class GhostState
    {
        public GhostMode state;      
        public GhostState()
        {
            this.state = GhostMode.Hidden;
        }

    }

    public enum GhostMode
    {
        Hidden,
        Hunting,
        Avoiding
    }
}
