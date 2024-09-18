<UserControl
    x:Class="GoldenSystem.Pages.ServerSettingListPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:mah="http://metro.mahapps.com/winfx/xaml/controls"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    d:DesignHeight="500"
    d:DesignWidth="600"
    Tag="Setting"
    mc:Ignorable="d">
    <UserControl.Resources>
        <Thickness x:Key="ControlMargin">0 5 0 0</Thickness>
        <Style
            x:Key="NormalCaseColumnHeader"
            BasedOn="{StaticResource MetroDataGridColumnHeader}"
            TargetType="{x:Type DataGridColumnHeader}">
            <Setter Property="mah:ControlsHelper.ContentCharacterCasing" Value="Normal" />
        </Style>
    </UserControl.Resources>

    <Grid Name="configuration">
        <Grid
            Width="Auto" Height="Auto" Margin="0,0,0,0" HorizontalAlignment="Stretch" VerticalAlignment="Stretch"
            ForceCursor="False" ShowGridLines="False">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="300" />
                <ColumnDefinition Width="*" />
                <ColumnDefinition Width="*" />
                <ColumnDefinition Width="*" />
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition />
                <RowDefinition Height="110" />
            </Grid.RowDefinitions>

            <GroupBox
                x:Name="gb_coreSettings" Grid.Row="0" Grid.Column="0" Grid.ColumnSpan="4" Margin="5">
                <Grid Background="{DynamicResource WhiteBrush}" Visibility="Visible">
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="200" />
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                    </Grid.ColumnDefinitions>
                    <Grid.RowDefinitions>
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                    </Grid.RowDefinitions>

                    <TextBox
                        x:Name="txt_specialServerServiceName" Grid.Row="0" Grid.Column="0" Margin="0,2,0,2" HorizontalContentAlignment="Left">
                        <TextBox.ToolTip>
                            <TextBlock>
                                Its the Server Service Name for Registering in Operation System.<LineBreak />
                                This Name is Used in All informations, registrations and info of Server.</TextBlock>
                        </TextBox.ToolTip>
                    </TextBox>

                    <Label
                        x:Name="lbl_specialServerLanguage" Grid.Row="0" Grid.Column="1" HorizontalAlignment="Right" />
                    <ComboBox
                        x:Name="cb_specialServerLanguage" Grid.Row="0" Grid.Column="2" Margin="0,2,0,2" HorizontalAlignment="Left"
                        DisplayMemberPath="Name" IsEnabled="true" SelectedValuePath="Value">
                        <ComboBox.ToolTip>
                            <TextBlock>
                                Server Language is for Set the responses of API messages. - "BackendCheck for Example"<LineBreak />
                                Can be used for WebPages, translating on Background from Central Dictionary<LineBreak />
                                Actualy used By Background Server Core and Hard API responses</TextBlock>
                        </ComboBox.ToolTip>
                    </ComboBox>

                    <CheckBox
                        x:Name="chb_specialCoreCheckerEmailSenderActive" Grid.Row="0" Grid.Column="2" HorizontalAlignment="Right">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                Server has monitor of Fails on Backgroud<LineBreak />
                                Enable the automatic Sending Emails with Detected Problems by Server.<LineBreak />
                                Incorrect Server statuses are monitored and sent to inserted service email<LineBreak />
                                Its run only in Production mode. in Debud mode are the detected fails written to console.</TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                    <CheckBox
                        x:Name="chb_specialEnableMassEmail" Grid.Row="0" Grid.Column="3" HorizontalAlignment="Right">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                This config Enable Mass emailing Service API<LineBreak />
                                Server will send All Email Request List Over Server Emailer.<LineBreak />
                                Format of API Request you can find in Documentation</TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                    <CheckBox
                        x:Name="chb_specialUseDbLocalAutoupdatedDials" Grid.Row="0" Grid.Column="4" HorizontalAlignment="Right">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                This Enable using Tables As OneTime Load with monitor for Changing.<LineBreak />
                                Actually is defined table LanguageList for Translation.<LineBreak />
                                Table is Loaded to Server Memory and communication with this table is Without Database Connection.<LineBreak />
                                If is Detected some content change - it be processed, refreshed and next run in this mode.<LineBreak />
                            </TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                </Grid>
            </GroupBox>

            <GroupBox
                x:Name="gb_databaseEngine" Grid.Row="1" Grid.Column="0" Grid.ColumnSpan="4" Margin="5">
                <Grid Background="{DynamicResource WhiteBrush}" Visibility="Visible">
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="200" />
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                    </Grid.ColumnDefinitions>
                    <Grid.RowDefinitions>
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                    </Grid.RowDefinitions>

                    <CheckBox
                        x:Name="chb_databaseInternalCachingEnabled" Grid.Row="0" Grid.Column="0" HorizontalAlignment="Left">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                Entity Framework has Custom Cache Controller.<LineBreak />
                                Can be good for low-volatile data. But its Duplicate Functions with Same Service on Database Server<LineBreak />
                                Recommended is disable, for clean direct working with DB.<LineBreak />
                                Important is correct setting on DB Engine.</TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                    <Label
                        x:Name="lbl_databaseInternalCacheTimeoutMin" Grid.Row="0" Grid.Column="3" HorizontalAlignment="Right" HorizontalContentAlignment="Right" />
                    <mah:NumericUpDown
                        x:Name="txt_databaseInternalCacheTimeoutMin" Grid.Row="0" Grid.Column="4" Margin="0,2,0,2" HorizontalContentAlignment="Left"
                        mah:TextBoxHelper.ClearTextButton="true" mah:TextBoxHelper.Watermark="" Maximum="9999" Minimum="1">
                        <mah:NumericUpDown.ToolTip>
                            <TextBlock>
                                Entity Framework has Custom Cache Controller.<LineBreak />
                                Its Timweout for Data validation and refreshing</TextBlock>
                        </mah:NumericUpDown.ToolTip>
                    </mah:NumericUpDown>
                </Grid>
            </GroupBox>

            <GroupBox
                x:Name="gb_serverServices" Grid.Row="2" Grid.Column="0" Grid.ColumnSpan="4" Margin="5">
                <Grid Background="{DynamicResource WhiteBrush}" Visibility="Visible">
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="200" />
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                    </Grid.ColumnDefinitions>
                    <Grid.RowDefinitions>
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                    </Grid.RowDefinitions>

                    <CheckBox
                        x:Name="chb_serverTimeTokenValidationEnabled" Grid.Row="0" Grid.Column="0" HorizontalAlignment="Left">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                If this is enabled, used tokens are checked for timeout.<LineBreak />
                                If is disabled the token validity is valid everyTime<LineBreak />
                                Validation Check the timeout and the allow User</TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                    <CheckBox
                        x:Name="chb_serverRazorWebPagesEngineEnabled" Grid.Row="0" Grid.Column="2" HorizontalAlignment="Left">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                By this set the Server Has enabled Razor Engine<LineBreak />
                                This is need when you develop some Razor webPages "cshtml"<LineBreak />
                                This Engine automaticaly set Pages Ednpoints.</TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                    <CheckBox
                        x:Name="chb_serverEnableWebSocketMonitor" Grid.Row="0" Grid.Column="3" HorizontalAlignment="Left">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                By this set Enable Server Core Logging to the WebSocket Stream<LineBreak />
                                Its for remote monitoring of Server Core Status<LineBreak />
                                This Example Vision For Central Managing Informations.</TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                    <CheckBox
                        x:Name="chb_serverMvcWebPagesEngineEnabled" Grid.Row="0" Grid.Column="4" HorizontalAlignment="Left">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                By this set the Server Has enabled MVC Engine<LineBreak />
                                This Cann be possible for Addons, Modules, Your MVC WebPages<LineBreak />
                                This Engine automaticaly set Pages Ednpoints.</TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                    <CheckBox
                        x:Name="chb_serverWebSocketEngineEnabled" Grid.Row="1" Grid.Column="0" HorizontalAlignment="Left">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                This config Enable WebSocket Support<LineBreak />
                                In Solution is prepared Basic WebSocket Server for Central working.</TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                    <CheckBox
                        x:Name="chb_serverFtpEngineEnabled" Grid.Row="1" Grid.Column="2" HorizontalAlignment="Left">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                This config Enable FTP on Server. FTP is actualy unlocked.<LineBreak />
                                FTP is oppened for EveryOne without authorization.<LineBreak />
                                Its Separated Place on the Backend Server.</TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                    <CheckBox
                        x:Name="chb_serverFtpSecurityEnabled" Grid.Row="1" Grid.Column="3" HorizontalAlignment="Left">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                This config Enable Securited Login on FTP Server.<LineBreak />
                                For access you must login.Valid Accounts are from Server DB<LineBreak />
                                The Service has implement with Online Remote Control with using over authorized API</TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                    <CheckBox
                        x:Name="chb_serverWebBrowserEnabled" Grid.Row="1" Grid.Column="4" HorizontalAlignment="Left">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                This config enable web browsing on Urls, where are not any WebPages.<LineBreak />
                                Its ideally for Saving Materials and others.</TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                </Grid>
            </GroupBox>


            <GroupBox
                x:Name="gb_apiConfiguration" Grid.Row="3" Grid.Column="0" Grid.ColumnSpan="4" Margin="5">
                <Grid Background="{DynamicResource WhiteBrush}" Visibility="Visible">
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="200" />
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                    </Grid.ColumnDefinitions>
                    <Grid.RowDefinitions>
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                    </Grid.RowDefinitions>

                    <Label
                        x:Name="lbl_configServerStartupPort" Grid.Row="0" Grid.Column="0" HorizontalAlignment="Right" HorizontalContentAlignment="Right" />
                    <mah:NumericUpDown
                        x:Name="txt_configServerStartupPort" Grid.Row="0" Grid.Column="1" Margin="0,2,0,2" HorizontalContentAlignment="Left"
                        Maximum="65535" Minimum="1">
                        <mah:NumericUpDown.ToolTip>
                            <TextBlock>
                                This is Startup Port For All Server Services.<LineBreak />
                                Services are run on All Available IP addresses on machine.</TextBlock>
                        </mah:NumericUpDown.ToolTip>
                    </mah:NumericUpDown>

                    <Label
                        x:Name="lbl_configWebSocketTimeoutMin" Grid.Row="0" Grid.Column="2" Grid.ColumnSpan="2" HorizontalAlignment="Right"
                        HorizontalContentAlignment="Right" />
                    <mah:NumericUpDown
                        x:Name="txt_configWebSocketTimeoutMin" Grid.Row="0" Grid.Column="4" Margin="0,2,0,2" HorizontalContentAlignment="Left"
                        Maximum="65535" Minimum="1" ToolTip="Timeout on inactivity for Websocket Connection." />

                    <Label
                        x:Name="lbl_configMaxWebSocketBufferSizeKb" Grid.Row="1" Grid.Column="0" HorizontalAlignment="Right" HorizontalContentAlignment="Right" />
                    <mah:NumericUpDown
                        x:Name="txt_configMaxWebSocketBufferSizeKb" Grid.Row="1" Grid.Column="1" Margin="0,2,0,2" HorizontalContentAlignment="Left"
                        Maximum="65535" Minimum="1" ToolTip="Maximum allowed size for Web Socket Communication. (Kb)" />

                    <CheckBox
                        x:Name="chb_configServerStartupOnHttps" Grid.Row="1" Grid.Column="2" HorizontalAlignment="Center">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                If this is enabled the Server will start on HTTPS protocol.<LineBreak />
                                Certificate is Generated automatically.</TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                    <Label
                        x:Name="lbl_configApiTokenTimeoutMin" Grid.Row="1" Grid.Column="3" HorizontalAlignment="Right" HorizontalContentAlignment="Right" />
                    <mah:NumericUpDown
                        x:Name="txt_configApiTokenTimeoutMin" Grid.Row="1" Grid.Column="4" Margin="0,2,0,2" HorizontalContentAlignment="Left"
                        Maximum="99999" Minimum="1" ToolTip="Timeout on inactivity for JWT Bearer Token." />


                    <TextBox
                        x:Name="txt_configCertificateDomain" Grid.Row="2" Grid.Column="0" Margin="0,2,0,2" HorizontalAlignment="Stretch"
                        mah:TextBoxHelper.Watermark="127.0.0.1">
                        <TextBox.ToolTip>
                            <TextBlock>
                                Insert Certificate Domain name or IP address.<LineBreak />
                                It will be set in automatic generated certificate in HTTPS running.</TextBlock>
                        </TextBox.ToolTip>
                    </TextBox>

                    <PasswordBox
                        x:Name="txt_configCertificatePassword" Grid.Row="2" Grid.Column="1" Margin="2,2,0,2" HorizontalAlignment="Stretch"
                        Style="{StaticResource MetroButtonRevealedPasswordBox}">
                        <PasswordBox.ToolTip>
                            <TextBlock>
                                Insert Certificate Password.<LineBreak />
                                It will be set in automatic generated certificate in HTTPS running.</TextBlock>
                        </PasswordBox.ToolTip>
                    </PasswordBox>

                    <TextBox
                        x:Name="txt_configJwtLocalKey" Grid.Row="2" Grid.Column="2" Grid.ColumnSpan="2" Margin="30,2,0,2"
                        HorizontalAlignment="Stretch">
                        <TextBox.ToolTip>
                            <TextBlock>
                                JWT Bearer Encryption Key.<LineBreak />
                                This is Using for generation Communication JWT Bearer Token.</TextBlock>
                        </TextBox.ToolTip>
                    </TextBox>

                    <Button
                        x:Name="btn_generateKey" Grid.Row="2" Grid.Column="4" Width="150" Height="25"
                        Margin="5,2,0,0" Padding="5,4,5,5" HorizontalAlignment="Right" VerticalAlignment="Top" mah:ButtonHelper.PreserveTextCase="True"
                        Click="BtnGenerateJwtKey_Click"
                        Style="{DynamicResource AccentedSquareButtonStyle}">
                        <Button.ToolTip>Generate 40 char lenght Random Key</Button.ToolTip>
                    </Button>
                </Grid>
            </GroupBox>


            <GroupBox
                x:Name="gb_serverModules" Grid.Row="4" Grid.Column="0" Grid.ColumnSpan="4" Margin="5">
                <Grid Background="{DynamicResource WhiteBrush}" Visibility="Visible">
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="200" />
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                    </Grid.ColumnDefinitions>
                    <Grid.RowDefinitions>
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                    </Grid.RowDefinitions>

                    <CheckBox
                        x:Name="chb_moduleSwaggerApiDocEnabled" Grid.Row="0" Grid.Column="0" HorizontalAlignment="Left">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                Enable Autogenerator for full Api documentation with online testing<LineBreak />
                                If is disabled the token validity is valid everyTime<LineBreak />
                                Validation Check the timeout and the allow User</TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                    <CheckBox
                        x:Name="chb_moduleDataManagerEnabled" Grid.Row="0" Grid.Column="1" HorizontalAlignment="Right">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                If this is enabled, in Debug mode is avaiable Data Manager<LineBreak />
                                Data manager is autogenerated for full data management</TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                    <CheckBox
                        x:Name="chb_moduleMdDocumentationEnabled" Grid.Row="0" Grid.Column="3" HorizontalAlignment="Left">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                This setting Enable Project Documentation MD Viewer<LineBreak />
                            </TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                    <CheckBox
                        x:Name="chb_moduleHealthServiceEnabled" Grid.Row="0" Grid.Column="4" HorizontalAlignment="Left">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                If this is enabled, Server HeatchCheck service<LineBreak />
                                control all configured statuses in Program<LineBreak />
                                is possible check more than 200 Server/NET/IS/DB/etc. statuses</TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                    <CheckBox
                        x:Name="chb_moduleDbDiagramGeneratorEnabled" Grid.Row="1" Grid.Column="0" HorizontalAlignment="Left">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                This setting Enable DB Diagram Generator API.<LineBreak />
                                By API request is generated actual Database Schema.</TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                    <Label
                        x:Name="lbl_moduleHealthServiceRefreshIntervalSec" Grid.Row="1" Grid.Column="3" HorizontalAlignment="Right" HorizontalContentAlignment="Right" />
                    <mah:NumericUpDown
                        x:Name="txt_moduleHealthServiceRefreshIntervalSec" Grid.Row="1" Grid.Column="4" Margin="0,2,0,2" HorizontalContentAlignment="Left"
                        Maximum="1000" Minimum="1" ToolTip="Refresh Interval for checking all monitored Statuses." />

                </Grid>
            </GroupBox>

            <GroupBox
                x:Name="gb_emailService" Grid.Row="5" Grid.Column="0" Grid.ColumnSpan="4" Margin="5">
                <Grid Background="{DynamicResource WhiteBrush}" Visibility="Visible">
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="200" />
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                    </Grid.ColumnDefinitions>
                    <Grid.RowDefinitions>
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                    </Grid.RowDefinitions>

                    <Label
                        x:Name="lbl_emailerServiceEmailAddress" Grid.Row="0" Grid.Column="0" HorizontalAlignment="Right" />
                    <TextBox
                        x:Name="txt_emailerServiceEmailAddress" Grid.Row="0" Grid.Column="1" Margin="0,2,0,2" HorizontalContentAlignment="Left">
                        <TextBox.ToolTip>
                            <TextBlock>
                                Insert Email Address for administration of detected fails by Server background.<LineBreak />
                                Its run only in Production mode.<LineBreak />
                                This is Email address, where will be sent all detected problems</TextBlock>
                        </TextBox.ToolTip>
                    </TextBox>

                    <Label
                        x:Name="lbl_emailerSMTPServerAddress" Grid.Row="0" Grid.Column="2" HorizontalAlignment="Right" />
                    <TextBox
                        x:Name="txt_emailerSMTPServerAddress" Grid.Row="0" Grid.Column="3" Margin="0,2,0,2" HorizontalContentAlignment="Left"
                        ToolTip="Insert Server addres IP or full Domain name for email Service." />

                    <CheckBox
                        x:Name="chb_emailerSMTPSslIsEnabled" Grid.Row="0" Grid.Column="4" HorizontalAlignment="Right">
                        <CheckBox.ToolTip>
                            <TextBlock>
                                Enable Ssl Email Connection.<LineBreak />
                                Next must be set correct port for SSL</TextBlock>
                        </CheckBox.ToolTip>
                    </CheckBox>

                    <Label
                        x:Name="lbl_emailerSMTPPort" Grid.Row="1" Grid.Column="0" HorizontalAlignment="Left" />
                    <mah:NumericUpDown
                        x:Name="txt_emailerSMTPPort" Grid.Row="1" Grid.Column="0" HorizontalAlignment="Right" Maximum="65535"
                        Minimum="1"
                        Style="{StaticResource DefaultSystemNumericStyle}"
                        ToolTip="Insert SMTP Port of external server for email Service. Typically 25/465" />

                    <TextBox
                        x:Name="txt_emailerSMTPLoginUsername" Grid.Row="1" Grid.Column="1" Margin="10,2,0,2" HorizontalAlignment="Stretch"
                        ToolTip="SMTP username for login to external Emailserver. Its using only for sending of services Emails." />

                    <Label
                        x:Name="lbl_emailerSMTPLoginPassword" Grid.Row="1" Grid.Column="2" HorizontalAlignment="Right" />
                    <PasswordBox
                        x:Name="txt_emailerSMTPLoginPassword" Grid.Row="1" Grid.Column="3" Margin="0,2,0,2"
                        Style="{StaticResource MetroButtonRevealedPasswordBox}"
                        ToolTip="SMTP password for login and send service Email by Server" />

                    <Button
                        x:Name="btn_sendTestEmail" Grid.Row="1" Grid.Column="4" Width="150" Height="25"
                        Margin="5,2,5,0" Padding="5,4,5,5" HorizontalAlignment="Right" VerticalAlignment="Top" mah:ButtonHelper.PreserveTextCase="True"
                        Click="BtnSendTestEmail_Click"
                        Style="{DynamicResource AccentedSquareButtonStyle}">
                        <Button.ToolTip>Send Test Email</Button.ToolTip>
                    </Button>
                </Grid>
            </GroupBox>


            <Button
                Name="btn_save" Grid.Row="20" Grid.Column="0" Width="200" Height="40"
                Margin="44,21,0,44" HorizontalAlignment="Left" VerticalAlignment="Bottom" mah:ButtonHelper.PreserveTextCase="True" Click="BtnSave_Click"
                Style="{DynamicResource AccentedSquareButtonStyle}" />

            <Button
                Name="btn_exportServerConfig" Grid.Row="20" Grid.Column="1" Width="200" Height="40"
                Margin="0,21,0,44" HorizontalAlignment="Center" VerticalAlignment="Bottom" mah:ButtonHelper.PreserveTextCase="True" Click="BtnExport_Click"
                Style="{DynamicResource AccentedSquareButtonStyle}" />

            <Button
                Name="btn_restartServer" Grid.Row="20" Grid.Column="3" Width="200" Height="40"
                Margin="0,21,0,44" HorizontalAlignment="Center" VerticalAlignment="Bottom" mah:ButtonHelper.PreserveTextCase="True" Click="BtnRestartServer_Click"
                Style="{DynamicResource AccentedSquareButtonStyle}" />

        </Grid>
    </Grid>
</UserControl>
