namespace icrypt
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
            this.button1 = new System.Windows.Forms.Button();
            this.encryptbutton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.keybox = new System.Windows.Forms.TextBox();
            this.decryptbutton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.statuslabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(423, 58);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(211, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "SELECT FOLDER";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // encryptbutton
            // 
            this.encryptbutton.Location = new System.Drawing.Point(423, 94);
            this.encryptbutton.Margin = new System.Windows.Forms.Padding(4);
            this.encryptbutton.Name = "encryptbutton";
            this.encryptbutton.Size = new System.Drawing.Size(211, 28);
            this.encryptbutton.TabIndex = 1;
            this.encryptbutton.Text = "ENCRYPT";
            this.encryptbutton.UseVisualStyleBackColor = true;
            this.encryptbutton.Click += new System.EventHandler(this.encryptbutton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AccessibleName = "";
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(69, 230);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(97, 20);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "custom key";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select folder to en/decrypt";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(69, 271);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.MaxLength = 32;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(328, 30);
            this.textBox1.TabIndex = 0;
            this.textBox1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 129);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Encryption key:";
            this.label2.Visible = false;
            // 
            // keybox
            // 
            this.keybox.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keybox.Location = new System.Drawing.Point(69, 162);
            this.keybox.Margin = new System.Windows.Forms.Padding(4);
            this.keybox.MaxLength = 32;
            this.keybox.Name = "keybox";
            this.keybox.Size = new System.Drawing.Size(328, 30);
            this.keybox.TabIndex = 5;
            this.keybox.Visible = false;
            this.keybox.TextChanged += new System.EventHandler(this.keybox_TextChanged);
            // 
            // decryptbutton
            // 
            this.decryptbutton.Location = new System.Drawing.Point(423, 129);
            this.decryptbutton.Margin = new System.Windows.Forms.Padding(4);
            this.decryptbutton.Name = "decryptbutton";
            this.decryptbutton.Size = new System.Drawing.Size(211, 28);
            this.decryptbutton.TabIndex = 6;
            this.decryptbutton.Text = "DECRYPT";
            this.decryptbutton.UseVisualStyleBackColor = true;
            this.decryptbutton.Click += new System.EventHandler(this.decryptbutton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(447, 246);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(173, 28);
            this.progressBar1.TabIndex = 7;
            this.progressBar1.Visible = false;
            // 
            // statuslabel
            // 
            this.statuslabel.AutoSize = true;
            this.statuslabel.Location = new System.Drawing.Point(494, 216);
            this.statuslabel.Name = "statuslabel";
            this.statuslabel.Size = new System.Drawing.Size(71, 16);
            this.statuslabel.TabIndex = 8;
            this.statuslabel.Text = "inprogress";
            this.statuslabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 367);
            this.Controls.Add(this.statuslabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.decryptbutton);
            this.Controls.Add(this.keybox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.encryptbutton);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button encryptbutton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox keybox;
        private System.Windows.Forms.Button decryptbutton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label statuslabel;
    }
}

