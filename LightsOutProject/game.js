var canvas, ctx;
var data;
var initBoard, board;
var lit, initLit;
var numMoves;
var numRows = numCols = 4;
var canvasSize = 120;
var tileOffset = 20;

window.onload = function main() {
    canvas = document.getElementById("board");
    canvas.width = canvas.height = numRows * canvasSize + tileOffset;
    ctx = canvas.getContext("2d");

    var moves = document.getElementById("moves");

    canvas.addEventListener("mousedown", mouseDown);

    init();
    tick();
}

function init() {
    if (data == null) {
        newGame();

        data = [];

        var count = 0;

        for (var i = 0; i < numRows; i++) {
            temp = [];

            for (var j = 0; j < numCols; j++) {
                var x = (j % numCols) * canvasSize + tileOffset;
                var y = Math.floor(count / numCols) * canvasSize + tileOffset;
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

    document.getElementById("numMoves").textContent = numMoves.toString();
}

function render() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    for (var i = 0; i< data.length; i++) {
        for (var j = 0; j < data.length; j++) {
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

    if (canvasX % canvasSize >= tileOffset && canvasY % canvasSize >= tileOffset) {
        var y = Math.floor(canvasX / canvasSize);
        var x = Math.floor(canvasY / canvasSize);

        action(x, y);
        checkState();
    }
}

function checkState() {
    if (lit == board.length * board.length) {
        $('#solved').show();
    }
}

function action(x, y) {
    var above = x - 1;
    var bot = x + 1;
    var left = y - 1;
    var right = y + 1;

    numMoves++;
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
}

function Tile(x, y) {
    var x = x, y = y;

    var tile = Tile.OFF;

    if (tile == null) {
        var _c = document.createElement("canvas");
        _c.width = _c.height = 100;
        var _ctx = _c.getContext("2d");

        _ctx.fillStyle = "#FFF";

        // Tile On
        _ctx.fillRect(0, 0, 100, 100);
        Tile.ON = new Image();
        Tile.ON.src = _c.toDataURL();

        // Tile Off
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

function newGame() {
    numMoves = 0;
    $('#solved').hide();
    random = Math.floor((Math.random() * numPuzzles) + 0);

    initBoard = JSON.parse(JSON.stringify(puzzles[random]));
    board = JSON.parse(JSON.stringify(puzzles[random]));
    initLit = lit = getNumLit(board);
}

function reset() {
    board = JSON.parse(JSON.stringify(initBoard));
    lit = JSON.parse(JSON.stringify(initLit));

    numMoves = 0;
    $('#solved').hide();
}

function solve() {
    document.getElementById("moves").textContent = "Solving...";
    var sendBoard = "{\'board\':\'" + JSON.stringify(board) + "\' }";

    $.ajax({
        url: 'BoardHandler.asmx/BoardHandle',
        type: 'POST',
        data: sendBoard,
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

function parse2DArray(arr) {
    if (arr == -1) {
        var s = "Unsolvable!";
    }
    else {
        var s = "Moves: \n";

        for (var i = 0; i < arr.length; i++) {
            s = s + "[ "

            for (var j = 0; j < arr[i].length; j++) {
                var temp = arr[i][j].toString();
                s = s + temp + " ";
            }

            s = s + "],";
        }
    }

    return s;
}

function getNumLit(board) {
    var litCount = 0;

    for(var i = 0; i < board.length; i++)
    {
        for(var j = 0; j < board.length;j++)
        {
            if(board[i][j] == 1)
            {
                litCount++;
            }
        }           
    }

    return litCount;
}