using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using TrainworksReloaded.Base.Character;
using TrainworksReloaded.Base.Effect;
namespace Silk_Song_Clan.Plugin
{
    [HarmonyPatch]
    public static class CardEffectAttachEquipmentPatches
    {
        static MethodBase TargetMethod()
        {
            // Find the original method definition
            var outerMethod = AccessTools.Method(typeof(CardEffectAttachEquipment), "ApplyEffect");

            // Get the compiler-generated iterator type from the attribute
            var attr = outerMethod.GetCustomAttribute<IteratorStateMachineAttribute>();
            if (attr?.StateMachineType == null)
                throw new System.Exception("No iterator type found for ApplyEffect");

            // Target MoveNext() on that generated type
            return AccessTools.Method(attr.StateMachineType, "MoveNext");
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var list = new List<CodeInstruction>(instructions);
            var getHasEquipment = AccessTools.Method(typeof(CharacterState), nameof(CharacterState.GetHasEquipment));

            for (int i = 0; i < list.Count; i++)
            {
                var instr = list[i];

                // Replace callvirt to GetHasEquipment
                if (instr.opcode == OpCodes.Callvirt && instr.operand as MethodInfo == getHasEquipment)
                {
                    // Remove the original call and inject equivalent manual check
                    // stack: dropTarget
                    list[i] = new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(CardEffectAttachEquipmentPatches), nameof(CheckEquipmentLimit)));
                }
            }
            return list;
        }


        // Reflective replacement for GetHasEquipment
        public static bool CheckEquipmentLimit(CharacterState dropTarget)
        {
            if (dropTarget == null)
            {
                Plugin.Logger.LogError("dropTarget is null");
                return false;
            }

            // --- Access private property PrimaryStateInformation ---
            var psiProp = AccessTools.Property(typeof(CharacterState), "PrimaryStateInformation");
            var psi = psiProp?.GetValue(dropTarget);
            if (psi == null){
                Plugin.Logger.LogError("PrimaryStateInformation is null");
                return false;
            }

            // --- Access members on private sealed StateInformation class ---
            var psiType = psi.GetType();
            var equipmentField = AccessTools.Field(psiType, "equipment");
            var limitProp = AccessTools.Property(psiType, "EquipmentLimit");

            var equipment = equipmentField?.GetValue(psi) as System.Collections.ICollection;
            var limit = (int)(limitProp?.GetValue(psi) ?? 0);

            Plugin.Logger.LogInfo("equipment: " + equipment?.Count + " limit: " + limit);
            return equipment != null && equipment.Count >= limit;
        }
    }
}