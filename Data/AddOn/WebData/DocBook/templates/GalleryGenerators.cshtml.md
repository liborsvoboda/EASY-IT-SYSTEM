ď»ż@page 
@model ServerCorePages.GalleryGeneratorsModel
@{
    ViewData["Title"] = "Image/Video Gallery Generators";
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
            Galleries are automatically generated for Download<br />
            Download, Unpack and Copy to your WebPages.<br />
            Gallery will be shown automatically by Index.html<br />
        </div>

        @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {

        }
        else {
            <div class="h4 fg-red ani-hover-heartbeat">
                For Create and Download your Gallery from your Pictures You must be Logged in
            </div>
        }

        <div class="about ">
            <div class="container">
                <div class="mt-10 text-center">
                    <div class="fg-darkSteel" style="font-weight:bold;font-size:40px;opacity:0.7;">
                        Static Pages Image / Videos Gallery GENERATORS
                    </div>
                    <p class="text-leader pl-20-md pr-20-md">
                        By click on left Icon open Example
                    </p>
                </div>

                <div class="row mt-10">
                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"Easy Gallery",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/EasyGallery/demo/index.html\" style=\"width:100%;height:600px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Click for Show Example"><span class="mif-images"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="easyGalleryRating" class="text-right" data-static="true" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("EasyGallery");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            1000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("EasyGallery");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            2000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="d-flex w-100">
                                                <div class="text-left pl-4"> EASY Gallery </div>
                                            </div>
                                            <div class="d-flex">
@*                                                 @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                    <input class="mt-4" type="file" data-role="file" data-on-select="uploadEasyGallery" accept=".png,.jpg,.jpeg,.tiff" data-cls-caption="width50" data-prepend="Select your images" data-button-title="<span class='mif-folder mif-3x ani-ring'></span>" multiple="multiple">

                                                    <span id="easyGalleryDownload" onclick='downloadEasyGallery();' class="mif-file-download ani-heartbeat mif-3x pl-10 mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download your EASY Gallery" />
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
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"Carousel Gallery",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/CarouselGallery/demo/fullscreenCarouselImage.html\" style=\"width:100%;height:600px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Click for Show Example"><span class="mif-replay"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="carouselGalleryRating" class="text-right" data-static="true" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("CarouselGallery");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            2000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("CarouselGallery");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            3000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md">
                                            <div class="text-left pl-4">Carousel Gallery</div>
                                            <div class="d-flex">
@*                                                 @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                    <input class="mt-4" type="file" data-role="file" data-on-select="uploadCarouselGallery" accept=".png,.jpg,.jpeg,.tiff" data-cls-caption="width50" data-prepend="Select your images" data-button-title="<span class='mif-folder mif-3x ani-ring'></span>" multiple="multiple">

                                                    <span id="carouselGalleryDownload" onclick='downloadCarouselGallery();' class="mif-file-download ani-heartbeat mif-3x pl-10 mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download your Carousel Gallery" />
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
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"MP4 Carousel Video Gallery",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/CarouselVideoGallery/demo/videoCarousel.html\" allowfullscreen style=\"width:100%;height:600px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Click for Show Example"><span class="mif-film"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="carouselVideoGalleryRating" class="text-right" data-static="true" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("CarouselVideoGallery");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            2000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("CarouselVideoGallery");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            3000KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="For Fast generation Carousel Video Gallery the Video Files are with zero size. After Download your Rewrite Video files in folder 'videos'">
                                            <div class="text-left pl-4">Carousel Video Gallery</div>
                                            <div class="d-flex">
@*                                                 @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                    <input class="mt-4" type="file" data-role="file" data-on-select="uploadCarouselVideoGallery" accept=".mp4" data-cls-caption="width50" data-prepend="Select your videos" data-button-title="<span class='mif-folder mif-3x ani-ring'></span>" multiple="multiple">

                                                    <span id="carouselVideoGalleryDownload" onclick='downloadCarouselVideoGallery();' class="mif-file-download ani-heartbeat mif-3x pl-10 mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download your Carousel Video Gallery" />
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
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"Video Player & PlayList",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Tools/VideoListGallery/demo/index.html\" style=\"width:100%;height:600px\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Click for Show Example"><span class="mif-list2"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="videoPlayListRating" class="text-right" data-static="true" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" onclick='buyLicense("VideoPlayList");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            1000KÄŤ
                                        </div>
                                        <div class="d-block c-pointer" onclick='buyFullCode("VideoPlayList");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            1500KÄŤ
                                        </div>
                                    </div>
                                    <div class="h4 text-light c-pointer">
                                        <div class="reduce-1 pt-2 enlarge-1-md" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="For Fast generation Video Download your Video Player & PlayList the Video Files are with zero size. After Download your Rewrite Video files in folder 'videos'">
                                            <div class="text-left pl-4">Video Player & PlayList</div>
                                            <div class="d-flex">
@*                                                 @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {
                                                    <input class="mt-4" type="file" data-role="file" data-on-select="uploadVideoPlayList" accept=".mp4" data-cls-caption="width50" data-prepend="Select your videos" data-button-title="<span class='mif-folder mif-3x ani-ring'></span>" multiple="multiple">

                                                    <span id="videoPlayListDownload" onclick='downloadVideoPlayList();' class="mif-file-download ani-heartbeat mif-3x pl-10 mif-3x" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Download your Video Player & PlayList" />
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


    </window>
 
    <script>
        /* Startup */

        // Declaration
        $('#easyGalleryDownload').hide();$('#carouselGalleryDownload').hide();$('#carouselVideoGalleryDownload').hide();$('#videoPlayListDownload').hide();
        let easyGalleryLastId = null; let carouselGalleryLastId = null; carouselVideoGalleryLastId = null; videoPlayListLastId = null;

        // Startup Calling
        GetGeneratorsRating();
        var notify = Metro.notify; notify.setup({ width: 300, duration: 5000, animation: 'easeOutBounce' });
        notify.create("Please Rate Tool After Download..."); notify.reset();




        // Function Part

        function setGeneratorsRating(value, star, element) {
            let recId;
            switch (element.id) {
                case "easyGalleryRating": recId = easyGalleryLastId; break;
                case "carouselGalleryRating": recId = carouselGalleryLastId; break;
                case "carouselVideoGalleryRating": recId = carouselVideoGalleryLastId; break;
                case "videoPlayListRating": recId = videoPlayListLastId; break;
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
                        case "EasyGallery":
                            $('#easyGalleryRating').data('rating').val(tool.Rating);
                            $('#easyGalleryRating').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "CarouselGallery":
                            $('#carouselGalleryRating').data('rating').val(tool.Rating);
                            $('#carouselGalleryRating').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "CarouselVideoGallery":
                            $('#carouselVideoGalleryRating').data('rating').val(tool.Rating);
                            $('#carouselVideoGalleryRating').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        case "VideoPlayList":
                            $('#videoPlayListRating').data('rating').val(tool.Rating);
                            $('#videoPlayListRating').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                        default:
                            break;
                    }
                });
            });
        };


        async function uploadEasyGallery(files) {
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
                    global: false, type: "POST", url: "/Generators/GenerateEasyGallery", dataType: 'json',
                    headers: {
                        'Content-type': 'application/json',
                        "Authorization": 'Bearer ' + Metro.storage.getItem('ApiToken', null)
                    },
                    data: JSON.stringify({ Files: dataset })
                });

                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Uploading Images for generate Easy Gallery..."); notify.reset();

                def.fail(function (data) {
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Download Easy Gallery Failed", "Alert", { cls: "alert" }); notify.reset();
                    $('#easyGalleryDownload').hide();
                    hidePageLoading();
                });

                def.done(function (data) {
                    easyGalleryLastId = data.genRecord;
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Your Easy Gallery was Generated succesfully."); notify.reset();
                    $('#easyGalleryDownload').show();
                    hidePageLoading();
                    $('#easyGalleryRating').data('rating').static(false);
                });
            } else {
                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Any Image was not Selected", "Info"); notify.reset();
                $('#easyGalleryDownload').hide();
            }
        };
        async function downloadEasyGallery() {
            showPageLoading();

            var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
            notify.create("Downloading EASY Gallery..."); notify.reset();
            AuthDownloadFile("GET", "/Generators/GetGeneratedEasyGallery");
            hidePageLoading();
        };

        async function uploadCarouselGallery(files) {
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
                    global: false, type: "POST", url: "/Generators/GenerateCarouselGallery", dataType: 'json',
                    headers: {
                        'Content-type': 'application/json',
                        "Authorization": 'Bearer ' + Metro.storage.getItem('ApiToken', null)
                    },
                    data: JSON.stringify({ Files: dataset })
                });

                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Uploading Images for generate Carousel Gallery..."); notify.reset();

                def.fail(function (data) {
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Download Carousel Gallery Failed", "Alert", { cls: "alert" }); notify.reset();
                    $('#carouselGalleryDownload').hide();
                    hidePageLoading();
                });

                def.done(function (data) {
                    carouselGalleryLastId = data.genRecord;
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Your Carousel Gallery was Generated succesfully."); notify.reset();
                    $('#carouselGalleryDownload').show();
                    hidePageLoading();
                    $('#carouselGalleryRating').data('rating').static(false);
                });
            } else {
                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Any Image was not Selected", "Info"); notify.reset();
                $('#carouselGalleryDownload').hide();
            }
        };
        async function downloadCarouselGallery() {
            showPageLoading();

            var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
            notify.create("Downloading Carousel Gallery..."); notify.reset();

            AuthDownloadFile("GET", "/Generators/GetGeneratedCarouselGallery");
            hidePageLoading();
        };

        async function uploadCarouselVideoGallery(files) {
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
                    global: false, type: "POST", url: "/Generators/GenerateCarouselVideoGallery", dataType: 'json',
                    headers: {
                        'Content-type': 'application/json',
                        "Authorization": 'Bearer ' + Metro.storage.getItem('ApiToken', null)
                    },
                    data: JSON.stringify({ Files: dataset })
                });

                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Uploading Images for generate Carousel Video Gallery..."); notify.reset();

                def.fail(function (data) {
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Download Carousel Video Gallery Failed", "Alert", { cls: "alert" }); notify.reset();
                    $('#carouselVideoGalleryDownload').hide();
                    hidePageLoading();
                });

                def.done(function (data) {
                    carouselVideoGalleryLastId = data.genRecord;
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Your Carousel Video Gallery was Generated succesfully."); notify.reset();
                    $('#carouselVideoGalleryDownload').show();
                    hidePageLoading();
                    $('#carouselVideoGalleryRating').data('rating').static(false);
                });
            } else {
                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Any video was not Selected", "Info"); notify.reset();
                $('#carouselVideoGalleryDownload').hide();
            }
        };
        async function downloadCarouselVideoGallery() {
            showPageLoading();

            var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
            notify.create("Downloading Carousel Video Gallery..."); notify.reset();

            AuthDownloadFile("GET", "/Generators/GetGeneratedCarouselVideoGallery");
            hidePageLoading();
        };

        async function uploadVideoPlayList(files) {
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
                    global: false, type: "POST", url: "/Generators/GenerateVideoPlayList", dataType: 'json',
                    headers: {
                        'Content-type': 'application/json',
                        "Authorization": 'Bearer ' + Metro.storage.getItem('ApiToken', null)
                    },
                    data: JSON.stringify({ Files: dataset })
                });

                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Uploading Videos for generate for VideoPlayer & PlayList..."); notify.reset();

                def.fail(function (data) {
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Download VideoPlayer & PlayList Failed", "Alert", { cls: "alert" }); notify.reset();
                    $('#videoPlayListDownload').hide();
                    hidePageLoading();
                });

                def.done(function (data) {
                    videoPlayListLastId = data.genRecord;
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Your VideoPlayer & PlayList was Generated succesfully."); notify.reset();
                    $('#videoPlayListDownload').show();
                    hidePageLoading();
                    $('#videoPlayListRating').data('rating').static(false);
                });
            } else {
                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Any video was not Selected", "Info"); notify.reset();
                $('#videoPlayListDownload').hide();
            }
        };
        async function downloadVideoPlayList() {
            showPageLoading();

            var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
            notify.create("Downloading VideoPlayer & PlayList..."); notify.reset();

            AuthDownloadFile("GET", "/Generators/GetGeneratedVideoPlayList");
            hidePageLoading();
        };
    </script>
</div>
