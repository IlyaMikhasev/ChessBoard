using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessBoard
{
    public enum ABC {
    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H}
    public partial class Form1 : Form
    {
        static int _sizeCeel = 80;
        static int _widthMap = 677;
        static int _heigthMap = 700;
        int border = 20;
        Graphics g;
        Rectangle cell;
        static Point moveStart = new Point(0,0), moveEnd;
        public Form1()
        {
            InitializeComponent();
        }
        private void createForm() {
            Form form = new Form();
            form.Size = new Size(_widthMap, _heigthMap);
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.Paint += paint_chessBoard;
            form.MouseClick += moveChess;
            form.ShowDialog();
            
        }
        private void moveChess(object sender, MouseEventArgs e) {
            if (moveStart != new Point(0, 0))
            {
                moveEnd = e.Location;
                PrintMoveChess();
                moveStart = new Point(0, 0);
            }
            else
                moveStart = e.Location;
        }
        private string searchingCellToClick(Point loc) {
            string locationW = string.Empty, locationH = string.Empty;
            ABC aBC = new ABC();
            int numeric = 1;
            for (int i = border; i < _heigthMap-border; i += _sizeCeel)
            {                
                for (int j = border; j < _widthMap - border; j += _sizeCeel)
                {                    
                    if(loc.X>=j && loc.X < j+_sizeCeel)
                        locationW = aBC.ToString();
                    aBC++;
                }
                aBC = ABC.A;
                if(loc.Y>=i && loc.Y<i+_sizeCeel)
                    locationH = numeric.ToString();
                numeric++;
            }
            return locationW + locationH;
        }
        private void PrintMoveChess() {
            label1.Text = searchingCellToClick(moveStart)+" - "+ searchingCellToClick(moveEnd);
        }
        private void paint_chessBoard(object sender, PaintEventArgs e) {
            g = e.Graphics;
            int counter = 1;
            ABC aBC = new ABC();
            Brush brush = new SolidBrush(Color.Red);
            int numeric = 1;
            for (int i = border; counter<=64; i+=_sizeCeel)
            {
                g.DrawString(aBC++.ToString(), this.Font, brush, new Point(i+ _sizeCeel/2, 5));
                g.DrawString((numeric++).ToString(), this.Font, brush, new Point(5, i + _sizeCeel / 2));
                for (int j = border; j <_widthMap-border; j += _sizeCeel)
                {
                    
                    cell = new Rectangle(j, i, _sizeCeel, _sizeCeel);
                    if (counter % 2 != 0)
                    {
                        g.FillRectangle(Brushes.AliceBlue, cell);
                    }
                    else {
                        g.FillRectangle(Brushes.Gray, cell);
                    }
                    counter++;
                  
                }
                counter++;
            }        
        }
        private void b_board_Click(object sender, EventArgs e)
        {
            createForm();   
        }
    }
}
