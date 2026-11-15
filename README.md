
# 🚨 Siren Chase

**A timed racing game built in Unity.** Drive through a dirt forest track, pass the halfway checkpoint and cross the finish line before the 90-second timer runs out. But the police are on your tail. If they catch you, you are **busted!**

> Mid-Term Group Project


| | |
|---|---|
| **Engine** | Unity [6000.3.8f1, exact version from Unity Hub] |
| **Language** | C# |
| **Render pipeline** | Universal Render Pipeline (URP) |
| **Input** | Unity Input System (keyboard) |
| **Platform** | Window standalone build |
| **Team size** | 4 students |

---

##  Quick Links

| Deliverable | Link |
|---|---|
|  Gameplay video | Here: https://youtu.be/oSlNgdebAno |
|  Playable build (ZIP) |Here: https://drive.google.com/drive/folders/1kFggliEfVPwXmfdwr_ZWk_31l5Kly5Ty?usp=sharing |
|Git repository |Here: https://github.com/John88897/Driving-Racing_Game.git|

---

## Objective

You are a getaway driver racing through a forest dirt track with a police car chasing you. Drive **one complete lap**, passing the **halfway checkpoint** and then crossing the **finish line**, before the **90-second timer** runs out.

## Rules

| Outcome | Condition | On-screen message |
|---|---|---|
| **Win** | Cross the finish line **after** passing the halfway checkpoint, with time left |  **YOU WIN!** (shows time left) |
| **Lose: time** | The 90-second timer reaches 0 |  **TIME'S UP! You lose.** |
| **Lose: busted** | The police car hits your car |  **BUSTED! The police caught you.** |
|  **Restart** | Click the **Restart** button after a win or a loss | The level reloads |

**How a round works**

1. The round starts and the 90-second timer begins counting down.
2. The player's car is locked for the first **3 seconds** (start countdown).
3. The police car starts moving after **3 seconds** and chases you.
4. Drive around the track and pass through the **halfway checkpoint** ("Checkpoint reached!").
5. Return to the **finish line** to win.
6. Crossing the finish line **before** the checkpoint does not count. The game tells you to pass the checkpoint first.

## Controls

| Key | Action |
|---|---|
| **W** or **↑** | Accelerate |
| **S** or **↓** | Brake / reverse |
| **A** or **←** | Steer left |
| **D** or **→** | Steer right |
| **R** | Reset the car upright (if flipped or stuck) |
| **Mouse** | Click the **Restart** button after the game ends |



## Player Feedback (UI)

| UI element | Shows |
|---|---|
| **Timer** (top left) | Remaining time. Turns red in the last 10 seconds |
| **Status text** (top centre) | Instructions, checkpoint progress, win or loss message, **colour-coded** by state |
| **Controls hint** (bottom) | Quick reminder of the keys |
| **Restart button** | Appears when the game ends |

Status colours: ⚪ instructions · 🟡 checkpoint reached · 🟠 warning · 🟢 win · 🔴 lose.



## Physics Setup

- **Rigidbody** on both cars with a lowered centre of mass for stability and extra gravity to keep them grounded.
- **Box Colliders** on the car bodies, with the `CarSlide` Physics Material (low friction).
- **Trigger colliders** on `HalfPointTrigger` and `FinishLine`, using `OnTriggerEnter`.
- **Collision event** `OnCollisionEnter` on the police car to detect catching the player.
- **Terrain Collider** on the track terrain.

---

##  Setup and Play Instructions

### Option A: Play the build (easiest)

1. Download the ZIP from the **Playable build** link above.
2. **Extract the whole ZIP** to a folder. Do not run the game from inside the ZIP.
3. Open the extracted folder and double-click **`.exe` file extension**.
   - Keep the `_Data` folder and the other files next to the `.exe`.
4. If Windows shows a "Windows protected your PC" screen, click **More info → Run anyway**.
5. Play with the keyboard controls above.

> The build runs without the Unity Editor.

### Option B: Open the project in Unity

1. Install **Unity Hub** and the Unity Editor version **[6000.x.xx]** (use exactly this version).
2. Clone the repository:
   ```bash
   git clone https://github.com/John88897/Driving-Racing_Game.git
   ```
