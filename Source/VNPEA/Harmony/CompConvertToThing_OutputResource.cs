using HarmonyLib;
using PipeSystem;
using RimWorld;
using Verse;
using VNPE;

namespace VNPEA
{
    [HarmonyPatch(typeof(CompConvertToThing))]
    [HarmonyPatch("OutputResource", MethodType.Normal)]
    public static class CompConvertToThing_OutputResource
    {
        public static bool Prefix(int amount, CompConvertToThing __instance)
        {
            var parent = __instance.parent;
            if (parent.def.defName == "VNPEA_NutrientPasteFeederI")
            {
                var net = __instance.PipeNet;

                var meal = ThingMaker.MakeThing(VAThingDefOf.MealImprovedNutrientPaste);
                if (meal.TryGetComp<CompIngredients>() is CompIngredients ingredients)
                {
                    for (int i = 0; i < net.storages.Count; i++)
                    {
                        var storage = net.storages[i].parent;
                        if (storage.TryGetComp<CompRegisterIngredients>() is CompRegisterIngredients storageIngredients)
                        {
                            for (int o = 0; o < storageIngredients.ingredients.Count; o++)
                                ingredients.RegisterIngredient(storageIngredients.ingredients[o]);
                        }
                    }
                }

                meal.stackCount = amount;
                GenPlace.TryPlaceThing(meal, parent.Position, parent.Map, ThingPlaceMode.Near);
                return false;
            }
            if (parent.def.defName == "VNPEA_NutrientPasteFeederF")
            {
                var net = __instance.PipeNet;

                var meal = ThingMaker.MakeThing(VAThingDefOf.MealFineNutrientPaste);
                if (meal.TryGetComp<CompIngredients>() is CompIngredients ingredients)
                {
                    for (int i = 0; i < net.storages.Count; i++)
                    {
                        var storage = net.storages[i].parent;
                        if (storage.TryGetComp<CompRegisterIngredients>() is CompRegisterIngredients storageIngredients)
                        {
                            for (int o = 0; o < storageIngredients.ingredients.Count; o++)
                                ingredients.RegisterIngredient(storageIngredients.ingredients[o]);
                        }
                    }
                }

                meal.stackCount = amount;
                GenPlace.TryPlaceThing(meal, parent.Position, parent.Map, ThingPlaceMode.Near);
                net.DistributeAmongStorage(2f / 3f);
                return false;
            }
            if (parent.def.defName == "VNPEA_NutrientPasteFeederBaby")
            {
                var net = __instance.PipeNet;

                var meal = ThingMaker.MakeThing(ThingDefOf.BabyFood);
                if (meal.TryGetComp<CompIngredients>() is CompIngredients ingredients)
                {
                    for (int i = 0; i < net.storages.Count; i++)
                    {
                        var storage = net.storages[i].parent;
                        if (storage.TryGetComp<CompRegisterIngredients>() is CompRegisterIngredients storageIngredients)
                        {
                            for (int o = 0; o < storageIngredients.ingredients.Count; o++)
                                ingredients.RegisterIngredient(storageIngredients.ingredients[o]);
                        }
                    }
                }

                meal.stackCount = amount * 18;
                GenPlace.TryPlaceThing(meal, parent.Position, parent.Map, ThingPlaceMode.Near);
                return false;
            }
            if (parent.def.defName == "VNPEA_NutrientPasteFeederP")
            {
                var net = __instance.PipeNet;

                var meal = ThingMaker.MakeThing(VAThingDefOf.MealPrisonerNutrientPaste);

                meal.stackCount = amount;
                GenPlace.TryPlaceThing(meal, parent.Position, parent.Map, ThingPlaceMode.Near);
                net.DistributeAmongStorage(1f / 3f);
                return false;
            }
            return true;
        }
    }
}
