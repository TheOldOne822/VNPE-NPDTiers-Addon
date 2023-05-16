using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace VNPEA
{
    [HarmonyPatch(typeof(ThingListGroupHelper), "Includes")]
    public static class ThingListGroupHelper_Includes
    {
        public static void Postfix(ThingDef def, ThingRequestGroup group, ref bool __result)
        {
            // Make sure the nutrient paste tap is considered as Building_NutrientPasteDispenser
            if ((group == ThingRequestGroup.FoodSourceNotPlantOrTree || group == ThingRequestGroup.FoodSource)
                && (def.defName == "VNPEA_NutrientPasteTapI" || def.defName == "VNPEA_NutrientPasteTapF" || def.defName == "VNPEA_NutrientPasteTapP"))
            {
                __result = true;
            }
        }
    }
}
