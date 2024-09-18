ď»ż@page
@model ServerCorePages.AdvantageousServicesModel
@{
    ViewData["Title"] = "Advantageous Services";
}
@*
    https://metroui.org.ua/intro.html
    https://metroui.org.ua/position.html
    https://www.c-sharpcorner.com/article/custom-login-register-with-identity-in-asp-net-core-3-1/
    *@

<div class="text-center info-panel mb-2">
    <window>
        <div class="h4 fg-darkOrange" style="top:10px;">
            EASY contact for Move Forward:<br />
            <div class="h2 d-block c-help ani-hover-heartbeat" style="font-weight:bolder;color: #155f82;" onclick="Metro.infobox.open('#ContactBox');" title="Contact List">
                <div class="">Phone / Email / Skype / Messenger/ WhatsApp / Signal / Message</div>
                <div class="" >LinkeId / FaceBook / Web / Portal / OnLine / GitHub / DataBox </div>
            </div>
            Easy and Fast GroupWare (Multi) Solutions are my Basic Views in solve of IT task <br />
            Here Are OneTime - Paid Services List For Fast Modern Multiple Solutions<br />

        </div>

        <div class="about ">
            <div class="container">
                <div class="mt-10 text-center">
                    <div class="fg-darkSteel" style="font-weight:bold;font-size:20px;opacity:0.7;">
                        Static Pages / NoBuild Pages / JS-TS Systems / Servers / Services / Support / HelpDesk / Remote Help
                    </div>
                    <p class="text-leader pl-20-md pr-20-md">
                        By click on Icons open Examples
                    </p>
                </div>

                <div class="row mt-10">
                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"Price List",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/EDC_ESB_InteliHelp/book/PriceList.html\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Price List"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-1x" style="top: 0px;right:0px;">
                                        <input id="HourPrice" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("HourPrice");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="LongTerm Projects Price ">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            500KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("HourPrice");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Any Support Service Price">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            750KÄŤ
                                        </div>

                                    </div>
                                    <div class="text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-block w-100">
                                                <div class="text-left pl-2">LongTerm Projects Work / Cooperation <br />IT Online Support / Remote HelpDesk</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                <span onclick="window.open('https://GroupWare-Solution.Eu','_blank');" class="mif-eye ani-heartbeat mif-3x mif-3x ml-2" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Detailed Situation Descriptions / Technologies / Solutions"></span>
                                                <span onclick="window.open('https://KlikneteZde.Cz','_blank');" class="mif-eye ani-heartbeat mif-3x mif-3x ml-2" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Online EDC & ESB Solutions & Clones"></span>
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
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"Expert Documentation Manager Multilanguage Implementation",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/ExpertDocManager\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-1x" style="top: 0px;right:0px;">
                                        <input id="OnlineWebtranslation" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("OnlineWebtranslation");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Implementation of Online Any Web Pages unlimited Translation">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            5000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("OnlineWebtranslation");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Solution for Distribute Online Any Web Pages unlimited Translation">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            10000KÄŤ
                                        </div>
                                    </div>
                                    <div class="text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-block w-100">
                                                <div class="text-left pl-2">Implementing in 1-3 days <br />Any Web Online Auto Translation</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                <span onclick='Metro.window.create({title:"Free Expert Documentation Viewer Multilanguage Implementation",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/ExpertDocViewer\" style=\"width:100%;height:650px\"></iframe>"});' class="mif-eye ani-heartbeat mif-3x mif-3x ml-2" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Show Free Expert Documentation Viewer Multilanguage Implementation Example"></span>

                                                <span onclick="window.open('https://KlikneteZde.Cz:5001','_blank');" class="mif-eye ani-heartbeat mif-3x mif-3x ml-2" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Show Project Management Server Multilanguage Implementation Example username/password: admin@admin.eu/admin"></span>
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
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"EASY-DATA-Center Dev Info",shadow:true,draggable:true,customButtons:CenterBackButton,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe id=\"CenterWindow\" src=\"../server/EASYDATACenter_Downloads\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-1x" style="top: 0px;right:0px;">
                                        <input id="EasyDataCenterPartner" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("EasyDataCenterPartner");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Unlimited Partner Distribution EASYDATACenter License">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            20 000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("EasyDataCenterPartner");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Unlimited Independent Distribution EASYDATACenter License">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            100 000KÄŤ
                                        </div>
                                    </div>
                                    <div class="text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-block w-100">
                                                <div class="text-left pl-2"><b>EASY-DATA-Center</b> Partner License <br />Full Independent Distribution License</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                <span onclick="window.open('../server/Downloads/EASYDATACenter','_blank');" class="mif-file-download ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download EASYDATACenter"></span>
                                                <span onclick='Metro.window.create({title:"EasyDataCenter ScreenShots",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../server/EASYDATACenter_Downloads/PhotoGallery\" style=\"width:100%;height:650px\"></iframe>"});' class="mif-eye ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Show ScreenShot"></span>
                                                <span onclick="window.open('http://kliknetezde.cz','_blank');" class="mif-local-service ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Online Server Client EDCManager is on Desktop"></span>
                                                <span onclick='generateTrialLicense("EasyDataCenter");' class="mif-document-file-key ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Trial License"></span>
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
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"EASY-SYSTEM-Builder Dev Info",shadow:true,draggable:true,customButtons:BuilderBackButton,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe  id=\"BuilderWindow\" src=\"../server/EASYSYSTEMBuilder_Downloads\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-1x" style="top: 0px;right:0px;">
                                        <input id="EasySystemBuilderPartner" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("EasySystemBuilderPartner");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Unlimited Partner Distribution EASYSYSTEMBuilder License">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            20 000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("EasySystemBuilderPartner");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Unlimited Independent Distribution EASYSYSTEMBuilder License">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            100 000KÄŤ
                                        </div>
                                    </div>
                                    <div class="text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-block w-100">
                                                <div class="text-left pl-2"><b>EASY-SYSTEM-Builder</b> Partner License <br />Full Independent Distribution License</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                <span onclick="window.open('../server/Downloads','_blank');" class="mif-file-download ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Any Server Client - Its ESB Clone"></span>
                                                <span onclick='Metro.window.create({title:"EasySystemBuilder ScreenShots",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../server/EASYSYSTEMBuilder_Downloads/PhotoGallery\" style=\"width:100%;height:650px\"></iframe>"});' class="mif-eye ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Show ScreenShot"></span>
                                                <span onclick="window.open('http://kliknetezde.cz','_blank');" class="mif-local-service ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Online Server Client Global Version & EDCManager is on Desktop"></span>
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


            <div id="ContactBox" class="info-box" data-role="infobox" data-type="info" data-width="800">
                <span class="button square closer"></span>
                <div class="info-box-content">
                    <h3>Libor Svoboda Contact Posibilities</h3>
                    <p style="font-size:13px;margin-top:0px">I working Every Day / Full day from Home. Therefore a i can find time for you almost any time.</p>
                    <p style="font-size:13px;margin-top:0px">Everything you see here is the result of one man hobby [transition to new technologies] less than 1 Year in spare time</p>
                    <p style="font-size:13px;margin-top:0px">
                        After 33 years daily pro-Active work in IT branch over 17 supra-national companies <br />
                        I don't know a problem I can't solve, come up with a solution as I go, give advice on anything
                    </p>

                    <ul>
                        <li class="c-pointer ani-hover-horizontal fg-indigo" onclick="window.open('tel:+420724986873','_blank');">Phone: +420 724 986 873</li>
                        <li>Email: <div class="d-inline-flex c-pointer ani-hover-horizontal fg-indigo" onclick="window.open('mailto:Libor.Svoboda@GroupWare-Solution.Eu','_blank');"> Libor.Svoboda@GroupWare-Solution.Eu</div> - <div class="d-inline-flex c-pointer ani-hover-horizontal fg-indigo" onclick="window.open('mailto:Libor.Svoboda@KlikneteZde.Cz','_blank');">Libor.Svoboda@KlikneteZde.Cz</div></li>
                        <li class="c-pointer ani-hover-horizontal fg-indigo" onclick="window.open('skype:Chrasty80','_blank');">Skype: Chrasty80</li>
                        <li>Messenger: <div class="d-inline-flex c-pointer ani-hover-horizontal fg-indigo" onclick="window.open('http://m.me/GroupWareSolution','_blank');">GroupWareSolution</div> - <div class="d-inline-flex c-pointer ani-hover-horizontal fg-indigo" onclick="window.open('http://m.me/LiborSvoboda80','_blank');">LiborSvoboda80</div></li>
                        <li class="c-pointer ani-hover-horizontal fg-indigo" onclick="window.open('https://api.whatsapp.com/send?phone=00420724986873','_blank');">WhatsApp</li>
                        <li class="c-pointer ani-hover-horizontal fg-indigo" onclick="window.open('https://signal.me/#p/+420724986873 ','_blank');">Signal liborsvoboda</li>
                        <li class="c-pointer ani-hover-horizontal fg-indigo" onclick="ShowMessagePanel()">Write Portal Message</li>
                    </ul>
                    <hr/>
                    <ul>
                        <li class="c-pointer ani-hover-horizontal fg-indigo" onclick="window.open('https://www.linkedin.com/in/libor-svoboda-7b96014a/','_blank');">LinkedIn libor-svoboda-7b96014a</li>
                        <li>FaceBook: <div class="d-inline-flex c-pointer ani-hover-horizontal fg-indigo" onclick="window.open('https://www.facebook.com/GroupWareSolution/','_blank');"> GroupWareSolution</div> - <div class="d-inline-flex c-pointer ani-hover-horizontal fg-indigo" onclick="window.open('https://www.facebook.com/LiborSvoboda80/','_blank');">LiborSvoboda80</div></li>
                        <li class="c-pointer ani-hover-horizontal fg-indigo" onclick="window.open('https://Groupware-Solution.eu','_blank');">Web Groupware-Solution.eu</li>
                        <li class="c-pointer ani-hover-horizontal fg-indigo" onclick="window.open('https://KlikneteZde.Cz:5000','_blank');">Portal KlikneteZde.Cz</li>
                        <li class="c-pointer ani-hover-horizontal fg-indigo" onclick="window.open('https://KlikneteZde.Cz','_blank');">Online KlikneteZde.Cz</li>
                        <li class="c-pointer ani-hover-horizontal fg-indigo" onclick="window.open('https://github.com/liborsvoboda?tab=repositories','_blank');">GitHub liborsvoboda</li>
                        <li class="c-pointer ani-hover-horizontal fg-indigo" onclick="window.open('https://www.mojedatovaschranka.cz/','_blank');">DataBox ffbd3at</li>
                    </ul>
                    <hr />
                    <ul>
                        <li class="c-pointer ani-hover-horizontal fg-indigo">
                            IT Support/HelpDesk, Administrator, Tester, Analytic, Programmer, Developer, Manager, Architect
                        </li>
                        <li class="c-pointer ani-hover-horizontal fg-indigo">Libor Svoboda</li>
                        <li class="c-pointer ani-hover-horizontal fg-indigo">Ĺ˝lutava 173</li>
                        <li class="c-pointer ani-hover-horizontal fg-indigo">763 61 Ĺ˝lutava, Zlinsko</li>
                        <li class="c-pointer ani-hover-horizontal fg-indigo">Czech Republic</li>
                    </ul>
                </div>
            </div>


    </window>

    <script>
        /* Startup */

        // Declaration
        let CenterBackButton = [{ html: "<span class=\"mif-backward\"></span>", cls: "warning", onclick: "$(\"#CenterWindow\").attr(\"src\", \"../server/EASYDATACenter_Downloads\")" }];
        let BuilderBackButton = [{ html: "<span class=\"mif-backward\"></span>", cls: "warning", onclick: "$(\"#BuilderWindow\").attr(\"src\", \"../server/EASYSYSTEMBuilder_Downloads\")" }];

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
                        case "HourPrice":
                            $('#HourPrice').data('rating').val(tool.Rating);
                            $('#HourPrice').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "OnlineWebtranslation":
                            $('#OnlineWebtranslation').data('rating').val(tool.Rating);
                            $('#OnlineWebtranslation').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "EasyDataCenterPartner":
                            $('#EasyDataCenterPartner').data('rating').val(tool.Rating);
                            $('#EasyDataCenterPartner').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "EasySystemBuilderPartner":
                            $('#EasySystemBuilderPartner').data('rating').val(tool.Rating);
                            $('#EasySystemBuilderPartner').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
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
