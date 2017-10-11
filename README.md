# Simput

Simput is a simple C# input manager for Monogame. Internally it wraps the Keyboard, Mouse, and Gamepad classes from Monogame and provides a simple abstraction with a lot of useful features.

#### Features

* Easy key/button checking: IsDown, IsUp, WasPressed, WasReleased
* Easy access to values.
  * Mouse position
  * Triggers
  * Analog Sticks
* Map actions to multiple inputs.
  * PlayerInput.WasActionPressed("Jump");
* Virtual cursor and/or cursor locking.
* Easily check if an input type was used/touched.

## Installation

**Nuget**

* Coming soon

**ZIP**

* Download the project
* Build it
* Reference the compiled .DLL in your Monogame projects.

## Usage

Simput can be used in a variety of ways. 

**SimpleKeyboard**, **SimpleMouse**, and **SimpleGamePad** offer abstractions from the Keyboard, Mouse, and GamePad classes.

**Input** contains an instance of SimpleKeyboard, SimpleMouse, and up to four SimpleGamePads and handles updating and managing them.

**PlayerInput** let's you manage input mappings for up to four players.

### Basics

To begin using Simput add a reference to it in your project and add using statements where necessary.

The easiest way to use Simput is via the `Input` class. 

```csharp
void Initialize()
{
  Input.Initialize(game); //Initialize once.
}

void Update(GameTime gameTime)
{
  Input.Update(); //Update once per frame.
  
  //Example of checking input to exit the game.
  if(Input.Keyboard.WasKeyPressed(Keys.Escape) || Input.GamePad(PlayerIndex.One).WasButtonPressed(Buttons.Back))
  {
    Exit();
  }
}
```

Input must be initialized with the Game object, and updated once every frame.

The `Input.Mouse`, `Input.Keyboard`, and `Input.GamePad` classes contain similar methods for checking the state of buttons/keys:

*Replace 'Button' with 'Key' for Keyboard*

* `IsButtonDown(Buttons.A);`
* `IsButtonUp(Buttons.A);`
* `WasButtonPressed(Buttons.A);`
* `WasButtonReleased(Buttons.A);`

The **Is** methods determine whether a button is in a certain state.

The **Was** methods determine if a button has changed state this frame.

### Advanced

Simput also makes it easy to map actions (e.g. jump, move left, shoot) to inputs using InputActions and InputMaps.

Furthermore Simput contains PlayerInput which assigns up to four players their own InputMap which can be configured specifically for them.

To learn more about InputMaps and advanced features of Simput visit the wiki.

## Credits

Authors:

* Frank Norton

## License

Simput is under the MIT license.