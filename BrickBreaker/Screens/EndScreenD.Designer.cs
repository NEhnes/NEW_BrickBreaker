namespace BrickBreaker.Screens
{
    partial class EndScreenD
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
            this.gameOverLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.replayButton = new System.Windows.Forms.Button();
            this.endButton = new System.Windows.Forms.Button();
            this.pictureBoxL = new System.Windows.Forms.PictureBox();
            this.pictureBoxR = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxR)).BeginInit();
            this.SuspendLayout();
            // 
            // gameOverLabel
            // 
            this.gameOverLabel.AutoSize = true;
            this.gameOverLabel.Font = new System.Drawing.Font("DynaPuff Medium", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameOverLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(127)))), ((int)(((byte)(113)))));
            this.gameOverLabel.Location = new System.Drawing.Point(344, 35);
            this.gameOverLabel.Name = "gameOverLabel";
            this.gameOverLabel.Size = new System.Drawing.Size(379, 76);
            this.gameOverLabel.TabIndex = 0;
            this.gameOverLabel.Text = "Game Over!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("DynaPuff Medium", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(127)))), ((int)(((byte)(113)))));
            this.label1.Location = new System.Drawing.Point(417, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 88);
            this.label1.TabIndex = 3;
            this.label1.Text = "Good Job! \r\nYou Socred:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Font = new System.Drawing.Font("DynaPuff Medium", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(127)))), ((int)(((byte)(113)))));
            this.scoreLabel.Location = new System.Drawing.Point(417, 253);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(223, 76);
            this.scoreLabel.TabIndex = 4;
            this.scoreLabel.Text = "SCORE";
            this.scoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // replayButton
            // 
            this.replayButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(135)))), ((int)(((byte)(153)))));
            this.replayButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.replayButton.Font = new System.Drawing.Font("DynaPuff", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replayButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.replayButton.Location = new System.Drawing.Point(372, 472);
            this.replayButton.Name = "replayButton";
            this.replayButton.Size = new System.Drawing.Size(111, 79);
            this.replayButton.TabIndex = 5;
            this.replayButton.Text = "Replay";
            this.replayButton.UseVisualStyleBackColor = false;
            this.replayButton.Click += new System.EventHandler(this.replayButton_Click);
            // 
            // endButton
            // 
            this.endButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(135)))), ((int)(((byte)(153)))));
            this.endButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.endButton.Font = new System.Drawing.Font("DynaPuff", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.endButton.Location = new System.Drawing.Point(557, 472);
            this.endButton.Name = "endButton";
            this.endButton.Size = new System.Drawing.Size(111, 79);
            this.endButton.TabIndex = 6;
            this.endButton.Text = "Exit";
            this.endButton.UseVisualStyleBackColor = false;
            this.endButton.Click += new System.EventHandler(this.endButton_Click);
            // 
            // pictureBoxL
            // 
            this.pictureBoxL.Image = global::BrickBreaker.Properties.Resources.seaweed_removebg_preview;
            this.pictureBoxL.Location = new System.Drawing.Point(0, 229);
            this.pictureBoxL.Name = "pictureBoxL";
            this.pictureBoxL.Size = new System.Drawing.Size(252, 493);
            this.pictureBoxL.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxL.TabIndex = 2;
            this.pictureBoxL.TabStop = false;
            // 
            // pictureBoxR
            // 
            this.pictureBoxR.Image = global::BrickBreaker.Properties.Resources.seaweed_removebg_preview;
            this.pictureBoxR.Location = new System.Drawing.Point(815, 229);
            this.pictureBoxR.Name = "pictureBoxR";
            this.pictureBoxR.Size = new System.Drawing.Size(252, 493);
            this.pictureBoxR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxR.TabIndex = 1;
            this.pictureBoxR.TabStop = false;
            // 
            // EndScreenD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(7)))), ((int)(((byte)(112)))));
            this.Controls.Add(this.endButton);
            this.Controls.Add(this.replayButton);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxL);
            this.Controls.Add(this.pictureBoxR);
            this.Controls.Add(this.gameOverLabel);
            this.Name = "EndScreenD";
            this.Size = new System.Drawing.Size(1067, 667);
            this.Load += new System.EventHandler(this.EndScreenL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label gameOverLabel;
        private System.Windows.Forms.PictureBox pictureBoxR;
        private System.Windows.Forms.PictureBox pictureBoxL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Button replayButton;
        private System.Windows.Forms.Button endButton;
    }
}
