export class Point
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