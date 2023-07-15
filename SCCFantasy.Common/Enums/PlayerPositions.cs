using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.Common.Enums
{
    public enum PlayerPositions
    {
        [Description("Goalkeeper")]
        GKP = 1,

        [Description("Defender")]
        DEF,

        [Description("Midfielder")]
        MID,

        [Description("Forward")]
        FWD
    }
}
