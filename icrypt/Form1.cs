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
                        if (add_failsafe(path))
                        {
                            encrypt(path, key);
                        }
                        else
                        {
                            const string message2 = "Failed to generate failsafe, encryption aborted";
                            const string caption2 = "Encrypt Folder";
                            MessageBox.Show(message2, caption2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
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
                            if (add_failsafe(path))
                            {
                                encrypt(path, key);
                            }
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
                        if (check_failsafe(path, key))
                        {
                            decrypt(path, key);
                            pushed_decr = 0;
                        }
                        else
                        {
                            const string message2 = "Failsafe check failed, key incorrect";
                            const string caption2 = "Decrypt Folder";
                            MessageBox.Show(message2, caption2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
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
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            progressBar1.Minimum = 0;
            progressBar1.Maximum = files.Length;
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            progressBar1.Visible = true;
            statuslabel.Text = "Encrypting..";
            statuslabel.Visible = true;
           
            foreach (string file in files)
            {
                if (!file.Contains("encr"))
                {
                    Console.WriteLine(file);
                    string newfilename = Path.GetFileNameWithoutExtension(file) + "_encr" + Path.GetExtension(file);
                    string dest = Path.Combine(Path.GetDirectoryName(file), newfilename);
                    EncryptFile(file, dest, key);
                    progressBar1.PerformStep();
                    System.IO.File.Delete(file);
                }
            }
            progressBar1.Visible = false;
            statuslabel.Text = "Finished encryption!";
        }

        public void decrypt(string path, string key)
        {
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            progressBar1.Minimum = 0;
            progressBar1.Maximum = files.Length;
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            progressBar1.Visible = true;
            statuslabel.Text = "Decrypting..";
            statuslabel.Visible = true;

            foreach (string file in files)
            {
                if (file.Contains("encr"))
                {
                    Console.WriteLine(file);
                    string dest = file.Replace("_encr", "");
                    DecryptFile(file, dest, key);
                    progressBar1.PerformStep();
                    System.IO.File.Delete(file);
                }
            }
            progressBar1.Visible = false;
            statuslabel.Text = "Finished decrypting!";
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

        public void EncryptFile(string inputFile, string outputFile, string key)
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

        public void DecryptFile(string inputFile, string outputFile, string key)
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

       
        public bool add_failsafe(string path)
        {
            string filepath = path + @"\icrypt_failsafe.txt";
            try
            {
                using (StreamWriter sw = System.IO.File.CreateText(filepath))
                {
                    sw.WriteLine("failsafe");
                }
                return true;
            }
            catch { return false; }

        }
        
        public bool check_failsafe(string path, string key)
        {
            string filepath = path + @"\icrypt_failsafe_encr.txt";
            
            if (System.IO.File.Exists(filepath))
            {
                string dest = filepath.Replace("_encr", "");
                try
                {
                    DecryptFile(filepath, dest, key);
                    using (StreamReader sr = new StreamReader(dest))
                    {
                        if (sr.ReadLine() == "failsafe")
                        {
                            return true;
                        }
                        else { return false; }
                    }
                }
                catch { return false; }
            }
            else
            {
                return false;
            }
        }
        
        private void keybox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
