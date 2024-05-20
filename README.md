# Zetris

Zetris - a MisaMino-based Tetris AI for Puyo Puyo Tetris and TETR.IO

[Zetris highlights on YouTube](https://www.youtube.com/@Zetris)

## Local Installation with Puyo Puyo Tetris

Zetris sends input to the game via a virtual gamepad. You need to install the driver for the virtual gamepad from [here](https://github.com/mogzol/ScpDriverInterface/releases/download/1.1/ScpDriverInterface_v1.1.zip) before using Zetris. If you're using Windows 7 (64-bit), you also need the Xbox 360 controller driver from [here](https://www.microsoft.com/accessories/en-au/d/xbox-360-controller-for-windows).

Extract the latest Zetris ZIP archive anywhere. Keep all of the files from the zip in the same place, as Zetris depends on those to work properly.

Launch Zetris and it will connect to Puyo Puyo Tetris. To watch Zetris play, use Solo Arcade modes. To play against Zetris, use Multiplayer Arcade Versus. **Online is not supported to prevent cheating.**

If you're playing with a gamepad yourself, you can tweak the MP Arcade Player parameter of Zetris to switch it to the other side of the screen if necessary.

## Usage

The Styles allow you to configure how Zetris will make decisions. You can edit these Styles to create your own in the Editor. It's possible to right-click the Styles in the list to manage them, as well as drag and drop to reorder.

The Speed setting configures how fast Zetris will play.

The Previews setting configures how many future pieces Zetris will consider while making a decision.

The Hold Allowed, Perfect Clear Finder, Center 4-Wide and TSD only checkboxes allow you to additionally enable these specific strategies when fighting against Zetris.

The Gamepad Connected checkbox can be used to reconnect the gamepad if the game is not receiving any inputs from Zetris.

## Training

1. Find a configuration you're comfortable playing against.
2. Increase the toughness of the bot to the point where it gives you a challenge, you can defeat it but only rarely.
3. Train until you can beat it consistently.
4. Repeat from Step 2.
