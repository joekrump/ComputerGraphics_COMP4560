using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;

namespace asgn5v1
{
	/// <summary>
	/// Summary description for Transformer.
	/// </summary>
	public class Transformer : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;

		// basic data for Transformer

		int numpts = 0;
		int numlines = 0;
		bool gooddata = false;		
		double[,] vertices;
		double[,] scrnpts;
		double[,] ctrans = new double[4,4];  //your main transformation matrix
		private System.Windows.Forms.ImageList tbimages;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton transleftbtn;
		private System.Windows.Forms.ToolBarButton transrightbtn;
		private System.Windows.Forms.ToolBarButton transupbtn;
		private System.Windows.Forms.ToolBarButton transdownbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton scaleupbtn;
		private System.Windows.Forms.ToolBarButton scaledownbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton rotxby1btn;
		private System.Windows.Forms.ToolBarButton rotyby1btn;
		private System.Windows.Forms.ToolBarButton rotzby1btn;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Windows.Forms.ToolBarButton rotxbtn;
		private System.Windows.Forms.ToolBarButton rotybtn;
		private System.Windows.Forms.ToolBarButton rotzbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton shearrightbtn;
		private System.Windows.Forms.ToolBarButton shearleftbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ToolBarButton resetbtn;
		private System.Windows.Forms.ToolBarButton exitbtn;
        private System.Threading.Timer t; // timer to be used for continuous rotations.
		int[,] lines;
        delegate void RefreshEvent();
        RefreshEvent refresh;

		public Transformer()
		{
			//
			// Required for Windows Form Designer support
			//
            InitializeComponent();

			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			Text = "COMP 4560:  Assignment 5 (201230) Joseph Krump";
			ResizeRedraw = true;
			BackColor = Color.Black;
			MenuItem miNewDat = new MenuItem("New &Data...",
				new EventHandler(MenuNewDataOnClick));
			MenuItem miExit = new MenuItem("E&xit", 
				new EventHandler(MenuFileExitOnClick));
			MenuItem miDash = new MenuItem("-");
			MenuItem miFile = new MenuItem("&File",
				new MenuItem[] {miNewDat, miDash, miExit});
			MenuItem miAbout = new MenuItem("&About",
				new EventHandler(MenuAboutOnClick));
			Menu = new MainMenu(new MenuItem[] {miFile, miAbout});
            refresh += InvalidateWindow;
		}

