﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Utils;
using Corsair.CUE.SDK;

namespace Aurora.Devices.Corsair
{
    internal static class LedMaps
    {
        internal static readonly Dictionary<CorsairLedId, DeviceKeys> KeyboardLedMap = new Dictionary<CorsairLedId, DeviceKeys>()
        {
            [CorsairLedId.CLK_Escape]			=                                     DeviceKeys.ESC,					
            [CorsairLedId.CLK_F1]			=                                         DeviceKeys.F1,					
            [CorsairLedId.CLK_F2]			=                                         DeviceKeys.F2,					
            [CorsairLedId.CLK_F3]			=                                         DeviceKeys.F3,					
            [CorsairLedId.CLK_F4]			=                                         DeviceKeys.F4,					
            [CorsairLedId.CLK_F5]			=                                         DeviceKeys.F5,					
            [CorsairLedId.CLK_F6]			=                                         DeviceKeys.F6,					
            [CorsairLedId.CLK_F7]			=                                         DeviceKeys.F7,					
            [CorsairLedId.CLK_F8]			=                                         DeviceKeys.F8,					
            [CorsairLedId.CLK_F9]			=                                         DeviceKeys.F9,					
            [CorsairLedId.CLK_F10]			=                                         DeviceKeys.F10,					
            [CorsairLedId.CLK_F11]			=                                         DeviceKeys.F11,					
            [CorsairLedId.CLK_GraveAccentAndTilde]		=                             DeviceKeys.TILDE,				
            [CorsairLedId.CLK_1]			=                                         DeviceKeys.ONE,					
            [CorsairLedId.CLK_2]			=                                         DeviceKeys.TWO,					
            [CorsairLedId.CLK_3]			=                                         DeviceKeys.THREE,				
            [CorsairLedId.CLK_4]			=                                         DeviceKeys.FOUR,				
            [CorsairLedId.CLK_5]			=                                         DeviceKeys.FIVE,				
            [CorsairLedId.CLK_6]			=                                         DeviceKeys.SIX,					
            [CorsairLedId.CLK_7]			=                                         DeviceKeys.SEVEN,				
            [CorsairLedId.CLK_8]			=                                         DeviceKeys.EIGHT,				
            [CorsairLedId.CLK_9]			=                                         DeviceKeys.NINE,				
            [CorsairLedId.CLK_0]			=                                         DeviceKeys.ZERO,				
            [CorsairLedId.CLK_MinusAndUnderscore]		=                             DeviceKeys.MINUS,				
            [CorsairLedId.CLK_Tab]			=                                         DeviceKeys.TAB,					
            [CorsairLedId.CLK_Q]			=                                         DeviceKeys.Q,					
            [CorsairLedId.CLK_W]			=                                         DeviceKeys.W,					
            [CorsairLedId.CLK_E]			=                                         DeviceKeys.E,					
            [CorsairLedId.CLK_R]			=                                         DeviceKeys.R,					
            [CorsairLedId.CLK_T]			=                                         DeviceKeys.T,					
            [CorsairLedId.CLK_Y]			=                                         DeviceKeys.Y,					
            [CorsairLedId.CLK_U]			=                                         DeviceKeys.U,					
            [CorsairLedId.CLK_I]			=                                         DeviceKeys.I,					
            [CorsairLedId.CLK_O]			=                                         DeviceKeys.O,					
            [CorsairLedId.CLK_P]			=                                         DeviceKeys.P,					
            [CorsairLedId.CLK_BracketLeft]			=                                 DeviceKeys.OPEN_BRACKET,		
            [CorsairLedId.CLK_CapsLock]			=                                     DeviceKeys.CAPS_LOCK,			
            [CorsairLedId.CLK_A]			=                                         DeviceKeys.A,					
            [CorsairLedId.CLK_S]			=                                         DeviceKeys.S,					
            [CorsairLedId.CLK_D]			=                                         DeviceKeys.D,					
            [CorsairLedId.CLK_F]			=                                         DeviceKeys.F,					
            [CorsairLedId.CLK_G]			=                                         DeviceKeys.G,					
            [CorsairLedId.CLK_H]			=                                         DeviceKeys.H,					
            [CorsairLedId.CLK_J]			=                                         DeviceKeys.J,					
            [CorsairLedId.CLK_K]			=                                         DeviceKeys.K,					
            [CorsairLedId.CLK_L]			=                                         DeviceKeys.L,					
            [CorsairLedId.CLK_SemicolonAndColon]		=                             DeviceKeys.SEMICOLON,			
            [CorsairLedId.CLK_ApostropheAndDoubleQuote]	=                             DeviceKeys.APOSTROPHE,			
            [CorsairLedId.CLK_LeftShift]			=                                 DeviceKeys.LEFT_SHIFT,			
            [CorsairLedId.CLK_NonUsBackslash]			=                             DeviceKeys.BACKSLASH_UK,		
            [CorsairLedId.CLK_Z]			=                                         DeviceKeys.Z,					
            [CorsairLedId.CLK_X]			=                                         DeviceKeys.X,					
            [CorsairLedId.CLK_C]			=                                         DeviceKeys.C,					
            [CorsairLedId.CLK_V]			=                                         DeviceKeys.V,					
            [CorsairLedId.CLK_B]			=                                         DeviceKeys.B,					
            [CorsairLedId.CLK_N]			=                                         DeviceKeys.N,					
            [CorsairLedId.CLK_M]			=                                         DeviceKeys.M,					
            [CorsairLedId.CLK_CommaAndLessThan]			=                             DeviceKeys.COMMA,				
            [CorsairLedId.CLK_PeriodAndBiggerThan]		=                             DeviceKeys.PERIOD,				
            [CorsairLedId.CLK_SlashAndQuestionMark]		=                             DeviceKeys.FORWARD_SLASH,		
            [CorsairLedId.CLK_LeftCtrl]			=                                     DeviceKeys.LEFT_CONTROL,		
            [CorsairLedId.CLK_LeftGui]			=                                     DeviceKeys.LEFT_WINDOWS,		
            [CorsairLedId.CLK_LeftAlt]			=                                     DeviceKeys.LEFT_ALT,			
            //[CorsairLedId.CLK_Lang2]			=                                     DeviceKeys.Lang2,				
            [CorsairLedId.CLK_Space]			=                                     DeviceKeys.SPACE,				
             //[CorsairLedId.CLK_Lang1]			=                                    DeviceKeys.Lang1,				
             // [CorsairLedId.CLK_International2]			=                           DeviceKeys.International2,	
            [CorsairLedId.CLK_RightAlt]			=                                     DeviceKeys.RIGHT_ALT,			
            [CorsairLedId.CLK_RightGui]			=                                     DeviceKeys.RIGHT_WINDOWS,		
            [CorsairLedId.CLK_Application]			=                                 DeviceKeys.APPLICATION_SELECT,	
            //[CorsairLedId.CLK_LedProgramming]			=                             DeviceKeys.LedProgramming,	
            [CorsairLedId.CLK_Brightness]			=                                 DeviceKeys.BRIGHTNESS_SWITCH,	
            [CorsairLedId.CLK_F12]			=                                         DeviceKeys.F12,					
            [CorsairLedId.CLK_PrintScreen]			=                                 DeviceKeys.PRINT_SCREEN,		
            [CorsairLedId.CLK_ScrollLock]			=                                 DeviceKeys.SCROLL_LOCK,			
            [CorsairLedId.CLK_PauseBreak]			=                                 DeviceKeys.PAUSE_BREAK,			
            [CorsairLedId.CLK_Insert]			=                                     DeviceKeys.INSERT,				
            [CorsairLedId.CLK_Home]			=                                         DeviceKeys.HOME,				
            [CorsairLedId.CLK_PageUp]			=                                     DeviceKeys.PAGE_UP,				
            [CorsairLedId.CLK_BracketRight]			=                                 DeviceKeys.CLOSE_BRACKET,		
            [CorsairLedId.CLK_Backslash]			=                                 DeviceKeys.BACKSLASH,			
            [CorsairLedId.CLK_NonUsTilde]			=                                 DeviceKeys.HASHTAG,				
            [CorsairLedId.CLK_Enter]			=                                     DeviceKeys.ENTER,				
            //[CorsairLedId.CLK_International1]			=                             DeviceKeys.International1,	
            [CorsairLedId.CLK_EqualsAndPlus]			=                             DeviceKeys.EQUALS,				
             //[CorsairLedId.CLK_International3]			=                            DeviceKeys.International3,	
            [CorsairLedId.CLK_Backspace]			=                                 DeviceKeys.BACKSPACE,			
            [CorsairLedId.CLK_Delete]			=                                     DeviceKeys.DELETE,				
            [CorsairLedId.CLK_End]			=                                         DeviceKeys.END,					
            [CorsairLedId.CLK_PageDown]			=                                     DeviceKeys.PAGE_DOWN,			
            [CorsairLedId.CLK_RightShift]			=                                 DeviceKeys.RIGHT_SHIFT,			
            [CorsairLedId.CLK_RightCtrl]			=                                 DeviceKeys.RIGHT_CONTROL,		
            [CorsairLedId.CLK_UpArrow]			=                                     DeviceKeys.ARROW_UP,			
            [CorsairLedId.CLK_LeftArrow]			=                                 DeviceKeys.ARROW_LEFT,			
            [CorsairLedId.CLK_DownArrow]			=                                 DeviceKeys.ARROW_DOWN,			
            [CorsairLedId.CLK_RightArrow]			=                                 DeviceKeys.ARROW_RIGHT,			
            [CorsairLedId.CLK_WinLock]			=                                     DeviceKeys.LOCK_SWITCH,			
            [CorsairLedId.CLK_Mute]			=                                         DeviceKeys.VOLUME_MUTE,			
            [CorsairLedId.CLK_Stop]			=                                         DeviceKeys.MEDIA_STOP,			
            [CorsairLedId.CLK_ScanPreviousTrack]	=                                 DeviceKeys.MEDIA_PREVIOUS,		
            [CorsairLedId.CLK_PlayPause]			=                                 DeviceKeys.MEDIA_PLAY_PAUSE,	
            [CorsairLedId.CLK_ScanNextTrack]		=                                 DeviceKeys.MEDIA_NEXT,			
            [CorsairLedId.CLK_NumLock]			=                                     DeviceKeys.NUM_LOCK,			
            [CorsairLedId.CLK_KeypadSlash]			=                                 DeviceKeys.NUM_SLASH,			
            [CorsairLedId.CLK_KeypadAsterisk]			=                             DeviceKeys.NUM_ASTERISK,		
            [CorsairLedId.CLK_KeypadMinus]			=                                 DeviceKeys.NUM_MINUS,			
            [CorsairLedId.CLK_KeypadPlus]			=                                 DeviceKeys.NUM_PLUS,			
            [CorsairLedId.CLK_KeypadEnter]			=                                 DeviceKeys.NUM_ENTER,			
            [CorsairLedId.CLK_Keypad7]			=                                     DeviceKeys.NUM_SEVEN,			
            [CorsairLedId.CLK_Keypad8]			=                                     DeviceKeys.NUM_EIGHT,			
            [CorsairLedId.CLK_Keypad9]			=                                     DeviceKeys.NUM_NINE,			
            [CorsairLedId.CLK_KeypadComma]			=                                 DeviceKeys.NUM_ZEROZERO,		
            [CorsairLedId.CLK_Keypad4]			=                                     DeviceKeys.NUM_FOUR,			
            [CorsairLedId.CLK_Keypad5]			=                                     DeviceKeys.NUM_FIVE,			
            [CorsairLedId.CLK_Keypad6]			=                                     DeviceKeys.NUM_SIX,				
            [CorsairLedId.CLK_Keypad1]			=                                     DeviceKeys.NUM_ONE,				
            [CorsairLedId.CLK_Keypad2]			=                                     DeviceKeys.NUM_TWO,				
            [CorsairLedId.CLK_Keypad3]			=                                     DeviceKeys.NUM_THREE,			
            [CorsairLedId.CLK_Keypad0]			=                                     DeviceKeys.NUM_ZERO,			
            [CorsairLedId.CLK_KeypadPeriodAndDelete] =                                DeviceKeys.NUM_PERIOD,			
            [CorsairLedId.CLK_G1]			=                                         DeviceKeys.G1,					
            [CorsairLedId.CLK_G2]			=                                         DeviceKeys.G2,					
            [CorsairLedId.CLK_G3]			=                                         DeviceKeys.G3,					
            [CorsairLedId.CLK_G4]			=                                         DeviceKeys.G4,					
            [CorsairLedId.CLK_G5]			=                                         DeviceKeys.G5,					
            [CorsairLedId.CLK_G6]			=                                         DeviceKeys.G6,					
            [CorsairLedId.CLK_G7]			=                                         DeviceKeys.G7,					
            [CorsairLedId.CLK_G8]			=                                         DeviceKeys.G8,					
            [CorsairLedId.CLK_G9]			=                                         DeviceKeys.G9,					
            [CorsairLedId.CLK_G10]			=                                         DeviceKeys.G10,					
            [CorsairLedId.CLK_VolumeUp]			=                                     DeviceKeys.VOLUME_UP,			
            [CorsairLedId.CLK_VolumeDown]		=                                     DeviceKeys.VOLUME_DOWN,
            //[CorsairLedId.CLK_MR]			=                                         DeviceKeys.MR,				
            //[CorsairLedId.CLK_M1]			=                                         //DeviceKeys.M1,				
            //[CorsairLedId.CLK_M2]			=                                         //DeviceKeys.M2,				
            //[CorsairLedId.CLK_M3]			=                                         //DeviceKeys.M3,				
            [CorsairLedId.CLK_G11]			=                                         DeviceKeys.G11,					
            [CorsairLedId.CLK_G12]			=                                         DeviceKeys.G12,					
            [CorsairLedId.CLK_G13]			=                                         DeviceKeys.G13,					
            [CorsairLedId.CLK_G14]			=                                         DeviceKeys.G14,					
            [CorsairLedId.CLK_G15]			=                                         DeviceKeys.G15,					
            [CorsairLedId.CLK_G16]			=                                         DeviceKeys.G16,					
            [CorsairLedId.CLK_G17]			=                                         DeviceKeys.G17,					
            [CorsairLedId.CLK_G18]			=                                         DeviceKeys.G18,
            //[CorsairLedId.CLK_International5]	=                                     //DeviceKeys.International5,	
            // [CorsairLedId.CLK_International4]	=                                     //DeviceKeys.International4,	
            [CorsairLedId.CLK_Fn]			    =                                     DeviceKeys.FN_Key,				
            [CorsairLedId.CLK_WinLock]			=                                     DeviceKeys.LOCK_SWITCH,			
            [CorsairLedId.CLK_Brightness]		=                                     DeviceKeys.BRIGHTNESS_SWITCH,	
            [CorsairLedId.CLK_Logo]			    =                                     DeviceKeys.LOGO,				
            [CorsairLedId.CLKLP_Zone1]			=                                     DeviceKeys.ADDITIONALLIGHT1,	
            [CorsairLedId.CLKLP_Zone2]			=                                     DeviceKeys.ADDITIONALLIGHT2,	
            [CorsairLedId.CLKLP_Zone3]			=                                     DeviceKeys.ADDITIONALLIGHT3,	
            [CorsairLedId.CLKLP_Zone4]			=                                     DeviceKeys.ADDITIONALLIGHT4,	
            [CorsairLedId.CLKLP_Zone5]			=                                     DeviceKeys.ADDITIONALLIGHT5,	
            [CorsairLedId.CLKLP_Zone6]			=                                     DeviceKeys.ADDITIONALLIGHT6,	
            [CorsairLedId.CLKLP_Zone7]			=                                     DeviceKeys.ADDITIONALLIGHT7,	
            [CorsairLedId.CLKLP_Zone8]			=                                     DeviceKeys.ADDITIONALLIGHT8,	
            [CorsairLedId.CLKLP_Zone9]			=                                     DeviceKeys.ADDITIONALLIGHT9,	
            [CorsairLedId.CLKLP_Zone10]			=                                     DeviceKeys.ADDITIONALLIGHT10,	
            [CorsairLedId.CLKLP_Zone11]			=                                     DeviceKeys.ADDITIONALLIGHT11,	
            [CorsairLedId.CLKLP_Zone12]			=                                     DeviceKeys.ADDITIONALLIGHT12,	
            [CorsairLedId.CLKLP_Zone13]			=                                     DeviceKeys.ADDITIONALLIGHT13,	
            [CorsairLedId.CLKLP_Zone14]			=                                     DeviceKeys.ADDITIONALLIGHT14,	
            [CorsairLedId.CLKLP_Zone15]			=                                     DeviceKeys.ADDITIONALLIGHT15,	
            [CorsairLedId.CLKLP_Zone16]			=                                     DeviceKeys.ADDITIONALLIGHT16,	
            [CorsairLedId.CLKLP_Zone17]			=                                     DeviceKeys.ADDITIONALLIGHT17,	
            [CorsairLedId.CLKLP_Zone18]			=                                     DeviceKeys.ADDITIONALLIGHT18,	
            [CorsairLedId.CLKLP_Zone19]         =                                     DeviceKeys.ADDITIONALLIGHT19,
        };

