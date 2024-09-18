ď»ż@page 
@model ServerCorePages.WebMenuBuilderModel



<div class="text-center mb-4 pb-5">
    <window style="position: relative; top: 60px;">
        <div id="TogglePanelBackground" class="panel" style="min-height: 700px;">
            <form class="form1" data-role="validator" action="javascript:" data-on-submit="isValid = true;"
                  data-interactive-check="true" autocomplete="off" data-on-error="isValid = false;">
                <div class="d-block">
                    <div class="d-flex row gutters mr-4">
                        <div class="col-xl-9 col-lg-9 col-md-9 col-sm-9 col-9 pl-5 pt-5 mb-0">
                            <ul data-role="tabs" data-expand="true" data-tabs-type="text" data-on-tab="">
                                <li id="menuListMenu" class="fg-black"><a href="#_menuList">PĹ™ehled Menu</a></li>
                                <li id="menuBuilderMenu" class="fg-black "><a href="#_menuBuilder">Menu Builder</a></li>
                            </ul>
                        </div>

                        <div class="col-xl-3 col-lg-3 col-md-3 col-sm-3 pt-4 col-12">
                            <div class="text-right">
                                <button type="reset" class="button warning outline shadowed">
                                    Reset
                                </button>
                                <button type="submit" class="button info outline shadowed" onclick="saveNew = false;SaveMenu();">
                                    UloĹľit
                                </button>
                                <button type="submit" class="button success outline shadowed" onclick="saveNew = true;SaveMenu();">
                                    UloĹľit jako NovĂ©
                                </button>
                            </div>
                        </div>

                    </div>

                    <div id="_menuList">
                        <div class="d-flex row gutters ml-5 mr-5 mb-5 border">
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                                <table id="menuTable" class="table striped table-border mt-4"
                                       data-role="table" data-cls-component="mt-10" data-show-activity="true" data-rows="30" data-pagination="true"
                                       data-show-all-pages="false" data-check="true" data-check-style="1" data-check-type="radio" data-on-check-click="SetRecId()"
                                       data-use-current-slice="true">
                                    <thead>
                                        <tr>
                                            <th data-sortable="true">Id</th>
                                            <th data-sortable="true">Sekvence</th>
                                            <th data-sortable="true">Group Id</th>
                                            <th data-sortable="true">NĂˇzev Skupiny </th>
                                            <th data-sortable="true">NĂˇzev menu</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div id="_menuBuilder">
                        <div class="d-flex row gutters ml-5 mr-5 mb-5 border">
                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
                                <div class="form-group">
                                    <label for="Street">Sequence</label>
                                    <input id="menuSeq" type="number" data-role="spinner" data-min-value="0" data-step="10" data-max-value="30000">
                                </div>
                            </div>
                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
                                <div class="form-group pt-6">
                                    <select id="groupMenu" data-role="select" data-filter-placeholder="Vyberte Skupinu menu" data-empty-value="" data-validate="required not=-1" data-clear-button="true"></select>
                                </div>
                            </div>
                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
                                <div class="form-group pt-5">
                                    <input id="menuName" type="text" data-role="input" data-label="NĂˇzev" data-validate="required" autocomplete="off" style="height: auto;" />
                                </div>
                            </div>

                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
                                <div class="form-group pt-5">
                                    <input id="menuDescription" type="text" data-role="input" data-label="Popisek" autocomplete="off" style="height: auto;" />
                                </div>
                            </div>

                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
                                <div class="form-group pt-5">
                                    <input id="menuClass" type="text" data-role="input" data-label="TĹ™Ă­dy Menu" autocomplete="off" style="height: auto;" />
                                </div>
                            </div>

                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
                            </div>

                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                                <div id="menuSummernote"></div>
                            </div>

                        </div>
                    </div>
                    <div class="row gutters pr-5">
                        <div class="col-xl-9 col-lg-9 col-md-9 col-sm-9 col-12">
                        </div>
                        <div class="col-xl-3 col-lg-3 col-md-3 col-sm-3 pt-4 col-12">
                            <div class="text-right">
                                <button type="reset" class="button warning outline shadowed">
                                    Reset
                                </button>
                                <button type="submit" class="button info outline shadowed" onclick="saveNew = false">
                                    UloĹľit
                                </button>
                                <button type="submit" class="button success outline shadowed" onclick="saveNew = true">
                                    UloĹľit jako NovĂ©
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </window>
    <div class="mb-10"></div>

 
    <script>

        // Form Definitions
        let recId = 0;
        let isValid = false;
        let saveNew = false;


        //Startup Actions
        $('#menuSummernote').summernote({
            tabsize: 2, height: 300, maxHeight: 300,
            toolbar: [['style', ['style']], ['font', ['bold', 'underline', 'clear']], ['fontname', ['fontname']],
            ['fontsize', ['fontsize']], ['color', ['color']], ['para', ['ul', 'ol', 'paragraph']], ['table', ['table']],
            ['insert', ['link', 'picture', 'video']], ['view', ['fullscreen', 'codeview', 'undo', 'redo', 'help']]]
        });
        $(document).ready(function () { StartUp(); });


        //Functions
        async function StartUp() {
            let data = []; Metro.storage.getItem('WebMenuList', null).forEach(item => { data.push([item.id, item.sequence, item.groupId, item.group.name, item.name]); });
            var table = Metro.getPlugin("#menuTable", "table");
            console.log(data);
        
            table.setItems(data); table.reload();

            let result = await GetMenuGroupList();
            let selectMenu = Metro.getPlugin('#groupMenu', 'select');
            let option = [];
            result.forEach(group => { option.push({ val: group.id, title: group.name, selected: false }); });
            selectMenu.addOptions(option);
        }

        function MenuSettingToJson() {
            return {
                Settings: [
                    { Key: "GroupId", Value: $("#groupMenu")[0].selectedOptions[0].value },
                    { Key: "Sequence", Value: $("#menuSeq").val() },
                    { Key: "Name", Value: $("#menuName").val() },
                    { Key: "MenuClass", Value: $("#menuClass").val() },
                    { Key: "Description", Value: $("#menuDescription").val() },
                    { Key: "HtmlContent", Value: $("#menuSummernote").summernote('code') }
                ]
            }
        };

        function SetRecId() {
            var table = Metro.getPlugin("#menuTable", "table");
            var groupSelect = Metro.getPlugin('#groupMenu', 'select');
            let selectedMenu = Metro.storage.getItem('WebMenuList', null).filter(menu => { return menu.id == table.getSelectedItems()[0][0]; })[0];
            recId = selectedMenu.id;
            groupSelect.val(selectedMenu.groupId);
            $("#menuSeq").val(selectedMenu.sequence);
            $("#menuName").val(selectedMenu.name);
            $("#menuDescription").val(selectedMenu.description);
            $("#menuClass").val(selectedMenu.menuClass);
            $("#menuSummernote").summernote('code', selectedMenu.htmlContent);
        }

        async function SaveMenu() {
            if (isValid) {
                await setTimeout(async function () {
                    let data = MenuSettingToJson();
                    if (saveNew) { data.Settings.push({ Key: "Id", Value: 0 }); }
                    else { data.Settings.push({ Key: "Id", Value: recId }); }
                    window.showPageLoading();
                    let response = await fetch(Metro.storage.getItem('ApiOriginSuffix', null) + '/WebPages/SetMenuList', {
                        method: 'POST',
                        headers: {
                            "Authorization": 'Bearer ' + Metro.storage.getItem('ApiToken', null),
                            'Content-type': 'application/json'
                        }, body: JSON.stringify(data)
                    }); let result = await response.json();

                    if (result.Status == "error") {
                        var notify = Metro.notify; notify.setup({ width: 300, timeout: Metro.storage.getItem('NotifyShowTime', null), duration: 500 });
                        notify.create(JSON.stringify(result), "Error", { cls: "alert" }); notify.reset();
                    } else {
                        var notify = Metro.notify; notify.setup({ width: 300, timeout: Metro.storage.getItem('NotifyShowTime', null), duration: 500 });
                        notify.create("UloĹľenĂ­ bylo ĂşspÄ›ĹˇnĂ©", "Success", { cls: "success" }); notify.reset();
                        await GetWebMenuList();
                    }
                    window.hidePageLoading();

                }, 100);
            }
        }


    </script>
</div>
