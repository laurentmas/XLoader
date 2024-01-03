using System.Drawing;
using System.Windows.Forms;

namespace XLoader
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.uploadStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.labelHexfile = new System.Windows.Forms.Label();
            this.textBoxHexfile = new System.Windows.Forms.TextBox();
            this.buttonHexfile = new System.Windows.Forms.Button();
            this.labelDevice = new System.Windows.Forms.Label();
            this.comboBoxDevice = new System.Windows.Forms.ComboBox();
            this.labelCOMPort = new System.Windows.Forms.Label();
            this.labelBaudrate = new System.Windows.Forms.Label();
            this.comboBoxCOMPort = new System.Windows.Forms.ComboBox();
            this.textBoxBaudrate = new System.Windows.Forms.TextBox();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.checkBoxVerify = new System.Windows.Forms.CheckBox();
            this.checkBoxViewLogs = new System.Windows.Forms.CheckBox();
            this.richTextBoxLogs = new System.Windows.Forms.RichTextBox();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadStatusLabel,
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 189);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip.Size = new System.Drawing.Size(244, 22);
            this.statusStrip.TabIndex = 0;
            // 
            // uploadStatusLabel
            // 
            this.uploadStatusLabel.Name = "uploadStatusLabel";
            this.uploadStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "\"hex files|*.hex|All Files|*.*\"";
            // 
            // labelHexfile
            // 
            this.labelHexfile.AutoSize = true;
            this.labelHexfile.Location = new System.Drawing.Point(8, 6);
            this.labelHexfile.Name = "labelHexfile";
            this.labelHexfile.Size = new System.Drawing.Size(42, 13);
            this.labelHexfile.TabIndex = 1;
            this.labelHexfile.Text = "Hex file";
            // 
            // textBoxHexfile
            // 
            this.textBoxHexfile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHexfile.Location = new System.Drawing.Point(10, 20);
            this.textBoxHexfile.Name = "textBoxHexfile";
            this.textBoxHexfile.Size = new System.Drawing.Size(197, 20);
            this.textBoxHexfile.TabIndex = 1;
            // 
            // buttonHexfile
            // 
            this.buttonHexfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHexfile.Location = new System.Drawing.Point(211, 20);
            this.buttonHexfile.Name = "buttonHexfile";
            this.buttonHexfile.Size = new System.Drawing.Size(24, 20);
            this.buttonHexfile.TabIndex = 0;
            this.buttonHexfile.Text = "...";
            this.buttonHexfile.UseVisualStyleBackColor = true;
            this.buttonHexfile.Click += new System.EventHandler(this.buttonHexfile_Click);
            // 
            // labelDevice
            // 
            this.labelDevice.AutoSize = true;
            this.labelDevice.Location = new System.Drawing.Point(9, 42);
            this.labelDevice.Name = "labelDevice";
            this.labelDevice.Size = new System.Drawing.Size(41, 13);
            this.labelDevice.TabIndex = 4;
            this.labelDevice.Text = "Device";
            // 
            // comboBoxDevice
            // 
            this.comboBoxDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDevice.FormattingEnabled = true;
            this.comboBoxDevice.Location = new System.Drawing.Point(10, 57);
            this.comboBoxDevice.Name = "comboBoxDevice";
            this.comboBoxDevice.Size = new System.Drawing.Size(225, 21);
            this.comboBoxDevice.Sorted = true;
            this.comboBoxDevice.TabIndex = 2;
            this.comboBoxDevice.SelectedIndexChanged += new System.EventHandler(this.comboBoxDevice_SelectedIndexChanged);
            // 
            // labelCOMPort
            // 
            this.labelCOMPort.AutoSize = true;
            this.labelCOMPort.Location = new System.Drawing.Point(9, 81);
            this.labelCOMPort.Name = "labelCOMPort";
            this.labelCOMPort.Size = new System.Drawing.Size(52, 13);
            this.labelCOMPort.TabIndex = 6;
            this.labelCOMPort.Text = "COM port";
            // 
            // labelBaudrate
            // 
            this.labelBaudrate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBaudrate.AutoSize = true;
            this.labelBaudrate.Location = new System.Drawing.Point(123, 81);
            this.labelBaudrate.Name = "labelBaudrate";
            this.labelBaudrate.Size = new System.Drawing.Size(53, 13);
            this.labelBaudrate.TabIndex = 7;
            this.labelBaudrate.Text = "Baud rate";
            // 
            // comboBoxCOMPort
            // 
            this.comboBoxCOMPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCOMPort.FormattingEnabled = true;
            this.comboBoxCOMPort.Location = new System.Drawing.Point(10, 96);
            this.comboBoxCOMPort.Name = "comboBoxCOMPort";
            this.comboBoxCOMPort.Size = new System.Drawing.Size(110, 21);
            this.comboBoxCOMPort.TabIndex = 3;
            this.comboBoxCOMPort.SelectedIndexChanged += new System.EventHandler(this.comboBoxCOMPort_SelectedIndexChanged);
            // 
            // textBoxBaudrate
            // 
            this.textBoxBaudrate.Location = new System.Drawing.Point(126, 96);
            this.textBoxBaudrate.Name = "textBoxBaudrate";
            this.textBoxBaudrate.Size = new System.Drawing.Size(110, 20);
            this.textBoxBaudrate.TabIndex = 4;
            // 
            // buttonUpload
            // 
            this.buttonUpload.Location = new System.Drawing.Point(9, 124);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(111, 23);
            this.buttonUpload.TabIndex = 5;
            this.buttonUpload.Text = "Upload";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Location = new System.Drawing.Point(126, 124);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(110, 23);
            this.buttonAbout.TabIndex = 6;
            this.buttonAbout.Text = "About";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // checkBoxVerify
            // 
            this.checkBoxVerify.AutoSize = true;
            this.checkBoxVerify.Checked = true;
            this.checkBoxVerify.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxVerify.Location = new System.Drawing.Point(11, 153);
            this.checkBoxVerify.Name = "checkBoxVerify";
            this.checkBoxVerify.Size = new System.Drawing.Size(90, 17);
            this.checkBoxVerify.TabIndex = 9;
            this.checkBoxVerify.Text = "Disable Verify";
            this.checkBoxVerify.UseVisualStyleBackColor = true;
            this.checkBoxVerify.CheckedChanged += new System.EventHandler(this.checkBoxVerify_CheckedChanged);
            // 
            // checkBoxViewLogs
            // 
            this.checkBoxViewLogs.AutoSize = true;
            this.checkBoxViewLogs.Location = new System.Drawing.Point(126, 153);
            this.checkBoxViewLogs.Name = "checkBoxViewLogs";
            this.checkBoxViewLogs.Size = new System.Drawing.Size(75, 17);
            this.checkBoxViewLogs.TabIndex = 10;
            this.checkBoxViewLogs.Text = "View Logs";
            this.checkBoxViewLogs.UseVisualStyleBackColor = true;
            this.checkBoxViewLogs.CheckedChanged += new System.EventHandler(this.checkBoxViewLogs_CheckedChanged);
            // 
            // richTextBoxLogs
            // 
            this.richTextBoxLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxLogs.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBoxLogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxLogs.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.richTextBoxLogs.Location = new System.Drawing.Point(10, 176);
            this.richTextBoxLogs.Name = "richTextBoxLogs";
            this.richTextBoxLogs.ReadOnly = true;
            this.richTextBoxLogs.Size = new System.Drawing.Size(225, 0);
            this.richTextBoxLogs.TabIndex = 11;
            this.richTextBoxLogs.Text = "";
            this.richTextBoxLogs.WordWrap = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 211);
            this.Controls.Add(this.richTextBoxLogs);
            this.Controls.Add(this.checkBoxViewLogs);
            this.Controls.Add(this.checkBoxVerify);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.buttonUpload);
            this.Controls.Add(this.textBoxBaudrate);
            this.Controls.Add(this.comboBoxCOMPort);
            this.Controls.Add(this.labelBaudrate);
            this.Controls.Add(this.labelCOMPort);
            this.Controls.Add(this.comboBoxDevice);
            this.Controls.Add(this.labelDevice);
            this.Controls.Add(this.buttonHexfile);
            this.Controls.Add(this.textBoxHexfile);
            this.Controls.Add(this.labelHexfile);
            this.Controls.Add(this.statusStrip);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1024, 600);
            this.MinimumSize = new System.Drawing.Size(260, 250);
            this.Name = "MainForm";
            this.Text = "XLoader v2.00";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private StatusStrip statusStrip;
        private ToolStripStatusLabel uploadStatusLabel;
        private OpenFileDialog openFileDialog;
        private Label labelHexfile;
        private TextBox textBoxHexfile;
        private Label labelDevice;
        private ComboBox comboBoxDevice;
        private Label labelCOMPort;
        private Label labelBaudrate;
        private ComboBox comboBoxCOMPort;
        private TextBox textBoxBaudrate;
        private Button buttonUpload;
        private Button buttonAbout;

        #endregion

        private ToolStripStatusLabel toolStripStatusLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private CheckBox checkBoxVerify;
        private RichTextBox richTextBoxLogs;
        private Button buttonHexfile;
        private CheckBox checkBoxViewLogs;
    }
}

