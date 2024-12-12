using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace KillForMeZones
{
    public class KillForMeZones_Mod : Mod
    {
        public static KillForMeZones_Settings settings;

        public KillForMeZones_Mod(ModContentPack content)
            : base(content)
        {
            settings = GetSettings<KillForMeZones_Settings>();
        }

        public override string SettingsCategory()
        {
            return "Kill For Me - Zones";
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            settings.DoWindowContents(inRect);
        }
    }
}
