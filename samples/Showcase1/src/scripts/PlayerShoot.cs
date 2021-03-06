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
	public class PlayerShoot : Script<Player>
	{
	
		SimpleObject target;
		SimpleObject mouse;
	
		Sound laserSound;
		
	
		public override void onStart()
		{
		 	laserSound = new Sound("gunfire.wav");
		 
		 	//laserSound.Looped = true;
		 
		 	//new Sound("gunfire.wav");
		 
		 	target = new SimpleObject(Level);
		 	target.Sprite.Color = new Color(255, 0, 0, 128);
		 	target.Light.Radius = 64;
		 	target.Light.Color = Color.Red;
		 	target.Light.Visible = true;
		 	target.IsDebug = true;
		 	
		 	
		 	mouse = new SimpleObject(Level);
		 	mouse.Sprite.Depth = 1;
		 	mouse.Sprite.Color = Color.Blue;
		 	mouse.Sprite.Scale *= .2f;
		 	mouse.Sprite.Visible = false;
		 	
		}
		
		public override void onUpdate() 
		{
		 
		 
		 	Vector dir = Input.MousePosition - new Vector(ScreenWidth/2, ScreenHeight/2);
		 	
		 	
		 	target.Position = Input.MouseGamePosition;
		 	mouse.Position = Input.MouseGamePosition;
		 	Vector start = Gob.GunTipPosition - Gob.Position;
		 	//dir = Input.MouseGamePosition - start;
		 	//mouse.Position = Gob.Position + dir.UnitSafe * 200;
		 	
		 	
		 	RayCollisions<Wall> rayColls = Gob.currentRayCollisions<Wall>(start, dir);
		 
		 	RayCollision rc = null;
		 
		 	if (rayColls.Count > 0){
		 		rc = rayColls[0];
		 		target.Position = rc.ContactPoint;
		 	}
		 	
		 	RayCollisions<Badguy> badguyColls = Gob.currentRayCollisions<Badguy>(start , dir);
		 	if (badguyColls.Count > 0){
		 		if (rc == null || rc.Length > badguyColls[0].Length){
		 			rc = badguyColls[0];
		 			target.Position = rc.ContactPoint;
		 		}
		 	}
		 	
		 	if (rc != null){
		 		Gob.LookAngle = rc.RayDirection.Angle - (float) Math.PI/2;
		 	}
		 	
		 	if (rayColls.Count > 0){
		 		target.Position = rc.ContactPoint;
		 		Vector screenPos = Level.Camera.getScreenCoordinate(Gob.GunTipPosition);
		 		
		 		Bounds b = new Bounds(Vector.Zero, new Vector(ScreenWidth, ScreenHeight), 0);
		 		RayCollisionInfo info = b.getRayCollisionInfo(screenPos, (rc.RayDirection));
		 		if (info != null){
			 		screenPos = info.ContactPoint;
			 		
			 		screenPos = Level.Camera.getGameCoordinate(screenPos);
			 		
			 		if ((screenPos - Gob.GunTipPosition).Length < (Gob.GunTipPosition - target.Position).Length){
			 			target.Position = screenPos;
			 		}
		 		}
		 	}
		 	
		 	
		 	if (Input.isNewMouseDown(MouseButtons.Left) && rc != null){
		 			
		 			laserSound.duplicate().play();
		 			//laserSound.Pan = 0;
		 			
		 			
		 			if (rc is RayCollision<Badguy>){
		 				RayCollision<Badguy> brc = (RayCollision<Badguy>) rc;
		 				brc.Gob.close();
		 			}
		 			
		 			
		 			
		 			float height = (Rand.Instance.nextFloat() * 20 - 10);
		 			
		 			Tracer tracer = new Tracer(Level);
		 			tracer.Light.Visible = true;
		 			float scaleAmt = .9f - (Rand.Instance.nextFloat()*.45f);
		 			scaleAmt = 1;
		 			tracer.Sprite.Scale = new Vector((rc.Length-0) * scaleAmt / 10f, .5f + (Rand.Instance.nextFloat()*.3f - .15f) );
		 			tracer.Sprite.Rotation = rc.RayDirection.Angle;
		 			tracer.Position = (rc.ContactPoint + rc.RayStart) /2;
		 			tracer.Sprite.Color = Rand.Instance.nextColorVariation(Color.DarkOrange, .1f, .1f, .1f, .2f);
		 			//tracer.Position += rc.RayDirection * (0 + ((1 - scaleAmt) * (rc.Length)/2 * (Rand.Instance.nextFloat() * 2 - 1)));
		 			tracer.Position += rc.RayDirection.Perpendicular * height;
		 			tracer.Decay = 5;
		 			
		 			tracer = new Tracer(Level);
		 			tracer.Light.Visible = true;
		 			//scaleAmt = .9f - (Rand.Instance.nextFloat()*.45f);
		 			//scaleAmt = (scaleAmt + 0) / 2;
		 			tracer.Sprite.Scale = new Vector((rc.Length-0) * scaleAmt / 10f, .2f + (Rand.Instance.nextFloat()*.3f - .15f) );
		 			tracer.Sprite.Rotation = rc.RayDirection.Angle;
		 			tracer.Position = (rc.ContactPoint + rc.RayStart) /2;
		 			tracer.Sprite.Color = Rand.Instance.nextColorVariation(Color.White, .1f, .1f, .1f, 0);
		 			tracer.Decay = 4;
		 			//tracer.Position += rc.rayDirection * (0 + ((1 - scaleAmt) * (rc.Length)/2 * (Rand.Instance.nextFloat() * 2 - 1)));
		 			//tracer.Position += -rc.RayDirection * (rc.Length/2 - tracer.Sprite.ImageSize.X/2);
		 			
		 			tracer.Position += rc.RayDirection.Perpendicular * height;
		 			tracer.Dir = rc.RayDirection;
		 			tracer.Speed = 1f;
		 			
		 			
		 			SpriteEffect fx = new SpriteEffect(Level);
		 			fx.Effect = "smokePlume3";
		 			fx.Position = rc.ContactPoint - rc.RayDirection * 10;
		 			fx.Sprite.Scale *= .4f;
		 			fx.Frames = Vector.One * 4;
		 			fx.Speed = 2;
		 			fx.Sprite.Color = Color.Orange;
		 			fx.Light.Visible = true;
		 			fx.Light.Radius = 256f;
		 			float f = 256f;
		 				
		 			fx.OnUpdate = (a) => {
		 				f *= .95f;
		 				fx.Light.Radius = f;
		 			};

		 			
		 			fx = new SpriteEffect(Level);
		 			fx.Effect = "muzzle";
		 			fx.Position = rc.RayStart - rc.RayDirection * 10;
		 			fx.Frames = new Vector(4, 2);
		 			fx.Speed = 2;
		 			fx.Sprite.Scale *= .4f;
		 			fx.Sprite.Rotation = rc.RayDirection.Angle;
		 			fx.Sprite.Color = Color.Orange;
		 			
		 		}
		 	
		 
		}
		
		public override void onClose()  
		{
		 //This code runs when the GameObject is closed
		}
		
	}
	
	[Serializable] 
	public class TracerObject : GameObject{
	
		public TracerObject(GameLevel level) : base(level)
		{
			
		
		}
	
		protected override void addInitialScripts(){
		
		}
		
		public override void initializeObject()
		{
			//implement your code here!
			Light.Visible = true;
		}
	
		public override void renderLight(RenderInfo info){
		
			Sprite.draw(info.SpriteBatch, info.Transform, Position);
			Light.Color = Color.Black;
			base.renderLight(info);
		}
	
	}
	
	
	
}


































































































































































































































































































