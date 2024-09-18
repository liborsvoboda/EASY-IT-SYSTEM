ď»ż@page 
@model ServerCorePages.UserProfileModel
@{
    ViewData["Title"] = "User Profile";
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
                    <form id="userform" method="post" 
                        class="login-form bg-white p-6 mx-auto border bd-default win-shadow"
                            data-role="validator"
                            action="javascript:"
                            data-clear-invalid="2000"
                            data-on-error-form="invalidForm"
                            data-on-validate-form="validateForm">
                        <span class="mif-vpn-lock mif-4x place-right" style="margin-top: -10px;"></span>
                        <h2 class="text-light">EDC&ESB Portal User Profile</h2>
                        <hr class="thin mt-4 mb-4 bg-white">
                        <div class="form-group">
                            <input id="usernameId" type="text" data-role="input" data-prepend="<span class='mif-envelop'>" placeholder="Enter your email..." data-validate="required">
                        </div>
                        <div class="form-group">
                            <input id="firstnameId" type="text" data-role="input" data-prepend="<span class='mif-user'>" placeholder="Enter your First Name" data-validate="required">
                        </div>
                        <div class="form-group">
                            <input id="lastnameId" type="text" data-role="input" data-prepend="<span class='mif-user'>" placeholder="Enter your Last Name" data-validate="required">
                        </div>

                        <div class="form-group">
                            <input id="passwordId" type="password" data-role="input" data-prepend="<span class='mif-key'>" placeholder="Empty or Insert New" >
                        </div>
                        <div class="form-group mt-10">
                            <button class="button" >Update Profile</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </window>

    <script>
        // check logged


        //Declaration
        
        

        //Startup
        getUserProfile();




        //Functions
        
        function getUserProfile() {
            showPageLoading();
            let getUser = $.ajax({
                global: false, type: "GET", url: "/WebUser/GetWebUser/" + Metro.storage.getItem('WebPagesLanguage', 'cz'), dataType: 'json',
                contentType: 'application/json',
                headers: {
                    "Authorization": 'Bearer ' + Metro.storage.getItem('ApiToken', null)
                }
            }).fail(function (data) {
                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Cannot Load User Data", "Alert", { cls: "alert" }); notify.reset();
                hidePageLoading();
            }).done(function (data) {
                Metro.storage.setItem('UserData', data);
                $("#usernameId").val(data.userName);
                if (data.userName != data.name) { $("#firstnameId").val(data.name); }
                if (data.userName != data.surName) { $("#lastnameId").val(data.surName); }
                hidePageLoading();
            });
        }

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
                global: false, type: "POST", url: "/WebUser/UpdateRegistration", dataType: 'json',
                contentType: 'application/json',
                headers: { "Authorization": 'Bearer ' + Metro.storage.getItem('ApiToken', null) },
                data: JSON.stringify({ EmailAddress: $("#usernameId").val(), FirstName: $("#firstnameId").val(),
                    LastName: $("#lastnameId").val(), Password: $("#passwordId").val(), Language: Metro.storage.getItem('WebPagesLanguage', 'cz')
                })
            }).fail(function (data) {
                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Update Profile Failed", "Alert", { cls: "alert" }); notify.reset();
                hidePageLoading();
            }).done(function (data) {

                //update storage user
                let user = Metro.storage.getItem('UserData', null);
                user.userName = $("#usernameId").val(); user.name = $("#firstnameId").val(); user.surName = $("#lastnameId").val();
                Metro.storage.setItem('UserData', user);

                var notify = Metro.notify; notify.setup({ width: 300, duration: 1000, animation: 'easeOutBounce' });
                notify.create("Update Profile Success", "Info", { cls: "success" }); notify.reset();
                hidePageLoading();
            });
        }

    </script>
</div>
