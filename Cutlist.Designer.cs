namespace WindowsFormsApplication1
{
    partial class Cutlist
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
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_menu = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbx_search = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btn_cutlist = new System.Windows.Forms.Button();
            this.lbl_listBox = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lbl_customer = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_2Item = new System.Windows.Forms.Label();
            this.lbl_task = new System.Windows.Forms.Label();
            this.lbl_itemDetails = new System.Windows.Forms.Label();
            this.lbl_1Item = new System.Windows.Forms.Label();
            this.lbl_details = new System.Windows.Forms.Label();
            this.lbl_2task = new System.Windows.Forms.Label();
            this.lbl_2itemDetails = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(804, 762);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(75, 23);
            this.btn_exit.TabIndex = 0;
            this.btn_exit.Text = "Exit";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_menu
            // 
            this.btn_menu.Location = new System.Drawing.Point(12, 762);
            this.btn_menu.Name = "btn_menu";
            this.btn_menu.Size = new System.Drawing.Size(75, 23);
            this.btn_menu.TabIndex = 1;
            this.btn_menu.Text = "Menu";
            this.btn_menu.UseVisualStyleBackColor = true;
            this.btn_menu.Click += new System.EventHandler(this.btn_menu_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(558, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Search";
            // 
            // tbx_search
            // 
            this.tbx_search.Location = new System.Drawing.Point(630, 52);
            this.tbx_search.Name = "tbx_search";
            this.tbx_search.Size = new System.Drawing.Size(191, 20);
            this.tbx_search.TabIndex = 25;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(26, 31);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(514, 82);
            this.listBox1.TabIndex = 24;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // btn_cutlist
            // 
            this.btn_cutlist.Location = new System.Drawing.Point(677, 90);
            this.btn_cutlist.Name = "btn_cutlist";
            this.btn_cutlist.Size = new System.Drawing.Size(75, 23);
            this.btn_cutlist.TabIndex = 23;
            this.btn_cutlist.Text = "Cutlist";
            this.btn_cutlist.UseVisualStyleBackColor = true;
            this.btn_cutlist.Click += new System.EventHandler(this.btn_cutlist_Click);
            // 
            // lbl_listBox
            // 
            this.lbl_listBox.AutoSize = true;
            this.lbl_listBox.Location = new System.Drawing.Point(359, 9);
            this.lbl_listBox.Name = "lbl_listBox";
            this.lbl_listBox.Size = new System.Drawing.Size(85, 13);
            this.lbl_listBox.TabIndex = 27;
            this.lbl_listBox.Text = "No File Selected";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(27, 28);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(328, 236);
            this.textBox1.TabIndex = 28;
            // 
            // lbl_customer
            // 
            this.lbl_customer.AutoSize = true;
            this.lbl_customer.Location = new System.Drawing.Point(382, 31);
            this.lbl_customer.Name = "lbl_customer";
            this.lbl_customer.Size = new System.Drawing.Size(66, 13);
            this.lbl_customer.TabIndex = 29;
            this.lbl_customer.Text = "lbl_customer";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.lbl_2task);
            this.groupBox1.Controls.Add(this.lbl_2itemDetails);
            this.groupBox1.Controls.Add(this.lbl_2Item);
            this.groupBox1.Controls.Add(this.lbl_task);
            this.groupBox1.Controls.Add(this.lbl_itemDetails);
            this.groupBox1.Controls.Add(this.lbl_1Item);
            this.groupBox1.Controls.Add(this.lbl_details);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.lbl_customer);
            this.groupBox1.Location = new System.Drawing.Point(26, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(705, 505);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // lbl_2Item
            // 
            this.lbl_2Item.AutoSize = true;
            this.lbl_2Item.Location = new System.Drawing.Point(382, 237);
            this.lbl_2Item.Name = "lbl_2Item";
            this.lbl_2Item.Size = new System.Drawing.Size(49, 13);
            this.lbl_2Item.TabIndex = 34;
            this.lbl_2Item.Text = "lbl_2Item";
            // 
            // lbl_task
            // 
            this.lbl_task.AutoSize = true;
            this.lbl_task.Location = new System.Drawing.Point(383, 187);
            this.lbl_task.Name = "lbl_task";
            this.lbl_task.Size = new System.Drawing.Size(43, 13);
            this.lbl_task.TabIndex = 33;
            this.lbl_task.Text = "lbl_task";
            // 
            // lbl_itemDetails
            // 
            this.lbl_itemDetails.AutoSize = true;
            this.lbl_itemDetails.Location = new System.Drawing.Point(383, 140);
            this.lbl_itemDetails.Name = "lbl_itemDetails";
            this.lbl_itemDetails.Size = new System.Drawing.Size(74, 13);
            this.lbl_itemDetails.TabIndex = 32;
            this.lbl_itemDetails.Text = "lbl_itemDetails";
            // 
            // lbl_1Item
            // 
            this.lbl_1Item.AutoSize = true;
            this.lbl_1Item.Location = new System.Drawing.Point(382, 99);
            this.lbl_1Item.Name = "lbl_1Item";
            this.lbl_1Item.Size = new System.Drawing.Size(49, 13);
            this.lbl_1Item.TabIndex = 31;
            this.lbl_1Item.Text = "lbl_1Item";
            // 
            // lbl_details
            // 
            this.lbl_details.AutoSize = true;
            this.lbl_details.Location = new System.Drawing.Point(383, 64);
            this.lbl_details.Name = "lbl_details";
            this.lbl_details.Size = new System.Drawing.Size(53, 13);
            this.lbl_details.TabIndex = 30;
            this.lbl_details.Text = "lbl_details";
            // 
            // lbl_2task
            // 
            this.lbl_2task.AutoSize = true;
            this.lbl_2task.Location = new System.Drawing.Point(382, 318);
            this.lbl_2task.Name = "lbl_2task";
            this.lbl_2task.Size = new System.Drawing.Size(49, 13);
            this.lbl_2task.TabIndex = 36;
            this.lbl_2task.Text = "lbl_2task";
            // 
            // lbl_2itemDetails
            // 
            this.lbl_2itemDetails.AutoSize = true;
            this.lbl_2itemDetails.Location = new System.Drawing.Point(382, 271);
            this.lbl_2itemDetails.Name = "lbl_2itemDetails";
            this.lbl_2itemDetails.Size = new System.Drawing.Size(80, 13);
            this.lbl_2itemDetails.TabIndex = 35;
            this.lbl_2itemDetails.Text = "lbl_2itemDetails";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(27, 293);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(328, 236);
            this.textBox2.TabIndex = 37;
            // 
            // Cutlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 797);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_listBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbx_search);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btn_cutlist);
            this.Controls.Add(this.btn_menu);
            this.Controls.Add(this.btn_exit);
            this.Name = "Cutlist";
            this.Text = "Cutlist";
            this.Load += new System.EventHandler(this.Cutlist_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_menu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbx_search;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btn_cutlist;
        private System.Windows.Forms.Label lbl_listBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lbl_customer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_task;
        private System.Windows.Forms.Label lbl_itemDetails;
        private System.Windows.Forms.Label lbl_1Item;
        private System.Windows.Forms.Label lbl_details;
        private System.Windows.Forms.Label lbl_2Item;
        private System.Windows.Forms.Label lbl_2task;
        private System.Windows.Forms.Label lbl_2itemDetails;
        private System.Windows.Forms.TextBox textBox2;
    }
}