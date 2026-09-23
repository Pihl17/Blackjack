# Blackjack

A command-line based single-player game of Blackjack.

## Description



## Getting Started

### Dependencies

- .NET 10

### Building and executing the program

Open up a command prompt terminal and run the following command:\
`dotnet build <Path to solution folder>\Blackjack --output <output path>`

The executable can then be found in the defined output folder, which you can open to run the game.

### Building and testing the solution

Open up a command prompt terminal and run the following command:\
`dotnet test <Path to solution folder>`

If the solution haven't been built yet, the command will first build it before running the test suite.



## Software Architecture

Table singleton
- Used as a global reference point that both the Gambler and Dealer classes can access, and through those access the CardDeck
- Has the player and dealer sitting at it, and a card deck placed on it.

Gambler class
- The player class
- Handles the player's turn and translating input into actions during their turn
- Has their own hand
- Handles the placing of bets and how many chips the player has

Dealer class
- Has their own hand
- Handles the dealer's decision making during their turn
- The dealer will only hit for another card under certain conditions, [the conditions and order are shown here](/Documentation/DealersDecisionMaking.png)
![Activity diagram over the dealer's decision making](/Documentation/DealersDecisionMaking.png)

Player class
- Superclass for both the Dealer and Gambler classes
- Handles the functions for Hits and Stands

Card struct
- Represents each individuel card
- Has Rank and Type (Numbered/Face/Ace) variables

CardDeck class
- Contains (and represents) a stack of cards
- The players can draw from it

Scorer static class
- Used to calculate the score of each player's hand
- Manages the use-cases of Ace scoring

Input class
- Class for managing user input


