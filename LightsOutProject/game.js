var canvas, ctx;
var data;
var initBoard;
var board;
var lit;
var initLit;
var numMoves;
var difficulty;
var numRows = 5;
var numCols = 5;
window.onload = function main() {
    //canvas = document.createElement("canvas");
    //canvas.id = "board";
    canvas = document.getElementById("board");
    canvas.width = canvas.height = numRows * 120 + 20;

    ctx = canvas.getContext("2d");

    //document.body.appendChild(canvas);

    var moves = document.getElementById("moves");

    canvas.addEventListener("mousedown", mouseDown);

    init();
    tick();
}

function solve() {
    var send2 = "{\'board\':\'" + JSON.stringify(board) + "\' }";

    $.ajax({
        url: 'BoardHandler.asmx/BoardHandle',
        type: 'POST',
        data: send2,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            document.getElementById("moves").textContent = parse2DArray(result.d);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status);
            console.log(xhr.responseText);
            console.log(thrownError);
        }
    });
}

function reset() {
    board = JSON.parse(JSON.stringify(initBoard))
    lit = JSON.parse(JSON.stringify(initLit))
    numMoves = 0;
    $('#solvedAlert').hide();
}

function setDiff(diff, btn) {
    $('#' + btn + 'Btn').addClass('active').siblings().removeClass('active');
    difficulty = diff;
}

function newGame() {
    if (difficulty == 1) {
        initBoard = [[1, 0, 0, 1, 0], [1, 1, 1, 0, 0], [0, 1, 0, 1, 0], [0, 1, 1, 1, 0], [0, 1, 1, 1, 0]];
        board = [[1, 0, 0, 1, 0], [1, 1, 1, 0, 0], [0, 1, 0, 1, 0], [0, 1, 1, 1, 0], [0, 1, 1, 1, 0]];
        initLit = lit = 10;
    }
    else if (difficulty == 2) {
        initBoard = [[1, 0, 0, 1, 0], [1, 1, 1, 0, 0], [0, 1, 0, 1, 0], [0, 1, 1, 1, 0], [0, 1, 1, 1, 0]];
        board = [[1, 0, 0, 1, 0], [1, 1, 1, 0, 0], [0, 1, 0, 1, 0], [0, 1, 1, 1, 0], [0, 1, 1, 1, 0]];
        initLit = lit = 4;
    }
    else {
        initBoard = [[1, 0, 0, 1, 0], [1, 1, 1, 0, 0], [0, 1, 0, 1, 0], [0, 1, 1, 1, 0], [0, 1, 1, 1, 0]];
        board = [[1, 0, 0, 1, 0], [1, 1, 1, 0, 0], [0, 1, 0, 1, 0], [0, 1, 1, 1, 0], [0, 1, 1, 1, 0]];
        initLit = lit = 12;
    }
}

function parse2DArray(arr) {
    var s = "Moves: \n";
    console.log(arr);
    for (var i = 0; i < arr.length; i++) {
        s = s + "[ "

        for (var j = 0; j < arr[i].length; j++) {
            var temp = arr[i][j].toString();
            s = s + temp + " ";
        }

        s = s + "],";
    }

    return s;
}

function init() {
    if (data == null) {
        difficulty = 1;
        $('#easyBtn').addClass('active')
        newGame();

        data = [];

        var count = 0;

        for (var i = 0; i < numRows; i++) {
            temp = [];

            for (var j = 0; j < numCols; j++) {
                var x = (j % numCols) * 120 + 20;
                var y = Math.floor(count / numCols) * 120 + 20;
                temp.push(new Tile(x, y));
                count++;
            }

            data.push(temp);
        }
    }
}

function tick() {
    window.requestAnimationFrame(tick);

    update();
    render();
}

function update() {
    for (var i = 0; i < numRows; i++) {
        for (var j = 0; j < numCols; j++) {
            (data[i][j]).set(board[i][j]);
        }
    }
}

function render() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    for (var i = data.length; i--;) {
        for (var j = data.length; j--;) {
            ctx.globalAlpha = data[i][j].getAlpha();
            data[i][j].draw(ctx);
        }

    }
}

function mouseDown(event) {
    var totalOffsetX = 0;
    var totalOffsetY = 0;
    var canvasX = 0;
    var canvasY = 0;
    var currentElement = this;

    do {
        totalOffsetX += currentElement.offsetLeft - currentElement.scrollLeft;
        totalOffsetY += currentElement.offsetTop - currentElement.scrollTop;
    }
    while (currentElement = currentElement.offsetParent)

    canvasX = event.pageX - totalOffsetX;
    canvasY = event.pageY - totalOffsetY;

    if (canvasX % 120 >= 20 && canvasY % 120 >= 20) {
        var y = Math.floor(canvasX / 120);
        var x = Math.floor(canvasY / 120);

        action(x, y);
        checkState();
    }
}

function checkState() {
    if (lit == board.length * board.length) {
        $('#solvedAlert').show();
        //alert("YOU WIN!");
    }
}

function action(x, y) {
    var above = x - 1;
    var bot = x + 1;
    var left = y - 1;
    var right = y + 1;

    flipValue(x, y);

    if (left > -1) {
        flipValue(x, left);
    }

    if (right < board.length) {
        flipValue(x, right);
    }

    if (above > -1) {
        flipValue(above, y);
    }

    if (bot < board.length) {
        flipValue(bot, y);
    }
}

function flipValue(x, y) {
    board[x][y] = 1 - board[x][y];
    board[x][y] == 1 ? lit++ : lit--;
    numMoves++;
}

function Tile(x, y) {
    var x = x, y = y;

    var tile = Tile.OFF;

    if (tile == null) {
        var _c = document.createElement("canvas");
        _c.width = _c.height = 100;
        var _ctx = _c.getContext("2d");

        _ctx.fillStyle = "#FFF";

        // On
        _ctx.fillRect(0, 0, 100, 100);
        Tile.ON = new Image();
        Tile.ON.src = _c.toDataURL();

        // Off
        _ctx.fillRect(0, 0, 100, 100);
        Tile.OFF = new Image();
        Tile.OFF.src = _c.toDataURL();

        tile = Tile.OFF;
    }

    this.isOFF = function () {
        return tile == Tile.OFF;
    }

    this.set = function (val) {
        if (val == 1) {
            tile = Tile.ON;
        }
        else {
            tile = Tile.OFF;
        }
    }

    this.getAlpha = function () {
        return tile == Tile.ON ? 1 : 0.2
    }

    this.draw = function (ctx) {
        ctx.drawImage(tile, x, y);
    }
}