(function(){'use strict';const{basicSetup,EditorView}=CM["codemirror"];const{javascript,javascriptLanguage,scopeCompletionSource}=CM["@codemirror/lang-javascript"];
window.JsEditor=new EditorView({doc:`<script>
	alert('Hello');
</script>
`,extensions:[basicSetup,javascript(),javascriptLanguage.data.of({autocomplete:scopeCompletionSource(globalThis)})],parent:document.querySelector("#JsEditor")});})();