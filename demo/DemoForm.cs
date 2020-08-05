using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Cyotek.Collections.Generic.CircularBuffer.Demo
{
  public partial class DemoForm : Form
  {
    #region Private Fields

    private Pen _bodyPen;

    private Point[] _buffer;

    private Direction _direction;

    private int _gridSize;

    private Pen _headPen;

    private int _headSize;

    private long _moveCount;

    private Random _random;

    private CircularBuffer<Point> _snakeBody;

    private Point _snakeHead;

    private double _turnChance;

    #endregion Private Fields

    #region Public Constructors

    public DemoForm()
    {
      this.InitializeComponent();
    }

    #endregion Public Constructors

    #region Protected Methods

    protected override void OnLoad(EventArgs e)
    {
      this.DoubleBuffered = true;

      base.OnLoad(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      Graphics g;

      g = e.Graphics;

      g.Clear(Color.White);

      g.SmoothingMode = SmoothingMode.AntiAlias;

      if (_snakeBody.Size > 1)
      {
        // don't do this!
        //g.DrawLines(_bodyPen, _snakeBody.ToArray());

        if (!this.HasSplitResults())
        {
          // this isn't great either really, but as Graphics.DrawLines
          // isn't enlightened enough to take a start and length,
          // we copy the buffer out of the CircularBuffer into an
          // existing byte array so in theory we aren't allocating
          // an array over and over again. In this demo though, we
          // are if the sizes are different
          this.EnsureSize(ref _buffer, _snakeBody.Size);
          _snakeBody.CopyTo(_buffer);

          g.DrawLines(_bodyPen, _buffer);
        }
        else
        {
          int start;
          Point previous;
          Point current;

          // if we've wrapped the playing field, I can't just
          // call DrawLines with the entire buffer as we'll get
          // lines drawn across the entire playing field, so
          // instead I need to break it down into smaller buffers.
          // In this scenario I'd be better off with a pool, but
          // I haven't implemented one for this simple demo

          start = 0;
          previous = _snakeBody.PeekAt(0);

          for (int i = 1; i < _snakeBody.Size; i++)
          {
            current = _snakeBody.PeekAt(i);

            if (this.GetDistance(previous, current) > _gridSize)
            {
              // here we have a split, so let us grab a subset of
              // the buffer and draw our lines
              this.DrawSection(g, start, i - start);
              start = i;
            }

            previous = current;
          }

          if (start < _snakeBody.Size)
          {
            this.DrawSection(g, start, _snakeBody.Size - start);
          }
        }
      }

      g.DrawEllipse(_headPen, new Rectangle(_snakeHead.X - _headSize, _snakeHead.Y - _headSize, _gridSize, _gridSize));
    }

    protected override void OnShown(EventArgs e)
    {
      Size size;

      size = this.ClientSize;
      _turnChance = 0.2;
      _snakeBody = new CircularBuffer<Point>(256);
      _snakeHead = new Point(size.Width / 2, size.Height / 2);
      _direction = Direction.Right;
      _buffer = new Point[0];
      _gridSize = 12;
      _headSize = _gridSize / 2;
      _bodyPen = new Pen(Color.CornflowerBlue)
      {
        Width = _gridSize / 2,
        EndCap = LineCap.Round,
        StartCap = LineCap.Round,
        LineJoin = LineJoin.Round
      };
      _headPen = new Pen(Color.SlateBlue)
      {
        Width = _gridSize / 2,
        EndCap = LineCap.Round,
        StartCap = LineCap.Round,
        LineJoin = LineJoin.Round
      };
      _random = new Random(20200805);

      this.SetupInitial();

      base.OnShown(e);
    }

    #endregion Protected Methods

    #region Private Methods

    private void AutomaticCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      snakeTimer.Enabled = automaticCheckBox.Checked;
    }

    private void DrawSection(Graphics g, int start, int length)
    {
      if (length > 1) // DrawLines crashes if there isn't at least two points
      {
        this.EnsureSize(ref _buffer, length);
        _snakeBody.CopyTo(start, _buffer, 0, length);
        g.DrawLines(_bodyPen, _buffer);
      }
    }

    private void EnsureSize(ref Point[] buffer, int size)
    {
      if (buffer.Length != size)
      {
        buffer = new Point[size];
      }
    }

    private int GetDistance(Point p1, Point p2)
    {
      return this.GetDistance(p1.X, p1.Y, p2.X, p2.Y);
    }

    private int GetDistance(int x1, int y1, int x2, int y2)
    {
      float dx;
      float dy;

      dx = x1 - x2;
      dy = y1 - y2;

      return (int)Math.Sqrt((dx * dx) + (dy * dy));
    }

    private Size GetHeading()
    {
      int x;
      int y;

      if (_direction == Direction.Up)
      {
        x = 0;
        y = -_gridSize;
      }
      else if (_direction == Direction.Down)
      {
        x = 0;
        y = _gridSize;
      }
      else if (_direction == Direction.Left)
      {
        x = -_gridSize;
        y = 0;
      }
      else if (_direction == Direction.Right)
      {
        x = _gridSize;
        y = 0;
      }
      else
      {
        x = 0;
        y = 0;
      }

      return new Size(x, y);
    }

    private Direction GetRandomDirection()
    {
      Direction newDirection;
      double sample;

      sample = _random.NextDouble();

      if (sample < _turnChance)
      {
        newDirection = _direction - 1;
        if (newDirection < Direction.Up)
        {
          newDirection = Direction.Left;
        }
      }
      else if (sample > 1D - _turnChance)
      {
        newDirection = _direction + 1;
        if (newDirection > Direction.Left)
        {
          newDirection = Direction.Up;
        }
      }
      else
      {
        newDirection = _direction;
      }

      return newDirection;
    }

    private bool HasSplitResults()
    {
      bool result;

      result = false;

      for (int i = 1; i < _snakeBody.Size; i++)
      {
        if (this.GetDistance(_snakeBody.PeekAt(i - 1), _snakeBody.PeekAt(i)) > _gridSize)
        {
          result = true;
          break;
        }
      }

      return result;
    }

    private void NextButton_Click(object sender, EventArgs e)
    {
      this.NextMove();
    }

    private void NextMove()
    {
      int x;
      int y;
      Size heading;
      Size field;

      _moveCount++;

      _snakeBody.Put(_snakeHead);

      heading = this.GetHeading();
      field = this.ClientSize;
      x = _snakeHead.X + heading.Width;
      y = _snakeHead.Y + heading.Height;

      if (wrapCheckBox.Checked)
      {
        if (x < 0)
        {
          x = field.Width - 1;
        }
        else if (x >= field.Width)
        {
          x = 0;
        }

        if (y < 0)
        {
          y = field.Height - 1;
        }
        else if (y >= field.Height)
        {
          y = 0;
        }

        _snakeHead = new Point(x, y);
      }
      else if (x >= 0 && y >= 0 && x < field.Width && y < field.Height)
      {
        _snakeHead = new Point(x, y);
      }

      _direction = this.GetRandomDirection();

      this.Invalidate();
    }

    private void SetupInitial()
    {
      string[] args;

      args = Environment.GetCommandLineArgs();

      if (args.Length == 2 && long.TryParse(args[1], out long count))
      {
        automaticCheckBox.Checked = false;

        for (int i = 0; i < count; i++)
        {
          this.NextMove();
        }
      }
      else
      {
        snakeTimer.Enabled = true;
      }
    }

    private void SnakeTimer_Tick(object sender, EventArgs e)
    {
      this.NextMove();
    }

    #endregion Private Methods
  }
}
