using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace icrypt
{
    public partial class Form1 : Form
    {
        public string path=null;
        public bool customkey = false;
        public int pushed_decr = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            customkey = checkBox1.Checked;
            textBox1.Visible = customkey;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog1.SelectedPath;
                label1.Text = path;
            }
        }

        private void encryptbutton_Click(object sender, EventArgs e)
        {

            if (path != null)
            {
                const string message = "Are you sure you want to encrypt this folder?";
                const string caption = "Encrypt Folder";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    string key;
                    if (!customkey)
                    {
                        key = generateKey(32);
                        keybox.Text = key;
                        keybox.Visible = true;
                        keybox.ReadOnly = true;
                        label2.Visible = true;
                        encrypt(path, key);
                    }
                    else
                    {
                        key = textBox1.Text;
                        if (key.Length != 32)
                        {
                            const string message2 = "KEY wrong lenth (32 char needed)";
                            const string caption2 = "Encrypt Folder";
                            MessageBox.Show(message2, caption2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            encrypt(path, key);
                        }
                    }
                }
            }
            else
            {
                const string message = "NO FOLDER SELECTED";
                const string caption = "Encrypt Folder";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void decryptbutton_Click(object sender, EventArgs e)
        {
            if(pushed_decr == 0)
            {
                pushed_decr++;
                keybox.Visible = true;
                keybox.ReadOnly = false;
                label2.Visible = true;
                
            }else if (pushed_decr == 1)
            {
                string key = keybox.Text;
                if (key.Length != 32)
                {
                    const string message = "KEY wrong lenth (32 char needed)";
                    const string caption = "Decrypt Folder";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (path != null)
                {
                    const string message = "Are you sure you want to decrypt this folder?";
                    const string caption = "Decrypt Folder";
                    var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        decrypt(path, key);
                        pushed_decr = 0;
                    }

                }
                else
                {
                    const string message = "NO FOLDER SELECTED";
                    const string caption = "Decrypt Folder";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        public void encrypt(string path, string key)
        {
            string[] files = Directory.GetFiles(path);
            progressBar1.Minimum = 0;
            progressBar1.Maximum = files.Length;
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            progressBar1.Visible = true;
            foreach (string file in files)
            {
                Console.WriteLine(file);
                string ext = file.Split('.').Last();
                string dest = file.Split('.')[0] + "encr." + ext;
                EncryptFile(file, dest, key);
                progressBar1.PerformStep();
                //File.Delete(file);
            }
            progressBar1.Visible = false;
        }

        public void decrypt(string path, string key)
        {
            string[] files = Directory.GetFiles(path);
            progressBar1.Minimum = 0;
            progressBar1.Maximum = files.Length;
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            progressBar1.Visible = true;
            foreach (string file in files)
            {
                string ext = file.Split('.').Last();
                string dest = file.Split('.')[0] + "dcr." + ext;

                DecryptFile(file, dest, key);
                progressBar1.PerformStep();
                //File.Delete(file);
            }
            progressBar1.Visible = false;
        }

        public static string generateKey(int n)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            for (int i = 0; i < n; i++)
            {
                ch = chars[random.Next(chars.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public static void EncryptFile(string inputFile, string outputFile, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = new byte[16];

                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                {
                    using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                    {
                        using (CryptoStream cs = new CryptoStream(fsOutput, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            byte[] buffer = new byte[4096];
                            int bytesRead;

                            while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                cs.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
            }
        }

        public static void DecryptFile(string inputFile, string outputFile, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = new byte[16];

                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                {
                    using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                    {
                        using (CryptoStream cs = new CryptoStream(fsInput, aes.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            byte[] buffer = new byte[4096];
                            int bytesRead;

                            while ((bytesRead = cs.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fsOutput.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
            }
        }

        private void keybox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
