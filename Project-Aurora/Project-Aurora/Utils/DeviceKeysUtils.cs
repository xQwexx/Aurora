using Aurora.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Utils
{
    public static class DeviceKeysUtils
    {
        public static class Logitech
        {
            public enum keyboardNames
            {
                ESC = 0x01,
                F1 = 0x3b,
                F2 = 0x3c,
                F3 = 0x3d,
                F4 = 0x3e,
                F5 = 0x3f,
                F6 = 0x40,
                F7 = 0x41,
                F8 = 0x42,
                F9 = 0x43,
                F10 = 0x44,
                F11 = 0x57,
                F12 = 0x58,
                PRINT_SCREEN = 0x137,
                SCROLL_LOCK = 0x46,
                PAUSE_BREAK = 0x45,
                TILDE = 0x29,
                ONE = 0x02,
                TWO = 0x03,
                THREE = 0x04,
                FOUR = 0x05,
                FIVE = 0x06,
                SIX = 0x07,
                SEVEN = 0x08,
                EIGHT = 0x09,
                NINE = 0x0A,
                ZERO = 0x0B,
                MINUS = 0x0C,
                EQUALS = 0x0D,
                BACKSPACE = 0x0E,
                INSERT = 0x152,
                HOME = 0x147,
                PAGE_UP = 0x149,
                NUM_LOCK = 0x145,
                NUM_SLASH = 0x135,
                NUM_ASTERISK = 0x37,
                NUM_MINUS = 0x4A,
                TAB = 0x0F,
                Q = 0x10,
                W = 0x11,
                E = 0x12,
                R = 0x13,
                T = 0x14,
                Y = 0x15,
                U = 0x16,
                I = 0x17,
                O = 0x18,
                P = 0x19,
                OPEN_BRACKET = 0x1A,
                CLOSE_BRACKET = 0x1B,
                BACKSLASH = 0x2B,
                KEYBOARD_DELETE = 0x153,
                END = 0x14F,
                PAGE_DOWN = 0x151,
                NUM_SEVEN = 0x47,
                NUM_EIGHT = 0x48,
                NUM_NINE = 0x49,
                NUM_PLUS = 0x4E,
                CAPS_LOCK = 0x3A,
                A = 0x1E,
                S = 0x1F,
                D = 0x20,
                F = 0x21,
                G = 0x22,
                H = 0x23,
                J = 0x24,
                K = 0x25,
                L = 0x26,
                SEMICOLON = 0x27,
                APOSTROPHE = 0x28,
                ENTER = 0x1C,
                NUM_FOUR = 0x4B,
                NUM_FIVE = 0x4C,
                NUM_SIX = 0x4D,
                LEFT_SHIFT = 0x2A,
                Z = 0x2C,
                X = 0x2D,
                C = 0x2E,
                V = 0x2F,
                B = 0x30,
                N = 0x31,
                M = 0x32,
                COMMA = 0x33,
                PERIOD = 0x34,
                FORWARD_SLASH = 0x35,
                RIGHT_SHIFT = 0x36,
                ARROW_UP = 0x148,
                NUM_ONE = 0x4F,
                NUM_TWO = 0x50,
                NUM_THREE = 0x51,
                NUM_ENTER = 0x11C,
                LEFT_CONTROL = 0x1D,
                LEFT_WINDOWS = 0x15B,
                LEFT_ALT = 0x38,
                SPACE = 0x39,
                RIGHT_ALT = 0x138,
                RIGHT_WINDOWS = 0x15C,
                APPLICATION_SELECT = 0x15D,
                RIGHT_CONTROL = 0x11D,
                ARROW_LEFT = 0x14B,
                ARROW_DOWN = 0x150,
                ARROW_RIGHT = 0x14D,
                NUM_ZERO = 0x52,
                NUM_PERIOD = 0x53,
                G_1 = 0xFFF1,
                G_2 = 0xFFF2,
                G_3 = 0xFFF3,
                G_4 = 0xFFF4,
                G_5 = 0xFFF5,
                G_6 = 0xFFF6,
                G_7 = 0xFFF7,
                G_8 = 0xFFF8,
                G_9 = 0xFFF9,
                G_LOGO = 0xFFFF1,
                G_BADGE = 0xFFFF2
            };

            public enum keyboardBitmapKeys
            {
                UNKNOWN = -1,
                ESC = 0,
                F1 = 4,
                F2 = 8,
                F3 = 12,
                F4 = 16,
                F5 = 20,
                F6 = 24,
                F7 = 28,
                F8 = 32,
                F9 = 36,
                F10 = 40,
                F11 = 44,
                F12 = 48,
                PRINT_SCREEN = 52,
                SCROLL_LOCK = 56,
                PAUSE_BREAK = 60,
                //64
                //68
                //72
                //76
                //80

                TILDE = 84,
                ONE = 88,
                TWO = 92,
                THREE = 96,
                FOUR = 100,
                FIVE = 104,
                SIX = 108,
                SEVEN = 112,
                EIGHT = 116,
                NINE = 120,
                ZERO = 124,
                MINUS = 128,
                EQUALS = 132,
                BACKSPACE = 136,
                INSERT = 140,
                HOME = 144,
                PAGE_UP = 148,
                NUM_LOCK = 152,
                NUM_SLASH = 156,
                NUM_ASTERISK = 160,
                NUM_MINUS = 164,

                TAB = 168,
                Q = 172,
                W = 176,
                E = 180,
                R = 184,
                T = 188,
                Y = 192,
                U = 196,
                I = 200,
                O = 204,
                P = 208,
                OPEN_BRACKET = 212,
                CLOSE_BRACKET = 216,
                BACKSLASH = 220,
                KEYBOARD_DELETE = 224,
                END = 228,
                PAGE_DOWN = 232,
                NUM_SEVEN = 236,
                NUM_EIGHT = 240,
                NUM_NINE = 244,
                NUM_PLUS = 248,

                CAPS_LOCK = 252,
                A = 256,
                S = 260,
                D = 264,
                F = 268,
                G = 272,
                H = 276,
                J = 280,
                K = 284,
                L = 288,
                SEMICOLON = 292,
                APOSTROPHE = 296,
                HASHTAG = 300,//300
                ENTER = 304,
                //308
                //312
                //316
                NUM_FOUR = 320,
                NUM_FIVE = 324,
                NUM_SIX = 328,
                //332

                LEFT_SHIFT = 336,
                BACKSLASH_UK = 340,
                Z = 344,
                X = 348,
                C = 352,
                V = 356,
                B = 360,
                N = 364,
                M = 368,
                COMMA = 372,
                PERIOD = 376,
                FORWARD_SLASH = 380,
                OEM102 = 384,
                RIGHT_SHIFT = 388,
                //392
                ARROW_UP = 396,
                //400
                NUM_ONE = 404,
                NUM_TWO = 408,
                NUM_THREE = 412,
                NUM_ENTER = 416,

                LEFT_CONTROL = 420,
                LEFT_WINDOWS = 424,
                LEFT_ALT = 428,
                //432
                JPN_MUHENKAN = 436,//436
                SPACE = 440,
                //444
                //448
                JPN_HENKAN = 452,//452
                JPN_HIRAGANA_KATAKANA = 456,//456
                                            //460
                RIGHT_ALT = 464,
                RIGHT_WINDOWS = 468,
                APPLICATION_SELECT = 472,
                RIGHT_CONTROL = 476,
                ARROW_LEFT = 480,
                ARROW_DOWN = 484,
                ARROW_RIGHT = 488,
                NUM_ZERO = 492,
                NUM_PERIOD = 496,
                //500
            };

            public static DeviceKeys ToDeviceKey(keyboardNames key)
            {
                switch (key)
                {
                    case (keyboardNames.ESC):
                        return DeviceKeys.ESC;
                    case (keyboardNames.F1):
                        return DeviceKeys.F1;
                    case (keyboardNames.F2):
                        return DeviceKeys.F2;
                    case (keyboardNames.F3):
                        return DeviceKeys.F3;
                    case (keyboardNames.F4):
                        return DeviceKeys.F4;
                    case (keyboardNames.F5):
                        return DeviceKeys.F5;
                    case (keyboardNames.F6):
                        return DeviceKeys.F6;
                    case (keyboardNames.F7):
                        return DeviceKeys.F7;
                    case (keyboardNames.F8):
                        return DeviceKeys.F8;
                    case (keyboardNames.F9):
                        return DeviceKeys.F9;
                    case (keyboardNames.F10):
                        return DeviceKeys.F10;
                    case (keyboardNames.F11):
                        return DeviceKeys.F11;
                    case (keyboardNames.F12):
                        return DeviceKeys.F12;
                    case (keyboardNames.PRINT_SCREEN):
                        return DeviceKeys.PRINT_SCREEN;
                    case (keyboardNames.SCROLL_LOCK):
                        return DeviceKeys.SCROLL_LOCK;
                    case (keyboardNames.PAUSE_BREAK):
                        return DeviceKeys.PAUSE_BREAK;
                    case (keyboardNames.TILDE):
                        return DeviceKeys.TILDE;
                    case (keyboardNames.ONE):
                        return DeviceKeys.ONE;
                    case (keyboardNames.TWO):
                        return DeviceKeys.TWO;
                    case (keyboardNames.THREE):
                        return DeviceKeys.THREE;
                    case (keyboardNames.FOUR):
                        return DeviceKeys.FOUR;
                    case (keyboardNames.FIVE):
                        return DeviceKeys.FIVE;
                    case (keyboardNames.SIX):
                        return DeviceKeys.SIX;
                    case (keyboardNames.SEVEN):
                        return DeviceKeys.SEVEN;
                    case (keyboardNames.EIGHT):
                        return DeviceKeys.EIGHT;
                    case (keyboardNames.NINE):
                        return DeviceKeys.NINE;
                    case (keyboardNames.ZERO):
                        return DeviceKeys.ZERO;
                    case (keyboardNames.MINUS):
                        return DeviceKeys.MINUS;
                    case (keyboardNames.EQUALS):
                        return DeviceKeys.EQUALS;
                    case (keyboardNames.BACKSPACE):
                        return DeviceKeys.BACKSPACE;
                    case (keyboardNames.INSERT):
                        return DeviceKeys.INSERT;
                    case (keyboardNames.HOME):
                        return DeviceKeys.HOME;
                    case (keyboardNames.PAGE_UP):
                        return DeviceKeys.PAGE_UP;
                    case (keyboardNames.NUM_LOCK):
                        return DeviceKeys.NUM_LOCK;
                    case (keyboardNames.NUM_SLASH):
                        return DeviceKeys.NUM_SLASH;
                    case (keyboardNames.NUM_ASTERISK):
                        return DeviceKeys.NUM_ASTERISK;
                    case (keyboardNames.NUM_MINUS):
                        return DeviceKeys.NUM_MINUS;
                    case (keyboardNames.TAB):
                        return DeviceKeys.TAB;
                    case (keyboardNames.Q):
                        return DeviceKeys.Q;
                    case (keyboardNames.W):
                        return DeviceKeys.W;
                    case (keyboardNames.E):
                        return DeviceKeys.E;
                    case (keyboardNames.R):
                        return DeviceKeys.R;
                    case (keyboardNames.T):
                        return DeviceKeys.T;
                    case (keyboardNames.Y):
                        return DeviceKeys.Y;
                    case (keyboardNames.U):
                        return DeviceKeys.U;
                    case (keyboardNames.I):
                        return DeviceKeys.I;
                    case (keyboardNames.O):
                        return DeviceKeys.O;
                    case (keyboardNames.P):
                        return DeviceKeys.P;
                    case (keyboardNames.OPEN_BRACKET):
                        return DeviceKeys.OPEN_BRACKET;
                    case (keyboardNames.CLOSE_BRACKET):
                        return DeviceKeys.CLOSE_BRACKET;
                    case (keyboardNames.BACKSLASH):
                        return DeviceKeys.BACKSLASH;
                    case (keyboardNames.KEYBOARD_DELETE):
                        return DeviceKeys.DELETE;
                    case (keyboardNames.END):
                        return DeviceKeys.END;
                    case (keyboardNames.PAGE_DOWN):
                        return DeviceKeys.PAGE_DOWN;
                    case (keyboardNames.NUM_SEVEN):
                        return DeviceKeys.NUM_SEVEN;
                    case (keyboardNames.NUM_EIGHT):
                        return DeviceKeys.NUM_EIGHT;
                    case (keyboardNames.NUM_NINE):
                        return DeviceKeys.NUM_NINE;
                    case (keyboardNames.NUM_PLUS):
                        return DeviceKeys.NUM_PLUS;
                    case (keyboardNames.CAPS_LOCK):
                        return DeviceKeys.CAPS_LOCK;
                    case (keyboardNames.A):
                        return DeviceKeys.A;
                    case (keyboardNames.S):
                        return DeviceKeys.S;
                    case (keyboardNames.D):
                        return DeviceKeys.D;
                    case (keyboardNames.F):
                        return DeviceKeys.F;
                    case (keyboardNames.G):
                        return DeviceKeys.G;
                    case (keyboardNames.H):
                        return DeviceKeys.H;
                    case (keyboardNames.J):
                        return DeviceKeys.J;
                    case (keyboardNames.K):
                        return DeviceKeys.K;
                    case (keyboardNames.L):
                        return DeviceKeys.L;
                    case (keyboardNames.SEMICOLON):
                        return DeviceKeys.SEMICOLON;
                    case (keyboardNames.APOSTROPHE):
                        return DeviceKeys.APOSTROPHE;
                    //case (keyboardNames.HASHTAG):
                    //    return DeviceKeys.HASHTAG;
                    case (keyboardNames.ENTER):
                        return DeviceKeys.ENTER;
                    case (keyboardNames.NUM_FOUR):
                        return DeviceKeys.NUM_FOUR;
                    case (keyboardNames.NUM_FIVE):
                        return DeviceKeys.NUM_FIVE;
                    case (keyboardNames.NUM_SIX):
                        return DeviceKeys.NUM_SIX;
                    case (keyboardNames.LEFT_SHIFT):
                        return DeviceKeys.LEFT_SHIFT;
                    //case (keyboardNames.BACKSLASH_UK):
                    //    return DeviceKeys.BACKSLASH_UK;
                    case (keyboardNames.Z):
                        return DeviceKeys.Z;
                    case (keyboardNames.X):
                        return DeviceKeys.X;
                    case (keyboardNames.C):
                        return DeviceKeys.C;
                    case (keyboardNames.V):
                        return DeviceKeys.V;
                    case (keyboardNames.B):
                        return DeviceKeys.B;
                    case (keyboardNames.N):
                        return DeviceKeys.N;
                    case (keyboardNames.M):
                        return DeviceKeys.M;
                    case (keyboardNames.COMMA):
                        return DeviceKeys.COMMA;
                    case (keyboardNames.PERIOD):
                        return DeviceKeys.PERIOD;
                    case (keyboardNames.FORWARD_SLASH):
                        return DeviceKeys.FORWARD_SLASH;
                    case (keyboardNames.RIGHT_SHIFT):
                        return DeviceKeys.RIGHT_SHIFT;
                    case (keyboardNames.ARROW_UP):
                        return DeviceKeys.ARROW_UP;
                    case (keyboardNames.NUM_ONE):
                        return DeviceKeys.NUM_ONE;
                    case (keyboardNames.NUM_TWO):
                        return DeviceKeys.NUM_TWO;
                    case (keyboardNames.NUM_THREE):
                        return DeviceKeys.NUM_THREE;
                    case (keyboardNames.NUM_ENTER):
                        return DeviceKeys.NUM_ENTER;
                    case (keyboardNames.LEFT_CONTROL):
                        return DeviceKeys.LEFT_CONTROL;
                    case (keyboardNames.LEFT_WINDOWS):
                        return DeviceKeys.LEFT_WINDOWS;
                    case (keyboardNames.LEFT_ALT):
                        return DeviceKeys.LEFT_ALT;
                    case (keyboardNames.SPACE):
                        return DeviceKeys.SPACE;
                    case (keyboardNames.RIGHT_ALT):
                        return DeviceKeys.RIGHT_ALT;
                    case (keyboardNames.RIGHT_WINDOWS):
                        return DeviceKeys.RIGHT_WINDOWS;
                    case (keyboardNames.APPLICATION_SELECT):
                        return DeviceKeys.APPLICATION_SELECT;
                    case (keyboardNames.RIGHT_CONTROL):
                        return DeviceKeys.RIGHT_CONTROL;
                    case (keyboardNames.ARROW_LEFT):
                        return DeviceKeys.ARROW_LEFT;
                    case (keyboardNames.ARROW_DOWN):
                        return DeviceKeys.ARROW_DOWN;
                    case (keyboardNames.ARROW_RIGHT):
                        return DeviceKeys.ARROW_RIGHT;
                    case (keyboardNames.NUM_ZERO):
                        return DeviceKeys.NUM_ZERO;
                    case (keyboardNames.NUM_PERIOD):
                        return DeviceKeys.NUM_PERIOD;
                    default:
                        return DeviceKeys.NONE;
                }
            }

            public static keyboardBitmapKeys ToLogitechBitmap(DeviceKeys key)
            {
                switch (key)
                {
                    case (DeviceKeys.ESC):
                        return keyboardBitmapKeys.ESC;
                    case (DeviceKeys.F1):
                        return keyboardBitmapKeys.F1;
                    case (DeviceKeys.F2):
                        return keyboardBitmapKeys.F2;
                    case (DeviceKeys.F3):
                        return keyboardBitmapKeys.F3;
                    case (DeviceKeys.F4):
                        return keyboardBitmapKeys.F4;
                    case (DeviceKeys.F5):
                        return keyboardBitmapKeys.F5;
                    case (DeviceKeys.F6):
                        return keyboardBitmapKeys.F6;
                    case (DeviceKeys.F7):
                        return keyboardBitmapKeys.F7;
                    case (DeviceKeys.F8):
                        return keyboardBitmapKeys.F8;
                    case (DeviceKeys.F9):
                        return keyboardBitmapKeys.F9;
                    case (DeviceKeys.F10):
                        return keyboardBitmapKeys.F10;
                    case (DeviceKeys.F11):
                        return keyboardBitmapKeys.F11;
                    case (DeviceKeys.F12):
                        return keyboardBitmapKeys.F12;
                    case (DeviceKeys.PRINT_SCREEN):
                        return keyboardBitmapKeys.PRINT_SCREEN;
                    case (DeviceKeys.SCROLL_LOCK):
                        return keyboardBitmapKeys.SCROLL_LOCK;
                    case (DeviceKeys.PAUSE_BREAK):
                        return keyboardBitmapKeys.PAUSE_BREAK;
                    case (DeviceKeys.JPN_HALFFULLWIDTH):
                        return keyboardBitmapKeys.TILDE;
                    case (DeviceKeys.OEM5):
                        if (Global.kbLayout.Loaded_Localization == Settings.PreferredKeyboardLocalization.jpn)
                            return keyboardBitmapKeys.UNKNOWN;
                        else
                            return keyboardBitmapKeys.TILDE;
                    case (DeviceKeys.TILDE):
                        return keyboardBitmapKeys.TILDE;
                    case (DeviceKeys.ONE):
                        return keyboardBitmapKeys.ONE;
                    case (DeviceKeys.TWO):
                        return keyboardBitmapKeys.TWO;
                    case (DeviceKeys.THREE):
                        return keyboardBitmapKeys.THREE;
                    case (DeviceKeys.FOUR):
                        return keyboardBitmapKeys.FOUR;
                    case (DeviceKeys.FIVE):
                        return keyboardBitmapKeys.FIVE;
                    case (DeviceKeys.SIX):
                        return keyboardBitmapKeys.SIX;
                    case (DeviceKeys.SEVEN):
                        return keyboardBitmapKeys.SEVEN;
                    case (DeviceKeys.EIGHT):
                        return keyboardBitmapKeys.EIGHT;
                    case (DeviceKeys.NINE):
                        return keyboardBitmapKeys.NINE;
                    case (DeviceKeys.ZERO):
                        return keyboardBitmapKeys.ZERO;
                    case (DeviceKeys.MINUS):
                        return keyboardBitmapKeys.MINUS;
                    case (DeviceKeys.EQUALS):
                        return keyboardBitmapKeys.EQUALS;
                    case (DeviceKeys.BACKSPACE):
                        return keyboardBitmapKeys.BACKSPACE;
                    case (DeviceKeys.INSERT):
                        return keyboardBitmapKeys.INSERT;
                    case (DeviceKeys.HOME):
                        return keyboardBitmapKeys.HOME;
                    case (DeviceKeys.PAGE_UP):
                        return keyboardBitmapKeys.PAGE_UP;
                    case (DeviceKeys.NUM_LOCK):
                        return keyboardBitmapKeys.NUM_LOCK;
                    case (DeviceKeys.NUM_SLASH):
                        return keyboardBitmapKeys.NUM_SLASH;
                    case (DeviceKeys.NUM_ASTERISK):
                        return keyboardBitmapKeys.NUM_ASTERISK;
                    case (DeviceKeys.NUM_MINUS):
                        return keyboardBitmapKeys.NUM_MINUS;
                    case (DeviceKeys.TAB):
                        return keyboardBitmapKeys.TAB;
                    case (DeviceKeys.Q):
                        return keyboardBitmapKeys.Q;
                    case (DeviceKeys.W):
                        return keyboardBitmapKeys.W;
                    case (DeviceKeys.E):
                        return keyboardBitmapKeys.E;
                    case (DeviceKeys.R):
                        return keyboardBitmapKeys.R;
                    case (DeviceKeys.T):
                        return keyboardBitmapKeys.T;
                    case (DeviceKeys.Y):
                        return keyboardBitmapKeys.Y;
                    case (DeviceKeys.U):
                        return keyboardBitmapKeys.U;
                    case (DeviceKeys.I):
                        return keyboardBitmapKeys.I;
                    case (DeviceKeys.O):
                        return keyboardBitmapKeys.O;
                    case (DeviceKeys.P):
                        return keyboardBitmapKeys.P;
                    case (DeviceKeys.OPEN_BRACKET):
                        return keyboardBitmapKeys.OPEN_BRACKET;
                    case (DeviceKeys.CLOSE_BRACKET):
                        return keyboardBitmapKeys.CLOSE_BRACKET;
                    case (DeviceKeys.BACKSLASH):
                        return keyboardBitmapKeys.BACKSLASH;
                    case (DeviceKeys.DELETE):
                        return keyboardBitmapKeys.KEYBOARD_DELETE;
                    case (DeviceKeys.END):
                        return keyboardBitmapKeys.END;
                    case (DeviceKeys.PAGE_DOWN):
                        return keyboardBitmapKeys.PAGE_DOWN;
                    case (DeviceKeys.NUM_SEVEN):
                        return keyboardBitmapKeys.NUM_SEVEN;
                    case (DeviceKeys.NUM_EIGHT):
                        return keyboardBitmapKeys.NUM_EIGHT;
                    case (DeviceKeys.NUM_NINE):
                        return keyboardBitmapKeys.NUM_NINE;
                    case (DeviceKeys.NUM_PLUS):
                        return keyboardBitmapKeys.NUM_PLUS;
                    case (DeviceKeys.CAPS_LOCK):
                        return keyboardBitmapKeys.CAPS_LOCK;
                    case (DeviceKeys.A):
                        return keyboardBitmapKeys.A;
                    case (DeviceKeys.S):
                        return keyboardBitmapKeys.S;
                    case (DeviceKeys.D):
                        return keyboardBitmapKeys.D;
                    case (DeviceKeys.F):
                        return keyboardBitmapKeys.F;
                    case (DeviceKeys.G):
                        return keyboardBitmapKeys.G;
                    case (DeviceKeys.H):
                        return keyboardBitmapKeys.H;
                    case (DeviceKeys.J):
                        return keyboardBitmapKeys.J;
                    case (DeviceKeys.K):
                        return keyboardBitmapKeys.K;
                    case (DeviceKeys.L):
                        return keyboardBitmapKeys.L;
                    case (DeviceKeys.SEMICOLON):
                        return keyboardBitmapKeys.SEMICOLON;
                    case (DeviceKeys.APOSTROPHE):
                        return keyboardBitmapKeys.APOSTROPHE;
                    case (DeviceKeys.HASHTAG):
                        return keyboardBitmapKeys.HASHTAG;
                    case (DeviceKeys.ENTER):
                        return keyboardBitmapKeys.ENTER;
                    case (DeviceKeys.NUM_FOUR):
                        return keyboardBitmapKeys.NUM_FOUR;
                    case (DeviceKeys.NUM_FIVE):
                        return keyboardBitmapKeys.NUM_FIVE;
                    case (DeviceKeys.NUM_SIX):
                        return keyboardBitmapKeys.NUM_SIX;
                    case (DeviceKeys.LEFT_SHIFT):
                        return keyboardBitmapKeys.LEFT_SHIFT;
                    case (DeviceKeys.BACKSLASH_UK):
                        if (Global.kbLayout.Loaded_Localization == Settings.PreferredKeyboardLocalization.jpn)
                            return keyboardBitmapKeys.OEM102;
                        else
                            return keyboardBitmapKeys.BACKSLASH_UK;
                    case (DeviceKeys.Z):
                        return keyboardBitmapKeys.Z;
                    case (DeviceKeys.X):
                        return keyboardBitmapKeys.X;
                    case (DeviceKeys.C):
                        return keyboardBitmapKeys.C;
                    case (DeviceKeys.V):
                        return keyboardBitmapKeys.V;
                    case (DeviceKeys.B):
                        return keyboardBitmapKeys.B;
                    case (DeviceKeys.N):
                        return keyboardBitmapKeys.N;
                    case (DeviceKeys.M):
                        return keyboardBitmapKeys.M;
                    case (DeviceKeys.COMMA):
                        return keyboardBitmapKeys.COMMA;
                    case (DeviceKeys.PERIOD):
                        return keyboardBitmapKeys.PERIOD;
                    case (DeviceKeys.FORWARD_SLASH):
                        return keyboardBitmapKeys.FORWARD_SLASH;
                    case (DeviceKeys.OEM8):
                        return keyboardBitmapKeys.FORWARD_SLASH;
                    case (DeviceKeys.OEM102):
                        return keyboardBitmapKeys.OEM102;
                    case (DeviceKeys.RIGHT_SHIFT):
                        return keyboardBitmapKeys.RIGHT_SHIFT;
                    case (DeviceKeys.ARROW_UP):
                        return keyboardBitmapKeys.ARROW_UP;
                    case (DeviceKeys.NUM_ONE):
                        return keyboardBitmapKeys.NUM_ONE;
                    case (DeviceKeys.NUM_TWO):
                        return keyboardBitmapKeys.NUM_TWO;
                    case (DeviceKeys.NUM_THREE):
                        return keyboardBitmapKeys.NUM_THREE;
                    case (DeviceKeys.NUM_ENTER):
                        return keyboardBitmapKeys.NUM_ENTER;
                    case (DeviceKeys.LEFT_CONTROL):
                        return keyboardBitmapKeys.LEFT_CONTROL;
                    case (DeviceKeys.LEFT_WINDOWS):
                        return keyboardBitmapKeys.LEFT_WINDOWS;
                    case (DeviceKeys.LEFT_ALT):
                        return keyboardBitmapKeys.LEFT_ALT;
                    case (DeviceKeys.JPN_MUHENKAN):
                        return keyboardBitmapKeys.JPN_MUHENKAN;
                    case (DeviceKeys.SPACE):
                        return keyboardBitmapKeys.SPACE;
                    case (DeviceKeys.JPN_HENKAN):
                        return keyboardBitmapKeys.JPN_HENKAN;
                    case (DeviceKeys.JPN_HIRAGANA_KATAKANA):
                        return keyboardBitmapKeys.JPN_HIRAGANA_KATAKANA;
                    case (DeviceKeys.RIGHT_ALT):
                        return keyboardBitmapKeys.RIGHT_ALT;
                    case (DeviceKeys.RIGHT_WINDOWS):
                        return keyboardBitmapKeys.RIGHT_WINDOWS;
                    case (DeviceKeys.FN_Key):
                        return keyboardBitmapKeys.RIGHT_WINDOWS;
                    case (DeviceKeys.APPLICATION_SELECT):
                        return keyboardBitmapKeys.APPLICATION_SELECT;
                    case (DeviceKeys.RIGHT_CONTROL):
                        return keyboardBitmapKeys.RIGHT_CONTROL;
                    case (DeviceKeys.ARROW_LEFT):
                        return keyboardBitmapKeys.ARROW_LEFT;
                    case (DeviceKeys.ARROW_DOWN):
                        return keyboardBitmapKeys.ARROW_DOWN;
                    case (DeviceKeys.ARROW_RIGHT):
                        return keyboardBitmapKeys.ARROW_RIGHT;
                    case (DeviceKeys.NUM_ZERO):
                        return keyboardBitmapKeys.NUM_ZERO;
                    case (DeviceKeys.NUM_PERIOD):
                        return keyboardBitmapKeys.NUM_PERIOD;
                    default:
                        return keyboardBitmapKeys.UNKNOWN;
                }
            }

            public static keyboardBitmapKeys ToLogitechBitmap(keyboardNames key)
            {
                switch (key)
                {
                    case (keyboardNames.ESC):
                        return keyboardBitmapKeys.ESC;
                    case (keyboardNames.F1):
                        return keyboardBitmapKeys.F1;
                    case (keyboardNames.F2):
                        return keyboardBitmapKeys.F2;
                    case (keyboardNames.F3):
                        return keyboardBitmapKeys.F3;
                    case (keyboardNames.F4):
                        return keyboardBitmapKeys.F4;
                    case (keyboardNames.F5):
                        return keyboardBitmapKeys.F5;
                    case (keyboardNames.F6):
                        return keyboardBitmapKeys.F6;
                    case (keyboardNames.F7):
                        return keyboardBitmapKeys.F7;
                    case (keyboardNames.F8):
                        return keyboardBitmapKeys.F8;
                    case (keyboardNames.F9):
                        return keyboardBitmapKeys.F9;
                    case (keyboardNames.F10):
                        return keyboardBitmapKeys.F10;
                    case (keyboardNames.F11):
                        return keyboardBitmapKeys.F11;
                    case (keyboardNames.F12):
                        return keyboardBitmapKeys.F12;
                    case (keyboardNames.PRINT_SCREEN):
                        return keyboardBitmapKeys.PRINT_SCREEN;
                    case (keyboardNames.SCROLL_LOCK):
                        return keyboardBitmapKeys.SCROLL_LOCK;
                    case (keyboardNames.PAUSE_BREAK):
                        return keyboardBitmapKeys.PAUSE_BREAK;
                    case (keyboardNames.TILDE):
                        return keyboardBitmapKeys.TILDE;
                    case (keyboardNames.ONE):
                        return keyboardBitmapKeys.ONE;
                    case (keyboardNames.TWO):
                        return keyboardBitmapKeys.TWO;
                    case (keyboardNames.THREE):
                        return keyboardBitmapKeys.THREE;
                    case (keyboardNames.FOUR):
                        return keyboardBitmapKeys.FOUR;
                    case (keyboardNames.FIVE):
                        return keyboardBitmapKeys.FIVE;
                    case (keyboardNames.SIX):
                        return keyboardBitmapKeys.SIX;
                    case (keyboardNames.SEVEN):
                        return keyboardBitmapKeys.SEVEN;
                    case (keyboardNames.EIGHT):
                        return keyboardBitmapKeys.EIGHT;
                    case (keyboardNames.NINE):
                        return keyboardBitmapKeys.NINE;
                    case (keyboardNames.ZERO):
                        return keyboardBitmapKeys.ZERO;
                    case (keyboardNames.MINUS):
                        return keyboardBitmapKeys.MINUS;
                    case (keyboardNames.EQUALS):
                        return keyboardBitmapKeys.EQUALS;
                    case (keyboardNames.BACKSPACE):
                        return keyboardBitmapKeys.BACKSPACE;
                    case (keyboardNames.INSERT):
                        return keyboardBitmapKeys.INSERT;
                    case (keyboardNames.HOME):
                        return keyboardBitmapKeys.HOME;
                    case (keyboardNames.PAGE_UP):
                        return keyboardBitmapKeys.PAGE_UP;
                    case (keyboardNames.NUM_LOCK):
                        return keyboardBitmapKeys.NUM_LOCK;
                    case (keyboardNames.NUM_SLASH):
                        return keyboardBitmapKeys.NUM_SLASH;
                    case (keyboardNames.NUM_ASTERISK):
                        return keyboardBitmapKeys.NUM_ASTERISK;
                    case (keyboardNames.NUM_MINUS):
                        return keyboardBitmapKeys.NUM_MINUS;
                    case (keyboardNames.TAB):
                        return keyboardBitmapKeys.TAB;
                    case (keyboardNames.Q):
                        return keyboardBitmapKeys.Q;
                    case (keyboardNames.W):
                        return keyboardBitmapKeys.W;
                    case (keyboardNames.E):
                        return keyboardBitmapKeys.E;
                    case (keyboardNames.R):
                        return keyboardBitmapKeys.R;
                    case (keyboardNames.T):
                        return keyboardBitmapKeys.T;
                    case (keyboardNames.Y):
                        return keyboardBitmapKeys.Y;
                    case (keyboardNames.U):
                        return keyboardBitmapKeys.U;
                    case (keyboardNames.I):
                        return keyboardBitmapKeys.I;
                    case (keyboardNames.O):
                        return keyboardBitmapKeys.O;
                    case (keyboardNames.P):
                        return keyboardBitmapKeys.P;
                    case (keyboardNames.OPEN_BRACKET):
                        return keyboardBitmapKeys.OPEN_BRACKET;
                    case (keyboardNames.CLOSE_BRACKET):
                        return keyboardBitmapKeys.CLOSE_BRACKET;
                    case (keyboardNames.BACKSLASH):
                        return keyboardBitmapKeys.BACKSLASH;
                    case (keyboardNames.KEYBOARD_DELETE):
                        return keyboardBitmapKeys.KEYBOARD_DELETE;
                    case (keyboardNames.END):
                        return keyboardBitmapKeys.END;
                    case (keyboardNames.PAGE_DOWN):
                        return keyboardBitmapKeys.PAGE_DOWN;
                    case (keyboardNames.NUM_SEVEN):
                        return keyboardBitmapKeys.NUM_SEVEN;
                    case (keyboardNames.NUM_EIGHT):
                        return keyboardBitmapKeys.NUM_EIGHT;
                    case (keyboardNames.NUM_NINE):
                        return keyboardBitmapKeys.NUM_NINE;
                    case (keyboardNames.NUM_PLUS):
                        return keyboardBitmapKeys.NUM_PLUS;
                    case (keyboardNames.CAPS_LOCK):
                        return keyboardBitmapKeys.CAPS_LOCK;
                    case (keyboardNames.A):
                        return keyboardBitmapKeys.A;
                    case (keyboardNames.S):
                        return keyboardBitmapKeys.S;
                    case (keyboardNames.D):
                        return keyboardBitmapKeys.D;
                    case (keyboardNames.F):
                        return keyboardBitmapKeys.F;
                    case (keyboardNames.G):
                        return keyboardBitmapKeys.G;
                    case (keyboardNames.H):
                        return keyboardBitmapKeys.H;
                    case (keyboardNames.J):
                        return keyboardBitmapKeys.J;
                    case (keyboardNames.K):
                        return keyboardBitmapKeys.K;
                    case (keyboardNames.L):
                        return keyboardBitmapKeys.L;
                    case (keyboardNames.SEMICOLON):
                        return keyboardBitmapKeys.SEMICOLON;
                    case (keyboardNames.APOSTROPHE):
                        return keyboardBitmapKeys.APOSTROPHE;
                    //case (keyboardNames.HASHTAG):
                    //    return Logitech_keyboardBitmapKeys.HASHTAG;
                    case (keyboardNames.ENTER):
                        return keyboardBitmapKeys.ENTER;
                    case (keyboardNames.NUM_FOUR):
                        return keyboardBitmapKeys.NUM_FOUR;
                    case (keyboardNames.NUM_FIVE):
                        return keyboardBitmapKeys.NUM_FIVE;
                    case (keyboardNames.NUM_SIX):
                        return keyboardBitmapKeys.NUM_SIX;
                    case (keyboardNames.LEFT_SHIFT):
                        return keyboardBitmapKeys.LEFT_SHIFT;
                    //case (keyboardNames.BACKSLASH_UK):
                    //    return Logitech_keyboardBitmapKeys.BACKSLASH_UK;
                    case (keyboardNames.Z):
                        return keyboardBitmapKeys.Z;
                    case (keyboardNames.X):
                        return keyboardBitmapKeys.X;
                    case (keyboardNames.C):
                        return keyboardBitmapKeys.C;
                    case (keyboardNames.V):
                        return keyboardBitmapKeys.V;
                    case (keyboardNames.B):
                        return keyboardBitmapKeys.B;
                    case (keyboardNames.N):
                        return keyboardBitmapKeys.N;
                    case (keyboardNames.M):
                        return keyboardBitmapKeys.M;
                    case (keyboardNames.COMMA):
                        return keyboardBitmapKeys.COMMA;
                    case (keyboardNames.PERIOD):
                        return keyboardBitmapKeys.PERIOD;
                    case (keyboardNames.FORWARD_SLASH):
                        return keyboardBitmapKeys.FORWARD_SLASH;
                    case (keyboardNames.RIGHT_SHIFT):
                        return keyboardBitmapKeys.RIGHT_SHIFT;
                    case (keyboardNames.ARROW_UP):
                        return keyboardBitmapKeys.ARROW_UP;
                    case (keyboardNames.NUM_ONE):
                        return keyboardBitmapKeys.NUM_ONE;
                    case (keyboardNames.NUM_TWO):
                        return keyboardBitmapKeys.NUM_TWO;
                    case (keyboardNames.NUM_THREE):
                        return keyboardBitmapKeys.NUM_THREE;
                    case (keyboardNames.NUM_ENTER):
                        return keyboardBitmapKeys.NUM_ENTER;
                    case (keyboardNames.LEFT_CONTROL):
                        return keyboardBitmapKeys.LEFT_CONTROL;
                    case (keyboardNames.LEFT_WINDOWS):
                        return keyboardBitmapKeys.LEFT_WINDOWS;
                    case (keyboardNames.LEFT_ALT):
                        return keyboardBitmapKeys.LEFT_ALT;
                    case (keyboardNames.SPACE):
                        return keyboardBitmapKeys.SPACE;
                    case (keyboardNames.RIGHT_ALT):
                        return keyboardBitmapKeys.RIGHT_ALT;
                    case (keyboardNames.RIGHT_WINDOWS):
                        return keyboardBitmapKeys.RIGHT_WINDOWS;
                    case (keyboardNames.APPLICATION_SELECT):
                        return keyboardBitmapKeys.APPLICATION_SELECT;
                    case (keyboardNames.RIGHT_CONTROL):
                        return keyboardBitmapKeys.RIGHT_CONTROL;
                    case (keyboardNames.ARROW_LEFT):
                        return keyboardBitmapKeys.ARROW_LEFT;
                    case (keyboardNames.ARROW_DOWN):
                        return keyboardBitmapKeys.ARROW_DOWN;
                    case (keyboardNames.ARROW_RIGHT):
                        return keyboardBitmapKeys.ARROW_RIGHT;
                    case (keyboardNames.NUM_ZERO):
                        return keyboardBitmapKeys.NUM_ZERO;
                    case (keyboardNames.NUM_PERIOD):
                        return keyboardBitmapKeys.NUM_PERIOD;
                    default:
                        return keyboardBitmapKeys.UNKNOWN;
                }
            }
        }

        public static class Corsair
        {

        }
    }
}
