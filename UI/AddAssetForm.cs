using AssetTracker.Data;
using AssetTracker.Models;
using AssetTracker.Services;
using Microsoft.Data.SqlClient;

namespace AssetTracker.UI;

public partial class AddAssetForm : Form
{
    private readonly AssetService _assetService;
    private readonly HardwareInfoCollector _collector;
    private readonly SqlEmployeeRepository _employeeRepository;

    public AddAssetForm(AssetService assetService,
                        HardwareInfoCollector collector,
                        SqlEmployeeRepository employeeRepository)
    {
        InitializeComponent();
        _assetService = assetService;
        _collector = collector;
        _employeeRepository = employeeRepository;
        Load += OnFormLoad;
    }

    private void OnFormLoad(object? sender, EventArgs e)
    {
        lblInstructions.Text =
            "Hardware details are filled in automatically. Select the responsible employee, " +
            "add the purchase date from the asset's sticker (if it has one), then click Save Asset.";

        var hardware = _collector.Collect();
        txtSystemName.Text = hardware.SystemName;
        txtModel.Text = hardware.Model;
        txtManufacturer.Text = hardware.Manufacturer;
        txtAssetType.Text = hardware.AssetType;
        txtIpAddress.Text = hardware.IpAddress;

        try
        {
            cboEmployee.DataSource = _employeeRepository.GetAll();
        }
        catch (SqlException)
        {
            MessageBox.Show("Could not load employees from the database. Check your connection and restart.",
                            "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void chkHasPurchaseDate_CheckedChanged(object? sender, EventArgs e)
        => dtpPurchaseDate.Enabled = chkHasPurchaseDate.Checked;

    private void btnSave_Click(object? sender, EventArgs e)
    {
        try
        {
            var asset = new Asset
            {
                SystemName = txtSystemName.Text.Trim(),
                Model = txtModel.Text.Trim(),
                Manufacturer = txtManufacturer.Text.Trim(),
                AssetType = txtAssetType.Text.Trim(),
                IpAddress = txtIpAddress.Text.Trim(),
                PurchaseDate = chkHasPurchaseDate.Checked ? dtpPurchaseDate.Value.Date : null,
                Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim(),
                EmployeeId = (cboEmployee.SelectedItem as Employee)?.EmployeeId ?? 0
            };

            _assetService.AddAsset(asset);

            MessageBox.Show($"Asset saved successfully.\n\nAsset ID: {asset.AssetId}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtNotes.Clear();
            chkHasPurchaseDate.Checked = false;
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, "Please check your input",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (SqlException)
        {
            MessageBox.Show("The asset could not be saved because the database is unavailable. " +
                            "Your entries are still on screen, so you can try again.",
                            "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}