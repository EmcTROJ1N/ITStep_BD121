const LINE_COLOR = "rgb(201,209,217)"

class Point
{
    x;
    y;
    constructor(x, y)
    {
        this.x = x;
        this.y = y;
    }

    sub(source)
    {
        if (typeof source == "Point")
            return new Point(this.x - source.x,
                             this.y - source.y)
        else if (typeof source == "int")
            return new Point(this.x - source,
                             this.y - source)
    }

    add(source)
    {
        if (typeof source == "Point")
            return new Point(this.x - source.x,
                             this.y - source.y)
        else if (typeof source == "int")
            return new Point(this.x - source,
                             this.y - source)
    }

}

class Line
{
    From;
    To;
    _RelationFrom;
    _RelationTo;

    set RelationFrom(value)
    {
        this._RelationFrom = value;
        this.From = this.From.add(this.Parent.ZeroPoint);
    }
    get RelationFrom() { return this.sub(this.Parent.ZeroPoint); }

    set RelationTo(value)
    {
        this._RelationTo = value;
        this.To = this.To.add(this.Parent.ZeroPoint);
    }
    get RelationTo() { return this.sub(this.Parent.ZeroPoint); }

    SVGLine;
    Parent;

    constructor(parent, from, to)
    {
        this.Parent = parent;
        this.From = from;
        this.To = to;
        this.SVGLine = document.createElementNS("http://www.w3.org/2000/svg", "line");
    }
    
    sub(source)
    {
        if (typeof source == "Line")
            return new Line(this.From.sub(source.From), this.To.sub(source.To))
        else if (typeof source == "int")
            return new Line(this.From.sub(source), this.To.sub(source))
    }

    add(source)
    {
        if (typeof source == "Line")
            return new Line(this.From.add(source.From), this.To.add(source.To))
        else if (typeof source == "int")
            return new Line(this.From.add(source), this.To.add(source))
    }

    Draw()
    {
        // context.beginPath();
        // context.moveTo(this.From.x - zeroPoint.x, this.From.y - zeroPoint.y);
        // context.lineTo(this.To.x - zeroPoint.x, this.To.y - zeroPoint.y);
        // context.stroke();
        // context.closePath();
        this.SVGLine.setAttribute("x1", from.x);
        this.SVGLine.setAttribute("y1", from.y);
        this.SVGLine.setAttribute("x2", to.x);
        this.SVGLine.setAttribute("y2", to.y);

        this.Parent.appendChild(this.SVGLine);
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

class SVGBoard
{
    Container;
    Tapped = false;
    ZeroPoint = new Point(0, 0);
    constructor(container)
    {
        this.Container = container;
    }

    boardMouseDown(e)
    {
        this.Tapped = true;
        this.Canvas.style.cursor = "move";
    }

    boardMouseMove(e)
    {
        if (this.Tapped != true)
            return;
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
}


let Board;

window.addEventListener("DOMContentLoaded", () =>
{
    Board = SVGBoard(document.getElementById("graphBoard"));
});