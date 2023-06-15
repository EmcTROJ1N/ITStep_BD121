class Point
{
    x;
    y;
    constructor(x, y)
    {
        this.x = x;
        this.y = y;
    }
}

class Line
{
    From;
    To;

    constructor(from, to)
    {
        this.From = from;
        this.To = to;
    }

    Draw(context, zeroPoint)
    {
        context.beginPath();
        context.moveTo(this.From.x - zeroPoint.x, this.From.y - zeroPoint.y);
        context.lineTo(this.To.x - zeroPoint.x, this.To.y - zeroPoint.y);
        context.stroke();
        context.closePath();
    }

    InOverlay(canvas, zeroPoint)
    {
        let flag = false;
        [this.From, this.To].forEach((point) =>
        {
            if (point.x > zeroPoint.x && point.y > zeroPoint.y &&
                point.x < canvas.width + zeroPoint.x && point.y < canvas.height + zeroPoint.y)
            {
                flag = true;
                return;
            }
        });
        return flag;
    }

}

class Vertex
{
    Cords;
    #_PhotoUrl;
    #_Caption;
    #_SideLength = 500;
    Dragging = false;
    DeltaCords = new Point(0, 0);
    StartPos = new Point(0, 0);

    set PhotoUrl(url)
    {
        this.#_PhotoUrl = url;
    }
    get PhotoUrl() { return this.#_PhotoUrl; }

    set Caption(caption)
    {
        this.#_Caption = caption;
    }
    get Caption() { return this.#_Caption; }

    set SideLength(len)
    {
        this.#_SideLength = len;
    }
    get SideLength() { return this.#_SideLength; }
    
    Id;
    Links = new Array();

    constructor(cords, url, caption)
    {
        this.Cords = cords;
        this.Caption = caption;
        this.PhotoUrl = url;
        // this.DOMContainer.addEventListener("mousedown", this.onMouseDown);
        // this.DOMContainer.addEventListener("mousemove", this.onMouseMove);
        // this.DOMContainer.addEventListener("mouseup", this.onMouseUp);
    }

    onMouseDown(e)
    {
        this.DeltaCords.x = e.clientX - this.DOMContainer.offsetLeft;
        this.DeltaCords.y = e.clientY - this.DOMContainer.offsetTop;
        this.StartPos = new Point(e.clientX, e.clientY);
        this.Dragging = true;
        e.stopPropagation();
    }

    onMouseMove(e)
    {
        if (this.Dragging)
        {
            this.Cords.x = e.clientX - this.DeltaCords.x;
            this.Cords.y = e.clientY - this.DeltaCords.y;
            this.DOMContainer.style.left = this.x - zeroPoint.x + "px";
            this.DOMContainer.style.top = this.y  - zeroPoint.y + "px";
            this.StartPos = new Point(e.clientX, e.clientY);
        }
        e.stopPropagation();
    }

    onMouseUp(e)
    {
        this.Dragging = false;
        e.stopPropagation();
    }

    InOverlay(canvas, zeroPoint)
    {
        let flag = false;
        [
            this.Cords,
            new Point(this.Cords.x + this.SideLength, this.Cords.y),
            new Point(this.Cords.x, this.Cords.y + this.SideLength),
            new Point(this.Cords.x + this.SideLength, this.Cords.y + this.SideLength),
        ].forEach((point) =>
        {
            if (point.x > zeroPoint.x && point.y > zeroPoint.y &&
                point.x < canvas.width + zeroPoint.x && point.y < canvas.height + zeroPoint.y)
            {
                flag = true;
                return;
            }
        });
        return flag;
    }

    Draw(context, zeroPoint)
    {
        context.beginPath();

        context.moveTo(this.Cords.x - zeroPoint.x, this.Cords.y - zeroPoint.y);
        let img = new Image();
        img.src = this.PhotoUrl;
        context.drawImage(img, this.Cords.x - zeroPoint.x, this.Cords.y - zeroPoint.y,
                            Math.floor(img.width % 500), Math.floor(img.height % 500));
        
        context.font = '18px Arial';
        context.fillText(this.Caption, this.Cords.x - zeroPoint.x,
            this.Cords.y - zeroPoint.y + Math.floor(img.height % 500) + 22);
        

        context.closePath();
    }
}

const CursorStatus =
{
    MoveBoard: "move",
    Draw: "draw",
    Eraser: "eraser"
}

class DrawingBoard
{
    // Элементы dom дерева
    Canvas;
    ScalingLabel;
    #_Context;
    Status = CursorStatus.MoveBoard;
    Spacing;
    Tapped = false;

    get Context()
    {
        if (this.#_Context == null)
            this.#_Context = this.Canvas.getContext("2d");
        return this.#_Context;
    }

    
    #_Scale = 100;
    set Scale(scale)
    {
        this.#_Scale = scale;
        this.ScalingLabel.innerText = `${this.Scale}%`;
        this.drawOverlay();
    }
    get Scale() { return this.#_Scale; }
    
    #_Figures = new Array();
    set Figures(figures)
    {
        this.#_Figures = figures;
        this.drawOverlay();
    }
    get Figures() { return this.#_Figures; }

    // Координаты
    ZeroPoint = new Point(0, 0);
    StartPoint = new Point(0, 0);
    DeltaPoint = new Point(0, 0);
    BeginGrid;

    constructor(canvas, scalingLabel, spacing = 45)
    {
        this.Canvas = canvas;
        this.ScalingLabel = scalingLabel;
        this.Spacing = spacing;

        this.Canvas.addEventListener("mousedown", (e) => this.boardMouseDown(e));
        this.Canvas.addEventListener("mousemove", (e) => this.boardMouseMove(e));
        this.Canvas.addEventListener("mouseup", (e) => this.boardMouseUp(e));
        this.Canvas.addEventListener("wheel", (e) => this.boardScaling(e));

        this.drawOverlay();
    }

    add(figure)
    {
        this.Figures.push(figure);
        this.drawOverlay();
    }

    remove(idx)
    {
        this.Figures.splice(idx, 1);
        this.drawOverlay();
    }

    boardMouseDown(e)
    {
        this.Tapped = true;
        this.StartPoint = new Point(e.clientX - this.Canvas.offsetLeft, e.clientY - this.Canvas.offsetTop);
    }

    boardMouseMove(e)
    {
        if (this.Tapped != true)
            return;
        
        switch (this.Status)
        {
            case (CursorStatus.MoveBoard):
            {
                this.Canvas.style.cursor = "move";
                this.DeltaPoint = new Point(this.StartPoint.x + this.Canvas.offsetLeft - e.clientX, this.StartPoint.y + this.Canvas.offsetTop - e.clientY);
                this.ZeroPoint = new Point(this.ZeroPoint.x + this.DeltaPoint.x, this.ZeroPoint.y + this.DeltaPoint.y);
                break;
            }
            case (CursorStatus.Draw):
            {
                let z = this.Scale / 100;
                console.log(z);
                this.add(new Line(new Point(this.ZeroPoint.x + this.StartPoint.x, this.ZeroPoint.y + this.StartPoint.y),
                                  new Point(this.ZeroPoint.x + e.clientX - this.Canvas.offsetLeft, this.ZeroPoint.y + e.clientY - this.Canvas.offsetTop)));
                break;
            }
        }  

        this.StartPoint = new Point(e.clientX - this.Canvas.offsetLeft, e.clientY - this.Canvas.offsetTop);
        this.drawOverlay();
    }

    boardMouseUp(e)
    {
        this.Canvas.style.cursor = "default";
        this.Tapped = false;
    }

    boardScaling(e)
    {
        let direction = e.deltaY > 0 ? -.3 : .3;
        let scale = 1 + direction;
        this.Context.scale(this.Scale, this.Scale);
        this.Canvas.width += this.Canvas.width / 10 * -direction;
        this.Canvas.height += this.Canvas.height / 10 * -direction;
        
        this.Scale += direction * 100;
    }

    drawOverlay()
    {
        let drawLayer = () =>
        {
            if (this.BeginGrid == null)
                this.BeginGrid = new Point(this.Spacing, this.Spacing);
            else if (this.Status == CursorStatus.MoveBoard)
            {
                this.BeginGrid.x = this.BeginGrid.x - this.DeltaPoint.x;
                this.BeginGrid.y = this.BeginGrid.y - this.DeltaPoint.y;
                
                if (this.BeginGrid.x < 0) this.BeginGrid.x = this.Spacing - Math.abs(this.BeginGrid.x);
                if (this.BeginGrid.y < 0) this.BeginGrid.y = this.Spacing - Math.abs(this.BeginGrid.y);
                if (this.BeginGrid.x > this.Spacing) this.BeginGrid.x = this.BeginGrid.x % this.Spacing;
                if (this.BeginGrid.y > this.Spacing) this.BeginGrid.y = this.BeginGrid.y % this.Spacing;
            }

            this.Context.strokeStyle = "gray";
            this.Context.lineWidth = .03;
            this.Context.beginPath();
            for (let w = this.BeginGrid.x; w < this.Canvas.width; w += this.Spacing)
            {
                this.Context.moveTo(w, 0);
                this.Context.lineTo(w, this.Canvas.height);
                this.Context.stroke();
            }
            for (let h = this.BeginGrid.y; h < this.Canvas.height; h += this.Spacing)
            {
                this.Context.moveTo(0, h);
                this.Context.lineTo(this.Canvas.width, h);
                this.Context.stroke();
            }
            this.Context.closePath();
        }

        this.Context.clearRect(0, 0, this.Canvas.width, this.Canvas.height);
        drawLayer(this.Spacing);

        this.Context.lineWidth = 2;
        this.Context.strokeStyle = "black";
        this.Figures.forEach((figure) =>
        {
            if (figure.InOverlay(this.Canvas, this.ZeroPoint))
                figure.Draw(this.Context, this.ZeroPoint);
        });
    }

    clearOverlay = () => this.Figures = new Array();
}

let Board;

document.addEventListener("DOMContentLoaded", () =>
{
    let scalingLabel = document.getElementById("scaling");
    let canvas = document.getElementById("board");
    
    let rect = canvas.getBoundingClientRect();
    canvas.height = rect.height;
    canvas.width = rect.width;

    Board = new DrawingBoard(canvas, scalingLabel);

    // Board.add(new Line(new Point(100, 200), new Point(200, 500)));
    // Board.add(new Vertex(new Point(100, 100),
    // "https://sun9-64.userapi.com/impg/HocP2Qk9Zls0LWakQqT-RbDm6MTbdZkEh5AsXQ/zjTT27po88E.jpg?size=1620x2160&quality=95&sign=17985558459b7dc33b57e0fc6b069488&type=album",
    // "Герман Покровский"));
});