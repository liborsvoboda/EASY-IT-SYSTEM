ď»ż@page
@model ServerCorePages.SolutionServersModel
@{
    ViewData["Title"] = "Solution Servers";
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
            All Servers are for Test with Trial 30 Days License<br />
            You can Buy Full Server/Client Project Code for Unlimited Customizing Server<br />
            All Solutions are NETCORE6 + EF with Linux, Windows, MacOS & Multi DB Supported
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
                        Solution Servers Downloads
                    </div>
                    <p class="text-leader pl-20-md pr-20-md">
                        By click on Icons open Server Info
                    </p>
                </div>

                <div class="row mt-10">
                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"Project Management Installation Info",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../server/Downloads/ProjectManagement/ExpertDocViewer/\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="ProjectManagement" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("ProjectManagement");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Unlimited License">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            3000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("ProjectManagement");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full NetCore6 C# Server/Client Project">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            10 000KÄŤ
                                        </div>

                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4">Project Management</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                <span onclick="window.open('../server/Downloads/ProjectManagement','_blank');" class="mif-file-download ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Project Management"></span>
                                                <span onclick='Metro.window.create({title:"Project Management ScreenShot & Video",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../server/Downloads/ProjectManagement/Gallery\" style=\"width:100%;height:650px\"></iframe>"});' class="mif-eye ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Show ScreenShot"></span>
                                                <span onclick="window.open('http://kliknetezde.cz:5001','_blank');" class="mif-local-service ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Project Management Online Test<br/>username: test@test.com<br/>password: tester"></span>
                                                <span onclick='generateTrialLicense("ProjectManagement");' class="mif-document-file-key ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Trial License"></span>
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
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"EASY-DATA-Center Installation Info",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/MDViewer/EASYDATACenter.html\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="EasyDataCenter" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("EASYDATACenter");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Unlimited License">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            10 000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("EASYDATACenter");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full NetCore6 C# Server/Client Project">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            30 000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4">EASYDATACenter</div>
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
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"Expert Documentation Editor",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/ExpertDocManager/Docs/ExportCs.html\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="ExpertDocManager" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("ExpertDocManager");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Unlimited License">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            5 000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("ExpertDocManager");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full NetCore6 C# Server/Client Project">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            10 000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4">ExpertDocManager</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                <span onclick="window.open('../server/Downloads/ExpertDocManager','_blank');" class="mif-file-download ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download ExpertDocManager"></span>
                                                <span onclick='Metro.window.create({title:"ExpertDocManager Short Example",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/ExpertDocManager/docs/ExpertDocManagerEn.html\" style=\"width:100%;height:650px\"></iframe>"});' class="mif-eye ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Show Fast Example"></span>
                                                <span onclick="window.open('/Tools/ExpertDocManager/','_blank');" class="mif-local-service ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Online ExpertDocManager in FilesManager Mode"></span>
                                                <span onclick="window.open('/Tools/ExpertDocViewer/','_blank');" class="mif-local-service ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Online ExpertDocViewer in FilesManager Mode"></span>
                                                <span onclick='generateTrialLicense("ExpertDocManager");' class="mif-document-file-key ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Trial License"></span>
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
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"Monitoring Server Info",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/MDViewer/EASYDATACenter.html\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="MonitServer" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("MonitServer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Unlimited License">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            5 000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("MonitServer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full NetCore6 C# Server/Client Project">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            10 000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4">MonitServer</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                <span onclick="window.open('../server/Downloads/MonitServer','_blank');" class="mif-file-download ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download EASYDATACenter"></span>
                                                <span onclick='Metro.window.create({title:"MonitServer ScreenShots",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../server/EASYDATACenter_Downloads/PhotoGallery\" style=\"width:100%;height:650px\"></iframe>"});' class="mif-eye ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Show ScreenShot"></span>
                                                <span onclick="window.open('http://kliknetezde.cz','_blank');" class="mif-local-service ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Online Server Client EDCManager is on Desktop"></span>
                                                <span onclick='generateTrialLicense("MonitServer");' class="mif-document-file-key ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Trial License"></span>
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
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"Expert Documentation Server",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/ExpertDocManager/Docs/ExportCs.html\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="ExpertDocServer" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("ExpertDocServer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Unlimited License">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            10 000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("ExpertDocServer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full NetCore6 C# Server/Client Project">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            20 000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4">ExpertDocServer</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                <span onclick="window.open('../server/Downloads/ExpertDocManager','_blank');" class="mif-file-download ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download ExpertDocServer"></span>
                                                <span onclick='Metro.window.create({title:"ExpertDocServer Short Example",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/ExpertDocManager/docs/ExpertDocManagerEn.html\" style=\"width:100%;height:650px\"></iframe>"});' class="mif-eye ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Show Fast Example"></span>
                                                <span onclick="window.open('/Tools/ExpertDocManager/','_blank');" class="mif-local-service ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Online ExpertDocManager in FilesManager Mode"></span>
                                                <span onclick="window.open('/Tools/ExpertDocViewer/','_blank');" class="mif-local-service ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Online ExpertDocViewer in FilesManager Mode"></span>
                                                <span onclick='generateTrialLicense("ExpertDocServer");' class="mif-document-file-key ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Trial License"></span>
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
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"Private Git Server Installation Info",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../server/Downloads/GitServer/ExpertDocViewer/\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Installation Info"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="PrivateGitServer" class="text-right" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("PrivateGitServer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Unlimited License">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            3 000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("PrivateGitServer");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full NetCore6 C# Server/Client Project">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            5 000KÄŤ
                                        </div>
                                    </div>
                                    <div class="text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-block w-100">
                                                <div class="text-left pl-4">MultiLingual Private Git Server with<br />Branches,Sharing,Public,Translation</div>
                                            </div>
                                            <div class="d-flex w-100">
                                                <span onclick="window.open('../server/Downloads/GitServer','_blank');" class="mif-file-download ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download ExpertDocServer"></span>
                                                <span onclick='Metro.window.create({title:"Private Git Server Screenshots & Video",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../server/Downloads/GitServer/Gallery\" style=\"width:100%;height:650px\"></iframe>"});' class="mif-eye ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Show Private Git Server Screenshots & Video"></span>
                                                <span onclick="window.open('https://Kliknetezde.cz:5002','_blank');" class="mif-local-service ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Online Private Git Server"></span>
                                                <span onclick='generateTrialLicense("PrivateGitServer");' class="mif-document-file-key ani-heartbeat mif-3x mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download Trial License"></span>
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
                        case "ProjectManagement":
                            $('#ProjectManagement').data('rating').val(tool.Rating);
                            $('#ProjectManagement').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "EasyDataCenter":
                            $('#EasyDataCenter').data('rating').val(tool.Rating);
                            $('#EasyDataCenter').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "ExpertDocManager":
                            $('#ExpertDocManager').data('rating').val(tool.Rating);
                            $('#ExpertDocManager').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "MonitServer":
                            $('#MonitServer').data('rating').val(tool.Rating);
                            $('#MonitServer').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "ExpertDocServer":
                            $('#ExpertDocServer').data('rating').val(tool.Rating);
                            $('#ExpertDocServer').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "PrivateGitServer":
                            $('#PrivateGitServer').data('rating').val(tool.Rating);
                            $('#PrivateGitServer').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
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
