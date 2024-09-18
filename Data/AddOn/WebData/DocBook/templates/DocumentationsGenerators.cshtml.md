ď»ż@page 
@model ServerCorePages.DocumentationsGeneratorsModel
@{
    ViewData["Title"] = "Project Documentations Generators";
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
            <div class=" d-flex c-help ani-hover-heartbeat fg-red" onclick="Metro.infobox.open('#VisualInfoBox');">
                Import Project Comment File XML from Visual Studio for Generate MarkDown MD documentation.
            </div>
            Import All MArkDown files With Summary File - Its signpost for Robust Inteligent HELP Documentations<br />
            Documentations are automatically generated for Download<br />
        </div>

        @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {

        }
        else {
            <div class="h4 fg-red ani-hover-heartbeat">
                For Make and Download Documentation Files from your XML/MD source files You must be Logged in
            </div>
        }

        <div class="about ">
            <div class="container">
                <div class="mt-10 text-center">
                    <div class="fg-darkSteel" style="font-weight:bold;font-size:40px;opacity:0.7;">
                        Static Pages Project Documentation GENERATORS
                    </div>
                    <p class="text-leader pl-20-md pr-20-md">
                        By click on left Icon open Example
                    </p>
                </div>

                <div class="row mt-10">
                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"MarkDownMD HTML Viewer",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/MDViewer/demo.html\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Click for Show Example"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="xmlToMdRating" class="text-right" data-static="true" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("XmlToMd");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            1000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("XmlToMd");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            2000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4">XML to MarkDown</div>
                                            </div>
                                            <div class="d-flex">
@*                                                 @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                    <input class="mt-4" type="file" data-role="file" data-on-select="uploadXmlToMd" accept=".xml" data-cls-caption="width50" data-prepend="Select your XML file" data-button-title="<span class='mif-folder mif-3x ani-ring'></span>">

                                                    <span id="xmlToMdDownload" onclick='downloadXmlToMd();' class="mif-file-download ani-heartbeat mif-3x pl-10 mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download your MD MarkDown Documentation" />
                                                } *@
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
                                <a href="#" class="unstyled-link c-pointer" onclick='openMDToHtml();Metro.window.create({title:"MD to HTML Online Converter",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/MDViewer/index.html\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Open MD to HTML Online Converter"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="mdToHtmlRating" class="text-right" data-static="true" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("MdToHtml");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            1000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("MdToHtml");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            2000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pt-7 pl-4">MD to HTML Online Converter</div>
                                            </div>
                                            <div class="d-flex">
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
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"Inteligent MD Book Example",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/EDC_ESB_InteliHelp/book/index.html\" style=\"width:100%;height:600px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Click for Show Example"><span class="mif-search"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="mdToMdBookRating" class="text-right" data-static="true" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("MdToMdBook");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            3000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("MdToMdBook");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            5000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="c-help text-left pl-4 ani-heartbeat" style="font-size:24px;" onclick="Metro.infobox.open('#MdBookInfoBox');">MarkDown to MD Book </div>
                                            <div class="d-flex">
