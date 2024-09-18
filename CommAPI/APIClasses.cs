namespace EasyITSystemCenter.Api {

    /// <summary>
    /// ALL standard View AND Form API Call must end with "List" - These will automatic added for
    /// reports Definitions TODO From Table?
    /// </summary>
    public enum ApiUrls {
        /*Authentication & Check APIs*/
        EasyITCenterAuthentication,
        EasyITCenterBackendCheck,

        /*Basic/Shared APIs*/
        EasyITCenterBasicAttachmentList,
        EasyITCenterBasicCalendarList,
        EasyITCenterBasicCurrencyList,
        EasyITCenterBasicImageGalleryList,
        EasyITCenterBasicItemList,
        EasyITCenterBasicUnitList,
        EasyITCenterBasicVatList,

        /*Business Module APIs*/
        EasyITCenterBusinessAddressList,
        EasyITCenterBusinessBranchList,
        EasyITCenterBusinessCreditNoteList,
        EasyITCenterBusinessCreditNoteSupportList,
        EasyITCenterBusinessExchangeRateList,
        EasyITCenterBusinessIncomingInvoiceList,
        EasyITCenterBusinessIncomingInvoiceSupportList,
        EasyITCenterBusinessIncomingOrderList,
        EasyITCenterBusinessIncomingOrderSupportList,
        EasyITCenterBusinessMaturityList,
        EasyITCenterBusinessNotesList,
        EasyITCenterBusinessOfferList,
        EasyITCenterBusinessOfferSupportList,
        EasyITCenterBusinessOutgoingInvoiceList,
        EasyITCenterBusinessOutgoingInvoiceSupportList,
        EasyITCenterBusinessOutgoingOrderList,
        EasyITCenterBusinessOutgoingOrderSupportList,
        EasyITCenterBusinessPaymentMethodList,
        EasyITCenterBusinessPaymentStatusList,
        EasyITCenterBusinessReceiptList,
        EasyITCenterBusinessReceiptSupportList,
        EasyITCenterBusinessWarehouseList,

        /*Production Guides APIs*/
        EasyITCenterProdGuidGroupList,
        EasyITCenterProdGuidOperationList,
        EasyITCenterProdGuidPartList,
        EasyITCenterProdGuidPersonList,
        EasyITCenterProdGuidWorkList,

        /*Documentation Server Administration APIs*/
        EasyITCenterDocSrvDocumentationCodeLibraryList,
        EasyITCenterDocSrvDocumentationList,
        EasyITCenterDocSrvDocumentationGroupList,
        EasyITCenterDocSrvDocTemplateList,

        /*License Server Administration APIs*/
        EasyITCenterLicSrvLicenseActivationFailList,
        EasyITCenterLicSrvLicenseAlgorithmList,
        EasyITCenterLicSrvUsedLicenseList,

        /*Server Administartion Setting APIs*/
        EasyITCenterServerApiSecurityList,
        EasyITCenterServerCorsDefAllowedOriginList,
        EasyITCenterServerHealthCheckTaskList,
        EasyITCenterServerLiveDataMonitorList,
        ServerModuleAndServiceList,
        EasyITCenterServerSettingList,
        EasyITCenterServerStaticOrMvcDefPathList,
        EasyITCenterServerToolPanelDefinitionList,
        EasyITCenterServerToolTypeList,
        EasyITCenterSolutionEmailerHistoryList,
        EasyITCenterSolutionEmailTemplateList,
        EasyITCenterSolutionLanguageList,
        EasyITCenterSolutionOperationList,
        SolutionMessageModuleList,
        SolutionMessageTypeList,
        EasyITCenterSolutionMixedEnumList,
        EasyITCenterSolutionMottoList,
        EasyITCenterSolutionSchedulerList,
        EasyITCenterSolutionSchedulerProcessList,
        EasyITCenterSolutionTaskList,
        EasyITCenterSolutionUserList,
        EasyITCenterSolutionUserRoleList,
        SolutionStaticFileList,
        SolutionWebsiteList,

        /*System Administartion Setting APIs*/
        EasyITCenterSystemCustomPageList,
        EasyITCenterSystemDocumentAdviceList,
        EasyITCenterSystemDocumentTypeList,
        EasyITCenterSystemGroupMenuList,
        EasyITCenterSystemIgnoredExceptionList,
        EasyITCenterSystemLoginHistoryList,
        EasyITCenterSystemMenuList,
        SystemModuleList,
        EasyITCenterSystemParameterList,
        EasyITCenterSystemReportQueueList,
        EasyITCenterSystemReportList,
        EasyITCenterSolutionFailList,
        EasyITCenterSystemTranslationList,
        EasyITCenterSystemSvgIconList,

        /*WebPortal APIs*/
        EasyITCenterWebBannedIpAddressList,
        EasyITCenterWebCodeLibraryList,
        EasyITCenterWebCoreFileList,
        EasyITCenterWebDeveloperNewsList,
        EasyITCenterWebDocumentationCodeLibraryList,
        EasyITCenterWebDocumentationList,
        EasyITCenterWebGlobalPageBlockList,
        EasyITCenterWebGroupMenuList,
        EasyITCenterWebMenuList,
        EasyITCenterWebSettingList,
        EasyITCenterWebVisitIpList,

        /*WebHosting Apis*/
        EasyITCenterWebConfiguratorList,

        /*Server APIs*/
        ServerApi,

        /*Servers*/
        CoreServerRestart,
        FtpServerStart,
        FtpServerStop,


        /*Template Classes*/
        EasyITCenterTemplateClassList,
        EasyITCenterTemplateClassListWithParrent
    }

    /// <summary>
    /// Global API Definition of Result API calls for All Calling of Insert / Update / Delete
    /// </summary>
    public class DBResultMessage {
        public int InsertedId { get; set; } = 0;
        public string Status { get; set; }
        public int RecordCount { get; set; } = -1;
        public string ErrorMessage { get; set; }
    }
}