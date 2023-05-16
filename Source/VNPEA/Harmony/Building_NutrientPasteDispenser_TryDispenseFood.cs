using HarmonyLib;
using PipeSystem;
using RimWorld;
using Verse;
using Verse.Sound;
using VNPE;

namespace VNPEA
{
    [HarmonyPatch(typeof(Building_NutrientPasteDispenser))]
    [HarmonyPatch("TryDispenseFood", MethodType.Normal)]
    public static class Building_NutrientPasteDispenser_TryDispenseFood
    {
    [HarmonyPriority(Priority.High)]
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
            if (__instance.def.defName == "INutrientPasteDispenser")
                if (!Building_NutrientPasteDispenser_GetGizmos.HasEnoughFeedstockInHoppers(__instance))
            {
                var comp = __instance.GetComp<CompResource>();
                if (comp != null && comp.PipeNet is PipeNet net && net.Stored >= 1)
                {
                    __instance.def.building.soundDispense.PlayOneShot(new TargetInfo(__instance.Position, __instance.Map));
                    net.DrawAmongStorage(1, net.storages);


                        var meal = ThingMaker.MakeThing(VAThingDefOf.MealImprovedNutrientPaste);
                        if (meal.TryGetComp<CompIngredients>() is CompIngredients ingredients)
                        {
                            for (int i = 0; i < net.storages.Count; i++)
                            {
                                var parent = net.storages[i].parent;
                                if (parent.TryGetComp<CompRegisterIngredients>() is CompRegisterIngredients storageIngredients)
                                {
                                    for (int o = 0; o < storageIngredients.ingredients.Count; o++)
                                        ingredients.RegisterIngredient(storageIngredients.ingredients[o]);
                                }
                            }
                        }

                        __result = meal;
                    return false;
                }
            }
            if (__instance.def.defName == "FNutrientPasteDispenser")
                if (!Building_NutrientPasteDispenser_GetGizmos.HasEnoughFeedstockInHoppers(__instance))
                {
                    var comp = __instance.GetComp<CompResource>();
                    if (comp != null && comp.PipeNet is PipeNet net && net.Stored >= 4f / 3f)
                    {
                        __instance.def.building.soundDispense.PlayOneShot(new TargetInfo(__instance.Position, __instance.Map));
                        net.DrawAmongStorage(4f / 3f, net.storages);

                        var meal = ThingMaker.MakeThing(VAThingDefOf.MealFineNutrientPaste);
                        if (meal.TryGetComp<CompIngredients>() is CompIngredients ingredients)
                        {
                            for (int i = 0; i < net.storages.Count; i++)
                            {
                                var parent = net.storages[i].parent;
                                if (parent.TryGetComp<CompRegisterIngredients>() is CompRegisterIngredients storageIngredients)
                                {
                                    for (int o = 0; o < storageIngredients.ingredients.Count; o++)
                                        ingredients.RegisterIngredient(storageIngredients.ingredients[o]);
                                }
                            }
                        }

                        __result = meal;
                        return false;
                    }
                }
            if (__instance.def.defName == "PrisonerNutrientPaste")
                if (!Building_NutrientPasteDispenser_GetGizmos.HasEnoughFeedstockInHoppers(__instance))
                {
                    var comp = __instance.GetComp<CompResource>();
                    if (comp != null && comp.PipeNet is PipeNet net && net.Stored >= 2f / 3f)
                    {
                        __instance.def.building.soundDispense.PlayOneShot(new TargetInfo(__instance.Position, __instance.Map));
                        net.DrawAmongStorage(2f / 3f, net.storages);

                        var meal = ThingMaker.MakeThing(VAThingDefOf.MealPrisonerNutrientPaste);

                        __result = meal;
                        return false;
                    }
                }


            return true;
        }
    }
}