

//INSERT JQUERY TO ANY WEB TOOL
//INSERT THIS FUNCTION FOR SAME USING WITH ANY TOOL WITH LOGIN I/O
//TEST EVERY TIME, CAN BE PROBLEM WITH WORK DATA IMMEDIATELY - SETTIMEOUT IS REQURED
//WRITE TO HEADER PART OFF TOOL



/*  Testing IO TOOL First - Example
 setTimeout(()=>{
    editor.html.set("test");
    editor.html.get();
 },1000);
*/

//TODO DOWNLOAD and CREATE LOCAL GALLERY with json GENERATOR 


//ESB Services Definitions
let ESBserver = "http://localhost:9000";
let ESBapiPaths = [
    { "systemLanguage": ["/SystemApi/GetSystemLanguage"] },
    { "test": ["aha"] },
]



//GLOBAL METHODS FOR AUTOUSE BY WEBVIEW2
function SetWebViewToEditorData(inputValue) {
    try {
        editorInstance.setContents(inputValue);
    //setTimeout(() => { editor.html.set(inputValue); }, 1000);
    } catch (err) { return false; }
    return true;
}

function GetWebViewFromEditorData() {
    try {
        return editorInstance.getContents();
    } catch (err) {
        return "WebViewEditorFalse";
    }
}

//Its Call Automatically with Openning Tool
//for non exist Config in Tool,
//Set Empty - non Error Method


//COMMAND FOR TOOL DIRECT CALL FOR LANGUAGE CODE
//GetDataFromESBhosting(ESBapiPaths["systemLanguage"]);


//CUSTOM METHOD for SETTING LANGUAGE CODE 
function SetSystemLanguage() {
    try {
        let esbLang = GetDataFromESBhosting(ESBapiPaths.filter(obj => obj.systemLanguage)[0].systemLanguage[0]);

        //HERE YOU write Command for set Languga in Tool;
    } catch (err) {
        //TODO write to DB LOG 
        return "SetToolLanguageError on:" + window.location.href;
    }
}




//Helpers Methods

async function GetDataFromESBhosting(apiPath) {
    try {
        let test = await fetch(window.location.origin + apiPath)
            .then(response => {
                if (!response.ok) { throw new Error('Network response was not ok'); }
                return response.json();
            }).then(data => { return data; });
    } catch (err) {
        //TODO write to DB LOG 
        return "SetToolLanguageError on:" + window.location.href;
    }
}


function CalcFullPageHeight() {
    return window.innerHeight;
}