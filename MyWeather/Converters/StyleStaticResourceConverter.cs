namespace MyWeather.Converters
{
    using System;
    using Windows.Foundation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media;

    public sealed class StyleStaticResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return null;

            var resourceKey = value.ToString();
            var resource = Application.Current.Resources[resourceKey] as Style;
            if (resource != null)
            {
                var style = new Style();
                style.BasedOn = resource.BasedOn;
                style.TargetType = resource.TargetType;
                foreach (var setter in resource.Setters)
                {
                    var s = setter as Setter;
                    if (s != null)
                    {
                        var g = s.Value as PathGeometry;
                        if (g != null)
                        {
                            style.Setters.Add(new Setter(s.Property, CloneDeep(g)));
                        }
                        else
                        {
                            style.Setters.Add(new Setter(s.Property, s.Value));
                        }
                        
                    }
                }
                
                return style;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private static PathGeometry CloneDeep(PathGeometry pathGeometry)
        {
            var newPathGeometry = new PathGeometry();
            foreach (var figure in pathGeometry.Figures)
            {
                var newFigure = new PathFigure();
                newFigure.StartPoint = figure.StartPoint;
                foreach (var segment in figure.Segments)
                {
                    var segmentAsPolyLineSegment = segment as PolyLineSegment;
                    if (segmentAsPolyLineSegment != null)
                    {
                        var newSegment = new PolyLineSegment();
                        foreach (var point in segmentAsPolyLineSegment.Points)
                        {
                            newSegment.Points.Add(point);
                        }
                        newFigure.Segments.Add(newSegment);
                    }

                    var segmentLineSegment = segment as LineSegment;
                    if (segmentLineSegment != null)
                    {
                        var newSegment = new LineSegment();
                        newSegment.Point = new Point(segmentLineSegment.Point.X, segmentLineSegment.Point.Y);
                        newFigure.Segments.Add(newSegment);
                    }

                    var segmentBezierSegment = segment as BezierSegment;
                    if (segmentBezierSegment != null)
                    {
                        var newSegment = new BezierSegment();
                        newSegment.Point1 = new Point(segmentBezierSegment.Point1.X, segmentBezierSegment.Point1.Y);
                        newSegment.Point2 = new Point(segmentBezierSegment.Point2.X, segmentBezierSegment.Point2.Y);
                        newSegment.Point3 = new Point(segmentBezierSegment.Point3.X, segmentBezierSegment.Point3.Y);
                        newFigure.Segments.Add(newSegment);
                    }
                }

                newPathGeometry.Figures.Add(newFigure);
            }
            return newPathGeometry;
        }
    }
}
