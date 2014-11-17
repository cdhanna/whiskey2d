###Bounds

The Bounds class holds information about the bounding rectangle around a GameObject. The Bounds property of a GameObject is a derived property from the GameObject's Position and Sprite. 

#### Properties

* **Vector Position** 	(get)
	The Position represents the top-left corner of the bounding rectangle.

* **Vector Size**		(get)
	The x component represents the width of the bounding rectangle, and the y component represents the height. 

* **float Bottam**		(get)
	The y coordinate of the bottam of the bounding rectangle.

* **float Top**			(get)
	The y coordinate of the top of the bounding rectangle

* **float Right**		(get)
	The x coordinate of the right side of the bounding rectangle

* **float Left**		(get)
	The x coordinate of the left side of the bounding rectangle

#### Constructors

* **Bounds (Vector position, Vector size)**
	Creates a new Bounds with the given parameters. 

#### Methods

* **Boolean vectorWithin (Vector vec)**
	Checks that the given Vector, vec, is within the bounding rectangle. 

	returns true if vec is within the bounds, and false otherwise. 