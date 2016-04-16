using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
	static void Main(string[] args)
	{
		string[] inputs;
		inputs = Console.ReadLine().Split(' ');
		int w = int.Parse(inputs[0]); // width of the board
		int h = int.Parse(inputs[1]); // height of the board
		int playerCount = int.Parse(inputs[2]); // number of players (2 or 3)
		int myId = int.Parse(inputs[3]); // id of my player (0 = 1st player, 1 = 2nd player, ...)
		string myTargetDir = "";
		int targetWallXOffset = 0;
		int blockingWallXOffset = 0;
		if(myId == 0) {
			myTargetDir = "RIGHT";
			targetWallXOffset = 0;
			blockingWallXOffset = 1;
		}
		if(myId == 1) {
			myTargetDir = "LEFT";
			targetWallXOffset = 1;
		}
		if(myId == 2) {
			myTargetDir = "DOWN";
			targetWallXOffset = 1;
		}
		bool foundWall = false;
		// game loop
		int myX = 0;
		int myY = 0;
		int myWalls = 0;
		int tX = 0;
		int tY = 0;
		List<int> targets = new List<int>();
		bool wallAbove = false;
		bool wallBelow = false;
		bool canPlaceWall = true;
		int moveTimes = 1;
		while (true)
		{
			wallAbove = false;
			wallBelow = false;
			canPlaceWall = true;
			moveTimes = 1;
			foundWall = false;
			string dir = myTargetDir;
			Random r = new Random();
			targets = new List<int>();
			for (int i = 0; i < playerCount; i++)
			{

				inputs = Console.ReadLine().Split(' ');
				int x = int.Parse(inputs[0]); // x-coordinate of the player
				int y = int.Parse(inputs[1]); // y-coordinate of the player
				int wallsLeft = int.Parse(inputs[2]); // number of walls available for the player
				if(i == myId) {
					myX = x;
					myY = y;
					myWalls = wallsLeft;
				} else {
					if(i == 1 || i == 2){
						targets.Add(i);
						targets.Add(x);
						targets.Add(y);
					}
				}
				if (i != 2 && i != myId) {
					tX = x;
					tY = y;
				} 
			}
			int wallCount = int.Parse(Console.ReadLine()); // number of walls on the board
			for (int i = 0; i < wallCount; i++)
			{
				inputs = Console.ReadLine().Split(' ');
				int wallX = int.Parse(inputs[0]); // x-coordinate of the wall
				int wallY = int.Parse(inputs[1]); // y-coordinate of the wall
				string wallOrientation = inputs[2]; // wall orientation ('H' or 'V')
				if(wallOrientation == "V" && (myTargetDir == "RIGHT" || myTargetDir == "LEFT")){

					if(wallX == (tX + targetWallXOffset) && (wallY == tY || wallY == tY-1 || wallY == tY-2 || wallY == tY+1)){
						Console.Error.WriteLine("Debug messages... Wall in way of enemy");
						canPlaceWall = false;   
					}
					if((tY == myY || tY == myY-1) && ((myTargetDir == "RIGHT" && tX > myX ) || (myTargetDir == "LEFT" && tX < myX))){
						canPlaceWall = false;   
					}
					if(wallX == myX+ blockingWallXOffset && (wallY == myY || wallY == myY-1 || wallY == myY-2)){
						Console.Error.WriteLine(wallY.ToString() + " " + myY.ToString());
						if(wallY == (myY)) {
							Console.Error.WriteLine("Debug messages... to me");
							wallBelow = true;  
							foundWall = true;
						}
						if(wallY == (myY-2)){
							Console.Error.WriteLine("Debug messages... Wall above");
							wallAbove = true;
							foundWall = true;
						}
						if(myY == 0 && wallBelow){
							dir = "DOWN";
						} else if(wallBelow) {
							dir = "UP";
							Console.Error.WriteLine("Debug messages... dir = " + dir);
							Console.Error.WriteLine("Debug messages... Wall to me going up");
						}
						if(wallY == myY-1 ){
							Console.Error.WriteLine("Debug messages... Wall above going down");
							dir = "DOWN";
							if(myY == h-1) {
								dir = "UP";
								Console.Error.WriteLine("Debug messages... at bottom going up ");   
							}
						}
						if(wallAbove && wallBelow){
							Console.Error.WriteLine("Debug messages... above and below");
							dir = "DOWN";
							moveTimes = 1;
							Console.Error.WriteLine("Debug messages... down 3");

						}
					}

				}
				Console.Error.WriteLine("Debug messages... end wall loop");
			}
			Console.Error.WriteLine("Debug messages... end wall loop dir = " + dir);
			if(canPlaceWall && ((tY == 0 || tY == h-1) ||(r.Next(3) == 0 && myWalls > 0 && !foundWall && tX > 0))){
				//place wall
				int wallX = tX;
				int wallY = 0;
				if(tY < (h/2)){
					wallY = tY;
				} else {
					wallY = tY - 1;
					if(wallY >=h-1) wallY = h-2;
				}
				Console.WriteLine((wallX  + targetWallXOffset).ToString()+ " "+wallY.ToString()+" "+"V");
			} else {
				Console.Error.WriteLine("Debug messages... end wall loop dir = " + dir);
				for(int i = 0; i < moveTimes; i++){
					Console.Error.WriteLine("Debug messages... moving " + moveTimes.ToString() + dir);
					Console.WriteLine(dir);    
				}
			}
			// Write an action using Console.WriteLine()
			// To debug: Console.Error.WriteLine("Debug messages...");
			// action: LEFT, RIGHT, UP, DOWN or "putX putY putOrientation" to place a wall
		}
	}
}