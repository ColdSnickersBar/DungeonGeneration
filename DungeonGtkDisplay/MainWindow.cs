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
		var stamper = new RoomStamper (_grid);

		var room = new RoomStamper.RoomSpec {
			x = 1,
			y = 1,
			width = 10,
			height = 10
		};
		stamper.Stamp (room);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	void OnExpose(object sender, ExposeEventArgs args)
	{
		DrawingArea area = (DrawingArea) sender;

		TileColor closedColor = new TileColor {
			Red = 0,
			Green = 0,
			Blue = 0
		};

		TileColor openColor = new TileColor {
			Red = 1d,
			Green = 1d,
			Blue = 1d
		};

		for(int y=0;y<_grid.Height;y++){
			for(int x=0;x<_grid.Width;x++){
				var tile = _grid.GetTile (x, y);
				if (!tile.IsOpen) {
					DrawSquare (area, 6d, x, y, closedColor);
				}
			}
		}

	}

	static void DrawSquare (DrawingArea area, double size, double x, double y, TileColor color)
	{
		Cairo.Context cr =  Gdk.CairoHelper.Create(area.GdkWindow);
		x *= size;
		y *= size;
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
