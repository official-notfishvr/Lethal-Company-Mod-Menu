﻿using BepInEx;
using GameNetcodeStuff;
using HarmonyLib;
using Lethal_Company_Mod_Menu.MainMenu;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.Netcode;
using UnityEngine;

namespace LethalCompanyHacks.MainMenuPatch
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    public class PlayerControllerBPatch : MonoBehaviour
    {
        [HarmonyPatch(typeof(PlayerControllerB))]
        [HarmonyPatch("AllowPlayerDeath")]
        public static bool OverrideDeath() => !MainGUI.enableGod;


        [HarmonyPatch(typeof(PlayerControllerB))]
        [HarmonyPatch("Update")]
        public static void ReadInput(PlayerControllerB playerController)
        {
            foreach (FlashlightItem pocketedFlashlight in UnityEngine.Object.FindObjectsOfType<FlashlightItem>())
            {
                playerController.helmetLight.enabled = true;
                pocketedFlashlight.usingPlayerHelmetLight = false;
                pocketedFlashlight.UseItemOnClient(true);
                pocketedFlashlight.PocketFlashlightServerRpc(true);
            }
        }
    }
}
