using System.Collections.Generic;
using System.Text;
using PipeSystem;
using RimWorld;
using UnityEngine;
using Verse;
using VNPE;
using Verse.Sound;

namespace VNPEA
{
    [StaticConstructorOnStartup]
    public class Building_NutrientPasteTapI : Building_NutrientPasteTap
    {

        new public bool CanDispenseNowOverride => powerComp.PowerOn && resourceComp.PipeNet is PipeNet net && net.Stored >= 1;

        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (var gizmo in base.GetGizmos())
                if (!(gizmo.ToString() == "Command(label=Extract 5 meals, defaultDesc=Extract 5 meals)"))
                    if (!(gizmo.ToString() == "Command(label=Extract 10 meals, defaultDesc=Extract 10 meals)"))
                        if (!(gizmo.ToString() == "Command(label=Extract 20 meals, defaultDesc=Extract 20 meals)"))
                        {
                    yield return gizmo;
                }
            yield return new Command_Action
            {
                action = () => TryDropFood(5),
                defaultLabel = "VNPE_Extract5".Translate(),
                defaultDesc = "VNPE_Extract5".Translate(),
                icon = aIcon,
            };
            yield return new Command_Action
            {
                action = () => TryDropFood(10),
                defaultLabel = "VNPE_Extract10".Translate(),
                defaultDesc = "VNPE_Extract10".Translate(),
                icon = bIcon,
            };
            yield return new Command_Action
            {
                action = () => TryDropFood(20),
                defaultLabel = "VNPE_Extract20".Translate(),
                defaultDesc = "VNPE_Extract20".Translate(),
                icon = cIcon,
            };
        }



        new public Thing TryDispenseFoodOverride()
        {
            var net = resourceComp.PipeNet;
            def.building.soundDispense.PlayOneShot(new TargetInfo(Position, Map));
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

            return meal;
        }

        private void TryDropFood(int amount)
        {
            if (!powerComp.PowerOn || amount <= 0)
                return;

            var net = resourceComp.PipeNet;
            var stored = net.Stored;

            if (stored <= 0)
                return;

            var map = Map;
            var cell = InteractionCell;

            int totalCount = (int)(amount > stored ? stored : amount);
            net.DrawAmongStorage(totalCount, net.storages);
            def.building.soundDispense.PlayOneShot(new TargetInfo(cell, map));

            var comps = new List<CompRegisterIngredients>();
            for (int i = 0; i < net.storages.Count; i++)
            {
                if (net.storages[i].parent.TryGetComp<CompRegisterIngredients>() is CompRegisterIngredients storageIngredients)
                    comps.Add(storageIngredients);
            }

            while (totalCount > 0)
            {
                var meal = ThingMaker.MakeThing(VAThingDefOf.MealImprovedNutrientPaste);
                if (meal.TryGetComp<CompIngredients>() is CompIngredients ingredients)
                {
                    for (int i = 0; i < comps.Count; i++)
                    {
                        var comp = comps[i];
                        for (int o = 0; o < comp.ingredients.Count; o++)
                            ingredients.RegisterIngredient(comp.ingredients[o]);
                    }
                }

                var stack = totalCount > meal.def.stackLimit ? meal.def.stackLimit : totalCount;
                meal.stackCount = stack;
                totalCount -= stack;

                GenPlace.TryPlaceThing(meal, cell, map, ThingPlaceMode.Near);
            }
        }
    }
}