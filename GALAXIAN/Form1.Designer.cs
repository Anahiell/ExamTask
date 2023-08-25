namespace GALAXIAN
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button_start = new System.Windows.Forms.Button();
            this.button_EXIT = new System.Windows.Forms.Button();
            this.button_Sond = new System.Windows.Forms.Button();
            this.button_Theme = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_start
            // 
            resources.ApplyResources(this.button_start, "button_start");
            this.button_start.Name = "button_start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_EXIT
            // 
            resources.ApplyResources(this.button_EXIT, "button_EXIT");
            this.button_EXIT.Name = "button_EXIT";
            this.button_EXIT.UseVisualStyleBackColor = true;
            this.button_EXIT.Click += new System.EventHandler(this.button3_Click);
            // 
            // button_Sond
            // 
            this.button_Sond.BackgroundImage = global::GALAXIAN.Properties.Resources.noizOFF;
            resources.ApplyResources(this.button_Sond, "button_Sond");
            this.button_Sond.Name = "button_Sond";
            this.button_Sond.UseVisualStyleBackColor = true;
            this.button_Sond.Click += new System.EventHandler(this.button_Sond_Click);
            // 
            // button_Theme
            // 
            resources.ApplyResources(this.button_Theme, "button_Theme");
            this.button_Theme.Name = "button_Theme";
            this.button_Theme.UseVisualStyleBackColor = true;
            this.button_Theme.Click += new System.EventHandler(this.button_Theme_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.AllowDrop = true;
            resources.ApplyResources(this.trackBar1, "trackBar1");
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GALAXIAN.Properties.Resources.main_menu;
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.button_Theme);
            this.Controls.Add(this.button_Sond);
            this.Controls.Add(this.button_EXIT);
            this.Controls.Add(this.button_start);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_EXIT;
        private System.Windows.Forms.Button button_Sond;
        private System.Windows.Forms.Button button_Theme;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}

