using System.Collections.Generic;
using System.Text;
using PipeSystem;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;
using VNPE;

namespace VNPEA
{
    [StaticConstructorOnStartup]
    public class Building_NutrientPasteTapP : Building_NutrientPasteTap
    {

        new public bool CanDispenseNowOverride => powerComp.PowerOn && resourceComp.PipeNet is PipeNet net && net.Stored >= 2f / 3f;

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

        public override bool HasEnoughFeedstockInHoppers()
        {
            return resourceComp.PipeNet is PipeNet net && net.Stored >= 2f / 3f;
        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            resourceComp = GetComp<CompResource>();
            powerComp = GetComp<CompPowerTrader>();
        }
        new public Thing TryDispenseFoodOverride()
        {
            var net = resourceComp.PipeNet;
            def.building.soundDispense.PlayOneShot(new TargetInfo(Position, Map));
            net.DrawAmongStorage(2f / 3f, net.storages);

            var meal = ThingMaker.MakeThing(VAThingDefOf.MealPrisonerNutrientPaste);

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

            int totalCount = (int)(amount * 2f / 3f > stored ? stored * 4f / 3f : amount);
            net.DrawAmongStorage(totalCount * 2f / 3f, net.storages);
            def.building.soundDispense.PlayOneShot(new TargetInfo(cell, map));


            while (totalCount > 0)
            {
                var meal = ThingMaker.MakeThing(VAThingDefOf.MealPrisonerNutrientPaste);

                var stack = totalCount > meal.def.stackLimit ? meal.def.stackLimit : totalCount;
                meal.stackCount = stack;
                totalCount -= stack;

                GenPlace.TryPlaceThing(meal, cell, map, ThingPlaceMode.Near);
            }
        }
    }
}