        /*internal static readonly Dictionary<DeviceKeys, CorsairLedId> MouseMatLedMap = new Dictionary<DeviceKeys, CorsairLedId>()
        {
            [DeviceKeys.MOUSEPADLIGHT1] = CorsairLedId.CLMM_Zone1,
            [DeviceKeys.MOUSEPADLIGHT2] = CorsairLedId.MM_Zone2,
            [DeviceKeys.MOUSEPADLIGHT3] = CorsairLedId.MM_Zone3,
            [DeviceKeys.MOUSEPADLIGHT4] = CorsairLedId.MM_Zone4,
            [DeviceKeys.MOUSEPADLIGHT5] = CorsairLedId.MM_Zone5,
            [DeviceKeys.MOUSEPADLIGHT6] = CorsairLedId.MM_Zone6,
            [DeviceKeys.MOUSEPADLIGHT7] = CorsairLedId.MM_Zone7,
            [DeviceKeys.MOUSEPADLIGHT8] = CorsairLedId.MM_Zone8,
            [DeviceKeys.MOUSEPADLIGHT9] = CorsairLedId.MM_Zone9,
            [DeviceKeys.MOUSEPADLIGHT10] = CorsairLedId.MM_Zone10,
            [DeviceKeys.MOUSEPADLIGHT11] = CorsairLedId.MM_Zone11,
            [DeviceKeys.MOUSEPADLIGHT12] = CorsairLedId.MM_Zone12,
            [DeviceKeys.MOUSEPADLIGHT13] = CorsairLedId.MM_Zone13,
            [DeviceKeys.MOUSEPADLIGHT14] = CorsairLedId.MM_Zone14,
            [DeviceKeys.MOUSEPADLIGHT15] = CorsairLedId.MM_Zone15
        };

        internal static readonly Dictionary<DeviceKeys, CorsairLedId> HeadsetStandLedMap = new Dictionary<DeviceKeys, CorsairLedId>()
        {
            [DeviceKeys.Peripheral_Logo] = CorsairLedId.HSS_Zone1,
            [DeviceKeys.MOUSEPADLIGHT15] = CorsairLedId.HSS_Zone2,
            [DeviceKeys.MOUSEPADLIGHT13] = CorsairLedId.HSS_Zone3,
            [DeviceKeys.MOUSEPADLIGHT11] = CorsairLedId.HSS_Zone4,
            [DeviceKeys.MOUSEPADLIGHT9] = CorsairLedId.HSS_Zone5,
            [DeviceKeys.MOUSEPADLIGHT7] = CorsairLedId.HSS_Zone6,
            [DeviceKeys.MOUSEPADLIGHT5] = CorsairLedId.HSS_Zone7,
            [DeviceKeys.MOUSEPADLIGHT3] = CorsairLedId.HSS_Zone8,
            [DeviceKeys.MOUSEPADLIGHT1] = CorsairLedId.HSS_Zone9,
        };

        internal static readonly Dictionary<DeviceKeys, CorsairLedId> MouseLedMap = new Dictionary<DeviceKeys, CorsairLedId>()
        {
            [DeviceKeys.Peripheral_Logo] = CorsairLedId.M_1,
            [DeviceKeys.Peripheral_FrontLight] = CorsairLedId.M_2,
            [DeviceKeys.Peripheral_ScrollWheel] = CorsairLedId.M_3,
            [DeviceKeys.ADDITIONALLIGHT1] = CorsairLedId.M_4,//TODO
            [DeviceKeys.ADDITIONALLIGHT2] = CorsairLedId.M_5,
            [DeviceKeys.ADDITIONALLIGHT3] = CorsairLedId.M_6
        };*/

