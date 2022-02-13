namespace PacMan
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
            this.components = new System.ComponentModel.Container();
            this.casovac = new System.Windows.Forms.Timer(this.components);
            this.skore_label = new System.Windows.Forms.Label();
            this.button_ukonci = new System.Windows.Forms.Button();
            this.vitezna_zprava = new System.Windows.Forms.Label();
            this.GameOverPic = new System.Windows.Forms.PictureBox();
            this.PacmanGif = new System.Windows.Forms.PictureBox();
            this.zacni_hru = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GameOverPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PacmanGif)).BeginInit();
            this.SuspendLayout();
            // 
            // casovac
            // 
            this.casovac.Interval = 25;
            this.casovac.Tick += new System.EventHandler(this.casovac_tik);
            // 
            // skore_label
            // 
            this.skore_label.ForeColor = System.Drawing.Color.Yellow;
            this.skore_label.Location = new System.Drawing.Point(10, 16);
            this.skore_label.Name = "skore_label";
            this.skore_label.Size = new System.Drawing.Size(123, 17);
            this.skore_label.TabIndex = 1;
            this.skore_label.Text = "Skore";
            this.skore_label.Visible = false;
            // 
            // button_ukonci
            // 
            this.button_ukonci.BackColor = System.Drawing.Color.Yellow;
            this.button_ukonci.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_ukonci.Location = new System.Drawing.Point(418, 354);
            this.button_ukonci.Name = "button_ukonci";
            this.button_ukonci.Size = new System.Drawing.Size(373, 111);
            this.button_ukonci.TabIndex = 4;
            this.button_ukonci.Text = "Ukončit";
            this.button_ukonci.UseVisualStyleBackColor = false;
            this.button_ukonci.Click += new System.EventHandler(this.ZavriOkno);
            // 
            // vitezna_zprava
            // 
            this.vitezna_zprava.BackColor = System.Drawing.Color.Black;
            this.vitezna_zprava.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.vitezna_zprava.ForeColor = System.Drawing.Color.White;
            this.vitezna_zprava.Location = new System.Drawing.Point(433, 44);
            this.vitezna_zprava.Name = "vitezna_zprava";
            this.vitezna_zprava.Size = new System.Drawing.Size(334, 282);
            this.vitezna_zprava.TabIndex = 5;
            this.vitezna_zprava.Text = "label1";
            this.vitezna_zprava.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.vitezna_zprava.Visible = false;
            // 
            // GameOverPic
            // 
            this.GameOverPic.BackgroundImage = global::PacMan.Properties.Resources.gameover;
            this.GameOverPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.GameOverPic.Location = new System.Drawing.Point(30, 36);
            this.GameOverPic.Name = "GameOverPic";
            this.GameOverPic.Size = new System.Drawing.Size(397, 241);
            this.GameOverPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GameOverPic.TabIndex = 3;
            this.GameOverPic.TabStop = false;
            this.GameOverPic.Visible = false;
            // 
            // PacmanGif
            // 
            this.PacmanGif.BackColor = System.Drawing.Color.Transparent;
            this.PacmanGif.BackgroundImage = global::PacMan.Properties.Resources.pacmangif;
            this.PacmanGif.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PacmanGif.Location = new System.Drawing.Point(49, 65);
            this.PacmanGif.Name = "PacmanGif";
            this.PacmanGif.Size = new System.Drawing.Size(536, 191);
            this.PacmanGif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PacmanGif.TabIndex = 2;
            this.PacmanGif.TabStop = false;
            // 
            // zacni_hru
            // 
            this.zacni_hru.AutoSize = true;
            this.zacni_hru.BackColor = System.Drawing.Color.BurlyWood;
            this.zacni_hru.BackgroundImage = global::PacMan.Properties.Resources.play;
            this.zacni_hru.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.zacni_hru.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.zacni_hru.ForeColor = System.Drawing.Color.Brown;
            this.zacni_hru.Location = new System.Drawing.Point(80, 294);
            this.zacni_hru.Margin = new System.Windows.Forms.Padding(4);
            this.zacni_hru.Name = "zacni_hru";
            this.zacni_hru.Size = new System.Drawing.Size(233, 214);
            this.zacni_hru.TabIndex = 0;
            this.zacni_hru.UseVisualStyleBackColor = false;
            this.zacni_hru.Click += new System.EventHandler(this.zacniHru);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(887, 543);
            this.Controls.Add(this.vitezna_zprava);
            this.Controls.Add(this.button_ukonci);
            this.Controls.Add(this.GameOverPic);
            this.Controls.Add(this.PacmanGif);
            this.Controls.Add(this.skore_label);
            this.Controls.Add(this.zacni_hru);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "PacMan";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GameOverPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PacmanGif)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button zacni_hru;
        private System.Windows.Forms.Timer casovac;
        private System.Windows.Forms.Label skore_label;
        private System.Windows.Forms.PictureBox PacmanGif;
        private System.Windows.Forms.PictureBox GameOverPic;
        private System.Windows.Forms.Button button_ukonci;
        private System.Windows.Forms.Label vitezna_zprava;
    }
}

