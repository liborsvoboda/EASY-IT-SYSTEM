using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using HelixToolkit.Wpf;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace EasyITSystemCenter.Pages {

    public partial class BasicItemListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static ExtendedItemList selectedRecord = new ExtendedItemList();

        private List<BasicUnitList> UnitList = new List<BasicUnitList>();
        private List<BasicCurrencyList> CurrencyList = new List<BasicCurrencyList>();
        private List<BasicVatList> VatList = new List<BasicVatList>();
        private List<BasicAttachmentList> AttachmentList = new List<BasicAttachmentList>();

        private string itemVatPriceFormat = "N2";

        public BasicItemListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            _ = FormOperations.TranslateFormFields(ListForm);

            LoadParameters();
            _ = LoadDataList();
            SetRecord(false);
        }

        private async void LoadParameters() {
            itemVatPriceFormat = await DataOperations.ParameterCheck("ItemVatPriceFormat");
        }

        //change datasource
        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;

            try {
                cb_unit.ItemsSource = UnitList = await CommunicationManager.GetApiRequest<List<BasicUnitList>>(ApiUrls.EasyITCenterBasicUnitList, null, App.UserData.Authentification.Token);
                cb_currency.ItemsSource = CurrencyList = await CommunicationManager.GetApiRequest<List<BasicCurrencyList>>(ApiUrls.EasyITCenterBasicCurrencyList, null, App.UserData.Authentification.Token);
                cb_vat.ItemsSource = VatList = await CommunicationManager.GetApiRequest<List<BasicVatList>>(ApiUrls.EasyITCenterBasicVatList, null, App.UserData.Authentification.Token);

                List<BasicItemList> ExtendedItemList = new List<BasicItemList>(); List<ExtendedItemList> extendedItemList = new List<ExtendedItemList>();
                if (MainWindow.serviceRunning)
                    DgListView.ItemsSource = ExtendedItemList = await CommunicationManager.GetApiRequest<List<BasicItemList>>(ApiUrls.EasyITCenterBasicItemList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                ExtendedItemList.ForEach(record => {
                    ExtendedItemList item = new ExtendedItemList(record) {
                        //Id = record.Id,
                        //PartNumber = record.PartNumber,
                        //Name = record.Name,
                        //Description = record.Description,
                        //Unit = record.Unit,
                        //Price = record.Price,
                        //VatId = record.VatId,
                        //CurrencyId = record.CurrencyId,
                        //UserId = record.UserId,
                        //Active = record.Active,
                        //TimeStamp = record.TimeStamp,
                        Vat = VatList.Where(a => a.Id == record.VatId).Select(a => a.Value).FirstOrDefault().ToString(),
                        Currency = CurrencyList.Where(a => a.Id == record.CurrencyId).Select(a => a.Name).FirstOrDefault().ToString()
                    }; extendedItemList.Add(item);
                });

                DgListView.ItemsSource = extendedItemList;
                DgListView.Items.Refresh();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        // set translate columns in listView
        private void DgListView_Translate(object sender, EventArgs ex) {
            ((DataGrid)sender).Columns.ToList().ForEach(e => {
                string headername = e.Header.ToString();
                if (headername == "PartNumber") e.Header = Resources["identifier"].ToString();
                else if (headername == "Value") e.Header = Resources["fname"].ToString();
                else if (headername == "Description") e.Header = Resources["description"].ToString();
                else if (headername == "Unit") { e.Header = Resources["unit"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; }
                else if (headername == "Price") { e.Header = Resources["price"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; }
                else if (headername == "Currency") { e.Header = Resources["currency"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 4; }
                else if (headername == "Vat") { e.Header = Resources["vat"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 3; }
                else if (headername == "Active") { e.Header = Resources["active"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 2; }
                else if (headername == "TimeStamp") { e.Header = Resources["timestamp"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                else if (headername == "Id") e.DisplayIndex = 0;
                else if (headername == "UserId") e.Visibility = Visibility.Hidden;
                else if (headername == "CurrencyId") e.Visibility = Visibility.Hidden;
                else if (headername == "VatId") e.Visibility = Visibility.Hidden;
            });
        }

        //change filter fields
        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    ExtendedItemList user = e as ExtendedItemList;
                    return user.PartNumber.ToLower().Contains(filter.ToLower())
                    || user.Name.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.Description) && user.Description.ToLower().Contains(filter.ToLower())
                    || user.Unit.ToLower().Contains(filter.ToLower());
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new ExtendedItemList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (ExtendedItemList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (ExtendedItemList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterBasicItemList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                _ = LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (ExtendedItemList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) {
                selectedRecord = (ExtendedItemList)DgListView.SelectedItem;
            }
            else {
                selectedRecord = new ExtendedItemList();
            }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        //change dataset save method
        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                MainWindow.progressRing = Visibility.Visible;
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                selectedRecord.PartNumber = txt_partNumber.Text;
                selectedRecord.Name = txt_name.Text;
                selectedRecord.Description = txt_description.Text;
                selectedRecord.Unit = ((BasicUnitList)cb_unit.SelectedItem).Name;
                selectedRecord.Price = (decimal)txt_price.Value;
                selectedRecord.CurrencyId = ((BasicCurrencyList)cb_currency.SelectedItem).Id;
                selectedRecord.VatId = ((BasicVatList)cb_vat.SelectedItem).Id;
                selectedRecord.Active = (bool)chb_active.IsChecked;
                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                DataOperations.NullSetInExtensionFields(ref selectedRecord);
                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterBasicItemList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterBasicItemList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    //Replace Attachments
                    AttachmentList.ForEach(attachment => { attachment.ParentId = dBResult.InsertedId; attachment.Id = 0; });
                    dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterBasicAttachmentList, "ITEM/" + selectedRecord.Id, App.UserData.Authentification.Token);
                    json = JsonConvert.SerializeObject(AttachmentList); httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterBasicAttachmentList, httpContent, null, App.UserData.Authentification.Token);
                    if (dBResult.RecordCount != AttachmentList.Count()) { await MainWindow.ShowMessageOnMainWindow(true, Resources["attachmentDBError"].ToString() + Environment.NewLine + dBResult.ErrorMessage); }
                    else {
                        selectedRecord = new ExtendedItemList();
                        await LoadDataList();
                        SetRecord(null);
                    }
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); } finally { MainWindow.progressRing = Visibility.Hidden; }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (ExtendedItemList)DgListView.SelectedItem : new ExtendedItemList();
            SetRecord(false);
        }

        //change dataset prepare for working
        private async void SetRecord(bool? showForm = null, bool copy = false) {
            SetSubListsNonActiveOnNewItem(selectedRecord.Id == 0);
            tv_attachments.Items.Clear(); AttachmentList.Clear();

            txt_id.Value = (copy) ? 0 : selectedRecord.Id;
            txt_partNumber.Text = selectedRecord.PartNumber;
            txt_name.Text = selectedRecord.Name;
            txt_description.Text = selectedRecord.Description;
            cb_unit.Text = selectedRecord.Unit;
            txt_price.Value = (double)selectedRecord.Price;
            cb_currency.Text = CurrencyList.Where(x => x.Id == selectedRecord.CurrencyId).Select(a => a.Name).FirstOrDefault();
            cb_vat.Text = VatList.Where(x => x.Id == selectedRecord.VatId).Select(a => a.Name).FirstOrDefault();
            chb_active.IsChecked = (selectedRecord.Id == 0) ? bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key == "beh_activeNewInputDefault").Value) : selectedRecord.Active;

            if (selectedRecord.Id > 0) {
                tv_attachments.Items.Clear();
                AttachmentList = await CommunicationManager.GetApiRequest<List<BasicAttachmentList>>(ApiUrls.EasyITCenterBasicAttachmentList, "ITEM/" + selectedRecord.Id, App.UserData.Authentification.Token);
                AttachmentList.ForEach(attachment => {
                    TreeViewItem attachmentItem = new TreeViewItem() { Header = attachment.FileName, Tag = attachment.FileName.Split('.').Last(), HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Center };
                    attachmentItem.Selected += AttachmentItem_Selected;
                    tv_attachments.Items.Add(attachmentItem);
                });
            }

            if (showForm != null && showForm == true) {
                HideAttachmentSection();
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
                if (CurrencyList.Where(a => a.Default).Count() == 1 && cb_currency.Text == null) { cb_currency.Text = CurrencyList.First(a => a.Default).Name; }
                if (UnitList.Where(a => a.Default).Count() == 1 && cb_unit.Text == null) { cb_unit.Text = UnitList.First(a => a.Default).Name; }
                if (VatList.Where(a => a.Default).Count() == 1 && cb_vat.Text == null) { cb_vat.Text = VatList.First(a => a.Default).Name; }
            }
            else {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
            }
        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e) {
            try {
                OpenFileDialog dlg = new OpenFileDialog { DefaultExt = ".*", Filter = "Supported files |*.*;", Title = Resources["fileOpenDescription"].ToString() };
                if (dlg.ShowDialog() == true) {
                    if (AttachmentList.Where(a => a.FileName == dlg.SafeFileName).Count() == 0) {
                        AttachmentList.Add(new BasicAttachmentList() {
                            ParentId = 0,
                            ParentType = "ITEM",
                            FileName = dlg.SafeFileName,
                            Attachment = File.ReadAllBytes(dlg.FileName),
                            UserId = App.UserData.Authentification.Id,
                            TimeStamp = DateTimeOffset.Now.DateTime
                        });
                    }
                    TreeViewItem attachmentItem = new TreeViewItem() { Header = dlg.SafeFileName, Tag = dlg.FileName.Split('.').Last(), HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Center };
                    attachmentItem.Selected += AttachmentItem_Selected;
                    tv_attachments.Items.Add(attachmentItem);
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private async void AttachmentItem_Selected(object sender, RoutedEventArgs e) {
            try {
                btn_delete.IsEnabled = true; btn_export.IsEnabled = true;
                viewPort3d.IsEnabled = webViewer.IsEnabled = false;
                viewPort3d.Visibility = webViewer.Visibility = Visibility.Hidden;

                TreeViewItem attachmentItem = (TreeViewItem)sender;
                string filePath = Path.Combine(App.appRuntimeData.tempFolder, attachmentItem.Header.ToString());
                FileOperations.ByteArrayToFile(filePath, AttachmentList.Where(a => a.FileName == attachmentItem.Header.ToString()).First().Attachment);
                switch (((TreeViewItem)sender).Tag.ToString().ToLower()) {
                    case "stl":
                        viewPort3d.IsEnabled = true; viewPort3d.Visibility = Visibility.Visible; //viewPort3d.Viewport.Print("");
                        ModelVisual3D device3D = new ModelVisual3D { Content = await Display3d(filePath) };
                        viewPort3d.Children.Add(device3D); viewPort3d.ZoomExtents();
                        break;

                    default:
                        webViewer.IsEnabled = true; webViewer.Visibility = Visibility.Visible;
                        webViewer.Source = new Uri(filePath);
                        break;
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void HideAttachmentSection() {
            btn_delete.IsEnabled = true; btn_export.IsEnabled = true;
            viewPort3d.IsEnabled = webViewer.IsEnabled = false;
            viewPort3d.Visibility = webViewer.Visibility = Visibility.Hidden;
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e) {
            try {
                string fileName = ((TreeViewItem)tv_attachments.SelectedValue).Header.ToString();
                SaveFileDialog dlg = new SaveFileDialog { DefaultExt = ".*", Filter = "Supported files |*.*;", Title = Resources["fileOpenDescription"].ToString(), FileName = fileName };
                if (dlg.ShowDialog() == true) { FileOperations.ByteArrayToFile(dlg.FileName, AttachmentList.Where(a => a.FileName == fileName).First().Attachment); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private async Task<Model3D> Display3d(string modelPath) {
            Model3D device = null;
            try {
                viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);
                ModelImporter import = new ModelImporter();
                import.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Color.FromRgb(255, 255, 255)));
                device = import.Load(modelPath);
            } catch (Exception ex) { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + ex.Message + Environment.NewLine + ex.StackTrace); }
            return device;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            AttachmentList.Remove(AttachmentList.Where(a => a.FileName == ((TreeViewItem)tv_attachments.SelectedValue).Header.ToString()).First());
            tv_attachments.Items.Remove(tv_attachments.SelectedItem);
            if (tv_attachments.SelectedItem == null) btn_delete.IsEnabled = btn_export.IsEnabled = false;
        }

        private void PriceVatChanged(object sender, SelectionChangedEventArgs e) => PriceChanged();

        private void PriceCurrencyChanged(object sender, SelectionChangedEventArgs e) => PriceChanged();

        private void PriceValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e) => PriceChanged();

        private void PriceChanged() {
            try {
                if (dataViewSupport.FormShown) {
                    val_priceWithVat.Content = (txt_price.Value + txt_price.Value * double.Parse(((BasicVatList)cb_vat.SelectedItem).Value.ToString(itemVatPriceFormat))) + " " + ((BasicCurrencyList)cb_currency.SelectedItem).Name;
                }
            } catch { val_priceWithVat.Content = string.Empty; }
        }

        private void SetSubListsNonActiveOnNewItem(bool newItem) {
            if (newItem) {
                cb_unit.ItemsSource = UnitList.Where(a => a.Active).ToList();
                cb_currency.ItemsSource = CurrencyList.Where(a => a.Active).ToList();
                cb_vat.ItemsSource = VatList.Where(a => a.Active).ToList();
            }
            else { cb_unit.ItemsSource = UnitList; cb_currency.ItemsSource = CurrencyList; cb_vat.ItemsSource = VatList; }
        }
    }
}