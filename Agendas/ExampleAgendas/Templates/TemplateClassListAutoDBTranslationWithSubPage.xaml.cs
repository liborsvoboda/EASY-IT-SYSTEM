﻿using EasyITSystemCenter.Api;
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

// This is Template Page ListView + UserForm + Full SubListView And Inserting SubItems
namespace EasyITSystemCenter.Pages {

    /// <summary>
    /// This standartized Template is only for list view od Data table Called from MainWindow.cs on
    /// open New Tab
    /// </summary>
    public partial class TemplateClassListAutoDBTranslationWithSubPage : UserControl {

        /// <summary>
        /// Standartized declaring static page data and selected record for All Pages this method is
        /// for global working with pages Called from MainWindow.cs for Control of Button Menu and
        /// Selections (Report,Filter and more) All is setted as global Classes for All Pages and
        /// Work is Fully automatized by System core
        ///
        /// HERE you Define All Data Variables For This Form
        /// </summary>
        public static DataViewSupport dataViewSupport = new DataViewSupport();

        public static ExtendedOfferList selectedRecord = new ExtendedOfferList();

        private SystemDocumentAdviceList DocumentAdviceList = new SystemDocumentAdviceList();
        private List<BasicCurrencyList> CurrencyList = new List<BasicCurrencyList>();
        private string Supplier = null; private string Customer = null;
        private List<BusinessAddressList> AddressList = new List<BusinessAddressList>();
        private string LastCustomerCorrectSearch, LastPartNumberCorrectSearch = ""; private bool messageShown = false;

        private List<DocumentItemList> DocumentItemList = new List<DocumentItemList>();
        private List<BasicItemList> ItemList = new List<BasicItemList>();
        private List<BasicVatList> VatList = new List<BasicVatList>();
        private List<BusinessNotesList> NotesList = new List<BusinessNotesList>();
        private List<BasicUnitList> UnitList = new List<BasicUnitList>();
        private string itemVatPriceFormat = "N2"; private string documentVatPriceFormat = "N0";

        /// <summary>
        /// Initialize page with loading Dictionary and start loding data Manual work needed
        /// Translate All XAML fields by Resources Runned on start
        /// </summary>
        public TemplateClassListAutoDBTranslationWithSubPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            try {
                _ = FormOperations.TranslateFormFields(ListForm);
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            LoadParameters();
            _ = LoadDataList();
            SetRecord(false);
        }

        private async void LoadParameters() {
            itemVatPriceFormat = await DataOperations.ParameterCheck("ItemVatPriceFormat");
            documentVatPriceFormat = await DataOperations.ParameterCheck("DocumentVatPriceFormat");
            DgListView.RowHeight = int.Parse(await DataOperations.ParameterCheck("DocumentRowHeight"));
        }

