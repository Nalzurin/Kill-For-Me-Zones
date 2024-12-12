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
    [StaticConstructorOnStartup]
    public class Zone_Kill : Zone
    {
        public static readonly Texture2D icon = ContentFinder<Texture2D>.Get("UI/Designators/KillDesignation");
        public override bool IsMultiselectable => false;

        public static Dictionary<Pawn, Zone_Kill> pawnsInZones = new Dictionary<Pawn, Zone_Kill>();
        public bool assignPack = false;
        public bool markForKill = true;
        public bool attackAnimals = true;
        public bool attackHumanlikes = true;
        public bool attackMechanoids = true;
        public bool attackEntities = true;
        public bool attackNeutrals = false;
        public bool attackHostiles = true;
        public bool unmarkOutsideOfZone = false;
        public static DesignationDef DesignationKill
        {
            get
            {
                return DefDatabase<DesignationDef>.GetNamed(aRandomKiwi.KFM.Utils.killDesignation);
            }
        }
        protected override Color NextZoneColor => new Color(0,0,0, 0.3f);

        public Zone_Kill()
        {
          
        }

        public Zone_Kill(ZoneManager zoneManager)
            : base("KillZone".Translate(), zoneManager)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref markForKill, "markForKill", true);
            Scribe_Values.Look(ref attackAnimals, "attackAnimals", true);
            Scribe_Values.Look(ref attackHumanlikes, "attackHumanlikes", true);
            Scribe_Values.Look(ref attackMechanoids, "attackMechanoids", true);
            Scribe_Values.Look(ref attackEntities, "attackEntities", true);
            Scribe_Values.Look(ref attackNeutrals, "attackNeutrals", false);
            Scribe_Values.Look(ref attackHostiles, "attackHostiles", true);
            Scribe_Values.Look(ref unmarkOutsideOfZone, "unmarkOutsideOfZone", false);
            Scribe_Collections.Look(ref pawnsInZones, "pawnsMarked", LookMode.Reference, LookMode.Reference);
        }


        public override void AddCell(IntVec3 c) 
        {
            base.AddCell(c);
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo gizmo in base.GetGizmos())
            {
                yield return gizmo;
            }
            /*            Command_Toggle command_ToggleAssignPack = new Command_Toggle();
                        command_ToggleAssignPack.defaultLabel = "CommandAllowAssignPack".Translate();
                        command_ToggleAssignPack.defaultDesc = "CommandAllowAssignPackDesc".Translate();
                        command_ToggleAssignPack.icon = null;
                        command_ToggleAssignPack.isActive = () => assignPack;
                        command_ToggleAssignPack.toggleAction = delegate
                        {
                            assignPack = !assignPack;
                        };
                        yield return command_ToggleAssignPack;*/
            Command_Toggle command_ToggleMarkForKill = new Command_Toggle();
            command_ToggleMarkForKill.defaultLabel = "CommandAllowMarkForKill".Translate();
            command_ToggleMarkForKill.defaultDesc = "CommandAllowMarkForKillDesc".Translate();
            command_ToggleMarkForKill.icon = icon;
            command_ToggleMarkForKill.isActive = () => markForKill;
            command_ToggleMarkForKill.toggleAction = delegate
            {
                markForKill = !markForKill;
            };
            yield return command_ToggleMarkForKill;
            if (markForKill)
            {
                Command_Toggle command_ToggleUnmarkOutsideOfZone = new Command_Toggle();
                command_ToggleUnmarkOutsideOfZone.defaultLabel = "CommandAllowUnmarkOutsideOfZone".Translate();
                command_ToggleUnmarkOutsideOfZone.defaultDesc = "CommandAllowUnmarkOutsideOfZoneDesc".Translate();
                command_ToggleUnmarkOutsideOfZone.icon = icon;
                command_ToggleUnmarkOutsideOfZone.isActive = () => unmarkOutsideOfZone;
                command_ToggleUnmarkOutsideOfZone.toggleAction = delegate
                {
                    unmarkOutsideOfZone = !unmarkOutsideOfZone;
                };
                yield return command_ToggleUnmarkOutsideOfZone;

                Command_Toggle command_ToggleAttackAnimals = new Command_Toggle();
                command_ToggleAttackAnimals.defaultLabel = "CommandAllowAttackAnimals".Translate();
                command_ToggleAttackAnimals.defaultDesc = "CommandAllowAttackAnimalsDesc".Translate();
                command_ToggleAttackAnimals.icon = icon;
                command_ToggleAttackAnimals.isActive = () => attackAnimals;
                command_ToggleAttackAnimals.toggleAction = delegate
                {
                    attackAnimals = !attackAnimals;
                };
                yield return command_ToggleAttackAnimals;

                Command_Toggle command_ToggleAttackHumanlikes = new Command_Toggle();
                command_ToggleAttackHumanlikes.defaultLabel = "CommandAllowAttackHumanlikes".Translate();
                command_ToggleAttackHumanlikes.defaultDesc = "CommandAllowAttackHumanlikesDesc".Translate();
                command_ToggleAttackHumanlikes.icon = icon;
                command_ToggleAttackHumanlikes.isActive = () => attackHumanlikes;
                command_ToggleAttackHumanlikes.toggleAction = delegate
                {
                    attackHumanlikes = !attackHumanlikes;
                };
                yield return command_ToggleAttackHumanlikes;

                Command_Toggle command_ToggleAttackMechanoids = new Command_Toggle();
                command_ToggleAttackMechanoids.defaultLabel = "CommandAllowAttackMechanoids".Translate();
                command_ToggleAttackMechanoids.defaultDesc = "CommandAllowAttackMechanoidsDesc".Translate();
                command_ToggleAttackMechanoids.icon = icon;
                command_ToggleAttackMechanoids.isActive = () => attackMechanoids;
                command_ToggleAttackMechanoids.toggleAction = delegate
                {
                    attackMechanoids = !attackMechanoids;
                };
                yield return command_ToggleAttackMechanoids;

                Command_Toggle command_ToggleAttackEntities = new Command_Toggle();
                command_ToggleAttackEntities.defaultLabel = "CommandAllowAttackEntities".Translate();
                command_ToggleAttackEntities.defaultDesc = "CommandAllowAttackEntitiesDesc".Translate();
                command_ToggleAttackEntities.icon = icon;
                command_ToggleAttackEntities.isActive = () => attackEntities;
                command_ToggleAttackEntities.toggleAction = delegate
                {
                    attackEntities = !attackEntities;
                };
                yield return command_ToggleAttackEntities;

                Command_Toggle command_ToggleAttackHostiles = new Command_Toggle();
                command_ToggleAttackHostiles.defaultLabel = "CommandAllowAttackHostiles".Translate();
                command_ToggleAttackHostiles.defaultDesc = "CommandAllowAttackHostilesDesc".Translate();
                command_ToggleAttackHostiles.icon = icon;
                command_ToggleAttackHostiles.isActive = () => attackHostiles;
                command_ToggleAttackHostiles.toggleAction = delegate
                {
                    attackHostiles = !attackHostiles;
                };
                yield return command_ToggleAttackHostiles;

                Command_Toggle command_ToggleAttackNeutrals = new Command_Toggle();
                command_ToggleAttackNeutrals.defaultLabel = "CommandAllowAttackNeutrals".Translate();
                command_ToggleAttackNeutrals.defaultDesc = "CommandAllowAttackNeutralsDesc".Translate();
                command_ToggleAttackNeutrals.icon = icon;
                command_ToggleAttackNeutrals.isActive = () => attackNeutrals;
                command_ToggleAttackNeutrals.toggleAction = delegate
                {
                    attackNeutrals = !attackNeutrals;
                };
                yield return command_ToggleAttackNeutrals;
            }

        }

        public override IEnumerable<Gizmo> GetZoneAddGizmos()
        {
            yield return DesignatorUtility.FindAllowedDesignator<Designator_ZoneAdd_Kill_Expand>();
        }

        public void CheckPawn(Pawn p)
        {
            if (!markForKill)
            {
                return;
            }
            if (!aRandomKiwi.KFM.Utils.isValidEnemy(p))
            {
                return;
            }
            if (p.AnimalOrWildMan() && !attackAnimals)
            {
                return;
            }
            if (p.RaceProps.Humanlike && !attackHumanlikes)
            {
                return;
            }
            if (p.RaceProps.IsMechanoid && !attackMechanoids)
            {
                return;
            }
            if (p.RaceProps.IsAnomalyEntity && !attackEntities)
            {
                return;
            }
            if (p.HostileTo(Faction.OfPlayer) && !attackHostiles)
            {
                return;
            }
            if (!p.HostileTo(Faction.OfPlayer) && !attackNeutrals)
            {
                return;
            }

            if (Map.designationManager.DesignationOn(p, DesignationKill) == null)
            {
                Map.designationManager.RemoveAllDesignationsOn(p, false);
                Map.designationManager.AddDesignation(new Designation(p, DesignationKill));
                pawnsInZones[p] = this;
            }
        }
        public static void TryUnmarkPawn(Pawn p)
        {
            if (pawnsInZones.ContainsKey(p))
            {
                if (pawnsInZones[p].unmarkOutsideOfZone)
                {
                    p.Map.designationManager.RemoveAllDesignationsOn(p, false);
                }
                pawnsInZones.Remove(p);
                
            }
            

        }
    }
}
