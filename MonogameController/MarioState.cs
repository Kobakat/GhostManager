using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameController
{
    public class MarioState
    {
        public State state;

        public MarioState()
        {
            //Makes sure mario starts falling so he isn't forced to be placed on ground in the editor
            this.state = State.Airborne;
        }
    }
    public enum State
    {
        Grounded,
        Airborne
    }
}
