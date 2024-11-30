using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// This is Template ListView + UserForm
namespace EasyITSystemCenter.Pages {

    public partial class BusinessIncomingInvoiceListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static ExtendedIncomingInvoiceList selectedRecord = new ExtendedIncomingInvoiceList();

        private SystemDocumentAdviceList DocumentAdviceList = new SystemDocumentAdviceList();
        private List<BasicCurrencyList> CurrencyList = new List<BasicCurrencyList>();
        private List<BusinessMaturityList> MaturityList = new List<BusinessMaturityList>();
        private List<BusinessPaymentMethodList> PaymentMethodList = new List<BusinessPaymentMethodList>();
        private List<BusinessPaymentStatusList> PaymentStatusList = new List<BusinessPaymentStatusList>();
        private List<BusinessNotesList> NotesList = new List<BusinessNotesList>();

        private string Supplier = null; private string Customer = null;
        private List<BusinessAddressList> AddressList = new List<BusinessAddressList>();
        private string LastSupplierCorrectSearch, LastPartNumberCorrectSearch = ""; private bool messageShown = false;

        private List<DocumentItemList> DocumentItemList = new List<DocumentItemList>();
        private List<BasicItemList> ItemList = new List<BasicItemList>();
        private List<BasicVatList> VatList = new List<BasicVatList>();
        private List<BasicUnitList> UnitList = new List<BasicUnitList>();
        private string itemVatPriceFormat = "N2"; private string documentVatPriceFormat = "N0";

        public BusinessIncomingInvoiceListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            //translate fields in detail form
            try {
                try {
                    _ = FormOperations.TranslateFormFields(ListForm);
                    _ = FormOperations.TranslateFormFields(SubListView);
                } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

                LoadParameters();
                Task<bool> start = LoadDataList();
                SetRecord(false);
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private async void LoadParameters() {
            itemVatPriceFormat = await DataOperations.ParameterCheck("ItemVatPriceFormat");
            documentVatPriceFormat = await DataOperations.ParameterCheck("DocumentVatPriceFormat");
            DgListView.RowHeight = double.Parse(await DataOperations.ParameterCheck("DocumentRowHeight"));
        }

        //change datasource
        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            List<BusinessIncomingInvoiceList> IncomingInvoiceList = new List<BusinessIncomingInvoiceList>();
            List<BusinessExchangeRateList> exchangeRateList = new List<BusinessExchangeRateList>();
            List<ExtendedIncomingInvoiceList> extendedIncomingInvoiceList = new List<ExtendedIncomingInvoiceList>();
            BusinessBranchList defaultAddress = new BusinessBranchList();
            try {
                defaultAddress = await CommunicationManager.GetApiRequest<BusinessBranchList>(ApiUrls.EasyITCenterBusinessBranchList, "Active", App.UserData.Authentification.Token);
                DocumentAdviceList = await CommunicationManager.GetApiRequest<SystemDocumentAdviceList>(ApiUrls.EasyITCenterSystemDocumentAdviceList, "incomingInvoice/" + defaultAddress.Id, App.UserData.Authentification.Token);
                if (DocumentAdviceList == null) { await MainWindow.ShowMessageOnMainWindow(true, Resources["documentAdviceNotSet"].ToString()); }
                AddressList = (await CommunicationManager.GetApiRequest<List<BusinessAddressList>>(ApiUrls.EasyITCenterBusinessAddressList, null, App.UserData.Authentification.Token)).Where(a => a.AddressType == "All" || a.AddressType == "Supplier").ToList();
                cb_totalCurrency.ItemsSource = CurrencyList = await CommunicationManager.GetApiRequest<List<BasicCurrencyList>>(ApiUrls.EasyITCenterBasicCurrencyList, null, App.UserData.Authentification.Token);
                cb_notes.ItemsSource = NotesList = await CommunicationManager.GetApiRequest<List<BusinessNotesList>>(ApiUrls.EasyITCenterBusinessNotesList, null, App.UserData.Authentification.Token);
                cb_maturity.ItemsSource = MaturityList = await CommunicationManager.GetApiRequest<List<BusinessMaturityList>>(ApiUrls.EasyITCenterBusinessMaturityList, null, App.UserData.Authentification.Token);
                cb_paymentMethod.ItemsSource = PaymentMethodList = await CommunicationManager.GetApiRequest<List<BusinessPaymentMethodList>>(ApiUrls.EasyITCenterBusinessPaymentMethodList, null, App.UserData.Authentification.Token);
                cb_paymentStatus.ItemsSource = PaymentStatusList = await CommunicationManager.GetApiRequest<List<BusinessPaymentStatusList>>(ApiUrls.EasyITCenterBusinessPaymentStatusList, null, App.UserData.Authentification.Token);
                cb_unit.ItemsSource = UnitList = (await CommunicationManager.GetApiRequest<List<BasicUnitList>>(ApiUrls.EasyITCenterBasicUnitList, null, App.UserData.Authentification.Token));
                cb_vat.ItemsSource = VatList = (await CommunicationManager.GetApiRequest<List<BasicVatList>>(ApiUrls.EasyITCenterBasicVatList, null, App.UserData.Authentification.Token));
                ItemList = (await CommunicationManager.GetApiRequest<List<BasicItemList>>(ApiUrls.EasyITCenterBasicItemList, null, App.UserData.Authentification.Token));

                CurrencyList.ForEach(async currency => {
                    if (!currency.Fixed) { currency.ExchangeRate = (await CommunicationManager.GetApiRequest<BusinessExchangeRateList>(ApiUrls.EasyITCenterBusinessExchangeRateList, currency.Name, App.UserData.Authentification.Token)).Value; }
                });

                Customer = defaultAddress.CompanyName + Environment.NewLine +
                            defaultAddress.ContactName + Environment.NewLine +
                            defaultAddress.Street + Environment.NewLine +
                            defaultAddress.PostCode + " " + defaultAddress.City + Environment.NewLine + Environment.NewLine +
                            Resources["account"].ToString() + ": " + defaultAddress.BankAccount + Environment.NewLine +
                            Resources["ico"].ToString() + ": " + defaultAddress.Ico + "; " + Resources["dic"].ToString() + ": " + defaultAddress.Dic + Environment.NewLine +
                            Resources["phone"].ToString() + ": " + defaultAddress.Phone + Environment.NewLine +
                            Resources["email"].ToString() + ": " + defaultAddress.Email;

                IncomingInvoiceList = await CommunicationManager.GetApiRequest<List<BusinessIncomingInvoiceList>>(ApiUrls.EasyITCenterBusinessIncomingInvoiceList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);
                IncomingInvoiceList.ForEach(record => {
                    ExtendedIncomingInvoiceList item = new ExtendedIncomingInvoiceList() {
                        Id = record.Id,
                        DocumentNumber = record.DocumentNumber,
                        Supplier = record.Supplier,
                        Customer = record.Customer,
                        IssueDate = record.IssueDate,
                        TaxDate = record.TaxDate,
                        MaturityDate = record.MaturityDate,
                        PaymentMethodId = record.PaymentMethodId,
                        MaturityId = record.MaturityId,
                        OrderNumber = record.OrderNumber,
                        Storned = record.Storned,
                        PaymentStatusId = record.PaymentStatusId,
                        TotalCurrencyId = record.TotalCurrencyId,
                        Description = record.Description,
                        TotalPriceWithVat = record.TotalPriceWithVat,
                        UserId = record.UserId,
                        TimeStamp = record.TimeStamp,
                        TotalCurrency = CurrencyList.Where(a => a.Id == record.TotalCurrencyId).First().Name
                    };
                    extendedIncomingInvoiceList.Add(item);
                });
                DgListView.ItemsSource = extendedIncomingInvoiceList;
                DgListView.Items.Refresh();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        // set translate columns in listView
        private void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(e => {
                    string headername = e.Header.ToString();
                    if (headername == "DocumentNumber") e.Header = Resources["documentNumber"].ToString();
                    else if (headername == "Supplier") e.Header = Resources["supplier"].ToString();
                    else if (headername == "Customer") e.Header = Resources["customer"].ToString();
                    else if (headername == "OrderNumber") { e.Header = Resources["customerOrderNumber"].ToString(); e.DisplayIndex = 2; }
                    else if (headername == "IssueDate") { e.Header = Resources["issueDate"].ToString(); (e as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy"; e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = 7; }
                    else if (headername == "MaturityDate") { e.Header = Resources["maturityDate"].ToString(); (e as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy"; e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = 8; }
                    else if (headername == "Storned") { e.Header = Resources["storned"].ToString(); e.DisplayIndex = DgListView.Columns.Count - 2; }
                    else if (headername == "Description") e.Header = Resources["description"].ToString();
                    else if (headername == "TotalPriceWithVat") { e.Header = Resources["totalPriceWithVat"].ToString(); e.DisplayIndex = 5; e.CellStyle = ProgramaticStyles.gridTextRightAligment; (e as DataGridTextColumn).Binding.StringFormat = "N2"; }
                    else if (headername == "TotalCurrency") { e.Header = Resources["currency"].ToString(); e.DisplayIndex = 6; }
                    else if (headername == "TimeStamp") { e.Header = Resources["timestamp"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "Id") e.DisplayIndex = 0;
                    else if (headername == "UserId") e.Visibility = Visibility.Hidden;
                    else if (headername == "TotalCurrencyId") e.Visibility = Visibility.Hidden;
                    else if (headername == "PaymentMethodId") e.Visibility = Visibility.Hidden;
                    else if (headername == "MaturityId") e.Visibility = Visibility.Hidden;
                    else if (headername == "PaymentStatusId") e.Visibility = Visibility.Hidden;
                    else if (headername == "TaxDate") e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    DataRowView search = e as DataRowView;
                    return search.ObjectToJson().ToLower().Contains(filter.ToLower());
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new ExtendedIncomingInvoiceList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (ExtendedIncomingInvoiceList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (ExtendedIncomingInvoiceList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterBusinessIncomingInvoiceList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                _ = LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (ExtendedIncomingInvoiceList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) {
                selectedRecord = (ExtendedIncomingInvoiceList)DgListView.SelectedItem;
            }
            else { selectedRecord = new ExtendedIncomingInvoiceList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        //change dataset save method
        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.DocumentNumber = txt_documentNumber.Text;
                selectedRecord.Supplier = txt_supplier.Text;
                selectedRecord.Customer = txt_customer.Text;

                selectedRecord.OrderNumber = txt_orderNumber.Text;
                selectedRecord.IssueDate = ((DateTime)dp_issueDate.Value).Date;
                selectedRecord.TaxDate = ((DateTime)dp_taxDate.Value).Date;
                selectedRecord.MaturityDate = ((DateTime)dp_maturityDate.Value).Date;
                selectedRecord.MaturityId = ((BusinessMaturityList)cb_maturity.SelectedItem).Id;
                selectedRecord.PaymentMethodId = ((BusinessPaymentMethodList)cb_paymentMethod.SelectedItem).Id;
                selectedRecord.PaymentStatusId = ((BusinessPaymentStatusList)cb_paymentStatus.SelectedItem).Id;
                selectedRecord.TotalCurrencyId = ((BasicCurrencyList)cb_totalCurrency.SelectedItem).Id;
                selectedRecord.Storned = (bool)chb_storned.IsChecked;
                selectedRecord.TotalCurrencyId = ((BasicCurrencyList)cb_totalCurrency.SelectedItem).Id;
                selectedRecord.TotalPriceWithVat = decimal.Parse(txt_totalPrice.Text.Split(' ')[0]);
                selectedRecord.Description = txt_description.Text;
                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                selectedRecord.TotalCurrency = null;
                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterBusinessIncomingInvoiceList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterBusinessIncomingInvoiceList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    //Save Items
                    DocumentItemList.ForEach(item => { item.Id = 0; item.DocumentNumber = dBResult.Status; item.UserId = App.UserData.Authentification.Id; });
                    dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterBusinessIncomingInvoiceSupportList, dBResult.Status, App.UserData.Authentification.Token);
                    json = JsonConvert.SerializeObject(DocumentItemList); httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterBusinessIncomingInvoiceSupportList, httpContent, null, App.UserData.Authentification.Token);
                    if (dBResult.RecordCount != DocumentItemList.Count()) {
                        await MainWindow.ShowMessageOnMainWindow(true, Resources["itemsDBError"].ToString() + Environment.NewLine + dBResult.ErrorMessage);
                    }
                    else {
                        selectedRecord = new ExtendedIncomingInvoiceList();
                        await LoadDataList();
                        SetRecord(null);
                    }
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (ExtendedIncomingInvoiceList)DgListView.SelectedItem : new ExtendedIncomingInvoiceList();
            SetRecord(false);
        }

        //change dataset prepare for working
        private async void SetRecord(bool? showForm = null, bool copy = false) {
            SetSubListsNonActiveOnNewItem(selectedRecord.Id == 0);
            selectedRecord.Id = (copy) ? 0 : selectedRecord.Id;

            string originalDocumentNumber = (string.IsNullOrWhiteSpace(selectedRecord.DocumentNumber) && !string.IsNullOrWhiteSpace(DocumentAdviceList.Number)) ? (DocumentAdviceList.Prefix + (int.Parse(DocumentAdviceList.Number) + 1).ToString("D" + DocumentAdviceList.Number.Length.ToString())) : selectedRecord.DocumentNumber;
            if (copy) {
                txt_documentNumber.Text = (DocumentAdviceList.Prefix + (int.Parse(DocumentAdviceList.Number) + 1).ToString("D" + DocumentAdviceList.Number.Length.ToString()));
            }
            else { txt_documentNumber.Text = originalDocumentNumber; }

            txt_customer.Text = (!string.IsNullOrWhiteSpace(selectedRecord.Customer)) ? selectedRecord.Customer : Customer;
            txt_supplier.Text = selectedRecord.Supplier;
            dp_issueDate.Value = selectedRecord.IssueDate;
            dp_taxDate.Value = selectedRecord.TaxDate;
            cb_maturity.Text = (MaturityList.Count > 0 && selectedRecord.MaturityId != null) ? MaturityList.FirstOrDefault(a => a.Id == selectedRecord.MaturityId).Name : "";
            cb_paymentMethod.Text = (PaymentMethodList.Count > 0 && selectedRecord.PaymentMethodId != null) ? PaymentMethodList.FirstOrDefault(a => a.Id == selectedRecord.PaymentMethodId).Name : "";
            cb_paymentStatus.Text = (PaymentStatusList.Count > 0 && selectedRecord.PaymentStatusId != null) ? PaymentStatusList.FirstOrDefault(a => a.Id == selectedRecord.PaymentStatusId).Name : "";
            chb_storned.IsChecked = selectedRecord.Storned;
            cb_totalCurrency.Text = selectedRecord.TotalCurrency;
            txt_description.Text = selectedRecord.Description;
            txt_orderNumber.Text = selectedRecord.OrderNumber;

            if (showForm != null && showForm == true) {
                //Load Items and Defaults
                DocumentItemList = await CommunicationManager.GetApiRequest<List<DocumentItemList>>(ApiUrls.EasyITCenterBusinessIncomingInvoiceSupportList, originalDocumentNumber, App.UserData.Authentification.Token);
                DgSubListView.ItemsSource = DocumentItemList; DgSubListView.Items.Refresh(); ClearItemsFields(); txt_totalPrice.Text = DocumentItemList.Sum(a => a.TotalPriceWithVat).ToString(documentVatPriceFormat) + ((cb_totalCurrency.SelectedItem != null) ? " " + ((BasicCurrencyList)cb_totalCurrency.SelectedItem).Name : "");
                if (CurrencyList.Where(a => a.Default).Count() == 1 && string.IsNullOrWhiteSpace(cb_totalCurrency.Text)) { cb_totalCurrency.Text = CurrencyList.First(a => a.Default).Name; }
                if (MaturityList.Where(a => a.Default).Count() == 1 && string.IsNullOrWhiteSpace(cb_maturity.Text)) { cb_maturity.Text = MaturityList.First(a => a.Default).Name; dp_maturityDate.Value = DateTimeOffset.Now.AddDays(double.Parse(MaturityList.First(a => a.Default).Value.ToString())).Date; }
                if (PaymentMethodList.Where(a => a.Default).Count() == 1 && string.IsNullOrWhiteSpace(cb_paymentMethod.Text)) { cb_paymentMethod.Text = PaymentMethodList.First(a => a.Default).Name; }
                if (PaymentStatusList.Where(a => a.Default).Count() == 1 && string.IsNullOrWhiteSpace(cb_paymentStatus.Text)) { cb_paymentStatus.Text = PaymentStatusList.First(a => a.Default).Name; }

                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
            }
            else {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
            }
        }

        private void DgSubListView_Translate(object sender, EventArgs ex) {
            ((DataGrid)sender).Columns.ToList().ForEach(e => {
                string headername = e.Header.ToString();
                if (headername == "PartNumber") e.Header = Resources["partNumber"].ToString();
                else if (headername == "Value") e.Header = Resources["fname"].ToString();
                else if (headername == "Unit") { e.Header = Resources["unit"].ToString(); ; e.CellStyle = ProgramaticStyles.gridTextRightAligment; }
                else if (headername == "PcsPrice") { e.Header = Resources["pcsPrice"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; }
                else if (headername == "Count") { e.Header = Resources["count"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; }
                else if (headername == "TotalPrice") { e.Header = Resources["totalPrice"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; }
                else if (headername == "Vat") { e.Header = Resources["vat"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; }
                else if (headername == "TotalPriceWithVat") { e.Header = Resources["totalPriceWithVat"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; }
                else if (headername == "Id") e.Visibility = Visibility.Hidden;
                else if (headername == "UserId") e.Visibility = Visibility.Hidden;
                else if (headername == "TimeStamp") e.Visibility = Visibility.Hidden;
                else if (headername == "DocumentNumber") e.Visibility = Visibility.Hidden;
            });
        }

        private void SelectGotFocus(object sender, RoutedEventArgs e) => UpdateSupplierSearchResults();

        private async void UpdateSupplierSearchResults() {
            try {
                lv_supplierSearchResults.Visibility = Visibility.Visible;
                List<BusinessAddressList> tempAddressList = AddressList.Where(a => a.CompanyName.ToLower().Contains(!string.IsNullOrWhiteSpace(txt_supplierFilter.Text) ? txt_supplierFilter.Text.ToLower() : "")).ToList();
                if (tempAddressList.Count() == 0 && !messageShown) {
                    messageShown = true;
                    var result = await MainWindow.ShowMessageOnMainWindow(false, Resources["companyNotExist"].ToString());
                    if (result == MessageDialogResult.Affirmative) { messageShown = false; }
                    txt_supplierFilter.Text = LastSupplierCorrectSearch; txt_supplierFilter.CaretIndex = txt_supplier.Text.Length;
                }
                else {
                    lv_supplierSearchResults.ItemsSource = tempAddressList;
                    if (lv_supplierSearchResults.Items.Count == 1) {
                        lv_supplierSearchResults.SelectedItem = lv_supplierSearchResults.Items[0];
                        SetSupplier();
                    }
                    else { lv_supplierSearchResults.Visibility = Visibility.Visible; }
                    LastSupplierCorrectSearch = txt_supplierFilter.Text;
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void Supplier_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Up && lv_supplierSearchResults.Visibility == Visibility.Visible) { lv_supplierSearchResults.Focus(); }
            if (e.Key == Key.Down && lv_supplierSearchResults.Visibility == Visibility.Visible) { lv_supplierSearchResults.Focus(); }
            if (e.Key != Key.Down && e.Key != Key.Up && e.Key != Key.Enter && lv_supplierSearchResults.Visibility == Visibility.Visible) { txt_supplierFilter.Focus(); }
        }

        private void SelectSupplier_Enter(object sender, KeyEventArgs e) {
            if ((e.Key == Key.Enter) && lv_supplierSearchResults.Visibility == Visibility.Visible) { SetSupplier(); }
        }

        private void MouseSelectSupplier_Click(object sender, MouseButtonEventArgs e) => SetSupplier();

        private void SetSupplier() {
            if (lv_supplierSearchResults.SelectedIndex > -1) {
                Supplier = ((BusinessAddressList)lv_supplierSearchResults.SelectedItem).CompanyName + Environment.NewLine +
                            ((BusinessAddressList)lv_supplierSearchResults.SelectedItem).ContactName + Environment.NewLine +
                            ((BusinessAddressList)lv_supplierSearchResults.SelectedItem).Street + Environment.NewLine +
                            ((BusinessAddressList)lv_supplierSearchResults.SelectedItem).PostCode + " " + ((BusinessAddressList)lv_supplierSearchResults.SelectedItem).City + Environment.NewLine + Environment.NewLine +
                            Resources["account"].ToString() + ": " + ((BusinessAddressList)lv_supplierSearchResults.SelectedItem).BankAccount + Environment.NewLine +
                            Resources["ico"].ToString() + ": " + ((BusinessAddressList)lv_supplierSearchResults.SelectedItem).Ico + "; " + Resources["dic"].ToString() + ": " + ((BusinessAddressList)lv_supplierSearchResults.SelectedItem).Dic + Environment.NewLine +
                            Resources["phone"].ToString() + ": " + ((BusinessAddressList)lv_supplierSearchResults.SelectedItem).Phone + Environment.NewLine +
                            Resources["email"].ToString() + ": " + ((BusinessAddressList)lv_supplierSearchResults.SelectedItem).Email;
                txt_supplier.Text = Supplier;
            }
            else { txt_customer.Text = null; }
            lv_supplierSearchResults.Visibility = Visibility.Hidden; txt_supplier.Focus();
        }

        private void PartNumberGotFocus(object sender, RoutedEventArgs e) => UpdatePartNumberSearchResults();

        private void NameGotFocus(object sender, RoutedEventArgs e) => lv_partNumberSearchResults.Visibility = Visibility.Hidden;

        private async void UpdatePartNumberSearchResults() {
            try {
                lv_partNumberSearchResults.Visibility = Visibility.Visible;
                List<BasicItemList> tempItemList = ItemList.Where(a => a.PartNumber.ToLower().Contains(!string.IsNullOrWhiteSpace(txt_partNumber.Text) ? txt_partNumber.Text.ToLower() : "")).ToList();
                if (tempItemList.Count() == 0 && !messageShown) {
                    messageShown = true;
                    var result = await MainWindow.ShowMessageOnMainWindow(false, Resources["itemNotExist"].ToString());
                    if (result == MessageDialogResult.Affirmative) { messageShown = false; }
                    txt_partNumber.Text = LastPartNumberCorrectSearch; txt_partNumber.CaretIndex = txt_customer.Text.Length;
                }
                else {
                    lv_partNumberSearchResults.ItemsSource = tempItemList;
                    if (lv_partNumberSearchResults.Items.Count == 1) {
                        lv_partNumberSearchResults.SelectedItem = lv_partNumberSearchResults.Items[0]; SetPartNumber();
                    }
                    else { lv_partNumberSearchResults.Visibility = Visibility.Visible; }
                    LastPartNumberCorrectSearch = txt_partNumber.Text;
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void PartNumber_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Up && lv_partNumberSearchResults.Visibility == Visibility.Visible) { lv_partNumberSearchResults.Focus(); }
            if (e.Key == Key.Down && lv_partNumberSearchResults.Visibility == Visibility.Visible) { lv_partNumberSearchResults.Focus(); }
            if (e.Key != Key.Down && e.Key != Key.Up && e.Key != Key.Enter && lv_partNumberSearchResults.Visibility == Visibility.Visible) { txt_count.Focus(); }
        }

        private void SelectPartNumber_Enter(object sender, KeyEventArgs e) {
            if ((e.Key == Key.Enter) && lv_partNumberSearchResults.Visibility == Visibility.Visible) { SetPartNumber(); }
        }

        private void MouseSelectPartNumber_Click(object sender, MouseButtonEventArgs e) => SetPartNumber();

        private void CountChanged(object sender, RoutedPropertyChangedEventArgs<double?> e) => CalculateItemVatPrice();

        private void VatChanged(object sender, SelectionChangedEventArgs e) => CalculateItemVatPrice();

        private void SetPartNumber() {
            if (lv_partNumberSearchResults.SelectedIndex > -1) {
                txt_partNumber.Text = ((BasicItemList)lv_partNumberSearchResults.SelectedItem).PartNumber;
                txt_name.Text = ((BasicItemList)lv_partNumberSearchResults.SelectedItem).Name;
                cb_unit.Text = ((BasicItemList)lv_partNumberSearchResults.SelectedItem).Unit;
                txt_pcsPrice.Value = double.Parse(((double)((BasicItemList)lv_partNumberSearchResults.SelectedItem).Price * (1 / (double)CurrencyList.First(a => a.Name == ((BasicCurrencyList)cb_totalCurrency.SelectedItem).Name).ExchangeRate) * (double)CurrencyList.First(a => a.Id == ((BasicItemList)lv_partNumberSearchResults.SelectedItem).CurrencyId).ExchangeRate).ToString(itemVatPriceFormat));
                cb_vat.SelectedItem = VatList.First(a => a.Id == ((BasicItemList)lv_partNumberSearchResults.SelectedItem).VatId);
                CalculateItemVatPrice();
            }
            lv_partNumberSearchResults.Visibility = Visibility.Hidden; txt_count.Focus();
        }

        private void CalculateItemVatPrice() {
            try {
                txt_totalPriceWithVat.Text = ((double)txt_count.Value * (double)(txt_pcsPrice.Value + txt_pcsPrice.Value * (double)((BasicVatList)cb_vat.SelectedItem).Value)).ToString(itemVatPriceFormat) + " " + ((BasicCurrencyList)cb_totalCurrency.SelectedItem).Name;
                btn_insert.IsEnabled = true;
            } catch { txt_totalPriceWithVat.Text = null; btn_insert.IsEnabled = false; }
            txt_totalPrice.Text = DocumentItemList.Sum(a => a.TotalPriceWithVat).ToString(documentVatPriceFormat) + ((cb_totalCurrency.SelectedItem != null) ? " " + ((BasicCurrencyList)cb_totalCurrency.SelectedItem).Name : "");
        }

        private void dgSubListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgSubListView.SelectedItems.Count > 0) {
                btn_delete.IsEnabled = true;
            }
            else { btn_delete.IsEnabled = false; }
        }

        private void BtnItemInsert_Click(object sender, RoutedEventArgs e) {
            try {
                DocumentItemList.Add(new DocumentItemList() {
                    DocumentNumber = txt_documentNumber.Text,
                    PartNumber = txt_partNumber.Text,
                    Name = txt_name.Text,
                    Unit = cb_unit.Text,
                    PcsPrice = (decimal)txt_pcsPrice.Value,
                    Count = (decimal)txt_count.Value,
                    TotalPrice = (decimal)txt_pcsPrice.Value * (decimal)txt_count.Value,
                    Vat = ((BasicVatList)cb_vat.SelectedItem).Value,
                    TotalPriceWithVat = decimal.Parse(((double)txt_count.Value * (double)(txt_pcsPrice.Value + txt_pcsPrice.Value * (double)((BasicVatList)cb_vat.SelectedItem).Value)).ToString(itemVatPriceFormat))
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            DgSubListView.ItemsSource = DocumentItemList;
            DgSubListView.Items.Refresh();

            txt_totalPrice.Text = DocumentItemList.Sum(a => a.TotalPriceWithVat).ToString(documentVatPriceFormat) + ((cb_totalCurrency.SelectedItem != null) ? " " + ((BasicCurrencyList)cb_totalCurrency.SelectedItem).Name : "");
            ClearItemsFields();
        }

        private void BtnItemDelete_Click(object sender, RoutedEventArgs e) {
            DocumentItemList.RemoveAt(DgSubListView.SelectedIndex);
            DgSubListView.ItemsSource = DocumentItemList;
            DgSubListView.Items.Refresh();
            txt_totalPrice.Text = DocumentItemList.Sum(a => a.TotalPriceWithVat).ToString(documentVatPriceFormat) + ((cb_totalCurrency.SelectedItem != null) ? " " + ((BasicCurrencyList)cb_totalCurrency.SelectedItem).Name : "");
        }

        private void NotesChanged(object sender, SelectionChangedEventArgs e) {
            if (cb_notes.SelectedItem != null) { txt_description.Text = ((BusinessNotesList)cb_notes.SelectedItem).Description; cb_notes.SelectedItem = null; }
        }

        private void MaturityChanged(object sender, SelectionChangedEventArgs e) {
            if (cb_maturity.SelectedItem != null) { dp_maturityDate.Value = ((DateTime)dp_issueDate.Value).AddDays(((BusinessMaturityList)cb_maturity.SelectedItem).Value).Date; }
        }

        private void IssueDateChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            if (cb_maturity.SelectedItem != null) { dp_taxDate.Value = dp_issueDate.Value; dp_maturityDate.Value = ((DateTime)dp_issueDate.Value).AddDays(((BusinessMaturityList)cb_maturity.SelectedItem).Value).Date; }
        }

        private void ClearItemsFields() {
            txt_partNumber.Text = txt_name.Text = cb_unit.Text = txt_totalPriceWithVat.Text = null;
            txt_count.Value = txt_pcsPrice.Value = null;
            cb_unit.SelectedItem = cb_vat.SelectedItem = null;
            lv_partNumberSearchResults.Visibility = Visibility.Hidden;
        }

        private void SetSubListsNonActiveOnNewItem(bool newItem) {
            if (newItem) {
                cb_totalCurrency.ItemsSource = CurrencyList.Where(a => a.Active).ToList();
                cb_notes.ItemsSource = NotesList.Where(a => a.Active).ToList();
                cb_maturity.ItemsSource = MaturityList.Where(a => a.Active).ToList();
                cb_paymentMethod.ItemsSource = PaymentMethodList.Where(a => a.Active).ToList();
                cb_paymentStatus.ItemsSource = PaymentStatusList.Where(a => a.Active).ToList();
            }
            else {
                cb_totalCurrency.ItemsSource = CurrencyList; cb_notes.ItemsSource = NotesList; cb_maturity.ItemsSource = MaturityList;
                cb_paymentMethod.ItemsSource = PaymentMethodList; cb_paymentStatus.ItemsSource = PaymentStatusList;
            }
        }
    }
}