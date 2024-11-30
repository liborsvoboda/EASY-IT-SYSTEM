using Dragablz;
using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalGenerators;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using EasyITSystemCenter.Pages;
using EasyITSystemCenter.Properties;
using EasyITSystemCenter.SystemHelper;
using EasyITSystemCenter.SystemStructure;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace EasyITSystemCenter {
    
    public partial class MainWindow : MetroWindow {
        /// <summary>
        /// MainControls Screen Variables
        /// </summary>

        #region Definitions

        public static DateTimeOffset lastUserAction = DateTimeOffset.UtcNow.AddSeconds(int.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key == "beh_timeToEnable").Value));
        public readonly Timer AppSystemTimer = new Timer() { Enabled = false, Interval = 1 };
        

        //Central Variables Declarations
        public static bool dataGridSelected, dgIdSetted, dgRefresh, serviceRunning, userLogged, updateChecked, runReleaseMode, operationRunning, serverLoggerSource, multiSameTabsEnabled = false;
        public static int dataGridSelectedId, downloadShow, downloadStatus = 0, vncProcessId = 0;
        public static string serviceStatus;
        public static Visibility progressRing, showSystemLogger = Visibility.Hidden;
        public SolidColorBrush vncRunning = Brushes.Red;
        public Process vncProcess;


        //Variables Centralisation Hanlers for Working Only Over MainWindow.
        //Its Central View For All Statuses Of System
        public static event EventHandler DataGridSelectedChanged, DataGridSelectedIdListIndicatorChanged, DgRefreshChanged, ServiceStatusChanged, ServiceRunningChanged, DownloadStatusChanged,
            DownloadShowChanged, ProgressRingChanged, UserLoggedChanged, VncRunningChanged, SystemLoggerChanged, RunReleaseModeChanged, OperationRunningChanged,
            ServerLoggerSourceChanged, ShowSystemLoggerChanged, MultiSameTabsEnabledChanged = delegate { };

        #endregion Definitions

        #region MainWindow Controller Statuses


        /// <summary>
        /// Enable/Disable MultiSameTabs Forms
        /// </summary>
        public bool MultiSameTabsEnabled {
            get => multiSameTabsEnabled;
            set {
                multiSameTabsEnabled = value;
                MultiSameTabsEnabledChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// System Logger Source Status and Controller This is status of Settings System Logger Source
        /// </summary>
        /// <value><c>true</c> if [server logger source]; otherwise, <c>false</c>.</value>
        public bool ServerLoggerSource {
            get => serverLoggerSource;
            set {
                serverLoggerSource = value;
                OWinApiMiddleWare.SystemLoggerWebSocketMonitorOnOff();
                ServerLoggerSourceChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// System Logger Activator by Setted Parametr Can be setted for All Apps or Every User individually
        /// </summary>
        /// <value><c>true</c> if [show system logger]; otherwise, <c>false</c>.</value>
        public static Visibility ShowSystemLogger {
            get => showSystemLogger;
            set {
                showSystemLogger = value;
                try {
                    ((MainWindow)Application.Current.MainWindow).btn_showOnlineLogger.Width = 0;
                    if (App.SystemLoggerWebSocketMonitor.ShowSystemLogger) { ((MainWindow)Application.Current.MainWindow).btn_showOnlineLogger.Width = 30; }
                } catch { }
                ShowSystemLoggerChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Indicator for mark operation status for any programmatic operations
        /// </summary>
        /// <value><c>true</c> if [operation running]; otherwise, <c>false</c>.</value>
        public bool OperationRunning {
            get => operationRunning;
            set {
                operationRunning = value;
                OperationRunningChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        ///VNC Server Running Indicator
        public bool RunReleaseMode {
            get => runReleaseMode;
            set {
                runReleaseMode = value;
                RunReleaseModeChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        ///VNC Server Running Indicator
        public SolidColorBrush VncRunning {
            get => vncRunning;
            set {
                vncRunning = value;
                ip_rdpServer.Foreground = (vncRunning == Brushes.Green) ? Brushes.Green : Brushes.Red;
                VncRunningChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// System Online Logger for EASY developing
        /// </summary>
        public string SystemLogger {
            get => rt_SystemLogger.Text;
            set {
                rt_SystemLogger.Text = DateTime.Now + Environment.NewLine + value + Environment.NewLine + (rt_SystemLogger.Text.Length > 1024 * 512 ? rt_SystemLogger.Text.Substring(1, 1000 * 512) : rt_SystemLogger.Text);
                if (!ServerLoggerSource) btn_showOnlineLogger.Background = Brushes.Red;

                if (App.appRuntimeData.AppSystemStatuses == SystemStatuses.Release.ToString() || App.appRuntimeData.AppSystemStatuses == SystemStatuses.DebugWithSystemLogger.ToString()) { i_onlineLoggerIcon.Spin = true; } else { i_onlineLoggerIcon.Spin = false; }

                SystemLoggerChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// User Logged Status
        /// </summary>
        public static bool UserLogged {
            get => userLogged;
            set {
                userLogged = value;
                UserLoggedChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Service Status description
        /// </summary>
        public static bool ServiceRunning {
            get => serviceRunning;
            set {
                serviceRunning = value;
                ServiceRunningChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Indicator for enable Refresh Button Indicator
        /// </summary>
        public static bool DgRefresh {
            get => dgRefresh;
            set {
                dgRefresh = value;
                DgRefreshChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Indicator for Enable New DataGrid Button
        /// </summary>
        public static bool DataGridSelected {
            get => dataGridSelected;
            set {
                dataGridSelected = value;
                DataGridSelectedChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// <see cref="DataGrid"/> have selected record indicator
        /// </summary>
        public static bool DataGridSelectedIdListIndicator {
            get => dgIdSetted;
            set {
                dgIdSetted = value;
                DataGridSelectedIdListIndicatorChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Service Status public Variable
        /// </summary>
        public static string ServiceStatus {
            get => serviceStatus;
            set {
                serviceStatus = value;
                ServiceStatusChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Downloading of update status variable
        /// </summary>
        public static int DownloadStatus {
            get => downloadStatus;
            set {
                downloadStatus = value;
                DownloadStatusChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Indicator for show Downloading area
        /// </summary>
        public static int DownloadShow {
            get => downloadShow;
            set {
                downloadShow = value;
                DownloadShowChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// ProgressRing Visibility indicator
        /// </summary>
        public static Visibility ProgressRing {
            get => progressRing;
            set {
                progressRing = value;
                ProgressRingChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        #endregion MainWindow Controller Statuses

        /*

        #region Help Added

        private DependencyObject CurrentHelpDO { get; set; }
        private Popup CurrentHelpPopup { get; set; }
        private bool HelpActive { get; set; }
        private MouseEventHandler _helpHandler = null;
        private static bool isHelpMode = false;

        #endregion Help Added

        */

        /// <summary>
        /// System Core AND ALL shared functionalities
        /// </summary>
        public MainWindow() {
            try {

                InitializeComponent();
                SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);
                Title = Resources["appName"].ToString();

                //https://github.com/lepoco/wpfui
                //ApplicationThemeManager.Apply(this);




                //Startup Setup MainWindow
                ToolTipService.ShowDurationProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(int.MaxValue));
                XamlMainWindow.Height = Settings.Default.Height; XamlMainWindow.Width = Settings.Default.Width;
                XamlMainWindow.Left = Settings.Default.Left; XamlMainWindow.Top = Settings.Default.Top;

                Topmost = bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key == "beh_topMost").Value);
                MultiSameTabsEnabled = bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_multiSameTabs").Value);
                mi_appearance.Header = Resources["appearance"].ToString(); mi_color.Header = Resources["color"].ToString(); btn_about.ToolTip = Resources["about"].ToString();

                //Offline Setting Menu
                ClientSettingsPage.Header = Resources["clientSettings"].ToString();

                //right panel
                tb_search.SetValue(TextBoxHelper.WatermarkProperty, Resources["search"].ToString()); mi_logout.Header = Resources["logon"].ToString();

                //grid panel
                mi_reload.Header = Resources["reload"].ToString(); mi_new.Header = Resources["new"].ToString();
                mi_edit.Header = Resources["edit"].ToString(); mi_copy.Header = Resources["copy"].ToString(); mi_delete.Header = Resources["delete"].ToString();

                cb_filter.SelectedIndex = 0;

                //Core Startup / Closing handlers
                Loaded += MainWindow_Loaded;
                KeyDown += MainWindow_KeyDown;
                Closing += MainWindow_Closing;
                
                //User Activity Handlers
                PreviewMouseDown += MainWindow_MouseLeave;
                PreviewKeyDown += MainWindow_PreviewKeyDown;
                PreviewMouseMove += MainWindow_PreviewMouseMove;
                rt_SystemLogger.Text = Resources["welcomeLogger"].ToString();
                ContentRendered += MainWindow_ContentRendered;
                
            } catch (Exception ex) { App.ApplicationLogging(ex); }
        }
        private void MainWindow_StateChanged(object sender, EventArgs e) => HandleWinMaximized();
        private void HandleWinMaximized() { 
            if (WindowState == WindowState.Maximized) {
                this.MinWidth = SystemParameters.FullPrimaryScreenWidth;
                this.MinHeight = SystemParameters.FullPrimaryScreenHeight;
            }
        }

        private void MainWindow_ContentRendered(object sender, EventArgs e) => ShowLoginDialog();
        internal void MainWindow_MouseLeave(object sender, MouseEventArgs e) => SetLastUserAction();
        internal void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e) => SetLastUserAction();
        private void MainWindow_PreviewMouseMove(object sender, MouseEventArgs e) => SetLastUserAction();

        internal DateTimeOffset SetLastUserAction() {
            //ScreenSaver
            if (bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key == "beh_disableOnActivity").Value) && this.FindChild<ScreenSaverPage>("") != null) {
                try {
                    SystemTabs existingTab = ((SystemWindowDataModel)DataContext).TabContents.ToList().Where(a => a.Tag?.ToString().ToLower() == "activesystemsaver").FirstOrDefault();
                    var result = (SystemWindowDataModel)this.DataContext;
                    result.TabContents.Remove(existingTab);
                } catch (Exception ex) { App.ApplicationLogging(ex); }
            }
            return lastUserAction = DateTimeOffset.UtcNow;
        }


        /// <summary>
        /// Set System Module Content
        /// </summary>
        private async Task<bool> SetSystemModuleListPanel() {
            try {
                List<SolutionMixedEnumList> mixedEnumTypesList = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, "ByGroup/SystemModules", App.UserData.Authentification.Token);
                App.SystemSvgIconList = await CommunicationManager.GetApiRequest<List<SystemSvgIconList>>(ApiUrls.EasyITCenterSystemSvgIconList, null, App.UserData.Authentification.Token);

                systemModuleList.Items.Clear();
                mixedEnumTypesList.ForEach(async panelType => {
                    try {
                        WrapPanel tabMenuPanel = new WrapPanel() { Name = "wp_" + panelType.Id.ToString(), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                        TabItem tabMenu = new TabItem() { Name = Regex.Replace(panelType.Name, @"[^a-zA-Z]", "_"), Tag = Regex.Replace(panelType.Name, @"[^a-zA-Z]", "_"), Header = await DBOperations.DBTranslation(panelType.Name), Content = tabMenuPanel };
                        systemModuleList.Items.Add(tabMenu);
                    } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
                });

                foreach (SystemModuleList panel in App.SystemModuleList) {
                    var toolPanel = new Tile() {
                        Tag = panel.Id.ToString(), Name = Regex.Replace(panel.Name, @"[^a-zA-Z]", "_"),
                        Uid = Regex.Replace(panel.ModuleType, @"[^a-zA-Z]", "_"), Title = panel.Name,
                        Background = (Brush)new BrushConverter().ConvertFromString(panel.BackgroundColor),
                        Width = 100, Height = 100, Margin = new Thickness(1), Foreground = (Brush)new BrushConverter().ConvertFromString(panel.ForegroundColor),
                        HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top, Cursor = Cursors.Hand, FontWeight = FontWeights.DemiBold,
                        HorizontalTitleAlignment = HorizontalAlignment.Left, VerticalTitleAlignment = VerticalAlignment.Bottom, Padding = new Thickness(1),
                        TitleFontSize = 14, ClickMode = ClickMode.Press, ToolTip = (!string.IsNullOrWhiteSpace(panel.Description)) ? panel.Description : null,
                        IsEnabled = panel.ModuleType.ToLower() != "webmodule" ? true : App.appRuntimeData.webServerRunning
                    }; System.Windows.Media.Imaging.BitmapImage panelIcon = new System.Windows.Media.Imaging.BitmapImage();
                    panelIcon = IconMaker.Icon((Color)ColorConverter.ConvertFromString(panel.IconColor), App.SystemSvgIconList.FirstOrDefault(a => a.Name.ToLower() == panel.IconName).SvgIconPath);
                    Image icon = new Image() { Width = 30, Height = 30, Source = panelIcon, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Center };
                    icon = (Image)EffectLibrary.GetAnimationEffect(icon, AnimationLibrary.RotationAndSizeEffect, EffectTypes.SizeZeroToWidthEffect, TriggerTypes.OnMouseMove, 30, 2);

                    toolPanel.Content = icon; toolPanel.Click += SystemModulePanel_Click;

                    ((WrapPanel)systemModuleList.Items.Cast<TabItem>().First(a => a.Name.ToString() == toolPanel.Uid.ToString()).Content).Children.Add(toolPanel);
                    btn_showModulePanel.IsEnabled = true;
                };
            } catch (Exception ex) { App.ApplicationLogging(ex); }
            return true;
        }


        /// <summary>
        /// Central Application Message Dialog for All Info / Error / other messages for User
        /// </summary>
        /// <param name="error">  </param>
        /// <param name="message"></param>
        /// <param name="confirm"></param>
        /// <returns></returns>
        public static async Task<MessageDialogResult> ShowMessageOnMainWindow(bool error, string message, bool confirm = false) {
            MessageDialogResult result = new MessageDialogResult();
            if (!string.IsNullOrWhiteSpace(message)) {
                if (error) App.ApplicationLogging(new Exception(), message);

                ProgressRing = Visibility.Hidden;
                MetroWindow metroWindow = Application.Current.MainWindow as MetroWindow;
                if (confirm) {
                    MetroDialogSettings settings = new MetroDialogSettings() { AffirmativeButtonText = metroWindow.Resources["yes"].ToString(), NegativeButtonText = metroWindow.Resources["no"].ToString() };
                    result = metroWindow.ShowModalMessageExternal(metroWindow.Resources["warning"].ToString(), message, MessageDialogStyle.AffirmativeAndNegative, settings);
                }
                else {
                    MetroDialogSettings settings = new MetroDialogSettings() { AffirmativeButtonText = metroWindow.Resources["ok"].ToString() };
                    result = metroWindow.ShowModalMessageExternal(error ? metroWindow.Resources["error"].ToString() : metroWindow.Resources["info"].ToString(), message, MessageDialogStyle.Affirmative, settings);
                }
            }
            return result;
        }

        /// <summary>
        /// Application Loaded Start Backend timer for check server set Theme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            try { this.Invoke(() => {
                    AppSystemTimer.Elapsed += SystemTimerController; AppSystemTimer.Enabled = true;
                    _ = SystemOperations.IncreaseFileVersionBuild();
                    AddOrRemoveTab(Resources["support"].ToString(), new SupportPage(), "Setting");
                }); //Load Theme
                AppTheme theme = ThemeManager.AppThemes.FirstOrDefault(t => t.Name.Equals(App.appRuntimeData.AppClientSettings.First(a => a.Key == "apper_themeName").Value));
                Accent accent = ThemeManager.Accents.FirstOrDefault(a => a.Name.Equals(App.appRuntimeData.AppClientSettings.First(b => b.Key == "apper_accentName").Value));
                if ((theme != null) && (accent != null)) { ThemeManager.ChangeAppStyle(Application.Current, accent, theme); }
            } catch (Exception ex) { App.ApplicationLogging(ex); }
        }

        /// <summary>
        /// Load UserMenu After Login And Sort
        /// </summary>
        private async Task<bool> LoadUserMenu() {
            try {
                App.SystemMenuList = await CommunicationManager.GetApiRequest<List<SystemMenuList>>(ApiUrls.ServerApi, "DatabaseServices/SpGetUserMenuList", App.UserData.Authentification.Token);
                App.SystemCustomList = await CommunicationManager.GetApiRequest<List<SystemCustomPageList>>(ApiUrls.EasyITCenterSystemCustomPageList, null, App.UserData.Authentification.Token);

                tb_verticalSystemMenu.Items.Clear();
                int? lastMenuGroupId = null; tb_verticalSystemMenu.Items.Clear(); TreeViewItem menuSection = null;
                App.SystemMenuList.ForEach(async menuItem => {
                    TreeViewItem menuUnit = null;
                    if (lastMenuGroupId != menuItem.GroupId) {
                        if (lastMenuGroupId != null) { tb_verticalSystemMenu.Items.Add(menuSection); menuSection = null; }
                        string headerName = await DBOperations.DBTranslation(menuItem.GroupName);
                        menuUnit = new TreeViewItem() { Name = "tv_" + menuItem.GroupName, Width = 280, Margin = new Thickness(0, 0, 0, 0), FontSize = 16, Header = headerName };
                        menuUnit.PreviewMouseDown += Menu_Selected;

                        //Fill subMenuTypeItems
                        SystemLocalEnumSets.MenuTypes.Where(a => a.Name.ToLower() != "agenda").ToList().ForEach(async menutype => {
                            string sectionHeaderName = await DBOperations.DBTranslation(menutype.Name + "s");
                            TreeViewItem item = new TreeViewItem() {
                                Name = "_" + menutype.Name.ToLower(),
                                Header = " " + sectionHeaderName,
                                Width = 280,
                                FontSize = 16,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                HorizontalContentAlignment = HorizontalAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Center,
                                VerticalContentAlignment = VerticalAlignment.Center
                            };
                            item.PreviewMouseDown += Menu_Selected;
                            menuUnit.Items.Add(item);
                        });
                    }
                    if (menuUnit == null) { menuUnit = menuSection; }

                    //Inserting PageItem
                    string pageName = await DBOperations.DBTranslation(menuItem.FormPageName); bool tooltipEnabled = bool.Parse(await DataOperations.ParameterCheck("ShowSystemMenuDescriptionToolTip"));
                    TreeViewItem menuPage = new TreeViewItem() { Name = menuItem.FormPageName, Header = pageName, ToolTip = (string.IsNullOrWhiteSpace(menuItem.Description) || !tooltipEnabled) ? null : menuItem.Description };
                    menuPage.PreviewMouseDown += Menu_Selected;

                    if (menuItem.MenuType.ToLower() != "agenda" && menuPage != null) { menuUnit.FindChildren<TreeViewItem>(false).Where(a => a.Name.ToLower() == "_" + menuItem.MenuType.ToLower()).First().Items.Add(menuPage); }
                    else { menuUnit.Items.Add(menuPage); }
                    menuSection = menuUnit; lastMenuGroupId = menuItem.GroupId;
                }); tb_verticalSystemMenu.Items.Add(menuSection);

                tb_verticalSystemMenu.Items.SortDescriptions.Add(new SortDescription("Header", ListSortDirection.Ascending));
                foreach (TreeViewItem menuItem in tb_verticalSystemMenu.Items) {
                    menuItem.FindChildren<TreeViewItem>(false).ToList().ForEach(submenuitem => { if (submenuitem.Items.Count == 0 && (submenuitem.Name.ToLower() == "_dial" || submenuitem.Name.ToLower() == "_view")) { menuItem.Items.Remove(submenuitem); } });
                }
                foreach (TreeViewItem menuItem in tb_verticalSystemMenu.Items) {
                    menuItem.Items.SortDescriptions.Add(new SortDescription("Header", ListSortDirection.Ascending));
                    menuItem.FindChildren<TreeViewItem>(false).ToList().ForEach(submenuitem => { submenuitem.Items.SortDescriptions.Add(new SortDescription("Header", ListSortDirection.Ascending)); });
                }

                await WebView2AutoInstaller.CheckAndInstallWebView();
                return true;
            } catch (Exception ex) { App.ApplicationLogging(ex); }
            return false;
        }

        /// <summary>
        /// Backend System Timer for check server connection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private async void SystemTimerController(object sender, ElapsedEventArgs e) {
            await this.Invoke(async () => {
                AppSystemTimer.Interval = (int.Parse(App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_serverCheckInterval").Value) * 1000);
                try {
                    //CheckSystemStatuses
                    RunReleaseMode = App.appRuntimeData.AppSystemStatuses == SystemStatuses.Release.ToString() || App.appRuntimeData.AppSystemStatuses == SystemStatuses.DebugWithSystemLogger.ToString();

                    //System Saver
                    if (bool.Parse(App.appRuntimeData.AppClientSettings.First(b => b.Key == "beh_activeSystemSaver").Value) && (DateTimeOffset.UtcNow - lastUserAction).TotalSeconds > int.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key == "beh_timeToEnable").Value) && this.FindChild<ScreenSaverPage>("") == null) {
                        SetLastUserAction();
                        string translatedName = await DBOperations.DBTranslation("activeSystemSaver", true);
                        AddOrRemoveTab(translatedName, new ScreenSaverPage(), "activesystemsaver");
                        await DBOperations.LoadWaitingDataInSleepMode();
                    }

                    //Check server connection
                    if (await CommunicationManager.CheckApiConnection()) {
                        ServiceRunning = true; ServiceStatus = Resources["running"].ToString();
                        UserLogged = App.UserData.Authentification != null;
                        if (!UserLogged) { App.UserData = new UserData(); si_loggedIn.Content = null; }

                        mi_logout.Header = UserLogged ? Resources["logout"].ToString() : Resources["logon"].ToString();

                        //Load All startup DB Settings
                        if (App.ServerSetting.Count() == 0) DBOperations.LoadStartupDBData();

                        //ONETime Update
                        if (ServiceRunning && !updateChecked && UserLogged) {
                            this.Invoke(() => { if (App.appRuntimeData.AppClientSettings.First(b => b.Key == "sys_automaticUpdate").Value != "never") { SystemUpdater.CheckUpdate(false); } updateChecked = true; });
                        }
                    }
                    else { SetServiceStop(); }
                } catch (Exception ex) { App.ApplicationLogging(ex); }
            });
        }

        /// <summary>
        /// Server is unavailable All operations are blocked
        /// </summary>
        private void SetServiceStop() {
            ServiceStatus = Resources["stopped"].ToString();
            DataGridSelected = DataGridSelectedIdListIndicator = DgRefresh = ServiceRunning = false;
            UserLogged = ServiceRunning && !string.IsNullOrWhiteSpace(si_loggedIn.Content.ToString());
            mi_logout.Header = UserLogged ? Resources["logout"].ToString() : Resources["logon"].ToString();
        }

        /// <summary>
        /// about applications information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private async void Btn_about_click(object sender, RoutedEventArgs e) {
            _ = await ShowMessageOnMainWindow(false,
                Resources["appName"].ToString() + "\n\n" + Resources["appDescription"].ToString() + "\n" + Resources["version"].ToString() +
                App.appRuntimeData.AppVersion + "\n" + Resources["author"].ToString() + "\n\n" + Resources["myCompany"].ToString() + "\n" + Resources["myName"].ToString() + "\n" +
                Resources["myStreet"].ToString() + "\n" + Resources["myState"].ToString() + "\n" + Resources["myInvoiceInfo"].ToString() + "\n" + Resources["myPhone"].ToString() + "\n" +
                Resources["myEmail"].ToString() + "\n" + Resources["myAccount"].ToString(), false);
        }


        /// <summary>
        /// Open Selected System Module
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemModulePanel_Click(object sender, RoutedEventArgs e) {
            try { SystemModuleList module = App.SystemModuleList.First(a => a.Id == int.Parse(((Tile)sender).Tag.ToString()));

                if (module.ModuleType.ToLower() == "webmodule") {

                    AddOrRemoveTab(((Tile)sender).Title, new LocSrvWebTmpViewerPage(), "Setting");
                    SystemTabs existingTab = ((SystemWindowDataModel)DataContext).TabContents.ToList().Where(a => a.Header.ToLower() == ((Tile)sender).Title.ToLower()).LastOrDefault();
                    if (existingTab != null) { ((LocSrvWebTmpViewerPage)existingTab.Content).ShowWebModule = module; }

                } else if (module.ModuleType.ToLower() == "appmodule") {

                    SystemOperations.StartExternalProccess("WINcmd", Path.Combine(App.appRuntimeData.appDataPath, module.FolderPath, module.FileName), Path.Combine(App.appRuntimeData.appDataPath, module.FolderPath));

                } else if (module.ModuleType.ToLower() == "systemtool") {

                    string objectToInstantiate = "EasyITSystemCenter.Pages." + module.StartupCommand.Split('_')[0] + "";
                    var objectType = Type.GetType(objectToInstantiate); object pageForm = Activator.CreateInstance(objectType, false);
                    AddOrRemoveTab(((Tile)sender).Title, pageForm);
                }
                SystemModulePanel.IsOpen = !SystemModulePanel.IsOpen;
            } catch (Exception ex) { App.ApplicationLogging(ex); }
        }


        /// <summary>
        /// MainWindow Keyboard pointer to Keyboard Central Application controller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void MainWindow_KeyDown(object sender, KeyEventArgs e) {
            if (!bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_disableSystemHotKeys").Value))
            { HardwareOperations.ApplicationKeyboardMaping(e); }
        }


        /// <summary>
        /// Help button controller for Show Help File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void Btn_LaunchHelp_Click(object sender, RoutedEventArgs e) => System.Windows.Forms.Help.ShowHelp(null, "Manual\\index.chm");

        /// <summary>
        /// Show Metro Theme possibilities
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void Btn_ShowModulePanel_Click(object sender, RoutedEventArgs e) { SystemModulePanel.IsOpen = !SystemModulePanel.IsOpen; }

        /// <summary>
        /// Show System On line Logger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void BtnShowLoggerClick(object sender, RoutedEventArgs e) {
            OnlineLogger.IsOpen = !OnlineLogger.IsOpen; btn_showOnlineLogger.Background = Brushes.DarkSeaGreen;
            i_onlineLoggerIcon.Spin = false;
            OWinApiMiddleWare.SystemLoggerWebSocketMonitorOnOff();
        }

        /// <summary>
        /// System Logger Source Selector Server Logger has Source From Client Settings by WebSocket URL
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">     The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SystemLoggerSourceChanged_Click(object sender, EventArgs e) => ServerLoggerSource = !ServerLoggerSource;

        /// <summary>
        /// Application Login Dialog
        /// </summary>
        public async void ShowLoginDialog() {
            try {
                ProgressRing = Visibility.Hidden;
                UserLogged = false; App.UserData = new UserData(); si_loggedIn.Content = null;
                LoginDialogSettings dialogSetting = new LoginDialogSettings() {
                    ColorScheme = MetroDialogOptions.ColorScheme, InitialUsername = "tester", InitialPassword = "tester",
                    AffirmativeButtonText = Resources["logon"].ToString(), NegativeButtonText = Resources["end"].ToString(),
                    AnimateShow = true, EnablePasswordPreview = true, NegativeButtonVisibility = Visibility.Visible, AnimateHide = false
                };

                LoginDialogData result = ((MetroWindow)App.Current.MainWindow).ShowModalLoginExternal(Resources["login"].ToString(), Resources["loginToApp"].ToString(), dialogSetting);
                if (result == null) { App.AppQuitRequest(false);
                } else {
                    ProgressRing = Visibility.Visible;
                    App.UserData.UserName = result.Username;
                    AuthentificationResponse dBResult = await CommunicationManager.BasicAuthApiRequest(ApiUrls.EasyITCenterAuthentication, result.Username, result.Password);
                    ProgressRing = Visibility.Hidden;
                    if (dBResult == null || dBResult.Token == null) {
                        
                        if (!UserLogged) {
                            if (!serviceRunning) { this.ShowModalMessageExternal(Resources["login"].ToString(), Resources["loginServiceError"].ToString() + "\n" + App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_apiAddress").Value + "/" + ApiUrls.EasyITCenterAuthentication + " " + Resources["loginServiceError1"].ToString()); }
                            else { this.ShowModalMessageExternal(Resources["login"].ToString(), Resources["incorrectNameOrPassword"].ToString() + (!string.IsNullOrWhiteSpace(dBResult.Message) ? Environment.NewLine + dBResult.Message : "")); }
                            ShowLoginDialog();
                        } else { this.ShowModalMessageExternal(Resources["login"].ToString(), Resources["loginError"].ToString() + (!string.IsNullOrWhiteSpace(dBResult.Message) ? Environment.NewLine + dBResult.Message : ""));  ShowLoginDialog(); }
                    } else {
                        App.UserData.Authentification = dBResult;
                        si_loggedIn.SetCurrentValue(ContentProperty, Resources["loggedIn"].ToString() + " " + ((App.UserData.Authentification != null) ? App.UserData.Authentification.Name + " " + App.UserData.Authentification.SurName : result.Username));
                        await DBOperations.LoadOrRefreshUserData();
                        await SetSystemModuleListPanel();
                        await LoadUserMenu();
                        ProgressRing = Visibility.Hidden;
                    }
                }
                ProgressRing = Visibility.Hidden;
            } catch (Exception ex) { App.ApplicationLogging(ex); ProgressRing = Visibility.Hidden; }
        }

        /// <summary>
        /// Applications Close Request Controller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        public static void MainWindow_Closing(object sender, CancelEventArgs e) { if (e.Cancel) return; e.Cancel = !e.Cancel; App.AppQuitRequest(false); }

        /// <summary>
        /// Application Logout button Controller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void Mi_logout_Click(object sender, RoutedEventArgs e) => ShowLoginDialog();

        /// <summary>
        /// System tools controllers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>

        #region Tools

        private void BtnKeyboardClick(object sender, RoutedEventArgs e) => TouchKeyboard.IsOpen = !TouchKeyboard.IsOpen;
        private void BtnCalculatorClick(object sender, RoutedEventArgs e) => Calc.Visibility = (Calc.IsVisible != true) ? Visibility.Visible : Visibility.Hidden;
        private void BtnStartRdpServerClick(object sender, RoutedEventArgs e) {
            if (vncProcessId > 0) { vncProcess.Kill(); vncProcessId = 0; VncRunning = Brushes.Red;
            } else {
                if (FileOperations.CheckFile(Path.Combine(App.appRuntimeData.startupPath, "Data", "Runtime", "VNC", "winvnc.exe"))) {
                    if (FileOperations.VncServerIniFile(Path.Combine(App.appRuntimeData.startupPath, "Data", "Runtime", "VNC"))) {
                        vncProcess = new Process();
                        ProcessStartInfo info = new ProcessStartInfo() {
                            FileName = Path.Combine(App.appRuntimeData.startupPath, "Data", "Runtime", "VNC", "winvnc.exe"),
                            WorkingDirectory = Path.Combine(App.appRuntimeData.startupPath, "Data", "Runtime", "VNC"),
                            Arguments = $@" -inifile {Path.Combine(App.appRuntimeData.startupPath, "Data", "Runtime", "VNC", "server.ini")} -run",
                            LoadUserProfile = true, CreateNoWindow = true,
                            UseShellExecute = false, WindowStyle = ProcessWindowStyle.Hidden,
                            RedirectStandardOutput = true, RedirectStandardError = true,
                            Verb = (Environment.OSVersion.Version.Major >= 6) ? "runas" : "",
                        };
                        vncProcess.StartInfo = info; vncProcess.Start(); vncProcessId = vncProcess.Id;
                        if (vncProcessId > 0) VncRunning = Brushes.Green; else VncRunning = Brushes.Red;
                    }
                }
            }
        }
        private void BtnCaptureAppClick(object sender, RoutedEventArgs e) => MediaOperations.SaveAppScreenShot(this);
        private void BtnShowToolTipsClick(object sender, RoutedEventArgs e) => FormOperations.DisplayAllToolTips_OnClick(this);

        #endregion Tools

        //----------------------------------------------------------------- BEGIN OF MENU REACTIONS -------------------------------------------------------------------------
        #region Tilt Actions

        /// <summary>
        /// Tilts: Standardized Opening or create Tilt documents
        /// </summary>
        /// <param name="translateHeader"></param>
        /// <returns></returns>
        public static async Task<bool> TiltOpenForm(string translateHeader) {
            MainWindow metroWindow = Application.Current.MainWindow as MainWindow;
            if (App.tiltTargets == TiltTargets.OfferToOrder.ToString()) {
                AddOrRemoveTab(translateHeader, new BusinessIncomingOrderListPage());
                metroWindow.cb_printReports.ItemsSource = await CommunicationManager.GetApiRequest<List<SystemReportList>>(ApiUrls.EasyITCenterSystemReportList, dataGridSelectedId.ToString() + "/IncomingOrderList", App.UserData.Authentification.Token);
            }
            else if (App.tiltTargets == TiltTargets.OfferToInvoice.ToString() || App.tiltTargets == TiltTargets.OrderToInvoice.ToString()) {
                AddOrRemoveTab(translateHeader, new BusinessOutgoingInvoiceListPage());
                metroWindow.cb_printReports.ItemsSource = await CommunicationManager.GetApiRequest<List<SystemReportList>>(ApiUrls.EasyITCenterSystemReportList, dataGridSelectedId.ToString() + "/OutgoingInvoiceList", App.UserData.Authentification.Token);
            }
            else if (App.tiltTargets == TiltTargets.InvoiceToCredit.ToString()) {
                AddOrRemoveTab(translateHeader, new BusinessCreditNoteListPage());
                metroWindow.cb_printReports.ItemsSource = await CommunicationManager.GetApiRequest<List<SystemReportList>>(ApiUrls.EasyITCenterSystemReportList, dataGridSelectedId.ToString() + "/CreditNoteList", App.UserData.Authentification.Token);
            }
            else if (App.tiltTargets == TiltTargets.ShowCredit.ToString()) {
                AddOrRemoveTab(translateHeader, new BusinessCreditNoteListPage());
                metroWindow.cb_printReports.ItemsSource = await CommunicationManager.GetApiRequest<List<SystemReportList>>(ApiUrls.EasyITCenterSystemReportList, dataGridSelectedId.ToString() + "/CreditNoteList", App.UserData.Authentification.Token);
            }
            else if (App.tiltTargets == TiltTargets.InvoiceToReceipt.ToString()) {
                AddOrRemoveTab(translateHeader, new BusinessReceiptListPage());
                metroWindow.cb_printReports.ItemsSource = await CommunicationManager.GetApiRequest<List<SystemReportList>>(ApiUrls.EasyITCenterSystemReportList, dataGridSelectedId.ToString() + "/ReceiptList", App.UserData.Authentification.Token);
            }
            else if (App.tiltTargets == TiltTargets.ShowReceipt.ToString()) {
                AddOrRemoveTab(translateHeader, new BusinessReceiptListPage());
                metroWindow.cb_printReports.ItemsSource = await CommunicationManager.GetApiRequest<List<SystemReportList>>(ApiUrls.EasyITCenterSystemReportList, dataGridSelectedId.ToString() + "/ReceiptList", App.UserData.Authentification.Token);
            }
            else { App.TiltInvoiceDoc = new ExtendedOutgoingInvoiceList(); App.TiltOrderDoc = new BusinessIncomingOrderList(); App.TiltOfferDoc = new BusinessOfferList(); App.TiltDocItems = new List<DocumentItemList>(); App.tiltTargets = TiltTargets.None.ToString(); }
            return true;
        }

        #endregion Tilt Actions

        #region SearchPanel

        /// <summary>
        /// Full dynamic apply sett ed filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void CbFilterDropDownClosed(object sender, EventArgs e) {
            SystemTabs SelectedTab = (SystemTabs)TabablzControl.GetLoadedInstances().Last().SelectedItem;
            string advancedFilter = SystemOperations.FilterToString(cb_filter);

            ((DataViewSupport)Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + SelectedTab.Content.GetType().Name.Split('_')[0])).GetType().GetField("dataViewSupport").GetValue(null)).AdvancedFilter = advancedFilter;
            cb_filter.SelectedIndex = 0;
            _ = Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + SelectedTab.Content.GetType().Name.Split('_')[0])).GetType()
                .GetMethod("LoadDataList").Invoke(((SystemTabs)InitialTabablzControl.SelectedItem).Content, null);
        }

        /// <summary>
        /// Full dynamic Show/Hidden DataGrid advanced Filter Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void Mi_filter_Click(object sender, RoutedEventArgs e) {
            string mark = DateTime.UtcNow.Ticks.ToString();

            if (((Button)sender).Name == "mi_plus") {
                SystemTabs SelectedTab = (SystemTabs)TabablzControl.GetLoadedInstances().Last().SelectedItem;
                var viewFields = Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + SelectedTab.Content.GetType().Name.Split('_')[0])).GetType().GetField("selectedRecord").GetValue(null);

                ComboBox cbFieldsBox = new ComboBox() { Name = "field_" + mark, Width = 200, Height = 30 };
                cbFieldsBox.SelectionChanged += FilterField_SelectionChanged;
                foreach (PropertyInfo propertyInfo in viewFields.GetType().GetProperties()) {
                    if (propertyInfo.ToString().Split(' ')[0].ToLower().Replace("system.", "") != "byte[]") {
                        ComboBoxItem cbItem = new ComboBoxItem() { Content = propertyInfo.Name, Tag = propertyInfo.ToString().Split(' ')[0].ToLower().Replace("system.", ""), Width = 200, Height = 30 };
                        cbFieldsBox.Items.Add(cbItem);
                    }
                }
                WrapPanel dockPanel = new WrapPanel() { Name = "filter_" + mark, Width = 230, Height = 60 };
                dockPanel.Children.Add(cbFieldsBox);
                Button removeBtn = new Button() { Name = "remove_" + mark, Width = 30, Content = "X", Height = 30, FontSize = 20, Padding = new Thickness(0) };
                removeBtn.Click += RemoveFilterItem_Click;
                dockPanel.Children.Add(removeBtn);

                ComboBox cbSignBox = new ComboBox() { Name = "sign_" + mark, Width = 80, Height = 30 };
                dockPanel.Children.Add(cbSignBox);
                dockPanel.Children.Add(new TextBox() { Name = "text_" + mark, Width = 150, FontSize = 16 });

                cb_filter.Items.Add(dockPanel);
            }
            else if (((Button)sender).Name == "mi_open" || ((Button)sender).Name == "mi_and" || ((Button)sender).Name == "mi_or") {
                if (((WrapPanel)cb_filter.Items[cb_filter.Items.Count - 1]).Name.Split('_')[0] == "condition") {
                    ((WrapPanel)cb_filter.Items[cb_filter.Items.Count - 1]).Children.Add(new Label() { Content = " " + ((Button)sender).Content + " ", Height = 30, FontSize = 20, Padding = new Thickness(0) });
                }
                else {
                    WrapPanel dockPanel = new WrapPanel() { Name = "condition_" + mark, Width = 230, Height = 30 };
                    Button removeBtn = new Button() { Name = "remove_" + mark, Width = 30, Content = "X", Height = 30, FontSize = 20, Padding = new Thickness(0) };
                    removeBtn.Click += RemoveFilterItem_Click;
                    dockPanel.Children.Add(removeBtn);
                    dockPanel.Children.Add(new Label() { Content = " " + ((Button)sender).Content + " ", Height = 30, FontSize = 20, Padding = new Thickness(0) });
                    cb_filter.Items.Add(dockPanel);
                }
            }
            else if (((Button)sender).Name == "mi_close") {
                WrapPanel dockPanel = new WrapPanel() { Name = "condition_" + mark, Width = 230, Height = 30 };
                Button removeBtn = new Button() { Name = "remove_" + mark, Width = 30, Content = "X", Height = 30, FontSize = 20, Padding = new Thickness(0) };
                removeBtn.Click += RemoveFilterItem_Click;
                dockPanel.Children.Add(removeBtn);
                dockPanel.Children.Add(new Label() { Content = " " + ((Button)sender).Content + " ", Height = 30, FontSize = 20, Padding = new Thickness(0) });
                cb_filter.Items.Add(dockPanel);
            }
        }

        /// <summary>
        /// Full dynamic build filter on selected page from saved advanced filter
        /// </summary>
        /// <param name="filterBox">     </param>
        /// <param name="advancedFilter"></param>
        /// <returns></returns>
        public ComboBox StringToFilter(ComboBox filterBox, string advancedFilter) {
            advancedFilter = (!string.IsNullOrWhiteSpace(advancedFilter)) ? advancedFilter : "";
            //clear Filter
            int filterItemCount = filterBox.Items.Count;
            if (filterItemCount > 1) {
                for (int index = 0; index < filterItemCount - 1; index++) {
                    filterBox.Items.RemoveAt(1);
                }
            }

            //Add existing items
            string[] filterLines = advancedFilter.Split(new[] { "[!]" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string filterLine in filterLines) {
                string mark = DateTime.UtcNow.Ticks.ToString();
                if (filterLine.FirstOrDefault().ToString() == " ") { //condition line
                    WrapPanel dockPanel = new WrapPanel() { Name = "condition_" + mark, Width = 230, Height = 30 };
                    Button removeBtn = new Button() { Name = "remove_" + mark, Width = 30, Content = "X", Height = 30, FontSize = 20, Padding = new Thickness(0) };
                    removeBtn.Click += RemoveFilterItem_Click;
                    dockPanel.Children.Add(removeBtn);

                    string[] filterItems = filterLine.Split(new[] { "{!}" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string filterItem in filterItems) {
                        dockPanel.Children.Add(new Label() { Content = filterItem, Height = 30, FontSize = 20, Padding = new Thickness(0) });
                    }
                    filterBox.Items.Add(dockPanel);
                }
                else // Item Field
                {
                    SystemTabs SelectedTab = (SystemTabs)TabablzControl.GetLoadedInstances().Last().SelectedItem;
                    var viewFields = Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + SelectedTab.Content.GetType().Name.Split('_')[0])).GetType().GetField("selectedRecord").GetValue(null);

                    ComboBox cbFieldsBox = new ComboBox() { Name = "field_" + mark, Width = 200, Height = 30 };
                    cbFieldsBox.SelectionChanged += FilterField_SelectionChanged;
                    foreach (PropertyInfo propertyInfo in viewFields.GetType().GetProperties()) {
                        if (propertyInfo.ToString().Split(' ')[0].ToLower().Replace("system.", "") != "byte[]") {
                            ComboBoxItem cbItem = new ComboBoxItem() { Content = propertyInfo.Name, Tag = propertyInfo.ToString().Split(' ')[0].ToLower().Replace("system.", ""), Width = 200, Height = 30 };
                            cbFieldsBox.Items.Add(cbItem);
                        }
                    }
                    WrapPanel dockPanel = new WrapPanel() { Name = "filter_" + mark, Width = 230, Height = 60 };
                    dockPanel.Children.Add(cbFieldsBox);
                    Button removeBtn = new Button() { Name = "remove_" + mark, Width = 30, Content = "X", Height = 30, FontSize = 20, Padding = new Thickness(0) };
                    removeBtn.Click += RemoveFilterItem_Click;
                    dockPanel.Children.Add(removeBtn);

                    ComboBox cbSignBox = new ComboBox() { Name = "sign_" + mark, Width = 80, Height = 30 };
                    dockPanel.Children.Add(cbSignBox);
                    dockPanel.Children.Add(new TextBox() { Name = "text_" + mark, Width = 150, FontSize = 16 });

                    cb_filter.Items.Add(dockPanel);
                    string[] filterItems = filterLine.Split(new[] { "{!}" }, StringSplitOptions.None);
                    ((ComboBox)((WrapPanel)cb_filter.Items[cb_filter.Items.Count - 1]).Children[0]).Text = filterItems[0];
                    ((ComboBox)((WrapPanel)cb_filter.Items[cb_filter.Items.Count - 1]).Children[2]).Text = filterItems[1];
                    ((TextBox)((WrapPanel)cb_filter.Items[cb_filter.Items.Count - 1]).Children[3]).Text = filterItems[2].Replace("'", "");
                }
            }
            return filterBox;
        }
        

        /// <summary>
        /// Full dynamic Remove Item from DataGrid advanced Filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        public void RemoveFilterItem_Click(object sender, RoutedEventArgs e) {
            ComboBox cb_newFilterItems = cb_filter;
            try { foreach (WrapPanel filterItem in cb_filter.Items) { if (filterItem.Name.Split('_')[1] == ((Button)sender).Name.Split('_')[1]) { cb_newFilterItems.Items.Remove(filterItem); cb_newFilterItems.Items.Refresh(); } } } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            cb_filter = cb_newFilterItems;
        }

        /// <summary>
        /// Full dynamic set sign DataGrid advanced filter type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void FilterField_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            int index = 0;
            try {
                foreach (WrapPanel filterItem in cb_filter.Items) {
                    if (index > 0 && filterItem.Name.Split('_')[1] == ((ComboBox)sender).Name.Split('_')[1]) {
                        ((ComboBox)filterItem.Children[2]).Items.Clear();
                        if ("string?".Contains(((ComboBoxItem)((ComboBox)sender).SelectedItem).Tag.ToString())) ((ComboBox)filterItem.Children[2]).Items.Add(new ComboBoxItem() { Content = " LIKE ", Width = 80 });
                        if ("string?,float?,int?,int32?,int64?,decimal?,double?,bit?,boolean?,datetime?,date?".Contains(((ComboBoxItem)((ComboBox)sender).SelectedItem).Tag.ToString())) {
                            ((ComboBox)filterItem.Children[2]).Items.Add(new ComboBoxItem() { Content = " = ", Width = 80, FontSize = 20 });
                            ((ComboBox)filterItem.Children[2]).Items.Add(new ComboBoxItem() { Content = " <> ", Width = 80, FontSize = 20 });
                        }
                        if (",int?,float?,int32?,int64?,decimal?,double?,datetime?,date?".Contains(((ComboBoxItem)((ComboBox)sender).SelectedItem).Tag.ToString())) {
                            ((ComboBox)filterItem.Children[2]).Items.Add(new ComboBoxItem() { Content = " > ", Width = 80, FontSize = 20 });
                            ((ComboBox)filterItem.Children[2]).Items.Add(new ComboBoxItem() { Content = " < ", Width = 80, FontSize = 20 });
                        }
                    }
                    index++;
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        #endregion SearchPanel


        /// <summary>
        /// Dragging and separate to more Applications: TabPanel drag Controller - not Used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void MainGrid_IsDraggingChanged(object sender, RoutedPropertyChangedEventArgs<bool> e) { }
        private void TabControl_OnDragEnter(object sender, DragEventArgs e) {
            if (!(e.OriginalSource is Visual v)) { return; }
            var item = AppExtension.GetParentOfType<DragablzItem>(v);
            if (!(item == null || !(item.Content is TabItem ti))) { ti.IsSelected = true; }
        }

        /// <summary>
        /// Print Report Selection Controller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private async void CbPrintReportsSelected(object sender, SelectionChangedEventArgs e) {
            if (cb_printReports.SelectedItem != null && !OperationRunning) {
                OperationRunning = true;

                try {
                    ProgressRing = Visibility.Visible;
                    SqlConnection cnn = new SqlConnection(App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_reportConnectionString").Value);
                    await cnn.OpenAsync();
                    if (cnn.State == System.Data.ConnectionState.Open) {
                        cnn.Close();
                        SystemTabs SelectedTab = (SystemTabs)TabablzControl.GetLoadedInstances().Last().SelectedItem;
                        string advancedFilter = ((DataViewSupport)Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + SelectedTab.Content.GetType().Name.Split('_')[0])).GetType().GetField("dataViewSupport").GetValue(null)).AdvancedFilter;
                        advancedFilter = (string.IsNullOrWhiteSpace(advancedFilter)) ? "1=1" : advancedFilter.Replace("[!]", "").Replace("{!}", "");

                        //Update Filter data for generate Report
                        SetReportFilter setReportFilter = new SetReportFilter() { TableName = SelectedTab.Content.GetType().Name.Replace("Page", "").Split('_')[0], Filter = advancedFilter, Search = tb_search.Text, RecId = dataGridSelectedId };
                        string json = JsonConvert.SerializeObject(setReportFilter);
                        StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                        DBResultMessage dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterSystemReportQueueList, httpContent, "WriteFilter", App.UserData.Authentification.Token);

                        string reportFile = Path.Combine(App.appRuntimeData.reportFolder, DateTimeOffset.Now.Ticks.ToString() + ".rdl");
                        if (FileOperations.ByteArrayToFile(reportFile, ((SystemReportList)cb_printReports.SelectedItem).File)) {
                            Process exeProcess = new Process();
                            ProcessStartInfo info = new ProcessStartInfo() {
                                FileName = App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_reportingPath").Value,
                                WorkingDirectory = Path.GetDirectoryName(App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_reportingPath").Value) + "\\",
                                Arguments = reportFile + " -p \"Connect=" + App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_reportConnectionString").Value + "&TableName=" + SelectedTab.Content.GetType().Name.Replace("Page", "").Split('_')[0] + "&Search=%" + tb_search.Text + "%&Id=" + dataGridSelectedId.ToString() + "&Filter=" + advancedFilter + "\"",
                                LoadUserProfile = true, CreateNoWindow = false, UseShellExecute = false,
                                WindowStyle = ProcessWindowStyle.Normal,
                                RedirectStandardOutput = true, RedirectStandardError = true,
                                Verb = (Environment.OSVersion.Version.Major >= 6) ? "runas" : "",
                            };
                            exeProcess.StartInfo = info; exeProcess.Start();
                        }
                    }
                    else { cnn.Close(); await ShowMessageOnMainWindow(true, Resources["connectionStringIsNotValid"].ToString()); }
                    ProgressRing = Visibility.Hidden;
                } catch (Exception ex) {
                    App.ApplicationLogging(ex);
                    ProgressRing = Visibility.Hidden;
                    await ShowMessageOnMainWindow(true, Resources["connectionStringIsNotValid"].ToString());
                } finally { OperationRunning = false; }

                cb_printReports.SelectedIndex = -1;
            }
        }

        //open or select existed TabPanel VERTICAL MENU
        public async void Menu_Selected(object sender, MouseButtonEventArgs e) {
            try {
                string name = ((FrameworkElement)sender).Name;
                if (name.StartsWith("tv_") && !((TreeViewItem)sender).IsExpanded) {
                    foreach (TreeViewItem menuItem in tb_verticalSystemMenu.Items) { menuItem.IsExpanded = false; }
                    ((TreeViewItem)sender).IsExpanded = !((TreeViewItem)sender).IsExpanded;
                }
                else if (name.ToLower() == "_dial" || name.ToLower() == "_view") {
                    foreach (TreeViewItem menuItem in tb_verticalSystemMenu.Items) {
                        menuItem.FindChildren<TreeViewItem>(false).ToList().ForEach(subMenuItem => {
                            if (name.ToLower() == "_dial" && subMenuItem.Name != "_dial") { subMenuItem.IsExpanded = false; }
                            else if (name.ToLower() == "_view" && subMenuItem.Name != "_view") { subMenuItem.IsExpanded = false; }
                        });
                    }
                    ((TreeViewItem)sender).IsExpanded = !((TreeViewItem)sender).IsExpanded;
                }
                else if (!name.StartsWith("tv_") && name.ToLower() != "_dial" && name.ToLower() != "_view") {
                    //Initiate PageClass by Page Name
                    string pageName = name; string objectToInstantiate = "EasyITSystemCenter.Pages." + pageName.Split('_')[0] + "";
                    var objectType = Type.GetType(objectToInstantiate);
                    object pageForm = Activator.CreateInstance(objectType, false);
                    if (((MainWindow)Application.Current.MainWindow).MultiSameTabsEnabled || TabablzControl.GetLoadedInstances().Last().GetOrderedHeaders().Count(a => a.Content.ToString() == DBOperations.DBTranslation(pageName.Split('_')[0]).GetAwaiter().GetResult()) == 0) 
                    { AddOrRemoveTab(await DBOperations.DBTranslation(pageName), pageForm); }
                 
                    else { InitialTabablzControl.SelectedIndex = TabablzControl.GetLoadedInstances().Last().GetOrderedHeaders().First(a => a.Content.ToString() == DBOperations.DBTranslation(pageName.Split('_')[0]).GetAwaiter().GetResult().ToString()).LogicalIndex; }
                    StringToFilter(cb_filter, "");

                    if (App.UserData.Authentification != null) {
                        cb_printReports.ItemsSource = await CommunicationManager.GetApiRequest<List<SystemReportList>>(ApiUrls.EasyITCenterSystemReportList, dataGridSelectedId.ToString() + "/" + pageName.Replace("Page", "").Split('_')[0], App.UserData.Authentification.Token);
                    }

                    cb_printReports.IsEnabled = cb_printReports.Items.Count > 0;
                    tb_verticalSystemMenu.IsOverflowOpen = false;
                }
            } catch (Exception ex) { App.ApplicationLogging(ex); }
        }


        /// <summary>
        /// THIS IS AUTOMATIC INCLUDE DATALIST VIEW MENU in FORMAT APIcallPage open or select
        /// existed TabPanel VERTICAL MENU - Copy and CHANGE ONLY Page Name AND Report CALL as /XXXX
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void Menu_action_Click(object sender, RoutedEventArgs e) {
            try {
                SystemTabs SelectedTab = (SystemTabs)TabablzControl.GetLoadedInstances().Last().SelectedItem;
                string senderName = SelectedTab?.Content.GetType().Name;
                DBResultMessage dBResult = new DBResultMessage();
                switch (((FrameworkElement)sender).Name) {
                    case "tb_search":
                        if (Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + senderName.Split('_')[0])).GetType().GetMethod("Filter") != null) {
                            _ = Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + senderName.Split('_')[0])).GetType()
                                .GetMethod("Filter").Invoke(((SystemTabs)InitialTabablzControl.SelectedItem).Content, new object[] { ((TextBox)e.Source).Text });
                        }
                        break;

                    case "mi_reload":
                        if (Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + senderName.Split('_')[0])).GetType().GetMethod("LoadDataList") != null) {
                            _ = Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + senderName.Split('_')[0])).GetType()
                             .GetMethod("LoadDataList").Invoke(((SystemTabs)InitialTabablzControl.SelectedItem).Content, null);
                        }
                        break;

                    case "mi_new":
                        if (Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + senderName.Split('_')[0])).GetType().GetMethod("NewRecord") != null) {
                            _ = Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + senderName.Split('_')[0])).GetType()
                            .GetMethod("NewRecord").Invoke(((SystemTabs)InitialTabablzControl.SelectedItem).Content, null);
                        }
                        break;

                    case "mi_edit":
                        if (Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + senderName.Split('_')[0])).GetType().GetMethod("EditRecord") != null) {
                            _ = Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + senderName.Split('_')[0])).GetType()
                            .GetMethod("EditRecord").Invoke(((SystemTabs)InitialTabablzControl.SelectedItem).Content, new object[] { false });
                        }
                        break;

                    case "mi_copy":
                        if (Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + senderName.Split('_')[0])).GetType().GetMethod("EditRecord") != null) {
                            _ = Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + senderName.Split('_')[0])).GetType()
                            .GetMethod("EditRecord").Invoke(((SystemTabs)InitialTabablzControl.SelectedItem).Content, new object[] { true });
                        }
                        break;

                    case "mi_delete":
                        if (Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + senderName.Split('_')[0])).GetType().GetMethod("DeleteRecord") != null) {
                            _ = Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + senderName.Split('_')[0])).GetType()
                            .GetMethod("DeleteRecord").Invoke(((SystemTabs)InitialTabablzControl.SelectedItem).Content, null);
                        }
                        break;

                    default: break;
                }
                cb_printReports.IsEnabled = cb_printReports.Items.Count > 0;
            } catch (Exception ex) { App.ApplicationLogging(ex); }
        }

        /// <summary>
        /// Tabs Pages control for Insert/Move/Change Pages
        /// </summary>
        /// <param name="headerName"></param>
        /// <param name="tabPage">   </param>
        /// <param name="tagText">   </param>
        public static void AddOrRemoveTab(string headerName, object tabPage = null, string tagText = null) {
            int? dbRecId = null; //Get DbRecId for Usermenu For UserControls
            try {  if (tabPage != null && App.SystemMenuList.Any()) { 

                    dbRecId = App.SystemMenuList.Where(a => a.FormPageName == tabPage.ToString().Split('.').Last() 
                    || a.FormPageName + "Page" == tabPage.ToString().Split('.').Last()).Select(a => a.Id).FirstOrDefault();

                    if (dbRecId == null || dbRecId == 0) {
                        SystemTranslationList translation = App.LanguageList.FirstOrDefault(a => a.DescriptionCz == headerName || a.DescriptionEn == headerName);
                        if (translation != null) { dbRecId = App.SystemCustomList.Where(a => a.PageName == translation.SystemName).Select(a => a.Id).FirstOrDefault(); }
                    }

                    //ENABLING AND START LOAD ACTION OF SYSTEM WEB MODULE
                    ((UIElement)tabPage).Uid = dbRecId.ToString(); ((UIElement)tabPage).IsEnabled = true; }
            } catch { }

            try {
                IEnumerable<DragablzItem> existedTabs = TabablzControl.GetLoadedInstances().Last().GetOrderedHeaders();
                existedTabs = TabablzControl.GetLoadedInstances().Last().GetOrderedHeaders();

                if (tabPage == null) { } //Removing TabPage by name

                else if (existedTabs.Count() > 0) {
                    SystemTabs tc1 = new SystemTabs(dbRecId, headerName, tabPage, tagText);
                    TabablzControl.AddItem(tc1, existedTabs.Last().DataContext, AddLocationHint.After);
                    TabablzControl.SelectItem(tc1);
                    existedTabs = TabablzControl.GetLoadedInstances().Last().GetOrderedHeaders();

                } else if (existedTabs.Count() == 0) {
                    SystemWindowDataModel result = new SystemWindowDataModel();
                    result.TabContents.Add(new SystemTabs(dbRecId, headerName, tabPage, tagText));
                    MetroWindow metroWindow = Application.Current.MainWindow as MetroWindow;
                    metroWindow.DataContext = result;
                    existedTabs = TabablzControl.GetLoadedInstances().Last().GetOrderedHeaders();
                }
            } catch (Exception ex) { App.ApplicationLogging(ex); }
        }



        /// <summary>
        /// Tab click selection change reload ID and Pointers for ListView Buttons
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">     
        /// The <see cref="SelectionChangedEventArgs"/> instance containing the event data.
        /// </param>
        private async void TabPanelOnSelectionChange(object sender, SelectionChangedEventArgs e) {
            try {
                // Closing TabPanel
                try {
                    if (e.RemovedItems.Count > 0 && ((SystemTabs)e.RemovedItems[0]).Tag != null && ((SystemTabs)e.RemovedItems[0]).Tag.ToString() == "activesystemsaver")
                        SetLastUserAction();
                } catch { }

                //Insert TabPanel
                SystemTabs SelectedTab = (SystemTabs)TabablzControl.GetLoadedInstances().Last().SelectedItem;
                DataViewSupport dataViewSupport = new DataViewSupport();

                if (SelectedTab != null && ((FrameworkElement)SelectedTab.Content).Tag != null && ((FrameworkElement)SelectedTab.Content).Tag.ToString() == "Setting") {
                    tb_search.Text = null; dataGridSelectedId = 0; DataGridSelected = false; DataGridSelectedIdListIndicator = false;DgRefresh = false; cb_printReports.ItemsSource = null;
                }
                else if (SelectedTab != null && (((FrameworkElement)SelectedTab.Content).Tag != null && new string[] { "View", "Form" }.Contains(((FrameworkElement)SelectedTab.Content).Tag.ToString()))) {
                    DataGridSelected = true; DgRefresh = true; string senderName = SelectedTab.Content.GetType().Name;
                    var AutoPageGeneration = Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + senderName.Split('_')[0])).GetType();
                    switch (((FrameworkElement)SelectedTab.Content).Tag.ToString()) {
                        //FORMS - LIST + DETAIL FORM
                        case "Form":
                            dataViewSupport = ((DataViewSupport)Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + senderName.Split('_')[0])).GetType().GetField("dataViewSupport").GetValue(null));
                            tb_search.Text = dataViewSupport.FilteredValue;
                            if (dataViewSupport.FormShown) { dataGridSelectedId = 0; DataGridSelectedIdListIndicator = false; DataGridSelected = false; DgRefresh = false; }
                            else {
                                if (dataViewSupport.SelectedRecordId == 0) { dataGridSelectedId = 0; DataGridSelectedIdListIndicator = false; }
                                else { dataGridSelectedId = dataViewSupport.SelectedRecordId; DataGridSelectedIdListIndicator = true; }
                            }
                            StringToFilter(cb_filter, dataViewSupport.AdvancedFilter);
                            try { cb_printReports.ItemsSource = await CommunicationManager.GetApiRequest<List<SystemReportList>>(ApiUrls.EasyITCenterSystemReportList, dataGridSelectedId.ToString() + "/" + senderName.Replace("Page", ""), App.UserData.Authentification.Token); } catch { }
                            break;

                        //VIEWS - LIST ONLY
                        case "View":
                            dataViewSupport = ((DataViewSupport)Convert.ChangeType(SelectedTab.Content, Type.GetType("EasyITSystemCenter.Pages." + senderName.Split('_')[0])).GetType().GetField("dataViewSupport").GetValue(null));
                            tb_search.Text = dataViewSupport.FilteredValue; dataGridSelectedId = dataViewSupport.SelectedRecordId;
                            DataGridSelected = DataGridSelectedIdListIndicator = false; DgRefresh = true;
                            StringToFilter(cb_filter, dataViewSupport.AdvancedFilter);
                            try { cb_printReports.ItemsSource = await CommunicationManager.GetApiRequest<List<SystemReportList>>(ApiUrls.EasyITCenterSystemReportList, dataGridSelectedId.ToString() + "/" + senderName.Replace("Page", ""), App.UserData.Authentification.Token); } catch { }
                            break;

                        default:
                            break;
                    }
                }

                cb_printReports.IsEnabled = cb_printReports.Items.Count > 0;
            } catch (Exception ex) { App.ApplicationLogging(ex); }
        }
    }
}