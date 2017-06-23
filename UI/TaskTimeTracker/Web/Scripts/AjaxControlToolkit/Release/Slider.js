﻿Type.registerNamespace("Sys.Extended.UI");Sys.Extended.UI._SliderDragDropManagerInternal=function(){Sys.Extended.UI._SliderDragDropManagerInternal.initializeBase(this);this._instance=null};Sys.Extended.UI._SliderDragDropManagerInternal.prototype={_getInstance:function(){return this._instance=new Sys.Extended.UI.GenericDragDropManager,this._instance.initialize(),this._instance.add_dragStart(Function.createDelegate(this,this._raiseDragStart)),this._instance.add_dragStop(Function.createDelegate(this,this._raiseDragStop)),this._instance}};Sys.Extended.UI._SliderDragDropManagerInternal.registerClass("Sys.Extended.UI._SliderDragDropManagerInternal",Sys.Extended.UI._DragDropManager);Sys.Extended.UI.SliderDragDropManagerInternal=new Sys.Extended.UI._SliderDragDropManagerInternal;Sys.Extended.UI.SliderOrientation=function(){};Sys.Extended.UI.SliderOrientation.prototype={Horizontal:0,Vertical:1};Sys.Extended.UI.SliderOrientation.registerEnum("Sys.Extended.UI.SliderOrientation",!1);Sys.Extended.UI.SliderBehavior=function(n){Sys.Extended.UI.SliderBehavior.initializeBase(this,[n]);this._minimum=0;this._maximum=100;this._value=null;this._steps=0;this._decimals=0;this._orientation=Sys.Extended.UI.SliderOrientation.Horizontal;this._railElement=null;this._railCssClass=null;this._isHorizontal=!0;this._isUpdatingInternal=!1;this._isInitializedInternal=!1;this._enableHandleAnimation=!1;this._handle=null;this._handleImage=null;this._handleAnimation=null;this._handleAnimationDuration=.1;this._handleImageUrl=null;this._handleCssClass=null;this._dragHandle=null;this._mouseupHandler=null;this._selectstartHandler=null;this._boundControlChangeHandler=null;this._boundControlKeyPressHandler=null;this._boundControlID=null;this._boundControl=null;this._length=null;this._raiseChangeOnlyOnMouseUp=!0;this._animationPending=!1;this._selectstartPending=!1;this._tooltipText="";this._enableKeyboard=!0;this._keyDownHandler=null};Sys.Extended.UI.SliderBehavior.prototype={initialize:function(){Sys.Extended.UI.SliderBehavior.callBaseMethod(this,"initialize");this._initializeLayout()},dispose:function(){this._disposeHandlers();this._disposeBoundControl();this._enableHandleAnimation&&this._handleAnimation&&this._handleAnimation.dispose();Sys.Extended.UI.SliderBehavior.callBaseMethod(this,"dispose")},_initializeLayout:function(){var n,t;this._railElement=document.createElement("DIV");this._railElement.id=this.get_id()+"_railElement";this._railElement.tabIndex=-1;this._railElement.innerHTML="<div><\/div>";this._handle=this._railElement.childNodes[0];this._handle.style.overflow="hidden";this._handle.style.position="absolute";Sys.Browser.agent==Sys.Browser.Opera&&(this._handle.style.left="0px",this._handle.style.top="0px");n=this.get_element();t=$common.getBounds(n);n.parentNode.insertBefore(this._railElement,n);this._isHorizontal=this._orientation==Sys.Extended.UI.SliderOrientation.Horizontal;var i=this._isHorizontal?"ajax__slider_h_rail":"ajax__slider_v_rail",r=this._isHorizontal?"ajax__slider_h_handle":"ajax__slider_v_handle",u=this._isHorizontal?Sys.Extended.UI.Images["Slider.Handle-Horizontal.gif"]:Sys.Extended.UI.Images["Slider.Handle-Vertical.gif"];this._railElement.className=this._railCssClass?this._railCssClass:i;this._handle.className=this._handleCssClass?this._handleCssClass:r;this._handleImageUrl||(this._handleImageUrl=u);this._isHorizontal?this._length&&(this._railElement.style.width=this._length):this._length&&(this._railElement.style.height=this._length);this._loadHandleImage();this._enforceTextBoxElementPositioning();this._hideTextBoxElement();this._initializeSlider()},_enforceTextBoxElementPositioning:function(){var n={position:this.get_element().style.position,top:this.get_element().style.top,right:this.get_element().style.right,bottom:this.get_element().style.bottom,left:this.get_element().style.left};n.position!=""&&(this._railElement.style.position=n.position);n.top!=""&&(this._railElement.style.top=n.top);n.right!=""&&(this._railElement.style.right=n.right);n.bottom!=""&&(this._railElement.style.bottom=n.bottom);n.left!=""&&(this._railElement.style.left=n.left)},_hideTextBoxElement:function(){var n=this.get_element(),t;n.readOnly=!0;t="0px";Sys.Browser.agent==Sys.Browser.Safari&&(t="1px");n.style.width=t;n.style.height=t;n.style.border="0px";n.style.padding="0px";n.style.margin="0px";n.style.fontSize="1px";n.style.lineHeight="1px";n.style.outline="0";n.style.position="absolute"},_loadHandleImage:function(){this._handleImage=document.createElement("IMG");this._handleImage.id=this.get_id()+"_handleImage";this._handle.appendChild(this._handleImage);this._handleImage.src=this._handleImageUrl},_initializeSlider:function(){this._initializeBoundControl();var n;try{n=parseFloat(this.get_element().value)}catch(t){n=Number.NaN}this.set_Value(n);this._setHandleOffset(this._value);this._initializeDragHandle();Sys.Extended.UI.SliderDragDropManagerInternal.registerDropTarget(this);this._initializeHandlers();this._initializeHandleAnimation();this._isInitializedInternal=!0;this._raiseEvent("sliderInitialized")},_initializeBoundControl:function(){if(this._boundControl){var n=this._boundControl.nodeName=="INPUT";n&&(this._boundControlChangeHandler=Function.createDelegate(this,this._onBoundControlChange),this._boundControlKeyPressHandler=Function.createDelegate(this,this._onBoundControlKeyPress),$addHandler(this._boundControl,"change",this._boundControlChangeHandler),$addHandler(this._boundControl,"keypress",this._boundControlKeyPressHandler))}},_disposeBoundControl:function(){if(this._boundControl){var n=this._boundControl.nodeName=="INPUT";n&&($removeHandler(this._boundControl,"change",this._boundControlChangeHandler),$removeHandler(this._boundControl,"keypress",this._boundControlKeyPressHandler))}},_onBoundControlChange:function(){this._animationPending=!0;this._setValueFromBoundControl()},_onBoundControlKeyPress:function(n){n.charCode==13&&(this._animationPending=!0,this._setValueFromBoundControl(),n.preventDefault())},_setValueFromBoundControl:function(){this._isUpdatingInternal=!0;this._boundControlID&&this._calcValue($get(this._boundControlID).value);this._isUpdatingInternal=!1},_initializeHandleAnimation:function(){if(this._steps>0){this._enableHandleAnimation=!1;return}this._enableHandleAnimation&&(this._handleAnimation=new Sys.Extended.UI.Animation.LengthAnimation(this._handle,this._handleAnimationDuration,100,"style"))},_ensureBinding:function(){var n,t;this._boundControl&&(n=this._value,(n>=this._minimum||n<=this._maximum)&&(t=this._boundControl.nodeName=="INPUT",t?this._boundControl.value=n:this._boundControl&&(this._boundControl.innerHTML=n)))},_getBoundsInternal:function(n){function r(){return t.width>0&&t.height>0}var t=$common.getBounds(n),i;if(!r()&&(t.width=parseInt($common.getCurrentStyle(n,"width")),t.height=parseInt($common.getCurrentStyle(n,"height")),!r()&&(i=n.cloneNode(!0),i.visibility="hidden",document.body.appendChild(i),t.width=parseInt($common.getCurrentStyle(i,"width")),t.height=parseInt($common.getCurrentStyle(i,"height")),document.body.removeChild(i),!r())))throw Error.argument("element size",Sys.Extended.UI.Resources.Slider_NoSizeProvided);return this._orientation==Sys.Extended.UI.SliderOrientation.Vertical&&(t={x:t.y,y:t.x,height:t.width,width:t.height,right:t.right,bottom:t.bottom,location:{x:t.y,y:t.x},size:{width:t.height,height:t.width}}),t},_getRailBounds:function(){return this._getBoundsInternal(this._railElement)},_getHandleBounds:function(){return this._getBoundsInternal(this._handle)},_initializeDragHandle:function(){var n=this._dragHandle=document.createElement("DIV");n.style.position="absolute";n.style.width="1px";n.style.height="1px";n.style.overflow="hidden";n.style.zIndex=Sys.Extended.UI.zIndex.SliderDragHandle;n.style.background="none";document.body.appendChild(this._dragHandle)},_resetDragHandle:function(){var n=$common.getBounds(this._handle);$common.setLocation(this._dragHandle,{x:n.x,y:n.y})},_initializeHandlers:function(){this._selectstartHandler=Function.createDelegate(this,this._onSelectStart);this._mouseupHandler=Function.createDelegate(this,this._onMouseUp);this._keyDownHandler=Function.createDelegate(this,this._onKeyDown);$addHandler(document,"mouseup",this._mouseupHandler);$addHandler(this.get_element(),"keydown",this._keyDownHandler);$addHandlers(this._handle,{mousedown:this._onMouseDown,dragstart:this._IEDragDropHandler,drag:this._IEDragDropHandler,dragend:this._IEDragDropHandler},this);$addHandlers(this._railElement,{click:this._onRailClick},this)},_disposeHandlers:function(){$clearHandlers(this._handle);$clearHandlers(this._railElement);$removeHandler(this.get_element(),"keydown",this._keyDownHandler);$removeHandler(document,"mouseup",this._mouseupHandler);this._keyDownHandler=null;this._mouseupHandler=null;this._selectstartHandler=null},startDragDrop:function(n){this._resetDragHandle();Sys.Extended.UI.SliderDragDropManagerInternal.startDragDrop(this,n,null)},_onMouseDown:function(n){window._event=n;n.preventDefault();Sys.Extended.UI.SliderBehavior.DropPending||(Sys.Extended.UI.SliderBehavior.DropPending=this,$addHandler(document,"selectstart",this._selectstartHandler),this._selectstartPending=!0,this.startDragDrop(this._dragHandle))},_onMouseUp:function(n){var t=n.target;Sys.Extended.UI.SliderBehavior.DropPending==this&&(Sys.Extended.UI.SliderBehavior.DropPending=null,this._selectstartPending&&$removeHandler(document,"selectstart",this._selectstartHandler))},_onKeyDown:function(n){if(this._enableKeyboard){var t=new Sys.UI.DomEvent(n);switch(t.keyCode||t.rawEvent.keyCode){case Sys.UI.Key.up:case Sys.UI.Key.left:this._handleSlide(!0);t.preventDefault();return;case Sys.UI.Key.down:case Sys.UI.Key.right:return this._handleSlide(!1),t.preventDefault(),!1;default:return!1}}},_handleSlide:function(n){var i,t,r,u;this._animationPending=!0;this._isUpdatingInternal=!0;i=this.get_Value();this._steps>0?(r=this._maximum-this._minimum,u=(r/(this._steps-1)).toFixed(this._decimals),t=u):t=this._decimals==0?1:1/Math.pow(10,this._decimals);n&&(t=0-t);this._calcValue(parseFloat(i)+parseFloat(t));this._isUpdatingInternal=!1;this._fireTextBoxChangeEvent()},_onRailClick:function(n){n.target==this._railElement&&(this._animationPending=!0,this._onRailClicked(n))},_IEDragDropHandler:function(n){n.preventDefault()},_onSelectStart:function(n){n.preventDefault()},_calcValue:function(n,t){var i;if(n!=null){if(!Number.isInstanceOfType(n))try{n=parseFloat(n)}catch(c){n=Number.NaN}isNaN(n)&&(n=this._minimum);i=n<this._minimum?this._minimum:n>this._maximum?this._maximum:n}else{var u=this._minimum,o=this._maximum,r=this._getHandleBounds(),f=this._getRailBounds(),e=t?t-r.width/2:r.x-f.x,s=f.width-r.width,h=e/s;i=e==0?u:e==f.width-r.width?o:u+h*(o-u)}return this._steps>0&&(i=this._getNearestStepValue(i)),i=i<this._minimum?this._minimum:i>this._maximum?this._maximum:i,this._isUpdatingInternal=!0,this.set_Value(i),this._isUpdatingInternal=!1,i},_setHandleOffset:function(n,t){var i=this._minimum,e=this._maximum,r=this._getHandleBounds(),u=this._getRailBounds(),o=e-i,s=(n-i)/o,h=Math.round(s*(u.width-r.width)),f=n==i?0:n==e?u.width-r.width:h;t?(this._handleAnimation.set_startValue(r.x-u.x),this._handleAnimation.set_endValue(f),this._handleAnimation.set_propertyKey(this._isHorizontal?"left":"top"),this._handleAnimation.play(),this._animationPending=!1):this._isHorizontal?this._handle.style.left=f+"px":this._handle.style.top=f+"px"},_getNearestStepValue:function(n){var t,i;return this._steps==0?n:(t=this._maximum-this._minimum,t==0)?n:(i=t/(this._steps-1),Math.round(n/i)*i)},_onHandleReleased:function(){this._raiseChangeOnlyOnMouseUp&&this._fireTextBoxChangeEvent();this._raiseEvent("slideEnd")},_onRailClicked:function(n){var u=this._getHandleBounds(),f=this._getRailBounds(),t=this._isHorizontal?n.offsetX:n.offsetY,i=u.width/2,r=f.width-i;t=t<i?i:t>r?r:t;this._calcValue(null,t,!0);this._fireTextBoxChangeEvent()},_fireTextBoxChangeEvent:function(){if(document.createEvent){var n=document.createEvent("HTMLEvents");n.initEvent("change",!0,!1);this.get_element().dispatchEvent(n)}else document.createEventObject&&this.get_element().fireEvent("onchange")},get_dragDataType:function(){return"HTML"},getDragData:function(){return this._handle},get_dragMode:function(){return Sys.Extended.UI.DragMode.Move},onDragStart:function(){this._resetDragHandle();this._raiseEvent("slideStart")},onDrag:function(){var n=this._getBoundsInternal(this._dragHandle),r=this._getHandleBounds(),t=this._getRailBounds(),i;i=this._isHorizontal?{x:n.x-t.x,y:0}:{y:n.x-t.x,x:0};$common.setLocation(this._handle,i);this._calcValue(null,null);this._steps>1&&this._setHandleOffset(this.get_Value(),!1)},onDragEnd:function(){this._onHandleReleased()},get_dropTargetElement:function(){return document.body},canDrop:function(n,t){return t=="HTML"},drop:Function.emptyMethod,onDragEnterTarget:Function.emptyMethod,onDragLeaveTarget:Function.emptyMethod,onDragInTarget:Function.emptyMethod,add_sliderInitialized:function(n){this.get_events().addHandler("sliderInitialized",n)},remove_sliderInitialized:function(n){this.get_events().removeHandler("sliderInitialized",n)},add_valueChanged:function(n){this.get_events().addHandler("valueChanged",n)},remove_valueChanged:function(n){this.get_events().removeHandler("valueChanged",n)},add_slideStart:function(n){this.get_events().addHandler("slideStart",n)},remove_slideStart:function(n){this.get_events().removeHandler("slideStart",n)},add_slideEnd:function(n){this.get_events().addHandler("slideEnd",n)},remove_slideEnd:function(n){this.get_events().removeHandler("slideEnd",n)},_raiseEvent:function(n,t){var i=this.get_events().getHandler(n);i&&(t||(t=Sys.EventArgs.Empty),i(this,t))},get_Value:function(){return this._value},set_Value:function(n){var i=this._value,t=n;if(this._isUpdatingInternal||(t=this._calcValue(n)),this.get_element().value=this._value=t.toFixed(this._decimals),this._ensureBinding(),!Number.isInstanceOfType(this._value))try{this._value=parseFloat(this._value)}catch(r){this._value=Number.NaN}this._tooltipText&&(this._handle.alt=this._handle.title=String.format(this._tooltipText,this._value));this._isInitializedInternal&&(this._setHandleOffset(t,this._enableHandleAnimation&&this._animationPending),this._isUpdatingInternal&&(this._raiseChangeOnlyOnMouseUp||this._fireTextBoxChangeEvent()),this._value!=i&&this._raiseEvent("valueChanged"))},get_RailCssClass:function(){return this._railCssClass},set_RailCssClass:function(n){this._railCssClass=n},get_HandleImageUrl:function(){return this._handleImageUrl},set_HandleImageUrl:function(n){this._handleImageUrl=n},get_HandleCssClass:function(){return this._handleCssClass},set_HandleCssClass:function(n){this._handleCssClass=n},get_Minimum:function(){return this._minimum},set_Minimum:function(n){this._minimum=n},get_Maximum:function(){return this._maximum},set_Maximum:function(n){this._maximum=n},get_Orientation:function(){return this._orientation},set_Orientation:function(n){this._orientation=n},get_Steps:function(){return this._steps},set_Steps:function(n){this._steps=Math.abs(n);this._steps=this._steps==1?2:this._steps},get_Decimals:function(){return this._decimals},set_Decimals:function(n){this._decimals=Math.abs(n)},get_EnableHandleAnimation:function(){return this._enableHandleAnimation},set_EnableHandleAnimation:function(n){this._enableHandleAnimation=n},get_HandleAnimationDuration:function(){return this._handleAnimationDuration},set_HandleAnimationDuration:function(n){this._handleAnimationDuration=n},get_BoundControlID:function(){return this._boundControlID},set_BoundControlID:function(n){this._boundControlID=n;this._boundControl=this._boundControlID?$get(this._boundControlID):null},get_Length:function(){return this._length},set_Length:function(n){this._length=n+"px"},get_SliderInitialized:function(){return this._isInitializedInternal},get_RaiseChangeOnlyOnMouseUp:function(){return this._raiseChangeOnlyOnMouseUp},set_RaiseChangeOnlyOnMouseUp:function(n){this._raiseChangeOnlyOnMouseUp=n},get_TooltipText:function(){return this._tooltipText},set_TooltipText:function(n){this._tooltipText=n},get_enableKeyboard:function(){return this._enableKeyboard},set_enableKeyboard:function(n){n!==this._enableKeyboard&&(this._enableKeyboard=n,this.raisePropertyChanged("enableKeyboard"))},getClientState:function(){var n=Sys.Extended.UI.SliderBehavior.callBaseMethod(this,"get_ClientState");return n==""&&(n=null),n},setClientState:function(n){return Sys.Extended.UI.SliderBehavior.callBaseMethod(this,"set_ClientState",[n])}};Sys.Extended.UI.SliderBehavior.DropPending=null;Sys.Extended.UI.SliderBehavior.registerClass("Sys.Extended.UI.SliderBehavior",Sys.Extended.UI.BehaviorBase,Sys.Extended.UI.IDragSource,Sys.Extended.UI.IDropTarget);