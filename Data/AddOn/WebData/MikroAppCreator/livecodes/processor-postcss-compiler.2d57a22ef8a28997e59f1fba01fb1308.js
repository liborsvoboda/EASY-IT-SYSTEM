"use strict";(()=>{var et=Object.create;var Q=Object.defineProperty;var tt=Object.getOwnPropertyDescriptor;var rt=Object.getOwnPropertyNames;var st=Object.getPrototypeOf,ot=Object.prototype.hasOwnProperty;var nt=(e,t)=>()=>(t||e((t={exports:{}}).exports,t),t.exports);var it=(e,t,n,u)=>{if(t&&typeof t=="object"||typeof t=="function")for(let d of rt(t))!ot.call(e,d)&&d!==n&&Q(e,d,{get:()=>t[d],enumerable:!(u=tt(t,d))||u.enumerable});return e};var at=(e,t,n)=>(n=e!=null?et(st(e)):{},it(t||!e||!e.__esModule?Q(n,"default",{value:e,enumerable:!0}):n,e));var Ke=nt((eg,F)=>{var ze=function(){var e=String.fromCharCode,t="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",n="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+-$",u={};function d(s,o){if(!u[s]){u[s]={};for(var l=0;l<s.length;l++)u[s][s.charAt(l)]=l}return u[s][o]}var y={compressToBase64:function(s){if(s==null)return"";var o=y._compress(s,6,function(l){return t.charAt(l)});switch(o.length%4){default:case 0:return o;case 1:return o+"===";case 2:return o+"==";case 3:return o+"="}},decompressFromBase64:function(s){return s==null?"":s==""?null:y._decompress(s.length,32,function(o){return d(t,s.charAt(o))})},compressToUTF16:function(s){return s==null?"":y._compress(s,15,function(o){return e(o+32)})+" "},decompressFromUTF16:function(s){return s==null?"":s==""?null:y._decompress(s.length,16384,function(o){return s.charCodeAt(o)-32})},compressToUint8Array:function(s){for(var o=y.compress(s),l=new Uint8Array(o.length*2),i=0,m=o.length;i<m;i++){var w=o.charCodeAt(i);l[i*2]=w>>>8,l[i*2+1]=w%256}return l},decompressFromUint8Array:function(s){if(s==null)return y.decompress(s);for(var o=new Array(s.length/2),l=0,i=o.length;l<i;l++)o[l]=s[l*2]*256+s[l*2+1];var m=[];return o.forEach(function(w){m.push(e(w))}),y.decompress(m.join(""))},compressToEncodedURIComponent:function(s){return s==null?"":y._compress(s,6,function(o){return n.charAt(o)})},decompressFromEncodedURIComponent:function(s){return s==null?"":s==""?null:(s=s.replace(/ /g,"+"),y._decompress(s.length,32,function(o){return d(n,s.charAt(o))}))},compress:function(s){return y._compress(s,16,function(o){return e(o)})},_compress:function(s,o,l){if(s==null)return"";var i,m,w={},L={},C="",U="",S="",v=2,_=3,x=2,b=[],c=0,p=0,j;for(j=0;j<s.length;j+=1)if(C=s.charAt(j),Object.prototype.hasOwnProperty.call(w,C)||(w[C]=_++,L[C]=!0),U=S+C,Object.prototype.hasOwnProperty.call(w,U))S=U;else{if(Object.prototype.hasOwnProperty.call(L,S)){if(S.charCodeAt(0)<256){for(i=0;i<x;i++)c=c<<1,p==o-1?(p=0,b.push(l(c)),c=0):p++;for(m=S.charCodeAt(0),i=0;i<8;i++)c=c<<1|m&1,p==o-1?(p=0,b.push(l(c)),c=0):p++,m=m>>1}else{for(m=1,i=0;i<x;i++)c=c<<1|m,p==o-1?(p=0,b.push(l(c)),c=0):p++,m=0;for(m=S.charCodeAt(0),i=0;i<16;i++)c=c<<1|m&1,p==o-1?(p=0,b.push(l(c)),c=0):p++,m=m>>1}v--,v==0&&(v=Math.pow(2,x),x++),delete L[S]}else for(m=w[S],i=0;i<x;i++)c=c<<1|m&1,p==o-1?(p=0,b.push(l(c)),c=0):p++,m=m>>1;v--,v==0&&(v=Math.pow(2,x),x++),w[U]=_++,S=String(C)}if(S!==""){if(Object.prototype.hasOwnProperty.call(L,S)){if(S.charCodeAt(0)<256){for(i=0;i<x;i++)c=c<<1,p==o-1?(p=0,b.push(l(c)),c=0):p++;for(m=S.charCodeAt(0),i=0;i<8;i++)c=c<<1|m&1,p==o-1?(p=0,b.push(l(c)),c=0):p++,m=m>>1}else{for(m=1,i=0;i<x;i++)c=c<<1|m,p==o-1?(p=0,b.push(l(c)),c=0):p++,m=0;for(m=S.charCodeAt(0),i=0;i<16;i++)c=c<<1|m&1,p==o-1?(p=0,b.push(l(c)),c=0):p++,m=m>>1}v--,v==0&&(v=Math.pow(2,x),x++),delete L[S]}else for(m=w[S],i=0;i<x;i++)c=c<<1|m&1,p==o-1?(p=0,b.push(l(c)),c=0):p++,m=m>>1;v--,v==0&&(v=Math.pow(2,x),x++)}for(m=2,i=0;i<x;i++)c=c<<1|m&1,p==o-1?(p=0,b.push(l(c)),c=0):p++,m=m>>1;for(;;)if(c=c<<1,p==o-1){b.push(l(c));break}else p++;return b.join("")},decompress:function(s){return s==null?"":s==""?null:y._decompress(s.length,32768,function(o){return s.charCodeAt(o)})},_decompress:function(s,o,l){var i=[],m,w=4,L=4,C=3,U="",S=[],v,_,x,b,c,p,j,g={val:l(0),position:o,index:1};for(v=0;v<3;v+=1)i[v]=v;for(x=0,c=Math.pow(2,2),p=1;p!=c;)b=g.val&g.position,g.position>>=1,g.position==0&&(g.position=o,g.val=l(g.index++)),x|=(b>0?1:0)*p,p<<=1;switch(m=x){case 0:for(x=0,c=Math.pow(2,8),p=1;p!=c;)b=g.val&g.position,g.position>>=1,g.position==0&&(g.position=o,g.val=l(g.index++)),x|=(b>0?1:0)*p,p<<=1;j=e(x);break;case 1:for(x=0,c=Math.pow(2,16),p=1;p!=c;)b=g.val&g.position,g.position>>=1,g.position==0&&(g.position=o,g.val=l(g.index++)),x|=(b>0?1:0)*p,p<<=1;j=e(x);break;case 2:return""}for(i[3]=j,_=j,S.push(j);;){if(g.index>s)return"";for(x=0,c=Math.pow(2,C),p=1;p!=c;)b=g.val&g.position,g.position>>=1,g.position==0&&(g.position=o,g.val=l(g.index++)),x|=(b>0?1:0)*p,p<<=1;switch(j=x){case 0:for(x=0,c=Math.pow(2,8),p=1;p!=c;)b=g.val&g.position,g.position>>=1,g.position==0&&(g.position=o,g.val=l(g.index++)),x|=(b>0?1:0)*p,p<<=1;i[L++]=e(x),j=L-1,w--;break;case 1:for(x=0,c=Math.pow(2,16),p=1;p!=c;)b=g.val&g.position,g.position>>=1,g.position==0&&(g.position=o,g.val=l(g.index++)),x|=(b>0?1:0)*p,p<<=1;i[L++]=e(x),j=L-1,w--;break;case 2:return S.join("")}if(w==0&&(w=Math.pow(2,C),C++),i[j])U=i[j];else if(j===L)U=_+_.charAt(0);else return null;S.push(U),i[L++]=_+U.charAt(0),w--,_=U,w==0&&(w=Math.pow(2,C),C++)}}};return y}();typeof define=="function"&&define.amd?define(function(){return ze}):typeof F<"u"&&F!=null&&(F.exports=ze)});var ee=["esm.sh","skypack","jspm"],te=["unpkg","jsdelivr","fastly.jsdelivr"],re=["fastly.jsdelivr.gh","jsdelivr.gh","statically"],P={getModuleUrl:(e,{isModule:t=!0,defaultCDN:n="esm.sh"}={})=>{e=e.replace(/#nobundle/g,"");let u=Y(e,t,n);return u||(t?"https://esm.sh/"+e:"https://cdn.jsdelivr.net/npm/"+e)},getUrl:(e,t)=>e.startsWith("http")||e.startsWith("data:")?e:Y(e,!1,t||se())||e,cdnLists:{npm:te,module:ee,gh:re},checkCDNs:async(e,t)=>{let n=[t,...P.cdnLists.npm].filter(Boolean);for(let u of n)try{if((await fetch(P.getUrl(e,u),{method:"HEAD"})).ok)return u}catch{}return P.cdnLists.npm[0]}},se=()=>{if(globalThis.appCDN)return globalThis.appCDN;try{return new URL(location.href).searchParams.get("appCDN")||P.cdnLists.npm[0]}catch{return P.cdnLists.npm[0]}},Y=(e,t,n)=>{let u=t&&e.startsWith("unpkg:")?"?module":"";e.startsWith("gh:")?e=e.replace("gh",re[0]):e.includes(":")||(e=(n||(t?ee[0]:te[0]))+":"+e);for(let d of ct){let[y,s]=d;if(y.test(e))return e.replace(y,s)+u}return null},ct=[[/^(jspm:)(.+)/i,"https://jspm.dev/$2"],[/^(npm:)(.+)/i,"https://esm.sh/$2"],[/^(node:)(.+)/i,"https://esm.sh/$2"],[/^(skypack:)(.+)/i,"https://cdn.skypack.dev/$2"],[/^(jsdelivr:)(.+)/i,"https://cdn.jsdelivr.net/npm/$2"],[/^(fastly.jsdelivr:)(.+)/i,"https://fastly.jsdelivr.net/npm/$2"],[/^(jsdelivr.gh:)(.+)/i,"https://cdn.jsdelivr.net/gh/$2"],[/^(fastly.jsdelivr.gh:)(.+)/i,"https://fastly.jsdelivr.net/gh/$2"],[/^(statically:)(.+)/i,"https://cdn.statically.io/gh/$2"],[/^(esm.run:)(.+)/i,"https://esm.run/$2"],[/^(esm.sh:)(.+)/i,"https://esm.sh/$2"],[/^(esbuild:)(.+)/i,"https://esbuild.vercel.app/$2"],[/^(bundle.run:)(.+)/i,"https://bundle.run/$2"],[/^(unpkg:)(.+)/i,"https://unpkg.com/$2"],[/^(bundlejs:)(.+)/i,"https://deno.bundlejs.com/?file&q=$2"],[/^(bundle:)(.+)/i,"https://deno.bundlejs.com/?file&q=$2"],[/^(deno:)(.+)/i,"https://deno.bundlejs.com/?file&q=https://deno.land/x/$2/mod.ts"],[/^(https:\/\/deno\.land\/.+)/i,"https://deno.bundlejs.com/?file&q=$1"],[/^(github:|https:\/\/github\.com\/)(.[^\/]+?)\/(.[^\/]+?)\/(?!releases\/)(?:(?:blob|raw)\/)?(.+?\/.+)/i,"https://deno.bundlejs.com/?file&q=https://cdn.jsdelivr.net/gh/$2/$3@$4"],[/^(gist\.github:)(.+?\/[0-9a-f]+\/raw\/(?:[0-9a-f]+\/)?.+)$/i,"https://gist.githack.com/$2"],[/^(gitlab:|https:\/\/gitlab\.com\/)([^\/]+.*\/[^\/]+)\/(?:raw|blob)\/(.+?)(?:\?.*)?$/i,"https://deno.bundlejs.com/?file&q=https://gl.githack.com/$2/raw/$3"],[/^(bitbucket:|https:\/\/bitbucket\.org\/)([^\/]+\/[^\/]+)\/(?:raw|src)\/(.+?)(?:\?.*)?$/i,"https://deno.bundlejs.com/?file&q=https://bb.githack.com/$2/raw/$3"],[/^(bitbucket:)snippets\/([^\/]+\/[^\/]+)\/revisions\/([^\/\#\?]+)(?:\?[^#]*)?(?:\#file-(.+?))$/i,"https://bb.githack.com/!api/2.0/snippets/$2/$3/files/$4"],[/^(bitbucket:)snippets\/([^\/]+\/[^\/\#\?]+)(?:\?[^#]*)?(?:\#file-(.+?))$/i,"https://bb.githack.com/!api/2.0/snippets/$2/HEAD/files/$3"],[/^(bitbucket:)\!api\/2.0\/snippets\/([^\/]+\/[^\/]+\/[^\/]+)\/files\/(.+?)(?:\?.*)?$/i,"https://bb.githack.com/!api/2.0/snippets/$2/files/$3"],[/^(api\.bitbucket:)2.0\/snippets\/([^\/]+\/[^\/]+\/[^\/]+)\/files\/(.+?)(?:\?.*)?$/i,"https://bb.githack.com/!api/2.0/snippets/$2/files/$3"],[/^(rawgit:)(.+?\/[0-9a-f]+\/raw\/(?:[0-9a-f]+\/)?.+)$/i,"https://gist.githack.com/$2"],[/^(rawgit:|https:\/\/raw\.githubusercontent\.com)(\/[^\/]+\/[^\/]+|[0-9A-Za-z-]+\/[0-9a-f]+\/raw)\/(.+)/i,"https://deno.bundlejs.com/?file&q=https://raw.githack.com/$2/$3"]];var{getUrl:h,getModuleUrl:Pr}=P,a=h("@live-codes/browser-compilers@0.9.0/dist/");var oe=h("art-template@4.13.2/lib/template-web.js");var ne=h("@assemblyscript/loader@0.27.5/umd/index.js");var ie=h("@hatemhosny/astro-internal@0.0.4/");var ae=h("@babel/standalone@7.22.4/babel.js");var D=h("brython@3.12.1/");var W=h("cherry-cljs@0.0.4/");var N=h("@live-codes/clio-browser-compiler@0.0.3/public/build/");var ce=h("dot@1.1.3/doT.js"),pe=h("ejs@3.1.9/ejs.js");var le=h("eta@2.2.0/dist/eta.umd.js");var E=h("@live-codes/go2js@0.4.0/build/");var H=h("handlebars@4.7.7/dist/");var J=h("imba@2.0.0-alpha.229/dist/");var me=h("liquidjs@10.8.2/dist/liquid.browser.min.js");var z="0.6.64",kr=h(`malinajs@${z}/malina.js`),ue=h("marked@5.0.4/marked.min.js");var ge=h("mjml-browser@4.14.1/lib/index.js");var de=h("mustache@4.2.0/mustache.js");var K=h("nunjucks@3.2.4/browser/"),I=h("https://cdn.opalrb.com/opal/1.7.3/"),fe=h("parinfer@3.13.1/parinfer.js");var ye=h("@live-codes/postcss-import-url@0.1.2/dist/postcss-import-url.js"),T=h("prettier@3.0.0/"),he=h("@prettier/plugin-php@0.19.6/standalone.js");var G=h("riot@7.1.0/");var xe=h("sql-formatter@12.2.1/dist/sql-formatter.min.js"),be=h("sql.js@1.8.0/dist/"),we=h("@stencil/core@3.2.2/compiler/stencil.js");var O=h("https://unpkg.com/svelte@4.0.0/src/runtime/internal/");var Se=h("@mhsdesign/jit-browser-tailwindcss@0.3.0/dist/cdn.min.js");var ve=h("twig@1.16.0/twig.min.js"),je=h("typescript@5.1.3/lib/typescript.js"),Le=h("uniter@2.18.0/dist/uniter.js");var V=h("vue@2"),Ce=h("vue@3.3.4/dist/vue.runtime.esm-browser.prod.js"),Ue=h("livecodes@0.4.0/vue.js"),Pe=h("vue3-sfc-loader@0.8.4/dist/");var M=(e,t=!0)=>e.replace(/\\/g,t?"\\\\":"\\").replace(/`/g,"\\`").replace(/<\/script>/g,"<\\/script>");var ke=e=>!e?.startsWith("http")&&!e?.startsWith("data:"),_e=(e,t=document.baseURI)=>ke(e)?new URL(e,t).href:e;var f=(e,t)=>({...t.customSettings[e]});var Te={name:"lightningcss",title:"Lightning CSS",isPostcssPlugin:!1,compiler:{url:a+"lightningcss/lightningcss.js",factory:(e,t)=>(self.importScripts(t+"processor-lightningcss-compiler.48a9cf9ef9b27f1af936c72a90193f1a.js"),self.createLightningcssCompiler())},editor:"style"};var Ee={name:"postcss",title:"Processors:",isPostcssPlugin:!1,compiler:{url:a+"postcss/postcss.js",factory:(e,t)=>(self.importScripts(t+"waiting"),self.createPostcssCompiler())},editor:"style",hidden:!0};var Ae={name:"autoprefixer",title:"Autoprefixer",isPostcssPlugin:!0,compiler:{url:a+"autoprefixer/autoprefixer.js",factory:e=>self.autoprefixer.autoprefixer({...f("autoprefixer",e)})},editor:"style"},Ie={name:"cssnano",title:"cssnano",isPostcssPlugin:!0,compiler:{url:a+"cssnano/cssnano.js",factory:()=>{let e=self.cssnano.cssnanoPresetDefault().plugins,t=[];for(let n of e){let[u,d]=n;(typeof d>"u"||typeof d=="object"&&!d.exclude||typeof d=="boolean"&&d===!0)&&t.push(u(d))}return t}},editor:"style"},Me={name:"postcssImportUrl",title:"Import Url",isPostcssPlugin:!0,compiler:{url:ye,factory:e=>self.postcssImportUrl({...f("postcssImportUrl",e)})},editor:"style"},Re={name:"postcssPresetEnv",title:"Preset Env",isPostcssPlugin:!0,compiler:{url:a+"postcss-preset-env/postcss-preset-env.js",factory:e=>self.postcssPresetEnv.postcssPresetEnv({autoprefixer:!1,...f("postcssPresetEnv",e)})},editor:"style"},Be={name:"purgecss",title:"PurgeCSS",isPostcssPlugin:!0,needsHTML:!0,compiler:{url:a+"purgecss/purgecss.js",factory:(e,t,n)=>self.purgecss.purgecss({...f("purgecss",e),content:[{raw:`<template>${n.html}
<script>${e.script.content}<\/script></template>`,extension:"html"}]})},editor:"style"},We={name:"tokencss",title:"Token CSS",isPostcssPlugin:!0,compiler:{url:a+"tokencss/tokencss.js",factory:e=>{let t=f("tokencss",e);Object.keys(t).length===0&&(t.$schema="https://tokencss.com/schema@0.0.1",t.extends="@tokencss/core/preset");let n=(d,y)=>{let s=JSON.parse(JSON.stringify(d));return Object.keys(y).forEach(o=>{s[o]=typeof y[o]!="object"||Array.isArray(y[o])?y[o]:{...s[o],...y[o]}}),s},u=t.extends?.includes("@tokencss/core/preset")?n(self.tokencss.preset,t):t;return self.tokencss.tokencss({config:u})}},editor:"style"},Oe={name:"cssmodules",title:"CSS Modules",isPostcssPlugin:!0,needsHTML:!0,compiler:{url:a+"postcss-modules/postcss-modules.js",factory:(e,t,n)=>{let u=f("cssmodules",e);return self.postcssModules.postcssModules({localsConvention:"camelCase",...u,getJSON(d,y,s){let o=u.addClassesToHTML!==!1,l=u.removeOriginalClasses===!0;o&&(n.html=self.postcssModules.addClassesToHtml(n.html,y,l)),n.compileInfo={...n.compileInfo,cssModules:y,...o?{modifiedHTML:n.html}:{}}}})}},editor:"style"};var $e={name:"tailwindcss",title:"Tailwind CSS",isPostcssPlugin:!1,needsHTML:!0,compiler:{url:Se,factory:(e,t)=>(self.importScripts(t+"processor-tailwindcss-compiler.ac07550ce2c8738308618a7537427ccc.js"),self.createTailwindcssCompiler())},editor:"style"};var qe={name:"unocss",title:"UnoCSS",isPostcssPlugin:!1,needsHTML:!0,compiler:{url:a+"unocss/unocss.js",factory:(e,t)=>(self.importScripts(t+"processor-unocss-compiler.99bd002593d3c7d5a46e509b5401a7f8.js"),self.createUnocssCompiler())},editor:"style"};var Fe={name:"windicss",title:"Windi CSS",isPostcssPlugin:!1,needsHTML:!0,compiler:{url:a+"windicss/windicss.js",factory:(e,t)=>(self.importScripts(t+"processor-windicss-compiler.c365cb04e31f1094050e565a06c38ed6.js"),self.createWindicssCompiler())},editor:"style"};var k=[$e,Fe,qe,We,Be,Me,Ae,Re,Te,Ie,Oe,Ee];var $=(e,t)=>k.map(n=>n.name).includes(e)?t.languages?t.languages.includes(e):!0:!1;var Ls=T+"standalone.js",r={babel:T+"plugins/babel.js",estree:T+"plugins/estree.js",glimmer:T+"plugins/glimmer.js",html:T+"plugins/html.js",markdown:T+"plugins/markdown.js",postcss:T+"plugins/postcss.js",php:he,pug:a+"prettier/parser-pug.js"};var pt={name:"babel",title:"Babel",parser:{name:"babel",pluginUrls:[r.babel,r.html]},compiler:{url:ae,factory:()=>async(e,{config:t})=>window.Babel.transform(e,{filename:"script.tsx",presets:[["env",{modules:!1}],"typescript","react"],...f("babel",t)}).code},extensions:["es","babel"],editor:"script",editorLanguage:"typescript"};var lt={name:"css",title:"CSS",info:!1,parser:{name:"css",pluginUrls:[r.postcss]},compiler:{factory:()=>async e=>e},extensions:["css"],editor:"style"};var mt={name:"haml",title:"Haml",compiler:{url:a+"clientside-haml-js/haml.js",factory:(e,t)=>(self.importScripts(t+"lang-haml-compiler.4fb75c67608adbe894ba55d78eb8d57c.js"),self.createHamlCompiler())},extensions:["haml"],editor:"markup"};var ut={name:"html",title:"HTML",info:!1,parser:{name:"html",pluginUrls:[r.html]},compiler:{factory:()=>async e=>e},extensions:["html","htm"],editor:"markup"};var gt={name:"javascript",title:"JS",longTitle:"JavaScript",info:!1,parser:{name:"babel",pluginUrls:[r.babel,r.html]},compiler:{factory:()=>async e=>e},extensions:["js"],editor:"script"};var dt={name:"jsx",title:"JSX",parser:{name:"babel",pluginUrls:[r.babel,r.html]},compiler:"typescript",extensions:["jsx"],editor:"script",editorLanguage:"javascript"};var ft={name:"tsx",title:"TSX",parser:{name:"babel-ts",pluginUrls:[r.babel,r.html]},compiler:"typescript",extensions:["tsx"],editor:"script",editorLanguage:"typescript"};var yt={name:"less",title:"Less",parser:{name:"less",pluginUrls:[r.postcss]},compiler:{url:a+"less/less.js",factory:()=>async(e,{config:t})=>(await window.less.render(e,{...f("less",t)})).css},extensions:["less"],editor:"style"};var ht={name:"markdown",title:"Markdown",parser:{name:"markdown",pluginUrls:[r.markdown,r.html]},compiler:{url:ue,factory:()=>async(e,{config:t})=>window.marked.parse(e,{headerIds:!1,mangle:!1,...f("markdown",t)})},extensions:["md","markdown","mdown","mkdn"],editor:"markup"};var R=e=>typeof e=="string"?{code:e,info:{}}:e;var A=async(e,t,n,u={},d=self)=>new Promise(y=>{if(!e||!t||!n)return y(R(""));let s=async function(o){let l=o.data.payload;o.data.trigger==="compileInCompiler"&&l?.content===e&&l?.language===t&&(d.removeEventListener("message",s),y(R(l.compiled)))};d.addEventListener("message",s),d.postMessage({type:"compileInCompiler",payload:{content:e,language:t,config:n,options:u}})});var xt=async(e,{config:t,worker:n})=>new Promise(async u=>{if(!e)return u("");let[d,{default:y}]=await Promise.all([import(a+"mdx/mdx.js"),import(a+"remark-gfm/remark-gfm.js")]),s=(await d.compile(e,{remarkPlugins:[y],...f("mdx",t)})).value,l=(w=>w.replace(/, {[^}]*} = _components/g,"").replace(/const {[^:]*} = props.components[^;]*;/g,""))(s),i=`import React from "react";
import { createRoot } from "react-dom/client";
${M(l,!1)}
createRoot(document.querySelector('#__livecodes_mdx_root__')).render(<MDXContent />,);
`,m=(await A(i,"jsx",t,{},n)).code;u(`<div id="__livecodes_mdx_root__"></div><script type="module">${m}<\/script>`)}),bt={name:"mdx",title:"MDX",parser:{name:"markdown",pluginUrls:[r.markdown,r.html]},compiler:{factory:()=>async e=>e,runOutsideWorker:xt,compiledCodeLanguage:"javascript"},extensions:["mdx"],editor:"markup",editorLanguage:"markdown"};var wt={name:"pug",title:"Pug",compiler:{url:a+"pug/pug.min.js",factory:(e,t)=>(self.importScripts(t+"lang-pug-compiler.61645362532461bc77195784b673d3fd.js"),self.createPugCompiler())},extensions:["pug","jade"],editor:"markup"};var St={name:"scss",title:"SCSS",parser:{name:"scss",pluginUrls:[r.postcss]},compiler:{url:a+"sass/sass.js",factory:(e,t)=>(self.importScripts(t+"lang-scss-compiler.363305ec5cea3f2059bf412e72d8badc.js"),self.createScssCompiler())},extensions:["scss"],editor:"style"};var vt={name:"svelte",title:"Svelte",parser:{name:"html",pluginUrls:[r.html,r.babel]},compiler:{url:a+"svelte/svelte-compiler.min.js",factory:(e,t)=>(self.importScripts(t+"lang-svelte-compiler.0901808d3e201b9d9f00f83ae40c0c4f.js"),self.createSvelteCompiler()),imports:{svelte:O+"index.js","svelte/internal":O+"index.js","svelte/internal/disclose-version":O+"disclose-version/index.js"}},extensions:["svelte"],editor:"script",editorLanguage:"html"};var jt={name:"stylus",title:"Stylus",compiler:{url:a+"stylus/stylus.min.js",factory:()=>async e=>window.stylus.render(e)},extensions:["styl"],editor:"style"};var Lt=(e,t)=>{let n={...f("typescript",t),...f(t.script.language,t)};return!!(n.jsx||n.jsxFactory||new RegExp(/\/\*\*[\s\*]*@jsx\s/g).test(e))},Z={target:"es2015",jsx:"react",allowUmdGlobalAccess:!0,esModuleInterop:!0},Ct={name:"typescript",title:"TS",longTitle:"TypeScript",parser:{name:"babel-ts",pluginUrls:[r.babel,r.html]},compiler:{url:je,factory:()=>async(e,{config:t})=>window.ts.transpile(e,{...Z,...["jsx","tsx"].includes(t.script.language)&&!Lt(e,t)?{jsx:"react-jsx"}:{},...f("typescript",t),...f(t.script.language,t)})},extensions:["ts","typescript"],editor:"script"};var Ut=a+"vue-compiler-sfc/vue-compiler-sfc.js",Pt={name:"vue",title:"Vue 3",longTitle:"Vue 3 SFC",parser:{name:"html",pluginUrls:[r.html]},compiler:{url:Ut,factory:(e,t)=>(self.importScripts(t+"lang-vue-compiler.4abf2976fa8bd17a2a45689e71abc003.js"),self.createVueCompiler()),imports:{vue:Ce,"livecodes/vue":Ue}},extensions:["vue","vue3"],editor:"script",editorLanguage:"html"};var kt=Pe+"vue2-sfc-loader.js",_t={name:"vue2",title:"Vue 2",longTitle:"Vue 2 SFC",parser:{name:"html",pluginUrls:[r.html]},compiler:{factory:(e,t)=>(self.importScripts(t+"lang-vue2-compiler.8c5fbc7b0786e50861d2af958d8ba36b.js"),self.createVue2Compiler()),scripts:[V,kt],imports:{vue:V+"/dist/vue.runtime.esm-browser.prod.js"}},extensions:["vue2"],editor:"script",editorLanguage:"html"};var Tt={name:"stencil",title:"Stencil",parser:{name:"babel-ts",pluginUrls:[r.babel,r.html]},compiler:{url:we,factory:()=>async(e,{config:t})=>(await window.stencil.transpile(e,{sourceMap:!1,target:"es2019",...f("stencil",t)})).code,types:{"@stencil/core":{url:a+"types/stencil-core.d.ts",declareAsModule:!1}}},extensions:["stencil.tsx"],editor:"script",editorLanguage:"typescript"};var Et={name:"livescript",title:"LiveScript",compiler:{url:a+"livescript/livescript-min.js",factory:()=>async(e,{config:t})=>window.require("livescript").compile(e,{bare:!0,...f("livescript",t)}),scripts:[a+"livescript/prelude-browser-min.js"]},extensions:["ls"],editor:"script"};var At=a+"assemblyscript/assemblyscript.js",It={name:"assemblyscript",title:"AS",longTitle:"AssemblyScript",parser:{name:"babel-ts",pluginUrls:[r.babel]},compiler:{url:At,factory:(e,t)=>(self.importScripts(t+"lang-assemblyscript-compiler.600543e522af84dce8288caf7b499bb0.js"),self.createAssemblyscriptCompiler()),scripts:({baseUrl:e})=>[ne,e+"lang-assemblyscript-script.c67aa66c1862e6d0289ba8fdb284d511.js"],scriptType:"application/wasm-uint8",compiledCodeLanguage:"wat",types:{assemblyscript:{url:a+"types/assemblyscript.d.ts",declareAsModule:!1,autoload:!0}}},extensions:["as","ts"],editor:"script",editorLanguage:"typescript"};var En=D+"brython.min.js",An=D+"brython_stdlib.js";var Rt=(e,t={})=>Array.from(new Set([...e.matchAll(new RegExp(/^\s*self\.\$require\("(\S+)"\);/gm))].map(n=>n[1]).map(n=>n.split("/")[0]).filter(n=>t.hasOwnProperty(n)||n!=="opal").map(n=>t[n]||`${I+n}.min.js`))),Bt={name:"ruby",title:"Ruby",compiler:{url:I+"opal.min.js",factory:()=>(importScripts(I+"opal-parser.min.js"),self.Opal.config.unsupported_features_severity="ignore",self.Opal.load("opal-parser"),async(e,{config:t})=>{let{autoloadStdlib:n,requireMap:u,...d}=f("ruby",t),y=e.includes("$0")?`$0 = __FILE__
`:"";return self.Opal.compile(y+e,d)}),scripts:({compiled:e,config:t})=>{let{autoloadStdlib:n,requireMap:u}=f("ruby",t),d=Rt(e,u),y=n!==!1?d:[];return[I+"opal.min.js",...y]}},extensions:["rb"],editor:"script"};var Wt={name:"php",title:"PHP",parser:{name:"php",pluginUrls:[r.php]},compiler:{factory:()=>async e=>(e=e.trim(),e.startsWith("<?php")&&(e=e.replace("<?php","/* <?php */"),e.endsWith("?>")&&(e=e.replace("?>","/* ?> */"))),e),scripts:[Le],deferScripts:!0,scriptType:"text/x-uniter-php",compiledCodeLanguage:"php"},extensions:["php"],editor:"script"};var Xn=a+"lua-fmt/lua-fmt.js";var q=()=>{let e=fe;return self.importScripts(e),async t=>({formatted:window.parinfer.parenMode(t).text,cursorOffset:0})};var Ot={name:"solid",title:"Solid",parser:{name:"babel",pluginUrls:[r.babel,r.html]},compiler:{dependencies:["babel"],url:a+"babel-preset-solid/babel-preset-solid.js",factory:(e,t)=>(self.importScripts(t+"lang-solid-compiler.d87f733d0a768ffda0bc6e8d27740503.js"),self.createSolidCompiler())},extensions:["solid.jsx"],editor:"script",editorLanguage:"javascript"};var $t={name:"solid.tsx",title:"Solid (TS)",parser:{name:"babel-ts",pluginUrls:[r.babel,r.html]},compiler:"solid",extensions:["solid.tsx"],editor:"script",editorLanguage:"typescript"};var qt={name:"liquid",title:"Liquid",parser:{name:"html",pluginUrls:[r.html]},compiler:{url:me,factory:(e,t)=>(self.importScripts(t+"lang-liquid-compiler.e68bb28cd934ef812d77ee8b83f7d433.js"),self.createLiquidCompiler())},extensions:["liquid","liquidjs"],editor:"markup",editorLanguage:"html"};var Ft={name:"ejs",title:"EJS",parser:{name:"html",pluginUrls:[r.html]},compiler:{url:pe,factory:(e,t)=>(self.importScripts(t+"lang-ejs-compiler.91f0ea2135f8b5f5e526c76b97a76f30.js"),self.createEjsCompiler())},extensions:["ejs"],editor:"markup",editorLanguage:"html"};var Dt=H+"handlebars.min.js",Ri=H+"handlebars.runtime.min.js",Nt={name:"handlebars",title:"Handlebars",parser:{name:"glimmer",pluginUrls:[r.glimmer]},compiler:{url:Dt,factory:(e,t)=>(self.importScripts(t+"lang-handlebars-compiler.418dc69949bbf9c5b857a4cc2965c1ce.js"),self.createHandlebarsCompiler())},extensions:["hbs","handlebars"],editor:"markup",editorLanguage:"html"};var Ht={name:"dot",title:"doT",parser:{name:"html",pluginUrls:[r.html]},compiler:{url:ce,factory:(e,t)=>(self.importScripts(t+"lang-dot-compiler.8a378ddc83ab8a55fcc9dc8e8f07e8e3.js"),self.createDotCompiler())},extensions:["dot"],editor:"markup",editorLanguage:"html"};var Jt=K+"nunjucks.min.js",zi=K+"nunjucks-slim.min.js",zt={name:"nunjucks",title:"Nunjucks",parser:{name:"html",pluginUrls:[r.html]},compiler:{url:Jt,factory:(e,t)=>(self.importScripts(t+"lang-nunjucks-compiler.068f4fec06cf424894eed3291b25650c.js"),self.createNunjucksCompiler())},extensions:["njk","nunjucks"],editor:"markup",editorLanguage:"html"};var Kt={name:"go",title:"Go",formatter:{factory:()=>(importScripts(E+"go2js-format.js"),async e=>{if(!e)return{formatted:"",cursorOffset:0};let[t,n]=globalThis.go2jsFormat(e);return n?(console.error(n),{formatted:e,cursorOffset:0}):{formatted:t,cursorOffset:0}})},compiler:{url:E+"go2js-compile.js",factory:()=>e=>new Promise(t=>{if(!e){t("");return}let n=E.endsWith("/")?E.slice(0,-1):E;globalThis.go2jsCompile(e,n,(u,d)=>{u?(console.error(u),t("")):t(d)})})},extensions:["go","golang"],editor:"script"};var ga=a+"wast-refmt/wast-refmt.js";var Gt=G+"riot+compiler.min.js",Vt=G+"riot.min.js",Zt={name:"riot",title:"Riot.js",parser:{name:"html",pluginUrls:[r.html,r.babel]},compiler:{url:Gt,factory:(e,t)=>(self.importScripts(t+"lang-riot-compiler.ffdca65bd03e3b811afc3ac4b9ecef8d.js"),self.createRiotCompiler()),scripts:[Vt],scriptType:"module"},extensions:["riot","riotjs"],editor:"script",editorLanguage:"html"};var Xt="application/json",Qt={name:"sql",title:"SQL",formatter:{factory:()=>(importScripts(xe),async e=>({formatted:await self.sqlFormatter.format(e,{linesBetweenQueries:2}),cursorOffset:0}))},compiler:{url:be+"sql-wasm.js",factory:(e,t)=>(self.importScripts(t+"lang-sql-compiler.1fa8701be0143e72e8400ef9870614f0.js"),self.createSqlCompiler()),scripts:({baseUrl:e})=>[e+"lang-sql-script.b774113dd7e5f2bea2f140515e4fae8f.js"],scriptType:Xt,compiledCodeLanguage:"json"},extensions:["sql","sqlite","sqlite3"],editor:"script"};var He=a+"react-native-web/react-native-web.js",Yt={name:"react-native",title:"RN",longTitle:"React Native",parser:{name:"babel",pluginUrls:[r.babel,r.html]},compiler:{dependencies:["typescript"],factory:()=>async(e,{config:t,language:n})=>window.ts.transpile(e,{...Z,jsx:"react-jsx",...f("typescript",t),...f(n,t)}),imports:{react:He,"react-native":He}},extensions:["react-native.jsx"],editor:"script",editorLanguage:"javascript"};var er={name:"react-native-tsx",title:"RN (TSX)",longTitle:"React Native (TSX)",parser:{name:"babel-ts",pluginUrls:[r.babel,r.html]},compiler:"react-native",extensions:["react-native.tsx"],editor:"script",editorLanguage:"typescript"};var tr={name:"twig",title:"Twig",parser:{name:"html",pluginUrls:[r.html]},compiler:{url:ve,factory:(e,t)=>(self.importScripts(t+"lang-twig-compiler.6b5c7309174a57495d2624f1b85056e1.js"),self.createTwigCompiler())},extensions:["twig"],editor:"markup",editorLanguage:"html"};var rr=ie+"compiler.min.js",sr={name:"astro",title:"Astro",parser:{name:"html",pluginUrls:[r.html,r.babel]},compiler:{url:rr,factory:(e,t)=>(self.importScripts(t+"lang-astro-compiler.6ef0dc8efe61a07b4d3a9dcb46e89ad3.js"),self.createAstroCompiler())},extensions:["astro"],editor:"markup"};var or={name:"malina",title:"Malina.js",parser:{name:"html",pluginUrls:[r.html,r.babel]},compiler:{factory:(e,t)=>(self.importScripts(t+"lang-malina-compiler.45378b2d89e57222f1adbe6e268c91c6.js"),self.createMalinaCompiler()),imports:{"malinajs/runtime.js":`https://jspm.dev/malinajs@${z}/runtime.js`}},extensions:["xht"],editor:"script",editorLanguage:"html"};var tc=a+"jscpp/JSCPP.es5.min.js";var nr={name:"clio",title:"Clio",compiler:{url:N+"compile.js",factory:(e,t)=>(self.importScripts(t+"lang-clio-compiler.94aca9723d51c3d926142491f3de48da.js"),self.createClioCompiler()),scripts:[N+"exec.js"]},extensions:["clio"],editor:"script",editorLanguage:"coffeescript"};var ir=async(e,{baseUrl:t,config:n})=>{let{diagramsCompiler:u}=await import(t+"lang-diagrams-compiler-esm.7319615cf88ab2c725abe8969097576d.js");return u(e,{config:n})},ar={name:"diagrams",title:"Diagrams",parser:{name:"html",pluginUrls:[r.html]},compiler:{factory:()=>async e=>e||"",runOutsideWorker:ir},extensions:["diagrams","diagram","graph","plt"],editor:"markup",editorLanguage:"html"};var cr={name:"imba",title:"Imba",compiler:{url:J+"compiler.js",factory:(e,t)=>(self.importScripts(t+"lang-imba-compiler.c5c7b69434893c97f82c4b3e8be7af1e.js"),self.createImbaCompiler()),imports:{imba:J+"imba.mjs"}},extensions:["imba"],editor:"script"};var pr={name:"mustache",title:"Mustache",parser:{name:"glimmer",pluginUrls:[r.glimmer]},compiler:{url:de,factory:(e,t)=>(self.importScripts(t+"lang-mustache-compiler.9b9c8f8b822f5321b06ba84f34b1c2df.js"),self.createMustacheCompiler())},extensions:["mustache"],editor:"markup",editorLanguage:"html"};var lr={name:"art-template",title:"art",longTitle:"art-template",parser:{name:"html",pluginUrls:[r.html]},compiler:{url:oe,factory:(e,t)=>(self.importScripts(t+"lang-art-template-compiler.0b0eabcf74e357f6368e8d9ad8ea2253.js"),self.createArtTemplateCompiler())},extensions:["art","art-template"],editor:"markup",editorLanguage:"html"};var Zc=a+"civet/civet.js";var ur={name:"flow",title:"Flow",parser:{name:"babel-flow",pluginUrls:[r.babel,r.html]},compiler:{url:a+"flow-remove-types/flow-remove-types.js",factory:()=>async(e,{config:t})=>window.flowRemoveTypes.transpile(e,{all:!0,...f("flow",t)}).toString()},extensions:["flow"],editor:"script",editorLanguage:"typescript"};var gr={name:"mjml",title:"MJML",parser:{name:"html",pluginUrls:[r.html]},compiler:{url:ge,factory:()=>async(e,{config:t})=>{if(!e.trim())return"";let{html:n,errors:u}=self.mjml(e,f("mjml",t));return u?.forEach(d=>{console.warn(d.formattedMessage)}),n}},extensions:["mjml"],editor:"markup",editorLanguage:"xml"};var dr={name:"sucrase",title:"Sucrase",parser:{name:"babel",pluginUrls:[r.babel,r.html]},compiler:{url:a+"sucrase/sucrase.js",factory:()=>async(e,{config:t})=>window.sucrase.transform(e,{transforms:["jsx","typescript"],...f("sucrase",t)}).code},extensions:["sucrase"],editor:"script",editorLanguage:"typescript"};var fr={name:"eta",title:"Eta",parser:{name:"html",pluginUrls:[r.html]},compiler:{url:le,factory:(e,t)=>(self.importScripts(t+"lang-eta-compiler.ae4d2dac4ab30e3976263cbb778e22f3.js"),self.createEtaCompiler())},extensions:["eta"],editor:"markup",editorLanguage:"html"};var yr={name:"clojurescript",title:"CLJS (cherry)",longTitle:"ClojureScript (cherry)",formatter:{factory:q},compiler:{url:W+"lib/cherry.umd.js",factory:()=>async(e,{config:t,options:n})=>{let u=self.CherryCljs.compileString(e);return e.includes("#jsx")?(await A(u,"jsx",t,n)).code:u},imports:{"cherry-cljs":W+"index.js","cherry-cljs/cljs.core.js":W+"cljs.core.js"}},extensions:["cljs","clj","cljc","edn","clojure"],editor:"script",editorLanguage:"clojure"};var hr={name:"php-wasm",title:"PHP (Wasm)",parser:{name:"php",pluginUrls:[r.php]},compiler:{factory:()=>async e=>e,scripts:({baseUrl:e})=>[a+"php-wasm/php-wasm.js",e+"lang-php-wasm-script.79ee18ec9a9fcdc4975925b5f2c1b352.js"],scriptType:"text/php-wasm",compiledCodeLanguage:"php"},extensions:["wasm.php","phpwasm"],editor:"script",editorLanguage:"php"};var B=(e=location.origin)=>!!(e&&(e.endsWith("livecodes.io")||e.endsWith("livecodes.pages.dev")||e.endsWith("localpen.pages.dev")||e.startsWith("http://127.0.0.1")||e.startsWith("http://localhost")));var X=at(Ke());var Ge="https://dpaste.com/",Sr="https://dpaste.com/api/v2/",Ve="https://api2.livecodes.io/share",Ze={getProject:async e=>{try{let t=await fetch(Ge+e+".txt");return t.ok?JSON.parse(await t.text()):{}}catch{return{}}},shareProject:async e=>{try{let t=await fetch(Sr,{method:"POST",mode:"cors",headers:{"Content-Type":"application/x-www-form-urlencoded","User-Agent":"LiveCodes / https://livecodes.io/"},body:`content=${encodeURIComponent(JSON.stringify(e))}&title=${encodeURIComponent(e.title||"")}&syntax=json&expiry_days=365`});return t.ok?(await t.text()).replace(Ge,""):""}catch{return""}}},vr={getProject:async e=>{if(e.length<11)return Ze.getProject(e);if(!B())return{};try{let t=await fetch(Ve+"?id="+e);return t.ok?JSON.parse(await t.text()):{}}catch{return{}}},shareProject:async e=>{if(!B())return"";try{let t=await fetch(Ve,{method:"POST",mode:"cors",body:JSON.stringify(e)});return t.ok?t.text():""}catch{return""}}},_g=B()?vr:Ze;var Xe=e=>!e.startsWith("https://")&&!e.startsWith("http://")&&!e.startsWith(".")&&!e.startsWith("/")&&!e.startsWith("data:")&&!e.startsWith("blob:");var jr=/(?:@import\s+?)((?:".*?")|(?:'.*?')|(?:url\('.*?'\))|(?:url\(".*?"\)))(.*)?;/g;var Qe=e=>e.replace(new RegExp(jr),(t,n,u)=>{let d=n.replace(/"/g,"").replace(/'/g,"").replace(/url\(/g,"").replace(/\)/g,""),y='@import "'+P.getUrl(d)+'";',s=u?.trim();return Xe(d)?s?`@media ${s} {
${y}
}`:y:t});var Ye=e=>k.find(t=>t.name===e);self.createPostcssCompiler=()=>{let e={from:void 0},t={},n=(s,o)=>{let l=Ye(s);if(!(!l||t[s]!=null))try{l.compiler.url&&self.importScripts(_e(l.compiler.url,o)),t[s]=l.compiler.factory}catch{throw new Error("Failed to load PostCSS plugin: "+s)}},u=s=>{let o=s.processors.filter(i=>Ye(i)?.isPostcssPlugin),l=i=>$(i,s)&&o.includes(i);return k.map(i=>i.name).filter(l)},d=(s,o,l)=>{let i=u(s);return i.forEach(m=>n(m,o)),k.filter(m=>i.includes(m.name)).map(m=>t[m.name]?.(s,o,l)).flat()},y=s=>M(Qe(s));return async function(o,{config:l,baseUrl:i,options:m}){if(!l||!i)return{code:o,info:{}};let w=d(l,i,m);return u(l).includes("tokencss")&&(o=`@inject "tokencss:base";
`+o),{code:(await self.postcss.postcss(w).process(y(o),e)).css,info:m.compileInfo||{}}}};})();