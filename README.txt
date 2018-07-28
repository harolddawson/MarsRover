Mars Rover Solution:
Completed by Harold Dawson, III
July 28, 2018

This application makes the assumption that the user is prompted for information via the Console. 
During the prompt for input, the user can track as many rovers as they so desire -- until machine resources are exhausted or collection capacities have been reached, at which point exceptions will be thrown (and are unhandled by this application). 
The program will iterate indefinitely until the user does not enter information for the rover. After that point, the rover's feedback will be printed to the Console.

Design:
1. Models are relatively dumb -- The only model with any intelligence is the NasaMarsRover. It has methods that allow a service to provide instructions but the NasaMarsRover is the only class that knows what those methods really do. These methods are internal as well, by design, so that only the service within the class can provide the instructions.
2. Methods and properties within the NasaMarsRover class are internal unless necessary. The MarsPlateauNavigationService is how anything outside of the namespace should interact with the NasaMarsRover class. The Program class has the logic to instantiate the NasaMarsRover based on user input, but it must give that object to the MarsPlateauNavigationService for the rover to be able to do anything.
3. The bulk of the logic lives in the MarsRover.Core project to allow the UI for interacting with it to be replaced with something else, such as a web API, or a WPF UI, or anything else. The Program.cs class contains the logic to interact with the Console.

Assumptions:
1. The rover's mobility is limited to the plateau on which it is placed. If a rover is given instructions to move outside of the bounds of the plateau, the application will NOT give the instruction to the rover. Instead the instruction will be skipped and the next instruction in the sequence will be performed, assuming it does not instruct the rover to go outside of the bounds of the plateau.
2. Rovers can share a coordinate. This means if a rover is already on the plateau at a point, another rover can be given an instruction that would cause it to move to the same coordinate. It is assumed that such instructions would not be given, but no logic is built to prevent it.
