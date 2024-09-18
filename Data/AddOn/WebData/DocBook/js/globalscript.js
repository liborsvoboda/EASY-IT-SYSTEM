//Function for  Mermaid Data to Graphics Conversion
async function Mermaid() { try { await mermaid.run({ nodes: document.querySelectorAll('.language-mermaid'), }); } catch (err) { } }

//Function for Highlighting Code Segments
function HighlightCode() { document.querySelectorAll('div.code').forEach(el => { hljs.highlightElement(el); }); }




$(document).ready(function () { setTimeout(()=>{Mermaid();}, 3000); });



