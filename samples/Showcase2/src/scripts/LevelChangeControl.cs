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
	public class LevelChangeControl : Script<LevelChangeButton>
	{
		public override void onStart()
		{
		 //This code runs when the GameObject is initialized
		}
		
		public override void onUpdate() 
		{
		 
		 	if (Gob.Bounds.vectorWithin(Input.MousePosition)){
		 		Gob.Sprite.Color = Gob.HoverColor;
		 		if (Input.isNewMouseDown(MouseButtons.Left)){
		 		
		 			if (Gob.LevelName.Equals("EXIT")){
		 				GameManager.Instance.GameController.Exit();
		 			}
		 			else {
		 				GameManager.SetLevel(Gob.LevelName + ".state");
		 			}
		 		}
		 	} else {
		 		Gob.Sprite.Color = Gob.IdleColor;
		 	}
		 
		 
		}
		
		public override void onClose()  
		{
		 //This code runs when the GameObject is closed
		}
		
	}
}















