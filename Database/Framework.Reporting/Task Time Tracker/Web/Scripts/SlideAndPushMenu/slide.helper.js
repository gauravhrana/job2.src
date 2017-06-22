function slide_helper(menuLeft, menuRight, menuRight2, menuTop, menuBottom)
{
       
    var showLeft = document.getElementById('showLeft');
    var showRight = document.getElementById('showRight');
    var showRight2 = document.getElementById('showRight2');
    //showTop = document.getElementById('showTop');
    var showBottom = document.getElementById('showBottom');
    //showLeftPush = document.getElementById('showLeftPush');
    //showRightPush = document.getElementById('showRightPush');
    var showTopPush = document.getElementById('showTopPush');
    var closeTop = document.getElementById('closeTop');
    var showBottomPush = document.getElementById('showBottomPush');
    var closeBottom = document.getElementById('closeBottom');
    var hprCustom1 = document.getElementById('hprCustomise');
    var body = document.body;
    

    hprCustom1.onclick = function() {
        classie.toggle(this, 'active');
        classie.toggle(body, 'cbp-spmenu-push-totop');
        classie.toggle(menuBottom, 'cbp-spmenu-open');
        disableOther('showBottomPush');
    };

    showLeft.onclick = function() {
        classie.toggle(this, 'active');
        classie.toggle(menuLeft, 'cbp-spmenu-open');
        disableOther('showLeft');
    };

    showRight.onclick = function() {
        classie.toggle(this, 'active');
        classie.toggle(menuRight, 'cbp-spmenu-open');
        disableOther('showRight');
    };

    showRight2.onclick = function() {
        classie.toggle(this, 'active');
        classie.toggle(menuRight2, 'cbp-spmenu-open');
        disableOther('showRight2');
    };
    //        showTop.onclick = function () {
    //            classie.toggle(this, 'active');
    //            classie.toggle(menuTop, 'cbp-spmenu-open');
    //            disableOther('showTop');
    //        };
    showBottom.onclick = function() {
        classie.toggle(this, 'active');
        classie.toggle(menuBottom, 'cbp-spmenu-open');
        disableOther('showBottom');
    };
    //        showLeftPush.onclick = function () {
    //            classie.toggle(this, 'active');
    //            classie.toggle(body, 'cbp-spmenu-push-toright');
    //            classie.toggle(menuLeft, 'cbp-spmenu-open');
    //            disableOther('showLeftPush');
    //        };
    //        showRightPush.onclick = function () {
    //            classie.toggle(this, 'active');
    //            classie.toggle(body, 'cbp-spmenu-push-toleft');
    //            classie.toggle(menuRight, 'cbp-spmenu-open');
    //            disableOther('showRightPush');
    //        };

    showTopPush.onclick = function() {
        classie.toggle(this, 'active');
        classie.toggle(body, 'cbp-spmenu-push-tobottom');
        classie.toggle(menuTop, 'cbp-spmenu-open');
        disableOther('showTopPush');
    };

    closeTop.onclick = function() {
        classie.toggle(this, 'active');
        classie.toggle(body, 'cbp-spmenu-push-tobottom');
        classie.toggle(menuTop, 'cbp-spmenu-open');
        disableOther('showTopPush');
    };

    showBottomPush.onclick = function() {
        classie.toggle(this, 'active');
        classie.toggle(body, 'cbp-spmenu-push-totop');
        classie.toggle(menuBottom, 'cbp-spmenu-open');
        disableOther('showBottomPush');
    };

    closeBottom.onclick = function() {
        classie.toggle(this, 'active');
        classie.toggle(body, 'cbp-spmenu-push-totop');
        classie.toggle(menuBottom, 'cbp-spmenu-open');
        disableOther('showBottomPush');
    };

    function hidemenu(menu) {
        if (menu == 'cbp_spmenu_s4')
            classie.toggle(body, 'cbp-spmenu-push-tobottom');
        if (menu == 'cbp_spmenu_s3')
            classie.toggle(body, 'cbp-spmenu-push-totop');
    }

    function disableOther(button) {

        if (button !== 'showLeft') {
            classie.toggle(showLeft, 'disabled');
        }

        if (button !== 'showRight') {
            classie.toggle(showRight, 'disabled');
        }
        
        if (button !== 'showRight2') {
            classie.toggle(showRight2, 'disabled');
        }
        //            if (button !== 'showTop') {
        //                classie.toggle(showTop, 'disabled');
        //            }
        if (button !== 'showBottom') {
            classie.toggle(showBottom, 'disabled');
        }
        //            if (button !== 'showLeftPush') {
        //                classie.toggle(showLeftPush, 'disabled');
        //            }
        //            if (button !== 'showRightPush') {
        //                classie.toggle(showRightPush, 'disabled');
        //            }
        if (button !== 'showTopPush') {
            classie.toggle(showTopPush, 'disabled');
        }
        
        if (button !== 'showBottomPush') {
            classie.toggle(showBottomPush, 'disabled');
        }
    }
}