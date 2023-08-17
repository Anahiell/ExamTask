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
            this.button_start = new System.Windows.Forms.Button();
            this.button_EXIT = new System.Windows.Forms.Button();
            this.button_Sond = new System.Windows.Forms.Button();
            this.button_Theme = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(169, 136);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(166, 60);
            this.button_start.TabIndex = 0;
            this.button_start.Text = "START GAME";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_EXIT
            // 
            this.button_EXIT.Location = new System.Drawing.Point(169, 341);
            this.button_EXIT.Name = "button_EXIT";
            this.button_EXIT.Size = new System.Drawing.Size(166, 58);
            this.button_EXIT.TabIndex = 0;
            this.button_EXIT.Text = "EXIT";
            this.button_EXIT.UseVisualStyleBackColor = true;
            this.button_EXIT.Click += new System.EventHandler(this.button3_Click);
            // 
            // button_Sond
            // 
            this.button_Sond.BackgroundImage = global::GALAXIAN.Properties.Resources.noizON;
            this.button_Sond.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_Sond.Location = new System.Drawing.Point(256, 234);
            this.button_Sond.Name = "button_Sond";
            this.button_Sond.Size = new System.Drawing.Size(79, 68);
            this.button_Sond.TabIndex = 1;
            this.button_Sond.UseVisualStyleBackColor = true;
            this.button_Sond.Click += new System.EventHandler(this.button_Sond_Click);
            // 
            // button_Theme
            // 
            this.button_Theme.Location = new System.Drawing.Point(169, 234);
            this.button_Theme.Name = "button_Theme";
            this.button_Theme.Size = new System.Drawing.Size(81, 68);
            this.button_Theme.TabIndex = 1;
            this.button_Theme.Text = "THEME";
            this.button_Theme.UseVisualStyleBackColor = true;
            this.button_Theme.Click += new System.EventHandler(this.button_Theme_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GALAXIAN.Properties.Resources.main_menu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(507, 444);
            this.Controls.Add(this.button_Theme);
            this.Controls.Add(this.button_Sond);
            this.Controls.Add(this.button_EXIT);
            this.Controls.Add(this.button_start);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_EXIT;
        private System.Windows.Forms.Button button_Sond;
        private System.Windows.Forms.Button button_Theme;
    }
}

