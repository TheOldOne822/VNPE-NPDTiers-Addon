using HarmonyLib;
using RimWorld;
using Verse;

namespace VNPEA
{
    [HarmonyPatch(typeof(Building_NutrientPasteDispenser))]
    [HarmonyPatch("TryDispenseFood", MethodType.Normal)]
    public static class Building_NutrientPasteDispenser_TryDispenseFood
    {
        public static bool Prefix(Building_NutrientPasteDispenser __instance, ref Thing __result)
        {
            if (__instance is Building_NutrientPasteTapI nutrientPasteTapI)
            {
                __result = nutrientPasteTapI.TryDispenseFoodOverride();
                return false;
            }

            if (__instance is Building_NutrientPasteTapF nutrientPasteTapF)
            {
                __result = nutrientPasteTapF.TryDispenseFoodOverride();
                return false;
            }

            if (__instance is Building_NutrientPasteTapP nutrientPasteTapP)
            {
                __result = nutrientPasteTapP.TryDispenseFoodOverride();
                return false;
            }

            return true;
        }
    }
}