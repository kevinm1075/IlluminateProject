var canvas, ctx;
var data;
var board;
var lit;

window.onload = function main() {
    canvas = document.createElement("canvas")
    canvas.width = canvas.height = 3 * 120 + 20;
    canvas.id = "board";

    ctx = canvas.getContext("2d");

    document.body.appendChild(canvas);

    var moves = document.createElement("P");
    moves.id = "moves"
    document.body.appendChild(moves);

    canvas.addEventListener("mousedown", mouseDown);

    init();
    tick();
}

function solve()
{
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

function parse2DArray(arr)
{
    var s = "Moves: \n";
    console.log(arr);
    for(var i = 0; i < arr.length; i++)
    {
        s = s + "[ "

        for(var j = 0; j < arr[i].length; j++)
        {
            var temp = arr[i][j].toString();
            s = s + temp + " ";
        }

        s = s + "],";
    }

    return s;
}

function init() {
    if (data == null) {
        board = [[0, 0, 0], [0, 0, 0], [0, 0, 0]];
        lit = 0;
        data = [];

        var count = 0;

        for (var i = 0; i < 3; i++) {
            temp = [];

            for (var j = 0; j < 3; j++) {
                var x = (j % 3) * 120 + 20;
                var y = Math.floor(count / 3) * 120 + 20;
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
    for (var i = 0; i < 3; i++) {
        for (var j = 0; j < 3; j++) {
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

function mouseDown(evt) {
    var el = evt.target;

    var px = evt.clientX - el.offsetLeft;
    var py = evt.clientY - el.offsetTop;

    if (px % 120 >= 20 && py % 120 >= 20) {
        var y = Math.floor(px / 120);
        var x = Math.floor(py / 120);

        action(x, y);
        checkState();
    }
}

function checkState() {
    if (lit == board.length * board.length) {
        alert("YOU WIN!");
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
}

function Tile(x, y) {
    var x = x, y = y;

    var tile = Tile.OFF;

    if (tile == null) {
        var _c = document.createElement("canvas");
        _c.width = _c.height = 100;
        var _ctx = _c.getContext("2d");

        _ctx.fillStyle = "#962727";

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

    this.over = function () {
        ctx.rotate(20 * Math.PI / 180);
    }

    this.getAlpha = function () {
        return tile == Tile.ON ? 1 : 0.2
    }

    this.draw = function (ctx) {
        ctx.drawImage(tile, x, y);
    }
}