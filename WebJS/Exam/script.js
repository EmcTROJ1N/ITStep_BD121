// const LINE_COLOR = "rgb(201,209,217)"
const LINE_COLOR = "white";
const GRID_COLOR = "white";

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
        if (source instanceof Point)
            return new Point(this.x - source.x,
                             this.y - source.y)
        else if (["int", "number"].includes(typeof source))
            return new Point(this.x - source,
                             this.y - source)
    }

    add(source)
    {
        if (source instanceof Point)
            return new Point(this.x - source.x,
                             this.y - source.y)
        else if (["int", "number"].includes(typeof source))
            return new Point(this.x - source,
                             this.y - source)
    }

}

class SVGLine
{
    From;
    To;
    SVGObject;
    Parent;

    constructor(parent, from, to)
    {
        this.Parent = parent;
        this.From = from;
        this.To = to;
        this.SVGObject = document.createElementNS("http://www.w3.org/2000/svg", "line");
        this.SVGObject.setAttribute("fill", LINE_COLOR);
        this.SVGObject.setAttribute("stroke", LINE_COLOR);
    }
    
    sub(source)
    {
        if (source instanceof SVGLine)
            return new SVGLine(this.From.sub(source.From), this.To.sub(source.To))
        else if (["int", "number"].includes(typeof source))
            return new SVGLine(this.From.sub(source), this.To.sub(source))
    }

    add(source)
    {
        if (source instanceof SVGLine)
            return new SVGLine(this.From.add(source.From), this.To.add(source.To))
        else if (["int", "number"].includes(typeof source))
            return new SVGLine(this.From.add(source), this.To.add(source))
    }

    Update()
    {
        this.From = this.From.add(this.Parent.DeltaPoint);
        this.To = this.To.add(this.Parent.DeltaPoint);

        this.SVGObject.setAttribute("x1", this.From.x);
        this.SVGObject.setAttribute("y1", this.From.y);
        this.SVGObject.setAttribute("x2", this.To.x);
        this.SVGObject.setAttribute("y2", this.To.y);
    }

