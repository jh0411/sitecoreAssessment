using System;
using System.Collections.Generic;
using System.Numerics;

namespace Question1
{
    // Represents a Point in 2D space
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Move(double deltaX, double deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }

        public void Rotate(double angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosAngle = Math.Cos(angleInRadians);
            double sinAngle = Math.Sin(angleInRadians);

            double newX = X * cosAngle - Y * sinAngle;
            double newY = X * sinAngle + Y * cosAngle;
            X = newX;
            Y = newY;
        }

        public void printCoordinates()
        {
            Console.WriteLine($"({X}, {Y})");
        }

        public override string ToString()
        {
            return $"Point({X}, {Y})";
        }

    }

    // Represents a Line segment
    public class Line
    {
        private double x1, y1, x2, y2;

        public Line(double x1, double y1, double x2, double y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        public void Move(double deltaX, double deltaY)
        {
            x1 += deltaX;
            y1 += deltaY;
            x2 += deltaX;
            y2 += deltaY;

        }

        public void Rotate(double angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosAngle = Math.Cos(angleInRadians);
            double sinAngle = Math.Sin(angleInRadians);

            double newX1 = x1 * cosAngle - y1 * sinAngle;
            double newY1 = x1 * sinAngle + y1 * cosAngle;
            double newX2 = x2 * cosAngle - y2 * sinAngle;
            double newY2 = x2 * sinAngle + y2 * cosAngle;

            x1 = newX1;
            y1 = newY1;
            x2 = newX2;
            y2 = newY2;
        }

        public void printCoordinates()
        {
            Console.WriteLine($"Line({x1}, {y1}), ({x2}, {y2})");
        }

        public override string ToString()
        {
            return $"Line(Start: ({x1}, {y1}), End: ({x2}, {y2}))";
        }


    }

    // Represents a Circle, now inheriting from Point
    public class Circle : Point
    {
        public double Radius { get; set; }

        public Circle(Point center, double radius) : base(center.X, center.Y)
        {
            Radius = radius;
        }

        public override string ToString()
        {
            return $"Circle(Center: ({X}, {Y}), Radius: {Radius})";
        }


    }

    // Aggregation class to hold various geometric figures
    public class Aggregation
    {
        private List<object> figures;

        public Aggregation()
        {
            figures = new List<object>();
        }

        public void AddFigure(object figure)
        {
            if (figure is Point || figure is Line || figure is Circle)
            {
                figures.Add(figure);
            }
            else
            {
                throw new ArgumentException("Unsupported figure type.");
            }
        }

        public void Move(double deltaX, double deltaY)
        {
            foreach (object figure in figures)
            {
                switch (figure)
                {
                    // Handle more specific types first (Line and Circle)
                    case Line line:
                        line.Move(deltaX, deltaY);
                        break;
                    case Circle circle:
                        circle.Move(deltaX, deltaY);
                        break;
                    // Handle general type last
                    case Point point:
                        point.Move(deltaX, deltaY);
                        break;
                }
            }
        }

        public void Rotate(double angleInDegrees)
        {
            foreach (object figure in figures)
            {
                switch (figure)
                {
                    // Handle more specific types first (Line and Circle)
                    case Line line:
                        line.Rotate(angleInDegrees);
                        break;
                    case Circle circle:
                        circle.Rotate(angleInDegrees);
                        break;
                    // Handle general type last
                    case Point point:
                        point.Rotate(angleInDegrees);
                        break;
                }
            }
        }



        public override string ToString()
        {
            string result = "Aggregation contains:\n";
            foreach (object figure in figures)
            {
                result += figure.ToString() + "\n";
            }
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Create Instances of individual geometric figures
            Console.Write("Please enter X coordinate for Point: ");
            double x = double.Parse(Console.ReadLine());
            Console.Write("Please enter X coordinate for Point: ");
            double y = double.Parse(Console.ReadLine());

            Point p = new Point(x, y);
            Console.Write("Initial Point coordinates: ");
            p.printCoordinates();

            p.Move(2, 3);
            Console.Write("Point coordinates after moving: ");
            p.printCoordinates();

            p.Rotate(45);
            Console.Write("Point coordinates after rotating: ");
            p.printCoordinates();
            Console.WriteLine();

            //Create Instance of Line with start and end points
            Console.Write("Please enter X coordinate for starting Point: ");
            double x1 = double.Parse(Console.ReadLine());
            Console.Write("Please enter X coordinate for starting Point: ");
            double y1 = double.Parse(Console.ReadLine());

            Console.Write("Please enter X coordinate for ending Point: ");
            double x2 = double.Parse(Console.ReadLine());
            Console.Write("Please enter X coordinate for ending Point: ");
            double y2 = double.Parse(Console.ReadLine());

            Line l = new Line(x1, y1, x2, y2);
            Console.Write("Initial line coordinates: ");
            l.printCoordinates();

            l.Move(2, 3);
            Console.Write("Line coordinates after moving: ");
            l.printCoordinates();

            l.Rotate(90);
            Console.Write("Line coordinates after rotating: ");
            l.printCoordinates();
            Console.WriteLine();

            //Create Instances of circle 
            Console.Write("Please enter X coordinate for Circle: ");
            double c1 = double.Parse(Console.ReadLine());
            Console.Write("Please enter X coordinate for Circle: ");
            double c2 = double.Parse(Console.ReadLine());

            Console.Write("Please enter radius for Circle: ");
            int r = int.Parse(Console.ReadLine());

            Circle c = new Circle(new Point(c1, c2), r);
            Console.Write("Initial circle coordinates: ");
            c.printCoordinates();

            c.Move(-1, -1);
            Console.Write("Circle coordinates after moving: ");
            c.printCoordinates();

            c.Rotate(30);
            Console.Write("Circle coordinates after rotating: ");
            c.printCoordinates();
            Console.WriteLine();


            // Create an aggregation instance
            Aggregation aggregation = new Aggregation();

            // Get user input for the number of figures
            Console.Write("How many figures do you want to add: ");
            int numFigures = int.Parse(Console.ReadLine());

            if (numFigures <= 0)
            {
                Console.WriteLine($"System could not proceed with {numFigures} figures");
                Console.Write("Please enter the figures do you want to add: ");
                numFigures = int.Parse(Console.ReadLine());
            }

            for (int i = 0; i < numFigures; i++)
            {
                Console.WriteLine("\nEnter figure type (Point, Line, Circle):");
                string figureType = Console.ReadLine();

                switch (figureType.ToLower())
                {
                    case "point":
                        Console.Write("Enter X coordinate for Point: ");
                        double pointX = double.Parse(Console.ReadLine());
                        Console.Write("Enter Y coordinate for Point: ");
                        double pointY = double.Parse(Console.ReadLine());

                        Point point = new Point(pointX, pointY);
                        aggregation.AddFigure(point);
                        break;

                    case "line":
                        Console.Write("Enter X coordinate for Line Start Point: ");
                        double lineStartX = double.Parse(Console.ReadLine());
                        Console.Write("Enter Y coordinate for Line Start Point: ");
                        double lineStartY = double.Parse(Console.ReadLine());

                        Console.Write("Enter X coordinate for Line End Point: ");
                        double lineEndX = double.Parse(Console.ReadLine());
                        Console.Write("Enter Y coordinate for Line End Point: ");
                        double lineEndY = double.Parse(Console.ReadLine());

                        Line line = new Line(lineStartX, lineStartY, lineEndX, lineEndY);
                        aggregation.AddFigure(line);
                        break;

                    case "circle":
                        Console.Write("Enter X coordinate for Circle Center: ");
                        double circleCenterX = double.Parse(Console.ReadLine());
                        Console.Write("Enter Y coordinate for Circle Center: ");
                        double circleCenterY = double.Parse(Console.ReadLine());

                        Console.Write("Enter radius for Circle: ");
                        double radius = double.Parse(Console.ReadLine());

                        Circle circle = new Circle(new Point(circleCenterX, circleCenterY), radius);
                        aggregation.AddFigure(circle);
                        break;

                    default:
                        Console.WriteLine("Unsupported figure type. Please enter Point, Line, or Circle.");
                        i--; // to ask again if input is incorrect
                        break;
                }
            }

                Console.WriteLine("\nInitial Aggregation State:");
                Console.WriteLine(aggregation);

                // Ask user to move and rotate the figures
                Console.Write("\nEnter how much to move along X-axis: ");
                double deltaX = double.Parse(Console.ReadLine());
                Console.Write("Enter how much to move along Y-axis: ");
                double deltaY = double.Parse(Console.ReadLine());

                aggregation.Move(deltaX, deltaY);
                Console.WriteLine("\nAfter Moving:");
                Console.WriteLine(aggregation);

                Console.Write("\nEnter the angle (in degrees) to rotate all figures: ");
                double angle = double.Parse(Console.ReadLine());


                aggregation.Rotate(angle);
                Console.WriteLine("\nAfter Rotating:");
                Console.WriteLine(aggregation);
        }
    }
}