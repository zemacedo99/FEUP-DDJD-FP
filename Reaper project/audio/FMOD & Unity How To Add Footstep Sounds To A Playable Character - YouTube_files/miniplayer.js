(function(g){var window=this;'use strict';var phb=function(a,b){g.V.call(this,{G:"button",Ja:["ytp-miniplayer-expand-watch-page-button","ytp-button","ytp-miniplayer-button-top-left"],X:{title:"{{title}}","data-tooltip-target-id":"ytp-miniplayer-expand-watch-page-button","aria-keyshortcuts":"i","data-title-no-tooltip":"{{data-title-no-tooltip}}"},W:[{G:"svg",X:{height:"24px",version:"1.1",viewBox:"0 0 24 24",width:"24px"},W:[{G:"g",X:{fill:"none","fill-rule":"evenodd",stroke:"none","stroke-width":"1"},W:[{G:"g",X:{transform:"translate(12.000000, 12.000000) scale(-1, 1) translate(-12.000000, -12.000000) "},
W:[{G:"path",X:{d:"M19,19 L5,19 L5,5 L12,5 L12,3 L5,3 C3.89,3 3,3.9 3,5 L3,19 C3,20.1 3.89,21 5,21 L19,21 C20.1,21 21,20.1 21,19 L21,12 L19,12 L19,19 Z M14,3 L14,5 L17.59,5 L7.76,14.83 L9.17,16.24 L19,6.41 L19,10 L21,10 L21,3 L14,3 Z",fill:"#fff","fill-rule":"nonzero"}}]}]}]}]});this.F=a;this.Sa("click",this.onClick,this);this.updateValue("title",g.QT(a,"Expand","i"));this.update({"data-title-no-tooltip":"Expand"});g.lb(this,g.KS(b.Jc(),this.element))},qhb=function(a){g.V.call(this,{G:"div",
N:"ytp-miniplayer-ui"});this.dg=!1;this.player=a;this.S(a,"minimized",this.Vh);this.S(a,"onStateChange",this.zP)},rhb=function(a){g.tT.call(this,a);
this.u=new g.XH(this);this.j=new qhb(this.player);this.j.hide();g.yS(this.player,this.j.element,4);a.Ag()&&(this.load(),g.Sp(a.getRootNode(),"ytp-player-minimized",!0))};
g.x(phb,g.V);phb.prototype.onClick=function(){this.F.Oa("onExpandMiniplayer")};g.x(qhb,g.V);g.k=qhb.prototype;
g.k.AM=function(){this.tooltip=new g.SV(this.player,this);g.I(this,this.tooltip);g.yS(this.player,this.tooltip.element,4);this.tooltip.scale=.6;this.Qc=new g.bU(this.player);g.I(this,this.Qc);this.Wj=new g.V({G:"div",N:"ytp-miniplayer-scrim"});g.I(this,this.Wj);this.Wj.Ea(this.element);this.S(this.Wj.element,"click",this.gH);var a=new g.V({G:"button",Ja:["ytp-miniplayer-close-button","ytp-button"],X:{"aria-label":"Close"},W:[g.yQ()]});g.I(this,a);a.Ea(this.Wj.element);this.S(a.element,"click",this.bp);
a=new phb(this.player,this);g.I(this,a);a.Ea(this.Wj.element);this.Lu=new g.V({G:"div",N:"ytp-miniplayer-controls"});g.I(this,this.Lu);this.Lu.Ea(this.Wj.element);this.S(this.Lu.element,"click",this.gH);var b=new g.V({G:"div",N:"ytp-miniplayer-button-container"});g.I(this,b);b.Ea(this.Lu.element);a=new g.V({G:"div",N:"ytp-miniplayer-play-button-container"});g.I(this,a);a.Ea(this.Lu.element);var c=new g.V({G:"div",N:"ytp-miniplayer-button-container"});g.I(this,c);c.Ea(this.Lu.element);this.aX=new g.dV(this.player,
this,!1);g.I(this,this.aX);this.aX.Ea(b.element);b=new g.cV(this.player,this);g.I(this,b);b.Ea(a.element);this.nextButton=new g.dV(this.player,this,!0);g.I(this,this.nextButton);this.nextButton.Ea(c.element);this.sj=new g.LV(this.player,this);g.I(this,this.sj);this.sj.Ea(this.Wj.element);this.Lc=new g.iV(this.player,this);g.I(this,this.Lc);g.yS(this.player,this.Lc.element,4);this.VG=new g.V({G:"div",N:"ytp-miniplayer-buttons"});g.I(this,this.VG);g.yS(this.player,this.VG.element,4);a=new g.V({G:"button",
Ja:["ytp-miniplayer-close-button","ytp-button"],X:{"aria-label":"Close"},W:[g.yQ()]});g.I(this,a);a.Ea(this.VG.element);this.S(a.element,"click",this.bp);a=new g.V({G:"button",Ja:["ytp-miniplayer-replay-button","ytp-button"],X:{"aria-label":"Close"},W:[g.FQ()]});g.I(this,a);a.Ea(this.VG.element);this.S(a.element,"click",this.y7);this.S(this.player,"presentingplayerstatechange",this.Bd);this.S(this.player,"appresize",this.Eb);this.S(this.player,"fullscreentoggled",this.Eb);this.Eb()};
g.k.show=function(){this.mf=new g.Ep(this.Hv,null,this);this.mf.start();this.dg||(this.AM(),this.dg=!0);0!==this.player.getPlayerState()&&g.V.prototype.show.call(this);this.Lc.show();this.player.unloadModule("annotations_module")};
g.k.hide=function(){this.mf&&(this.mf.dispose(),this.mf=void 0);g.V.prototype.hide.call(this);this.player.Ag()||(this.dg&&this.Lc.hide(),this.player.loadModule("annotations_module"))};
g.k.qa=function(){this.mf&&(this.mf.dispose(),this.mf=void 0);g.V.prototype.qa.call(this)};
g.k.bp=function(){this.player.stopVideo();this.player.Oa("onCloseMiniplayer")};
g.k.y7=function(){this.player.playVideo()};
g.k.gH=function(a){if(a.target===this.Wj.element||a.target===this.Lu.element)g.dP(this.player.Db())?this.player.pauseVideo():this.player.playVideo()};
g.k.Vh=function(){g.Sp(this.player.getRootNode(),"ytp-player-minimized",this.player.Ag())};
g.k.Le=function(){this.Lc.zc();this.sj.zc()};
g.k.Hv=function(){this.Le();this.mf&&this.mf.start()};
g.k.Bd=function(a){g.cO(a.state,32)&&this.tooltip.hide()};
g.k.Eb=function(){g.uV(this.Lc,0,this.player.kb().getPlayerSize().width,!1);g.jV(this.Lc)};
g.k.zP=function(a){this.player.Ag()&&(0===a?this.hide():this.show())};
g.k.Jc=function(){return this.tooltip};
g.k.zg=function(){return!1};
g.k.Xg=function(){return!1};
g.k.Ub=function(){return!1};
g.k.Sl=function(){return!1};
g.k.cI=function(){};
g.k.Pp=function(){};
g.k.qy=function(){};
g.k.Qm=function(){return null};
g.k.QF=function(){return null};
g.k.SL=function(){return new g.xe(0,0)};
g.k.Ck=function(){return new g.Mm(0,0,0,0)};
g.k.handleGlobalKeyDown=function(){return!1};
g.k.handleGlobalKeyUp=function(){return!1};
g.k.Tv=function(a,b,c,d,e){var f=0,h=d=0,l=g.$m(a);if(b){c=g.Np(b,"ytp-prev-button")||g.Np(b,"ytp-next-button");var m=g.Np(b,"ytp-play-button"),n=g.Np(b,"ytp-miniplayer-expand-watch-page-button");c?f=h=12:m?(b=g.Ym(b,this.element),h=b.x,f=b.y-12):n&&(h=g.Np(b,"ytp-miniplayer-button-top-left"),f=g.Ym(b,this.element),b=g.$m(b),h?(h=8,f=f.y+40):(h=f.x-l.width+b.width,f=f.y-20))}else h=c-l.width/2,d=25+(e||0);b=this.player.kb().getPlayerSize().width;e=f+(e||0);l=g.pe(h,0,b-l.width);e?(a.style.top=e+"px",
a.style.bottom=""):(a.style.top="",a.style.bottom=d+"px");a.style.left=l+"px"};
g.k.showControls=function(){};
g.k.vp=function(){};
g.k.Kl=function(){return!1};
g.k.RD=function(){};
g.k.Pz=function(){};
g.k.Xq=function(){};
g.k.Wq=function(){};
g.k.VA=function(){};
g.k.Yr=function(){};
g.k.DD=function(){};g.x(rhb,g.tT);g.k=rhb.prototype;g.k.onVideoDataChange=function(){if(this.player.getVideoData()){var a=this.player.getVideoAspectRatio(),b=16/9;a=a>b+.1||a<b-.1;g.Sp(this.player.getRootNode(),"ytp-rounded-miniplayer-not-regular-wide-video",a)}};
g.k.create=function(){g.tT.prototype.create.call(this);this.u.S(this.player,"videodatachange",this.onVideoDataChange);this.onVideoDataChange()};
g.k.Vk=function(){return!1};
g.k.load=function(){this.player.hideControls();this.j.show()};
g.k.unload=function(){this.player.showControls();this.j.hide()};g.sT("miniplayer",rhb);})(_yt_player);
