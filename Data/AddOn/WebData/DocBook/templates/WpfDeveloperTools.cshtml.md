ď»ż@page 
@model ServerCorePages.WpfDeveloperToolsModel
@{
    ViewData["Title"] = "Wpf Developer Tools";
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
            Prepared Tools & Packages for EASY Graphics Developing WPF Metro Systems.<br />
            <a href="https://mahapps.com/" target="_blank">Metro is Very Modern Technology</a><br />
            Download, Unpack, install or Compile for Run Examples of Graphics Posibilities.<br />
            Its for maximalize Graphics developing - Low Code - Way is Generate Graphics Builders<br />
        </div>

@*         @if (User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role) != null) {

        }
        else {
            <div class="h4 fg-red ani-hover-heartbeat">
                For Download You must be Logged in
            </div>
        } *@

        <div class="about ">
            <div class="container">
                <div class="mt-10 text-center">
                    <div class="fg-darkSteel" style="font-weight:bold;font-size:20px;opacity:0.7;">
                        WPF Applications & Project for EASY Developing from Graphics Builders 
                    </div>
                    <p class="text-leader pl-20-md pr-20-md">
                        By click on left Icon open Example
                    </p>
                </div>

                <div class="row mt-10">
                   

                    <div class="cell-md-6 pt-4">
                        <div class="p-4 bg-brand-secondary h-100">
                            <div class="icon-box border bd-brand">
                                <a href="#" class="unstyled-link c-pointer" onclick='Metro.window.create({title:"WPF Developer Theme / Style / Code / Comment / Design Tools",shadow:true,draggable:true,customButtons:WpfToolBackButton,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe id=\"WpfToolWindow\" src=\"../server/Downloads/WPF_DevWindowsTools\" style=\"width:100%;height:640px;\"></iframe>"});'>
                                    <div class="icon bg-brand fg-white button" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Click for open Download"><span class="mif-file-text"></span></div>
                                </a>
                                <div class="content d-flex flex-align-center">
                                    <span class="pos-absolute w-100 text-right mif-3x" style="top: 0px;right:0px;">
                                        <input id="WPFDeveloperTools" class="text-right" data-static="false" data-role="rating" data-on-star-click="setGeneratorsRating" data-star-color="cyan" data-stared-color="fg-blue">
                                    </span>
                                    <div class="h4 text-right pos-absolute w-100 mb-0 pb-0 pr-1">
                                        <div class="d-block c-pointer mt-5" data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Free Prepared Full Design WPF Code Package">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            Free
                                        </div>
@*                                         <div class="d-block c-pointer" onclick='buyFullCode("MetroPosibilities");' data-role="hint" data-cls-hint="bg-cyan fg-white drop-shadow" data-hint-text="Buy Full Code with Help and Support">
                                            <span class="mif-shopping-basket ani-heartbeat mif-1x mif-1x"></span>
                                            5000KÄŤ
                                        </div> *@
                                    </div>
                                    <div class="text-light c-pointer w-100">
                                        <div class="reduce-1 pt-2 enlarge-1-md text-right">
                                            <div class="d-block w-100">
                                                <div class="text-left pl-2">WPF Designing Windows Tools <br />Prepared Full Developer Package</div>
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
                        case "WPFDeveloperTools":
                            $('#WPFDeveloperTools').data('rating').val(tool.Rating);
                            $('#WPFDeveloperTools').data('rating').msg('<span class="mr-1 fg-blue" style="font-size:16px;font-weight:bold">' + tool.TotalCount + '</span>');
                            break;
                            
                        default:
                            break;
                    }
                });
            });
        };

    </script>
</div>
