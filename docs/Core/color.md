### Color

A Color contains a R,G,B,A set that represents the Red, Green, Blue, and Alpha components of the Color. Each datum has a range from 0 to 255. There are many default colors already created. They are statically attached to the Color class. See the example for a glimpse at how to access the predefined colors. 

#### Properties

* **int R** 		(get set)
	The red component of the color. 

* **int G**			(get set)
	The green component of the color.

* **int B**			(get set)
	The blue component of the color.

* **int A**			(get set)
	The Alpha component of the color. (alpha is transparency)

#### Constructors

* **Color (float r, float g, float b, float a)**
	Creates a color with the given parameters. In this constructor, the parameters are floats, and are on the scale of 0 to 1, not 0 to 255. Giving a component value of 1 will translate to 255. This constructor is useful for doing maths. 

* **Color (int r, int g, int b, int a)**
	Creates a color with the given parameters. In this constructor, the parameters are ints, and are given on the scale of 0 to 255. 

#### Example

```

Color predefinedColor = Color.Red;
Color lessRed = new Color(predefinedColor.R / 2, 0, 0, 0);

```