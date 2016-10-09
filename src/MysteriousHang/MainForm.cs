using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Threading;

namespace MysteriousHang
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Button btnClickMe;
        private System.Windows.Forms.Label txtWillHang;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            Thread.CurrentThread.Name = "GuiThread";
            DangerousStuff();
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
            this.btnClickMe = new System.Windows.Forms.Button();
            this.txtWillHang = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClickMe
            // 
            this.btnClickMe.Location = new System.Drawing.Point(86, 122);
            this.btnClickMe.Name = "btnClickMe";
            this.btnClickMe.Size = new System.Drawing.Size(120, 23);
            this.btnClickMe.TabIndex = 0;
            this.btnClickMe.Text = "Click Me";
            this.btnClickMe.Click += new System.EventHandler(this.btnClickMe_Click);
            // 
            // txtWillHang
            // 
            this.txtWillHang.Location = new System.Drawing.Point(24, 32);
            this.txtWillHang.Name = "txtWillHang";
            this.txtWillHang.Size = new System.Drawing.Size(240, 80);
            this.txtWillHang.TabIndex = 1;
            this.txtWillHang.Text = "label1";
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(292, 267);
            this.Controls.Add(this.txtWillHang);
            this.Controls.Add(this.btnClickMe);
            this.Name = "MainForm";
            this.Text = "Mysterious Hang";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

        private void btnClickMe_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(this, "Boo!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DangerousStuff()
        {
            MethodInvoker d = new MethodInvoker(WorkerThreadFunc);
            d.EndInvoke(d.BeginInvoke(null, null));
        }

        private void WorkerThreadFunc()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(WorkerThreadFunc), null);
                return;
            }

            IntPtr handle = new Form().Handle;
        }

        private bool IsVsHost()
        {
            // not 100% accurate, but good enough for demonstration purposes
            return Environment.CommandLine.IndexOf(".vshost.exe") != -1;
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            txtWillHang.Text = 
                String.Format(".NET version: {0}\r\nVS Host: {1}\r\nWill hang: {2}",
                    Environment.Version,
                    IsVsHost()? "yes": "no",
                    WillHang());
        }

        private string WillHang()
        {
            if (Environment.Version.ToString().StartsWith("1.1.4322.")) 
            {
                return "no"; // .NET 1.1
            }

            if (Environment.Version.ToString().StartsWith("2.0.50727."))
            {
                // .NET 2.0
                return IsVsHost()?"no":"yes";
            }
            
            // unknown version of .NET
            return "unknown";
        }
	}
}
