using System.Collections.Generic;
using System.Text;
using PipeSystem;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace VNPEA
{
    [StaticConstructorOnStartup]
    public class Building_NutrientPasteTapP : Building_NutrientPasteDispenser
    {
        public CompResource resourceComp;

        public bool CanDispenseNowOverride => powerComp.PowerOn && resourceComp.PipeNet is PipeNet net && net.Stored >= 0.6666f;
        public override Color DrawColor => !this.IsSociallyProper(null, false) ? Building_Bed.SheetColorForPrisoner : base.DrawColor;

        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (var gizmo in base.GetGizmos())
                yield return gizmo;

        }

        public override string GetInspectString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(base.GetInspectString());

            if (!this.IsSociallyProper(null, false))
                builder.AppendLine("InPrisonCell".Translate());

            return builder.ToString().Trim();
        }

        public override bool HasEnoughFeedstockInHoppers()
        {
            return resourceComp.PipeNet is PipeNet net && net.Stored >= 1;
        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            resourceComp = GetComp<CompResource>();
            powerComp = GetComp<CompPowerTrader>();
        }
        public Thing TryDispenseFoodOverride()
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