using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Whiskey2D.Core;

namespace WhiskeyEditor.MonoHelp
{
    using MonoKeys = Microsoft.Xna.Framework.Input.Keys; //typedef

    class EditorInputSource : InputSource
    {

        

        private WhiskeyControl control;
        private Dictionary<Keys, MonoKeys> winToMono;
        private Dictionary<Keys, bool> winDown;

        public EditorInputSource(WhiskeyControl control)
        {
            this.control = control;
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
    }
}
