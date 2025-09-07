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

    enum BitwiseAction
    {
        Unknown,
        None,
        Not,
        And,
        LShift,
        Or,
        RShift
    }

    unsafe struct Wire
    {
        public string Name;
        public string Reference;
        public ushort Value;
        public bool UsingReference;
        public bool HasProcessed;
    }

    struct BitwiseCommand
    {
        public BitwiseAction Action;
        public string DestinationKey;
        //public Wire[] Wires;
        public string[] WireRefs;
        public bool HasProcessed;

        //public string[] InputReferences;
        //public uint[] InputValues;
        //public bool HasInputReferences;

        //internal bool ContainsReferenceInputs()
        //{
        //    ushort input1, input2;
        //    bool result = false;

        //    if (!ushort.TryParse(Inputs[0], out input1))
        //    {
        //        result = true;
        //    } 
        //    else if (Action > BitwiseAction.Not)
        //    {
        //        if (!ushort.TryParse(Inputs[1], out input2))
        //        {
        //            result = true;
        //        }
        //    }

        //    return result;
        //}

        //internal string[] GetReferenceInputs()
        //{
        //    string[] referenceInputs = null;

        //    ushort input1, input2;
        //    bool result = false;

        //    if (!ushort.TryParse(Inputs[0], out input1))
        //    {
        //        result = true;
        //    }


        //    if (Action > BitwiseAction.Not)
        //    {
        //        if (!ushort.TryParse(Inputs[1], out input2))
        //        {
        //            result = true;
        //        }
        //    }

        //    return referenceInputs;
        //}
    }
}
