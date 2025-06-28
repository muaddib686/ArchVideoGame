# Game Project Architecture

This repository contains assignments for the "Game Project Architecture" course.

First of all, I'd like to specify that I use Rust (which is not an object-oriented language) as my daily driver language for a couple of years, so some of my OOP implementations may look a bit weird. I don't like OOP anymore and find a lot of mainstream OOP patterns weird.

## Assignment 1

GameManager is a singleton that manages implements the game state machine.

It also stores instances of all states, because states are, well, states.
Some of them store some internal data, for example GamePlayState stores a counter value.

There are 3 states:
- MainMenuState
- GamePlayState
- PauseState

Also, there is UIService. It encapsulates all UI interactions.
