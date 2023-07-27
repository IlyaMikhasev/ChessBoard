using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
        Form form;

        ComboBox comboBox = new ComboBox();
        public Form1()
        {
            InitializeComponent();
        }
        private void createForm() {
            form = new Form();
            form.Size = new Size(_widthMap, _heigthMap);
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.Paint += paint_chessBoard;
            form.MouseClick += moveChess;
            form.ShowDialog();
        }
        private void moveChess(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left)
            {
                if (moveStart != new Point(0, 0))
                {
                    moveEnd = e.Location;
                    PrintMoveChess();
                    moveStart = new Point(0, 0);
                }
                else
                    moveStart = e.Location;
            }
            else { AddFigure(e.Location); }
        }
        private void AddFigure(Point mouse) {
            comboBox.Location = new Point(mouse.X,mouse.Y);
            string[] figures = { "черный квадрат", "белый круг" };
            comboBox.Items.AddRange(figures);
            comboBox.Size = new Size(90, 23);
            comboBox.Visible = true;
            comboBox.SelectedValueChanged += printfigure;
            form.Controls.Add(comboBox);
           
           
        }
        private void printfigure(object sender, EventArgs e) {
            Point mouse = new Point(((ComboBox)sender).Location.X, ((ComboBox)sender).Location.Y);
            searchingCellToClick(ref mouse);
            Rectangle Fi_rect = new Rectangle(mouse.X+5,mouse.Y+5,_sizeCeel-10, _sizeCeel-10);
            switch (comboBox.SelectedItem)
            {
                case "черный квадрат": printRectangle(Fi_rect); break;
                case "белый круг": printElipse(Fi_rect); break;
                default: break;
            }
        }
        private void printElipse(Rectangle rectangle) {
            comboBox.Items.Clear();
            form.Controls.Remove(comboBox);
            g = form.CreateGraphics();
            g.FillEllipse(Brushes.White, rectangle);
        }
        private void printRectangle( Rectangle rectangle)
        {
            comboBox.Items.Clear();
            form.Controls.Remove(comboBox);
            g = form.CreateGraphics();
            g.FillRectangle(Brushes.Black, rectangle);
        }

        private string searchingCellToClick(ref Point loc) {
            string locationW = string.Empty, locationH = string.Empty;
            ABC aBC = new ABC();
            int numeric = 1;
            for (int i = border; i < _heigthMap-border; i += _sizeCeel)
            {
                for (int j = border; j < _widthMap - border; j += _sizeCeel)
                {
                    if (loc.X >= j && loc.X < j + _sizeCeel) { 
                    locationW = aBC.ToString();
                    loc.X = j;
                    }
                    aBC++;
                }
                aBC = ABC.A;
                if (loc.Y >= i && loc.Y < i + _sizeCeel)
                {
                    locationH = numeric.ToString();
                    loc.Y = i;
                }
                numeric++;
            }
            return locationW + locationH;
        }
        private void PrintMoveChess() {
            label1.Text = "Ход: "+searchingCellToClick(ref moveStart)+" - "+ searchingCellToClick( ref moveEnd);
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
            MessageBox.Show("Левая клавиша мыши - сделать ход\nправая клавиша мыши - добавить фигуру");
            createForm();   
        }
    }
}
