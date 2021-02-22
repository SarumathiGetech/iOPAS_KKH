function hidestatus()
{
    window.status = ''
    
    return true
}
if (document.layers)
document.captureEvents(Event.MOUSEOVER | Event.MOUSEOUT)
document.onmouseover=hidestatus
document.onmouseout=hidestatus

//function scrollit_r2l(seed) {
//    /* ---- Variables ---- */
//    var m0 = " Welcome ";
//    var m1 = " To";
//    //var m2 = " - , Hello!!! "; 
//    var m2 = " Opas ";
//    var m3 = " -  www.getecha.com.sg!!! ";
//    var msg = m0 + m1 + m2;
//    var out = " "; // Autocomplete Timeout in ms 
//    var c = 1;
//    if (document.layers)
//        document.captureEvents(Event.MOUSEOVER | Event.MOUSEOUT)
//    document.onmouseover = hidestatus
//    document.onmouseout = hidestatus

//    if (seed > 99) {
//        seed--;
//        var cmd = "scrollit_r2l(" + seed + ")";
//        timerTwo = window.setTimeout(cmd, 99);
//    }
//    else if (seed <= 99 && seed > 0) {
//        for (c = 0; c < seed; c++) {
//            out += " ";
//        }
//        out += msg;
//        seed--;
//        var cmd1 = "scrollit_r2l(" + seed + ")";
//        window.status = out;
//        timerTwo = window.setTimeout(cmd, 99);
//    }
//    else if (seed <= 0) {
//        if (-seed < msg.length) {
//            out += msg.substring(-seed, msg.length);
//            seed--;
//            var cmd2 = "scrollit_r2l(" + seed + ")";
//            window.status = out;
//            timerTwo = window.setTimeout(cmd, 99);
//        }
//        else {
//            window.status = " ";
//            if (document.layers)
//                document.captureEvents(Event.MOUSEOVER | Event.MOUSEOUT)
//            document.onmouseover = hidestatus
//            document.onmouseout = hidestatus
//            timerTwo = window.setTimeout("scrollit_r2l(100)", 75);
//        }
//    }
//}
//timeONE = window.setTimeout('scrollit_r2l(100)', 500); 
{
    javascript: window.history.forward(1);
   // var message = "Right click option disabled in this screen";
//    function click(e) {
//        if (document.all) {
//            if (event.button == 2) {
//                alert(message);
//                return false;
//            }
//        }
//        if (document.layers) {
//            if (e.which == 3) {
//                alert(message);
//                return false;
//            }
//        }
//    }
//    if (document.layers) {
//        document.captureEvents(Event.MOUSEDOWN);
//    }
   // document.onmousedown = click;
}


    