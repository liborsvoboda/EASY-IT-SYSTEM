ď»ż@page 
@model ServerCorePages.ResetPasswordModel
@{
    ViewData["Title"] = "Reset Password";
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
                        <h3 class="text-light">EDC&ESB Portal Reset Password</h3>
                        <hr class="thin mt-4 mb-4 bg-white">
                        <div class="form-group">
                            <input id="emailAddressId" type="email" data-role="input" data-prepend="<span class='mif-envelop'>" placeholder="Enter your email..."
                                   data-validate="required, email" data-clear-button="true">
                        </div>
                        <div class="form-group mb-10">
                            <span class="button c-pointer" onclick="ResetPassword()">Send New Password to Email</span>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </window>
    <script>
        //Declaration


        //Functions

        function ResetPassword() {
            if ($("#emailAddressId").val().length == 0) {
                $("#loginform").addClass("ani-ring");
                setTimeout(function () {
                    $("#loginform").removeClass("ani-ring");
                }, 1000);
            } else {

                var def = $.ajax({
                    global: false, type: "POST", url: "/WebUser/ResetPassword",
                    dataType: 'json', contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ EmailAddress: $("#emailAddressId").val(), Language: Metro.storage.getItem('WebPagesLanguage', 'cz') })
                });

                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Sending New Password to Email..."); notify.reset();

                def.fail(function (data) {
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Send Failed", "Alert", { cls: "alert" }); notify.reset();
                });

                def.done(function (data) {
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Email Sent. ", "Info", { cls: "success" }); notify.reset();
                });
            }
        }

        function invalidForm() {
            var form  = $(this);
            form.addClass("ani-ring");
            setTimeout(function(){
                form.removeClass("ani-ring");
            }, 1000);
        }

    </script>
</div>