3. In Unity Hub, click **Add → Add project from disk** and select the cloned folder.
4. Open the project. The first load can take several minutes while Unity imports assets.
5. Open the scene: `Assets/Scenes/[RaceArea01].unity`.
6. Press **Play**.

### Create a build yourself

1. **File → Build Profiles**.
2. Select **Windows** (or macOS) and make sure the scene is in the **Scene List**.
3. Click **Build** and choose an empty folder.

---

##  Team Members and Contributions

###  Chan Veasna

- Set up the player car (`N_Muscle Car_10`) with Rigidbody, Box Collider and the `CarSlide` physics material
- Wrote and tuned `CarController.cs` (acceleration, speed limit, steering, extra gravity, sideways grip, 3-second start countdown)
- Wrote `FollowCamera.cs` so the camera follows the player car
- Wrote `CarRecovery.cs` (reset the car upright with the **R** key) and `FallRespawn.cs` (respawn when falling off the world)
- Tested and adjusted the car physics so the car stays on the ground

###  Roth Sorayuth

- Set up the police car (`Police Car N_4`) with Rigidbody and collider
- Wrote and tuned `PoliceChase.cs` (chase behaviour, speed, start delay, unstick logic)
- Implemented the "busted" lose condition when the police hit the player
- Balanced the police speed against the player's speed so the game is challenging but winnable
- Tested the chase on different parts of the track

###  Sarong Kimsorng

- Designed the dirt forest track, terrain, hills, trees and obstacles
- Placed boundaries so cars cannot leave the track
- Created the start line, the halfway checkpoint (`HalfPointTrigger`) and the finish line (`FinishLine`) with trigger colliders
- Wrote `LapTrigger.cs` for checkpoint and finish detection
- Tuned the layout so one lap fits within the 90-second time limit

### Heng Sreyka

- Wrote `GameManager.cs` (90-second timer, win and lose states, colour-coded status messages, restart)
- Built the UI in the Canvas (timer, status text, controls hint, Restart button)
- Created the Windows build, tested it outside the Unity Editor and uploaded the ZIP
- Recorded and uploaded the gameplay video
- Set up the GitHub repository and `.gitignore`, and wrote this README

### Shared by all members

- Playtested the game and reported bugs
- Reviewed each other's work in the repository
- Each member can explain their own scripts and the overall game

> See the Git commit history for the detailed record of each member's work.
---

##  Credits and References

### Tutorials followed
- How To Make A Driving & Racing Game In Unity - Tutorial 01 - Beginner Basics - Best Guide, 
Jimmy Vegas,
 Link: https://youtu.be/MQ5GJPlAGS4?si=eP2PhBgGOCzVfSzk: used for basic car components and movement guide


### Assets
| Asset | Creator | Source |
|---|---|---|
| Player car `N_Muscle Car_10` and police car `Police Car N_4` | [creator] | [Asset Store / website link] |
| Terrain textures: *Realistic Terrain Textures* | [creator] | [link] |
| Environment / trees: [exact pack names from your Project folders, e.g. *Mobile Optimized, Free L...*, *CartoonTra...*] | [creator] | [link] |
| [Sound / music, if any] |mixkit.co|

> Open each pack's folder in the Unity Project window and check its readme or Asset Store page to get the exact name, creator and link.

### Unity packages
- Universal Render Pipeline (URP)
- Input System
- TextMeshPro

### AI assistance
In line with the course's academic integrity rules, we declare the following use of AI tools:


- **Claude (Anthropic)**: used for explanations and help  and debugging C# scripts (car controller, police chase, game manager, checkpoint triggers, camera)


---

##  Known Limitations

- The police car chases in a straight line toward the player and has no pathfinding, so it can get stuck on trees or steep slopes. It frees itself after a short time.
- Only one level and one scene (as allowed by the brief).
- Keyboard controls only.
- [Add any other bugs or limits you know about.]

##  Project Structure

```
Assets/
 ├─ Scenes/        Main scene
 ├─ Scripts/       All C# scripts
 ├─ Prefabs/       Reusable game objects
 ├─ Material/      Materials and the CarSlide physics material
 └─ ...            Third-party asset packs
Packages/
ProjectSettings/
README.md
```


