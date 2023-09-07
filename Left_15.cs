using System;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing;

namespace RutonyChat {
    public class Script {

        public void InitParams(string param) {
        }
        public void Closing() {
        }
        public void NewMessage(string site, string name, string text, bool system) {
        }
        public void NewMessageEx(string site, string name, string text, bool system, Dictionary<string, string> Params) {
        }

        [DllImport("User32.dll")]
        public static extern bool SetCursorPos(int x, int y);
        [DllImport("User32.dll")]
        public static extern bool GetCursorPos(out System.Drawing.Point point);
        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);

        [Flags]
        public enum InputType
        {
            Mouse = 0,
            Keyboard = 1,
            Hardware = 2
        }

        [Flags]
        public enum MouseEventF
        {
            Absolute = 0x8000,
            HWheel = 0x01000,
            Move = 0x0001,
            MoveNoCoalesce = 0x2000,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            VirtualDesk = 0x4000,
            Wheel = 0x0800,
            XDown = 0x0080,
            XUp = 0x0100
        }

        public struct Input
        {
            public int type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HardwareInput
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KeyboardInput
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)] public MouseInput mi;
            [FieldOffset(0)] public KeyboardInput ki;
            [FieldOffset(0)] public HardwareInput hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MouseInput
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        public void NewAlert(string site, string typeEvent, string subplan, string name, string text, float donate, string currency, int qty) 
        //string site, string typeEvent, string subplan, string name, string text, float donate, string currency, int qty
        {
            
        }

        public void RunScript(string username, string param)
        {
		Point point;
             GetCursorPos(out point);
           // if (typeEvent == "TwitchPoints" && donate == 100)
            {
                Input[] inputs = new Input[]
                {
                    new Input
                    {
                        type = (int) InputType.Mouse, u = new InputUnion

                        {
                            mi = new MouseInput
                            {
                                dx = -36,
                                dy = 0,
                                dwFlags = (uint)(MouseEventF.Move),
                                dwExtraInfo = GetMessageExtraInfo()
                            }
                        }
                    },
                    new Input
                    {
                        type = (int) InputType.Mouse,
                        u = new InputUnion
                        {
                            mi = new MouseInput
                            {
                                dwFlags = (uint)MouseEventF.LeftUp,
                                dwExtraInfo = GetMessageExtraInfo()
                            }
                        }
                    }
                };
                SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
            }
        }
    }
}