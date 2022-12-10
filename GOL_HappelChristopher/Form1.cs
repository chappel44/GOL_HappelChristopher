using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOL_HappelChristopher
{
    public partial class Form1 : Form
    {
        //The maximum cooridinates for the universes x and y by default 25
        int universeXmax = 25;
        int universeYmax = 25;
        
        //variable to control simulation speed by default 500
        int simSpeed = 500;

        int livingCells = 0; //used to keep track of living cells

        // The universe array
        bool[,] universe = new bool[0, 0];

        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.BlueViolet;
        Color bgColor = Color.CadetBlue;
        Color neighborNumClr = Color.Red;

        //Temp colors
        Color tempGridColor = Properties.Settings.Default.GridColor;
        Color tempCellColor = Properties.Settings.Default.CellColor;
        Color tempBgColor = Properties.Settings.Default.BGColor;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        //variable used for the seed
        static int seed = 1001; 

        public Form1()
        {
            InitializeComponent();

            Form1_Load();

            universeTL.Text = $"  Universe: X = {universeXmax}, Y = {universeYmax}"; //initialized the text to the max coordinates of x and y
            simSpeedTL.Text = $"Simulation Speed = {simSpeed}"; //initializes and displays simSpeed

            // Setup the timer
            timer.Interval = simSpeed; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running

            graphicsPanel1.Invalidate();
        }
        

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            bool[,] sandbox = new bool[universeXmax, universeYmax]; //used to keep track of changes

            livingCells = 0; //Used to keep track of living cells

            for (int y = 0; y < sandbox.GetLength(1); y++)
            {
                for (int x = 0; x < sandbox.GetLength(0); x++)
                {
                    int neighbors = Neighbors.Count(universe, x, y); //gives the amount  of neighbors

                    if (ToroidialTMS.Checked) //Checks to see if the tool strip menu for toroidial is checked
                    {
                        CheckBorders(ref neighbors, x, y);
                    }

                    if (universe[x, y]) //check true cell's conditions
                    {
                        livingCells++; //Incremenets the number of living cells

                        sandbox[x, y] = true; //by default true

                        //checks the neighbor count
                        if (neighbors < 2 | neighbors > 3) //changes to false if one of these conditions are met
                            sandbox[x, y] = false;
                    }
                    else //checks dead cell's condition
                    {
                        if (neighbors == 3) //if the dead cell has 3 neighbors it becomes true
                            sandbox[x, y] = true;
                    }
                }
            }

            universe = sandbox.Clone() as bool[,]; //applies changes in sandbox to universe

            // Increment generation count
            generations++;

            //updates status strip living cells
            toolStripStatus_LivingCells.Text = "Living Cells = " + livingCells.ToString();

            //updates status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();

            //updates HUDs generations
            generationsTL.Text = "  Generations = " + generations.ToString();

            //updates HUDs livings cells
            livingCellsTL.Text = "Living Cells = " + livingCells.ToString();

            graphicsPanel1.Invalidate();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);

            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);
            
            bool swapped = false;
            
            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    Rectangle cellRect = Rectangle.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    int neighborCount = Neighbors.Count(universe, x, y); //Gets the amount of neighbors

                    if (ToroidialTMS.Checked) //checks to see if the tool strip menu for toroidial is checked
                    {
                        CheckBorders(ref neighborCount, x, y); //runs the border check
                    }

                    neighborNumClr = Color.Red; //by default red

                    if (neighborCount >= 2 && neighborCount <= 3 && universe[x, y]) //if the cells alive and count is between 2 and 3 the color becomes green
                        neighborNumClr = Color.Green;

                    if (!universe[x, y] && neighborCount == 3) //if the cells dead and count is 3 color neighbor number color becomes green
                        neighborNumClr = Color.Green;

                    // Brush for drawing the neighbor amount
                    Brush neighborCountBrush = new SolidBrush(neighborNumClr);

                    //String format used to center where neighborCount will print
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    // Fill the cell with a brush if alive
                    if (universe[x, y])
                        e.Graphics.FillRectangle(cellBrush, cellRect);

                    //draws the neighbor count if the neighbor count setting is enabled and count is greater than 0
                    if (neighborCount > 0 && neighborCountTSM.Checked) 
                        e.Graphics.DrawString(neighborCount.ToString(), graphicsPanel1.Font, neighborCountBrush, cellRect, stringFormat);
                    
                    // Outline the cell with a pen if the grid setting is enabled
                    if(gridOnOffTSM.Checked) 
                        e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);

                    neighborCountBrush.Dispose();
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }


        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = e.Y / cellHeight;

                // Toggle the cell's state
                universe[x, y] = !universe[x, y];

                //adds one if living cell clicked on is true decreases if the living cell is false
                if (universe[x, y])
                    livingCells++;
                else
                    livingCells--;

                toolStripStatus_LivingCells.Text = "Living Cells = " + livingCells.ToString(); //updates count of living cells in the status bar

                livingCellsTL.Text = "Living Cells = " + livingCells.ToString(); //updates living cells in the HUD

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }


        


        ///////////////// Button Strips ////////////
        private void exitMenuStrip_Click(object sender, EventArgs e) //Exit Button
        {
            this.Close();
        }


        private void pauseStripButton_Click(object sender, EventArgs e) //Pause Button
        {
            timer.Stop();
        }


        private void playStripButton_Click(object sender, EventArgs e) //Play Button
        {
            timer.Start();
        }


        private void nextStripButton_Click(object sender, EventArgs e) //Next button
        {
            if (!timer.Enabled) //Next will only work while paused
                NextGeneration();
        }


        private void clearStripButton_Click(object sender, EventArgs e) //Clear button
        {
            Clear();
        }


        ///////////////////// File Input Output ////////////////////
        private void saveAsMenuStrip_Click(object sender, EventArgs e)
        {
            FileIO.SaveAs(universe);
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileIO.Open(ref universe);

            //resizes universe x and y max to new universe size
            universeXmax = universe.GetLength(0);
            universeYmax = universe.GetLength(1);

            //updates the HUD
            universeTL.Text = $"  Universe: X = {universeXmax}, Y = {universeYmax}";

            graphicsPanel1.Invalidate();
        }


        ////////////////// Menu strip options /////////////////////////


        //////// Color Section ////////


        private void cellColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangeObjColor(ref cellColor, ref tempCellColor);
        }


        private void gridColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeObjColor(ref gridColor, ref tempGridColor);
        }


        private void backgroundColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangeObjColor(ref bgColor, ref tempBgColor, true);
        }

        //MS stands for menu strip
        private void UndoColorMS_Click(object sender, EventArgs e) //Menu strip for returning the users last made changes
        {
            //Changes colors to the temporary variables
            ProgramColors(tempCellColor, tempGridColor, tempBgColor);

            graphicsPanel1.Invalidate();
        }


        private void ResetColorMS_Click(object sender, EventArgs e) //Menu strip for resetting system defaults
        {
            //Resets the properties to original values specified in GOL_HappelChristopher -> Properties -> Settings. 
            Properties.Settings.Default.Reset();

            //Restores colors to program defaults
            ProgramColors(Properties.Settings.Default.CellColor, Properties.Settings.Default.GridColor, Properties.Settings.Default.BGColor);

            graphicsPanel1.Invalidate();
        }


        ///////// Speed Section ////////
        
        
        private void simulationSpeedToolStripMenuItem2_Click(object sender, EventArgs e) //menu strip for changing universe speed
        {
            DisplaySpeedChangeMenu();
        }


        private void fastSpeedStripMenuItem2_Click(object sender, EventArgs e)
        {
            //multiplies simSpeed by the parameter. Same thing for the next 2.
            ChangeSpeed(.5f);
        }


        private void normalSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSpeed(1f);
        }


        private void slowSpeedStripMenuItem3_Click(object sender, EventArgs e)
        {
            ChangeSpeed(1.5f);
        }


        /////////// Boundary Section /////////////
        

        private void ToroidialTMS_Click(object sender, EventArgs e)
        {
            ToggleBoundary("Toroidial", true, false);
        }


        private void finiteTMS_Click(object sender, EventArgs e)
        {
            ToggleBoundary("Finite", false, true);
        }


        private void universeSizeToolStripMenuItem_Click(object sender, EventArgs e) //changes the universe size through a menu strip
        {
            ChangeUniverseMenu();
        }


        //////////// HUD Section ////////////


        private void HUD_TSM_Click(object sender, EventArgs e) //tool strip menu for toggling the HUD
        {
            //Context menu   =      Tool strip menu
            HUD_CMS.Checked = HUD_TSM.Checked;
            ToggleHUD();
            graphicsPanel1.Invalidate();
        }


        private void gridOnOffTSM_Click(object sender, EventArgs e) //tool strip menu for toggling grid
        {
            //Context menu   =      Tool strip menu
            grid_CMS.Checked = gridOnOffTSM.Checked;

            graphicsPanel1.Invalidate();
        }

        private void neighborCountTSM_Click(object sender, EventArgs e)
        {
            //Context menu strip      =          Tool strip menu
            neighborCount_CMS.Checked = neighborCountTSM.Checked;
            
            graphicsPanel1.Invalidate();
        }
        

        //////////////// Randomization Section /////////////


        private void fromSeedToolStripMenuItem_Click(object sender, EventArgs e) // menu strip generate random from seed
        {
            RandSeedBox randBox = new RandSeedBox(seed);

            if (DialogResult.OK == randBox.ShowDialog())
            {
                seed = randBox.GetSeed();//Sets the seed to the value returned from up down in randbox

                RandomizeUniverse(true); //parameters is true because we are generating random from a seed
            }
        }


        private void currentSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomizeUniverse(true); //parameters is true because we are generating random from a seed
        }


        private void fromTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomizeUniverse();

            graphicsPanel1.Invalidate();
        }


        ////////////////////////// Context menu options /////////////////////
        
        
        ///////////// Color Section ////////////


        private void CellColorContextMenu_Click(object sender, EventArgs e) //context menu for changing color of cells
        {
            ChangeObjColor(ref cellColor, ref tempCellColor);
        }


        private void GridColorCM_Click(object sender, EventArgs e) //context menu for changing grid color
        {
            ChangeObjColor(ref gridColor, ref tempGridColor);
        }


        private void BgColorContextMenu_Click(object sender, EventArgs e) //context menu for changing background color
        {
            ChangeObjColor(ref bgColor, ref tempBgColor, true);
        }


        private void CellCountContextMenu_Click(object sender, EventArgs e) //Context menu for changing universe size
        {
            ChangeUniverseMenu();
        }


        private void simSpeedCM_Click(object sender, EventArgs e) //CM stands for Context Menu
        {
            DisplaySpeedChangeMenu();
        }


        ////////////// HUD Section /////////////


        private void toroidialCMS_Click(object sender, EventArgs e) //context menu for changing boundary to Toroidial
        {
            ToggleBoundary("Toroidial", true, false);
        }


        private void finiteCMS_Click(object sender, EventArgs e) //context menu for changing boundary to Finite
        {
            ToggleBoundary("Finite", false, true);
        }


        private void HUD_CMS_Click(object sender, EventArgs e) //context menu for toggling the HUD
        {
            //Tool strip menu    =  Context menu strip
            HUD_TSM.Checked = HUD_CMS.Checked;
            ToggleHUD();
            graphicsPanel1.Invalidate();
        }


        private void neighborCount_CMS_Click(object sender, EventArgs e) 
        {
            //Tool strip menu        =         Context menu strip
            neighborCountTSM.Checked = neighborCount_CMS.Checked;

            graphicsPanel1.Invalidate();
        }


        private void grid_CMS_Click(object sender, EventArgs e)
        {
            //Tool strip menu    =  Context menu strip
            gridOnOffTSM.Checked = grid_CMS.Checked;

            graphicsPanel1.Invalidate();
        }


        ////////////////////// Save User Changes ///////////////////
        private void saveSettings_Closed(object sender, FormClosedEventArgs e) //Saves the users changes upon being closed
        {
            Properties.Settings.Default.CellColor = cellColor;//saves the cell color
            Properties.Settings.Default.GridColor = gridColor; //saves grid color
            Properties.Settings.Default.BGColor = bgColor; //Saves the background color

            Properties.Settings.Default.XMax = universeXmax; //saves the universe x max
            Properties.Settings.Default.YMax = universeYmax; //saves the universe y max

            Properties.Settings.Default.SimSpeed = simSpeed; //saves simulation speed

            Properties.Settings.Default.Save();
        }


        //////////////////// Load User Changes ///////////////////
        private void Form1_Load() //Loads saved data from settings
        {
            ProgramColors(Properties.Settings.Default.CellColor, Properties.Settings.Default.GridColor, Properties.Settings.Default.BGColor);

            CreateUniverse(Properties.Settings.Default.XMax, Properties.Settings.Default.YMax);

            simSpeed = Properties.Settings.Default.SimSpeed;
        }


        ///////////////// Support Functions ////////////////
        void CheckBorders(ref int neighbors, int x, int y)
        {
            if (x % (universe.GetLength(0) - 1) == 0 && y % (universe.GetLength(1) - 1) == 0)
            {
                neighbors += Neighbors.CountCorner(universe, x, y);
            }
            if (x % (universe.GetLength(0) - 1) == 0)
            {
                neighbors += Neighbors.CountEdgesX(universe, x, y);
            }
            if (y % (universe.GetLength(1) - 1) == 0)
            {
                neighbors += Neighbors.CountEdgesY(universe, x, y);
            }
        }


        void ChangeUniverseMenu()
        {
            ModalDialog dlg = new ModalDialog(universe.GetLength(0), universe.GetLength(1)); //Creates Dialog box for the universes size

            if (DialogResult.OK == dlg.ShowDialog())
            {
                //checks to see if the x or y max have changed
                if (universeXmax == dlg.GetUDRow() && universeYmax == dlg.GetUDColumn()) //if the current x and y of the universe are equal to the values in the up down nothing has changed
                {
                }
                else //if x or y is it will create a new universe
                {
                    CreateUniverse(dlg.GetUDRow(), dlg.GetUDColumn()); //creates a new universe the size of the changes
                    universeTL.Text = $"  Universe: X = {universeXmax}, Y = {universeYmax}";
                    graphicsPanel1.Invalidate();
                }
            }
        }


        void DisplaySpeedChangeMenu()
        {
            ModalSimSpeed dlg = new ModalSimSpeed(simSpeed); //creates an instance of the window for adjusting simspeed

            if (DialogResult.OK == dlg.ShowDialog()) //applies the changes when the ok button clicked or enter is pressed
            {
                simSpeed = dlg.GetUDSpeed(); //sim speed becomes the value returned from the up down 
                timer.Interval = simSpeed; //updates the interval
                simSpeedTL.Text = $"Simulation Speed = {simSpeed}"; //changes the simulation speed displayed on the HUD
            }
        }


        void ChangeSpeed(float speedMultiplier) //easier way for the user user to change the simulation speed
        {
            float modifiedSpeed = simSpeed * speedMultiplier; //multiplies the simSpeed by the amount specified in the menu option

            if (modifiedSpeed < 1) //prevents speed from going lower than 1 which causes an exception
                modifiedSpeed = 1; //sets the speed to 1

            timer.Interval = (int)modifiedSpeed; //Sets timer interval to modifiedSpeed

            simSpeedTL.Text = $"Simulation Speed = {simSpeed}"; //updates the HUDs text to the new sim speed
        }


        void CreateUniverse(int newX, int newY)
        {
            universeXmax = newX; //Changes universe x max to the newX parameter
            universeYmax = newY; //Changes universe y max to the newY parameter
            universe = new bool[universeXmax, universeYmax]; //Creates new universe
        }


        void ProgramColors(Color cellClr, Color gridClr, Color bgClr)
        {
            //changes the program colors to the parameters
            cellColor = cellClr;
            gridColor = gridClr;
            graphicsPanel1.BackColor = bgClr;
        }


        void ChangeObjColor(ref Color color, ref Color tempColor, bool isGraphicPanel = false)
        {
            tempColor = color;//assigns the current color to the temporary for if the user wants to undo changes

            color = VisualSettings.ChangeColor(color); //assigns the color parameter to the value returned from function

            if (isGraphicPanel) //if you are changing the graphics panels color it will do so here
                graphicsPanel1.BackColor = color;

            graphicsPanel1.Invalidate();
        }


        void ToggleHUD()
        {
            //Changes the visible property the opposite of it's current value (Toggles)
            universeTL.Visible = !universeTL.Visible;
            simSpeedTL.Visible = !simSpeedTL.Visible;
            generationsTL.Visible = !generationsTL.Visible;
            livingCellsTL.Visible = !livingCellsTL.Visible;
            boundaryTL.Visible = !boundaryTL.Visible;
        }


        void ToggleBoundary(string type, bool isToroidial, bool isFinite)
        {
            boundaryTL.Text = $"Boundary Type: {type}"; //Update the HUDs boundary type

            //changes the tool strip menu and context menu strip to value passed in by parameter
            finiteTMS.Checked = isFinite;
            finiteCMS.Checked = isFinite;

            //changes the tool strip menu and context menu strip to value passed in by parameter
            ToroidialTMS.Checked = isToroidial;
            toroidialCMS.Checked = isToroidial;
        }


        void RandomizeUniverse(bool isSeed = false)
        {
            Random rand = new Random();

            if (isSeed) //if you intend to generate from a seed it assigns the seed
                rand = new Random(seed);

            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    int boolNum = rand.Next(0, 2); // creates a random number between 0 and 1.

                    //assigns universe a bool based on whether randNum is 0 or 1.
                    if (boolNum == 1)
                        universe[x, y] = true;
                    else
                        universe[x, y] = false;
                }
            }

            graphicsPanel1.Invalidate();
        }


        void Clear()
        {
            universe = new bool[universeXmax, universeYmax]; //clears changes by creating a new version of itself

            livingCells = 0;

            toolStripStatus_LivingCells.Text = "Living Cells = " + livingCells.ToString(); //updates the text in the status bar

            livingCellsTL.Text = "Living Cells = " + livingCells.ToString(); //updates the text in the HUD

            graphicsPanel1.Invalidate();
        }
    }
}