### ObjectManager

The ObjectManager keeps track of all GameObjects in the game. 

#### Properties

*none*

#### Constructors

* **GameManager.Objects**
	Access the ObjectManager through the GameManager.

#### Methods

* **void init ()**
	Called by Whiskey2D to initialize the manager.

* **void close ()**
	Called by Whiskey2D to close the manager.

* **void updateAll ()**
	Called by Whiskey2D to update every GameObject.

* **void addObject ()**
	Called by Whiskey2D when a new GameObject is created.

* **void removeObject ()**
	Called by Whiskey2D when a GameObject is closed.

* **List(GameObject) getAllObjects ()**
	Gets all of the GameObjects in the game.

* **List(GameObject) getAllObjectsNotOfType(G) () where G : GameObject**
	Gets all of the GameObjects in the game that are not of the specified type.

* **List(G) getAllObjectsOfType(G) () where G : GameObject**
	Gets all of the GameObjects in the game that are of the specified type.

* **State getState ()**
	Gets the current State of the game. 

* **void setState (State state)**
	Sets the ObjectManager from the given State.