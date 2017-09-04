namespace Planar
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.drawButton = new System.Windows.Forms.Button();
            this.spanButton = new System.Windows.Forms.Button();
            this.transitButton = new System.Windows.Forms.Button();
            this.componentButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.drawButton);
            this.panel1.Controls.Add(this.spanButton);
            this.panel1.Controls.Add(this.transitButton);
            this.panel1.Controls.Add(this.componentButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 485);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(964, 69);
            this.panel1.TabIndex = 0;
            // 
            // drawButton
            // 
            this.drawButton.Location = new System.Drawing.Point(21, 12);
            this.drawButton.Name = "drawButton";
            this.drawButton.Size = new System.Drawing.Size(101, 45);
            this.drawButton.TabIndex = 3;
            this.drawButton.Text = "DRAW";
            this.drawButton.UseVisualStyleBackColor = true;
            this.drawButton.Click += new System.EventHandler(this.drawButton_Click);
            // 
            // spanButton
            // 
            this.spanButton.Location = new System.Drawing.Point(547, 22);
            this.spanButton.Name = "spanButton";
            this.spanButton.Size = new System.Drawing.Size(98, 24);
            this.spanButton.TabIndex = 2;
            this.spanButton.Text = "Spanning Tree";
            this.spanButton.UseVisualStyleBackColor = true;
            this.spanButton.Click += new System.EventHandler(this.spanButton_Click);
            // 
            // transitButton
            // 
            this.transitButton.Location = new System.Drawing.Point(676, 22);
            this.transitButton.Name = "transitButton";
            this.transitButton.Size = new System.Drawing.Size(108, 24);
            this.transitButton.TabIndex = 1;
            this.transitButton.Text = "Transitive Closure";
            this.transitButton.UseVisualStyleBackColor = true;
            this.transitButton.Click += new System.EventHandler(this.transitButton_Click);
            // 
            // componentButton
            // 
            this.componentButton.Location = new System.Drawing.Point(815, 22);
            this.componentButton.Name = "componentButton";
            this.componentButton.Size = new System.Drawing.Size(87, 24);
            this.componentButton.TabIndex = 0;
            this.componentButton.Text = "Components";
            this.componentButton.UseVisualStyleBackColor = true;
            this.componentButton.Click += new System.EventHandler(this.componentButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 554);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button spanButton;
        private System.Windows.Forms.Button transitButton;
        private System.Windows.Forms.Button componentButton;
        private System.Windows.Forms.Button drawButton;
    }
}

