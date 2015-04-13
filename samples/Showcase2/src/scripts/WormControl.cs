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
	public class WormControl : Script<Worm>
	{
	
		SimpleObject vis;
		Animation idle;
		Animation attack;
	
		GameObject target;
		bool attacking = false;
	
		public override void onStart()
		{
			target = Objects.getObject(Gob.Target);
			Gob.Sprite.Color = new Color(0, 1, 0, .2f);
			Gob.Sprite.Visible = false;
			vis = new SimpleObject(Level);
			vis.Sprite.ImagePath = "enemy2c.png";
			vis.Sprite.Rows = 2;
			vis.Sprite.Columns = 16;
			vis.Position = Gob.Position;
			vis.Sprite.Depth = .7f;
			vis.Sprite.Scale = Gob.Sprite.Scale / 300f;
			idle = vis.Sprite.createAnimation(16, 20, 7, true);	
			attack = vis.Sprite.createAnimation(1, 11, 6, true);
			
		}
		
		public override void onUpdate() 
		{

		
			if (target != null){
			
				Vector toTarget = target.Position - Gob.Position;
				Vector toTargetUnit = toTarget.UnitSafe;
				
				int dir = Math.Sign(toTarget.X);
				//attacking = false;
				
				
				if (Math.Abs(toTarget.X) < Gob.Sprite.Scale.X ){
					attacking = true;
				//	Gob.Acceleration -= toTargetUnit * .1f;
				} else if (!attacking && (Math.Abs(toTarget.X) > Gob.Sprite.Scale.X + 20)){
					
					//Gob.Acceleration += toTargetUnit * 1f;
				}
				
			
				vis.Sprite.Scale = new Vector(dir * Math.Abs(vis.Sprite.Scale.X), vis.Sprite.Scale.Y);
			}

			vis.Position = Gob.Position;
			
			
			if (attacking){
				attack.advanceFrame();
				
				if (attack.CurrentFrame == 5){
					attacking = false;
				}
				
			} else {
				idle.advanceFrame();
			}
			
			
		}
		
		public override void onClose()  
		{
		 float x = Math.Sign(vis.Sprite.Scale.X);
		 	SpriteEffect boom = new SpriteEffect(Level);
		 
		 	boom.Effect = "bigBang";
		 	boom.Frames = new Vector(1, 48);
		 	boom.Position = Gob.Position;
		 	boom.Speed = 1;
		 	boom.Sprite.Scale *= 1.5f;
		 	
		 	vis.close();
		 	
		 	SpriteEffect death = new SpriteEffect(Level);
		 
		 	death.Effect = "enemy2c";
		 	death.Frames = new Vector(2, 16);
		 	death.Position = Gob.Position+ new Vector(0, 0);;
		 	death.Speed = 13;
		 	death.StartFrame = 12;
		 	death.EndFrame = 16;
		 	death.Sprite.Scale = new Vector(x * Gob.Sprite.Scale.X, Gob.Sprite.Scale.Y) / 300;
		 	
		}
		
	}
}



























