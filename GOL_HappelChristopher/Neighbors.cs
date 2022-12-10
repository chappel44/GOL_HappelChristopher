using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOL_HappelChristopher
{
    public class Neighbors
    {
        public static int Count(bool[,] universe, int cellX, int cellY)
        {
            int neighbors = 0;

            //Sets up the maximum points that the for loop will iterate through
            int yMax = cellY + 1;
            int xMax = cellX + 1;

            if (yMax == universe.GetLength(1)) //decrements whem the y max is equal to the max length of the y axis
            {
                yMax--;
            }

            if (xMax == universe.GetLength(0)) //decrements when the x max is equal to the max length of the x axis
            {
                xMax--;
            }

            //Sets up the minimum points that the for loop will iterate through
            int yMin = cellY - 1; 
            int xMin = cellX - 1;

            if (yMin < 0)
            {
                yMin = 0;
            }

            if (xMin < 0) //if the min is lower than 0 it's set to 0 to prevent exception
            {
                xMin = 0;
            }

            for (int y = yMin; y <= yMax; y++)
            {
                for (int x = xMin; x <= xMax; x++)
                {
                    if (x == cellX && y == cellY) //skips when the x and y are the same coordinate as the parameters passed in
                    {
                        continue;
                    }
                    if (universe[x, y]) //adds to count when a cell is true
                    {
                        neighbors++;
                    }
                }
            }

            return neighbors; 
        }

        public static int CountEdgesX(bool[,] universe, int cellX, int cellY) //Checks the edges on the x axis
        {
            int neighbors = 0;

            int xOpposite = universe.GetLength(0) - 1 - cellX; //Finds the other side of the X position

            int yMin = cellY - 1; //gets the starting point of the for loop

            if(yMin < 0) { yMin = 0; } //if it's less than 0 it's set to 0 to prevent exceptions

            int yMax = cellY + 1; //gets the y max by adding 1

            if(yMax == universe.GetLength(1)) { yMax--; } //if it's equal to the length it subtracts to prevent exceptions

            //iterate through
            for (int y = yMin; y <= yMax; y++)
            {
                if (universe[xOpposite, y]) //adds to the count if the conditions true
                {
                    neighbors++; 
                }
            }

            return neighbors;
        }

        public static int CountEdgesY(bool[,] universe, int cellX, int cellY) 
        {
            int neighbors = 0; //initializes neighbors to 0

            //gets the opposite side by subtracting the maximum y - 1 by cellY
            int yOpposite = universe.GetLength(1) - 1 - cellY; 

            //sets up the starting point of the for loop
            int xMin = cellX - 1;

            if (xMin < 0) { xMin = 0; }

            // sets up the exit condition
            int xMax = cellX + 1;

            if (xMax == universe.GetLength(0)) { xMax--; }

            for (int x = xMin; x <= xMax; x++)
            {
                if (universe[x, yOpposite])
                {
                    neighbors++;
                }
            }

            return neighbors;
        }
        public static int CountCorner(bool[,] universe, int cellX, int cellY)
        {
            int neighbors = 0;

            //gets 
            int xOpposite = universe.GetLength(0) - 1 - cellX;
            int yOpposite = universe.GetLength(1) - 1 - cellY;

            if (universe[xOpposite, yOpposite])
                neighbors++;

            return neighbors;
        }
    }
}
