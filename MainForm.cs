using System;
using System.IO;
using System.IO.Ports;
using System.Management;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections;
using System.Threading;
using System.Diagnostics;
using System.Globalization;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace XLoader
{
    public partial class MainForm : Form
    {        
        private Process UploadProcess;
        private ManualResetEvent exitWait = new ManualResetEvent(false);
        private string[] devices = null;
        private string[] ports = null;
        private int comportsTimeout = 5;
        private string selectDefaultPort = "";
        private string hexFileLocation = "";
        private string comport = "";
        private string baudrate = "";
        private string programmer = "";
        private string partno = "";
        private string uploadType = "";
        private string options = "";
        private string disableVerify = " -V";
        private string currentOperation = "";


        #region Local Helpers
        private void UpdateCOMPortList()
        {
            comboBoxCOMPort.Items.Clear();
            try
            {
                // Get all existing Com Port names
                ports = System.IO.Ports.SerialPort.GetPortNames();
                ports = ports.Distinct().ToArray();
                Array.Sort(ports);

                // Append existing COM to the cboxComport list
                foreach (var item in ports)
                {
                    comboBoxCOMPort.Items.Add(item);
                    selectDefaultPort = item;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                toolStripStatusLabel.Text = "Error getting COM Port";
            }
        }

        private void ReadDevices_File()
        {
            comboBoxDevice.Items.Clear();
            try
            {
                string textFile = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "devices.txt");

                if (File.Exists(textFile))
                {
                    // Read a text file line by line.
                    devices = File.ReadAllLines(textFile);
                    foreach (string device in devices)
                    {
                        comboBoxDevice.Items.Add(new { Text = device.Split(';')[0], Value = device });
                    }
                }
                else
                {
                    toolStripStatusLabel.Text = "Missing devices.txt file";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                toolStripStatusLabel.Text = "Error reading devices.txt file";
            }
        }

        private void GetXLoader_registryInfos()
        {
            string comport = "";
            int device = -1;
            string filename = "";
            bool viewlogs = false;
            bool verify = false;

            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Xloader");

                if (key != null)
                {
                    comport = key.GetValue("comport").ToString();
                    device = int.Parse(key.GetValue("device").ToString());
                    filename = key.GetValue("filename").ToString();
                    viewlogs = bool.Parse(key.GetValue("viewlogs").ToString());
                    verify = bool.Parse(key.GetValue("verify").ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (comport != "")
                comboBoxCOMPort.Text = comport;
            else
                comboBoxCOMPort.Text = selectDefaultPort;

            if (filename != "")
                textBoxHexfile.Text = filename;

            if (comboBoxDevice.Items.Count > 0)
            {
                if (device != -1)
                {
                    try
                    {
                        comboBoxDevice.SelectedIndex = device;
                    }
                    catch
                    {
                        comboBoxDevice.SelectedIndex = 0;
                    }
                }
                else
                {
                    comboBoxDevice.SelectedIndex = 0;
                }
            }

            checkBoxViewLogs.Checked = viewlogs;
            checkBoxVerify.Checked = verify;
        }

        private void SaveXLoader_registryInfos()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Xloader");
            key.SetValue("comport", comboBoxCOMPort.Text);
            key.SetValue("device", comboBoxDevice.SelectedIndex.ToString());
            key.SetValue("filename", textBoxHexfile.Text);
            key.SetValue("viewlogs", checkBoxViewLogs.Checked);
            key.SetValue("verify", checkBoxVerify.Checked);
            key.Close();
        }

        #endregion

        #region Delegates
        public delegate void UPDATE_STRIP_STATUS(String Str);
        public void UpdateStripStatus(String Str)
        {
            toolStripStatusLabel.Text = Str;           
        }
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            comboBoxDevice.DisplayMember = "Text";
            comboBoxDevice.ValueMember = "Value";
            toolStripStatusLabel.Text = "";

            UpdateCOMPortList();            
            ReadDevices_File();
            GetXLoader_registryInfos();
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            string message = "XLoader v2.00\r\nLaurent Mas\r\n\r\nOriginal version developed by \r\nGeir Lunde\r\n";
            string title = "About";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Asterisk);            
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            if (buttonUpload.Text == "Upload")
            {
                richTextBoxLogs.Clear();
                if (comboBoxCOMPort.SelectedItem != null)
                {
                    hexFileLocation = @textBoxHexfile.Text;
                    if (hexFileLocation != String.Empty)
                    {
                        if (textBoxBaudrate.Text != String.Empty)
                        {
                            baudrate = textBoxBaudrate.Text;

                            // Start BackgroundWorker
                            backgroundWorker.RunWorkerAsync();
                            currentOperation = "";
                            buttonUpload.Text = "Cancel";

                            comboBoxDevice.Enabled = false;
                            textBoxHexfile.Enabled = false;
                            buttonHexfile.Enabled = false;
                            comboBoxCOMPort.Enabled = false;
                            textBoxBaudrate.Enabled = false;
                            buttonAbout.Enabled = false;
                            checkBoxVerify.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Cannot start the upload\r\nMissing Baud Rate info.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot start the upload\r\nMissing Hex File.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Cannot start the upload\r\nMissing COM Port.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {                
                // Cancel BackgroundWorker
                if (backgroundWorker.IsBusy)
                    backgroundWorker.CancelAsync();
                toolStripStatusLabel.Text = "Canceling...";
                currentOperation = "";
                buttonUpload.Enabled = false;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            kill();//kill avrdude process if it was running
            SaveXLoader_registryInfos();
        }

        private void buttonHexfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "hex",
                Filter = "hex files|*.hex|All Files|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxHexfile.Text = openFileDialog.FileName;
            }
        }

        private void comboBoxDevice_SelectedIndexChanged(object sender, EventArgs e)
        {            
            try
            {
                string[] deviceInfos = ((comboBoxDevice.SelectedItem as dynamic).Value).Split(';');
                partno = deviceInfos[1];
                programmer = deviceInfos[2];                
                baudrate = deviceInfos[3];
                uploadType = deviceInfos[4];
                if (deviceInfos.Length > 5)
                {
                    if (deviceInfos[5] != "")
                    {
                        options = " " + deviceInfos[5];
                    }
                    else
                        options = "";
                }
            }
            catch
            {
                baudrate = "";
                partno = "";
                programmer = "";
                uploadType = "default";
                options = "";
            }
            textBoxBaudrate.Text = baudrate;
        }

        protected bool isActive()
        {
            return (UploadProcess != null && !UploadProcess.HasExited);
        }

        public bool kill()
        {
            if (!isActive())
                return false;
            UploadProcess.Kill();
            return true;
        }

        private void p_Exited(object sender, EventArgs e)
        {
            exitWait.Set();
            //Thread.Sleep(300);
            if (backgroundWorker.IsBusy)
                backgroundWorker.Dispose();
        }

        private void appendRichTextBoxText(string text)
        {
            if (null != text)
            {
                Invoke(new ToDoDelegate(() => this.richTextBoxLogs.AppendText(text)));
                if (text.Contains("\n")) // Without this the text box spazzes a bit on the progress bars
                    Invoke(new ToDoDelegate(() => this.richTextBoxLogs.ScrollToCaret()));
            }
        }

        private void appendRichTextBoxText(string text, Color color)
        {
            if (null != text)
            {
                Invoke(new ToDoDelegate(() => this.richTextBoxLogs.SelectionStart = this.richTextBoxLogs.TextLength));
                Invoke(new ToDoDelegate(() => this.richTextBoxLogs.SelectionLength = 0));
                Invoke(new ToDoDelegate(() => this.richTextBoxLogs.SelectionColor = color));
                Invoke(new ToDoDelegate(() => this.richTextBoxLogs.AppendText(text)));
                Invoke(new ToDoDelegate(() => this.richTextBoxLogs.SelectionColor = this.richTextBoxLogs.ForeColor));
                if (text.Contains("\n")) // Without this the text box spazzes a bit on the progress bars
                    Invoke(new ToDoDelegate(() => this.richTextBoxLogs.ScrollToCaret()));
            }
        }

        private bool Enable_Bootloader(string comPort)
        {
            appendRichTextBoxText("Forcing reset using 1200bps open/ close on port "+ comPort + Environment.NewLine);
            SerialPort Serial = new SerialPort();
            Serial.Parity = Parity.None;
            Serial.DataBits = 8;
            Serial.StopBits = StopBits.One;
            Serial.PortName = comPort;
            Serial.BaudRate = 1200;;

            try
            {
                Serial.Open();
                Thread.Sleep(100);
                Serial.Close();
                Thread.Sleep(100);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string Detect_newComPort(string[] exisitingPorts, BackgroundWorker bw, DoWorkEventArgs e)
        {
            bool bootloader = Enable_Bootloader(comport);

            if (bootloader)
            {
                List<string> compPorts = new List<string>();
                string[] updatedPorts = null;
                var timeout = DateTimeOffset.UtcNow.Add(TimeSpan.FromSeconds(comportsTimeout));

                while ((compPorts.Count() == 0) && (DateTimeOffset.UtcNow < timeout) && bw.CancellationPending == false)
                {
                    Thread.Sleep(250);
                    updatedPorts = System.IO.Ports.SerialPort.GetPortNames();
                    appendRichTextBoxText("PORTS : { " + string.Join(", ", updatedPorts.Distinct().ToArray()) + " } => {}" + Environment.NewLine);
                    compPorts = updatedPorts.Except(exisitingPorts).ToList();
                }
                if (compPorts.Any())
                {
                    appendRichTextBoxText("PORTS : { " + string.Join(", ", updatedPorts.Distinct().ToArray()) + " } => { " + compPorts[0] + " }" + Environment.NewLine);
                    appendRichTextBoxText("Found upload port: " + compPorts[0] + Environment.NewLine + Environment.NewLine);

                    return compPorts[0];
                }
                return String.Empty;
            }
            else
            {
                return String.Empty;
            }
        }

        private string upload_arduinoHexFile(BackgroundWorker bw, DoWorkEventArgs e)
        {
            toolStripStatusLabel.Text = "Uploading...";
            string consoleLog = string.Empty;
            string uploadComPort = comport;
            string uploadExecutable = "avrdude.exe";
            int uploadProgress = 0;
            string _baudrate = string.Empty;

            if (File.Exists(uploadExecutable))
            {
                //check if upload using bootloader or default way
                if (uploadType.ToLower() == "bootloader")
                {
                    uploadComPort = Detect_newComPort(ports, bw, e);
                    //if upload port is not find, rollback to the previous port
                    if (uploadComPort == String.Empty)
                    {
                        uploadComPort = comport;
                        appendRichTextBoxText("Using upload port: " + uploadComPort + Environment.NewLine + Environment.NewLine);
                    }
                }

                UploadProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = uploadExecutable,
                        Arguments = " -c " + programmer + " -p " + partno + " -P " + uploadComPort + " -b " + baudrate + disableVerify + options + " -D -U flash:w:\"" + hexFileLocation + "\":i",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                    },
                    EnableRaisingEvents = true,

                };
                UploadProcess.Exited += new EventHandler(p_Exited);

                UploadProcess.Start();

                appendRichTextBoxText(">>>: " + UploadProcess.StartInfo.FileName + UploadProcess.StartInfo.Arguments + Environment.NewLine, Color.Blue);

                do
                {
                    Thread.Sleep(15);

                    try
                    {
                        if (UploadProcess != null)
                        {
                            char[] buff = new char[256];

                            // TODO: read from stdError AND stdOut (AVRDUDE outputs stuff through stdError)
                            if (UploadProcess.StandardError.Read(buff, 0, buff.Length) > 0)
                            {
                                string output = (new string(buff)).Replace("\0", string.Empty);
                                appendRichTextBoxText(output);
                                consoleLog += output;//cleanup console Log
                            }

                            //check if avrdude is verifing uploaded code
                            var verify_groups = Regex.Match(consoleLog, @"Reading \| (#*)").Groups;
                            if (verify_groups.Count > 1)
                            {
                                currentOperation = "Verifying... ";
                                uploadProgress = (verify_groups[1].Value.Split('#').Length - 1) * 2;
                                bw.ReportProgress(uploadProgress);
                            }
                            else
                            {
                                //check if avrdude is uploading code
                                var writing_groups = Regex.Match(consoleLog, @"Writing \| (#*)").Groups;
                                if (writing_groups.Count > 1)
                                {
                                    currentOperation = "Uploading... ";
                                    uploadProgress = (writing_groups[1].Value.Split('#').Length - 1) * 2;
                                    bw.ReportProgress(uploadProgress);
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return "Upload failed";
                    }
                } while (!UploadProcess.StandardError.EndOfStream & bw.CancellationPending == false);
                //if I manually close the cmd.exe window by clicking X
                //in the top right corner the program runs correctly
                //at these lines of code right here
                if (bw.CancellationPending == true)
                {
                    kill();//kill avrdude process
                    e.Cancel = true;
                    string output = UploadProcess.StandardError.ReadLine();
                    appendRichTextBoxText(output);
                    return "Canceled";
                }
                if (consoleLog.Contains("error") || consoleLog.Contains("ERROR"))
                {
                    return "Upload failed";
                }
                else
                {
                    var groups = Regex.Match(consoleLog, @"avrdude(.exe)?: ([0-9].*) bytes of flash written").Groups;
                    if (groups.Count > 0)
                    {
                        return groups[2].Value + " bytes uploaded";
                    }
                    else
                        return "Upload succeeded";

                }
            }
            else
                return "Upload failed. "+ uploadExecutable + " is missing..." ;

        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker helperBW = sender as BackgroundWorker;
            e.Result = upload_arduinoHexFile(helperBW, e);
            if (helperBW.CancellationPending)
            {
                kill();
                e.Cancel = true;
            }
        }

        private delegate void ToDoDelegate();
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.toolStripStatusLabel.Text = currentOperation + e.ProgressPercentage + "%";
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                toolStripStatusLabel.Text = "Operation was canceled";
                buttonUpload.Text = "Upload";
                currentOperation = "";
                buttonUpload.Enabled = true;
                comboBoxDevice.Enabled = true;
                textBoxHexfile.Enabled = true;
                buttonHexfile.Enabled = true;
                comboBoxCOMPort.Enabled = true;
                textBoxBaudrate.Enabled = true;
                buttonAbout.Enabled = true;
                checkBoxVerify.Enabled = true;
            }
            else if (e.Error != null) MessageBox.Show(e.Error.Message);
            else
            {

                toolStripStatusLabel.Text = e.Result.ToString();
                buttonUpload.Text = "Upload";
                currentOperation = "";
                buttonUpload.Enabled = true;
                comboBoxDevice.Enabled = true;
                textBoxHexfile.Enabled = true;
                buttonHexfile.Enabled = true;
                comboBoxCOMPort.Enabled = true;
                textBoxBaudrate.Enabled = true;
                buttonAbout.Enabled = true;
                checkBoxVerify.Enabled = true;
            }
        }

        private void checkBoxViewLogs_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxViewLogs.Checked)
            {
                richTextBoxLogs.Visible = true;
                this.MinimumSize = new Size(this.MinimumSize.Width, 450);
                this.MaximumSize = new Size(this.MaximumSize.Width, 800);
                this.Size = new Size(800, 450);
            }
            else
            {
                richTextBoxLogs.Visible = false;
                this.MinimumSize = new Size(this.MinimumSize.Width, 250);
                this.MaximumSize = new Size(this.MaximumSize.Width, 250);
                this.Size = new Size(260, 250);
            }
            richTextBoxLogs.SelectAll();
        }

        private void comboBoxCOMPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            comport = comboBoxCOMPort.SelectedItem.ToString();
        }

        private void checkBoxVerify_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxVerify.Checked) 
            { 
                disableVerify = " -V";
            }
            else
            { 
                disableVerify = ""; 
            }
        }
    }
 }