    InOverlay()
    {
        let flag = false;
        let boundRect = this.Parent.Container.getBoundingClientRect();
        [this.From, this.To].forEach((point) =>
        {
            // if (point.x > this.Parent.ZeroPoint.x && point.y > this.Parent.ZeroPoint.y &&
            //     point.x < boundRect.width + this.Parent.ZeroPoint.x && point.y < boundRect.height + this.Parent.ZeroPoint.y)
            if (point.x > 0 && point.y > 0 && point.x < boundRect.width && point.y < boundRect.height)
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
    GridSpacing = 45;
    Figures = []
    GridLines = []
    
    get BoundingRect() { return this.Container.getBoundingClientRect() }

    BeginGrid = new Point(0, 0);
    DeltaPoint = new Point(0, 0);
    StartPoint = new Point(0, 0);
    
    constructor(container, scalingLabel, spacing = 45)
    {
        this.Container = container;
        this.GridSpacing = spacing;

        this.Container.addEventListener("mousedown", (e) => this.boardMouseDown(e));
        this.Container.addEventListener("mousemove", (e) => this.boardMouseMove(e));
        this.Container.addEventListener("mouseup", (e) => this.boardMouseUp(e));
        // this.Container.addEventListener("wheel", (e) => this.boardScaling(e));

        this.UpdateOverlay();
    }

    boardMouseDown(e)
    {
        this.Tapped = true;
        this.Container.style.cursor = "move";
        // this.StartPoint = new Point(e.clientX - this.Canvas.offsetLeft, e.clientY - this.Canvas.offsetTop);
        this.StartPoint = new Point(e.clientX - this.BoundingRect.left, e.clientY - this.BoundingRect.top);
    }

    boardMouseMove(e)
    {
        if (this.Tapped != true)
            return;

        
        console.log(this.StartPoint);
        console.log(this.BoundingRect.left, this.BoundingRect.top);
        console.log(e.clientX, e.clientY);
        console.log(this.StartPoint.y + this.BoundingRect.top - e.clientY);
        console.log(this.DeltaPoint);

        this.DeltaPoint = new Point(this.StartPoint.x + this.BoundingRect.left - e.clientX,
                                    this.StartPoint.y + this.BoundingRect.top - e.clientY);
        this.StartPoint = new Point(e.clientX - this.BoundingRect.left, e.clientY - this.BoundingRect.top);
        this.UpdateOverlay();
    }

    boardMouseUp(e)
    {
        this.Container.style.cursor = "default";
        this.Tapped = false;
    }

    boardScaling(e)
    {
        let direction = e.deltaY > 0 ? -.3 : .3;
        let scale = 1 + direction;

        this.Scale += direction * 100;
    }

    add(figure)
    {
        this.Figures.push(figure);
        this.UpdateOverlay();
    }

    remove(figure)
    {
        this.Figures.remove(figure);
        this.UpdateOverlay();
    }

    #DrawGrid()
    {
        let drawLine = (from, to, strokeWidth) =>
        {
            let line = document.createElementNS("http://www.w3.org/2000/svg", "line");
            line.setAttribute("stroke", GRID_COLOR);
            line.setAttribute("stroke-width", strokeWidth);

            line.setAttribute("x1", from.x);
            line.setAttribute("y1", from.y);
            line.setAttribute("x2", to.x);
            line.setAttribute("y2", to.y);

            this.Container.appendChild(line);
            this.GridLines.push(line);
        }

        let lineWidth = .1;
        for (let w = this.BeginGrid.x; w < this.BoundingRect.width; w += this.GridSpacing)
            drawLine(new Point(w, 0), new Point(w, this.BoundingRect.height), lineWidth);
        for (let h = this.BeginGrid.y; h < this.BoundingRect.height; h += this.GridSpacing)
            drawLine(new Point(0, h), new Point(this.BoundingRect.width, h), lineWidth);
    }

    #UpdateGrid()
    {
        // if (this.BeginGrid == null)
        //     this.BeginGrid = new Point(this.GridSpacing, this.GridSpacing);
        // else
        // {
        //     this.BeginGrid.x = this.BeginGrid.x - this.DeltaPoint.x;
        //     this.BeginGrid.y = this.BeginGrid.y - this.DeltaPoint.y;
            
        //     if (this.BeginGrid.x < 0) this.BeginGrid.x = this.Spacing - Math.abs(this.BeginGrid.x);
        //     if (this.BeginGrid.y < 0) this.BeginGrid.y = this.Spacing - Math.abs(this.BeginGrid.y);
        //     if (this.BeginGrid.x > this.Spacing) this.BeginGrid.x = this.BeginGrid.x % this.Spacing;
        //     if (this.BeginGrid.y > this.Spacing) this.BeginGrid.y = this.BeginGrid.y % this.Spacing;
        // }
    }

    UpdateOverlay()
    {
        this.Figures.forEach((figure) =>
        {
            if (figure.InOverlay())
            {
                // console.log("in overlay");
                figure.Update()
                if ([...this.Container.children].includes(figure.SVGObject) == false)
                    this.Container.appendChild(figure.SVGObject);
            }
            else
            {
                if ([...this.Container.children].includes(figure.SVGObject))
                    this.Container.removeChild(figure.SVGObject);
            }
        });
        
        if (this.GridLines.length == 0) this.#DrawGrid()
        else this.#UpdateGrid();
    }
}


let Board;

window.addEventListener("DOMContentLoaded", () =>
{
    Board = new SVGBoard(document.getElementById("graphBoard"));
    // line = new Line(Board, new Point(1, 1), new Point(10, 10));
    Board.add(new SVGLine(Board, new Point(100, 100),
                              new Point(300, 300)));
});