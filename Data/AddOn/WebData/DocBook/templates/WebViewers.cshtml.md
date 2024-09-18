ď»ż@page
@model ServerCorePages.WebViewersModel
@{
    ViewData["Title"] = "Web Viewers";
}
@*
    https://metroui.org.ua/intro.html
    https://metroui.org.ua/position.html
    https://www.c-sharpcorner.com/article/custom-login-register-with-identity-in-asp-net-core-3-1/
    *@

<div class="text-center info-panel mb-2">
    <window>
        <div class="h4 fg-darkOrange" style="top:10px;">
            EASY to USE:<br />
            Read the Installation description.<br />
            Look on Online Tool. For Next using is only COPY. All Problem Solved<br />
            All Tools are Static Pages, Run by Click on index.html only<br />

        </div>

        @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {

        }
        else {
            <div class="h4 fg-red ani-hover-heartbeat">
                For Download License You must be Logged in
            </div>
        }

        <div class="about ">
            <div class="container">
                <div class="mt-10 text-center">
                    <div class="fg-darkSteel" style="font-weight:bold;font-size:40px;opacity:0.7;">
                        Static Pages Web Viewers Tools
                    </div>
                    <p class="text-leader pl-20-md pr-20-md">
                        By click on Icons open Tool Info
                    </p>
                </div>

                <div class="row mt-10">
                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='openMdViewer();Metro.window.create({title:"Markdown Web Viewer and Conerter",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/MDViewer/demo.html\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="MarkDownViewer" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("MarkDownViewer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            1000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("MarkDownViewer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            2000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4">MarkDownViewer</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                @* <span onclick="window.open('../server/Downloads/ProjectManagement','_blank');" class="mif-file-download ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Project Management"></span>
                                                <span onclick='Metro.window.create({title:"Project Management ScreenShot",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../server/Downloads/ProjectManagement/Gallery\" style=\"width:100%;height:650px\"></iframe>"});' class="mif-eye ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Show ScreenShot"></span>
                                                <span onclick="window.open('http://kliknetezde.cz:5001','_blank');" class="mif-local-service ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Project Management Online Test<br/>username: test@test.com<br/>password: tester"></span> *@

                                                @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                }
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='openPdfWebViewer();Metro.window.create({title:"Pdf Web Viewer",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/PdfViewer/web/index.html\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="PdfWebViewer" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("PdfWebViewer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            1000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("PdfWebViewer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            3000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4">PdfWebViewer</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                @* <span onclick="window.open('../server/Downloads/EASYDATACenter','_blank');" class="mif-file-download ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download EASYDATACenter"></span>
                                                <span onclick='Metro.window.create({title:"EasyDataCenter ScreenShots",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../server/EASYDATACenter_Downloads/PhotoGallery\" style=\"width:100%;height:650px\"></iframe>"});' class="mif-eye ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Show ScreenShot"></span>
                                                <span onclick="window.open('http://kliknetezde.cz','_blank');" class="mif-local-service ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Online Server Client EDCManager is on Desktop"></span>
                                                <span onclick='generateTrialLicense("EasyDataCenter");' class="mif-document-file-key ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Trial License"></span> *@
                                                @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                }
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='openExcelViewer();Metro.window.create({title:"Excel Viewer",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/ExcelViewer/docs\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="ExcelViewer" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("ExcelViewer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            1000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("ExcelViewer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            2000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4">ExcelViewer</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                @* <span onclick="window.open('../server/Downloads/EASYDATACenter','_blank');" class="mif-file-download ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download EASYDATACenter"></span>
                                                
                                                <span onclick="window.open('http://kliknetezde.cz','_blank');" class="mif-local-service ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Online Server Client EDCManager is on Desktop"></span>
                                                <span onclick='generateTrialLicense("EasyDataCenter");' class="mif-document-file-key ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Trial License"></span> *@
                                                @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                }
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                     <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='openExpertDocViewer();Metro.window.create({title:"Expert Documentation Viewer",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/ExpertDocViewer\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="ExpertDocViewer" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("ExpertDocViewer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            1000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("ExpertDocViewer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            2000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4">ExpertDocViewer</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                @* <span onclick="window.open('../server/Downloads/EASYDATACenter','_blank');" class="mif-file-download ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download EASYDATACenter"></span>

                                                <span onclick="window.open('http://kliknetezde.cz','_blank');" class="mif-local-service ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Online Server Client EDCManager is on Desktop"></span>
                                                <span onclick='generateTrialLicense("EasyDataCenter");' class="mif-document-file-key ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Trial License"></span> *@
                                                @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                }
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='openWebCharts();Metro.window.create({title:"WebCharts",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/ChartJs/public\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="WebCharts" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("WebCharts");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            1000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("WebCharts");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            2000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4">WebCharts</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                @* <span onclick="window.open('../server/Downloads/EASYDATACenter','_blank');" class="mif-file-download ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download EASYDATACenter"></span>

                                                <span onclick="window.open('http://kliknetezde.cz','_blank');" class="mif-local-service ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Online Server Client EDCManager is on Desktop"></span>
                                                <span onclick='generateTrialLicense("EasyDataCenter");' class="mif-document-file-key ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Trial License"></span> *@
                                                @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                }
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='openMsOfficeViewer();Metro.window.create({title:"Ms Office Web Viewer",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/MsOfficeViewer/dist\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="MsOfficeViewer" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("MsOfficeViewer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            2000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("MsOfficeViewer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            3000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4">MsOfficeViewer</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                @* <span onclick="window.open('../server/Downloads/EASYDATACenter','_blank');" class="mif-file-download ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download EASYDATACenter"></span>

                                                <span onclick="window.open('http://kliknetezde.cz','_blank');" class="mif-local-service ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Online Server Client EDCManager is on Desktop"></span>
                                                <span onclick='generateTrialLicense("EasyDataCenter");' class="mif-document-file-key ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Trial License"></span> *@
                                                @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                }
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='openPanoramaViewer();Metro.window.create({title:"Panorama Viewer",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/PanoramaViewer/\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="PanoramaViewer" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("PanoramaViewer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            1000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("PanoramaViewer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            2000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4">PanoramaViewer</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                @* <span onclick="window.open('../server/Downloads/EASYDATACenter','_blank');" class="mif-file-download ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download EASYDATACenter"></span>

                                                <span onclick="window.open('http://kliknetezde.cz','_blank');" class="mif-local-service ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Online Server Client EDCManager is on Desktop"></span>
                                                <span onclick='generateTrialLicense("EasyDataCenter");' class="mif-document-file-key ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Trial License"></span> *@
                                                @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                }
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='openXmlDataViewer();Metro.window.create({title:"Xml Data Viewer",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/XmlDataViewer/resources/html\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="XmlDataViewer" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("XmlDataViewer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            1000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("XmlDataViewer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            2000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4">XmlDataViewer</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                @* <span onclick="window.open('../server/Downloads/EASYDATACenter','_blank');" class="mif-file-download ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download EASYDATACenter"></span>

                                                <span onclick="window.open('http://kliknetezde.cz','_blank');" class="mif-local-service ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Online Server Client EDCManager is on Desktop"></span>
                                                <span onclick='generateTrialLicense("EasyDataCenter");' class="mif-document-file-key ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Trial License"></span> *@
                                                @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                }
                                            </div>
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


        // Startup Calling

        var notify = Metro.notify; notify.setup({ width: 300, duration: 5000, animation: 'easeOutBounce' });
        notify.create("Please Rate Tool After Download..."); notify.reset();
        GetGeneratorsRating();



        // Function Part
        function setGeneratorsRating(value, star, element) {
            let recId;
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
                        case "MarkDownViewer":
                            $('#MarkDownViewer').data('rating').val(tool.Rating);
                            $('#MarkDownViewer').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "PdfWebViewer":
                            $('#PdfWebViewer').data('rating').val(tool.Rating);
                            $('#PdfWebViewer').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "ExpertDocViewer":
                            $('#ExpertDocViewer').data('rating').val(tool.Rating);
                            $('#ExpertDocViewer').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "ExcelViewer":
                            $('#ExcelViewer').data('rating').val(tool.Rating);
                            $('#ExcelViewer').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                          case "WebCharts":
                            $('#WebCharts').data('rating').val(tool.Rating);
                            $('#WebCharts').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;                          
                          case "MsOfficeViewer":
                            $('#MsOfficeViewer').data('rating').val(tool.Rating);
                            $('#MsOfficeViewer').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                          case "PanoramaViewer":
                            $('#PanoramaViewer').data('rating').val(tool.Rating);
                            $('#PanoramaViewer').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                          case "XmlDataViewer":
                            $('#XmlDataViewer').data('rating').val(tool.Rating);
                            $('#XmlDataViewer').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;

                        default:
                            break;
                    }
                });
            });
        };


        async function generateTrialLicense(project) {
            if (IsLogged()) {
                showPageLoading();

                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Downloading " + project + " Trial License..."); notify.reset();

                AuthDownloadFile("GET", "LicenseRequest/Trial/" + project);
                hidePageLoading();
            } else {
                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("You must Login First", "Alert", { cls: "alert" }); notify.reset();
            }
        };

        async function openMdViewer() {
            $('#MarkDownViewer').data('rating').static(false);
        };

        async function openPdfWebViewer() {
            $('#PdfWebViewer').data('rating').static(false);
        };

        async function openExcelViewer() {
            $('#ExcelViewer').data('rating').static(false);
        };

        async function openExpertDocViewer() {
            $('#ExpertDocViewer').data('rating').static(false);
        };

        async function openWebCharts() {
            $('#WebCharts').data('rating').static(false);
        };        
        
        async function openMsOfficeViewer() {
            $('#MsOfficeViewer').data('rating').static(false);
        };  
        async function openPanoramaViewer() {
            $('#PanoramaViewer').data('rating').static(false);
        };  
        async function openXmlDataViewer() {
            $('#XmlDataViewer').data('rating').static(false);
        };  
        
        
    </script>
</div>
