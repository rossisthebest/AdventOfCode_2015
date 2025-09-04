using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2015.Types
{
    struct CoOrd
    {
        public int x, y;
    }

    delegate bool Rule(string input);

    enum LightAction
    {
        Unknown,
        On,
        Off,
        Toggle
    }

    struct LightCommand
    {
        public LightAction Action;
        public CoOrd StartPos, EndPos;
    }
}