        public void InvalidateWindow()
        {
            Invalidate(ClientRectangle);
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transformer));
            this.tbimages = new System.Windows.Forms.ImageList(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.transleftbtn = new System.Windows.Forms.ToolBarButton();
            this.transrightbtn = new System.Windows.Forms.ToolBarButton();
            this.transupbtn = new System.Windows.Forms.ToolBarButton();
            this.transdownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.scaleupbtn = new System.Windows.Forms.ToolBarButton();
            this.scaledownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.rotxby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotyby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotzby1btn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.rotxbtn = new System.Windows.Forms.ToolBarButton();
            this.rotybtn = new System.Windows.Forms.ToolBarButton();
            this.rotzbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.shearrightbtn = new System.Windows.Forms.ToolBarButton();
            this.shearleftbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.resetbtn = new System.Windows.Forms.ToolBarButton();
            this.exitbtn = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // tbimages
            // 
            this.tbimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tbimages.ImageStream")));
            this.tbimages.TransparentColor = System.Drawing.Color.Transparent;
            this.tbimages.Images.SetKeyName(0, "");
            this.tbimages.Images.SetKeyName(1, "");
            this.tbimages.Images.SetKeyName(2, "");
            this.tbimages.Images.SetKeyName(3, "");
            this.tbimages.Images.SetKeyName(4, "");
            this.tbimages.Images.SetKeyName(5, "");
            this.tbimages.Images.SetKeyName(6, "");
            this.tbimages.Images.SetKeyName(7, "");
            this.tbimages.Images.SetKeyName(8, "");
            this.tbimages.Images.SetKeyName(9, "");
            this.tbimages.Images.SetKeyName(10, "");
            this.tbimages.Images.SetKeyName(11, "");
            this.tbimages.Images.SetKeyName(12, "");
            this.tbimages.Images.SetKeyName(13, "");
            this.tbimages.Images.SetKeyName(14, "");
            this.tbimages.Images.SetKeyName(15, "");
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.transleftbtn,
            this.transrightbtn,
            this.transupbtn,
            this.transdownbtn,
            this.toolBarButton1,
            this.scaleupbtn,
            this.scaledownbtn,
            this.toolBarButton2,
            this.rotxby1btn,
            this.rotyby1btn,
            this.rotzby1btn,
            this.toolBarButton3,
            this.rotxbtn,
            this.rotybtn,
            this.rotzbtn,
            this.toolBarButton4,
            this.shearrightbtn,
            this.shearleftbtn,
            this.toolBarButton5,
            this.resetbtn,
            this.exitbtn});
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.tbimages;
            this.toolBar1.Location = new System.Drawing.Point(484, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(24, 306);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // transleftbtn
            // 
            this.transleftbtn.ImageIndex = 1;
            this.transleftbtn.Name = "transleftbtn";
            this.transleftbtn.ToolTipText = "translate left";
            // 
            // transrightbtn
            // 
            this.transrightbtn.ImageIndex = 0;
            this.transrightbtn.Name = "transrightbtn";
            this.transrightbtn.ToolTipText = "translate right";
            // 
            // transupbtn
            // 
            this.transupbtn.ImageIndex = 2;
            this.transupbtn.Name = "transupbtn";
            this.transupbtn.ToolTipText = "translate up";
            // 
            // transdownbtn
            // 
            this.transdownbtn.ImageIndex = 3;
            this.transdownbtn.Name = "transdownbtn";
            this.transdownbtn.ToolTipText = "translate down";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // scaleupbtn
            // 
            this.scaleupbtn.ImageIndex = 4;
            this.scaleupbtn.Name = "scaleupbtn";
            this.scaleupbtn.ToolTipText = "scale up";
            // 
            // scaledownbtn
            // 
            this.scaledownbtn.ImageIndex = 5;
            this.scaledownbtn.Name = "scaledownbtn";
            this.scaledownbtn.ToolTipText = "scale down";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxby1btn
            // 
            this.rotxby1btn.ImageIndex = 6;
            this.rotxby1btn.Name = "rotxby1btn";
            this.rotxby1btn.ToolTipText = "rotate about x by 1";
            // 
            // rotyby1btn
            // 
            this.rotyby1btn.ImageIndex = 7;
            this.rotyby1btn.Name = "rotyby1btn";
            this.rotyby1btn.ToolTipText = "rotate about y by 1";
            // 
            // rotzby1btn
            // 
            this.rotzby1btn.ImageIndex = 8;
            this.rotzby1btn.Name = "rotzby1btn";
            this.rotzby1btn.ToolTipText = "rotate about z by 1";
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxbtn
            // 
            this.rotxbtn.ImageIndex = 9;
            this.rotxbtn.Name = "rotxbtn";
            this.rotxbtn.ToolTipText = "rotate about x continuously";
            // 
            // rotybtn
            // 
            this.rotybtn.ImageIndex = 10;
            this.rotybtn.Name = "rotybtn";
            this.rotybtn.ToolTipText = "rotate about y continuously";
            // 
            // rotzbtn
            // 
            this.rotzbtn.ImageIndex = 11;
            this.rotzbtn.Name = "rotzbtn";
            this.rotzbtn.ToolTipText = "rotate about z continuously";
            // 
            // toolBarButton4
            // 
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // shearrightbtn
            // 
            this.shearrightbtn.ImageIndex = 12;
            this.shearrightbtn.Name = "shearrightbtn";
            this.shearrightbtn.ToolTipText = "shear right";
            // 
            // shearleftbtn
            // 
            this.shearleftbtn.ImageIndex = 13;
            this.shearleftbtn.Name = "shearleftbtn";
            this.shearleftbtn.ToolTipText = "shear left";
            // 
            // toolBarButton5
            // 
            this.toolBarButton5.Name = "toolBarButton5";
            this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // resetbtn
            // 
            this.resetbtn.ImageIndex = 14;
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.ToolTipText = "restore the initial image";
            // 
            // exitbtn
            // 
            this.exitbtn.ImageIndex = 15;
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.ToolTipText = "exit the program";
            // 
            // Transformer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(508, 306);
            this.Controls.Add(this.toolBar1);
            this.Name = "Transformer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Transformer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private void Transformer_Load(object sender, EventArgs e)
        {
        }
		#endregion
        
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Transformer());
		}

		protected override void OnPaint(PaintEventArgs pea)
		{
			Graphics grfx = pea.Graphics;
            Pen pen = new Pen(Color.White, 3);
			double temp;
			int k;
            
            if (gooddata)
            {
                //create the screen coordinates:
                // scrnpts = vertices*ctrans

                for (int i = 0; i < numpts; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        temp = 0.0d;
                        for (k = 0; k < 4; k++)
                            temp += vertices[i, k] * ctrans[k, j];
                        scrnpts[i, j] = temp;
                    }
                }

                //now draw the lines

                for (int i = 0; i < numlines; i++)
                {
                    grfx.DrawLine(pen, (int)scrnpts[lines[i, 0], 0], (int)scrnpts[lines[i, 0], 1],
                        (int)scrnpts[lines[i, 1], 0], (int)scrnpts[lines[i, 1], 1]);
                }


            } // end of gooddata block	
		} // end of OnPaint

		void MenuNewDataOnClick(object obj, EventArgs ea)
		{
			//MessageBox.Show("New Data item clicked.");
			gooddata = GetNewData();
			RestoreInitialImage();			
		}

		void MenuFileExitOnClick(object obj, EventArgs ea)
		{
			Close();
		}

		void MenuAboutOnClick(object obj, EventArgs ea)
		{
			AboutDialogBox dlg = new AboutDialogBox();
			dlg.ShowDialog();
		}

		void RestoreInitialImage()
		{
            // assuming that the character is drawn approx 20x20
            double init_q_height = 20;
            double q_scale_factor;
            // Client canvas dimensions.
            int cx = ClientSize.Width - 24;//minus 24 because that is the width of the toolbar.
            int cy = ClientSize.Height;
            double x_mid = cx / 2;
            double y_mid = cy / 2;

            double[,] translate_10_10 = new double[4, 4];
            double[,] rotate_90_cw = new double[4, 4];
            double[,] translate_negative_10_10 = new double[4, 4];
            double[,] scale = new double[4, 4];
            double[,] translate_to_center = new double[4, 4];
            double[][,] matrices = new double[5][,];

            // initialize tranformation matricies to identity matrix.
            setIdentity(translate_10_10, 4, 4);
            setIdentity(rotate_90_cw, 4, 4);
            setIdentity(translate_negative_10_10, 4, 4);
            setIdentity(scale, 4, 4);
            setIdentity(translate_to_center, 4, 4);

            // get the scale factor which is said to be 1/2 of the window's height.
            q_scale_factor = y_mid / init_q_height;

            // assign transformation matrices proper values.

            // center q on 0,0 by translating by -10, -10
            translate_negative_10_10[3, 0] = -vertices[0, 0];
            translate_negative_10_10[3, 1] = -vertices[0, 1];
            matrices[0] = translate_negative_10_10;
            // rotate 90 deg clockwise
            rotate_90_cw[0, 0] = 0;
            rotate_90_cw[0, 1] = 1;
            rotate_90_cw[1, 0] = -1;
            rotate_90_cw[1, 1] = 0;
            matrices[1] = rotate_90_cw;
            //scale
            scale[0, 0] = q_scale_factor;
            scale[1, 1] = q_scale_factor;
            scale[2, 2] = q_scale_factor;
            matrices[2] = scale;
            // translate back to starting position by 10, 10
            translate_10_10[3, 0] = -vertices[0, 0];
            translate_10_10[3, 1] = -vertices[0, 1];
            matrices[3] = translate_10_10;
            // translate to center
            translate_to_center[3, 0] = x_mid;
            translate_to_center[3, 1] = y_mid;
            matrices[4] = translate_to_center;

            // get net transformation matrix
            ctrans = make_net_transform(matrices);
            // redraw
            Invalidate();

		} // end of RestoreInitialImage

		bool GetNewData()
		{
			string strinputfile,text;
			ArrayList coorddata = new ArrayList();
			ArrayList linesdata = new ArrayList();
			OpenFileDialog opendlg = new OpenFileDialog();
			opendlg.Title = "Choose File with Coordinates of Vertices";
			if (opendlg.ShowDialog() == DialogResult.OK)
			{
				strinputfile=opendlg.FileName;				
				FileInfo coordfile = new FileInfo(strinputfile);
				StreamReader reader = coordfile.OpenText();
				do
				{
					text = reader.ReadLine();
					if (text != null) coorddata.Add(text);
				} while (text != null);
				reader.Close();
				DecodeCoords(coorddata);
			}
			else
			{
				MessageBox.Show("***Failed to Open Coordinates File***");
				return false;
			}
            
			opendlg.Title = "Choose File with Data Specifying Lines";
			if (opendlg.ShowDialog() == DialogResult.OK)
			{
				strinputfile=opendlg.FileName;
				FileInfo linesfile = new FileInfo(strinputfile);
				StreamReader reader = linesfile.OpenText();
				do
				{
					text = reader.ReadLine();
					if (text != null) linesdata.Add(text);
				} while (text != null);
				reader.Close();
				DecodeLines(linesdata);
			}
			else
			{
				MessageBox.Show("***Failed to Open Line Data File***");
				return false;
			}
			scrnpts = new double[numpts,4];
			setIdentity(ctrans,4,4);  //initialize transformation matrix to identity

			return true;
		} // end of GetNewData

		void DecodeCoords(ArrayList coorddata)
		{

			//this may allocate slightly more rows that necessary
			vertices = new double[coorddata.Count,4];
			numpts = 0;
			string [] text = null;
			for (int i = 0; i < coorddata.Count; i++)
			{
				text = coorddata[i].ToString().Split(' ',',');
                vertices[numpts, 0] = double.Parse(text[0]);
				if (vertices[numpts,0] < 0.0d) break;
                vertices[numpts, 1] = double.Parse(text[1]);
				vertices[numpts,2]=double.Parse(text[2]);
				vertices[numpts,3] = 1.0d;
				numpts++;						
			}
			
		}// end of DecodeCoords

		void DecodeLines(ArrayList linesdata)
		{
			//this may allocate slightly more rows that necessary
			lines = new int[linesdata.Count,2];
			numlines = 0;
			string [] text = null;
			for (int i = 0; i < linesdata.Count; i++)
			{
				text = linesdata[i].ToString().Split(' ',',');
				lines[numlines,0]= int.Parse(text[0]);
				if (lines[numlines,0] < 0) break;
				lines[numlines,1]=int.Parse(text[1]);
				numlines++;						
			}
		} // end of DecodeLines

		void setIdentity(double[,] A,int nrow,int ncol)
		{
			for (int i = 0; i < nrow;i++) 
			{
				for (int j = 0; j < ncol; j++) A[i,j] = 0.0d;
				A[i,i] = 1.0d;
			}
            
		}// end of setIdentity    

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
            if (t != null)
            {
                t.Dispose();
                t = null;
            }
            // translate left
			if (e.Button == transleftbtn)
			{
                double[,] trans_left_50 = new double[4, 4];
                setIdentity(trans_left_50, 4, 4);
                trans_left_50[3, 0] = -50;
                ctrans = matrix_multiplier(ctrans, trans_left_50);
                Invalidate();
			}
            // translate right
			if (e.Button == transrightbtn) 
			{
                double[,] trans_right_50 = new double[4, 4];
                setIdentity(trans_right_50, 4, 4);
                trans_right_50[3, 0] = 50;
                ctrans = matrix_multiplier(ctrans, trans_right_50);
                Invalidate();
			}
            // translate up 
			if (e.Button == transupbtn)
			{
                ctrans = matrix_multiplier(ctrans, MakeTranslateYMatrix(-25));
                Invalidate();
			}
			// translate down
			if(e.Button == transdownbtn)
			{
                ctrans = matrix_multiplier(ctrans,MakeTranslateYMatrix(25));
                Invalidate();
			}
            // increase scale
			if (e.Button == scaleupbtn) 
			{
                Scale(1.1);
			}
            // decrease scale
			if (e.Button == scaledownbtn) 
			{
                Scale(0.9);
			}
            // single rotation by x.
			if (e.Button == rotxby1btn) 
			{
                Rotate(0.05, 'x');
                Invalidate();              
			}
            // single rotation by y.
			if (e.Button == rotyby1btn) 
			{
                Rotate(0.05, 'y');
                Invalidate();
			}
            // single rotation by z.
			if (e.Button == rotzby1btn) 
			{
                Rotate(0.05, 'z');
                Invalidate();
			}

			if (e.Button == rotxbtn) 
			{
                // check to see if the timer is already initialized.
                // if it is we don't want to start another concurrent timer.
                if (t == null)
                {
                    t = new System.Threading.Timer(RotateXContinuously, null, 10, 10);
                }
			}
			if (e.Button == rotybtn) 
			{
                // check to see if the timer is already initialized.
                // if it is we don't want to start another concurrent timer.
                if (t == null)
                {
                    t = new System.Threading.Timer(RotateYContinuously, null, 10, 10);
                }
			}
			
			if (e.Button == rotzbtn) 
			{
                // check to see if the timer is already initialized.
                // if it is we don't want to start another concurrent timer.
                if (t == null)
                {
                    t = new System.Threading.Timer(RotateZContinuously, null, 10, 10);
                }
			}

			if(e.Button == shearleftbtn)
			{
                ShearX(0.1);
			}

			if (e.Button == shearrightbtn) 
			{
                ShearX(-0.1);
			}

			if (e.Button == resetbtn)
			{
				RestoreInitialImage();
			}

			if(e.Button == exitbtn) 
			{
				Close();
			}

		}
        private double[,] MakeTranslateYMatrix(double amount)
        {
            double[,] trans = new double[4, 4];
            setIdentity(trans, 4, 4);
            trans[3, 1] = amount;
            return trans;
        }

        private void ShearX(double shear_factor)
        {
            double[][,] transformation_matrices = new double[4][,];
            double[,] shear_matrix = new double[4, 4];
            double translateYAmt = scrnpts[10, 1]; //the y value of one of the bottom points of the q. 
            transformation_matrices[0] = ctrans;
            transformation_matrices[1] = MakeTranslateYMatrix(-translateYAmt);
            setIdentity(shear_matrix, 4, 4);
            shear_matrix[1, 0] = shear_factor;
            transformation_matrices[2] = shear_matrix;
            transformation_matrices[3] = MakeTranslateYMatrix(translateYAmt);
            ctrans = make_net_transform(transformation_matrices);
            Invalidate();
        }
        private void Scale(double scale_factor)
        {
            double[,] scale = new double[4, 4];
            double[][,] transformation_matrices = new double[4][,];

            transformation_matrices[0] = ctrans;
            // initialize the transformation matrices to the identity matrix
            setIdentity(scale, 4, 4);
            transformation_matrices[1] = TranslateToOrigin();
            // set values for scaling down by 10%
            scale[0, 0] = scale_factor;
            scale[1, 1] = scale_factor;
            scale[2, 2] = scale_factor;
            transformation_matrices[2] = scale;
            transformation_matrices[3] = UndoTranslateToOrigin(transformation_matrices[1]);
            ctrans = make_net_transform(transformation_matrices);
            Invalidate();
        }
        private double[,] TranslateToOrigin(){
            double[,] to_origin_matrix = new double[4,4];
            setIdentity(to_origin_matrix, 4, 4);
            to_origin_matrix[3, 0] = -scrnpts[0, 0];
            to_origin_matrix[3, 1] = -scrnpts[0, 1];
            return to_origin_matrix;
        }

        private double[,] UndoTranslateToOrigin(double[,] to_origin_matrix)
        {
            double[,] back_from_origin_matrix = new double[4, 4];
            setIdentity(back_from_origin_matrix, 4, 4);
            back_from_origin_matrix[3, 0] = -to_origin_matrix[3, 0];
            back_from_origin_matrix[3, 1] = -to_origin_matrix[3, 1];
            return back_from_origin_matrix;
        }

        private void RotateXContinuously(object obj)
        {
            Rotate(0.05, 'x');
        }

        private void RotateYContinuously(object obj)
        {
            Rotate(0.05, 'y');
        }

        private void RotateZContinuously(object obj)
        {
            Rotate(0.05, 'z');
        }


        private static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }
        private void reset_ctrans()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    ctrans[i, j] = 0;
                }
            }
        }

        public void Rotate(double rotation_rads, char rotation_axis)
        {
            double[,] rotation_matrix = new double[4, 4];
            double[,] move_to_origin = new double[4, 4];
            double[,] move_to_back = new double[4, 4];
            double[][,] transformation_matrices = new double[4][,];

            transformation_matrices[0] = ctrans;

            setIdentity(move_to_origin, 4, 4);
            move_to_origin[3, 0] = -scrnpts[0, 0];
            move_to_origin[3, 1] = -scrnpts[0, 1];
            move_to_origin[3, 2] = -scrnpts[0, 2];

            transformation_matrices[1] = move_to_origin;

            setIdentity(rotation_matrix, 4, 4);
            if (rotation_axis == 'z')
            {
                rotation_matrix[0, 0] = Math.Cos(rotation_rads);
                rotation_matrix[0, 1] = Math.Sin(rotation_rads);
                rotation_matrix[1, 0] = -Math.Sin(rotation_rads);
                rotation_matrix[1, 1] = Math.Cos(rotation_rads);
            }
            else if(rotation_axis == 'y')
            {
                rotation_matrix[0, 0] = Math.Cos(rotation_rads);
                rotation_matrix[0, 2] = Math.Sin(rotation_rads);
                rotation_matrix[2, 0] = -Math.Sin(rotation_rads);
                rotation_matrix[2, 2] = Math.Cos(rotation_rads);
            }
            else if (rotation_axis == 'x')
            {
                rotation_matrix[1, 1] = Math.Cos(rotation_rads);
                rotation_matrix[1, 2] = Math.Sin(rotation_rads);
                rotation_matrix[2, 1] = -Math.Sin(rotation_rads);
                rotation_matrix[2, 2] = Math.Cos(rotation_rads);
            }

            transformation_matrices[2] = rotation_matrix;

            setIdentity(move_to_back, 4, 4);
            move_to_back[3, 0] = -move_to_origin[3, 0];
            move_to_back[3, 1] = -move_to_origin[3, 1];
            move_to_back[3, 2] = -move_to_origin[3, 2];

            transformation_matrices[3] = move_to_back;

            ctrans = make_net_transform(transformation_matrices);
            try
            {
                Invoke(refresh);
            }
            catch (Exception) { }
        }

 
        private double[,] matrix_multiplier(double[,] m1, double[,] m2)
        {
            double[,] resulting_matrix = new double[4,4];
            double val;
            // multiply a row onto multiple columns
            for (int i = 0; i < 4; i++)
            {     
                for (int j = 0; j < 4; j++)
                {
                    val = 0;
                    for(int k = 0; k < 4; k++){
                        val += m1[i, k] * m2[k, j];
                    }
                    resulting_matrix[i, j] = val;
                }       
            }
            return resulting_matrix;
        }

        private double[,] make_net_transform(double[][,] matrices)
        {
            int matrix_count = matrices.Length;
            double[,] net_transform = new double[4,4];
            // if passed an empty array return the identity matrix.
            if(matrix_count <= 0)
            {
                setIdentity(net_transform, 4, 4);
                return net_transform;
            }
            // if only 1 matrix then the first matrix is the net transform
            if(matrix_count == 1)
            {
                return matrices[0];
            }
            // if at least 2 then can safely multiply the first two matrices together to get the first part of net transform.
            net_transform = matrix_multiplier(matrices[0], matrices[1]);
            // go through the remaining matricies and multiply onto the previous net.
            for (int i = 2; i < matrix_count; i++)
            {
                net_transform = matrix_multiplier(net_transform, matrices[i]);
            }

            return net_transform;
        }
	}
}
