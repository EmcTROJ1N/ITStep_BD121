const ELEMENT_COLOR = "white";
const GRID_COLOR = "white";
const SELECTED_COLOR = "yellow";
const LINES_THICKNESS = 10;
const VERTEX_WIDTH = 400;
const VERTEX_HEIGHT = 500;
const FONT_SIZE = 30;


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

    InOverlay(sourceRect)
    {
        return this.x >= 0 && this.y >= 0 && 
                this.x <= sourceRect.width && this.y <= sourceRect.height;
    }
}

class SVGBase
{
    SVGObject;
    Parent;

    get Color() { this.SVGObject.getAttribute("stroke"); }
    set Color(value)
    {
        this.SVGObject.setAttribute("fill", value);
        this.SVGObject.setAttribute("stroke", value);
    }

    constructor(parent)
    {
        this.Parent = parent;
    }

    Update() { throw new Error("Method Update would be overrided"); }
    Offset(offsetPoint) { throw new Error("Method Offset would be overrided"); }
    InOverlay() { throw new Error("Method InOverlay would be overrided"); }
}

class SVGLine extends SVGBase
{
    From;
    To;
    
    get StrokeWidth() { this.SVGObject.getAttribute("stroke-width"); }
    set StrokeWidth(value) { this.SVGObject.setAttribute("stroke-width", value); }

    constructor(parent, from, to)
    {
        super(parent);
        this.From = from;
        this.To = to;
        this.SVGObject = document.createElementNS("http://www.w3.org/2000/svg", "line");
        this.SVGObject.Tag = this;
        this.StrokeWidth = LINES_THICKNESS;
        this.Color = ELEMENT_COLOR;
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
        this.SVGObject.setAttribute("x1", this.From.x);
        this.SVGObject.setAttribute("y1", this.From.y);
        this.SVGObject.setAttribute("x2", this.To.x);
        this.SVGObject.setAttribute("y2", this.To.y);
    }

    Offset(offsetPoint)
    {
        this.From = this.From.add(offsetPoint);
        this.To = this.To.add(offsetPoint);
    }

    InOverlay()
    {
        let flag = false;
        [this.From, this.To].forEach(point =>
        {
            if (point.InOverlay(this.Parent.BoundingRect))
            {
                flag = true;
                return;
            }
        });
        return flag;
    }

}

class SVGVertex extends SVGBase
{
    Cords;

    #_PhotoUrl;
    #_About;
    
