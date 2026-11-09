namespace AssetTracker.UI
{
    partial class AddAssetForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblInstructions = new System.Windows.Forms.Label();
            lblSystemName = new System.Windows.Forms.Label();
            txtSystemName = new System.Windows.Forms.TextBox();
            lblModel = new System.Windows.Forms.Label();
            txtModel = new System.Windows.Forms.TextBox();
            lblManufacturer = new System.Windows.Forms.Label();
            txtManufacturer = new System.Windows.Forms.TextBox();
            lblAssetType = new System.Windows.Forms.Label();
            txtAssetType = new System.Windows.Forms.TextBox();
            lblIpAddress = new System.Windows.Forms.Label();
            txtIpAddress = new System.Windows.Forms.TextBox();
            lblEmployee = new System.Windows.Forms.Label();
            cboEmployee = new System.Windows.Forms.ComboBox();
            chkHasPurchaseDate = new System.Windows.Forms.CheckBox();
            dtpPurchaseDate = new System.Windows.Forms.DateTimePicker();
            lblNotes = new System.Windows.Forms.Label();
            txtNotes = new System.Windows.Forms.TextBox();
            btnSave = new System.Windows.Forms.Button();
            SuspendLayout();
            //
            // lblInstructions
            //
            lblInstructions.BackColor = System.Drawing.Color.LightYellow;
            lblInstructions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lblInstructions.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblInstructions.Location = new System.Drawing.Point(20, 15);
            lblInstructions.Name = "lblInstructions";
            lblInstructions.Padding = new System.Windows.Forms.Padding(6);
            lblInstructions.Size = new System.Drawing.Size(480, 70);
            lblInstructions.TabIndex = 0;
            lblInstructions.Text = "Instructions";
            //
            // lblSystemName
            //
            lblSystemName.AutoSize = true;
            lblSystemName.Location = new System.Drawing.Point(20, 105);
            lblSystemName.Name = "lblSystemName";
            lblSystemName.Text = "System name";
            //
            // txtSystemName
            //
            txtSystemName.Location = new System.Drawing.Point(190, 102);
            txtSystemName.Name = "txtSystemName";
            txtSystemName.Size = new System.Drawing.Size(310, 23);
            txtSystemName.TabIndex = 1;
            //
            // lblModel
            //
            lblModel.AutoSize = true;
            lblModel.Location = new System.Drawing.Point(20, 143);
            lblModel.Name = "lblModel";
            lblModel.Text = "Model";
            //
            // txtModel
            //
            txtModel.Location = new System.Drawing.Point(190, 140);
            txtModel.Name = "txtModel";
            txtModel.Size = new System.Drawing.Size(310, 23);
            txtModel.TabIndex = 2;
            //
            // lblManufacturer
            //
            lblManufacturer.AutoSize = true;
            lblManufacturer.Location = new System.Drawing.Point(20, 181);
            lblManufacturer.Name = "lblManufacturer";
            lblManufacturer.Text = "Manufacturer";
            //
            // txtManufacturer
            //
            txtManufacturer.Location = new System.Drawing.Point(190, 178);
            txtManufacturer.Name = "txtManufacturer";
            txtManufacturer.Size = new System.Drawing.Size(310, 23);
            txtManufacturer.TabIndex = 3;
            //
            // lblAssetType
            //
            lblAssetType.AutoSize = true;
            lblAssetType.Location = new System.Drawing.Point(20, 219);
            lblAssetType.Name = "lblAssetType";
            lblAssetType.Text = "Asset type";
            //
            // txtAssetType
            //
            txtAssetType.Location = new System.Drawing.Point(190, 216);
            txtAssetType.Name = "txtAssetType";
            txtAssetType.Size = new System.Drawing.Size(310, 23);
            txtAssetType.TabIndex = 4;
            //
            // lblIpAddress
            //
            lblIpAddress.AutoSize = true;
            lblIpAddress.Location = new System.Drawing.Point(20, 257);
            lblIpAddress.Name = "lblIpAddress";
            lblIpAddress.Text = "IP address";
            //
            // txtIpAddress
            //
            txtIpAddress.Location = new System.Drawing.Point(190, 254);
            txtIpAddress.Name = "txtIpAddress";
            txtIpAddress.Size = new System.Drawing.Size(310, 23);
            txtIpAddress.TabIndex = 5;
            //
            // lblEmployee
            //
            lblEmployee.AutoSize = true;
            lblEmployee.Location = new System.Drawing.Point(20, 295);
            lblEmployee.Name = "lblEmployee";
            lblEmployee.Text = "Responsible employee";
            //
            // cboEmployee
            //
            cboEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboEmployee.Location = new System.Drawing.Point(190, 292);
            cboEmployee.Name = "cboEmployee";
            cboEmployee.Size = new System.Drawing.Size(310, 23);
            cboEmployee.TabIndex = 6;
            //
            // chkHasPurchaseDate
            //
            chkHasPurchaseDate.AutoSize = true;
            chkHasPurchaseDate.Location = new System.Drawing.Point(20, 333);
            chkHasPurchaseDate.Name = "chkHasPurchaseDate";
            chkHasPurchaseDate.TabIndex = 7;
            chkHasPurchaseDate.Text = "Asset has a purchase date sticker";
            chkHasPurchaseDate.UseVisualStyleBackColor = true;
            chkHasPurchaseDate.CheckedChanged += new System.EventHandler(chkHasPurchaseDate_CheckedChanged);
            //
            // dtpPurchaseDate
            //
            dtpPurchaseDate.Enabled = false;
            dtpPurchaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpPurchaseDate.Location = new System.Drawing.Point(190, 362);
            dtpPurchaseDate.Name = "dtpPurchaseDate";
            dtpPurchaseDate.Size = new System.Drawing.Size(150, 23);
            dtpPurchaseDate.TabIndex = 8;
            //
            // lblNotes
            //
            lblNotes.AutoSize = true;
            lblNotes.Location = new System.Drawing.Point(20, 405);
            lblNotes.Name = "lblNotes";
            lblNotes.Text = "Notes (optional)";
            //
            // txtNotes
            //
            txtNotes.Location = new System.Drawing.Point(190, 402);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtNotes.Size = new System.Drawing.Size(310, 100);
            txtNotes.TabIndex = 9;
            //
            // btnSave
            //
            btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnSave.Location = new System.Drawing.Point(190, 525);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(150, 40);
            btnSave.TabIndex = 10;
            btnSave.Text = "Save Asset";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += new System.EventHandler(btnSave_Click);
            //
            // AddAssetForm
            //
            AcceptButton = btnSave;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(520, 590);
            Controls.Add(lblInstructions);
            Controls.Add(lblSystemName);
            Controls.Add(txtSystemName);
            Controls.Add(lblModel);
            Controls.Add(txtModel);
            Controls.Add(lblManufacturer);
            Controls.Add(txtManufacturer);
            Controls.Add(lblAssetType);
            Controls.Add(txtAssetType);
            Controls.Add(lblIpAddress);
            Controls.Add(txtIpAddress);
            Controls.Add(lblEmployee);
            Controls.Add(cboEmployee);
            Controls.Add(chkHasPurchaseDate);
            Controls.Add(dtpPurchaseDate);
            Controls.Add(lblNotes);
            Controls.Add(txtNotes);
            Controls.Add(btnSave);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "AddAssetForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Add Asset";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Label lblSystemName;
        private System.Windows.Forms.TextBox txtSystemName;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label lblManufacturer;
        private System.Windows.Forms.TextBox txtManufacturer;
        private System.Windows.Forms.Label lblAssetType;
        private System.Windows.Forms.TextBox txtAssetType;
        private System.Windows.Forms.Label lblIpAddress;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Label lblEmployee;
        private System.Windows.Forms.ComboBox cboEmployee;
        private System.Windows.Forms.CheckBox chkHasPurchaseDate;
        private System.Windows.Forms.DateTimePicker dtpPurchaseDate;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnSave;
    }
}