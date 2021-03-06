﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VVVV.Utils.Win32
{
    public static class Const
    {
        public const int WHEEL_DELTA = 120;
        public const byte KEY_PRESSED = 0x80;
        public const byte KEY_TOGGLED = 0x01;
        public const uint MAPVK_VK_TO_VSC = 0x00;
        public const uint MAPVK_VSC_TO_VK = 0x01;
        public const uint MAPVK_VK_TO_CHAR = 0x02;
        public const uint MAPVK_VSC_TO_VK_EX = 0x03;
        public const uint MAPVK_VK_TO_VSC_EX = 0x04;
        public static readonly IntPtr HWND_MESSAGE = new IntPtr(-3);

        // Touch event flags ((TOUCHINPUT.dwFlags) [winuser.h]
        public const int TOUCHEVENTF_MOVE = 0x0001;
        public const int TOUCHEVENTF_DOWN = 0x0002;
        public const int TOUCHEVENTF_UP = 0x0004;
        public const int TOUCHEVENTF_INRANGE = 0x0008;
        public const int TOUCHEVENTF_PRIMARY = 0x0010;
        public const int TOUCHEVENTF_NOCOALESCE = 0x0020;
        public const int TOUCHEVENTF_PEN = 0x0040;

        // Touch input mask values (TOUCHINPUT.dwMask) [winuser.h]
        public const int TOUCHINPUTMASKF_TIMEFROMSYSTEM = 0x0001; // the dwTime field contains a system generated value
        public const int TOUCHINPUTMASKF_EXTRAINFO = 0x0002; // the dwExtraInfo field is valid
        public const int TOUCHINPUTMASKF_CONTACTAREA = 0x0004; // the cxContact and cyContact fields are valid

        // GestureConfig
        public const int GC_ALLGESTURES = 0x00000001;
        public const int GC_ZOOM = 0x00000001;
        public const int GC_PAN = 0x00000001;
        public const int GC_PAN_WITH_SINGLE_FINGER_VERTICALLY = 0x00000002;
        public const int GC_PAN_WITH_SINGLE_FINGER_HORIZONTALLY = 0x00000004;
        public const int GC_PAN_WITH_GUTTER = 0x00000008;
        public const int GC_PAN_WITH_INERTIA = 0x00000010;
        public const int GC_ROTATE = 0x00000001;
        public const int GC_TWOFINGERTAP = 0x00000001;
        public const int GC_PRESSANDTAP = 0x00000001;

        // Gesture IDs 
        public const int GID_BEGIN = 1;
        public const int GID_END = 2;
        public const int GID_ZOOM = 3;
        public const int GID_PAN = 4;
        public const int GID_ROTATE = 5;
        public const int GID_TWOFINGERTAP = 6;
        public const int GID_PRESSANDTAP = 7;

        // Gesture flags - GESTUREINFO.dwFlags
        public const int GF_BEGIN = 0x00000001;
        public const int GF_INERTIA = 0x00000002;
        public const int GF_END = 0x00000004;

        // One of the fields in GESTUREINFO structure is type of Int64 (8 bytes).
        // The relevant gesture information is stored in lower 4 bytes. This
        // bit mask is used to get 4 lower bytes from this argument.
        public const Int64 ULL_ARGUMENTS_BIT_MASK = 0x00000000FFFFFFFF;
    }
}
