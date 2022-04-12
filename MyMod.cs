using System.Linq;
using System;
using MelonLoader;
using ActionMenuApi.Api;
using System.Runtime.InteropServices;

[assembly: MelonInfo(typeof(meow.MyMod), "Media controll", "1.1", "Geri")]
[assembly: MelonColor(ConsoleColor.Yellow)]
[assembly: MelonGame("VRChat", "VRChat")]

namespace meow

{
    public class MyMod : MelonMod
    {
        public static MelonLogger.Instance Logger;

        public const int KEYEVENTF_EXTENTEDKEY = 1;
        public const int KEYEVENTF_KEYUP = 0;
        public const int VK_MEDIA_NEXT_TRACK = 0xB0;
        public const int VK_MEDIA_PLAY_PAUSE = 0xB3;
        public const int VK_MEDIA_PREV_TRACK = 0xB1;

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, IntPtr extraInfo);

        public override void OnApplicationStart()
        {

            Logger = new MelonLogger.Instance("Media_controll", ConsoleColor.Yellow);

            if (MelonHandler.Mods.Any(mods => mods.Info.Name == "ActionMenuApi"))
            {
                init_ama();
                Logger.Msg("Initialized");
            }
            else
            {
                Logger.Msg("required mod missing: ActionMenuApi");
                Logger.Msg("https://github.com/gompocp/ActionMenuApi");
            }
        }
        public static void init_ama()
        {
            VRCActionMenuPage.AddSubMenu(ActionMenuPage.Main, "<color=#00fbff>Media ctrl</color>", () => menu() );
        }
        public static void menu()
        {
            CustomSubMenu.AddButton("NextTrack", () =>
            {
                keybd_event(VK_MEDIA_NEXT_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
            });
            CustomSubMenu.AddButton("Play/Pause", () =>
            {
                keybd_event(VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
            });
                        CustomSubMenu.AddButton("PrevTrack", () =>
            {
                keybd_event(VK_MEDIA_PREV_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
            });
        }
    }
}