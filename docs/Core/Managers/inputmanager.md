### InputManager

The InputManager is used to know if a key is being pressed, or what the current mouse position is. 

#### Properties

*none*

#### Constructors

* **GameManager.Input**
	The InputManager should be referenced through the GameManager

#### Methods

* **void init ()**
	Called by Whiskey2D to start the manager.

* **void close ()**
	Called by Whiskey2D to close the manager.

* **void update ()**
	Called by Whiskey2D to update the manager.

* **bool isKeyDown (Keys key)**
	Checks if the specified key is being pushed. 

* **bool isNewKeyDown (Keys key)**
	Checks to see if the specified key has *just* been pushed. This will only return true when the key is pushed down.

* **Vector getMousePosition ()**
	Gets the position of the mouse.

* **bool isMouseDown (MouseButtons button)**
	Checks if the specified mouse button is being pushed.

* **bool isNewMouseDown (MouseButtons button)**
	Checks if the specified mouse button has *just* been pushed.

#### Example
```

if (GameManager.Input.isNewKeyDown(Keys.Right)){
	//the player just hit the right key
}

```