@*                                                 @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                    <input class="mt-4" type="file" data-role="file" data-on-select="uploadMdToMdBook" accept=".md" data-cls-caption="width50" data-prepend="Select MarkDown Files" data-button-title="<span class='mif-folder mif-3x ani-ring'></span>" multiple="multiple">

                                                    <span id="mdToMdBookDownload" onclick='downloadMdToMdBook();' class="mif-file-download ani-heartbeat mif-3x pl-10 mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download your MD Book" />
                                                } *@
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


            @* Info Boxs *@
            <div id="VisualInfoBox" class="info-box" data-role="infobox" data-type="info" data-width="800">
                <span class="button square closer"></span>
                <div class="info-box-content">
                    <h5>How To Export All Comment from Visual Studio To XML</h5>
                    <p style="font-size:20px;margin-top:0px">
                        <a href="https://www.c-sharpcorner.com/article/exporting-comments-in-visual-studio/" target="_blank" title="Open Click by Click Help How to Export All project Comments">
                        Pages with Click by Click Help How to Export All project comments to XML file
                        </a>
                    </p>
                    <p style="font-size:20px;margin-top:0px">Exported XML file is source file for Easy & Pretty documentations of Projects</p>
                </div>
            </div>

            <div id="MdBookInfoBox" class="info-box" data-role="infobox" data-type="info" data-width="800">
                <span class="button square closer"></span>
                <div class="info-box-content">
                    <h5>What is Required For Generating MD Book from MD files</h5>
                    <p style="font-size:16px;margin-top:0px">
                        MD Book is generated web Pages with indexed Content For FullText Searching in All MD Files
                    </p>
                    <p style="font-size:16px;margin-top:0px">
                        Required is only file SUMMARY.md In this file are Linked All MD Files As MD Book Menu
                    </p>
                    <p style="font-size:16px;margin-top:0px">
                        Code Format is Easy:<br /><br />
                        ### Main Menu 1 Section<br />
                        ```markdown<br />
                        [MenuItemName](./SomeFile.MD)<br />
                        [MenuItemName](./NextMdFile.md)<br />
                        ```<br />
                        ---<br />
                        ### Next Main Menu 2 Section<br />
                        ```markdown<br />
                        [ThirdMenuItem](./Next.MD)<br />
                        [XXX](./other.md)<br />
                        ```<br />
                        ---<br /><br />
                    </p>
                    <p style="font-size:20px;margin-top:0px">
                        If Generation is not succes Insert 'Empty line' <br />
                        before each line in SUMMARY.MD file and 3 spaces to each end line
                    </p>

                    <p style="font-size:20px;margin-top:0px">Exported XML file is source file for Easy & Pretty documentations of Projects</p>
                </div>
            </div>
    </window>
 
    <script>
        /* Startup */

        // Declaration
        $('#xmlToMdDownload').hide(); $('#mdToMdBookDownload').hide();
        let xmlToMdLastId = null; mdToMdBookLastId = null;

        // Startup Calling
        
        var notify = Metro.notify; notify.setup({ width: 300, duration: 5000, animation: 'easeOutBounce' });
        notify.create("Please Rate Tool After Download..."); notify.reset();
        GetGeneratorsRating();



        // Function Part
        function setGeneratorsRating(value, star, element) {
            let recId;
            switch (element.id) {
                case "xmlToMdRating": recId = xmlToMdLastId; break;
                case "mdToHtmlRating": recId = "MdToHtml"; break;
                case "mdToMdBookRating": recId = mdToMdBookLastId; break;
                default:
                    break;
            }

            if (recId == "MdToHtml") {
                $.ajax({
                    type: "GET", url: "/Generators/SetGeneratedToolRatingName/" + recId + "/" + value, dataType: 'json',
                    headers: {
                        'Content-type': 'application/json',
                        'Authorization': 'Bearer ' + Metro.storage.getItem('ApiToken', null)
                    }
                }).always(function (data) { GetGeneratorsRating(); });
            } else {
                $.ajax({
                    type: "GET", url: "/Generators/SetGeneratedToolRatingList/" + recId + "/" + value, dataType: 'json',
                    headers: {
                        'Content-type': 'application/json',
                        'Authorization': 'Bearer ' + Metro.storage.getItem('ApiToken', null)
                    }
                }).always(function (data) { GetGeneratorsRating(); });
            }
            $(element).data('rating').static(true);
        };
        function GetGeneratorsRating() {
            $.get("/Generators/GetGeneratedToolRatingList").then(function (data) {
                data = JSON.parse(data);
                data.forEach(tool => {
                    switch (tool.Name) {
                        case "XmlToMd":
                            $('#xmlToMdRating').data('rating').val(tool.Rating);
                            $('#xmlToMdRating').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "MdToHtml":
                            $('#mdToHtmlRating').data('rating').val(tool.Rating);
                            $('#mdToHtmlRating').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "MdToMdBook":
                            $('#mdToMdBookRating').data('rating').val(tool.Rating);
                            $('#mdToMdBookRating').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        default:
                            break;
                    }
                });
            });
        };


        async function uploadXmlToMd(files,element) {
            if (files.length > 0) {
                showPageLoading();
                let dataset = [];

                for (let i = 0; i < files.length; i++) {
                    const reader = new FileReader();
                    let fileContent = await new Promise((resolve, reject) => {
                        const reader = new FileReader()
                        reader.onloadend = () => resolve(reader.result)
                        reader.onerror = reject
                        reader.readAsDataURL(files[i])
                    });
                    dataset.push({ name: files[i].name, extension: files[i].name.split('.').pop(), fileArray: fileContent });
                }

                var def = $.ajax({
                    global: false, type: "POST", url: "/Generators/GenerateXmlToMd", dataType: 'json',
                    headers: {
                        'Content-type': 'application/json',
                        "Authorization": 'Bearer ' + Metro.storage.getItem('ApiToken', null)
                    },
                    data: JSON.stringify({ Files: dataset })
                });

                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Uploading XML file for generate MD MarkDown..."); notify.reset();

                def.fail(function (data) {
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Download MD MarkDown documentation Failed", "Alert", { cls: "alert" }); notify.reset();
                    $('#xmlToMdDownload').hide();
                    hidePageLoading();
                });

                def.done(function (data) {
                    xmlToMdLastId = data.genRecord;
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Your MD MarkDown documentation was Generated succesfully."); notify.reset();
                    $('#xmlToMdDownload').show();
                    hidePageLoading();
                    $('#xmlToMdRating').data('rating').static(false);
                });
            } else {
                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Any XML was not Selected", "Info"); notify.reset();
                $('#xmlToMdDownload').hide();
            }
        };
        async function downloadXmlToMd() {
            showPageLoading();

            var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
            notify.create("Downloading MD MarkDown Documentation File..."); notify.reset();

            AuthDownloadFile("GET", "/Generators/GetGeneratedXmlToMd");
            hidePageLoading();
        };

        async function openMDToHtml() {
            $('#mdToHtmlRating').data('rating').static(false);
        };

        async function uploadMdToMdBook(files) {
            if (files.length > 0) {
                showPageLoading();
                let dataset = [];

                for (let i = 0; i < files.length; i++) {
                    const reader = new FileReader();
                    let fileContent = await new Promise((resolve, reject) => {
                        const reader = new FileReader()
                        reader.onloadend = () => resolve(reader.result)
                        reader.onerror = reject
                        reader.readAsDataURL(files[i])
                    });
                    dataset.push({ name: files[i].name, extension: files[i].name.split('.').pop(), fileArray: fileContent });
                }

                var def = $.ajax({
                    global: false, type: "POST", url: "/Generators/GenerateMdToMdBook", dataType: 'json',
                    headers: {
                        'Content-type': 'application/json',
                        "Authorization": 'Bearer ' + Metro.storage.getItem('ApiToken', null)
                    },
                    data: JSON.stringify({ Files: dataset })
                });

                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Uploading MD files for generate MD Book..."); notify.reset();

                def.fail(function (data) {
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Download MD Book Failed", "Alert", { cls: "alert" }); notify.reset();
                    $('#mdToMdBookDownload').hide();
                    hidePageLoading();
                });

                def.done(function (data) {
                    mdToMdBookLastId = data.genRecord;
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Your MD Book was Generated succesfully."); notify.reset();
                    $('#mdToMdBookDownload').show();
                    hidePageLoading();
                    $('#mdToMdBookRating').data('rating').static(false);
                });
            } else {
                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Any MD file was not Selected", "Info"); notify.reset();
                $('#GenerateMdToMdBook').hide();
            }
        };
        async function downloadMdToMdBook() {
            showPageLoading();

            var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
            notify.create("Downloading MD Book..."); notify.reset();

            AuthDownloadFile("GET", "/Generators/GetGeneratedMdToMdBook");
            hidePageLoading();
        };

       
    </script>
</div>
