//
// COMP4560: Assignment 2 - draw circles
// C# starting from a Windows application project.
//

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace jkrump_COMP4560_asn1
{

    public partial class CirclesWindow : Form
    {
        public CirclesWindow()
        {
            // This is code to be added tot he default constructor set up by visual studio.

            Text = "C4560: Assignment2: Part 1 (Joseph Krump 4C/2013)";
            BackColor = Color.Black;
            ResizeRedraw = true;
            // enable double-buffering to avoid flicker
            // copied from http://www.publicjoe.f9.co.uk/csharp/card09.html
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

        }

        protected override void OnPaint(PaintEventArgs pea)
        {
            Graphics grfx = pea.Graphics;

            grfx.SmoothingMode = SmoothingMode.HighQuality;
            grfx.PixelOffsetMode = PixelOffsetMode.HighQuality;

            int cx = ClientSize.Width;
            int cy = ClientSize.Height;
            
            // Get the midpoint for the drawing region.
            int mx = cx / 2;
            int my = cy / 2;

            int s = cx <= cy ? cx : cy; // s will equal the smaller of the two.

            // Radiuses for the circles being drawn are based off of 's'.
            int small_diameter = s / 6;
            int large_diameter = s / 3;

            // Get containing square upper left corner coordinates

            int small_left_x = mx - (s / 3);
            int large_left_x = mx - (5 * s / 12);
            int small_bottom_y = my + (s / 6);
            int large_bottom_y = my + (s / 12);

            int small_right_x = mx + (s / 6);
            int large_right_x = mx + (s / 12);
            int small_top_y = my - (s / 3);
            int large_top_y = my - (5 * s / 12);

            // Brushes
            SolidBrush red = new SolidBrush(Color.Red);
            SolidBrush green = new SolidBrush(Color.Green);
            SolidBrush blue = new SolidBrush(Color.Blue);
            SolidBrush yellow = new SolidBrush(Color.Yellow);

            // Draw large circles
            grfx.FillEllipse(blue, large_left_x, large_top_y, large_diameter, large_diameter);
            grfx.FillEllipse(blue, large_left_x, large_bottom_y, large_diameter, large_diameter);
            grfx.FillEllipse(yellow, large_right_x, large_top_y, large_diameter, large_diameter);
            grfx.FillEllipse(yellow, large_right_x, large_bottom_y, large_diameter, large_diameter);

            // Draw small circles
            grfx.FillEllipse(green, small_left_x, small_top_y, small_diameter, small_diameter);
            grfx.FillEllipse(red, small_left_x, small_bottom_y, small_diameter, small_diameter);
            grfx.FillEllipse(green, small_right_x, small_top_y, small_diameter, small_diameter);
            grfx.FillEllipse(red, small_right_x, small_bottom_y, small_diameter, small_diameter);
        }
    }
}