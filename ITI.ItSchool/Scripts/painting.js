'use strict';

$(document).ready(function() {
    console.log("Document ready.");

    // Creation of the canvas context
    var context = document.getElementById("canvas").getContext('2d');
    var clickX = new Array();
    var clickY = new Array();
    var clickDrag = new Array();
    var paint = false;

    // Colors
    var clickColor = new Array();
    var currentColor = "#000000";

    // Red shadings
    var red = "#FFA07A";
    var red2 = "#DC143C";
    var red3 = "#FF0000";
    var red4 = "#B22222";
    var red5 = "#8B0000";

    // Blue shadings
    var blue = "#1E90FF";
    var blue2 = "#6495ED";
    var blue3 = "#7B68EE";
    var blue4 = "#4169E1";
    var blue5 = "#0000FF";

    // Yellow shadings
    var yellow = "#FFD700";
    var yellow2 = "#FFFF00";
    var yellow3 = "#FFFFE0";
    var yellow4 = "#FFFACD";
    var yellow5 = "#FAFAD2";

    // Fuschia shadings
    var fuschia = "#D8BFD8";
    var fuschia2 = "#DDA0DD";
    var fuschia3 = "#EE82EE";
    var fuschia4 = "#DA70D6";
    var fuschia5 = "#FF00FF";

    // Pink shadings 
    var pink = "#FFC0CB";
    var pink2 = "#FFB6C1";
    var pink3 = "#FF69B4";
    var pink4 = "#FF1493";
    var pink5 = "#C71585";

    // Orange shadings
    var orange = "#FF7F50";
    var orange2 = "#FF6347";
    var orange3 = "#FF4500";
    var orange4 = "#FF8C00";
    var orange5 = "#FFA500";

    // Cyan shadings
    var cyan = "#B0E0E6";
    var cyan2 = "#ADD8E6";
    var cyan3 = "#87CEEB";
    var cyan4 = "#87CEFA";
    var cyan5 = "#00BFFF";

    // Grey shadings
    var grey = "#D3D3D3";
    var grey2 = "#C0C0C0";
    var grey3 = "#A9A9A9";
    var grey4 = "#808080";
    var grey5 = "#696969";

    // Saves the mouse position and lines to, depends on the dragging value (bool).
    function addClick(x, y, dragging) {
        clickX.push(x);
        clickY.push(y);
        clickDrag.push(dragging);
        clickColor.push(currentColor);
    }

    // Redraw all the aspects.
    function redraw() {
        context.clearRect(0, 0, context.canvas.width, context.canvas.height);
        context.lineJoin = "round";
        context.lineWidth = 5;

        for(var i = 0; i < clickX.length; i++) {
            context.beginPath();
            if(clickDrag[i] && i) {
                context.moveTo(clickX[i - 1], clickY[i - 1]);
            } else {
                context.moveTo(clickX[i] - 1, clickY[i])
            }
            context.lineTo(clickX[i], clickY[i]);
            context.strokeStyle = clickColor[i];
            context.closePath();
            context.stroke();
        }
    }

    // Events listening management

    // For colors changes
    $('#red').mousedown(function(e) {
        currentColor = red;
    });

    $('#red2').mousedown(function(e) {
        currentColor = red2;
    });

    $('#red3').mousedown(function(e) {
        currentColor = red3;
    });

    $('#red4').mousedown(function(e) {
        currentColor = red4;
    });

    $('#red5').mousedown(function(e) {
        currentColor = red5;
    });

    $('#blue').mousedown(function (e) {
        currentColor = blue;
    });

    $('#blue2').mousedown(function (e) {
        currentColor = blue2;
    });

    $('#blue3').mousedown(function (e) {
        currentColor = blue3;
    });

    $('#blue4').mousedown(function (e) {
        currentColor = blue4;
    });

    $('#blue5').mousedown(function (e) {
        currentColor = blue5;
    });

    $('#yellow').mousedown(function (e) {
        currentColor = yellow5;
    });

    $('#yellow2').mousedown(function (e) {
        currentColor = yellow4;
    });

    $('#yellow3').mousedown(function (e) {
        currentColor = yellow3;
    });

    $('#yellow4').mousedown(function (e) {
        currentColor = yellow2;
    });

    $('#yellow5').mousedown(function (e) {
        currentColor = yellow;
    });

    $('#fuschia').mousedown(function (e) {
        currentColor = fuschia;
    });

    $('#fuschia2').mousedown(function (e) {
        currentColor = fuschia2;
    });

    $('#fuschia3').mousedown(function (e) {
        currentColor = fuschia3;
    });

    $('#fuschia4').mousedown(function (e) {
        currentColor = fuschia4;
    });

    $('#fuschia5').mousedown(function (e) {
        currentColor = fuschia5;
    });

    $('#pink').mousedown(function (e) {
        currentColor = pink;
    });

    $('#pink2').mousedown(function (e) {
        currentColor = pink2;
    });

    $('#pink2').mousedown(function (e) {
        currentColor = pink2;
    });

    $('#pink3').mousedown(function (e) {
        currentColor = pink3;
    });

    $('#pink4').mousedown(function (e) {
        currentColor = pink4;
    });

    $('#pink5').mousedown(function (e) {
        currentColor = pink5;
    });

    $('#orange').mousedown(function (e) {
        currentColor = orange;
    });

    $('#orange2').mousedown(function (e) {
        currentColor = orange2;
    });

    $('#orange3').mousedown(function (e) {
        currentColor = orange3;
    });

    $('#orange4').mousedown(function (e) {
        currentColor = orange4;
    });

    $('#orange5').mousedown(function (e) {
        currentColor = orange5;
    });

    $('#cyan').mousedown(function (e) {
        currentColor = cyan;
    });

    $('#cyan2').mousedown(function (e) {
        currentColor = cyan2;
    });

    $('#cyan3').mousedown(function (e) {
        currentColor = cyan3;
    });

    $('#cyan4').mousedown(function (e) {
        currentColor = cyan4;
    });

    $('#cyan5').mousedown(function (e) {
        currentColor = cyan5;
    });

    $('#grey').mousedown(function (e) {
        currentColor = grey;
    });

    $('#grey2').mousedown(function (e) {
        currentColor = grey2;
    });

    $('#grey3').mousedown(function (e) {
        currentColor = grey3;
    });

    $('#grey4').mousedown(function (e) {
        currentColor = grey4;
    });

    $('#grey5').mousedown(function (e) {
        currentColor = grey5;
    });

    // Canvas events
    $('#canvas').mousemove(function (e) {
        document.getElementById('canvas').style.cursor = 'pointer';
        if(paint) {
            addClick(e.pageX - this.offsetLeft, e.pageY - this.offsetTop, true);
            redraw();
        }
    });

    // Mouse position : refer to the web site http://codetheory.in/creating-a-paint-application-with-html5-canvas/ 
    // for more informations about offsetLeft, offsetTop, pageX/Y notions.
    $('#canvas').mousedown(function (e) {
        var mouseX = e.pageX - this.offsetLeft;
        var mouseY = e.pageY - this.offsetTop;
        document.getElementById('canvas').style.cursor = 'pointer';
        paint = true;
        addClick(mouseX, mouseY, false);
        redraw();
    });

    $('#canvas').mouseup(function (e) {
        document.getElementById('canvas').style.cursor = 'default';
        paint = false;
    });

    $('#canvas').mouseleave(function(e) {
        paint = false;
    });

    // Colors events listening management
});