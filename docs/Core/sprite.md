### Sprite

The Sprite holds visual data, such as an image to display, and the scale at which the image should be displayed. 

#### Properties

* **String ImagePath** 			(get)
	The path to the image that the Sprite is using to render.

* **Vector Scale**				(get set)
	The scale of the image. By default, the Scale is (x=1, y=1). By setting Scale to (x=2, y=1), the width of the Sprite would be doubled.

* **Vector ImageSize**			(get)
	The true image size of the Sprite, in pixels. This property is derived from the actual Image, and the Scale.

* **float ImageWidth**			(get)
	The true image width of the Sprite, in pixels. This property is derived from the actual Image, and the Scale.

* **float ImageHeight**			(get)
	The true image height of the Sprite, in pixels. This property is derived from the actual Image, and the Scale.

* **float Rotation**			(get set)
	The rotation of the Sprite. By default, the Sprite has no rotation. Rotation is measued in radians, so that setting Rotation to pi, the Sprite will be rotated 180 degrees. 

* **Color Color**				(get set)
	The Color that the sprite will be tinted. By default, the Color is set to Color.White, and no tint is applied. By changing the Color to Color.Red, the entire Sprite will be tinted red.

* **float Depth**				(get set)
	The Depth that the Sprite will use to determine draw order in the z-plane. When two sprites overlap one another, one of the sprites will not be displayed. The Sprite with the lower Depth value will not be displayed. 

* **Vector Offset**				(get set)
	The Offset of the Sprite controls where the Image is drawn. By default, the top-left corner of the Image will be drawn at some position. However, if the Sprite is meant to be centered, then the Offset must be adjusted to (Width/2, Height/2). Use the Center() method to auto-center a Sprite.

#### Constructors

* **Sprite (string imagePath)**
	Creates a Sprite with the image found at the given image path.

* **Sprite ()**
	Creates a Sprite with no image. The Sprite will use a pixel to draw. This is useful to draw boxes and simple shapes.

#### Methods

* **void Center ()**
	Sets the Offset of the Sprite to the center of the Sprite's Image.

* **void setImage (Texture2D image)**
	Change the Image the Sprite uses to display.

* **Texture2D getImage ()**
	Get the Image that the Sprite uses to display.
	
#### Example
```
Sprite pixelSprite = new Sprite();
pixelSprite.Scale = new Vector(100, 100); //make the sprite be 100x100 pixels large
pixelSprite.Color = Color.Blue;
```