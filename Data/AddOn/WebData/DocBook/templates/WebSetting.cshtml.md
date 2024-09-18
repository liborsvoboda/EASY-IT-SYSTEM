ď»ż@page
@model ServerCorePages.WebSettingModel
@{
    ViewData["Title"] = "NastavenĂ­ Webu";
}


<div class="text-center mb-4 pb-5">
    <window style="position: relative; top: 60px;">
        <div id="TogglePanelBackground" class="panel" style="min-height: 700px;">
            <form class="form1" data-role="validator" action="javascript:" data-on-submit="SaveWebSettings"
                  data-interactive-check="true" autocomplete="off" data-on-error="newCommentIsValid = false;">
                <div class="d-block">
                    <div class="d-flex row gutters mr-4">
                        <div class="col-xl-9 col-lg-9 col-md-9 col-sm-9 col-9 pl-5 pt-5 mb-0">
                            <ul data-role="tabs" data-expand="true" data-tabs-type="text">
                                <!-- <ul data-app-bar="true" data-role="materialtabs" data-fixed-tabs="true" data-deep="true"> -->
                                <li id="systemBehaviorMenu" class="fg-black"><a href="#_systemBehavior">ChovĂˇnĂ­ SystĂ©mu</a></li>
                                <li id="systemDesignMenu" class="fg-black"><a href="#_systemDesign">Vzhled SystĂ©mu</a></li>
                            </ul>
                        </div>
                        <div class="col-xl-3 col-lg-3 col-md-3 col-sm-3 pt-4 col-12">
                            <div class="text-right">
                                <button type="button" class="button warning outline shadowed" onclick="ShowBackgound()">
                                    UkĂˇzat pozadĂ­
                                </button>
                                <button type="button" class="button info outline shadowed" onclick="StoreWebSettings()">
                                    Otestovat
                                </button>
                                <button type="submit" class="button success outline shadowed">
                                    UloĹľit
                                </button>
                            </div>
                        </div>
                    </div>

                    <div id="_systemBehavior">
                        <div class="d-flex row gutters ml-5 mr-5 mb-5 border">

                            <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
                                <div class="form-group">
                                    <label for="Street">ZobrazenĂ­ Notifikace milisekundy</label>
                                    <input id="NotifyShowTime" type="number" data-role="spinner" data-min-value="0" data-max-value="30000">
                                </div>
                            </div>

                            <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
                                <div class="form-group pt-5">
                                </div>
                            </div>

                            <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
                                <div class="form-group pt-5">
                                    <input id="AutomaticDetectedLanguageTranslate" type="checkbox" data-role="checkbox" data-caption="AutomatickĂ˝ pĹ™eklad do DetekovanĂ©ho Jazyka NĂˇvĹˇtÄ›vnĂ­ka" />
                                </div>
                            </div>

                        </div>
                    </div>

                    <div id="_systemDesign">
                        <div class="d-flex row gutters ml-5 mr-5 mb-5 border">

                            <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
                                <div class="form-group p-3 pb-0">
                                    <label for="Street">PrĹŻhlednost pĹ™ektrytĂ­ PozadĂ­ v %</label>
                                    <input id="BackgroundOpacitySetting" type="text" data-role="spinner" data-validate="required" data-step="10" data-min-value="0" data-max-value="100">
                                </div>

                                <div class="form-group p-3 pb-0">
                                    <label for="Street">Url/Cesta Videa PozadĂ­</label>
                                    <input id="BackgroundVideoSetting" type="text" data-role="input" data-validate="required" autocomplete="off" />
                                </div>

                                <div class="form-group p-3 pb-0">
                                    <label for="Street">Url/Cesta ObrĂˇzku PozadĂ­</label>
                                    <input id="BackgroundImageSetting" type="text" data-role="input" data-validate="required" autocomplete="off" />
                                </div>
                                

                                <div class="form-group p-3 pb-0">
                                    <label for="Street">Barva pĹ™ektrytĂ­ PozadĂ­</label>
                                    <input id="BackgroundColorSetting" type="text" data-role="input" data-validate="required hexcolor" autocomplete="off" />
                                </div>
                            </div>

                            <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
                                <button type="button" class="button info outline shadowed mt-8" onclick='Metro.window.create({title:"DostupnĂ© obrĂˇzky",shadow:true,draggable:true,modal:true,icon:"<span class=\"mif-info\"</span>",btnClose:true,width:1050,height:680,place:"center",content:"<iframe src=\"../Images/List/backgroundLinks.html\" style=\"width:100%;height:600px;\"></iframe>"});'>
                                    Zobrazit dostupnĂ© obrĂˇzky PozadĂ­
                                </button>
                            </div>

                            <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
                                <div class="form-group p-3 pb-0">
                                    <label for="Street">ZkopĂ­rujte Barvu</label>
                                    <div data-role="color-selector" data-show-user-colors="false" data-show-alpha-channel="false" data-readonly-input="true"></div>
                                </div>
                            </div>


                        </div>
                    </div>


                </div>
            </form>

            <div class="row gutters pr-5">
                <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
                </div>
                <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
                    <div class="text-right">
                        <button type="button" class="button warning outline shadowed" onclick="ShowBackgound()">
                            UkĂˇzat pozadĂ­
                        </button>
                        <button type="button" class="button info outline shadowed" onclick="StoreWebSettings()">
                            Otestovat
                        </button>
                        <button type="submit" class="button success outline shadowed">
                            UloĹľit
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </window>
    <div class="mb-10"></div>

    <script>

        // Definitions


        //Startup
        async function Startup() {
            await GetWebSettingsList();
            $("#NotifyShowTime").val(Metro.storage.getItem('NotifyShowTime', null));
            $("#AutomaticDetectedLanguageTranslate").val('checked')[0].checked = Metro.storage.getItem('AutomaticDetectedLanguageTranslate', null);
            $("#BackgroundOpacitySetting").val(Metro.storage.getItem('BackgroundOpacitySetting', null));
            $("#BackgroundVideoSetting").val(Metro.storage.getItem('BackgroundVideoSetting', null));
            $("#BackgroundImageSetting").val(Metro.storage.getItem('BackgroundImageSetting', null));
            $("#BackgroundColorSetting").val(Metro.storage.getItem('BackgroundColorSetting', null));
        }; 
        $(document).ready(async function () { await Startup(); });
        


        //Functions
        function ShowBackgound() {
            if ($("#TogglePanelBackground")[0].style.backgroundColor != "transparent") { $("#TogglePanelBackground")[0].style.backgroundColor = "transparent"; }
            else { $("#TogglePanelBackground")[0].style.backgroundColor = "white"; }
        }

        function WebSettingsToJson() {
            return {
                Settings:  [
                    {Key: "NotifyShowTime", Value: $("#NotifyShowTime").val()},
                    { Key: "AutomaticDetectedLanguageTranslate", Value: $("#AutomaticDetectedLanguageTranslate").val('checked')[0].checked },
                    { Key: "BackgroundOpacitySetting", Value: $("#BackgroundOpacitySetting").val() },
                    { Key: "BackgroundVideoSetting", Value: $("#BackgroundVideoSetting").val() },
                    { Key: "BackgroundImageSetting", Value: $("#BackgroundImageSetting").val() },
                    { Key: "BackgroundColorSetting", Value: $("#BackgroundColorSetting").val() }
                ]
            }
        };

        function StoreWebSettings() {
            Metro.storage.setItem('NotifyShowTime', $("#NotifyShowTime").val());
            Metro.storage.setItem('AutomaticDetectedLanguageTranslate', $("#AutomaticDetectedLanguageTranslate").val('checked')[0].checked);
            Metro.storage.setItem('BackgroundOpacitySetting', $("#BackgroundOpacitySetting").val());
            Metro.storage.setItem('BackgroundVideoSetting', $("#BackgroundVideoSetting").val());
            Metro.storage.setItem('BackgroundImageSetting', $("#BackgroundImageSetting").val());
            Metro.storage.setItem('BackgroundColorSetting', $("#BackgroundColorSetting").val());
            ApplyLoadedWebSetting("isRuntimeChange");
        };

        async function SaveWebSettings() {
            window.showPageLoading();
            let response = await fetch(Metro.storage.getItem('ApiOriginSuffix', null) + '/WebPages/SetSettingList', {
                method: 'POST',
                headers: {
                    "Authorization": 'Bearer ' + Metro.storage.getItem('ApiToken', null),
                    'Content-type': 'application/json'
                },
                body: JSON.stringify(WebSettingsToJson())
            });
            let result = await response.json();

            if (result.errors) {
                var notify = Metro.notify; notify.setup({ width: 300, timeout: Metro.storage.getItem('NotifyShowTime', null), duration: 500 });
                notify.create(JSON.stringify(result), "Error", { cls: "alert" }); notify.reset();
            } else {
                var notify = Metro.notify; notify.setup({ width: 300, timeout: Metro.storage.getItem('NotifyShowTime', null), duration: 500 });
                notify.create("UloĹľenĂ­ bylo ĂşspÄ›ĹˇnĂ©", "Success", { cls: "success" }); notify.reset();
                StoreWebSettings();
            }
            window.hidePageLoading();
        }

    </script>
</div>
