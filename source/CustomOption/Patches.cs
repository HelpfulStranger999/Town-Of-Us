using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using TownOfUs.Services;
using UnhollowerBaseLib;
using Unity;
using UnityEngine;

namespace TownOfUs.CustomOption
{
    public static class Patches
    {
        public static ExportButton ExportButton;
        public static Import ImportButton;
        public static List<OptionBehaviour> DefaultOptions;
        public static float LobbyTextRowHeight { get; set; } = 0.081F;

        private static List<OptionBehaviour> CreateOptions(GameOptionsMenu __instance)
        {
            var options = new List<OptionBehaviour>
            {
                ExportButton.Render(),
                ImportButton.Render()
            };

            ExportButton.InitializeOption();
            ImportButton.InitializeOption();

            DefaultOptions = __instance.Children.ToList();
            foreach (var defaultOption in __instance.Children) options.Add(defaultOption);

            foreach (var option in CustomOptionBase.AllOptions)
            {
                options.Add(option.Render());
                option.InitializeOption();
            }

            return options;
        }

        private static bool OnEnable(OptionBehaviour opt)
        {
            if (opt == ExportButton.Setting)
            {
                ExportButton.InitializeOption();
                return false;
            }

            if (opt == ImportButton.Setting)
            {
                ImportButton.InitializeOption();
                return false;
            }

            var customOption =
                CustomOptionBase.AllOptions.FirstOrDefault(option =>
                    option.Setting == opt); // Works but may need to change to gameObject.name check

            if (customOption == null)
            {
                customOption = ExportButton.SlotButtons.FirstOrDefault(option => option.Setting == opt);
                if (customOption == null)
                {
                    customOption = ImportButton.SlotButtons.FirstOrDefault(option => option.Setting == opt);
                    if (customOption == null) return true;
                }
            }

            customOption.InitializeOption();

            return false;
        }

        [HarmonyPatch(typeof(GameOptionsMenu), nameof(GameOptionsMenu.Start))]
        private class GameOptionsMenu_Start
        {
            public static void Postfix(GameOptionsMenu __instance)
            {
                var customOptions = CreateOptions(__instance);

                var initialPosition = new Vector3(__instance.Children.First().transform.position.x,
                    __instance.Children.Max(option => option.transform.position.y),
                    __instance.Children.First().transform.position.z);

                __instance.Children = new Il2CppReferenceArray<OptionBehaviour>(customOptions.ToArray());
            }
        }

        [HarmonyPatch(typeof(GameOptionsMenu), nameof(GameOptionsMenu.Update))]
        private class GameOptionsMenu_Update
        {
            public static void Postfix(GameOptionsMenu __instance)
            {
                var i = 0;
                foreach (var option in __instance.Children)
                    option.transform.localPosition -= new Vector3(0, 0.5f * i++, 0);
            }
        }

        [HarmonyPatch(typeof(ToggleOption), nameof(ToggleOption.OnEnable))]
        private static class ToggleOption_OnEnable
        {
            private static bool Prefix(ToggleOption __instance)
            {
                return OnEnable(__instance);
            }
        }

        [HarmonyPatch(typeof(NumberOption), nameof(NumberOption.OnEnable))]
        private static class NumberOption_OnEnable
        {
            private static bool Prefix(NumberOption __instance)
            {
                return OnEnable(__instance);
            }
        }

        [HarmonyPatch(typeof(StringOption), nameof(StringOption.OnEnable))]
        private static class StringOption_OnEnable
        {
            private static bool Prefix(StringOption __instance)
            {
                return OnEnable(__instance);
            }
        }