        /// <summary>
        /// Standartized Method for Loading data. Manual Changing is needed for simple form is All
        /// changed By CLASNAME Chage, but If you need More API data for selection Here are Defined
        /// All incoming Data Loading is same centralized only change ClasName For Diferent Dataset
        ///
        /// After all data for DatagridView (List Data) are loaded The ProgressRing is hidden This
        /// is on Every page ('View' and 'Form' Types) without 'Setting' Type (Name=Setting and
        /// Tag=Setting in XAML part) this method is for global working with pages Called from
        /// MainWindow.cs on Refresh button click Runned on Pageloading or Filter or View Change
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            List<BusinessOfferList> offerList = new List<BusinessOfferList>();
            List<BusinessExchangeRateList> exchangeRateList = new List<BusinessExchangeRateList>();
            List<ExtendedOfferList> extendedOfferList = new List<ExtendedOfferList>();
            BusinessBranchList defaultAddress = new BusinessBranchList();
            try {
                defaultAddress = await CommunicationManager.GetApiRequest<BusinessBranchList>(ApiUrls.EasyITCenterBusinessBranchList, "Active", App.UserData.Authentification.Token);
                DocumentAdviceList = await CommunicationManager.GetApiRequest<SystemDocumentAdviceList>(ApiUrls.EasyITCenterSystemDocumentAdviceList, "offer/" + defaultAddress.Id, App.UserData.Authentification.Token);
                if (DocumentAdviceList == null) { await MainWindow.ShowMessageOnMainWindow(true, Resources["documentAdviceNotSet"].ToString()); }
                cb_totalCurrency.ItemsSource = CurrencyList = await CommunicationManager.GetApiRequest<List<BasicCurrencyList>>(ApiUrls.EasyITCenterBasicCurrencyList, null, App.UserData.Authentification.Token);
                cb_notes.ItemsSource = NotesList = await CommunicationManager.GetApiRequest<List<BusinessNotesList>>(ApiUrls.EasyITCenterBusinessNotesList, null, App.UserData.Authentification.Token);
                cb_unit.ItemsSource = UnitList = await CommunicationManager.GetApiRequest<List<BasicUnitList>>(ApiUrls.EasyITCenterBasicUnitList, null, App.UserData.Authentification.Token);
                cb_vat.ItemsSource = VatList = await CommunicationManager.GetApiRequest<List<BasicVatList>>(ApiUrls.EasyITCenterBasicVatList, null, App.UserData.Authentification.Token);

                CurrencyList.ForEach(async currency => {
                    if (!currency.Fixed) { currency.ExchangeRate = (await CommunicationManager.GetApiRequest<BusinessExchangeRateList>(ApiUrls.EasyITCenterBusinessExchangeRateList, currency.Name, App.UserData.Authentification.Token)).Value; }
                });

                Supplier = defaultAddress.CompanyName + Environment.NewLine +
                            defaultAddress.ContactName + Environment.NewLine +
                            defaultAddress.Street + Environment.NewLine +
                            defaultAddress.PostCode + " " + defaultAddress.City + Environment.NewLine + Environment.NewLine +
                            Resources["account"].ToString() + ": " + defaultAddress.BankAccount + Environment.NewLine +
                            Resources["ico"].ToString() + ": " + defaultAddress.Ico + "; " + Resources["dic"].ToString() + ": " + defaultAddress.Dic + Environment.NewLine +
                            Resources["phone"].ToString() + ": " + defaultAddress.Phone + Environment.NewLine +
                            Resources["email"].ToString() + ": " + defaultAddress.Email;

                offerList = await CommunicationManager.GetApiRequest<List<BusinessOfferList>>(ApiUrls.EasyITCenterBusinessOfferList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);
                offerList.ForEach(record => {
                    ExtendedOfferList item = new ExtendedOfferList() {
                        Id = record.Id,
                        DocumentNumber = record.DocumentNumber,
                        Supplier = record.Supplier,
                        Customer = record.Customer,
                        OfferValidity = record.OfferValidity,
                        Storned = record.Storned,
                        TotalCurrencyId = record.TotalCurrencyId,
                        Description = record.Description,
                        TotalPriceWithVat = record.TotalPriceWithVat,
                        UserId = record.UserId,
                        TimeStamp = record.TimeStamp,
                        TotalCurrency = CurrencyList.Where(a => a.Id == record.TotalCurrencyId).First().Name
                    };
                    extendedOfferList.Add(item);
                });
                DgListView.ItemsSource = extendedOfferList;
                DgListView.Items.Refresh();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        /// <summary>
        /// Standartized method for translating column names of DatagidView (List Data) Manual
        /// Changing is needed for set Translate of Column Names via Dictionary Items Here you can
        /// set Format(Date,time, etc),Index position, Hide Column, Translate, change grahics Style
        /// This is on Every page ('View' and 'Form' Types) without 'Setting' Type (Name=Setting and
        /// Tag=Setting in XAML part) this method is for global working with page internal reaction
        /// on DatagrigView DataFiling on Start Page Runned On Page Loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ex">    </param>
        private async void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                    string headername = e.Header.ToString();
                    if (headername == "DocumentNumber") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                    else if (headername == "Supplier") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                    else if (headername == "Customer") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 3; }
                    else if (headername == "OfferValidity") { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = 4; }
                    else if (headername == "Storned") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 8; }
                    else if (headername == "Description") e.Header = await DBOperations.DBTranslation(headername);
                    else if (headername == "TotalPriceWithVat") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 5; e.CellStyle = ProgramaticStyles.gridTextRightAligment; (e as DataGridTextColumn).Binding.StringFormat = "N2"; }
                    else if (headername == "TotalCurrency") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 6; }
                    else if (headername == "TimeStamp") { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "Id") e.DisplayIndex = 0;
                    else if (headername == "UserId") e.Visibility = Visibility.Hidden;
                    else if (headername == "TotalCurrencyId") e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        /// <summary>
        /// Standartized method for searching match in setted columns. Searched value is from the
        /// simple 'Search Input' for DatagidView (List Data) Manual Changing is needed of filtered
        /// columns by Search Value This is on Every page ('View' and 'Form' Types) without
        /// 'Setting' Type (Name=Setting and Tag=Setting in XAML part) this method is for global
        /// working with pages Called from MainWindow.cs Dynamicaly Called Only from MainWindow.cs
        /// when Search value Inserted
        /// </summary>
        /// <param name="filter"></param>
        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    ExtendedOfferList user = e as ExtendedOfferList;
                    return user.Customer.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.Description) && user.Description.ToLower().Contains(filter.ToLower());
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        /// <summary>
        /// Standartized Method on All Pages with Forms for New Record ALL Needed changes Are done
        /// By Replace CLASSNAME not needed manual work Dynamicaly Called Only from MainWindow.cs on
        /// New button Click Only Set Record And Hide Dataview and Show Detail
        /// </summary>
        public void NewRecord() {
            selectedRecord = new ExtendedOfferList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        /// <summary>
        /// Standartized Method on All Pages with Forms for New Record ALL Needed changes Are done
        /// By Replace CLASSNAME not needed manual work Dynamicaly Called Only from MainWindow.cs on
        /// Edit button Click Only Set Record And Hide Dataview and Show Detail with selected Record
        /// </summary>
        public void EditRecord(bool copy) {
            selectedRecord = (ExtendedOfferList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        /// <summary>
        /// Standartized Method on All Pages with Forms for New Record ALL Needed changes Are done
        /// By Replace CLASSNAME not needed manual work Dynamicaly Called Only from MainWindow.cs on
        /// Delete button Click Show MainWindow Standartized Message with info About Delete and
        /// After confirm Send DeleteApiRequest Reload Datalist and cancel Selected Record
        /// </summary>
        public async void DeleteRecord() {
            selectedRecord = (ExtendedOfferList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterBusinessOfferList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        /// <summary>
        /// Standartized method for selecting and opening Detail Form. This is only View Page, that
        /// is only for Select record This is full automatic, not needed manual work This is on
        /// Every page ('View' and 'Form' Types) without 'Setting' Type (Name=Setting and
        /// Tag=Setting in XAML part) this method is for global working page its local control From XAML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (ExtendedOfferList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        /// <summary>
        /// Standartized method for select one record only. This is full automatic, not needed
        /// manual work This is on Every page ('View' and 'Form' Types) without 'Setting' Type
        /// (Name=Setting and Tag=Setting in XAML part) this method is for global working page its
        /// local control From XAML
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">     
        /// The <see cref="SelectionChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (ExtendedOfferList)DgListView.SelectedItem; }
            else { selectedRecord = new ExtendedOfferList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        /// <summary>
        /// Standartized method for Save Record And return to Dataview. Manual work need, Here is
        /// Join Betwen XAML inputs and Selected Record Dataset (dataset for Detail): Direction FORM
        /// to SELECTED RECORD By ClasName Replacing All other changes are automaticaly
        /// (API,DatasetType), just must define and control Each Field Of Dataset this method is for
        /// global working page its local control From XAML
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">     
        /// The <see cref="RoutedEventArgs"/> instance containing the event data.
        /// </param>
        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.DocumentNumber = txt_documentNumber.Text;
                selectedRecord.Supplier = txt_supplier.Text;
                selectedRecord.Customer = txt_customer.Text;
                selectedRecord.OfferValidity = (txt_offerValidity.Value == null) ? 30 : (int)txt_offerValidity.Value;
                selectedRecord.Storned = (bool)chb_storned.IsChecked;
                selectedRecord.TotalCurrencyId = ((BasicCurrencyList)cb_totalCurrency.SelectedItem).Id;
                selectedRecord.Description = txt_description.Text;
                selectedRecord.TotalPriceWithVat = decimal.Parse(txt_totalPrice.Text.Split(' ')[0]);
                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterBusinessOfferList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterBusinessOfferList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    //Save Items
                    DocumentItemList.ForEach(item => { item.Id = 0; item.DocumentNumber = dBResult.Status; item.UserId = App.UserData.Authentification.Id; });
                    dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterBusinessOfferSupportList, dBResult.Status, App.UserData.Authentification.Token);
                    json = JsonConvert.SerializeObject(DocumentItemList); httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterBusinessOfferSupportList, httpContent, null, App.UserData.Authentification.Token);
                    if (dBResult.RecordCount != DocumentItemList.Count()) { await MainWindow.ShowMessageOnMainWindow(true, Resources["itemsDBError"].ToString() + Environment.NewLine + dBResult.ErrorMessage); }
                    else {
                        selectedRecord = new ExtendedOfferList();
                        await LoadDataList();
                        SetRecord(null);
                    }
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        /// <summary>
        /// Standartized method for cancel of Editing. Hide Detail and Dataview List is Shown This
        /// is full automatic, not needed manual work This is on Every page ('View' and 'Form'
        /// Types) without 'Setting' Type (Name=Setting and Tag=Setting in XAML part) this method is
        /// for global working page its local control From XAML
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">     
        /// The <see cref="RoutedEventArgs"/> instance containing the event data.
        /// </param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (ExtendedOfferList)DgListView.SelectedItem : new ExtendedOfferList();
            SetRecord(false);
        }

        /// <summary>
        /// Standartized method for ¨Set New Record/ Edit Record / Copy Record And return to
        /// Dataview. Manual work need, Here is Join Betwen XAML inputs and Selected Record Dataset
        /// (dataset for Detail): Direction Selected record to FORM By ClasName Replacing All other
        /// changes are automaticaly (API,DatasetType), just must define and control Each Field Of
        /// Dataset this method is for global working page its local control From XAML
        ///
        /// In this type Form Here Are loaded Data for SublistView (on knows selected record for
        /// correct join)
        /// </summary>
        /// <param name="showForm">if set to <c>true</c> [show form].</param>
        /// <param name="copy">    if set to <c>true</c> [copy].</param>
        private async void SetRecord(bool? showForm = null, bool copy = false) {
            SetSubListsNonActiveOnNewItem(selectedRecord.Id == 0);
            selectedRecord.Id = (copy) ? 0 : selectedRecord.Id;

            string originalDocumentNumber = (string.IsNullOrWhiteSpace(selectedRecord.DocumentNumber) && !string.IsNullOrWhiteSpace(DocumentAdviceList.Number)) ? (DocumentAdviceList.Prefix + (int.Parse(DocumentAdviceList.Number) + 1).ToString("D" + DocumentAdviceList.Number.Length.ToString())) : selectedRecord.DocumentNumber;
            if (copy) {
                txt_documentNumber.Text = (DocumentAdviceList.Prefix + (int.Parse(DocumentAdviceList.Number) + 1).ToString("D" + DocumentAdviceList.Number.Length.ToString()));
            }
            else { txt_documentNumber.Text = originalDocumentNumber; }

            txt_supplier.Text = (!string.IsNullOrWhiteSpace(selectedRecord.Supplier)) ? selectedRecord.Supplier : Supplier;
            txt_customer.Text = selectedRecord.Customer;
            txt_offerValidity.Value = (selectedRecord.OfferValidity == 0) ? 30 : selectedRecord.OfferValidity;
            chb_storned.IsChecked = selectedRecord.Storned;
            cb_totalCurrency.Text = selectedRecord.TotalCurrency;
            txt_description.Text = selectedRecord.Description;

            if (showForm != null && showForm == true) {
                DocumentItemList = await CommunicationManager.GetApiRequest<List<DocumentItemList>>(ApiUrls.EasyITCenterBusinessOfferSupportList, originalDocumentNumber, App.UserData.Authentification.Token);
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

        /// <summary>
        /// Standartized method for translating column names of SubDatagidView (List Data) Manual
        /// Changing is needed for set Translate of Column Names via Dictionary Items Here you can
        /// set Format(Date,time, etc),Index position, Hide Column, Translate, change grahics Style
        /// This is on Every page ('View' and 'Form' Types) without 'Setting' Type (Name=Setting and
        /// Tag=Setting in XAML part) this method is for global working with page internal reaction
        /// on DatagrigView DataFiling on Start Page Runned On Page Loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ex">    </param>
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

        #region Customer Selection

        /// <summary>
        /// Standartized method indicate start loading all data of SubRecord after Selected in
        /// Combobox This is full automatic, not needed manual work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void SelectGotFocus(object sender, RoutedEventArgs e) => UpdateCustomerSearchResults();

        /// <summary>
        /// Standartized method Filling Customer Input by Selected Value This is full automatic, not
        /// needed manual work
        /// </summary>
        private async void UpdateCustomerSearchResults() {
            try {
                lv_customerSearchResults.Visibility = Visibility.Visible;
                List<BusinessAddressList> tempAddressList = AddressList.Where(a => a.CompanyName.ToLower().Contains(!string.IsNullOrWhiteSpace(txt_customerFilter.Text) ? txt_customerFilter.Text.ToLower() : "")).ToList();
                if (tempAddressList.Count() == 0 && !messageShown) {
                    messageShown = true;
                    var result = await MainWindow.ShowMessageOnMainWindow(false, Resources["companyNotExist"].ToString());
                    if (result == MessageDialogResult.Affirmative) { messageShown = false; }
                    txt_customerFilter.Text = LastCustomerCorrectSearch; txt_customerFilter.CaretIndex = txt_customer.Text.Length;
                }
                else {
                    lv_customerSearchResults.ItemsSource = tempAddressList;
                    if (lv_customerSearchResults.Items.Count == 1) {
                        lv_customerSearchResults.SelectedItem = lv_customerSearchResults.Items[0];
                        SetCustomer();
                    }
                    else { lv_customerSearchResults.Visibility = Visibility.Visible; }
                    LastCustomerCorrectSearch = txt_customerFilter.Text;
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        /// <summary>
        /// Standartized method for Keyboard control of SelectBox This is full automatic, not needed
        /// manual work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void Customer_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Up && lv_customerSearchResults.Visibility == Visibility.Visible) { lv_customerSearchResults.Focus(); }
            if (e.Key == Key.Down && lv_customerSearchResults.Visibility == Visibility.Visible) { lv_customerSearchResults.Focus(); }
            if (e.Key != Key.Down && e.Key != Key.Up && e.Key != Key.Enter && lv_customerSearchResults.Visibility == Visibility.Visible) { txt_customerFilter.Focus(); }
        }

        /// <summary>
        /// Standartized methods with Indicate Customer Selection and Start Filling Input This is
        /// full automatic, not needed manual work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void SelectCustomer_Enter(object sender, KeyEventArgs e) { if ((e.Key == Key.Enter) && lv_customerSearchResults.Visibility == Visibility.Visible) { SetCustomer(); } }

        private void MouseSelectCustomer_Click(object sender, MouseButtonEventArgs e) => SetCustomer();

        /// <summary>
        /// Standartized methods For Filling Input after Selection This is full automatic, not
        /// needed manual work
        /// </summary>
        private void SetCustomer() {
            if (lv_customerSearchResults.SelectedIndex > -1) {
                Customer = ((BusinessAddressList)lv_customerSearchResults.SelectedItem).CompanyName + Environment.NewLine +
                            ((BusinessAddressList)lv_customerSearchResults.SelectedItem).ContactName + Environment.NewLine +
                            ((BusinessAddressList)lv_customerSearchResults.SelectedItem).Street + Environment.NewLine +
                            ((BusinessAddressList)lv_customerSearchResults.SelectedItem).PostCode + " " + ((BusinessAddressList)lv_customerSearchResults.SelectedItem).City + Environment.NewLine + Environment.NewLine +
                            Resources["account"].ToString() + ": " + ((BusinessAddressList)lv_customerSearchResults.SelectedItem).BankAccount + Environment.NewLine +
                            Resources["ico"].ToString() + ": " + ((BusinessAddressList)lv_customerSearchResults.SelectedItem).Ico + "; " + Resources["dic"].ToString() + ": " + ((BusinessAddressList)lv_customerSearchResults.SelectedItem).Dic + Environment.NewLine +
                            Resources["phone"].ToString() + ": " + ((BusinessAddressList)lv_customerSearchResults.SelectedItem).Phone + Environment.NewLine +
                            Resources["email"].ToString() + ": " + ((BusinessAddressList)lv_customerSearchResults.SelectedItem).Email;
                txt_customer.Text = Customer;
            }
            else { txt_customer.Text = null; }
            lv_customerSearchResults.Visibility = Visibility.Hidden; txt_customer.Focus();
        }

        #endregion Customer Selection

        #region SubItem Selection

        /// <summary>
        /// Standartized method indicate start loading all data of SubRecord after Selected in
        /// Combobox This is full automatic, not needed manual work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void PartNumberGotFocus(object sender, RoutedEventArgs e) => UpdatePartNumberSearchResults();

        private void NameGotFocus(object sender, RoutedEventArgs e) => lv_partNumberSearchResults.Visibility = Visibility.Hidden;

        /// <summary>
        /// Standartized method Filling Customer Input by Selected Value This is full automatic, not
        /// needed manual work
        /// </summary>
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

        /// <summary>
        /// Standartized method for Keyboard control of SelectBox This is full automatic, not needed
        /// manual work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void PartNumber_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Up && lv_partNumberSearchResults.Visibility == Visibility.Visible) { lv_partNumberSearchResults.Focus(); }
            if (e.Key == Key.Down && lv_partNumberSearchResults.Visibility == Visibility.Visible) { lv_partNumberSearchResults.Focus(); }
            if (e.Key != Key.Down && e.Key != Key.Up && e.Key != Key.Enter && lv_partNumberSearchResults.Visibility == Visibility.Visible) { txt_count.Focus(); }
        }

        /// <summary>
        /// Standartized method for select one record only. This is full automatic, not needed
        /// manual work This is on page With Sublist ('View' and 'Form' Types) without 'Setting'
        /// Type (Name=Setting and Tag=Setting in XAML part) this method is for global working page
        /// its local control From XAML
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">     The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void SelectPartNumber_Enter(object sender, KeyEventArgs e) { if ((e.Key == Key.Enter) && lv_partNumberSearchResults.Visibility == Visibility.Visible) { SetPartNumber(); } }

        private void MouseSelectPartNumber_Click(object sender, MouseButtonEventArgs e) => SetPartNumber();

        private void CountChanged(object sender, RoutedPropertyChangedEventArgs<double?> e) => CalculateItemVatPrice();

        /// <summary>
        /// Standartized methods For Filling Input after Selection This is full automatic, not
        /// needed manual work
        /// </summary>
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

        #endregion SubItem Selection

        private void VatChanged(object sender, SelectionChangedEventArgs e) {
            CalculateItemVatPrice();
        }

        private void CalculateItemVatPrice() {
            try {
                txt_totalPriceWithVat.Text = ((double)txt_count.Value * (double)(txt_pcsPrice.Value + txt_pcsPrice.Value * (double)((BasicVatList)cb_vat.SelectedItem).Value)).ToString(itemVatPriceFormat) + " " + ((BasicCurrencyList)cb_totalCurrency.SelectedItem).Name;
                btn_insert.IsEnabled = true;
            } catch { txt_totalPriceWithVat.Text = null; btn_insert.IsEnabled = false; }
            txt_totalPrice.Text = DocumentItemList.Sum(a => a.TotalPriceWithVat).ToString(documentVatPriceFormat) + ((cb_totalCurrency.SelectedItem != null) ? " " + ((BasicCurrencyList)cb_totalCurrency.SelectedItem).Name : "");
        }

        /// <summary>
        /// Standartized method for select one record only. This is full automatic, not needed
        /// manual work This is on page With Sublist ('View' and 'Form' Types) without 'Setting'
        /// Type (Name=Setting and Tag=Setting in XAML part) this method is for global working page
        /// its local control From XAML
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">     
        /// The <see cref="SelectionChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void dgSubListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgSubListView.SelectedItems.Count > 0) {
                btn_delete.IsEnabled = true;
            }
            else { btn_delete.IsEnabled = false; }
        }

        /// <summary>
        /// Standartized method for Direct Insert SubRecord to SubListView Manual work needed, set
        /// correct data set for SubRecord This is on page With Sublist ('View' and 'Form' Types)
        /// without 'Setting' Type (Name=Setting and Tag=Setting in XAML part) this method is for
        /// global working page its local control From XAML
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">     
        /// The <see cref="RoutedEventArgs"/> instance containing the event data.
        /// </param>
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

        /// <summary>
        /// Standartized method for Direct Delete SubRecord to SubListView Manual work needed, set
        /// correct data set for SubRecord This is on page With Sublist ('View' and 'Form' Types)
        /// without 'Setting' Type (Name=Setting and Tag=Setting in XAML part) this method is for
        /// global working page its local control From XAML
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">     
        /// The <see cref="RoutedEventArgs"/> instance containing the event data.
        /// </param>
        private void BtnItemDelete_Click(object sender, RoutedEventArgs e) {
            DocumentItemList.RemoveAt(DgSubListView.SelectedIndex);
            DgSubListView.ItemsSource = DocumentItemList;
            DgSubListView.Items.Refresh();
            txt_totalPrice.Text = DocumentItemList.Sum(a => a.TotalPriceWithVat).ToString(documentVatPriceFormat) + ((cb_totalCurrency.SelectedItem != null) ? " " + ((BasicCurrencyList)cb_totalCurrency.SelectedItem).Name : "");
        }

        /// <summary>
        /// Standartized Maximal Simle Code with Reaction and Fill input After ParentComboboxSelection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void NotesChanged(object sender, SelectionChangedEventArgs e) { if (cb_notes.SelectedItem != null) { txt_description.Text = ((BusinessNotesList)cb_notes.SelectedItem).Description; cb_notes.SelectedItem = null; } }

        /// <summary>
        /// Standartized Method for Clear SubRecord Input Fields with custom Dataset For Correct
        /// Using must be Fields changed for used dataset
        /// </summary>
        private void ClearItemsFields() {
            txt_partNumber.Text = txt_name.Text = cb_unit.Text = txt_totalPriceWithVat.Text = null;
            txt_count.Value = txt_pcsPrice.Value = null;
            cb_unit.SelectedItem = cb_vat.SelectedItem = null;
            lv_partNumberSearchResults.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Standartized Method for Load All SubData which is needed for Working with SubRecord For
        /// Correct Using must be changed for actual datasets
        /// </summary>
        private async void SetSubListsNonActiveOnNewItem(bool newItem) {
            if (newItem) {
                cb_totalCurrency.ItemsSource = CurrencyList.Where(a => a.Active).ToList();
                AddressList = (await CommunicationManager.GetApiRequest<List<BusinessAddressList>>(ApiUrls.EasyITCenterBusinessAddressList, null, App.UserData.Authentification.Token)).Where(a => a.Active).ToList();
                ItemList = (await CommunicationManager.GetApiRequest<List<BasicItemList>>(ApiUrls.EasyITCenterBasicItemList, null, App.UserData.Authentification.Token)).Where(a => a.Active).ToList();
                cb_notes.ItemsSource = NotesList.Where(a => a.Active).ToList(); cb_unit.ItemsSource = UnitList.Where(a => a.Active).ToList(); cb_vat.ItemsSource = VatList.Where(a => a.Active).ToList();
            }
            else {
                cb_totalCurrency.ItemsSource = CurrencyList;
                AddressList = await CommunicationManager.GetApiRequest<List<BusinessAddressList>>(ApiUrls.EasyITCenterBusinessAddressList, null, App.UserData.Authentification.Token);
                ItemList = await CommunicationManager.GetApiRequest<List<BasicItemList>>(ApiUrls.EasyITCenterBasicItemList, null, App.UserData.Authentification.Token);
                cb_notes.ItemsSource = NotesList; cb_unit.ItemsSource = UnitList; cb_vat.ItemsSource = VatList;
            }
        }
    }
}