using HarmonyLib;
using Verse;
using VNPE;

namespace VNPEA
{
    [HarmonyPatch(typeof(Room))]
    [HarmonyPatch("Notify_RoomShapeChanged", MethodType.Normal)]
    public static class Room_NotifyRoomShapeChanged
    {
        public static void Postfix(Room __instance)
        {
            if (!__instance.Dereferenced)
            {
                var map = __instance.Map;

                var pasteTapsI = map.listerThings.ThingsOfDef(VAThingDefOf.VNPEA_NutrientPasteTapI);
                for (int i = 0; i < pasteTapsI.Count; i++)
                    pasteTapsI[i].Notify_ColorChanged();

                var pasteFeedersI = map.listerThings.ThingsOfDef(VAThingDefOf.VNPEA_NutrientPasteFeederI);
                for (int i = 0; i < pasteFeedersI.Count; i++)
                    pasteFeedersI[i].Notify_ColorChanged();

                var pasteDrippersI = map.listerThings.ThingsOfDef(VAThingDefOf.VNPEA_NutrientPasteDripperI);
                for (int i = 0; i < pasteDrippersI.Count; i++)
                    pasteDrippersI[i].Notify_ColorChanged();

                var pasteTapsF = map.listerThings.ThingsOfDef(VAThingDefOf.VNPEA_NutrientPasteTapF);
                for (int i = 0; i < pasteTapsF.Count; i++)
                    pasteTapsF[i].Notify_ColorChanged();

                var pasteFeedersF = map.listerThings.ThingsOfDef(VAThingDefOf.VNPEA_NutrientPasteFeederF);
                for (int i = 0; i < pasteFeedersF.Count; i++)
                    pasteFeedersF[i].Notify_ColorChanged();

                var pasteDrippersF = map.listerThings.ThingsOfDef(VAThingDefOf.VNPEA_NutrientPasteDripperF);
                for (int i = 0; i < pasteDrippersF.Count; i++)
                    pasteDrippersF[i].Notify_ColorChanged();

                var pasteTapsP = map.listerThings.ThingsOfDef(VAThingDefOf.VNPEA_NutrientPasteTapP);
                for (int i = 0; i < pasteTapsP.Count; i++)
                    pasteTapsP[i].Notify_ColorChanged();

                var pasteFeedersP = map.listerThings.ThingsOfDef(VAThingDefOf.VNPEA_NutrientPasteFeederP);
                for (int i = 0; i < pasteFeedersP.Count; i++)
                    pasteFeedersP[i].Notify_ColorChanged();

                var pasteDrippersP = map.listerThings.ThingsOfDef(VAThingDefOf.VNPEA_NutrientPasteDripperP);
                for (int i = 0; i < pasteDrippersP.Count; i++)
                    pasteDrippersP[i].Notify_ColorChanged();
            }
        }
    }
}