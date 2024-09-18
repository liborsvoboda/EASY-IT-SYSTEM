ď»ż@page 
@model ServerCorePages.SystemToolsModel
@{
    ViewData["Title"] = "System Tools Generators";
}
    @*
    https://metroui.org.ua/intro.html
    https://metroui.org.ua/position.html
    https://www.c-sharpcorner.com/article/custom-login-register-with-identity-in-asp-net-core-3-1/
    *@

<div class="text-center info-panel mb-2">
    <window>
        <div class="h4 fg-darkOrange" style="top:10px;font-size:16px;">
            EASY to USE:<br />
            Prepared Web Pages & Tools for Immediatelly Using For make Modern Systems, Portals, Intranet.<br />
            <a href="https://korzh.com/blog/single-file-web-service-aspnetcore" target="_blank">Metro4 is Very Modern Standalone Technology</a><br />
            Its ideally for Easy build any Web/System for Every Web Server [example EDC EASY-DATA-Center]<br />
            Download, Unpack and Copy to your WebPages.<br />
            You show Presentations by Index.html<br />
        </div>

        @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {

        }
        else {
            <div class="h4 fg-red ani-hover-heartbeat">
                For Download You must be Logged in
            </div>
        }

        <div class="about ">
            <div class="container">
                <div class="mt-10 text-center">
                    <div class="fg-darkSteel" style="font-weight:bold;font-size:40px;opacity:0.7;">
                        Static Pages - Templates & Web Tools 
                    </div>
                    <p class="text-leader pl-20-md pr-20-md">
                        By click on left Icon open Example
                    </p>
                </div>

                <div class="row mt-10">
                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='openSystemDesktop();Metro.window.create({title:"System Desktop Template",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/SystemDesktop/index.html\" style=\"width:100%;height:640px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Click for Show Example"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="SystemDesktop" class="text-right" data-static="true" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("SystemDesktop");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            1000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("SystemDesktop");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            2000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer w-100">
                                        <div class="reduce-1 pt-2 enlarge-1-md text-right">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-2">System Desktop</div>
                                            </div>
@*                                                 @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                    <span id="systemDesktopDownload" onclick='downloadSystemDesktop();' class="mif-file-download ani-heartbeat pl-1 mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download System Desktop Template" />
                                                } *@
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='openSystemToolDesktop();Metro.window.create({title:"System Tool Desktop Template",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/SystemToolDesktop/index.html\" style=\"width:100%;height:640px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Click for Show Example"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="SystemToolDesktop" class="text-right" data-static="true" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("SystemToolDesktop");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            1000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("SystemToolDesktop");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            2000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer w-100">
                                        <div class="reduce-1 pt-2 enlarge-1-md text-right">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-2">System Tool Desktop</div>
                                            </div>
@*                                             @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                <span id="systemToolDesktopDownload" onclick='downloadSystemToolDesktop();' class="mif-file-download ani-heartbeat pl-1 mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download System Desktop Template" />
                                            } *@
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='openAdminDesktop();Metro.window.create({title:"System Tool Desktop Template",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/AdminDesktop/index.html\" style=\"width:100%;height:640px;\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Click for Show Example"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="AdminDesktop" class="text-right" data-static="true" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("AdminDesktop");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            3000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("AdminDesktop");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            5000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer w-100">
                                        <div class="reduce-1 pt-2 enlarge-1-md text-right">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-2">Admin Desktop</div>
                                            </div>
@*                                             @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                <span id="adminDesktopDownload" onclick='downloadAdminDesktop();' class="mif-file-download ani-heartbeat pl-1 mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download System Desktop Template" />
                                            } *@
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='openMoreEffects();Metro.window.create({title:"More Effects Library",shadow:true,draggable:true,customButtons:windowBackButton,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe  id=\"moreEffectWindow\" src=\"../server/EASYSYSTEMBuilder_Downloads/Metro4DevHelp/Metro4Example/examples\" style=\"width:100%;height:640px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Click for Show Example"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="MoreEffects" class="text-right" data-static="true" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("MoreEffects");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            2000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("MoreEffects");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            3000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer w-100">
                                        <div class="reduce-1 pt-2 enlarge-1-md text-right">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-2">More Effects Library</div>
                                            </div>
