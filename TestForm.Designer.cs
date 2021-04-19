namespace Symantec.ICDM
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /*
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        */
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ClientId = new System.Windows.Forms.TextBox();
            this.ClientSecret = new System.Windows.Forms.TextBox();
            this.APIHost = new System.Windows.Forms.TextBox();
            this.url = new System.Windows.Forms.TextBox();
            this.json = new System.Windows.Forms.TextBox();
            this.method = new System.Windows.Forms.TextBox();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.Result = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.PostData = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.CustomerId = new System.Windows.Forms.TextBox();
            this.DomainId = new System.Windows.Forms.TextBox();
            this.accessToken = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.TestSelection = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupId = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.deviceId = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.threatIntel = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ClientId
            // 
            this.ClientId.Location = new System.Drawing.Point(173, 111);
            this.ClientId.Margin = new System.Windows.Forms.Padding(4);
            this.ClientId.Name = "ClientId";
            this.ClientId.Size = new System.Drawing.Size(569, 22);
            this.ClientId.TabIndex = 0;
            // 
            // ClientSecret
            // 
            this.ClientSecret.Location = new System.Drawing.Point(173, 141);
            this.ClientSecret.Margin = new System.Windows.Forms.Padding(4);
            this.ClientSecret.Name = "ClientSecret";
            this.ClientSecret.Size = new System.Drawing.Size(569, 22);
            this.ClientSecret.TabIndex = 1;
            this.ClientSecret.TextChanged += new System.EventHandler(this.apiKey_TextChanged);
            // 
            // APIHost
            // 
            this.APIHost.Location = new System.Drawing.Point(173, 176);
            this.APIHost.Margin = new System.Windows.Forms.Padding(4);
            this.APIHost.Name = "APIHost";
            this.APIHost.Size = new System.Drawing.Size(569, 22);
            this.APIHost.TabIndex = 2;
            // 
            // url
            // 
            this.url.Location = new System.Drawing.Point(173, 329);
            this.url.Margin = new System.Windows.Forms.Padding(4);
            this.url.Name = "url";
            this.url.Size = new System.Drawing.Size(569, 22);
            this.url.TabIndex = 3;
            // 
            // json
            // 
            this.json.Location = new System.Drawing.Point(173, 462);
            this.json.Margin = new System.Windows.Forms.Padding(4);
            this.json.Multiline = true;
            this.json.Name = "json";
            this.json.Size = new System.Drawing.Size(569, 111);
            this.json.TabIndex = 4;
            this.json.TextChanged += new System.EventHandler(this.json_TextChanged);
            // 
            // method
            // 
            this.method.Location = new System.Drawing.Point(173, 295);
            this.method.Margin = new System.Windows.Forms.Padding(4);
            this.method.Name = "method";
            this.method.Size = new System.Drawing.Size(95, 22);
            this.method.TabIndex = 5;
            this.method.TextChanged += new System.EventHandler(this.method_TextChanged);
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(705, 798);
            this.SubmitButton.Margin = new System.Windows.Forms.Padding(4);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(100, 28);
            this.SubmitButton.TabIndex = 6;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // Result
            // 
            this.Result.Location = new System.Drawing.Point(768, 79);
            this.Result.Margin = new System.Windows.Forms.Padding(4);
            this.Result.Multiline = true;
            this.Result.Name = "Result";
            this.Result.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Result.Size = new System.Drawing.Size(569, 686);
            this.Result.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 111);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Client ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 141);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Client Secret:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.CausesValidation = false;
            this.label3.Location = new System.Drawing.Point(61, 176);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "API Host:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(69, 295);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Method:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(88, 329);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "URL:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(79, 462);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 17);
            this.label6.TabIndex = 18;
            this.label6.Text = "JSON:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(768, 49);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 17);
            this.label7.TabIndex = 20;
            this.label7.Text = "Result:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(88, 581);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 17);
            this.label8.TabIndex = 21;
            this.label8.Text = "Post:";
            // 
            // PostData
            // 
            this.PostData.Location = new System.Drawing.Point(173, 581);
            this.PostData.Margin = new System.Windows.Forms.Padding(4);
            this.PostData.Multiline = true;
            this.PostData.Name = "PostData";
            this.PostData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PostData.Size = new System.Drawing.Size(569, 88);
            this.PostData.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(38, 206);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 17);
            this.label9.TabIndex = 23;
            this.label9.Text = "Customer ID:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(50, 236);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 17);
            this.label10.TabIndex = 24;
            this.label10.Text = "Domain ID:";
            // 
            // CustomerId
            // 
            this.CustomerId.Location = new System.Drawing.Point(173, 206);
            this.CustomerId.Margin = new System.Windows.Forms.Padding(4);
            this.CustomerId.Name = "CustomerId";
            this.CustomerId.Size = new System.Drawing.Size(569, 22);
            this.CustomerId.TabIndex = 25;
            // 
            // DomainId
            // 
            this.DomainId.Location = new System.Drawing.Point(173, 236);
            this.DomainId.Margin = new System.Windows.Forms.Padding(4);
            this.DomainId.Name = "DomainId";
            this.DomainId.Size = new System.Drawing.Size(569, 22);
            this.DomainId.TabIndex = 26;
            // 
            // accessToken
            // 
            this.accessToken.Location = new System.Drawing.Point(173, 677);
            this.accessToken.Margin = new System.Windows.Forms.Padding(4);
            this.accessToken.Multiline = true;
            this.accessToken.Name = "accessToken";
            this.accessToken.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.accessToken.Size = new System.Drawing.Size(569, 88);
            this.accessToken.TabIndex = 27;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(27, 677);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 17);
            this.label11.TabIndex = 28;
            this.label11.Text = "Access Token:";
            // 
            // TestSelection
            // 
            this.TestSelection.FormattingEnabled = true;
            this.TestSelection.Location = new System.Drawing.Point(173, 76);
            this.TestSelection.Name = "TestSelection";
            this.TestSelection.Size = new System.Drawing.Size(292, 24);
            this.TestSelection.TabIndex = 29;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(77, 76);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 17);
            this.label12.TabIndex = 30;
            this.label12.Text = "TEST:";
            // 
            // groupId
            // 
            this.groupId.Location = new System.Drawing.Point(173, 361);
            this.groupId.Margin = new System.Windows.Forms.Padding(4);
            this.groupId.Name = "groupId";
            this.groupId.Size = new System.Drawing.Size(569, 22);
            this.groupId.TabIndex = 31;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(54, 361);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 17);
            this.label13.TabIndex = 32;
            this.label13.Text = "Group ID:";
            // 
            // deviceId
            // 
            this.deviceId.Location = new System.Drawing.Point(173, 391);
            this.deviceId.Margin = new System.Windows.Forms.Padding(4);
            this.deviceId.Name = "deviceId";
            this.deviceId.Size = new System.Drawing.Size(569, 22);
            this.deviceId.TabIndex = 33;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(51, 391);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 17);
            this.label14.TabIndex = 34;
            this.label14.Text = "Device ID:";
            // 
            // threatIntel
            // 
            this.threatIntel.Location = new System.Drawing.Point(173, 421);
            this.threatIntel.Margin = new System.Windows.Forms.Padding(4);
            this.threatIntel.Name = "threatIntel";
            this.threatIntel.Size = new System.Drawing.Size(569, 22);
            this.threatIntel.TabIndex = 35;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 424);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(118, 17);
            this.label15.TabIndex = 36;
            this.label15.Text = "Threat Intel Data:";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1475, 859);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.threatIntel);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.deviceId);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.groupId);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.TestSelection);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.accessToken);
            this.Controls.Add(this.DomainId);
            this.Controls.Add(this.CustomerId);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.PostData);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Result);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.method);
            this.Controls.Add(this.json);
            this.Controls.Add(this.url);
            this.Controls.Add(this.APIHost);
            this.Controls.Add(this.ClientSecret);
            this.Controls.Add(this.ClientId);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TestForm";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ClientId;
        private System.Windows.Forms.TextBox ClientSecret;
        private System.Windows.Forms.TextBox APIHost;
        private System.Windows.Forms.TextBox url;
        private System.Windows.Forms.TextBox json;
        private System.Windows.Forms.TextBox method;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.TextBox Result;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox PostData;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox CustomerId;
        private System.Windows.Forms.TextBox DomainId;
        private System.Windows.Forms.TextBox accessToken;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox TestSelection;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox groupId;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox deviceId;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox threatIntel;
        private System.Windows.Forms.Label label15;
    }
}