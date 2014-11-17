### Script

A Script controls behaviour in a GameObject. Each GameObject instance will have its own copies of every Script. Every Script must be built to work with a specific kind of GameObject, by stating a certain kind of GameObject in the class defintion. For example, a Script called *DriveScript* that works with GameObjects called *Car*, the following class signature would be required,
```
class DriveScript : Script<Car>
```

Every script must implement 2 methods from Script. The first is *onStart*, and the second is *onUpdate*. This is where the control code goes that controls the GameObject.

#### Properties

* **GameObject Gob**		(get)
	The Gob will be a GameObject of the type stated in the class signature. 

#### Constructors

* **Script ()**
	Creates a Script

#### Methods

* **void onStart ()**
	This method will be called when the Script is added to a GameObject instance. 

* **void onUpdate ()**
	This method will be called once every tick.

#### Example

```

//sample GameObject
class Car : GameObject
{
	public int Wheels {get; set;} //test property
}

class DriveScript : Script<Car>
{

	public override void onStart(){
		Gob.Wheels = 4;
	}

	public override void onUpdate(){
		Gob.X += Gob.Wheels;
	}

}

```