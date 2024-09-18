ď»ż@page 
@model ServerCorePages.VideoGalleryModel
@{
    ViewData["Title"] = "";  
    //Layout = "~/Shared/_Layout.cshtml";  
}

    @*
    https://metroui.org.ua/intro.html
    https://metroui.org.ua/position.html
    https://www.c-sharpcorner.com/article/custom-login-register-with-identity-in-asp-net-core-3-1/
    *@

<div class="text-center info-panel mb-2">
   
    <div id="imageWindow" data-role="window" class="window"
         data-title="EASY DATA Center & EASY SYSTEM Builder Video Gallery of One Man Developing as Hobby less than One Year"
         data-btn-min="false"
         data-btn-max="false"
         data-btn-close="false"
         data-resizable="false"
         data-draggable="false"
         data-width="100%"
         data-height="650px"
         data-shadow="true"
         data-icon="<span class='mif-image'></span>"
         data-content="<iframe src='../server/Media/VideoGallery/index.html' frameborder='0' style='overflow:hidden;height:610px;width:100%' width='100%'></iframe>">
    </div>
 
    <script>

    </script>
</div>
