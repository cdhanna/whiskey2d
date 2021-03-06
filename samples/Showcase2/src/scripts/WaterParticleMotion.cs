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
	public class WaterParticleMotion : Script<WaterParticle>
	{
		public override void onStart()
		{
			Gob.Sprite.Scale *= .8f + (.25f - Rand.Instance.nextFloat() * .5f);
			
		}
		
		public override void onUpdate() 
		{

			
			Gob.Velocity += Gob.Acceleration;
			Gob.Velocity *= .99f;
			
			Gob.Position += Gob.Velocity;


			//remove on collisions with walls
//			Bounds accurate = new Bounds(Gob.Position - Gob.Bounds.Size/4, Gob.Bounds.Size/2, 0);
//			foreach(var w in Objects.getAllObjectsOfType<Wall>()){
//				if (w.Bounds.boundWithin(accurate)){
//					Gob.close();
//				}
//			}
			
//			//add force to water
//			if (Gob.EffectWater && Gob.Sprite.Scale.Length > .1f){
//				foreach(var w in Objects.getAllObjectsOfType<WaterNode>()){
//					if (w.Bounds.boundWithin(accurate)){
//						w.Acceleration += Gob.Velocity * .1f * Gob.Sprite.Scale;
//						WaterParticle.makeSplash(Gob.Position, 4, new Vector(0, .4f * -Gob.Velocity.Y), Gob.Sprite.Scale.Length * .5f, 1, false);
//					}
//					if (w.Bounds.Bottom < accurate.Top){
//					//	Gob.close();
//					//	break;
//					}
//				}
//			}
			Gob.Acceleration = Vector.UnitY;

		}
		
		public override void onClose()  
		{
		 //This code runs when the GameObject is closed
		}
		
	}
}






































































