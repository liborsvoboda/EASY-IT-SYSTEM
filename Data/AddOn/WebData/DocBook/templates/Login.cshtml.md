ď»ż@page 
@model ServerCorePages.LoginModel
@{
    ViewData["Title"] = "Login";  
    //Layout = "~/Shared/_Layout.cshtml";  
}

    @*
    https://metroui.org.ua/intro.html
    https://metroui.org.ua/position.html
    https://www.c-sharpcorner.com/article/custom-login-register-with-identity-in-asp-net-core-3-1/
    *@

<div class="text-center info-panel">
    <window>
        <div class="hero hero-bg 1bg-brand-secondary add-neb">
            <div class="container">
                <div class="row">
                    <form id="loginform" method="post" 
                        class="login-form bg-white p-6 mx-auto border bd-default win-shadow"
                          data-role="validator"
                          action="javascript:"
                          data-clear-invalid="2000"
                          data-on-error-form="invalidForm"
                          data-on-validate-form="validateForm">
                        <span class="mif-vpn-lock mif-4x place-right" style="margin-top: -10px;"></span>
                        <h2 class="text-light">EDC&ESB Portal Login</h2>
                        <hr class="thin mt-4 mb-4 bg-white">
                        <div class="form-group">
                            <input id="usernameId" type="email" data-role="input" data-prepend="<span class='mif-envelop'>" placeholder="Enter your email..." data-validate="required">
                        </div>
                        <div class="form-group">
                            <input id="passwordId" type="password" data-role="input" data-prepend="<span class='mif-key'>" placeholder="Enter your password..." data-validate="required minlength=6">
                        </div>
                        <div class="form-group mt-10">
                            <input type="checkbox" data-role="checkbox" data-caption="Remember me" class="place-right">
                            <button class="button" >Submit form</button>
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
                global: false, type: "POST", url: "/GLOBALNETAuthentication", dataType: 'json',
                headers: { "Authorization": "Basic " + btoa($("#usernameId").val() + ":" + $("#passwordId").val()) }
            });

            def.fail(function (data) {
                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Login Failed", "Alert", { cls: "alert" }); notify.reset();
                hidePageLoading();
            });

            def.done(function (data) {
                Login(data);
                hidePageLoading();
            });
        }

    </script>
</div>
