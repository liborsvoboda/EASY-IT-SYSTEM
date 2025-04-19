using EasyITSystemCenter.Api;
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

    public partial class BusinessOutgoingOrderListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static ExtendedOutgoingOrderList selectedRecord = new ExtendedOutgoingOrderList();

        private SystemDocumentAdviceList DocumentAdviceList = new SystemDocumentAdviceList();
        private List<BasicCurrencyList> CurrencyList = new List<BasicCurrencyList>();
        private string Supplier = null; private string Customer = null;
        private List<BusinessAddressList> AddressList = new List<BusinessAddressList>();
        private List<BusinessNotesList> NotesList = new List<BusinessNotesList>();
        private List<BasicUnitList> UnitList = new List<BasicUnitList>();
        private string LastSupplierCorrectSearch, LastPartNumberCorrectSearch = ""; private bool messageShown = false;

        private List<DocumentItemList> DocumentItemList = new List<DocumentItemList>();
        private List<BasicItemList> ItemList = new List<BasicItemList>();
        private List<BasicVatList> VatList = new List<BasicVatList>();
        private string itemVatPriceFormat = "N2"; private string documentVatPriceFormat = "N0";

        public BusinessOutgoingOrderListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            //translate fields in detail form
            try {
                try {
                    _ = FormOperations.TranslateFormFields(ListForm);
                    _ = FormOperations.TranslateFormFields(SubListView);
                } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

                LoadParameters();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            _ = LoadDataList();
            SetRecord(false);
        }

        private async void LoadParameters() {
            itemVatPriceFormat = await DataOperations.ParameterCheck("ItemVatPriceFormat");
            documentVatPriceFormat = await DataOperations.ParameterCheck("DocumentVatPriceFormat");
            DgListView.RowHeight = int.Parse(await DataOperations.ParameterCheck("DocumentRowHeight"));
        }

        //change datasource
        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            List<BusinessOutgoingOrderList> OutgoingOrderList = new List<BusinessOutgoingOrderList>();
            List<BusinessExchangeRateList> exchangeRateList = new List<BusinessExchangeRateList>();
            List<ExtendedOutgoingOrderList> extendedOutgoingOrderList = new List<ExtendedOutgoingOrderList>();
            BusinessBranchList defaultAddress = new BusinessBranchList();
            try {
                defaultAddress = await CommunicationManager.GetApiRequest<BusinessBranchList>(ApiUrls.EasyITCenterBusinessBranchList, "Active", App.UserData.Authentification.Token);
                DocumentAdviceList = await CommunicationManager.GetApiRequest<SystemDocumentAdviceList>(ApiUrls.EasyITCenterSystemDocumentAdviceList, "outgoingOrder/" + defaultAddress.Id, App.UserData.Authentification.Token);
                if (DocumentAdviceList == null) { await MainWindow.ShowMessageOnMainWindow(true, Resources["documentAdviceNotSet"].ToString()); }
                cb_totalCurrency.ItemsSource = CurrencyList = await CommunicationManager.GetApiRequest<List<BasicCurrencyList>>(ApiUrls.EasyITCenterBasicCurrencyList, null, App.UserData.Authentification.Token);
                cb_notes.ItemsSource = NotesList = await CommunicationManager.GetApiRequest<List<BusinessNotesList>>(ApiUrls.EasyITCenterBusinessNotesList, null, App.UserData.Authentification.Token);
                cb_unit.ItemsSource = UnitList = await CommunicationManager.GetApiRequest<List<BasicUnitList>>(ApiUrls.EasyITCenterBasicUnitList, null, App.UserData.Authentification.Token);
                cb_vat.ItemsSource = VatList = await CommunicationManager.GetApiRequest<List<BasicVatList>>(ApiUrls.EasyITCenterBasicVatList, null, App.UserData.Authentification.Token);

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

                OutgoingOrderList = await CommunicationManager.GetApiRequest<List<BusinessOutgoingOrderList>>(ApiUrls.EasyITCenterBusinessOutgoingOrderList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);
                OutgoingOrderList.ForEach(record => {
                    ExtendedOutgoingOrderList item = new ExtendedOutgoingOrderList() {
                        Id = record.Id,
                        DocumentNumber = record.DocumentNumber,
                        Supplier = record.Supplier,
                        Customer = record.Customer,
                        Storned = record.Storned,
                        TotalCurrencyId = record.TotalCurrencyId,
                        Description = record.Description,
                        TotalPriceWithVat = record.TotalPriceWithVat,
                        UserId = record.UserId,
                        TimeStamp = record.TimeStamp,
                        TotalCurrency = CurrencyList.Where(a => a.Id == record.TotalCurrencyId).First().Name
                    };
                    extendedOutgoingOrderList.Add(item);
                });
                DgListView.ItemsSource = extendedOutgoingOrderList;
                DgListView.Items.Refresh();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        // set translate columns in listView
        private async void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                    string headername = e.Header.ToString().ToLower();
                    if (headername == "DocumentNumber".ToLower()) e.Header = await DBOperations.DBTranslation(headername);
                    else if (headername == "Supplier".ToLower()) e.Header = await DBOperations.DBTranslation(headername);
                    else if (headername == "Customer".ToLower()) e.Header = await DBOperations.DBTranslation(headername);
                    else if (headername == "Storned".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 7; }
                    else if (headername == "Description".ToLower()) e.Header = await DBOperations.DBTranslation(headername);
                    else if (headername == "TotalPriceWithVat".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 4; e.CellStyle = ProgramaticStyles.gridTextRightAligment; (e as DataGridTextColumn).Binding.StringFormat = "N2"; }
                    else if (headername == "TotalCurrency".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 5; }
                    else if (headername == "TimeStamp".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "Id".ToLower()) e.DisplayIndex = 0;
                    else if (headername == "UserId".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "TotalCurrencyId".ToLower()) e.Visibility = Visibility.Hidden;
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
            selectedRecord = new ExtendedOutgoingOrderList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (ExtendedOutgoingOrderList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (ExtendedOutgoingOrderList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterBusinessOutgoingOrderList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                _ = LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (ExtendedOutgoingOrderList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) {
                selectedRecord = (ExtendedOutgoingOrderList)DgListView.SelectedItem;
            }
            else { selectedRecord = new ExtendedOutgoingOrderList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        //change dataset save method
        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.DocumentNumber = txt_documentNumber.Text;
                selectedRecord.Customer = txt_customer.Text;
                selectedRecord.Supplier = txt_supplier.Text;
                selectedRecord.Storned = (bool)chb_storned.IsChecked;
                selectedRecord.TotalCurrencyId = ((BasicCurrencyList)cb_totalCurrency.SelectedItem).Id;
                selectedRecord.Description = txt_description.Text;
                selectedRecord.TotalPriceWithVat = decimal.Parse(txt_totalPrice.Text.Split(' ')[0]);
                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                selectedRecord.TotalCurrency = null;
                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterBusinessOutgoingOrderList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterBusinessOutgoingOrderList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    //Save Items
                    DocumentItemList.ForEach(item => { item.Id = 0; item.DocumentNumber = dBResult.Status; item.UserId = App.UserData.Authentification.Id; });
                    dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterBusinessOutgoingOrderSupportList, dBResult.Status, App.UserData.Authentification.Token);
                    json = JsonConvert.SerializeObject(DocumentItemList); httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterBusinessOutgoingOrderSupportList, httpContent, null, App.UserData.Authentification.Token);
                    if (dBResult.RecordCount != DocumentItemList.Count()) { await MainWindow.ShowMessageOnMainWindow(true, Resources["itemsDBError"].ToString() + Environment.NewLine + dBResult.ErrorMessage); }
                    else {
                        selectedRecord = new ExtendedOutgoingOrderList();
                        await LoadDataList();
                        SetRecord(null);
                    }
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (ExtendedOutgoingOrderList)DgListView.SelectedItem : new ExtendedOutgoingOrderList();
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
            chb_storned.IsChecked = selectedRecord.Storned;
            cb_totalCurrency.Text = selectedRecord.TotalCurrency;
            txt_description.Text = selectedRecord.Description;

            if (showForm != null && showForm == true) {
                //Load Items
                DocumentItemList.Clear();
                DocumentItemList = await CommunicationManager.GetApiRequest<List<DocumentItemList>>(ApiUrls.EasyITCenterBusinessOutgoingOrderSupportList, originalDocumentNumber, App.UserData.Authentification.Token);
                DgSubListView.ItemsSource = DocumentItemList; DgSubListView.Items.Refresh(); ClearItemsFields(); txt_totalPrice.Text = DocumentItemList.Sum(a => a.TotalPriceWithVat).ToString(documentVatPriceFormat) + ((cb_totalCurrency.SelectedItem != null) ? " " + ((BasicCurrencyList)cb_totalCurrency.SelectedItem).Name : "");
                if (CurrencyList.Where(a => a.Default).Count() == 1 && cb_totalCurrency.Text == null) { cb_totalCurrency.Text = CurrencyList.First(a => a.Default).Name; }

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
                    txt_supplierFilter.Text = LastSupplierCorrectSearch; txt_supplierFilter.CaretIndex = txt_customer.Text.Length;
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
            lv_supplierSearchResults.Visibility = Visibility.Hidden; txt_customer.Focus();
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

        private void ClearItemsFields() {
            txt_partNumber.Text = txt_name.Text = cb_unit.Text = txt_totalPriceWithVat.Text = null;
            txt_count.Value = txt_pcsPrice.Value = null;
            cb_unit.SelectedItem = cb_vat.SelectedItem = null;
            lv_partNumberSearchResults.Visibility = Visibility.Hidden;
        }

        private async void SetSubListsNonActiveOnNewItem(bool newItem) {
            if (newItem) {
                cb_totalCurrency.ItemsSource = CurrencyList.Where(a => a.Active).ToList();
                AddressList = (await CommunicationManager.GetApiRequest<List<BusinessAddressList>>(ApiUrls.EasyITCenterBusinessAddressList, null, App.UserData.Authentification.Token)).Where(a => a.Active && (a.AddressType == "All" || a.AddressType == "Supplier")).ToList();
                ItemList = (await CommunicationManager.GetApiRequest<List<BasicItemList>>(ApiUrls.EasyITCenterBasicItemList, null, App.UserData.Authentification.Token)).Where(a => a.Active).ToList();
                cb_notes.ItemsSource = NotesList.Where(a => a.Active).ToList();
                cb_unit.ItemsSource = UnitList.Where(a => a.Active).ToList();
                cb_vat.ItemsSource = VatList.Where(a => a.Active).ToList();
            }
            else {
                cb_totalCurrency.ItemsSource = CurrencyList;
                AddressList = (await CommunicationManager.GetApiRequest<List<BusinessAddressList>>(ApiUrls.EasyITCenterBusinessAddressList, null, App.UserData.Authentification.Token)).Where(a => a.AddressType == "All" || a.AddressType == "Supplier").ToList();
                ItemList = await CommunicationManager.GetApiRequest<List<BasicItemList>>(ApiUrls.EasyITCenterBasicItemList, null, App.UserData.Authentification.Token);
                cb_notes.ItemsSource = NotesList; cb_unit.ItemsSource = UnitList; cb_vat.ItemsSource = VatList;
            }
        }
    }
}