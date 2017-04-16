namespace FlightEmulatorConnection
{
    partial class ConnectionForm
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
            this.SerialPorts = new System.Windows.Forms.ComboBox();
            this.BuadRateChoose = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.ConnectionButton = new System.Windows.Forms.Button();
            this.DisarmButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SerialPorts
            // 
            this.SerialPorts.FormattingEnabled = true;
            this.SerialPorts.Location = new System.Drawing.Point(36, 45);
            this.SerialPorts.Name = "SerialPorts";
            this.SerialPorts.Size = new System.Drawing.Size(121, 20);
            this.SerialPorts.TabIndex = 0;
            this.SerialPorts.Click += new System.EventHandler(this.SerialPorts_Click);
            // 
            // BuadRateChoose
            // 
            this.BuadRateChoose.FormattingEnabled = true;
            this.BuadRateChoose.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "57600",
            "115200"});
            this.BuadRateChoose.Location = new System.Drawing.Point(184, 45);
            this.BuadRateChoose.Name = "BuadRateChoose";
            this.BuadRateChoose.Size = new System.Drawing.Size(121, 20);
            this.BuadRateChoose.TabIndex = 1;
            this.BuadRateChoose.Text = "115200";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "串口";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "波特率";
            // 
            // ConnectionButton
            // 
            this.ConnectionButton.Location = new System.Drawing.Point(36, 84);
            this.ConnectionButton.Name = "ConnectionButton";
            this.ConnectionButton.Size = new System.Drawing.Size(75, 44);
            this.ConnectionButton.TabIndex = 4;
            this.ConnectionButton.Text = "连接";
            this.ConnectionButton.UseVisualStyleBackColor = true;
            this.ConnectionButton.Click += new System.EventHandler(this.ConnectionButton_Click);
            // 
            // DisarmButton
            // 
            this.DisarmButton.Location = new System.Drawing.Point(150, 87);
            this.DisarmButton.Name = "DisarmButton";
            this.DisarmButton.Size = new System.Drawing.Size(75, 41);
            this.DisarmButton.TabIndex = 5;
            this.DisarmButton.Text = "解除限制";
            this.DisarmButton.UseVisualStyleBackColor = true;
            this.DisarmButton.Click += new System.EventHandler(this.DisarmButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(264, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 6;
            // 
            // ConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 167);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DisarmButton);
            this.Controls.Add(this.ConnectionButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BuadRateChoose);
            this.Controls.Add(this.SerialPorts);
            this.Name = "ConnectionForm";
            this.Text = "ConnectionForm";
            this.Load += new System.EventHandler(this.ConnectionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SerialPorts;
        private System.Windows.Forms.ComboBox BuadRateChoose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button ConnectionButton;
        private System.Windows.Forms.Button DisarmButton;
        private System.Windows.Forms.Label label3;
    }
}