    set PhotoUrl(url)
    {
        this.SVGPhoto.setAttribute("href", url);
        this.#_PhotoUrl = url;
    }
    get PhotoUrl() { return this.#_PhotoUrl; }

    set About(text)
    {
        this.SVGText.innerHTML = text;
        this.#_About = text;
    }
    get About() { return this.#_About; }
    
    Width;
    Height;
    SVGPhoto;
    SVGText;

    constructor(parent, cords, photoUrl, about, width = VERTEX_WIDTH, height = VERTEX_HEIGHT)
    {
        super(parent);
        
        this.Width = width;
        this.Height = height;

        this.SVGPhoto = document.createElementNS("http://www.w3.org/2000/svg", "image");
        this.SVGPhoto.setAttribute("width", this.Width);
        this.SVGPhoto.setAttribute("height", this.Height);

        this.SVGText = document.createElementNS("http://www.w3.org/2000/svg", "text");
        this.SVGText.setAttribute("x", cords.x);
        this.SVGText.setAttribute("y", cords.y + this.Height);
        this.SVGText.setAttribute("font-size", FONT_SIZE);
        this.SVGText.setAttribute("fill", ELEMENT_COLOR);
        // this.SVGText.setAttribute("dominant-baseline", "middle");
        // this.SVGText.setAttribute("text-anchor", "center");

        this.Cords = cords;
        this.Update();

        this.PhotoUrl = photoUrl;
        this.About = about;

        this.SVGObject = document.createElementNS("http://www.w3.org/2000/svg", "g");
        this.SVGObject.setAttribute("width", this.Width);
        this.SVGObject.setAttribute("height", this.Height);

        this.SVGObject.appendChild(this.SVGText);
        this.SVGObject.appendChild(this.SVGPhoto);
        
        this.SVGObject.Tag = this;
    }

    Update()
    {
        this.SVGPhoto.setAttribute("x", this.Cords.x);
        this.SVGPhoto.setAttribute("y", this.Cords.y);
    
        this.SVGText.setAttribute("x", this.Cords.x);
        this.SVGText.setAttribute("y", this.Cords.y + this.Height + FONT_SIZE + 5);
    }

    Offset(offsetPoint)
    {
        this.Cords = this.Cords.add(offsetPoint);
    }

    InOverlay()
    {
        let flag = false;
        [this.Cords,
         new Point(this.Cords.x + this.Width, this.Cords.y),
         new Point(this.Cords.x, this.Cords.y + this.Height),
         new Point(this.Cords.x + this.Width, this.Cords.y + this.Width),
        ].forEach(point =>
        {
            if (point.InOverlay(this.Parent.BoundingRect))
            {
                flag = true;
                return;
            }
        });
        return flag;
    }
}

class ObservableArray extends Array
{
    SelectFunction;
    RemoveFunction;

    constructor(selectFunc, removeFunc, ...values)
    {
        super(...values);
        this.SelectFunction = selectFunc;
        this.RemoveFunction = removeFunc;
    }

    push(...items)
    {
        super.push(...items);
        this.SelectFunction(...items);
    }

    pop()
    {
        let popped = super.pop();
        this.RemoveFunction(popped);
        return popped;
    }

    splice(start, deletedCount, ...items)
    {
        let deleted = super.splice(start, deletedCount, ...items);
        if ([...items].length != 0)
            this.SelectFunction(...items)
        this.RemoveFunction(...deleted);
        return deleted;
    }
}

class SVGBoard
{
    Container;
    #BoardTapped = false;
    #FigureTapped = false;
    GridSpacing = 45;
    #Figures = []
    #DraggableFigures;
    #GridLines = [];
    ScalingLabel;

    get BoundingRect()
    { 
        let rect = this.Container.getBoundingClientRect();
        
        rect.width /= this.Container.currentScale;
        rect.height /= this.Container.currentScale;
        
        return rect;
    }

    DeltaPoint = new Point(0, 0);
    StartPoint = new Point(0, 0);
    
    constructor(container, scalingLabel, spacing = 45)
    {
        this.Container = container;
        this.GridSpacing = spacing;
        this.ScalingLabel = scalingLabel;

        this.#DraggableFigures = new ObservableArray(this.selectDraggable, this.removeDraggable);

        this.Container.addEventListener("mousedown", this.boardMouseDown);
        this.Container.addEventListener("mousemove", this.areaMouseMove);
        this.Container.addEventListener("mouseup", this.boardMouseUp);
        this.Container.addEventListener("wheel", this.boardScaling);

        this.UpdateOverlay();
    }

    selectDraggable(...items)
    {
        items.forEach(element =>
        {
            if (element instanceof SVGBase)
                element.SVGObject.classList.add("selected");
        });
    }

    removeDraggable(...items)
    {
        items.forEach(element =>
        {
            if (element instanceof SVGBase)
                element.SVGObject.classList.remove("selected");
        });
    }

    boardMouseDown = (e) =>
    {
        this.#BoardTapped = true;
        this.Container.style.cursor = "move";
        this.StartPoint = new Point(e.clientX - this.BoundingRect.left, e.clientY - this.BoundingRect.top);
    }

    areaMouseMove = (e) =>
    {
        if (this.#BoardTapped != true && this.#FigureTapped != true)
            return;
        
        this.DeltaPoint = new Point((this.StartPoint.x + this.BoundingRect.left - e.clientX) / this.Container.currentScale,
                                    (this.StartPoint.y + this.BoundingRect.top - e.clientY) / this.Container.currentScale);
        this.StartPoint = new Point(e.clientX - this.BoundingRect.left, e.clientY - this.BoundingRect.top);

        if (this.#BoardTapped == true)
        {
            this.#Figures.forEach(figure => figure.Offset(this.DeltaPoint));
            this.UpdateOverlay();
        }
        else if (this.#FigureTapped == true)
        {
            this.#DraggableFigures.forEach(figure =>
            {
                figure.Offset(this.DeltaPoint);
                figure.Update();
            });
        }
    }

    boardMouseUp = (e) =>
    {
        this.Container.style.cursor = "default";
        this.#BoardTapped = false;
    }

    boardScaling = (e) =>
    {
        let direction = e.deltaY > 0 ? -.03 : .03;
        if ((this.Container.currentScale <= .3 && direction < 0) ||
            (this.Container.currentScale >= 2 && direction > 0))
            return;
        
        this.#GridLines.forEach(line => this.Container.removeChild(line.SVGObject));
        this.#GridLines.splice(0, this.#GridLines.length);
        
        this.Container.currentScale += direction;

        this.#DrawGrid();
        this.#UpdateGrid();
        
        this.ScalingLabel.innerText = `${Math.floor(this.Container.currentScale * 100)}%`;
    }

    figureMouseDown = (e) =>
    {
        this.#FigureTapped = true;
        this.Container.style.cursor = "move";
        this.StartPoint = new Point(e.clientX - this.BoundingRect.left, e.clientY - this.BoundingRect.top);
        this.#DraggableFigures.push(e.currentTarget.Tag);
        e.stopPropagation();
    }

    figureMouseUp = (e) =>
    {
        this.#FigureTapped = false;
        this.Container.style.cursor = "default";
        this.#DraggableFigures.splice(this.#DraggableFigures.indexOf(e.currentTarget.Tag), 1);
        e.stopPropagation();
    }

    add(figure)
    {
        this.#Figures.push(figure);
        figure.SVGObject.addEventListener("mousedown", this.figureMouseDown);
        figure.SVGObject.addEventListener("mouseup", this.figureMouseUp);
        this.UpdateOverlay();
    }

    remove(figure)
    {
        this.#Figures.remove(figure);
        figure.SVGObject.removeEventListener("mousedown", this.figureMouseDown);
        figure.SVGObject.removeEventListener("mouseup", this.figureMouseUp);
        this.UpdateOverlay();
    }

    #DrawGrid()
    {
        let drawLine = (from, to, strokeWidth) =>
        {
            let line = new SVGLine(this, from, to);
            line.StrokeWidth = strokeWidth;
            line.Color = GRID_COLOR;
            line.Visible = true;
            this.Container.appendChild(line.SVGObject);
            this.#GridLines.push(line);
        }

        let lineWidth = .7;

        
        for (let w = 0; w < this.BoundingRect.width; w += this.GridSpacing)
            drawLine(new Point(w, 0), new Point(w, this.BoundingRect.height), lineWidth);
        for (let h = 0; h < this.BoundingRect.height; h += this.GridSpacing)
            drawLine(new Point(0, h), new Point(this.BoundingRect.width, h), lineWidth);
    }

    #UpdateGrid()
    {
        let horizontalLinesCount = this.#GridLines.filter(line => line.From.y == line.To.y).length;
        let verticalLinesCount = this.#GridLines.filter(line => line.From.x == line.To.x).length;

        this.#GridLines.forEach(line =>
        {
            line.Offset(this.DeltaPoint);
            
            if (line.LastFrom != undefined)
            {
                line.LastFrom = line.LastFrom.add(this.DeltaPoint);
                line.LastTo = line.LastTo.add(this.DeltaPoint);
            }

            let remainderHorizontal = horizontalLinesCount * this.GridSpacing - this.BoundingRect.height;
            let remainderVertical   = verticalLinesCount   * this.GridSpacing - this.BoundingRect.width;

            if (line.To.y == line.From.y)
            {
                if (line.From.y > this.BoundingRect.height && line.Visible == false && this.DeltaPoint.y < 0)
                {
                    let tmp = new Point(line.From.x, line.From.y);
                    line.From = line.LastFrom;
                    line.LastFrom = tmp;
                    
                    tmp = line.To;
                    line.To = line.LastTo;
                    line.LastTo = tmp;
                }
                if (line.From.y < 0 && line.Visible == false && this.DeltaPoint.y > 0)
                {
                    let tmp = new Point(line.From.x, line.From.y);
                    line.From = line.LastFrom;
                    line.LastFrom = tmp;
                    
                    tmp = line.To;
                    line.To = line.LastTo;
                    line.LastTo = tmp;
                }

                if (line.From.y < 0 && line.Visible)
                {
                    line.LastFrom = new Point(line.From.x, line.From.y);
                    line.LastTo = new Point(line.To.x, line.To.y);

                    line.From.y = horizontalLinesCount * this.GridSpacing - Math.abs(line.From.y);
                    line.To.y = line.From.y;
                    line.Visible = false;
                }
                if (line.From.y > this.BoundingRect.height && line.Visible)
                {
                    line.LastFrom = new Point(line.From.x, line.From.y);
                    line.LastTo = new Point(line.To.x, line.To.y);

                    line.To.y = -remainderHorizontal + Math.abs(line.From.y - this.BoundingRect.height);
                    line.From.y = line.To.y;
                    line.Visible = false;
                }

                if (line.From.x < 0)
                {
                    line.To.x += Math.abs(line.From.x);
                    line.From.x += Math.abs(line.From.x);
                }
                if (line.To.x > this.BoundingRect.width)
                {
                    line.From.x -= line.To.x - this.BoundingRect.width;
                    line.To.x -= line.To.x - this.BoundingRect.width;
                }
            }

            else if (line.To.x == line.From.x)
            {
                if (line.From.x > this.BoundingRect.width && line.Visible == false && this.DeltaPoint.x < 0)
                {
                    let tmp = new Point(line.From.x, line.From.y);
                    line.From = line.LastFrom;
                    line.LastFrom = tmp;
                    
                    tmp = line.To;
                    line.To = line.LastTo;
                    line.LastTo = tmp;
                }
                if (line.From.x < 0 && line.Visible == false && this.DeltaPoint.x > 0)
                {
                    let tmp = new Point(line.From.x, line.From.y);
                    line.From = line.LastFrom;
                    line.LastFrom = tmp;
                    
                    tmp = line.To;
                    line.To = line.LastTo;
                    line.LastTo = tmp;
                }

                if (line.From.x < 0 && line.Visible)
                {
                    line.LastFrom = new Point(line.From.x, line.From.y);
                    line.LastTo = new Point(line.To.x, line.To.y);

                    line.From.x = verticalLinesCount * this.GridSpacing - Math.abs(line.From.x);
                    line.To.x = line.From.x;
                    line.Visible = false;
                }
                if (line.From.x > this.BoundingRect.width && line.Visible)
                {
                    line.LastFrom = new Point(line.From.x, line.From.y);
                    line.LastTo = new Point(line.To.x, line.To.y);

                    line.To.x = -remainderVertical + Math.abs(line.From.x - this.BoundingRect.width);
                    line.From.x = line.To.x;
                    line.Visible = false;
                }

                if (line.From.y < 0)
                {
                    line.To.y += Math.abs(line.From.y);
                    line.From.y += Math.abs(line.From.y);
                }
                if (line.To.y > this.BoundingRect.height)
                {
                    line.From.y -= line.To.y - this.BoundingRect.height;
                    line.To.y -= line.To.y - this.BoundingRect.height;
                }
            }

            if (line.Visible != true)
            {
                if (line.InOverlay())
                    line.Visible = true;
            }
            
            line.Update();
        });
    }

    UpdateOverlay()
    {
        this.#Figures.forEach((figure) =>
        {
            if (figure.InOverlay())
            {
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
        
        if (this.#GridLines.length == 0) this.#DrawGrid()
        else this.#UpdateGrid();
    }
}


let Board;

window.addEventListener("DOMContentLoaded", () =>
{
    Board = new SVGBoard(document.getElementById("graphBoard"),
                         document.getElementById("scaling"));
    Board.add(new SVGLine(Board, new Point(100, 100),
                              new Point(300, 500)));
    Board.add(new SVGLine(Board, new Point(500, 500),
                              new Point(100, 600)));
    Board.add(new SVGVertex(Board, new Point(700, 100),
        "https://sun9-64.userapi.com/impg/HocP2Qk9Zls0LWakQqT-RbDm6MTbdZkEh5AsXQ/zjTT27po88E.jpg?size=1620x2160&quality=95&sign=17985558459b7dc33b57e0fc6b069488&type=album",
        "German Pokrovskiy"));
});