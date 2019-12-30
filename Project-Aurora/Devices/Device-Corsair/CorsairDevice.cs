using Aurora.Devices;
using CorsairRGB.NET;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace Device_Corsair
{
    public class CorsairDevice : Device
    {
        protected override string DeviceName => "Corsair";
        private readonly List<CorsairDeviceInfo> deviceInfos = new List<CorsairDeviceInfo>();

        public override string GetDeviceDetails() => isInitialized ?
                                                    DeviceName + ": " + string.Join(" ", deviceInfos.Select(d => d.Model)) :
                                                    DeviceName + ": Not Initialized";

        public override bool Initialize()
        {
            CUE.PerformProtocolHandshake();
            var error = CUE.GetLastError();
            if (error != CorsairError.Success)
            {
                LogError("Corsair Error: " + error);
                return isInitialized = false;
            }

            int count = CUE.GetDeviceCount();
            for (int i = 0; i < count; i++)
                deviceInfos.Add(CUE.GetDeviceInfo(i));

            if (!CUE.RequestControl())
            {
                LogError("Error requesting cuesdk exclusive control:" + CUE.GetLastError());
                return isInitialized = false;
            }

            return isInitialized = true;
        }

        public override void Shutdown()
        {
            isInitialized = false;
            deviceInfos.Clear();
            CUE.ReleaseControl();
        }

        public override bool UpdateDevice(Dictionary<DeviceKeys, Color> keyColors, DoWorkEventArgs e, bool forced = false)
        {
            List<CorsairLedColor> colors = new List<CorsairLedColor>();
            foreach (var asd in keyColors)
            {
                if (KeyMap.TryGetValue(asd.Key, out var ledid))
                {
                    colors.Add(new CorsairLedColor()
                    {
                        LedId = ledid,
                        R = asd.Value.R,
                        G = asd.Value.G,
                        B = asd.Value.B
                    });
                }
            }

            CUE.SetLedColors(colors.ToArray());
            return CUE.Update();
        }

        private static readonly Dictionary<DeviceKeys, CorsairLedId> KeyMap = new Dictionary<DeviceKeys, CorsairLedId>()
        {
            [DeviceKeys.ESC] = CorsairLedId.K_Escape,
            [DeviceKeys.F1] = CorsairLedId.K_F1,
            [DeviceKeys.F2] = CorsairLedId.K_F2,
            [DeviceKeys.F3] = CorsairLedId.K_F3,
            [DeviceKeys.F4] = CorsairLedId.K_F4,
            [DeviceKeys.F5] = CorsairLedId.K_F5,
            [DeviceKeys.F6] = CorsairLedId.K_F6,
            [DeviceKeys.F7] = CorsairLedId.K_F7,
            [DeviceKeys.F8] = CorsairLedId.K_F8,
            [DeviceKeys.F9] = CorsairLedId.K_F9,
            [DeviceKeys.F10] = CorsairLedId.K_F10,
            [DeviceKeys.F11] = CorsairLedId.K_F11,
            [DeviceKeys.TILDE] = CorsairLedId.K_GraveAccentAndTilde,
            [DeviceKeys.ONE] = CorsairLedId.K_1,
            [DeviceKeys.TWO] = CorsairLedId.K_2,
            [DeviceKeys.THREE] = CorsairLedId.K_3,
            [DeviceKeys.FOUR] = CorsairLedId.K_4,
            [DeviceKeys.FIVE] = CorsairLedId.K_5,
            [DeviceKeys.SIX] = CorsairLedId.K_6,
            [DeviceKeys.SEVEN] = CorsairLedId.K_7,
            [DeviceKeys.EIGHT] = CorsairLedId.K_8,
            [DeviceKeys.NINE] = CorsairLedId.K_9,
            [DeviceKeys.ZERO] = CorsairLedId.K_0,
            [DeviceKeys.MINUS] = CorsairLedId.K_MinusAndUnderscore,
            [DeviceKeys.TAB] = CorsairLedId.K_Tab,
            [DeviceKeys.Q] = CorsairLedId.K_Q,
            [DeviceKeys.W] = CorsairLedId.K_W,
            [DeviceKeys.E] = CorsairLedId.K_E,
            [DeviceKeys.R] = CorsairLedId.K_R,
            [DeviceKeys.T] = CorsairLedId.K_T,
            [DeviceKeys.Y] = CorsairLedId.K_Y,
            [DeviceKeys.U] = CorsairLedId.K_U,
            [DeviceKeys.I] = CorsairLedId.K_I,
            [DeviceKeys.O] = CorsairLedId.K_O,
            [DeviceKeys.P] = CorsairLedId.K_P,
            [DeviceKeys.OPEN_BRACKET] = CorsairLedId.K_BracketLeft,
            [DeviceKeys.CAPS_LOCK] = CorsairLedId.K_CapsLock,
            [DeviceKeys.A] = CorsairLedId.K_A,
            [DeviceKeys.S] = CorsairLedId.K_S,
            [DeviceKeys.D] = CorsairLedId.K_D,
            [DeviceKeys.F] = CorsairLedId.K_F,
            [DeviceKeys.G] = CorsairLedId.K_G,
            [DeviceKeys.H] = CorsairLedId.K_H,
            [DeviceKeys.J] = CorsairLedId.K_J,
            [DeviceKeys.K] = CorsairLedId.K_K,
            [DeviceKeys.L] = CorsairLedId.K_L,
            [DeviceKeys.SEMICOLON] = CorsairLedId.K_SemicolonAndColon,
            [DeviceKeys.APOSTROPHE] = CorsairLedId.K_ApostropheAndDoubleQuote,
            [DeviceKeys.LEFT_SHIFT] = CorsairLedId.K_LeftShift,
            [DeviceKeys.BACKSLASH_UK] = CorsairLedId.K_NonUsBackslash,
            [DeviceKeys.Z] = CorsairLedId.K_Z,
            [DeviceKeys.X] = CorsairLedId.K_X,
            [DeviceKeys.C] = CorsairLedId.K_C,
            [DeviceKeys.V] = CorsairLedId.K_V,
            [DeviceKeys.B] = CorsairLedId.K_B,
            [DeviceKeys.N] = CorsairLedId.K_N,
            [DeviceKeys.M] = CorsairLedId.K_M,
            [DeviceKeys.COMMA] = CorsairLedId.K_CommaAndLessThan,
            [DeviceKeys.PERIOD] = CorsairLedId.K_PeriodAndBiggerThan,
            [DeviceKeys.FORWARD_SLASH] = CorsairLedId.K_SlashAndQuestionMark,
            [DeviceKeys.LEFT_CONTROL] = CorsairLedId.K_LeftCtrl,
            [DeviceKeys.LEFT_WINDOWS] = CorsairLedId.K_LeftGui,
            [DeviceKeys.LEFT_ALT] = CorsairLedId.K_LeftAlt,
            // [DeviceKeys.Lang2] = CorsairLedId.K_Lang2,
            [DeviceKeys.SPACE] = CorsairLedId.K_Space,
            // [DeviceKeys.Lang1] = CorsairLedId.K_Lang1,
            //   [DeviceKeys.International2] = CorsairLedId.K_International2,
            [DeviceKeys.RIGHT_ALT] = CorsairLedId.K_RightAlt,
            [DeviceKeys.RIGHT_WINDOWS] = CorsairLedId.K_RightGui,
            [DeviceKeys.APPLICATION_SELECT] = CorsairLedId.K_Application,
            //  [DeviceKeys.LedProgramming] = CorsairLedId.K_LedProgramming,
            //[DeviceKeys.Brightness] = CorsairLedId.K_Brightness,
            [DeviceKeys.F12] = CorsairLedId.K_F12,
            [DeviceKeys.PRINT_SCREEN] = CorsairLedId.K_PrintScreen,
            [DeviceKeys.SCROLL_LOCK] = CorsairLedId.K_ScrollLock,
            [DeviceKeys.PAUSE_BREAK] = CorsairLedId.K_PauseBreak,
            [DeviceKeys.INSERT] = CorsairLedId.K_Insert,
            [DeviceKeys.HOME] = CorsairLedId.K_Home,
            [DeviceKeys.PAGE_UP] = CorsairLedId.K_PageUp,
            [DeviceKeys.CLOSE_BRACKET] = CorsairLedId.K_BracketRight,
            [DeviceKeys.BACKSLASH] = CorsairLedId.K_Backslash,
            [DeviceKeys.HASHTAG] = CorsairLedId.K_NonUsTilde,
            [DeviceKeys.ENTER] = CorsairLedId.K_Enter,
            // [DeviceKeys.International1] = CorsairLedId.K_International1,
            [DeviceKeys.EQUALS] = CorsairLedId.K_EqualsAndPlus,
            //   [DeviceKeys.International3] = CorsairLedId.K_International3,
            [DeviceKeys.BACKSPACE] = CorsairLedId.K_Backspace,
            [DeviceKeys.DELETE] = CorsairLedId.K_Delete,
            [DeviceKeys.END] = CorsairLedId.K_End,
            [DeviceKeys.PAGE_DOWN] = CorsairLedId.K_PageDown,
            [DeviceKeys.RIGHT_SHIFT] = CorsairLedId.K_RightShift,
            [DeviceKeys.RIGHT_CONTROL] = CorsairLedId.K_RightCtrl,
            [DeviceKeys.ARROW_UP] = CorsairLedId.K_UpArrow,
            [DeviceKeys.ARROW_LEFT] = CorsairLedId.K_LeftArrow,
            [DeviceKeys.ARROW_DOWN] = CorsairLedId.K_DownArrow,
            [DeviceKeys.ARROW_RIGHT] = CorsairLedId.K_RightArrow,
            //[DeviceKeys.] = CorsairLedId.K_WinLock,
            [DeviceKeys.VOLUME_MUTE] = CorsairLedId.K_Mute,
            [DeviceKeys.MEDIA_STOP] = CorsairLedId.K_Stop,
            [DeviceKeys.MEDIA_PREVIOUS] = CorsairLedId.K_ScanPreviousTrack,
            [DeviceKeys.MEDIA_PLAY_PAUSE] = CorsairLedId.K_PlayPause,
            [DeviceKeys.MEDIA_NEXT] = CorsairLedId.K_ScanNextTrack,
            [DeviceKeys.NUM_LOCK] = CorsairLedId.K_NumLock,
            [DeviceKeys.NUM_SLASH] = CorsairLedId.K_KeypadSlash,
            [DeviceKeys.NUM_ASTERISK] = CorsairLedId.K_KeypadAsterisk,
            [DeviceKeys.NUM_MINUS] = CorsairLedId.K_KeypadMinus,
            [DeviceKeys.NUM_PLUS] = CorsairLedId.K_KeypadPlus,
            [DeviceKeys.NUM_ENTER] = CorsairLedId.K_KeypadEnter,
            [DeviceKeys.NUM_SEVEN] = CorsairLedId.K_Keypad7,
            [DeviceKeys.NUM_EIGHT] = CorsairLedId.K_Keypad8,
            [DeviceKeys.NUM_NINE] = CorsairLedId.K_Keypad9,
            [DeviceKeys.NUM_ZEROZERO] = CorsairLedId.K_KeypadComma,
            [DeviceKeys.NUM_FOUR] = CorsairLedId.K_Keypad4,
            [DeviceKeys.NUM_FIVE] = CorsairLedId.K_Keypad5,
            [DeviceKeys.NUM_SIX] = CorsairLedId.K_Keypad6,
            [DeviceKeys.NUM_ONE] = CorsairLedId.K_Keypad1,
            [DeviceKeys.NUM_TWO] = CorsairLedId.K_Keypad2,
            [DeviceKeys.NUM_THREE] = CorsairLedId.K_Keypad3,
            [DeviceKeys.NUM_ZERO] = CorsairLedId.K_Keypad0,
            [DeviceKeys.NUM_PERIOD] = CorsairLedId.K_KeypadPeriodAndDelete,
            [DeviceKeys.G1] = CorsairLedId.K_G1,
            [DeviceKeys.G2] = CorsairLedId.K_G2,
            [DeviceKeys.G3] = CorsairLedId.K_G3,
            [DeviceKeys.G4] = CorsairLedId.K_G4,
            [DeviceKeys.G5] = CorsairLedId.K_G5,
            [DeviceKeys.G6] = CorsairLedId.K_G6,
            [DeviceKeys.G7] = CorsairLedId.K_G7,
            [DeviceKeys.G8] = CorsairLedId.K_G8,
            [DeviceKeys.G9] = CorsairLedId.K_G9,
            [DeviceKeys.G10] = CorsairLedId.K_G10,
            [DeviceKeys.VOLUME_UP] = CorsairLedId.K_VolumeUp,
            [DeviceKeys.VOLUME_DOWN] = CorsairLedId.K_VolumeDown,
            // [DeviceKeys.MR] = CorsairLedId.K_MR,
            // [DeviceKeys.M1] = CorsairLedId.K_M1,
            //[DeviceKeys.M2] = CorsairLedId.K_M2,
            // [DeviceKeys.M3] = CorsairLedId.K_M3,
            [DeviceKeys.G11] = CorsairLedId.K_G11,
            [DeviceKeys.G12] = CorsairLedId.K_G12,
            [DeviceKeys.G13] = CorsairLedId.K_G13,
            [DeviceKeys.G14] = CorsairLedId.K_G14,
            [DeviceKeys.G15] = CorsairLedId.K_G15,
            [DeviceKeys.G16] = CorsairLedId.K_G16,
            [DeviceKeys.G17] = CorsairLedId.K_G17,
            [DeviceKeys.G18] = CorsairLedId.K_G18,
            //  [DeviceKeys.International5] = CorsairLedId.K_International5,
            //  [DeviceKeys.International4] = CorsairLedId.K_International4,
            [DeviceKeys.FN_Key] = CorsairLedId.K_Fn
        };
    }
}
