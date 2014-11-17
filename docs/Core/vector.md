### Vector

The Vector has an x component and y component. It represents a point in 2D space. There are a few static properties that allow access to very standard Vectors, such as the zero vector. The Vector can be used in standard math operators, such as +, and -.

#### Properties

* **float X**			(get set)
	The x coordinate of the Vector.

* **float Y**			(get set)
	The y coordinate of the Vector.

#### Constructors

* **Vector (float v)**
	Creates a Vector that has an x and y component both equal to *v*.

* **Vector (float x, float y)**
	Creates a Vector with its components set to the given parameters.

#### Methods

* **bool Equals (Vector otherVector)**
	Checks if the given Vector has the same x and y components as this Vector. Returns true if the Vectors are the same, and false otherwise.

* **float Length ()**
	Calculates the length of the Vector using Euler distance. The resulting distance will be equal to sqrt( x^2 + y^2 ).

* **void Normalize ()**
	Normalizes the Vector so that its length is equal to 1. 

#### Example

```
//commonly used Vectors
Vector zero = Vector.Zero;		//creates a vector with the values (x=0, y=0)
Vector one = Vector.One;		//creates a vector with the values (x=1, y=1)
Vector unitX = Vector.UnitX;	//creates a vector with the values (x=1, y=0)
Vector unitY = Vector.UnitY;	//creates a vector with the values (x=0, y=1)

//math operators
Vector sum = unitX + unitY;		//creates a vector with the values (x=1, y=1)
Vector scaled = sum / .5f;		//creates a vector with the values (x=.5, y=.5)

```