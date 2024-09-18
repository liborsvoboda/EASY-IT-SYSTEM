ď»ż@page 
@model ServerCorePages.AdminLoginModel
@{
    ViewData["Title"] = "PĹ™ihlĂˇĹˇenĂ­ Admina";  
    //Layout = "~/Shared/_Layout.cshtml";  
}

    @*
    https://metroui.org.ua/intro.html
    https://metroui.org.ua/position.html
    https://www.c-sharpcorner.com/article/custom-login-register-with-identity-in-asp-net-core-3-1/
    *@

<div class="text-center">
    <window>
        <div class="hero hero-bg 1bg-brand-secondary add-neb">
            <div class="container">
                <div class="row">
                    <form id="loginform" method="post"
                          class="login-form bg-white fg-darkBlue p-6 mx-auto border bd-default win-shadow"
                          data-role="validator"
                          action="javascript:"
                          data-clear-invalid="2000"
                          data-on-error-form="invalidForm"
                          data-on-validate-form="validateForm">
                        <span class="mif-vpn-lock mif-4x place-right" style="margin-top: -10px;"></span>
                        <h2 class="text-light">Golden Portal PĹ™ihlĂˇĹˇenĂ­</h2>
                        
                        <div class="form-group">
                            <input id="usernameId" type="email" data-role="input" class="input" data-prepend="<span class='mif-envelop'>" placeholder="VloĹľte email..." data-validate="required" maxlength="50" style="height: auto;" />
                        </div>
                        <div class="form-group">
                            <input id="passwordId" type="password" data-role="input" data-prepend="<span class='mif-key'>" placeholder="VloĹľte heslo..." data-validate="required minlength=6">
                        </div>
                        <div class="form-group mt-10">
                            <input type="checkbox" data-role="checkbox" data-caption="Zapamatovat" class="place-right">
                            <button class="button shadowed">PĹ™ihlĂˇsit</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </window>
    <script>
        function invalidForm() {
            var form = $(this);
            form.addClass("ani-ring");
            setTimeout(function () {
                form.removeClass("ani-ring");
            }, 1000);
        }

        function validateForm() {
            showPageLoading();
            var def = $.ajax({
                global: false, type: "POST", url: "/GoldenSystemAuthentication", dataType: 'json',
                headers: { "Authorization": "Basic " + btoa($("#usernameId").val() + ":" + $("#passwordId").val()) }
            });

            def.fail(function (data) {
                var notify = Metro.notify; notify.setup({ width: 300, timeout: Metro.storage.getItem('NotifyShowTime', null), duration: 500 });
                notify.create("NesprĂˇvnĂ© jmĂ©no nebo heslo", "Error", { cls: "alert" }); notify.reset();
                hidePageLoading();
            });

            def.done(function (data) {
                AdminLogin(data);
                hidePageLoading();
            });
        }

    </script>
</div>
