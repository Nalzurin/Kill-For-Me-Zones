using KTrie;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace KillForMeZones
{
    public class Designator_ZoneAdd_Kill : Designator_ZoneAdd
    {
        protected override string NewZoneLabel => "KillZone".Translate();

        public Designator_ZoneAdd_Kill()
        {
            zoneTypeToPlace = typeof(Zone_Kill);
            defaultLabel = "KillZone".Translate();
            defaultDesc = "DesignatorKillZoneDesc".Translate();
            icon = ContentFinder<Texture2D>.Get("UI/Designators/ZoneCreate_Kill");
            soundSucceeded = SoundDefOf.Designate_ZoneAdd_Growing;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            AcceptanceReport result = base.CanDesignateCell(c);
            if (!result.Accepted)
            {
                return result;
            }
            if (c.GetTerrain(base.Map).passability == Traversability.Impassable)
            {
                return false;
            }
            List<Thing> list = base.Map.thingGrid.ThingsListAt(c);
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].def.CanOverlapZones)
                {
                    return false;
                }
            }
            return true;
        }

        protected override Zone MakeNewZone()
        {
            return new Zone_Kill(Find.CurrentMap.zoneManager);
        }
    }
}
