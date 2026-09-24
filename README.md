# Blackjack

A command-line based single-player game of Blackjack.

## Description

The game simulates a Blackjack table, where the user can play multiple rounds of Blackjack against the table's dealer. 
The user starts with 100 chips, which they then use to bet every round they play, with the goal of getting as many chips as possible.



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

The overall architecture of the game is shown in the below UML class diagram
![Class diagram](/Documentation/ClassDiagram.png)

### The Table singleton
At the core is a Table singleton, which operates as a global reference point that both the Gambler and Dealer classes can access the assigned CardDeck instance through.\
It is responsible for managing the round order and starting both the user's and dealer's turn in order. Before announcing and managing the outcome of the round.\
It is also here that the user is asked if they want to go another round.

### Player, Gambler and Dealer classes
For the two players at the table, three classes are used:

The Player superclass handles the basics that both the dealer and user can use.
Defining their playing hands and the handles funtions for Hit and Stand.

The Gambler class inherits from Player, and represents the user.
Handling the user's turn and what actions they can perform during it.
It also contains the number of chips the user has, and is responsible for handling bet placements and the gains/losses/returns from the round's outcome.

While the Dealer class, which also inherits from Player, represents the dealer at the table.
Handling the dealer's decision making during their turn.
The dealer will draw cards until they either beat the user's hand, or they have a hand with a score of 17 or higher without any Aces counting as 11. 
The UML graph below shows the activity of the dealer's decision making
![Activity diagram over the dealer's decision making](/Documentation/DealersDecisionMaking.png)

### Card struct and CardDeck class
The cards are represented with the Card struct.
Each Card variable represents an individuel card, and has a defined Rank and a Type.\
The Rank is an int, which corresponds to the card's number, with 1 being Aces, 11 being Jacks, 12 is Queen, and 13 being Kings.\
While the Type variable denotes whether the card is a number, face card, or an ace.

The table has a single CardDeck object on it, which represents a deck of cards that the players can draw from.
Each new deck consists of exactly 4 of each card, which gets shuffled at the start of each round.

### Other systems and classes
The Scorer class is a static class that is used to calculate the scoring of the user and dealer's hands.
The class has functionality to determine what each Ace card should score, and can also be used to detect if a score is soft, meaning that an Ace card is scoring 11 for it.

And lastly, the Input class is called by both the Gambler and Table classes for user input. Input managing the user input and returning it to the calling object.