@*                                             @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                <span id="moreEffectsDownload" onclick='downloadMoreEffects();' class="mif-file-download ani-heartbeat pl-1 mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download More Effects Library" />
                                            } *@
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    @*<div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='openMdViewer();Metro.window.create({title:"Markdown Web Viewer and Conerter",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/MDViewer/demo.html\" style=\"width:100%;height:640px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Click for Show Example"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="MdViewer" class="text-right" data-static="true" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-light c-pointer w-100">
                                        <div class="reduce-1 pt-2 enlarge-1-md text-right">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-2" style="font-size:18px;">MarkDown Web Converter/Viewer</div>
                                            </div>
                                            @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                <span id="mdViewerDownload" onclick='downloadMdViewer();' class="mif-file-download ani-heartbeat pl-1 mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download MarkDown Web Viewer" />
                                            }
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div> *@

                    @* <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='openPdfWebViewer();Metro.window.create({title:"Pdf Web Viewer",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe id=\"pdfWebViewerWindow\" src=\"../Tools/PdfViewer/web/index.html\" style=\"width:100%;height:640px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Click for Show Example"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="PdfWebViewer" class="text-right" data-static="true" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-light c-pointer w-100">
                                        <div class="reduce-1 pt-2 enlarge-1-md text-right">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-2">Pdf Web Viewer</div>
                                            </div>
                                            @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                <span id="pdfWebViewerDownload" onclick='downloadPdfWebViewer();' class="mif-file-download ani-heartbeat pl-1 mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Pdf Web Viewer" />
                                            }
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div> *@

                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='openMetroPosibilities();Metro.window.create({title:"Metro4 Developer Posibilities",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../server/EASYSYSTEMBuilder_Downloads/Metro4DevHelp/Metro4Example/m4q-about.html\" style=\"width:100%;height:640px;\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Click for Show Example"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="MetroPosibilities" class="text-right" data-static="true" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("MetroPosibilities");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            3000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("MetroPosibilities");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            5000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer w-100">
                                        <div class="reduce-1 pt-2 enlarge-1-md text-right">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-2">Metro4 Developing</div>
                                            </div>
