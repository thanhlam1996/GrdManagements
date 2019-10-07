using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace CommonLib.UserControls {
	internal enum InputSealedKey {ALT = Keys.Alt, CTRL = Keys.Control, SHIFT = Keys.Shift }
	internal enum InputSealedKeyValue { ALTVALUE = 18, CTRLVALUE = 17, SHIFTVALUE = 0x10 }

	public class ActiveActionsImports {
		public class Msg {
			public uint hWnd;
			public uint Message;
			public uint wParam;
			public uint lParam;
			public uint time;
			public System.IntPtr pt;
		}
		public const uint PM_NOREMOVE = 0;
		public const uint WM_MOUSEFIRST = 0x0200;
		public const uint WM_MOUSELAST = 0x020A;
		public const int WM_KEYDOWN = 0x0100;
		public const int WM_KEYUP = 0x0101;
		public const int WM_CHAR = 0x0102;
		public const int WM_ACTIVATEAPP = 0x001C;

		[DllImport("user32.dll", EntryPoint = "SendInput")]
		extern public static uint SendMouseInput(int nInputs, [MarshalAs(UnmanagedType.LPArray)] MouseInputArgs[] pInputs, int cbSize);

		[DllImport("user32.dll", EntryPoint = "SendInput")]
		extern public static uint SendKeyInput(int nInputs, [MarshalAs(UnmanagedType.LPArray)] KeyInputArgs[] pInputs, int cbSize);

		[DllImport("kernel32.dll", EntryPoint = "GetLastError")]
		extern public static uint GetLastError();

		[DllImport("user32.dll", EntryPoint = "MapVirtualKey")]
		extern public static uint MapVirtualKey(uint uCode, uint uMapType);

		[DllImport("user32.dll")]
		extern public static void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);
		[DllImport("user32.dll", EntryPoint = "PeekMessage")]
		extern public static uint PeekMessage(Msg msg, System.IntPtr hWnd,uint firstMessage, uint lastMessage, uint options);
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		internal static extern int SendMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		internal static extern int PostMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);
		[System.Runtime.InteropServices.DllImport("user32.dll")] 
		public static extern short GetKeyState(int keyCode);
		[System.Runtime.InteropServices.DllImport("user32.dll")] 
		public static extern bool SetKeyboardState([MarshalAs(UnmanagedType.LPArray)] byte[] bytes);
		[System.Runtime.InteropServices.DllImport("user32.dll")] 
		public static extern bool GetKeyboardState(byte[] bytes);
		[System.Runtime.InteropServices.DllImport("user32.dll")] 
		public static extern IntPtr GetActiveWindow();
		[System.Runtime.InteropServices.DllImport("user32.dll")] 
		public static extern IntPtr WindowFromPoint(Point point);
		[System.Runtime.InteropServices.DllImport("user32.dll")] 
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref int lpdwProcessId);
		[System.Runtime.InteropServices.DllImport("kernel32.dll")] 
		public static extern uint GetCurrentThreadId();

		public static bool CapsIsPressed {
			get { return GetKeyPressed(Keys.CapsLock); }
			set { SetKeyPressed(Keys.CapsLock, value);	}
		}
		public static bool GetKeyPressed(Keys key) {
			uint keyValue = GetKeyValue(key);
			if(keyValue > 255) return false;
			byte[] bytes = new byte[255];
			if(GetKeyboardState(bytes)){
				return bytes[keyValue] != 0;
			} 
			return false;
		}
		public static void SetKeyPressed(Keys key, bool value) {
			uint keyValue = GetKeyValue(key);
			if(keyValue > 255) return;
			byte[] bytes = new byte[255];
			if(GetKeyboardState(bytes)){
				if(value) {
					bytes[keyValue] = 128;
				}
				else { 
					bytes[keyValue] = 0;
				}
				SetKeyboardState(bytes);
			} 
		}
		public static uint GetKeyValue(Keys key) {
			if(IsSealedKey(key))
				return (uint)GetSealedKeyValue(key);
			else return (uint)key & 0x0000FFFF;
		}
		public static bool IsSealedKey(Keys key) {
			return GetSealedKeyValue(key) > -1;
		}
		public static int GetSealedKeyValue(Keys key) {
			Array ar = Enum.GetValues(typeof(InputSealedKey));
			int i;
			for(i = 0; i < ar.Length; i ++)
				if((Keys)ar.GetValue(i) == key) break;
			if(i < ar.Length) {
				ar = Enum.GetValues(typeof(InputSealedKeyValue));
				return (int)ar.GetValue(i);
			}
			return -1;
		}
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct MouseInputArgs { 
		public System.Int32 Type;
		public System.Int32 dx;
		public System.Int32 dy;
		public System.Int32 mouseData;
		public System.Int32 dwFlags;
		public System.Int32 time;
		public IntPtr extraInfo;
		public MouseInputArgs(System.Int32 dx, System.Int32 dy, System.Int32 dwFlags) {
			Type = 0;
			mouseData = 0;
			extraInfo = System.IntPtr.Zero;
			time = 0;
			this.dx = dx;
			this.dy = dy;
			this.dwFlags = dwFlags;
		}
		public MouseInputArgs(System.Int32 dx, System.Int32 dy, System.Int32 dwFlags, System.Int32 mouseData) : this(dx, dy, dwFlags) {
			this.mouseData = mouseData;
		}
	}

	public enum KeyEventType { KeyDown, KeyUp }
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct KeyInputArgs { 
		public int Type;
		public ushort wVK;
		public ushort wScan;
		public UInt32 dwFlags;
		public UInt32 time;
		public IntPtr extraInfo;
		public KeyInputArgs(KeyEventType keyType, ushort vk) {
			this.Type = 1;
			this.wVK = vk;
			this.wScan = Convert.ToUInt16(ActiveActionsImports.MapVirtualKey(vk, 0));
			this.dwFlags = Convert.ToUInt32((keyType == KeyEventType.KeyDown ? 0 : 0x0002));
			this.time = 0;
			this.extraInfo = IntPtr.Zero;
		}
	}

	public enum ActiveActionsCancelMode {None, ApplicationDeactivated, UserCancel, UnknownTopWindow};

	public class ActiveActions : object, IMessageFilter, IDisposable {
		class WndForm : Form {
			ActiveActions ActiveActions;
			public WndForm(ActiveActions ActiveActions) {
				this.ActiveActions = ActiveActions;
				StartPosition = FormStartPosition.Manual;
				FormBorderStyle = FormBorderStyle.None;
				ShowInTaskbar = false;
				Width = 1;
				Height = 1;
				Left = -100;
				Top = -100;

			}
			protected override void WndProc(ref Message m) {
				base.WndProc(ref m);
				if((m.Msg == ActiveActionsImports.WM_ACTIVATEAPP) && !this.ActiveActions.Canceled) {
					this.ActiveActions.CancelMode = ActiveActionsCancelMode.ApplicationDeactivated;
				}
			}
		}

		const int DefaultMouseMoveDelay = 10;
		const int DefaultMouseMoveDelayPerPixels = 3;
		const int DefaultKeyboardDelay = 200;
		const uint MOUSEEVENTF_MOVE = 0x0001,
			MOUSEEVENTF_LEFTDOWN   = 0x0002,
			MOUSEEVENTF_LEFTUP     = 0x0004,
			MOUSEEVENTF_RIGHTDOWN  = 0x0008, 
			MOUSEEVENTF_RIGHTUP    = 0x0010, 
			MOUSEEVENTF_MIDDLEDOWN = 0x0020, 
			MOUSEEVENTF_MIDDLEUP   = 0x0040, 
			//MOUSEEVENTF_WHEEL      = 0x0800, 
			MOUSEEVENTF_ABSOLUTE   = 0x8000; 
		ActiveActionsCancelMode canceleMode;
		WndForm wndForm;
		bool keyActiveActionsProccessing;
		
		public ActiveActions() {
			this.canceleMode = ActiveActionsCancelMode.None;
			Application.AddMessageFilter(this);
			wndForm = new WndForm(this);
			wndForm.Show();
		}
		public virtual void Dispose() {
			Application.RemoveMessageFilter(this);
			wndForm.Hide();
			wndForm.Dispose();
			wndForm = null;
		}
		public bool Canceled { get { return CancelMode != ActiveActionsCancelMode.None; } }
		public ActiveActionsCancelMode CancelMode { get { return this.canceleMode; } set { this.canceleMode = value; }}
		public static void DoEvents() {
			SendKeys.Flush();
			Application.DoEvents();
		}
		public void SendKey(Control control, char key) {
			if(Canceled) return;
			SendKeyCore(control != null? control: ActiveControl, key);
			SendKeys.Flush();
		}
		public void SendString(Control control, string keys) {
			if(Canceled) return;
			SendStringCore(control != null? control: ActiveControl, keys);
			SendKeys.Flush();
		}
		public void MoveMousePointTo(Control control, Point pt) {
			Point screenPoint = control == null ? pt : control.PointToScreen(pt);
			MoveMousePointTo(screenPoint);
		}
		public void MoveMousePointTo(Point pt) {
			if(Canceled) return;
			int count = 0, countX = 1, countY = 1; 
			while(! Cursor.Position.Equals(pt)) {
				if(Canceled) return;
				Point cpt = Cursor.Position;
				int dX = cpt.X - pt.X, dY = cpt.Y - pt.Y;
				double dXY = dY != 0 ? Math.Abs((double)dX / (double)dY) : 1;
				double dYX = dX != 0 ? Math.Abs((double)dY / (double)dX) : 1;
				if((cpt.X == pt.X) || (dXY * (countX++) <= 1))
					dX = 0;
				else {
					dX = cpt.X < pt.X ? 1 : -1;
					countX = 1;
				}
				if((cpt.Y == pt.Y) || (dYX * (countY++) <= 1)) 
					dY = 0;
				else {
					dY = cpt.Y < pt.Y ? 1 : -1;
					countY = 1;
				}
				cpt.X += dX;
				cpt.Y += dY;
				Cursor.Position = cpt;
				uint x = Convert.ToUInt32((65536.0 * cpt.X / Screen.PrimaryScreen.Bounds.Width) + (65536.0 / Screen.PrimaryScreen.Bounds.Width  / 2));
				uint y = Convert.ToUInt32((65536.0 * cpt.Y / Screen.PrimaryScreen.Bounds.Height) + (65536.0 / Screen.PrimaryScreen.Bounds.Height / 2));
				ActiveActionsImports.mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, x, y, 0, IntPtr.Zero);
				DoEvents();
				if (count ++ == DefaultMouseMoveDelayPerPixels) {
					Delay(DefaultMouseMoveDelay);
					count = 0;
				}
			}
		}
		public void MouseClick() {
			MouseClick(MouseButtons.Left);
		}
		public void MouseClick(MouseButtons mouseButtons) {
			MouseClick(Cursor.Position, mouseButtons);
		}
		public void MouseClick(Control control, Point pt) {
			MouseClick(control, pt, MouseButtons.Left);
		}
		public void MouseClick(Control control, Point pt, MouseButtons mouseButtons) {
			Point screenPoint = control == null ? pt : control.PointToScreen(pt);
			MouseClick(screenPoint, mouseButtons);
		}
		public void MouseClick(Point pt) {
			MouseClick(pt, MouseButtons.Left);
		}
		public void MouseClick(Point pt, MouseButtons mouseButtons) {
			if(Canceled) return;
			this.MoveMousePointTo(pt);
			if(CheckMouse(pt)) {
				/*
				UInt32 x = Convert.ToUInt32(pt.X);
				UInt32 y = Convert.ToUInt32(pt.Y);
				ActiveActionsImports.mouse_event(GetMouseFlagsByMouseButtons(mouseButtons, true), x, y, 0, IntPtr.Zero);
				DoEvents();
				ActiveActionsImports.mouse_event(GetMouseFlagsByMouseButtons(mouseButtons, false), x, y, 0, IntPtr.Zero);
				DoEvents();
				*/
				MouseInputArgs[] inputs = new MouseInputArgs[2];
				inputs[0].dx = Convert.ToInt32(pt.X);
				inputs[0].dy = Convert.ToInt32(pt.Y);
				inputs[0].dwFlags = Convert.ToInt32(MOUSEEVENTF_LEFTDOWN);
				inputs[1].dx = Convert.ToInt32(pt.X);
				inputs[1].dy = Convert.ToInt32(pt.Y);
				inputs[1].dwFlags = Convert.ToInt32(MOUSEEVENTF_LEFTUP);
				ActiveActionsImports.SendMouseInput(inputs.Length, inputs, Marshal.SizeOf(inputs[0].GetType()));
				DoEvents();
			}
		}
		public void MouseDown() {
			MouseDown(MouseButtons.Left);
		}
		public void MouseDown(MouseButtons mouseButtons) {
			MouseDown(Cursor.Position, mouseButtons);
		}
		public void MouseDown(Control control, Point pt) {
			MouseDown(control, pt, MouseButtons.Left);
		}
		public void MouseDown(Control control, Point pt, MouseButtons mouseButtons) {
			Point screenPoint = control == null ? pt : control.PointToScreen(pt);
			MouseDown(screenPoint, mouseButtons);
		}
		public void MouseDown(Point pt) {
			MouseDown(pt, MouseButtons.Left);
		}
		public void MouseDown(Point pt, MouseButtons mouseButtons) {
			if(Canceled) return;
			this.MoveMousePointTo(pt);
			if(CheckMouse(pt)) {
				ActiveActionsImports.mouse_event(GetMouseFlagsByMouseButtons(mouseButtons, true), Convert.ToUInt32(pt.X), Convert.ToUInt32(pt.Y), 0, IntPtr.Zero);
				DoEvents();
			}
		}
		public void MouseUp() {
			MouseUp(MouseButtons.Left);
		}
		public void MouseUp(MouseButtons mouseButtons) {
			MouseUp(Cursor.Position, mouseButtons);
		}
		public void MouseUp(Control control, Point pt) {
			MouseUp(control, pt, MouseButtons.Left);
		}
		public void MouseUp(Control control, Point pt, MouseButtons mouseButtons) {
			Point screenPoint = control == null ? pt : control.PointToScreen(pt);
			MouseUp(screenPoint, mouseButtons);
		}
		public void MouseUp(Point pt) {
			MouseUp(pt, MouseButtons.Left);
		}
		public void MouseUp(Point pt, MouseButtons mouseButtons) {
			if(Canceled) return;
			this.MoveMousePointTo(pt);
			if(CheckMouse(pt)) {
				ActiveActionsImports.mouse_event(GetMouseFlagsByMouseButtons(mouseButtons, false), Convert.ToUInt32(pt.X), Convert.ToUInt32(pt.Y), 0, IntPtr.Zero);
				DoEvents();
			}
		}
		public void MouseDblClick() {
			MouseDblClick(MouseButtons.Left);
		}
		public void MouseDblClick(MouseButtons mouseButtons) {
			MouseDblClick(Cursor.Position, mouseButtons);
		}
		public void MouseDblClick(Control control, Point pt) {
			MouseDblClick(control, pt, MouseButtons.Left);
		}
		public void MouseDblClick(Control control, Point pt, MouseButtons mouseButtons) {
			Point screenPoint = control == null ? pt : control.PointToScreen(pt);
			MouseDblClick(screenPoint, mouseButtons);
		}
		public void MouseDblClick(Point pt) {
			MouseDblClick(pt, MouseButtons.Left);
		}
		public void MouseDblClick(Point pt, MouseButtons mouseButtons) {
			if(Canceled) return;
			this.MoveMousePointTo(pt);
			if(CheckMouse(pt)) {
				ActiveActionsImports.mouse_event(GetMouseFlagsByMouseButtons(mouseButtons, true), Convert.ToUInt32(pt.X), Convert.ToUInt32(pt.Y), 0, IntPtr.Zero);
				ActiveActionsImports.mouse_event(GetMouseFlagsByMouseButtons(mouseButtons, false), Convert.ToUInt32(pt.X), Convert.ToUInt32(pt.Y), 0, IntPtr.Zero);
				DoEvents();
				ActiveActionsImports.mouse_event(GetMouseFlagsByMouseButtons(mouseButtons, true), Convert.ToUInt32(pt.X), Convert.ToUInt32(pt.Y), 0, IntPtr.Zero);
				ActiveActionsImports.mouse_event(GetMouseFlagsByMouseButtons(mouseButtons, false), Convert.ToUInt32(pt.X), Convert.ToUInt32(pt.Y), 0, IntPtr.Zero);
				DoEvents();
			}
		}
		bool CheckMouse(Point pt) {
			IntPtr handle = ActiveActionsImports.WindowFromPoint(pt);
			if(handle != IntPtr.Zero) {
				uint currentTaskId = ActiveActionsImports.GetCurrentThreadId();
				int dummyvalue = 1;
				uint windowTaskId = ActiveActionsImports.GetWindowThreadProcessId(handle, ref dummyvalue);
				if(currentTaskId != windowTaskId) 
					CancelMode = ActiveActionsCancelMode.UnknownTopWindow;
			}
			return ! Canceled;
		}
		
		uint GetMouseFlagsByMouseButtons(MouseButtons mouseButtons, bool down) {
			uint flags = 0;
			switch(mouseButtons) {
				case MouseButtons.Left: 
					flags = down ? MOUSEEVENTF_LEFTDOWN : MOUSEEVENTF_LEFTUP;
					break;
				case MouseButtons.Middle:
					flags = down ? MOUSEEVENTF_RIGHTDOWN : MOUSEEVENTF_RIGHTUP;
					break;
				case MouseButtons.Right:
					flags = down ? MOUSEEVENTF_MIDDLEDOWN : MOUSEEVENTF_MIDDLEUP;
					break;
			}
			return flags | MOUSEEVENTF_ABSOLUTE;
		}

		bool IMessageFilter.PreFilterMessage(ref Message m) {
			if((m.Msg == ActiveActionsImports.WM_KEYDOWN) && !this.keyActiveActionsProccessing) {
				CancelMode = ActiveActionsCancelMode.UserCancel;
				return true;
			}
			return false;
		}
		void IDisposable.Dispose() {
			Dispose();
		}
		
		static Control ActiveControl { 
			get {
				if(Form.ActiveForm != null)
					return Form.ActiveForm.ActiveControl;
				else return null;
			}
		}

		static public void Delay(int millisecs) {
			Thread.Sleep(millisecs);
		}
		void SendKeyCore(Control control, char key) {
			if(control == null) return;
			this.keyActiveActionsProccessing = true;
			try {
				string upstring = new string(key, 1);
				char upkey = upstring.ToUpper()[0];
				ActiveActionsImports.SendMessage(control.Handle, ActiveActionsImports.WM_KEYDOWN, (uint)upkey, 0);
				ActiveActionsImports.SendMessage(control.Handle, ActiveActionsImports.WM_CHAR, (uint)key, 0);
				ActiveActionsImports.SendMessage(control.Handle, ActiveActionsImports.WM_KEYUP, (uint)upkey, 0);
				DoEvents();
				Delay(DefaultKeyboardDelay);
			} finally {
				this.keyActiveActionsProccessing = false;
			}
		}
		void SendStringCore(Control control, string keys) {
			if(control == null) return;
			Keys key;
			this.keyActiveActionsProccessing = true; 
			try {
				ArrayList sealedKeyPressed = new ArrayList();

				while(keys != string.Empty) {
					if(HasKey(ref keys, out key))
						InputKey(control, key, sealedKeyPressed);
					else { 
						SendKeyCore(control, keys[0]);
						keys = keys.Remove(0, 1);
					}
					Delay(DefaultKeyboardDelay);
				}
				foreach(object k in sealedKeyPressed) {
					UnPressedSealedKey(control, (Keys)k);
				}
			} finally {
				this.keyActiveActionsProccessing = false;
			}
		}
		static void InputKey(Control control, Keys key, ArrayList sealedKeyPressed) {
			if(PressSealedKey(control, key, sealedKeyPressed)) return;
			uint v = ActiveActionsImports.GetKeyValue(key);
			if (Keys.Back == key) {
				ActiveActionsImports.PostMessage(control.Handle, ActiveActionsImports.WM_CHAR, v, 0);
			} else {
				if(ActiveControl != null) {
					Message msg = new Message();
					msg.HWnd = control.Handle;
					msg.LParam = (IntPtr)0;
					msg.WParam = (IntPtr)v;
					msg.Msg = ActiveActionsImports.WM_KEYDOWN;
					if(! control.PreProcessMessage(ref msg))
						ActiveActionsImports.SendMessage(control.Handle, ActiveActionsImports.WM_KEYDOWN, v, 0);
					msg.Msg = ActiveActionsImports.WM_KEYUP;
					if(! control.PreProcessMessage(ref msg))
						ActiveActionsImports.SendMessage(control.Handle, ActiveActionsImports.WM_KEYUP, v, 0);
				}
			}
		}
		static bool PressSealedKey(Control control, Keys key, ArrayList sealedKeyPressed) {
			if(ActiveActionsImports.IsSealedKey(key)) {
				if(sealedKeyPressed.IndexOf(key) < 0) {
					sealedKeyPressed.Add(key);
					uint v = ActiveActionsImports.GetKeyValue(key);
					//TODO alt do SendMessage(FHandle, WM_SYSKEYDOWN, VKMap[SealKey], $20380001)
					ActiveActionsImports.SetKeyPressed(key, true);
					ActiveActionsImports.SendMessage(control.Handle, ActiveActionsImports.WM_KEYDOWN, v, 0);
				}
				return true;
			}
			return false; 
		}
		static void UnPressedSealedKey(Control control, Keys key) {
			uint v = ActiveActionsImports.GetKeyValue(key);
			//TODO alt do SendMessage(FHandle, WM_SYSKEYDOWN, VKMap[SealKey], $20380001)
			ActiveActionsImports.SetKeyPressed(key, false);
			ActiveActionsImports.SendMessage(control.Handle, ActiveActionsImports.WM_KEYUP, v, 0);
		}
		static bool HasKey(ref string keys, out Keys key) {
			key = Keys.A;
			if((keys == string.Empty) || (keys[0] != '[')) return false;
			int closedBracket = keys.IndexOf(']');
			int openBracket = keys.IndexOf('[', 1);
			if((closedBracket < 0) || ((openBracket > 0) && (closedBracket > openBracket))) 
				return false;
			string tabKey = keys.Substring(1, closedBracket - 1);
			if(tabKey == string.Empty) return false;
			foreach(Keys i in Enum.GetValues(typeof(Keys))) {
				if(i.ToString().ToUpper() == tabKey.ToUpper()) {
					key = i;
					keys = keys.Remove(0, closedBracket + 1);
					return true;
				}
			}
			return false;
		}
	}
}
