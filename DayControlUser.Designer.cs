
namespace XufiScheduler
{
    partial class DayControlUser
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dayNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dayNumber
            // 
            this.dayNumber.AutoSize = true;
            this.dayNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dayNumber.Dock = System.Windows.Forms.DockStyle.Left;
            this.dayNumber.Location = new System.Drawing.Point(0, 0);
            this.dayNumber.Name = "dayNumber";
            this.dayNumber.Size = new System.Drawing.Size(15, 15);
            this.dayNumber.TabIndex = 0;
            this.dayNumber.Text = "2";
            this.dayNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // DayControlUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.dayNumber);
            this.Name = "DayControlUser";
            this.Size = new System.Drawing.Size(150, 81);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dayNumber;
    }
}
