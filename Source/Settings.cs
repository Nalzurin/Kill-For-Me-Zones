using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace KillForMeZones
{
    public class KillForMeZones_Settings : ModSettings
    {
        public static int ZoneTick = 1;

        public override void ExposeData()
        {

            Scribe_Values.Look(ref ZoneTick, "ZoneTick", defaultValue: 1, forceSave: true);


            base.ExposeData();
        }

        public void DoWindowContents(Rect inRect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(inRect);
            listing_Standard.Label("KillZoneTickInterval".Translate() + ": " + ZoneTick.ToString() + "ms");
            ZoneTick = (int)listing_Standard.Slider(ZoneTick, 1, 100);
            listing_Standard.End();
        }
    }
}
