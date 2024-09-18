require.config({ paths: { 'vs': './js/monaco-editor/min/vs' } });
require(['vs/editor/editor.main'], function () {


    var fileCounter = 0;
    var editorArray = [];
    var defaultCode = [
        'function helloWorld() {',
        '   console.log("Hello world!");',
        '}'
    ].join('\n');


    monaco.editor.defineTheme('myTheme', {
        base: 'vs',
        inherit: true,
        rules: [{ background: 'EDF9FA' }],
        // colors: { 'editor.lineHighlightBackground': '#0000FF20' }
    });
    monaco.editor.setTheme('myTheme');


    function newEditor(container_id, code, language) {
        var model = monaco.editor.createModel(code, language);
        var editor = monaco.editor.create(document.getElementById(container_id), {
            model: model,
        });
        editorArray.push(editor);
        return editor;
    }


    function addNewEditor(code, language) {
        var new_container = document.createElement("DIV");
        new_container.id = "container-" + fileCounter.toString(10);
        new_container.className = "container";
        document.getElementById("root").appendChild(new_container);
        newEditor(new_container.id, code, language);
        fileCounter += 1;
    }

    addNewEditor(defaultCode, 'javascript');

    var languageSelected = document.querySelector('.language');    
    languageSelected.onchange = function () {
        monaco.editor.setModelLanguage(window.monaco.editor.getModels()[0], languageSelected.value)
    }

    var themeSelected = document.querySelector('.theme');    
    themeSelected.onchange = function () {
        monaco.editor.setTheme(themeSelected.value)
    }
	
	var text = document.createElement("DIV");
    text.id = "show-position";
    text.innerHTML = "Ln 1, Col 1";
    var header = document.getElementById("header");
    header.appendChild(text);

    var myEditor = editorArray[0];
    myEditor.onDidChangeCursorPosition((e) => {
        document.getElementById("show-position").innerHTML = "Ln " + myEditor.getPosition().lineNumber + ", Col " + myEditor.getPosition().column;
    });	
	
	
	
	var btn = document.createElement("button");
    btn.id = "show-content";
    btn.innerHTML = "Uložit Soubor";
    var header = document.getElementById("header");
    header.appendChild(btn);

    document.getElementById("show-content").addEventListener("click", function () {
        alert(editorArray[0].getValue());
    });
	

});