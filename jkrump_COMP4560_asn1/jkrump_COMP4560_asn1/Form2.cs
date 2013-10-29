//
// COMP4560: Assignment 2 - draw squares
// C# starting from a Windows application project.
//

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace jkrump_COMP4560_asn1
{

    public partial class SquareGrid : Form
    {
        public bool white_first = true;

        public SquareGrid()
        {
            // This is code to be added tot he default constructor set up by visual studio.

            Text = "C4560: Assignment2: Part 2 (Joseph Krump 4C/2013)";
            BackColor = Color.Black;
            ResizeRedraw = true;
            // enable double-buffering to avoid flicker
            // copied from http://www.publicjoe.f9.co.uk/csharp/card09.html
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // Add an annonymous MouseMove event handler function to change the value of 'white_first' when
            // the mouse cursor moves past the x midpoint.
            base.MouseMove += (object sender, MouseEventArgs e) =>
            {
                white_first = e.X < this.ClientRectangle.Width / 2;
                this.Refresh();
            };
        }

        protected override void OnPaint(PaintEventArgs pea)
        {
            // Client canvas dimensions.
            int cx = ClientSize.Width;
            int cy = ClientSize.Height;
            int mid_x = cx / 2;
            int ulx = 0;
            int uly = 0;

            ulx = white_first ? 0 : mid_x;

            Graphics graphics = pea.Graphics;
            graphics.FillRectangle(Brushes.White, ulx, uly, mid_x, cy);

            // draw the grid of squares
            drawGrid(pea, 30, 20, cx, cy);

        }

        ///<summary>
        ///Draws a grid of small squares in a given space
        ///</summary>
        private void drawGrid(PaintEventArgs pea, int s, int g, int w, int h)
        {
            Graphics grfx = pea.Graphics;

            grfx.SmoothingMode = SmoothingMode.HighQuality;
            grfx.PixelOffsetMode = PixelOffsetMode.HighQuality;


            int area_width = (w / 2) - (s / 2);
            int area_height = h - (s + g);
            int cols = area_width / (s + g);
            int rows = area_height / (s + g);

            int mid_x = (w / 2); // x midpoint.

            int uly; // upper-left y location
            int ulxr; // upper-left x location for right-side square
            int ulxl; // upper-left x location for left-side square

            for (int i = 0; i < rows; i++)
            {
                uly = (s / 2) + g + (i * (s + g));

                for (int j = 0; j < cols; j++)
                {
                    ulxr = mid_x + (j * (s + g));
                    ulxl = mid_x - s - (j * (s + g));

                    // Draw squares

                    // draw left
                    grfx.FillRectangle(Brushes.LawnGreen, ulxl, uly, s, s);

                    // draw right
                    grfx.FillRectangle(Brushes.LawnGreen, ulxr, uly, s, s);
                }
            }
        }

    }     
}