        [HarmonyPatch(typeof(ToggleOption), nameof(ToggleOption.Toggle))]
        private class ToggleButtonPatch
        {
            public static bool Prefix(ToggleOption __instance)
            {
                var menuService = PluginSingleton<TownOfUs>.Instance.Container.Resolve<MenuService>();
                var currentMenu = menuService.CurrentMenu;

                var option = currentMenu.FindOption(__instance);

                // Needs further refactoring
                if (option is CustomHeaderOption)
                {
                    return false;
                }
                else if (option is CustomToggleOption toggle)
                {
                    toggle.Toggle();
                    return false;
                }
                else if (option is CustomButtonOption button)
                {
                    button.Click();
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        [HarmonyPatch(typeof(NumberOption), nameof(NumberOption.Increase))]
        private class NumberOptionPatchIncrease
        {
            public static bool Prefix(NumberOption __instance)
            {
                var option =
                    CustomOptionBase.AllOptions.FirstOrDefault(option =>
                        option.Setting == __instance); // Works but may need to change to gameObject.name check
                if (option is CustomNumberOption number)
                {
                    number.Increase();
                    return false;
                }

                return true;
            }
        }

        [HarmonyPatch(typeof(NumberOption), nameof(NumberOption.Decrease))]
        private class NumberOptionPatchDecrease
        {
            public static bool Prefix(NumberOption __instance)
            {
                var option =
                    CustomOptionBase.AllOptions.FirstOrDefault(option =>
                        option.Setting == __instance); // Works but may need to change to gameObject.name check
                if (option is CustomNumberOption number)
                {
                    number.Decrease();
                    return false;
                }

                return true;
            }
        }

        [HarmonyPatch(typeof(StringOption), nameof(StringOption.Increase))]
        private class StringOptionPatchIncrease
        {
            public static bool Prefix(StringOption __instance)
            {
                var option =
                    CustomOptionBase.AllOptions.FirstOrDefault(option =>
                        option.Setting == __instance); // Works but may need to change to gameObject.name check
                if (option is CustomStringOption str)
                {
                    str.Increase();
                    return false;
                }

                return true;
            }
        }

        [HarmonyPatch(typeof(StringOption), nameof(StringOption.Decrease))]
        private class StringOptionPatchDecrease
        {
            public static bool Prefix(StringOption __instance)
            {
                var option =
                    CustomOptionBase.AllOptions.FirstOrDefault(option =>
                        option.Setting == __instance); // Works but may need to change to gameObject.name check
                if (option is CustomStringOption str)
                {
                    str.Decrease();
                    return false;
                }

                return true;
            }
        }

        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSyncSettings))]
        private class PlayerControlPatch
        {
            public static void Postfix()
            {
                if (PlayerControl.AllPlayerControls.Count < 2 || !AmongUsClient.Instance ||
                    !PlayerControl.LocalPlayer || !AmongUsClient.Instance.AmHost) return;

                Rpc.SendRpc();
            }
        }

        [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
        private class HudManagerUpdate
        {
            private const float
                MinX = -5.233334F /*-5.3F*/,
                OriginalY = 2.9F,
                MinY = 3F; // Differs to cause excess options to appear cut off to encourage scrolling

            private static Scroller Scroller;
            private static Vector3 LastPosition = new Vector3(MinX, MinY);

            public static void Prefix(HudManager __instance)
            {
                if (__instance.GameSettings?.transform == null) return;

                // Scroller disabled
                if (!CustomOptionBase.LobbyTextScroller)
                {
                    // Remove scroller if disabled late
                    if (Scroller != null)
                    {
                        __instance.GameSettings.transform.SetParent(Scroller.transform.parent);
                        __instance.GameSettings.transform.localPosition = new Vector3(MinX, OriginalY);

                        Object.Destroy(Scroller);
                    }

                    return;
                }

                CreateScroller(__instance);

                Scroller.gameObject.SetActive(__instance.GameSettings.gameObject.activeSelf);

                if (!Scroller.gameObject.active) return;

                var rows = __instance.GameSettings.text.Count(c => c == '\n');
                var maxY = Mathf.Max(MinY, rows * LobbyTextRowHeight + (rows - 38) * LobbyTextRowHeight);

                Scroller.YBounds = new FloatRange(MinY, maxY);

                // Prevent scrolling when the player is interacting with a menu
                if (PlayerControl.LocalPlayer?.CanMove != true)
                {
                    __instance.GameSettings.transform.localPosition = LastPosition;

                    return;
                }

                if (__instance.GameSettings.transform.localPosition.x != MinX ||
                    __instance.GameSettings.transform.localPosition.y < MinY) return;

                LastPosition = __instance.GameSettings.transform.localPosition;
            }

            private static void CreateScroller(HudManager __instance)
            {
                if (Scroller != null) return;

                Scroller = new GameObject("SettingsScroller").AddComponent<Scroller>();
                Scroller.transform.SetParent(__instance.GameSettings.transform.parent);
                Scroller.gameObject.layer = 5;

                Scroller.transform.localScale = Vector3.one;
                Scroller.allowX = false;
                Scroller.allowY = true;
                Scroller.active = true;
                Scroller.velocity = new Vector2(0, 0);
                Scroller.ScrollerYRange = new FloatRange(0, 0);
                Scroller.XBounds = new FloatRange(MinX, MinX);
                Scroller.enabled = true;

                Scroller.Inner = __instance.GameSettings.transform;
                __instance.GameSettings.transform.SetParent(Scroller.transform);
            }
        }
    }
}