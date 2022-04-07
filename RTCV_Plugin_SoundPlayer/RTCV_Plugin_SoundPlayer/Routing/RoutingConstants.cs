using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOUNDPLAYER
{
    internal static class Ep
    {
        public const string PREFIX = nameof(SOUNDPLAYER);
        public const string EMU_SIDE = PREFIX + "EMU";
        public const string RTC_SIDE = PREFIX + "RTC";
    }

    internal static class Commands
    {
        //Commands
        //public const string PROFILE_SELECTED = "NULLSVMDPROFILER";
        public const string GETMEM = Ep.PREFIX + nameof(GETMEM);

        public const string SPECUPDATE = Ep.PREFIX + nameof(SPECUPDATE);
        public const string SPECPUSH = Ep.PREFIX + nameof(SPECPUSH);

        //To emu side
        public const string BYTESGET = Ep.PREFIX + nameof(BYTESGET);

        //To rtc side
        public const string BYTESGET_CALLBACK = Ep.PREFIX + nameof(BYTESGET_CALLBACK);

        public const string SHOW_WINDOW = Ep.PREFIX + nameof(SHOW_WINDOW);
    }
}
