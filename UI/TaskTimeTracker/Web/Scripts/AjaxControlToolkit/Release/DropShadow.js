﻿Type.registerNamespace("Sys.Extended.UI");Sys.Extended.UI.DropShadowBehavior=function(n){Sys.Extended.UI.DropShadowBehavior.initializeBase(this,[n]);this._opacity=1;this._width=5;this._shadowDiv=null;this._trackPosition=null;this._trackPositionDelay=50;this._timer=null;this._tickHandler=null;this._roundedBehavior=null;this._shadowRoundedBehavior=null;this._rounded=!1;this._radius=5;this._lastX=null;this._lastY=null;this._lastW=null;this._lastH=null};Sys.Extended.UI.DropShadowBehavior.prototype={initialize:function(){Sys.Extended.UI.DropShadowBehavior.callBaseMethod(this,"initialize");e=this.get_element();$common.getCurrentStyle(e,"position",e.style.position)!="absolute"&&(e.style.position="relative");this._rounded&&this.setupRounded();this._trackPosition&&this.startTimer();this.setShadow()},dispose:function(){this.stopTimer();this.disposeShadowDiv();Sys.Extended.UI.DropShadowBehavior.callBaseMethod(this,"dispose")},buildShadowDiv:function(){if((e=this.get_element(),this.get_isInitialized()&&e&&this._width)&&e.style.boxShadow==undefined&&e.style.MozBoxShadow==undefined&&e.style.WebkitBoxShadow==undefined){var n=document.createElement("DIV");n.style.backgroundColor="black";n.style.position="absolute";e.id&&(n.id=e.id+"_DropShadow");this._shadowDiv=n;e.parentNode.appendChild(n);this._rounded?(this._shadowDiv.style.height=Math.max(0,e.offsetHeight-2*this._radius)+"px",this._shadowRoundedBehavior?this._shadowRoundedBehavior.set_Radius(this._radius):this._shadowRoundedBehavior=$create(Sys.Extended.UI.RoundedCornersBehavior,{Radius:this._radius},null,null,this._shadowDiv)):this._shadowRoundedBehavior&&this._shadowRoundedBehavior.set_Radius(0);this._opacity!=1&&this.setupOpacity();this.setShadow(!1,!0);this.updateZIndex()}},disposeShadowDiv:function(){this._shadowDiv&&(this._shadowDiv.parentNode&&this._shadowDiv.parentNode.removeChild(this._shadowDiv),this._shadowDiv=null);this._shadowRoundedBehavior&&(this._shadowRoundedBehavior.dispose(),this._shadowRoundedBehavior=null)},onTimerTick:function(){this.setShadow()},startTimer:function(){this._timer||(this._tickHandler||(this._tickHandler=Function.createDelegate(this,this.onTimerTick)),this._timer=new Sys.Timer,this._timer.set_interval(this._trackPositionDelay),this._timer.add_tick(this._tickHandler),this._timer.set_enabled(!0))},stopTimer:function(){this._timer&&(this._timer.remove_tick(this._tickHandler),this._timer.set_enabled(!1),this._timer.dispose(),this._timer=null)},setShadow:function(n,t){var u,i,f,r,o;(e=this.get_element(),this.get_isInitialized()&&e&&(this._width||n))&&(e.style.boxShadow==undefined&&e.style.MozBoxShadow==undefined&&e.style.WebkitBoxShadow==undefined?(u=this._shadowDiv,u||this.buildShadowDiv(),i={x:e.offsetLeft,y:e.offsetTop},(n||this._lastX!=i.x||this._lastY!=i.y||!u)&&(this._lastX=i.x,this._lastY=i.y,r=this.get_Width(),i.x+=r,i.y+=r,$common.setLocation(this._shadowDiv,i)),f=e.offsetHeight,r=e.offsetWidth,(n||f!=this._lastH||r!=this._lastW||!u)&&(this._lastW=r,this._lastH=f,this._rounded&&u&&!t?(this.disposeShadowDiv(),this.setShadow()):(this._shadowDiv.style.width=r+"px",this._shadowDiv.style.height=f+"px")),this._shadowDiv&&(this._shadowDiv.style.visibility=$common.getCurrentStyle(e,"visibility"))):(o=this._opacity==".25"?this._width+"px "+this._width+"px "+this._width+"px #D3D3D3":this._opacity==".5"?this._width+"px "+this._width+"px "+this._width+"px #778899":this._opacity==".75"?this._width+"px "+this._width+"px "+this._width+"px #808080":this._width+"px "+this._width+"px "+this._width+"px #000",e.style.boxShadow!=undefined?e.style.boxShadow=o:e.style.MozBoxShadow!=undefined?e.style.MozBoxShadow=o:e.style.WebkitBoxShadow!=undefined&&(e.style.WebkitBoxShadow=o)))},setupOpacity:function(){this.get_isInitialized()&&this._shadowDiv&&$common.setElementOpacity(this._shadowDiv,this._opacity)},setupRounded:function(){!this._roundedBehavior&&this._rounded&&(this._roundedBehavior=$create(Sys.Extended.UI.RoundedCornersBehavior,null,null,null,this.get_element()));this._roundedBehavior&&this._roundedBehavior.set_Radius(this._rounded?this._radius:0)},updateZIndex:function(){if(this._shadowDiv){var i=this.get_element(),n=i.style.zIndex,t=this._shadowDiv.style.zIndex;t&&n&&n>t||(n=Math.max(2,n),t=n-1,i.style.zIndex=n,this._shadowDiv.style.zIndex=t)}},updateRoundedCorners:function(){this.get_isInitialized()&&(this.setupRounded(),this.disposeShadowDiv(),this.setShadow())},get_Opacity:function(){return this._opacity},set_Opacity:function(n){this._opacity!=n&&(this._opacity=n,this.setShadow(),this.setupOpacity(),this.raisePropertyChanged("Opacity"))},get_Rounded:function(){return this._rounded},set_Rounded:function(n){n!=this._rounded&&(this._rounded=n,this.updateRoundedCorners(),this.raisePropertyChanged("Rounded"))},get_Radius:function(){return this._radius},set_Radius:function(n){n!=this._radius&&(this._radius=n,this.updateRoundedCorners(),this.raisePropertyChanged("Radius"))},get_Width:function(){return this._width},set_Width:function(n){n!=this._width&&(this._width=n,this._shadowDiv&&$common.setVisible(this._shadowDiv,n>0),this.setShadow(!0),this.raisePropertyChanged("Width"))},get_TrackPositionDelay:function(){return this._trackPositionDelay},set_TrackPositionDelay:function(n){n!=this._trackPositionDelay&&(this._trackPositionDelay=n,e||(e=this.get_element()),e.style.boxShadow==undefined&&e.style.MozBoxShadow==undefined&&e.style.WebkitBoxShadow==undefined&&(this._trackPosition&&(this.stopTimer(),this.startTimer()),this.raisePropertyChanged("TrackPositionDelay")))},get_TrackPosition:function(){return this._trackPosition},set_TrackPosition:function(n){n!=this._trackPosition&&(this._trackPosition=n,e||(e=this.get_element()),e.style.boxShadow==undefined&&e.style.MozBoxShadow==undefined&&e.style.WebkitBoxShadow==undefined&&(this.get_element()&&(n?this.startTimer():this.stopTimer()),this.raisePropertyChanged("TrackPosition")))}};Sys.Extended.UI.DropShadowBehavior.registerClass("Sys.Extended.UI.DropShadowBehavior",Sys.Extended.UI.BehaviorBase);