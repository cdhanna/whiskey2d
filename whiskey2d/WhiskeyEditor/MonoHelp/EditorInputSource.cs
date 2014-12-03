﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Whiskey2D.Core;
using Whiskey2D.Core.Inputs;
namespace WhiskeyEditor.MonoHelp
{
    using WinMouse = System.Windows.Forms.MouseButtons;
    using MonoMouse = Microsoft.Xna.Framework.Input.MouseState;
    using MonoKeys = Microsoft.Xna.Framework.Input.Keys; //typedef
    
    public class EditorInputSource : InputSource
    {

        

        private WhiskeyControl control;
        private Dictionary<Keys, MonoKeys> winToMono;
        private Dictionary<Keys, bool> winDown;

        private Dictionary<WinMouse, Microsoft.Xna.Framework.Input.ButtonState> winMouseState;

        private int mouseX, mouseY;


        public EditorInputSource(WhiskeyControl control)
        {
            this.control = control;
            winMouseState = new Dictionary<WinMouse, Microsoft.Xna.Framework.Input.ButtonState>();
            winMouseState.Add(WinMouse.Left, Microsoft.Xna.Framework.Input.ButtonState.Released);
            winMouseState.Add(WinMouse.Middle, Microsoft.Xna.Framework.Input.ButtonState.Released);
            winMouseState.Add(WinMouse.Right, Microsoft.Xna.Framework.Input.ButtonState.Released);
            winMouseState.Add(WinMouse.None, Microsoft.Xna.Framework.Input.ButtonState.Released);
            winMouseState.Add(WinMouse.XButton1, Microsoft.Xna.Framework.Input.ButtonState.Released);
            winMouseState.Add(WinMouse.XButton2, Microsoft.Xna.Framework.Input.ButtonState.Released);
            


            winToMono = new Dictionary<Keys, MonoKeys>();
            winToMono.Add(Keys.Right, MonoKeys.Right);
            winToMono.Add(Keys.Left, MonoKeys.Left);
            winToMono.Add(Keys.Up, MonoKeys.Up);
            winToMono.Add(Keys.Down, MonoKeys.Down);
            winToMono.Add(Keys.Space, MonoKeys.Space);


            winDown = new Dictionary<Keys, bool>();
            Keys[] all = (Keys[])Enum.GetValues(typeof(Keys));
            foreach (Keys k in all)
            {
                if (!winDown.ContainsKey(k))
                    winDown.Add(k, false);
            }


            control.KeyDown += keyDown;
            control.KeyUp += keyUp;
            control.PreviewKeyDown += previewKey;

            control.MouseMove += mouseMove;
            control.MouseDown += mouseDown;
            control.MouseUp += mouseUp;
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;

        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            WinMouse m = e.Button;
            winMouseState[m] = Microsoft.Xna.Framework.Input.ButtonState.Released;
            
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            WinMouse m = e.Button;
            winMouseState[m] = Microsoft.Xna.Framework.Input.ButtonState.Pressed;
        }


        private void previewKey(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }
        private void keyDown(object sender, KeyEventArgs e)
        {
            winDown[e.KeyData] = true;
            Console.WriteLine("down: " + e.KeyData.ToString());
        }
        private void keyUp(object sender, KeyEventArgs e)
        {
            winDown[e.KeyData] = false;
        }


        public void init()
        {
            
        }

        public Dictionary<MonoKeys, bool> getAllKeysDown()
        {
            Dictionary<MonoKeys, bool> map = new Dictionary<MonoKeys, bool>();
            MonoKeys[] all = (MonoKeys[]) Enum.GetValues(typeof(MonoKeys));
            foreach (MonoKeys k in all)
            {
                map.Add(k, false);
            }

            foreach (Keys k in winToMono.Keys)
            {
                map[ winToMono[k] ] = winDown[k];
            }


            return map;
        }



        public MonoMouse getMouseState()
        {
            return new MonoMouse(mouseX, mouseY, 
                0,
                winMouseState[WinMouse.Left],
                winMouseState[WinMouse.Middle],
                winMouseState[WinMouse.Right],
                winMouseState[WinMouse.XButton1],
                winMouseState[WinMouse.XButton2]);
        }
    }
}
