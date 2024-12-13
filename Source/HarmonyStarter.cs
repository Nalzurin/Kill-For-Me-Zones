using AnimalBehaviours;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Diagnostics;
using Verse;


namespace KillForMeZones
{
    [StaticConstructorOnStartup]
    public static class HarmonyStarter
    {
        public static DesignationDef def => DefDatabase<DesignationDef>.GetNamed(aRandomKiwi.KFM.Utils.killDesignation);
        static HarmonyStarter()
        {

            Harmony harmony = new Harmony("KillForMeZones");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch]
    public static class Patch_MapTick
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(Map), nameof(Map.MapPostTick));
        }

        private static void Postfix(Map __instance)
        {
            if (__instance.IsHashIntervalTick(KillForMeZones_Settings.ZoneTick))
            {
                foreach(Zone_Kill zone in __instance.zoneManager.AllZones.Where(c=>c is Zone_Kill))
                {
                    zone.ScanZone();
                }

            }


        }
    }
}
