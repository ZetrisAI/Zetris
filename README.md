# Zetris

### MisaMino-based TETR.IO and Puyo Puyo Tetris bot

- [Discord](https://discord.gg/MGpTFsMDeh)
- [YouTube](https://www.youtube.com/@Zetris)
- [Twitch](https://www.twitch.tv/zetris_ppt)

maintained by [mat1jaczyyy](https://github.com/mat1jaczyyy)

## Local Installation with Puyo Puyo Tetris

The [Visual C++ 2022 Redistributable](https://aka.ms/vs/17/release/vc_redist.x64.exe) is required to run Zetris.

Extract the latest Zetris ZIP archive anywhere. Keep all of the files from the zip in the same place, as Zetris depends on those to work properly.

If this is your first time running Zetris, it will likely ask you for administrative rights in order to install ScpDriverInterface, a driver which simulates a virtual gamepad which Zetris uses to play the game. If you're using Windows 7 (64-bit), you also need the Xbox 360 controller driver from [here](https://www.microsoft.com/accessories/en-au/d/xbox-360-controller-for-windows).

Launch Zetris and it will connect to Puyo Puyo Tetris. To watch Zetris play, use Solo Arcade modes. To play against Zetris, use Multiplayer Arcade Versus. **Online is not supported to prevent cheating.**

## Usage

The Styles allow you to configure how Zetris will make decisions. You can edit these Styles to create your own in the Editor. It's possible to right-click the Styles in the list to manage them, as well as drag and drop to reorder.

The Speed setting configures how fast Zetris will play.

The Previews setting configures how many future pieces Zetris will consider while making a decision.

The Intelligence setting configures how many less obvious placements Zetris will explore (search width).

Other options found below allow you to additionally enable and configure additional specific strategies when fighting against Zetris.

The Gamepad Connected checkbox can be used to reconnect the gamepad if the game is not receiving any inputs from Zetris.

If you're playing with a gamepad yourself, you can tweak the MP Arcade Player parameter of Zetris to switch it to the other side of the screen if necessary.

To run multiple instances of Zetris, hold the Shift key while launching the program to select a different virtual gamepad for it to use (default is 4). Once you have multiple Zetris instances running on different controllers, all you need to do is correctly set the "MP Arcade Player" dial to make sure each bot is looking at the correct player.

If Accurate Game Sync is enabled, Zetris will constantly scan for the next game frame to help prevent frame skipping at the cost of increased CPU usage. Uncheck this only if your computer has performance issues while running Zetris.

## Training

1. Find a configuration you're comfortable playing against.
2. Increase the toughness of the bot to the point where it gives you a challenge, you can defeat it but only rarely.
3. Train until you can beat it consistently.
4. Repeat from Step 2.