        internal static readonly Dictionary<CorsairDeviceType, Dictionary<CorsairLedId, DeviceKeys>> MapsMap = new Dictionary<CorsairDeviceType, Dictionary<CorsairLedId, DeviceKeys>>()
        {
            [CorsairDeviceType.CDT_Keyboard] = KeyboardLedMap,
            /*[CorsairDeviceType.CDT_Mouse] = MouseLedMap,
            [CorsairDeviceType.CDT_MouseMat] = MouseMatLedMap,
            [CorsairDeviceType.CDT_HeadsetStand] = HeadsetStandLedMap,*/
        };

        internal static readonly List<CorsairLedId> Channel1LedIds = EnumUtils.GetEnumValues<CorsairLedId>()
            .Where(id => Enum.GetName(typeof(CorsairLedId), id).StartsWith("D_C1_"))
            .ToList();

        internal static readonly List<CorsairLedId> Channel2LedIds = EnumUtils.GetEnumValues<CorsairLedId>()
            .Where(id => Enum.GetName(typeof(CorsairLedId), id).StartsWith("D_C2_"))
            .ToList();

        internal static readonly List<CorsairLedId> Channel3LedIds = EnumUtils.GetEnumValues<CorsairLedId>()
            .Where(id => Enum.GetName(typeof(CorsairLedId), id).StartsWith("D_C3_"))
            .ToList();

        internal static readonly List<List<CorsairLedId>> ChannelLeds = new List<List<CorsairLedId>> 
        {
            Channel1LedIds,
            Channel2LedIds,
            Channel3LedIds
        };

        public static string ToString(this CorsairLedColor corsairLedColor) => $"{corsairLedColor.ledId}, ({corsairLedColor.r},{corsairLedColor.g},{corsairLedColor.b})";
    }
}
