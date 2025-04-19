using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EasyITSystemCenter.Pages {

    public partial class ServerSettingListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static SolutionSchedulerList selectedRecord = new SolutionSchedulerList();

        private List<SolutionMixedEnumList> serverConfigGroups = new List<SolutionMixedEnumList>();
        private List<SolutionMixedEnumList> serverLanguages = new List<SolutionMixedEnumList>();
        private List<SolutionMixedEnumList> inheritedTokenEncryptType = new List<SolutionMixedEnumList>();

        
        public ServerSettingListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            try {
                _ = FormOperations.TranslateFormFields(ConfigForm);
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            LoadDataList();
        }


        public async void LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {
                serverLanguages = await DBOperations.LoadInheritedDataList("ServerLanguageType");
                serverConfigGroups = await DBOperations.LoadInheritedDataList("SrvConfigType");
                inheritedTokenEncryptType = await DBOperations.LoadInheritedDataList("TokenEncryptType");
                App.ServerSetting = await CommunicationManager.GetApiRequest<List<ServerServerSettingList>>(ApiUrls.EasyITCenterServerSettingList, null, null);

                serverLanguages.ForEach(async srvLanguage => { srvLanguage.Translation = await DBOperations.DBTranslation(srvLanguage.Name); });
                serverConfigGroups.ForEach(async tasktype => { tasktype.Translation = await DBOperations.DBTranslation(tasktype.Name); });
                inheritedTokenEncryptType.ForEach(async tasktype => { tasktype.Translation = await DBOperations.DBTranslation(tasktype.Name); });

                App.ServerSetting.ForEach(async serverConfig => {
                    serverConfig.KeyTranslation = await DBOperations.DBTranslation(serverConfig.Key);
                    serverConfig.GroupNameTranslation = serverConfigGroups.First(a => a.Name == serverConfig.InheritedSrvConfigType).Translation;
                });

                //Generate Menu Panel
                TabMenuList.Items.Clear();
                serverConfigGroups.ForEach(async group => {
                    try {
                        WrapPanel tabMenuPanel = new WrapPanel() { Name = "wp_" + group.Name, Width = 850, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };

                        TabItem tabMenu = new TabItem() { Name = group.Name, Header = group.Translation, Content = tabMenuPanel };
                        if (!string.IsNullOrWhiteSpace(group.Description)) { tabMenu.ToolTip = group.Description; }
                        TabMenuList.Items.Add(tabMenu);
                    } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
                });

                //Generate Config Fields
                App.ServerSetting.ForEach(async configuration => {
                    try {
                        //Get Parent Object
                        WrapPanel targetGrid = ((WrapPanel)TabMenuList.Items.Cast<TabItem>().ToList().Where(a => a.Name == configuration.InheritedSrvConfigType).First().Content);
                        DataGrid panel = new DataGrid();

                        //Insert Label
                        Label label = new Label() {
                            Name = "lbl_" + configuration.Key,
                            Content = await DBOperations.DBTranslation(configuration.Key),
                            Width = 350,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Center,
                            FontSize = 16,
                            Foreground = new SolidColorBrush(Colors.White),
                            ToolTip = (!string.IsNullOrWhiteSpace(configuration.Description)) ? configuration.Description : null
                        };
                        targetGrid.Children.Add(label);

                        //Insert Input By Defined Type
                        switch (configuration.Type.ToLower()) {
                            case "bit":
                                CheckBox check = new CheckBox() { Name = configuration.Key, IsChecked = bool.Parse(configuration.Value), Width = 300, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };
                                targetGrid.Children.Add(check);
                                break;

                            case "int":
                                NumericUpDown numericUpDown = new NumericUpDown() { Name = configuration.Key, Value = int.Parse(configuration.Value), Width = 300, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };
                                targetGrid.Children.Add(numericUpDown);
                                break;

                            case "string":
                                TextBox textbox = new TextBox() { Name = configuration.Key, Text = configuration.Value, Width = 300, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };
                                targetGrid.Children.Add(textbox);
                                break;

                            case "list":
                                ComboBox comboBox = new ComboBox() { Name = configuration.Key, Text = configuration.Value, Width = 300, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };

                                //All Uses Lists Definitions
                                if (configuration.Key.ToLower() == "ServiceServerLanguage".ToLower()) { comboBox.ItemsSource = serverLanguages; comboBox.DisplayMemberPath = "Translation"; comboBox.SelectedValuePath = "Name"; comboBox.SelectedValue = configuration.Value; }
                                if (configuration.Key.ToLower() == "ConfigTokenEncryption".ToLower()) { comboBox.ItemsSource = inheritedTokenEncryptType; comboBox.DisplayMemberPath = "Translation"; comboBox.SelectedValuePath = "Name"; comboBox.SelectedValue = configuration.Value; }
                                targetGrid.Children.Add(comboBox);
                                break;
                        }
                        bool buttonInserted = false;

                        //Add Unique Functionality
                        switch (configuration.Key.ToLower()) {
                            case "emailersmtpserveraddress":
                                Button button = new Button() { Name = "btn_" + configuration.Key, Margin = new Thickness(5), Content = await DBOperations.DBTranslation("SendTestEmail"), Width = 150, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };
                                button.Click += BtnSendTestEmail_Click;
                                targetGrid.Children.Add(button);
                                buttonInserted = true;
                                break;
                        }

                        //Add Link Button
                        if (!string.IsNullOrWhiteSpace(configuration.Link)) {
                            Button button = new Button() { Name = "link_" + configuration.Key, Content = await DBOperations.DBTranslation("OpenLink"), Width = 150, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center };
                            button.Tag = configuration.Link; button.Click += BtnOpenLink_Click;
                            targetGrid.Children.Add(button);
                            buttonInserted = true;
                        }

                        if (!buttonInserted) { targetGrid.Children.Add(new Label() { Width = 200 }); };
                    } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden;
        }

        private bool PrepareConfigurationForSave() {
            try {
                App.ServerSetting.ForEach(configuration => {
                    try {
                        WrapPanel targetGrid = ((WrapPanel)TabMenuList.Items.Cast<TabItem>().ToList().Where(a => a.Name == configuration.InheritedSrvConfigType).First().Content);

                        targetGrid.Children.OfType<CheckBox>().ToList().ForEach(checkBox => {
                            App.ServerSetting.First(a => a.Key == checkBox.Name).Value = checkBox.IsChecked.ToString();
                            App.ServerSetting.First(a => a.Key == checkBox.Name).TimeStamp = DateTimeOffset.Now.DateTime;
                        });
                        targetGrid.Children.OfType<TextBox>().ToList().ForEach(textbox => {
                            App.ServerSetting.First(a => a.Key == textbox.Name).Value = textbox.Text;
                            App.ServerSetting.First(a => a.Key == textbox.Name).TimeStamp = DateTimeOffset.Now.DateTime;
                        });
                        targetGrid.Children.OfType<NumericUpDown>().ToList().ForEach(numeric => {
                            App.ServerSetting.First(a => a.Key == numeric.Name).Value = numeric.Value.ToString();
                            App.ServerSetting.First(a => a.Key == numeric.Name).TimeStamp = DateTimeOffset.Now.DateTime;
                        });
                        targetGrid.Children.OfType<ComboBox>().ToList().ForEach(combobox => {
                            App.ServerSetting.First(a => a.Key == combobox.Name).Value = combobox.SelectedValue.ToString();
                            App.ServerSetting.First(a => a.Key == combobox.Name).TimeStamp = DateTimeOffset.Now.DateTime;
                        });
                    } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
                });
                return true;
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            return false;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            MainWindow.ProgressRing = Visibility.Visible;
            if (PrepareConfigurationForSave()) {
                string json = JsonConvert.SerializeObject(App.ServerSetting);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                DBResultMessage dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterServerSettingList, httpContent, null, App.UserData.Authentification.Token);
                MainWindow.ProgressRing = Visibility.Hidden;

                if (dBResult.RecordCount > 0) { await MainWindow.ShowMessageOnMainWindow(false, Resources["savingSuccessfull"].ToString()); }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            }
            else {
                MainWindow.ProgressRing = Visibility.Hidden;
                await MainWindow.ShowMessageOnMainWindow(true, await DBOperations.DBTranslation("ConfigurationReadError"));
            }
        }

        private void BtnOpenLink_Click(object sender, RoutedEventArgs e) => Process.Start(App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_apiAddress").Value + ((Button)sender).Tag.ToString());

        private async void BtnSendTestEmail_Click(object sender, RoutedEventArgs e) {
            try {
                SystemOperations.SendMailWithServerSetting(await DBOperations.DBTranslation("TestEmailFrom") + " " + App.ServerSetting.FirstOrDefault(a => a.Key == "SpecialServerServiceName").Value);
                DBResultMessage dBResultMessage = await CommunicationManager.GetApiRequest<DBResultMessage>(ApiUrls.ServerApi, "EmailServices/GetMessenger/" + await DBOperations.DBTranslation("TestEmailFrom") + " " + App.ServerSetting.FirstOrDefault(a => a.Key == "SpecialServerServiceName").Value + " API", App.UserData.Authentification.Token);
                { await MainWindow.ShowMessageOnMainWindow(false, dBResultMessage.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        //private void BtnExport_Click(object sender, RoutedEventArgs e) {
        //    try {
        //        SaveFileDialog dlg = new SaveFileDialog { DefaultExt = ".json", Filter = "Config file |*.json", Title = Resources["fileOpenDescription"].ToString() };
        //        if (dlg.ShowDialog() == true) { FileOperations.WriteToFile(dlg.FileName, DataOperations.ConvertDataSetToJson()); }
        //    } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        //}

        private async void BtnRestartServer_Click(object sender, RoutedEventArgs e) {
            DBResultMessage dBResultMessage = await CommunicationManager.GetApiRequest<DBResultMessage>(ApiUrls.ServerApi, "ManagementServices/CoreServerRestart", App.UserData.Authentification.Token);
            {
                await MainWindow.ShowMessageOnMainWindow(false, dBResultMessage.ErrorMessage);
            }
        }
    }
}