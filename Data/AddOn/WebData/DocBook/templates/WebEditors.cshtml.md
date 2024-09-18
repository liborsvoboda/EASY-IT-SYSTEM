ď»ż@page
@model ServerCorePages.WebEditorsModel
@{
    ViewData["Title"] = "Web Editors";
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
                        Static Pages - Editors & Managers & Builders Tools
                    </div>
                    <p class="text-leader pl-20-md pr-20-md">
                        By click on Icons open Tool Info
                    </p>
                </div>

                <div class="row mt-10">
                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"Simple Code Editor Static Pages",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/SimpleCodeEditor/website/dist\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="SimpleCodeEditor" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("SimpleCodeEditor");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            500KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("SimpleCodeEditor");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            1000KÄŤ
                                        </div>

                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4">SimpleCodeEditor</div>
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
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"SQL Builder Tool Static Pages",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/SqlBuilder\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="SqlBuilder" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("SqlBuilder");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            1000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("SqlBuilder");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            3000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4">SqlBuilder</div>
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
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"Expert Documentation Manager + Server + Viewer + Fullext Search WebPages",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/ExpertDocManager\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="ExpertDocManager" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("ExpertDocManager");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            5000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("ExpertDocManager");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            10000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4">ExpertDocManager</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                <span onclick='Metro.window.create({title:"EasyDataCenter ScreenShots",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../tools/ExpertDocManager/docs/ExpertDOCManagerEn.html\" style=\"width:100%;height:650px\"></iframe>"});' class="mif-eye ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Show Example"></span>
                                                <span onclick='Metro.window.create({title:"EasyDataCenter ScreenShots",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../tools/ExpertDocManager/docs/ExportCs.html\" style=\"width:100%;height:650px\"></iframe>"});' class="mif-eye ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Automatic Translate"></span>
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
                        case "SimpleCodeEditor":
                            $('#SimpleCodeEditor').data('rating').val(tool.Rating);
                            $('#SimpleCodeEditor').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "ExpertDocManager":
                            $('#ExpertDocManager').data('rating').val(tool.Rating);
                            $('#ExpertDocManager').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "SqlBuilder":
                            $('#SqlBuilder').data('rating').val(tool.Rating);
                            $('#SqlBuilder').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
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





    </script>
</div>
