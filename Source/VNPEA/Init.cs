using HarmonyLib;
using RimWorld;
using Verse;

namespace VNPEA
{
    [StaticConstructorOnStartup]
    public class Init
    {
        static Init()
        {
            Harmony harmony = new Harmony("VNPEA");
            harmony.PatchAll();
            // Add dripper to all beds
            var defs = DefDatabase<ThingDef>.AllDefsListForReading;
            foreach (var def in defs)
            {
                // Is it a bed?
                if (def.building != null
                    && def.building.buildingTags.Contains("Bed")
                    && !def.defName.Contains("Spot")
                    && def.GetCompProperties<CompProperties_AffectedByFacilities>() is CompProperties_AffectedByFacilities props)
                {
                    if (!props.linkableFacilities.Contains(VAThingDefOf.VNPEA_NutrientPasteDripperP))
                    {
                        props.linkableFacilities.Add(VAThingDefOf.VNPEA_NutrientPasteDripperP);
                    }

                    if (!props.linkableFacilities.Contains(VAThingDefOf.VNPEA_NutrientPasteDripperI))
                    {
                        props.linkableFacilities.Add(VAThingDefOf.VNPEA_NutrientPasteDripperI);
                    }

                    if (!props.linkableFacilities.Contains(VAThingDefOf.VNPEA_NutrientPasteDripperF))
                    {
                        props.linkableFacilities.Add(VAThingDefOf.VNPEA_NutrientPasteDripperF);
                    }

                }



            }

            var dripperi = DefDatabase<ThingDef>.GetNamed("VNPEA_NutrientPasteDripperI");
            dripperi.GetCompProperties<CompProperties_Facility>().ResolveReferences(dripperi);
            var dripperp = DefDatabase<ThingDef>.GetNamed("VNPEA_NutrientPasteDripperP");
            dripperi.GetCompProperties<CompProperties_Facility>().ResolveReferences(dripperp);
            var dripperf = DefDatabase<ThingDef>.GetNamed("VNPEA_NutrientPasteDripperF");
            dripperi.GetCompProperties<CompProperties_Facility>().ResolveReferences(dripperf);
        }
    }
}