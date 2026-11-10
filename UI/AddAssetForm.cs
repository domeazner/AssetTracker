using AssetTracker.Data;
using AssetTracker.Models;
using AssetTracker.Services;
using Microsoft.Data.SqlClient;

namespace AssetTracker.UI;

/// <summary>
/// The Add Asset screen (presentation layer), it only gathers input and shows messages. Validation is done by AssetService and saving by the repository, so no business logic or SQL lives in this class.
///
/// Partial: the layout code (controls and positions) is in AddAssetForm.Designer.cs and this file holds the behaviour.
/// 
/// 1) Form opens > OnFormLoad auto fills hardware details & loads employees.
/// 2) User picks an employee optionally ticks the purchase date box and adds notes.
/// 3) User clicks Save > btnSave_Click builds an Asset & passes it onto to AssetService.
/// 4) If Success > confirmation message with the new Asset ID. 
///    If Failure > a friendly message and the user's entries stay on screen.
/// </summary>

public partial class AddAssetForm : Form
{

    /// Everything the form needs is passed in from Program.cs (dependency injection) instead of being created here.

    private readonly AssetService _assetService;
    private readonly HardwareInfoCollector _collector;
    private readonly SqlEmployeeRepository _employeeRepository;

    public AddAssetForm(AssetService assetService,
                        HardwareInfoCollector collector,
                        SqlEmployeeRepository employeeRepository)
    {

        /// Builds all the controls defined in the Designer file, this must be called first.

        InitializeComponent();
        _assetService = assetService;
        _collector = collector;
        _employeeRepository = employeeRepository;

        /// Subscribe to the Load event so OnFormLoad that runs when the form is first shown. Slow work (WMI and database) happens there & not in the constructor.

        Load += OnFormLoad;
    }


    /// Action 1: runs once, just before the form first appears.

    private void OnFormLoad(object? sender, EventArgs e)
    {

        /// Instructions are set in the code so the wording is easy to find & edit.

        lblInstructions.Text =
            "Hardware details are filled in automatically. Select the responsible employee, " +
            "add the purchase date from the asset's sticker (if it has one), then click Save Asset.";


        /// This asks Windows for this machines details and pre fills the text boxes. The boxes stay editable so the user can correct anything that is wrong or wants to edit.

        var hardware = _collector.Collect();
        txtSystemName.Text = hardware.SystemName;
        txtModel.Text = hardware.Model;
        txtManufacturer.Text = hardware.Manufacturer;
        txtAssetType.Text = hardware.AssetType;
        txtIpAddress.Text = hardware.IpAddress;

        try
        {

            /// Load employees from Azure SQL and bind them to the dropdown menu. Each item is an Employee object shown as "First Last" through the ToString() override within the Employee class. Issues remain around the free Azure tier this may be slow if the database has been paused or timed out.

            cboEmployee.DataSource = _employeeRepository.GetAll();
        }
        catch (SqlException)
        {

            /// If the Database is unreachable: tell the user instead of crashing the program. The dropdown stays empty so Save will be refused by the "select an employee" validation rule.

            MessageBox.Show("Could not load employees from the database. Check your connection and restart.",
                            "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


    /// Action 2: The user fills in the form. The logic needed here is that the purchase date is optional, so the date picker is greyed out until the box is ticked which makes it clear the date can be left empty. This is wired up to the checkbox in the Designer file.

    private void chkHasPurchaseDate_CheckedChanged(object? sender, EventArgs e)
        => dtpPurchaseDate.Enabled = chkHasPurchaseDate.Checked;


    /// Action 3 & 4: runs when the user clicks Save Asset (or presses Enter because the Designer sets AcceptButton = btnSave).

    private void btnSave_Click(object? sender, EventArgs e)
    {
        try
        {

            /// Build an Asset object from what is currently on screen. Trim() removes accidental leading and trailing spaces.

            var asset = new Asset
            {
                SystemName = txtSystemName.Text.Trim(),
                Model = txtModel.Text.Trim(),
                Manufacturer = txtManufacturer.Text.Trim(),
                AssetType = txtAssetType.Text.Trim(),
                IpAddress = txtIpAddress.Text.Trim(),

                /// Optional set date: only used if the box is ticked if not otherwise null. .Date drops the time part because the database column is stored in DATE.

                PurchaseDate = chkHasPurchaseDate.Checked ? dtpPurchaseDate.Value.Date : null,

                /// Optional fillable notes: blank or whitespace only becomes null which is stored as NULL in the database rather than an empty string.

                Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim(),

                /// The selected dropdown item is an Employee object. "as" returns null if nothing is selected and ?? 0 turns that into 0 which the service rejects as "no employee chosen".

                EmployeeId = (cboEmployee.SelectedItem as Employee)?.EmployeeId ?? 0
            };


            /// This hands over to the service layer it validates the asset, then saves it through the repository. If anything is not right an exception is thrown and execution jumps to the catch blocks below.

            _assetService.AddAsset(asset);


            /// If Success: confirm to the user and show the generated Asset ID, which can be matched to the relevant row in Azure.

            MessageBox.Show($"Asset saved successfully.\n\nAsset ID: {asset.AssetId}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            /// Reset only the per asset fields so the form is ready for the next one. Hardware details are left in place as they describe the machine.

            txtNotes.Clear();
            chkHasPurchaseDate.Checked = false;
        }

        /// A validation rule in AssetService failed (e.g. no employee, future date). The service's own message is shown so the user knows what to fix.

        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, "Please check your input",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// The database could not be reached or the insert failed (such as: no Wi-Fi, or the free-tier database is taking time waking up). A friendly message is shown, and nothing is cleared, so the user can retry without retyping.

        catch (SqlException)
        {
            MessageBox.Show("The asset could not be saved because the database is unavailable. " +
                            "Your entries are still on screen, so you can try again.",
                            "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}