ď»ż@page 
@model ServerCorePages.RegistrationModel
@{
    ViewData["Title"] = "Registration";
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
                        <h2 class="text-light">EDC&ESB Portal Registration</h2>
                        <hr class="thin mt-4 mb-4 bg-white">
                        <div class="form-group">
                            <input id="emailAddressId" type="email" data-role="input" data-prepend="<span class='mif-envelop'>" placeholder="Enter your email..."
                                   data-validate="required, email" data-clear-button="true">
                        </div>
                        <div class="form-group mb-10">
                            <span class="button c-pointer" onclick="sendVerifyCode()">Send Verify Code to Email</span>
                        </div>
                        <div class="form-group">
                            <input id="verifyCodeId" type="text" data-role="input" oninput="checkVerify(false)" data-prepend="<span class='mif-verified'>"
                                   placeholder="Enter your verify code..." data-validate="required minlength=10" data-clear-button="true" data-custom-buttons="checkButton">
                        </div>
                        <div class="form-group">
                            <input id="passwordId" type="password" disabled data-role="input" data-prepend="<span class='mif-key'>"
                                   placeholder="Enter your password..." data-validate="required minlength=6" data-reveal-button="true">
                        </div>
                        <div class="form-group mt-10">
                            <button id="formButton" class="button" disabled>Register</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </window>
    <script>
        //Declaration
        let doVerify = true;
        let verifyCode = null;
        var checkButton = [{ html: "<span class='mif-user-check'></span>", cls: "warning", onclick: "checkVerify(true)"}]


        //Functions

        function checkVerify(showNotify) {
            if (doVerify) {
                let resultOk = verifyCode != null && verifyCode == $("#verifyCodeId").val();

                if (showNotify && !resultOk) {
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Verification Failed", "Alert", { cls: "alert" }); notify.reset();
                }
                if (resultOk) {
                    $("#passwordId").prop('disabled', false);
                    $("#formButton").prop('disabled', false);
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Email was Verified", "Success", { cls: "success" }); notify.reset();
                    doVerify = false;
                }
            }
        }

        function sendVerifyCode() {
            if ($("#emailAddressId").val().length == 0) {
                $("#loginform").addClass("ani-ring");
                setTimeout(function () {
                    $("#loginform").removeClass("ani-ring");
                }, 1000);
            } else {

                var def = $.ajax({
                    global: false, type: "POST", url: "/WebUser/SendVerifyCode", 
                    dataType: 'json', contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ EmailAddress: $("#emailAddressId").val(), Language: Metro.storage.getItem('WebPagesLanguage', 'en' )})
                });

                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Sending Verification Email..."); notify.reset();

                def.fail(function (data) {
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Send Failed " + data.responseJSON.ErrorMessage, "Alert", { cls: "alert" }); notify.reset();
                });

                def.done(function (data) {
                    verifyCode = data.verifyCode;
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Email Sent. Write Verify Code from Email ", "Info", { cls: "success" }); notify.reset();
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

        function validateForm() {
            if (!doVerify) {
                showPageLoading();

                var def = $.ajax({
                    global: false, type: "POST", url: "/WebUser/Registration", dataType: 'json',
                    dataType: 'json', contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ EmailAddress: $("#emailAddressId").val(), Password: $("#passwordId").val(), Language: Metro.storage.getItem('WebPagesLanguage', 'cz') })
                });

                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Waiting for Login..."); notify.reset();

                def.fail(function (data) {
                    var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                    notify.create("Login Failed", "Alert", { cls: "alert" }); notify.reset();
                    hidePageLoading();
                });

                def.done(function (data) {
                    hidePageLoading();
                    login();
                });
            }
        }

        function login() {
            showPageLoading();
            var def = $.ajax({
                global: false, type: "POST", url: "/GLOBALNETAuthentication", dataType: 'json',
                headers: { "Authorization": "Basic " + btoa($("#emailAddressId").val() + ":" + $("#passwordId").val()) }
            });

            var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
            notify.create("Waiting for Login..."); notify.reset();

            def.fail(function (data) {
                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Login Failed", "Alert", { cls: "alert" }); notify.reset();
                hidePageLoading();
            });

            def.done(function (data) {
                hidePageLoading();
                Cookies.set('ApiToken', data.Token);
                window.location.href = "/Dashboard";
            });
        }

    </script>
</div>
