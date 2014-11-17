### GameObject

GameObjects represent the core level of Whiskey2D. Everything is a GameObject. When users create games, they should create new classes that extend GameObject.

#### Properties

* **Sprite Sprite**			(get set)
	The Sprite that will be used to visualize the GameObject.

* **float X**				(get set)
	The x coordinate of the GameObject.

* **float Y**				(get set)
	The y coordinate of the GameObject.

* **Vector Position**		(get set)
	The Vector that represents the position of the GameObject.

* **int ID**				(get set)
	The unique int identifing the GameObject.

* **Bounds Bounds**			(get)
	The bounding rectangle of the GameObject. This is generated from the Position and the Sprite.


#### Constructors

* **GameObject ()**
	Creates a new GameObject. All new GameObjects are automatically added the GameManager's ObjectManager.


#### Methods

* **void update ()**
	Called once a tick. On the update method, all of the GameObject's scripts will invoke the onUpdate() method.

* **void init ()**
	Called when the GameObject is created.

* **void close ()**
	Called to remove the GameObject from the GameManager's ObjectManager.

* **void addScript (Script script)**
	Add a script to the GameObject's collection of Scripts. It is very important that the script that is added is built to work with the GameObject.

* **void removeScript (Script script)**
	Remove a script from the GameObject's collection of Scripts. 

* **abstract void addInitialScripts ()**
	All sub classes must implement this method. This is a chance for the user to call the addScript () method and have scripts be added by default.

#### Example
```

class Car : GameObject{
	
	public int Wheels { get; set; }

	protected override void addInitialScripts(){
		this.addScript(new DriveScript());
	}

}

```