﻿using Aurora.Devices;
using Aurora.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vulcan.NET;

namespace Device_Vulcan
{
    public class VulcanDeviceConnector : AuroraDeviceConnector
    {
        protected override string ConnectorName => "Vulcan";

        protected override List<AuroraDevice> GetDevices()
        {
            return new List<AuroraDevice>() { new VulcanDevice() };
        }

        protected override bool InitializeImpl() => VulcanKeyboard.Initialize();

        protected override void ShutdownImpl() => VulcanKeyboard.Disconnect();


       
    }
    public class VulcanDevice : AuroraKeyboardDevice
    {
        protected override string DeviceName => "Vulcan";

        protected override bool UpdateDeviceImpl(DeviceColorComposition composition)
        {
            if (!VulcanKeyboard.IsConnected)
                Disconnect();

            foreach (var key in composition.keyColors)
            {
                if (KeyMap.TryGetValue(key.Key, out var vulcanKey))
                    VulcanKeyboard.SetKeyColor(vulcanKey, CorrectAlpha(key.Value));
            }

            return VulcanKeyboard.Update();
        }
        public static Dictionary<DeviceKeys, Key> KeyMap = new Dictionary<DeviceKeys, Key>
        {
            [DeviceKeys.ESC] = Key.ESC,
            [DeviceKeys.TILDE] = Key.TILDE,
            [DeviceKeys.TAB] = Key.TAB,
            [DeviceKeys.LEFT_SHIFT] = Key.LEFT_SHIFT,
            [DeviceKeys.CAPS_LOCK] = Key.CAPS_LOCK,
            [DeviceKeys.LEFT_CONTROL] = Key.LEFT_CONTROL,
            [DeviceKeys.LEFT_WINDOWS] = Key.LEFT_WINDOWS,
            [DeviceKeys.ONE] = Key.ONE,
            [DeviceKeys.TWO] = Key.TWO,
            [DeviceKeys.THREE] = Key.THREE,
            [DeviceKeys.FOUR] = Key.FOUR,
            [DeviceKeys.FIVE] = Key.FIVE,
            [DeviceKeys.SIX] = Key.SIX,
            [DeviceKeys.SEVEN] = Key.SEVEN,
            [DeviceKeys.EIGHT] = Key.EIGHT,
            [DeviceKeys.NINE] = Key.NINE,
            [DeviceKeys.ZERO] = Key.ZERO,
            [DeviceKeys.F1] = Key.F1,
            [DeviceKeys.F2] = Key.F2,
            [DeviceKeys.F3] = Key.F3,
            [DeviceKeys.F4] = Key.F4,
            [DeviceKeys.F5] = Key.F5,
            [DeviceKeys.F6] = Key.F6,
            [DeviceKeys.F7] = Key.F7,
            [DeviceKeys.F8] = Key.F8,
            [DeviceKeys.F9] = Key.F9,
            [DeviceKeys.F10] = Key.F10,
            [DeviceKeys.F11] = Key.F11,
            [DeviceKeys.F12] = Key.F12,
            [DeviceKeys.Q] = Key.Q,
            [DeviceKeys.A] = Key.A,
            [DeviceKeys.W] = Key.W,
            [DeviceKeys.S] = Key.S,
            [DeviceKeys.Z] = Key.Z,
            [DeviceKeys.LEFT_ALT] = Key.LEFT_ALT,
            [DeviceKeys.E] = Key.E,
            [DeviceKeys.D] = Key.D,
            [DeviceKeys.X] = Key.X,
            [DeviceKeys.R] = Key.R,
            [DeviceKeys.C] = Key.C,
            [DeviceKeys.T] = Key.T,
            [DeviceKeys.G] = Key.G,
            [DeviceKeys.V] = Key.V,
            [DeviceKeys.Y] = Key.Y,
            [DeviceKeys.H] = Key.H,
            [DeviceKeys.B] = Key.B,
            [DeviceKeys.SPACE] = Key.SPACE,
            [DeviceKeys.U] = Key.U,
            [DeviceKeys.J] = Key.J,
            [DeviceKeys.N] = Key.N,
            [DeviceKeys.I] = Key.I,
            [DeviceKeys.K] = Key.K,
            [DeviceKeys.M] = Key.M,
            [DeviceKeys.O] = Key.O,
            [DeviceKeys.L] = Key.L,
            [DeviceKeys.F] = Key.F,
            [DeviceKeys.P] = Key.P,
            [DeviceKeys.COMMA] = Key.COMMA,
            [DeviceKeys.SEMICOLON] = Key.SEMICOLON,
            [DeviceKeys.PERIOD] = Key.PERIOD,
            [DeviceKeys.RIGHT_ALT] = Key.RIGHT_ALT,
            [DeviceKeys.MINUS] = Key.MINUS,
            [DeviceKeys.OPEN_BRACKET] = Key.OPEN_BRACKET,
            [DeviceKeys.APOSTROPHE] = Key.APOSTROPHE,
            [DeviceKeys.FORWARD_SLASH] = Key.FORWARD_SLASH,
            [DeviceKeys.FN_Key] = Key.FN_Key,
            [DeviceKeys.EQUALS] = Key.EQUALS,
            [DeviceKeys.CLOSE_BRACKET] = Key.CLOSE_BRACKET,
            [DeviceKeys.BACKSLASH] = Key.BACKSLASH,
            [DeviceKeys.RIGHT_SHIFT] = Key.RIGHT_SHIFT,
            [DeviceKeys.APPLICATION_SELECT] = Key.APPLICATION_SELECT,
            [DeviceKeys.BACKSPACE] = Key.BACKSPACE,
            [DeviceKeys.ENTER] = Key.ENTER,
            [DeviceKeys.RIGHT_CONTROL] = Key.RIGHT_CONTROL,
            [DeviceKeys.PRINT_SCREEN] = Key.PRINT_SCREEN,
            [DeviceKeys.INSERT] = Key.INSERT,
            [DeviceKeys.DELETE] = Key.DELETE,
            [DeviceKeys.ARROW_LEFT] = Key.ARROW_LEFT,
            [DeviceKeys.SCROLL_LOCK] = Key.SCROLL_LOCK,
            [DeviceKeys.HOME] = Key.HOME,
            [DeviceKeys.END] = Key.END,
            [DeviceKeys.ARROW_UP] = Key.ARROW_UP,
            [DeviceKeys.ARROW_DOWN] = Key.ARROW_DOWN,
            [DeviceKeys.PAUSE_BREAK] = Key.PAUSE_BREAK,
            [DeviceKeys.PAGE_UP] = Key.PAGE_UP,
            [DeviceKeys.PAGE_DOWN] = Key.PAGE_DOWN,
            [DeviceKeys.ARROW_RIGHT] = Key.ARROW_RIGHT,
            [DeviceKeys.NUM_LOCK] = Key.NUM_LOCK,
            [DeviceKeys.NUM_SEVEN] = Key.NUM_SEVEN,
            [DeviceKeys.NUM_FOUR] = Key.NUM_FOUR,
            [DeviceKeys.NUM_ONE] = Key.NUM_ONE,
            [DeviceKeys.NUM_ZERO] = Key.NUM_ZERO,
            [DeviceKeys.NUM_SLASH] = Key.NUM_SLASH,
            [DeviceKeys.NUM_EIGHT] = Key.NUM_EIGHT,
            [DeviceKeys.NUM_FIVE] = Key.NUM_FIVE,
            [DeviceKeys.NUM_TWO] = Key.NUM_TWO,
            [DeviceKeys.NUM_ASTERISK] = Key.NUM_ASTERISK,
            [DeviceKeys.NUM_NINE] = Key.NUM_NINE,
            [DeviceKeys.NUM_SIX] = Key.NUM_SIX,
            [DeviceKeys.NUM_THREE] = Key.NUM_THREE,
            [DeviceKeys.NUM_PERIOD] = Key.NUM_PERIOD,
            [DeviceKeys.NUM_MINUS] = Key.NUM_MINUS,
            [DeviceKeys.NUM_PLUS] = Key.NUM_PLUS,
            [DeviceKeys.NUM_ENTER] = Key.NUM_ENTER
        };
    }
}