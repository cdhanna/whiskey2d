using System;
using System.Collections.Generic;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers;
using Whiskey2D.Core.Inputs;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

//auto-generated by Whiskey2D
namespace Project
{
	[Serializable] 
	public class SpoutScript : Script<SimpleObject>
	{
		public override void onStart()
		{
		 //This code runs when the GameObject is initialized
		}
		
		public override void onUpdate() 
		{

			if (Input.isNewKeyDown(Keys.F1)){
				WaterParticle wp = new WaterParticle(Level);
				wp.Layer = Level.getLayer("Water");
				wp.Position = Gob.Position;
				wp.Position += new Vector(-Gob.Bounds.Size.X/2 + Rand.Instance.nextFloat() * Gob.Bounds.Size.X, 0);
				wp.EffectWater = true;
			}

		}
		
		public override void onClose()  
		{
		 //This code runs when the GameObject is closed
		}
		
	}
}










