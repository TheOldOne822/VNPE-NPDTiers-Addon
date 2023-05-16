using RimWorld;
using Verse;

namespace VNPEA
{
    [DefOf]
    public static class VAThingDefOf
    {
        public static ThingDef VNPEA_NutrientPasteTapI;
        public static ThingDef INutrientPasteDispenser;
        public static ThingDef VNPEA_NutrientPasteFeederI;
        public static ThingDef VNPEA_NutrientPasteDripperI;
        public static ThingDef VNPEA_NutrientPasteDripperBaby;
        public static ThingDef VNPEA_NutrientPasteTapF;
        public static ThingDef FNutrientPasteDispenser;
        public static ThingDef VNPEA_NutrientPasteFeederF;
        public static ThingDef VNPEA_NutrientPasteDripperF;
        public static ThingDef VNPEA_NutrientPasteTapP;
        public static ThingDef PrisonerNutrientPaste;
        public static ThingDef VNPEA_NutrientPasteFeederP;
        public static ThingDef VNPEA_NutrientPasteDripperP;
        public static ThingDef MealImprovedNutrientPaste;
        public static ThingDef MealFineNutrientPaste;
        public static ThingDef MealPrisonerNutrientPaste;
        public static EffecterDef EatVegetarian;

        static VAThingDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(VAThingDefOf));
    }
}