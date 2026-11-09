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
        // Placeholder: replaced with the real save logic in Step 5.
    }
}