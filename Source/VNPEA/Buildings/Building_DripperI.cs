using System.Linq;
using RimWorld;
using VNPE;
using Verse;

namespace VNPEA
{
    public class Building_DripperI : Building_Dripper
    {
        public override void TickRare()
        {
            if (!powerComp.PowerOn)
                return;

            var pos = Position;
            var net = resourceComp.PipeNet;

            var linkeds = facilityComp.LinkedBuildings;
            for (int i = 0; i < linkeds.Count; i++)
            {
                var linked = linkeds[i];
                // Get linked bed
                if (linked is Building_Bed bed)
                {
                    var occupants = bed.CurOccupants.ToList();
                    for (int o = 0; o < occupants.Count; o++)
                    {
                        var occupant = occupants[o];
                        // Make sure we only apply effect to pawn right next to the dripper
                        if (occupant.Position.AdjacentToCardinal(pos))
                        {
                            if (occupant.needs.food.CurLevelPercentage <= 0.4)
                            {
                                net.DrawAmongStorage(1, net.storages);
                                var meal = ThingMaker.MakeThing(VAThingDefOf.MealImprovedNutrientPaste);
                                if (meal.TryGetComp<CompIngredients>() is CompIngredients ingredients)
                                {
                                    for (int s = 0; s < net.storages.Count; s++)
                                    {
                                        var parent = net.storages[s].parent;
                                        if (parent.TryGetComp<CompRegisterIngredients>() is CompRegisterIngredients storageIngredients)
                                        {
                                            for (int ig = 0; ig < storageIngredients.ingredients.Count; ig++)
                                                ingredients.RegisterIngredient(storageIngredients.ingredients[ig]);
                                        }
                                    }
                                }

                                var ingestedNum = meal.Ingested(occupant, occupant.needs.food.NutritionWanted);
                                occupant.needs.food.CurLevel += ingestedNum;
                                occupant.records.AddTo(RecordDefOf.NutritionEaten, ingestedNum);
                            }
                        }
                    }
                }
            }
            base.TickRare();
        }
    }
}