function SetupContextMenu() {

    $.contextMenu({
        selector: '.context-menu-one',
        trigger: 'hover',
        delay: 100,
        autoHide: true,
        callback: function (key, options) {
            var m = "clicked: " + key;
            window.console && console.log(m) || alert(m);
        },
        items: {
            "edit": { name: "Edit" },
            "cut": { name: "Cut" },
            "copy": { name: "Copy" },
            "paste": { name: "Paste" },
            "delete": { name: "Delete" },
            "sep1": "---------",
            "quit": { name: "Quit" }
        }
    });
}