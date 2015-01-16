using System;
using Gtk;
using DungeonGeneration;

public partial class MainWindow: Gtk.Window
{
	Grid _grid;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		SetDefaultSize(230 * 6, 150 * 6);
		SetPosition(WindowPosition.Center);
		DeleteEvent += delegate { Application.Quit(); };;

		DrawingArea darea = new DrawingArea();
		darea.ExposeEvent += OnExpose;

		Add(darea);

		ShowAll();

		_grid = new Grid (100, 100);

		var stamper = new RectangularRoomStamper (_grid);

		var generator = new DungeonGenerator (stamper, null);
		generator.RoomPasses = 100;
		generator.Generate ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	void OnExpose(object sender, ExposeEventArgs args)
	{
		var area = (DrawingArea)sender;

		DrawGrid (area);

	}

	void DrawGrid (DrawingArea area)
	{
		for (int y = 0; y < _grid.Height; y++) {
			for (int x = 0; x < _grid.Width; x++) {
				DrawTile (area, x , y);
			}
		}
	}

	void DrawTile (DrawingArea area, int x, int y)
	{
		TileColor closedColor = new TileColor {
			Red = 0,
			Green = 0,
			Blue = 0
		};

		TileColor openBorderColor = new TileColor {
			Red = 0.6d,
			Green = 0.6d,
			Blue = 0.2d
		};

		var openColor = new TileColor {
			Red = 1d,
			Green = 1d,
			Blue = 1d
		};

		var squareSize = 6d;
		var padding = 0.5d;

		var tile = _grid.GetTile (x, y);
		if (!tile.IsOpen) {
			DrawSquare (area, squareSize, x, y, closedColor);
		}
		else {
			DrawSquare (area, squareSize, x, y, openBorderColor);
			DrawSquare (area, squareSize, x, y, openColor, padding);
		}
	}

	static void DrawSquare (DrawingArea area, double size, double x, double y, TileColor color, double padding = 0d)
	{
		Cairo.Context cr =  Gdk.CairoHelper.Create(area.GdkWindow);

		x = (x * size) + padding;
		y = (y * size) + padding;

		size = size - (padding * 2);

		cr.LineWidth = size;
		cr.SetSourceRGB (color.Red, color.Green, color.Blue);
		cr.MoveTo (new Cairo.PointD (x, y));
		cr.LineTo (new Cairo.PointD (x, size+y));
		cr.ClosePath ();
		cr.Stroke ();

		((IDisposable) cr.GetTarget()).Dispose();                                      
		((IDisposable) cr).Dispose();
	}

	public struct TileColor {
		public double Red;
		public double Green;
		public double Blue;
	}
}
