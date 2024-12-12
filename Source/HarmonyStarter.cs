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
    public static class Patch_PawnTick
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(Pawn), nameof(Pawn.Tick));
        }

        private static void Postfix(Pawn __instance)
        {
            if (__instance.IsHashIntervalTick(KillForMeZones_Settings.ZoneTick))
            {
                if (__instance.Spawned && !__instance.DeadOrDowned && !__instance.Suspended)
                {
                    if (__instance.Map.zoneManager.ZoneAt(__instance.Position) is Zone_Kill zone)
                    {
                        zone.CheckPawn(__instance);

                    }
                    else
                    {
                        Zone_Kill.TryUnmarkPawn(__instance);
                    }

                }
            }
           

        }
    }
    [HarmonyPatch]
    public static class Patch_Pawn_DeadOrDowned
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(Pawn), "get_DeadOrDowned");
        }

        private static void Postfix(ref bool __result, Pawn __instance)
        {
           
            if(__result)
            {
                Zone_Kill.pawnsInZones[__instance] = null;
            }
        }
    }

    /*    [HarmonyPatch]
        public static class Patch_AreaManager_AddStartingAreas
        {
            private static MethodBase TargetMethod()
            {
                return AccessTools.Method(typeof(AreaManager), nameof(AreaManager.AddStartingAreas));
            }

            private static void Postfix(AreaManager __instance, List<Area> ___areas)
            {
                ___areas.Add(new Area_Kill(__instance));

            }
        }*/
}
