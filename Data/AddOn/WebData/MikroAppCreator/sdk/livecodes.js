var Y=Object.create;var q=Object.defineProperty;var F=Object.getOwnPropertyDescriptor;var V=Object.getOwnPropertyNames;var ee=Object.getPrototypeOf,te=Object.prototype.hasOwnProperty;var oe=(c,g)=>()=>(g||c((g={exports:{}}).exports,g),g.exports);var ne=(c,g,P,w)=>{if(g&&typeof g=="object"||typeof g=="function")for(let v of V(g))!te.call(c,v)&&v!==P&&q(c,v,{get:()=>g[v],enumerable:!(w=F(g,v))||w.enumerable});return c};var re=(c,g,P)=>(P=c!=null?Y(ee(c)):{},ne(g||!c||!c.__esModule?q(P,"default",{value:c,enumerable:!0}):P,c));var Q=oe((ae,H)=>{var N=function(){var c=String.fromCharCode,g="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",P="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+-$",w={};function v(o,n){if(!w[o]){w[o]={};for(var d=0;d<o.length;d++)w[o][o.charAt(d)]=d}return w[o][n]}var b={compressToBase64:function(o){if(o==null)return"";var n=b._compress(o,6,function(d){return g.charAt(d)});switch(n.length%4){default:case 0:return n;case 1:return n+"===";case 2:return n+"==";case 3:return n+"="}},decompressFromBase64:function(o){return o==null?"":o==""?null:b._decompress(o.length,32,function(n){return v(g,o.charAt(n))})},compressToUTF16:function(o){return o==null?"":b._compress(o,15,function(n){return c(n+32)})+" "},decompressFromUTF16:function(o){return o==null?"":o==""?null:b._decompress(o.length,16384,function(n){return o.charCodeAt(n)-32})},compressToUint8Array:function(o){for(var n=b.compress(o),d=new Uint8Array(n.length*2),s=0,i=n.length;s<i;s++){var a=n.charCodeAt(s);d[s*2]=a>>>8,d[s*2+1]=a%256}return d},decompressFromUint8Array:function(o){if(o==null)return b.decompress(o);for(var n=new Array(o.length/2),d=0,s=n.length;d<s;d++)n[d]=o[d*2]*256+o[d*2+1];var i=[];return n.forEach(function(a){i.push(c(a))}),b.decompress(i.join(""))},compressToEncodedURIComponent:function(o){return o==null?"":b._compress(o,6,function(n){return P.charAt(n)})},decompressFromEncodedURIComponent:function(o){return o==null?"":o==""?null:(o=o.replace(/ /g,"+"),b._decompress(o.length,32,function(n){return v(P,o.charAt(n))}))},compress:function(o){return b._compress(o,16,function(n){return c(n)})},_compress:function(o,n,d){if(o==null)return"";var s,i,a={},y={},E="",L="",m="",x=2,S=3,f=2,p=[],e=0,t=0,A;for(A=0;A<o.length;A+=1)if(E=o.charAt(A),Object.prototype.hasOwnProperty.call(a,E)||(a[E]=S++,y[E]=!0),L=m+E,Object.prototype.hasOwnProperty.call(a,L))m=L;else{if(Object.prototype.hasOwnProperty.call(y,m)){if(m.charCodeAt(0)<256){for(s=0;s<f;s++)e=e<<1,t==n-1?(t=0,p.push(d(e)),e=0):t++;for(i=m.charCodeAt(0),s=0;s<8;s++)e=e<<1|i&1,t==n-1?(t=0,p.push(d(e)),e=0):t++,i=i>>1}else{for(i=1,s=0;s<f;s++)e=e<<1|i,t==n-1?(t=0,p.push(d(e)),e=0):t++,i=0;for(i=m.charCodeAt(0),s=0;s<16;s++)e=e<<1|i&1,t==n-1?(t=0,p.push(d(e)),e=0):t++,i=i>>1}x--,x==0&&(x=Math.pow(2,f),f++),delete y[m]}else for(i=a[m],s=0;s<f;s++)e=e<<1|i&1,t==n-1?(t=0,p.push(d(e)),e=0):t++,i=i>>1;x--,x==0&&(x=Math.pow(2,f),f++),a[L]=S++,m=String(E)}if(m!==""){if(Object.prototype.hasOwnProperty.call(y,m)){if(m.charCodeAt(0)<256){for(s=0;s<f;s++)e=e<<1,t==n-1?(t=0,p.push(d(e)),e=0):t++;for(i=m.charCodeAt(0),s=0;s<8;s++)e=e<<1|i&1,t==n-1?(t=0,p.push(d(e)),e=0):t++,i=i>>1}else{for(i=1,s=0;s<f;s++)e=e<<1|i,t==n-1?(t=0,p.push(d(e)),e=0):t++,i=0;for(i=m.charCodeAt(0),s=0;s<16;s++)e=e<<1|i&1,t==n-1?(t=0,p.push(d(e)),e=0):t++,i=i>>1}x--,x==0&&(x=Math.pow(2,f),f++),delete y[m]}else for(i=a[m],s=0;s<f;s++)e=e<<1|i&1,t==n-1?(t=0,p.push(d(e)),e=0):t++,i=i>>1;x--,x==0&&(x=Math.pow(2,f),f++)}for(i=2,s=0;s<f;s++)e=e<<1|i&1,t==n-1?(t=0,p.push(d(e)),e=0):t++,i=i>>1;for(;;)if(e=e<<1,t==n-1){p.push(d(e));break}else t++;return p.join("")},decompress:function(o){return o==null?"":o==""?null:b._decompress(o.length,32768,function(n){return o.charCodeAt(n)})},_decompress:function(o,n,d){var s=[],i,a=4,y=4,E=3,L="",m=[],x,S,f,p,e,t,A,l={val:d(0),position:n,index:1};for(x=0;x<3;x+=1)s[x]=x;for(f=0,e=Math.pow(2,2),t=1;t!=e;)p=l.val&l.position,l.position>>=1,l.position==0&&(l.position=n,l.val=d(l.index++)),f|=(p>0?1:0)*t,t<<=1;switch(i=f){case 0:for(f=0,e=Math.pow(2,8),t=1;t!=e;)p=l.val&l.position,l.position>>=1,l.position==0&&(l.position=n,l.val=d(l.index++)),f|=(p>0?1:0)*t,t<<=1;A=c(f);break;case 1:for(f=0,e=Math.pow(2,16),t=1;t!=e;)p=l.val&l.position,l.position>>=1,l.position==0&&(l.position=n,l.val=d(l.index++)),f|=(p>0?1:0)*t,t<<=1;A=c(f);break;case 2:return""}for(s[3]=A,S=A,m.push(A);;){if(l.index>o)return"";for(f=0,e=Math.pow(2,E),t=1;t!=e;)p=l.val&l.position,l.position>>=1,l.position==0&&(l.position=n,l.val=d(l.index++)),f|=(p>0?1:0)*t,t<<=1;switch(A=f){case 0:for(f=0,e=Math.pow(2,8),t=1;t!=e;)p=l.val&l.position,l.position>>=1,l.position==0&&(l.position=n,l.val=d(l.index++)),f|=(p>0?1:0)*t,t<<=1;s[y++]=c(f),A=y-1,a--;break;case 1:for(f=0,e=Math.pow(2,16),t=1;t!=e;)p=l.val&l.position,l.position>>=1,l.position==0&&(l.position=n,l.val=d(l.index++)),f|=(p>0?1:0)*t,t<<=1;s[y++]=c(f),A=y-1,a--;break;case 2:return m.join("")}if(a==0&&(a=Math.pow(2,E),E++),s[A])L=s[A];else if(A===y)L=S+S.charAt(0);else return null;m.push(L),s[y++]=S+L.charAt(0),a--,S=L,a==0&&(a=Math.pow(2,E),E++)}}};return b}();typeof define=="function"&&define.amd?define(function(){return N}):typeof H!="undefined"&&H!=null&&(H.exports=N)});var G=re(Q());async function se(c,g={}){typeof c=="object"&&!(c instanceof HTMLElement)&&c.view==="headless"&&(g=c,c=null);let{appUrl:P="https://livecodes.io/",params:w={},config:v={},import:b,lite:o,loading:n="lazy",template:d,view:s="split"}=g,i=s==="headless",a=null;if(typeof c=="string")a=document.querySelector(c);else if(c instanceof HTMLElement)a=c;else if(!(i&&typeof c=="object"))throw new Error("A valid container element is required.");if(!a)if(i)a=document.createElement("div"),K(a),document.body.appendChild(a);else throw new Error(`Cannot find element: "${c}"`);let y;try{y=new URL(P)}catch(r){throw new Error(`"${P}" is not a valid URL.`)}let E=y.origin;if(typeof w=="object"&&Object.keys(w).forEach(r=>{y.searchParams.set(r,String(w[r]))}),typeof v=="string")try{new URL(v),y.searchParams.set("config",v)}catch(r){throw new Error('"config" is not a valid URL or configuration object.')}else if(typeof v=="object")Object.keys(v).length>0&&y.searchParams.set("config","sdk");else throw new Error('"config" is not a valid URL or configuration object.');d&&y.searchParams.set("template",d),b&&y.searchParams.set("x",b),o&&y.searchParams.set("lite","true"),y.searchParams.set("embed","true"),y.searchParams.set("loading",i?"eager":n),y.searchParams.set("view",s);let L=!1,m="Cannot call API methods after calling `destroy()`.",S=await(()=>new Promise(r=>{var O,T,C,U,R,j,_,z,W;if(!a)return;let h=a.dataset.height||a.style.height;if(h&&!i){let k=isNaN(Number(h))?h:h+"px";a.style.height=k}a.dataset.defaultStyles!=="false"&&!i&&((O=a.style).backgroundColor||(O.backgroundColor="#fff"),(T=a.style).border||(T.border="1px solid black"),(C=a.style).borderRadius||(C.borderRadius="5px"),(U=a.style).boxSizing||(U.boxSizing="border-box"),(R=a.style).padding||(R.padding="0"),(j=a.style).width||(j.width="100%"),(_=a.style).height||(_.height=a.style.height||"300px"),a.style.minHeight="200px",a.style.flexGrow="1",(z=a.style).overflow||(z.overflow="hidden"),(W=a.style).resize||(W.resize="vertical"));let u=document.createElement("iframe");u.setAttribute("allow","accelerometer; camera; encrypted-media; display-capture; geolocation; gyroscope; microphone; midi; clipboard-read; clipboard-write; web-share"),u.setAttribute("allowtransparency","true"),u.setAttribute("allowpaymentrequest","true"),u.setAttribute("allowfullscreen","true"),u.setAttribute("sandbox","allow-same-origin allow-downloads allow-forms allow-modals allow-orientation-lock allow-pointer-lock allow-popups allow-presentation allow-scripts");let M=n==="eager"?"eager":"lazy";u.setAttribute("loading",M),u.classList.add("livecodes"),i?K(u):(u.style.height="100%",u.style.minHeight="200px",u.style.width="100%",u.style.margin="0",u.style.border="0",u.style.borderRadius=a.style.borderRadius),addEventListener("message",function k(D){var B,J;D.source!==u.contentWindow||D.origin!==E||((B=D.data)==null?void 0:B.type)!=="livecodes-get-config"||(removeEventListener("message",k),(J=u.contentWindow)==null||J.postMessage({type:"livecodes-config",payload:v},E))}),u.onload=()=>{r(u)},u.src=y.href,a.appendChild(u)}))(),f=new Promise(r=>{addEventListener("message",function h(u){var M;u.source!==S.contentWindow||u.origin!==E||((M=u.data)==null?void 0:M.type)!=="livecodes-ready"||(removeEventListener("message",h),r(),f.settled=!0)})}),p=()=>L?Promise.reject(m):new Promise(async r=>{var u;f.settled&&r();let h={type:"livecodes-load"};(u=S.contentWindow)==null||u.postMessage(h,E),await f,r()}),e=(r,h)=>new Promise(async(u,M)=>{var T;if(L)return M(m);await p();let O=X();addEventListener("message",function C(U){var R,j;if(!(U.source!==S.contentWindow||U.origin!==E||((R=U.data)==null?void 0:R.type)!=="livecodes-api-response"||((j=U.data)==null?void 0:j.id)!==O)&&U.data.method===r){removeEventListener("message",C);let _=U.data.payload;_!=null&&_.error?M(_.error):u(_)}}),(T=S.contentWindow)==null||T.postMessage({method:r,id:O,args:h},E)}),t={},A=["load","ready","code","console","tests","destroy"],l=(r,h)=>{var u;if(L)throw new Error(m);return A.includes(r)?(e("watch",[r]),t[r]||(t[r]=[]),(u=t[r])==null||u.push(h),{remove:()=>{var M,O;t[r]=(M=t[r])==null?void 0:M.filter(T=>T!==h),((O=t[r])==null?void 0:O.length)===0&&e("watch",[r,"unsubscribe"])}}):{remove:()=>{}}},$=r=>({"livecodes-app-loaded":"load","livecodes-ready":"ready","livecodes-change":"code","livecodes-console":"console","livecodes-test-results":"tests","livecodes-destroy":"destroy"})[r];addEventListener("message",async r=>{var M,O,T,C;let h=$((O=(M=r.data)==null?void 0:M.type)!=null?O:"");if(r.source!==S.contentWindow||r.origin!==E||!h||!t[h])return;let u=(T=r.data)==null?void 0:T.payload;(C=t[h])==null||C.forEach(U=>{U(u)})});let I=()=>{var r;Object.values(t).forEach(h=>{h.length=0}),(r=S==null?void 0:S.remove)==null||r.call(S),L=!0};n==="lazy"&&"IntersectionObserver"in window&&new IntersectionObserver((h,u)=>{h.forEach(async M=>{M.isIntersecting&&(await p(),u.unobserve(a))})},{rootMargin:"150px"}).observe(a);function K(r){r.style.position="absolute",r.style.top="0",r.style.visibility="hidden",r.style.opacity="0"}let X=()=>(String(Math.random())+Date.now().toFixed()).replace("0.","");return{load:()=>p(),run:()=>e("run"),format:r=>e("format",[r]),getShareUrl:r=>e("getShareUrl",[r]),getConfig:r=>e("getConfig",[r]),setConfig:r=>e("setConfig",[r]),getCode:()=>e("getCode"),show:(r,h)=>e("show",[r,h]),runTests:()=>e("runTests"),onChange:r=>l("code",r),watch:l,exec:(r,...h)=>e("exec",[r,...h]),destroy:()=>f.settled?e("destroy").then(I):L?Promise.reject(m):(I(),Promise.resolve())}}function le(c={}){let{appUrl:g,params:P,config:w,import:v,...b}=c,o=typeof w=="string"?{config:w}:typeof w=="object"?{x:"code/"+(0,G.compressToEncodedURIComponent)(JSON.stringify(w))}:{},n=new URLSearchParams(JSON.parse(JSON.stringify({...b,...P,x:v,...o}))).toString();return(g||"https://livecodes.io")+(n?"?"+n:"")}var Z;globalThis.document&&document.currentScript&&"prefill"in((Z=document.currentScript)==null?void 0:Z.dataset)&&window.addEventListener("load",()=>{document.querySelectorAll(".livecodes").forEach(c=>{let g,P=c.dataset.options;if(P)try{g=JSON.parse(P)}catch(o){}let w,v=c.dataset.config||c.dataset.prefill;if(v)try{w=JSON.parse(v)}catch(o){}let b=encodeURIComponent(c.outerHTML);c.innerHTML="",se(c,{import:"dom/"+b,...g,...w?{config:w}:{}})})});export{se as createPlayground,le as getPlaygroundUrl};