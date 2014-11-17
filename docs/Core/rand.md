### Rand

Rand gives random capabilities. It is the official supported random service for Whiskey2D. It is important to only use Rand to generate random numbers, instead of 3rd party random services. The reason is that Whiskey2D's replay service can only garuntee accuracy if it has control over random numbers. If a 3rd party random service is used, then the replay service will not have control over the random numbers, and the replay may differ from the original. 

#### Properties

*none*

#### Constructors

* **Rand.getInstance()**
	Rand is a singleton, and has a private constructor. To access Rand, use the static access method, getInstance(). 

#### Methods

* **int getSeed ()**
	Returns the seed that was used to generate random numbers.	

* **void setSeed (int seed)**
	Sets the seed that will be used to generate random numbers.

* **void reSeed ()**
	Randomly generate a new seed that will be used to generate random numbers.

* **int Next ()**
	Returns a random int value from 0 to the max integer value.

* **int Next (int max)**
	Returns a random int value from 0 to the specified max value.

* **int Next (int min, int max)**
	Returns a random int value from the specified min value, to the max value.

* **float nextFloat ()**
	Returns a random float from 0 to 1.

* **Vector nextUnit2 ()**
	Returns a random Vector that is has a length of 1.

* **Color nextColorVariation (Color baseColor, float redVar, float greenVar, float blueVar, float alphaVar)**
	Returns a Color similar to the given *baseColor*.
	The new Color will vary randomly in red, green, blue, and alpha, by the values given for *redVar, greenVar, blueVar* and *alphaVar*. Remember that in this case, the color variations should be given on a scale from 0 to 1. If a value of .5 is given for *redVar*, then the resulting Color will have the *baseColor*s red value, plus or minus .25

#### Example

```
Rand r = Rand.getInstance();
Color randomColor = r.nextColorVariation(Color.Red, .5f, .5f, .5f, .5f);

```