@*                                             @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                <span id="metroPosibilitiesDownload" onclick='downloadMetroPosibilities();' class="mif-file-download ani-heartbeat pl-1 mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Metro4 Dev Help" />
                                            } *@
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="pb-10 mb-10"></div>


    </window>
 
    <script>
        /* Startup */

        // Declaration
        let windowBackButton = [{ html: "<span class=\"mif-backward\"></span>", cls: "warning", onclick: "$(\"#moreEffectWindow\").attr(\"src\", \"../server/EASYSYSTEMBuilder_Downloads/Metro4DevHelp/Metro4Example/examples\")" }];
        let WpfToolBackButton = [{ html: "<span class=\"mif-backward\"></span>", cls: "warning", onclick: "$(\"#WpfToolWindow\").attr(\"src\", \"../server/Downloads/WPF_DevWindowsTools\")" }];


        // Startup Calling
        var notify = Metro.notify; notify.setup({ width: 300, duration: 5000, animation: 'easeOutBounce' });
        notify.create("Please Rate Tool After Download..."); notify.reset();
        GetGeneratorsRating();



        // Function Part
        function setGeneratorsRating(value, star, element) {
            $.ajax({
                type: "GET", url: "/Generators/SetGeneratedToolRatingName/" + element.id + "/" + value, dataType: 'json',
                headers: {
                    'Content-type': 'application/json',
                    'Authorization': 'Bearer ' + Metro.storage.getItem('ApiToken', null)
                }
            }).always(function (data) { GetGeneratorsRating(); });
            $(element).data('rating').static(true);
        };
        function GetGeneratorsRating() {
            $.get("/Generators/GetGeneratedToolRatingList").then(function (data) {
                data = JSON.parse(data);
                data.forEach(tool => {
                    switch (tool.Name) {
                        case "SystemDesktop":
                            $('#SystemDesktop').data('rating').val(tool.Rating);
                            $('#SystemDesktop').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "SystemToolDesktop":
                            $('#SystemToolDesktop').data('rating').val(tool.Rating);
                            $('#SystemToolDesktop').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "AdminDesktop":
                            $('#AdminDesktop').data('rating').val(tool.Rating);
                            $('#AdminDesktop').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "MoreEffects":
                            $('#MoreEffects').data('rating').val(tool.Rating);
                            $('#MoreEffects').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "MdViewer":
                            $('#MdViewer').data('rating').val(tool.Rating);
                            $('#MdViewer').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "PdfWebViewer":
                            $('#PdfWebViewer').data('rating').val(tool.Rating);
                            $('#PdfWebViewer').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "MetroPosibilities":
                            $('#MetroPosibilities').data('rating').val(tool.Rating);
                            $('#MetroPosibilities').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        default:
                            break;
                    }
                });
            });
        };

        async function openSystemDesktop() {
            $('#SystemDesktop').data('rating').static(false);
        };

        async function downloadSystemDesktop() {
            showPageLoading();

            var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
            notify.create("Downloading System Desktop Template..."); notify.reset();

            AuthDownloadFile("GET", "/Generators/GetSystemDesktop");
            hidePageLoading();
            $('#SystemDesktop').data('rating').static(false);
        };

        async function openSystemToolDesktop() {
            $('#SystemToolDesktop').data('rating').static(false);
        };

        async function downloadSystemToolDesktop() {
            showPageLoading();

            var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
            notify.create("Downloading System Tool Desktop Template..."); notify.reset();

            AuthDownloadFile("GET", "/Generators/GetSystemToolDesktop");
            hidePageLoading();
            $('#SystemToolDesktop').data('rating').static(false);
        };

        async function openAdminDesktop() {
            $('#AdminDesktop').data('rating').static(false);
        };

        async function downloadAdminDesktop() {
            showPageLoading();

            var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
            notify.create("Downloading Admin Desktop Template..."); notify.reset();

            AuthDownloadFile("GET", "/Generators/GetAdminDesktop");
            hidePageLoading();
            $('#AdminDesktop').data('rating').static(false);
        };

        async function openMoreEffects() {
            $('#MoreEffects').data('rating').static(false);
        };

        async function downloadMoreEffects() {
            showPageLoading();

            var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
            notify.create("Downloading Metro4 + Effects Template..."); notify.reset();

            AuthDownloadFile("GET", "/Generators/GetMoreEffects");
            hidePageLoading();
            $('#MoreEffects').data('rating').static(false);
        };

        async function openMdViewer() {
            $('#MdViewer').data('rating').static(false);
        };

        async function downloadMdViewer() {
            showPageLoading();

            var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
            notify.create("Downloading MarkDown Viewer..."); notify.reset();

            AuthDownloadFile("GET", "/Generators/GetMdViewer");
            hidePageLoading();
            $('#MdViewer').data('rating').static(false);
        };


        async function openPdfWebViewer() {
            $('#PdfWebViewer').data('rating').static(false);
        };

        async function downloadPdfWebViewer() {
            showPageLoading();

            var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
            notify.create("Downloading Pdf Web Viewer..."); notify.reset();

            AuthDownloadFile("GET", "/Generators/GetPdfWebViewer");
            hidePageLoading();
            $('#PdfWebViewer').data('rating').static(false);
        };

        async function openMetroPosibilities() {
            $('#MetroPosibilities').data('rating').static(false);
        };
        async function downloadMetroPosibilities() {
            showPageLoading();

            var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
            notify.create("Downloading Metro4 Developer Help..."); notify.reset();

            AuthDownloadFile("GET", "/Generators/GetMetroPosibilities");
            hidePageLoading();
            $('#MetroPosibilities').data('rating').static(false);
        };
        
    </script>
</div>
