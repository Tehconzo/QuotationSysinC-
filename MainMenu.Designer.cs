namespace WindowsFormsApplication1
{
    partial class MainMenu
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
            this.btn_quote = new System.Windows.Forms.Button();
            this.btn_cutlist = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_despatch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_quote
            // 
            this.btn_quote.Location = new System.Drawing.Point(236, 56);
            this.btn_quote.Name = "btn_quote";
            this.btn_quote.Size = new System.Drawing.Size(302, 72);
            this.btn_quote.TabIndex = 0;
            this.btn_quote.Text = "Quote";
            this.btn_quote.UseVisualStyleBackColor = true;
            this.btn_quote.Click += new System.EventHandler(this.btn_quote_Click);
            // 
            // btn_cutlist
            // 
            this.btn_cutlist.Location = new System.Drawing.Point(236, 238);
            this.btn_cutlist.Name = "btn_cutlist";
            this.btn_cutlist.Size = new System.Drawing.Size(302, 72);
            this.btn_cutlist.TabIndex = 1;
            this.btn_cutlist.Text = "Cutlist";
            this.btn_cutlist.UseVisualStyleBackColor = true;
            this.btn_cutlist.Click += new System.EventHandler(this.btn_cutlist_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_exit.Location = new System.Drawing.Point(804, 604);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(75, 23);
            this.btn_exit.TabIndex = 2;
            this.btn_exit.Text = "Exit";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_despatch
            // 
            this.btn_despatch.Location = new System.Drawing.Point(236, 400);
            this.btn_despatch.Name = "btn_despatch";
            this.btn_despatch.Size = new System.Drawing.Size(302, 72);
            this.btn_despatch.TabIndex = 3;
            this.btn_despatch.Text = "Despatch";
            this.btn_despatch.UseVisualStyleBackColor = true;
            this.btn_despatch.Click += new System.EventHandler(this.btn_despatch_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 639);
            this.Controls.Add(this.btn_despatch);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_cutlist);
            this.Controls.Add(this.btn_quote);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_quote;
        private System.Windows.Forms.Button btn_cutlist;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_despatch;
    }
}