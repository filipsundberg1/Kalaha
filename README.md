# Kalaha

The classic board game Kalaha (also known as Mancala), playable in the terminal. Written in C# (.NET 8).

Built as the project in the course Object-Oriented Programming 1 (OOP1), 2024–2025. The in-game text is in Swedish.

## Features

- **Two game modes:** play against a friend on the same computer, or against the computer
- **Two computer opponents:**
  - Random: picks a random non-empty pit
  - Greedy: always picks the pit with the most stones
- **Full Kalaha rules:** sowing, extra turns, captures and end-of-game scoring
- **Built-in rules introduction** for new players
- **Play again** after each game

## Rules

The board has 12 pits with 4 stones each, 6 per player, plus one store per player.

1. On your turn, pick up all stones from one of your pits and sow them one by one counter-clockwise.
2. If your last stone lands in your own store, you get another turn.
3. If your last stone lands in an empty pit on your side, you capture it together with the stones in the opposite pit.
4. The game ends when one player's pits are all empty. Remaining stones go to their owner's store.
5. The player with the most stones in their store wins.

## Getting started

Requires the [.NET 8 SDK](https://dotnet.microsoft.com/download).

```bash
git clone https://github.com/filipsundberg1/Kalaha.git
cd Kalaha
dotnet run
```

Pits are numbered 1–6 for player 1 and 8–13 for player 2.

## Code structure

| File | Responsibility |
| --- | --- |
| `Program.cs` | Entry point |
| `Meny.cs` | Menus, rules text and play-again prompt |
| `SpeletsGång.cs` | Game loop, sowing, captures and scoring |
| `Spelplan.cs` | The board and how it is drawn |
| `Grop.cs` | A single pit or store |
| `VemsTur.cs` | Keeps track of whose turn it is |
| `MänskligSpelare.cs` | Player name input |
| `IAiDrag.cs` | Interface for computer strategies |
| `AiSlump.cs`, `AiHögsta.cs` | The random and greedy computer opponents |

## Object-oriented concepts

The course requirements are documented in comments in the code (marked `KRAV 1` to `KRAV 6`):

- **Encapsulation:** player data is kept private inside its class
- **Constructor overloading:** separate constructors for two-player and computer games
- **Computed properties:** the winner is calculated when needed instead of stored
- **Composition:** the board is built from `Grop` objects
- **Dependency injection:** the computer strategy is passed into the game
- **Polymorphism:** both computer opponents implement the `IAIDrag` interface

## Author

Filip Sundberg
