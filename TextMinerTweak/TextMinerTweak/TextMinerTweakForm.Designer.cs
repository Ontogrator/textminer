namespace TextMinerTweak
{
    partial class TextMinerTweakForm
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
            this.pane01Button = new System.Windows.Forms.Button();
            this.pane01FileBox = new System.Windows.Forms.TextBox();
            this.dbConnBox = new System.Windows.Forms.TextBox();
            this.dbConnGroup = new System.Windows.Forms.GroupBox();
            this.ontologyGroup = new System.Windows.Forms.GroupBox();
            this.customButton = new System.Windows.Forms.Button();
            this.pane04Button = new System.Windows.Forms.Button();
            this.pane04FileBox = new System.Windows.Forms.TextBox();
            this.pane03Button = new System.Windows.Forms.Button();
            this.pane03FileBox = new System.Windows.Forms.TextBox();
            this.pane02Button = new System.Windows.Forms.Button();
            this.pane02FileBox = new System.Windows.Forms.TextBox();
            this.SSHGrp = new System.Windows.Forms.GroupBox();
            this.localPortBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.remotePortBox = new System.Windows.Forms.TextBox();
            this.forHostBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.userIDBox = new System.Windows.Forms.TextBox();
            this.userIDLabel = new System.Windows.Forms.Label();
            this.hostBox = new System.Windows.Forms.TextBox();
            this.hostLabel = new System.Windows.Forms.Label();
            this.sshCheck = new System.Windows.Forms.CheckBox();
            this.dbConnGroup.SuspendLayout();
            this.ontologyGroup.SuspendLayout();
            this.SSHGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // pane01Button
            // 
            this.pane01Button.Location = new System.Drawing.Point(124, 51);
            this.pane01Button.Name = "pane01Button";
            this.pane01Button.Size = new System.Drawing.Size(151, 30);
            this.pane01Button.TabIndex = 1;
            this.pane01Button.Text = "Import Pane 01 Ontology";
            this.pane01Button.UseVisualStyleBackColor = true;
            this.pane01Button.Click += new System.EventHandler(this.pane01Button_Click);
            // 
            // pane01FileBox
            // 
            this.pane01FileBox.Location = new System.Drawing.Point(14, 25);
            this.pane01FileBox.Name = "pane01FileBox";
            this.pane01FileBox.Size = new System.Drawing.Size(380, 20);
            this.pane01FileBox.TabIndex = 0;
            // 
            // dbConnBox
            // 
            this.dbConnBox.Location = new System.Drawing.Point(15, 22);
            this.dbConnBox.Name = "dbConnBox";
            this.dbConnBox.Size = new System.Drawing.Size(380, 20);
            this.dbConnBox.TabIndex = 0;
            // 
            // dbConnGroup
            // 
            this.dbConnGroup.Controls.Add(this.dbConnBox);
            this.dbConnGroup.Location = new System.Drawing.Point(12, 12);
            this.dbConnGroup.Name = "dbConnGroup";
            this.dbConnGroup.Size = new System.Drawing.Size(411, 57);
            this.dbConnGroup.TabIndex = 0;
            this.dbConnGroup.TabStop = false;
            this.dbConnGroup.Text = "MySql Database Connection";
            // 
            // ontologyGroup
            // 
            this.ontologyGroup.Controls.Add(this.customButton);
            this.ontologyGroup.Controls.Add(this.pane04Button);
            this.ontologyGroup.Controls.Add(this.pane04FileBox);
            this.ontologyGroup.Controls.Add(this.pane03Button);
            this.ontologyGroup.Controls.Add(this.pane03FileBox);
            this.ontologyGroup.Controls.Add(this.pane02Button);
            this.ontologyGroup.Controls.Add(this.pane02FileBox);
            this.ontologyGroup.Controls.Add(this.pane01Button);
            this.ontologyGroup.Controls.Add(this.pane01FileBox);
            this.ontologyGroup.Location = new System.Drawing.Point(12, 213);
            this.ontologyGroup.Name = "ontologyGroup";
            this.ontologyGroup.Size = new System.Drawing.Size(410, 304);
            this.ontologyGroup.TabIndex = 2;
            this.ontologyGroup.TabStop = false;
            this.ontologyGroup.Text = "Ontologies";
            // 
            // customButton
            // 
            this.customButton.Location = new System.Drawing.Point(310, 256);
            this.customButton.Name = "customButton";
            this.customButton.Size = new System.Drawing.Size(84, 30);
            this.customButton.TabIndex = 8;
            this.customButton.Text = "Custom";
            this.customButton.UseVisualStyleBackColor = true;
            this.customButton.Click += new System.EventHandler(this.customButton_Click);
            // 
            // pane04Button
            // 
            this.pane04Button.Location = new System.Drawing.Point(125, 256);
            this.pane04Button.Name = "pane04Button";
            this.pane04Button.Size = new System.Drawing.Size(151, 30);
            this.pane04Button.TabIndex = 7;
            this.pane04Button.Text = "Import Pane 04 Ontology";
            this.pane04Button.UseVisualStyleBackColor = true;
            this.pane04Button.Click += new System.EventHandler(this.pane04Button_Click);
            // 
            // pane04FileBox
            // 
            this.pane04FileBox.Location = new System.Drawing.Point(15, 230);
            this.pane04FileBox.Name = "pane04FileBox";
            this.pane04FileBox.Size = new System.Drawing.Size(380, 20);
            this.pane04FileBox.TabIndex = 6;
            // 
            // pane03Button
            // 
            this.pane03Button.Location = new System.Drawing.Point(125, 187);
            this.pane03Button.Name = "pane03Button";
            this.pane03Button.Size = new System.Drawing.Size(151, 30);
            this.pane03Button.TabIndex = 5;
            this.pane03Button.Text = "Import Pane 03 Ontology";
            this.pane03Button.UseVisualStyleBackColor = true;
            this.pane03Button.Click += new System.EventHandler(this.pane03Button_Click);
            // 
            // pane03FileBox
            // 
            this.pane03FileBox.Location = new System.Drawing.Point(15, 161);
            this.pane03FileBox.Name = "pane03FileBox";
            this.pane03FileBox.Size = new System.Drawing.Size(380, 20);
            this.pane03FileBox.TabIndex = 4;
            // 
            // pane02Button
            // 
            this.pane02Button.Location = new System.Drawing.Point(125, 119);
            this.pane02Button.Name = "pane02Button";
            this.pane02Button.Size = new System.Drawing.Size(151, 30);
            this.pane02Button.TabIndex = 3;
            this.pane02Button.Text = "Import Pane 02 Ontology";
            this.pane02Button.UseVisualStyleBackColor = true;
            this.pane02Button.Click += new System.EventHandler(this.pane02Button_Click);
            // 
            // pane02FileBox
            // 
            this.pane02FileBox.Location = new System.Drawing.Point(15, 93);
            this.pane02FileBox.Name = "pane02FileBox";
            this.pane02FileBox.Size = new System.Drawing.Size(380, 20);
            this.pane02FileBox.TabIndex = 2;
            // 
            // SSHGrp
            // 
            this.SSHGrp.Controls.Add(this.localPortBox);
            this.SSHGrp.Controls.Add(this.label6);
            this.SSHGrp.Controls.Add(this.remotePortBox);
            this.SSHGrp.Controls.Add(this.forHostBox);
            this.SSHGrp.Controls.Add(this.label8);
            this.SSHGrp.Controls.Add(this.label5);
            this.SSHGrp.Controls.Add(this.portBox);
            this.SSHGrp.Controls.Add(this.label3);
            this.SSHGrp.Controls.Add(this.passwordBox);
            this.SSHGrp.Controls.Add(this.label2);
            this.SSHGrp.Controls.Add(this.userIDBox);
            this.SSHGrp.Controls.Add(this.userIDLabel);
            this.SSHGrp.Controls.Add(this.hostBox);
            this.SSHGrp.Controls.Add(this.hostLabel);
            this.SSHGrp.Controls.Add(this.sshCheck);
            this.SSHGrp.Location = new System.Drawing.Point(12, 76);
            this.SSHGrp.Name = "SSHGrp";
            this.SSHGrp.Size = new System.Drawing.Size(411, 131);
            this.SSHGrp.TabIndex = 1;
            this.SSHGrp.TabStop = false;
            this.SSHGrp.Text = "SSH";
            // 
            // localPortBox
            // 
            this.localPortBox.Location = new System.Drawing.Point(281, 46);
            this.localPortBox.Name = "localPortBox";
            this.localPortBox.Size = new System.Drawing.Size(100, 20);
            this.localPortBox.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(222, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Local Port";
            // 
            // remotePortBox
            // 
            this.remotePortBox.Location = new System.Drawing.Point(281, 98);
            this.remotePortBox.Name = "remotePortBox";
            this.remotePortBox.Size = new System.Drawing.Size(100, 20);
            this.remotePortBox.TabIndex = 14;
            // 
            // forHostBox
            // 
            this.forHostBox.Location = new System.Drawing.Point(281, 72);
            this.forHostBox.Name = "forHostBox";
            this.forHostBox.Size = new System.Drawing.Size(100, 20);
            this.forHostBox.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(209, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Remote Port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(193, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Forwarding Host";
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(281, 20);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(100, 20);
            this.portBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(246, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Port";
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(77, 98);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.Size = new System.Drawing.Size(100, 20);
            this.passwordBox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            // 
            // userIDBox
            // 
            this.userIDBox.Location = new System.Drawing.Point(77, 72);
            this.userIDBox.Name = "userIDBox";
            this.userIDBox.Size = new System.Drawing.Size(100, 20);
            this.userIDBox.TabIndex = 4;
            // 
            // userIDLabel
            // 
            this.userIDLabel.AutoSize = true;
            this.userIDLabel.Location = new System.Drawing.Point(28, 75);
            this.userIDLabel.Name = "userIDLabel";
            this.userIDLabel.Size = new System.Drawing.Size(43, 13);
            this.userIDLabel.TabIndex = 3;
            this.userIDLabel.Text = "User ID";
            // 
            // hostBox
            // 
            this.hostBox.Location = new System.Drawing.Point(77, 46);
            this.hostBox.Name = "hostBox";
            this.hostBox.Size = new System.Drawing.Size(100, 20);
            this.hostBox.TabIndex = 2;
            // 
            // hostLabel
            // 
            this.hostLabel.AutoSize = true;
            this.hostLabel.Location = new System.Drawing.Point(42, 49);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(29, 13);
            this.hostLabel.TabIndex = 1;
            this.hostLabel.Text = "Host";
            // 
            // sshCheck
            // 
            this.sshCheck.AutoSize = true;
            this.sshCheck.Location = new System.Drawing.Point(77, 19);
            this.sshCheck.Name = "sshCheck";
            this.sshCheck.Size = new System.Drawing.Size(65, 17);
            this.sshCheck.TabIndex = 0;
            this.sshCheck.Text = "Enabled";
            this.sshCheck.UseVisualStyleBackColor = true;
            // 
            // TextMinerTweakForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 529);
            this.Controls.Add(this.SSHGrp);
            this.Controls.Add(this.ontologyGroup);
            this.Controls.Add(this.dbConnGroup);
            this.Name = "TextMinerTweakForm";
            this.Text = "Ontology Parser";
            this.dbConnGroup.ResumeLayout(false);
            this.dbConnGroup.PerformLayout();
            this.ontologyGroup.ResumeLayout(false);
            this.ontologyGroup.PerformLayout();
            this.SSHGrp.ResumeLayout(false);
            this.SSHGrp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pane01Button;
        private System.Windows.Forms.TextBox pane01FileBox;
        private System.Windows.Forms.TextBox dbConnBox;
        private System.Windows.Forms.GroupBox dbConnGroup;
        private System.Windows.Forms.GroupBox ontologyGroup;
        private System.Windows.Forms.Button pane04Button;
        private System.Windows.Forms.TextBox pane04FileBox;
        private System.Windows.Forms.Button pane03Button;
        private System.Windows.Forms.TextBox pane03FileBox;
        private System.Windows.Forms.Button pane02Button;
        private System.Windows.Forms.TextBox pane02FileBox;
        private System.Windows.Forms.Button customButton;
        private System.Windows.Forms.GroupBox SSHGrp;
        private System.Windows.Forms.TextBox localPortBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox remotePortBox;
        private System.Windows.Forms.TextBox forHostBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox userIDBox;
        private System.Windows.Forms.Label userIDLabel;
        private System.Windows.Forms.TextBox hostBox;
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.CheckBox sshCheck;
    }
}

