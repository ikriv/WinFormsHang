using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;

namespace Freezer
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class FreezerForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Button btnFreezeEm;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FreezerForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            this.btnFreezeEm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFreezeEm
            // 
            this.btnFreezeEm.Location = new System.Drawing.Point(89, 122);
            this.btnFreezeEm.Name = "btnFreezeEm";
            this.btnFreezeEm.Size = new System.Drawing.Size(115, 23);
            this.btnFreezeEm.TabIndex = 0;
            this.btnFreezeEm.Text = "Freeze \'em!";
            this.btnFreezeEm.Click += new System.EventHandler(this.btnFreezeEm_Click);
            // 
            // FreezerForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(292, 267);
            this.Controls.Add(this.btnFreezeEm);
            this.Name = "FreezerForm";
            this.Text = "Freezer";
            this.ResumeLayout(false);

        }
		#endregion

        [DllImport("user32.dll")]
        extern static uint SendMessageTimeout(uint hWnd, uint msg, uint wParam, 
            string lParam, uint flags, uint timeout, out uint result);

        const uint WM_SETTINGCHANGE = 0x001A;
        const uint HWND_BROADCAST = 0xFFFF;
        const uint SMTO_ABORTIFHUNG = 0x0002;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FreezerForm());
		}

        private void btnFreezeEm_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                uint unusedResult;
                SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, 0, "Whatever", SMTO_ABORTIFHUNG, 5000, out unusedResult);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }
	}
}
