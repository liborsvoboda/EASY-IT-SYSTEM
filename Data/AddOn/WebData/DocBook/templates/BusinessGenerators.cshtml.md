ď»ż@page 
@model ServerCorePages.BusinessGeneratorsModel
@{
    ViewData["Title"] = "Business Generators";
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
            Import Image Files for Presentations only.<br />
            Presentations are automatically generated for Download<br />
            Download, Unpack and Copy to your WebPages.<br />
            You show Presentations by Index.html<br />
        </div>

        @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {

        }
        else {
            <div class="h4 fg-red ani-hover-heartbeat">
                For Make and Download Presentations You must be Logged in
            </div>
        }

        <div class="about ">
            <div class="container">
                <div class="mt-10 text-center">
                    <div class="fg-darkSteel" style="font-weight:bold;font-size:40px;opacity:0.7;">
                        Static Pages - Business Presentation GENERATORS
                    </div>
                    <p class="text-leader pl-20-md pr-20-md">
                        By click on left Icon open Example
                    </p>
                </div>

                <div class="row mt-10">
                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"Image Book",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/Book/demo/index.html\" style=\"width:100%;height:650px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Click for Show Example"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="imageBookRating" class="text-right" data-static="true" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("ImageBook");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            1000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("ImageBook");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            2000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Single od Double Page can be set">Image Book</div>
                                            </div>
                                            <div class="d-flex">
                                                @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                    <input class="mt-4" type="file" data-role="file" data-on-select="uploadImageBook" accept=".png,.jpg,.jpeg,.tiff" data-cls-caption="width50" data-prepend="Select your Images" data-button-title="<span class='mif-folder mif-3x ani-ring'></span>" multiple="multiple">

                                                    <span id="imageBookDownload" onclick='downloadImageBook();' class="mif-file-download ani-heartbeat mif-3x pl-10 mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download your Image Book" />
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
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"Medial Presentation",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../server/Media/Presentation/index.html\" style=\"width:100%;height:600px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Click for Show Example"><span class="mif-search"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="medialPresentationRating" class="text-right" data-static="true" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("MedialPresentation");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            3000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("MedialPresentation");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            5000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="c-help text-left pl-4 ani-heartbeat" onclick="Metro.infobox.open('#PresentationInfoBox');">Medial Presentation </div>
                                            <div class="d-flex">
                                                @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                    <input class="mt-4" type="file" data-role="file" data-on-select="uploadMedialPresentation" accept=".png,.jpg,.jpeg,.tiff" data-cls-caption="width50" data-prepend="Select Images" data-button-title="<span class='mif-folder mif-3x ani-ring'></span>" multiple="multiple">

                                                    <span id="medialPresentationDownload" onclick='downloadMedialPresentation();' class="mif-file-download ani-heartbeat mif-3x pl-10 mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download your Medial Presentation" />
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


            @* Info Boxs *@
            <div id="PresentationInfoBox" class="info-box" data-role="infobox" data-type="info" data-width="800">
                <span class="button square closer"></span>
                <div class="info-box-content">
                    <h5>Universal Medial Presentation Posibilities</h5>
                    <p style="font-size:16px;margin-top:0px">This Medial Presentation is complexed code for show Pretty Presentation as alone Page </p>
                    <p style="font-size:16px;margin-top:0px">Here is Prepared Generating only from Images and URLs </p>
                    <p style="font-size:16px;margin-top:0px">Presentation content can be from: Html code, Images, Videos, Urls and More </p>
                    <p style="font-size:16px;margin-top:0px">For Modify edit Index.html only. Templates included in Comments</p>
                    <p style="font-size:16px;margin-top:0px">Possible automatic sliding, omnidirectional navigation and much more</p>
                </div>
            </div>
    </window>
 
    <script>
        /* Startup */

        // Declaration
        $('#imageBookDownload').hide(); $('#medialPresentationDownload').hide();
        let imageBookLastId = null; medialPresentationLastId = null;

        // Startup Calling
        
        var notify = Metro.notify; notify.setup({ width: 300, duration: 5000, animation: 'easeOutBounce' });
        notify.create("Please Rate Tool After Download..."); notify.reset();
        GetGeneratorsRating();



        // Function Part
        function setGeneratorsRating(value, star, element) {
            let recId;
            switch (element.id) {
                case "imageBookRating": recId = imageBookLastId; break;
                case "medialPresentationRating": recId = medialPresentationLastId; break;
                default:
                    break;
            }
            $.ajax({
                type: "GET", url: "/Generators/SetGeneratedToolRatingList/" + recId + "/" + value, dataType: 'json',
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
                        case "ImageBook":
                            $('#imageBookRating').data('rating').val(tool.Rating);
                            $('#imageBookRating').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "MedialPresentation":
                            $('#medialPresentationRating').data('rating').val(tool.Rating);
                            $('#medialPresentationRating').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        default:
                            break;
                    }
                });
            });
        };


        async function uploadImageBook(files, element) {
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
                    global: false, type: "POST", url: "/Generators/GenerateImageBook", dataType: 'json',
                    headers: {
                        'Content-type': 'application/json',
                        "Authorization": 'Bearer ' + Metro.storage.getItem('ApiToken', null)
                    },
                    data: JSON.stringify({ Files: dataset })
                });

                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Uploading Images for generate Image Book..."); notify.reset();

                def.fail(function (data) {
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Download Image Book Failed", "Alert", { cls: "alert" }); notify.reset();
                    $('#imageBookDownload').hide();
                    hidePageLoading();
                });

                def.done(function (data) {
                    imageBookLastId = data.genRecord;
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Your Image Book was Generated succesfully."); notify.reset();
                    $('#imageBookDownload').show();
                    hidePageLoading();
                    $('#imageBookRating').data('rating').static(false);
                });
            } else {
                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Any Image was not Selected", "Info"); notify.reset();
                $('#imageBookDownload').hide();
            }
        };
        async function downloadImageBook() {
            showPageLoading();

            var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
            notify.create("Downloading Image Book..."); notify.reset();

            AuthDownloadFile("GET", "/Generators/GetGeneratedImageBook");
            hidePageLoading();
        };


        async function uploadMedialPresentation(files) {
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
                    global: false, type: "POST", url: "/Generators/GenerateMedialPresentation", dataType: 'json',
                    headers: {
                        'Content-type': 'application/json',
                        "Authorization": 'Bearer ' + Metro.storage.getItem('ApiToken', null)
                    },
                    data: JSON.stringify({ Files: dataset })
                });

                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Uploading Images for generate Medial Presentation..."); notify.reset();

                def.fail(function (data) {
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Download Medial Presentation Failed", "Alert", { cls: "alert" }); notify.reset();
                    $('#medialPresentationDownload').hide();
                    hidePageLoading();
                });

                def.done(function (data) {
                    medialPresentationLastId = data.genRecord;
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Your Medial Presentation was Generated succesfully."); notify.reset();
                    $('#medialPresentationDownload').show();
                    hidePageLoading();
                    $('#medialPresentationRating').data('rating').static(false);
                });
            } else {
                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Any Image was not Selected", "Info"); notify.reset();
                $('#GenerateMedialPresentation').hide();
            }
        };
        async function downloadMedialPresentation() {
            showPageLoading();

            var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
            notify.create("Downloading Medial Presentation..."); notify.reset();

            AuthDownloadFile("GET", "/Generators/GetGeneratedMedialPresentation");
            hidePageLoading();
        };

       
    </script>
</div>
