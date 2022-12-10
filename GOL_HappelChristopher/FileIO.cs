using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOL_HappelChristopher
{
    public class FileIO
    {
        public static void SaveAs(bool[,] universe)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2; dlg.DefaultExt = "cells";

            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamWriter writer = new StreamWriter(dlg.FileName);

                for(int y = 0; y < universe.GetLength(1); y++) 
                {
                    string currentRow = string.Empty;
                    for(int x = 0; x < universe.GetLength(0); x++)
                    {
                        if (universe[x, y])
                            currentRow += 'O';
                        else
                            currentRow += '.';
                    }
                    writer.WriteLine(currentRow);
                }
                writer.Close();
            }
        }
        public static void Open(ref bool[,] universe)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2; dlg.DefaultExt = "cells"; 

            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamReader reader = new StreamReader(dlg.FileName);
                    
                //Next two variables used in the for loop for the exit conditions
                int xMax = 0; //maximum x value or width of the universe
                int yMax = 0; // maximum y value or height

                while (!reader.EndOfStream)
                {
                    string row = reader.ReadLine(); // string used to keep track of what characters are in the row

                    if (row[0] == '!') //continues to next line if the row beigns with exclamation point
                        continue;
                    else
                    {
                        yMax++;     //increments y max each time through the loop
                        xMax = row.Length;  //assigns the x max to the amount of characters in row
                    }
                }

                reader.BaseStream.Seek(0, SeekOrigin.Begin);//sets files input pointer to the beginning of the file

                universe = new bool[xMax, yMax];//resizes the 2d bool array 

                //Iterate through file again, this time reading in the cells

                int y = 0; //used to keep track of the y coordinate of the universe

                while (!reader.EndOfStream)
                {
                    string row = reader.ReadLine();// string used to keep track of what characters are in the row

                    if (row[0] == '!') //skips the line if begins with ! (comment)
                        continue;
                    
                    for (int x = 0; x < row.Length; x++)
                    {
                        if (row[x] == 'O') //sets universes x and y position to true if the element is a 'O' or sets to false for any other type of character
                        {
                            universe[x, y] = true;
                        }
                    }
                    y++;//incremenets
                }
                reader.Close(); //closes file
            }
        }
    }
}
