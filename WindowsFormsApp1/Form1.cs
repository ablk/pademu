using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public struct PathState
    {
        public Point[] path;
        public int size;
        public int combo;
        public int[,] board;
    };

    public partial class Form1 : Form
    {
        private enum STATE { NORMAL, SETDP, AUTO, PLAY };


        private Graphics board_graphics; // for drawing
        private Color[] dp_colors; // colors mapping of droppu
        private STATE state;
        private int setdp_color; // color for SETDP

        private int[,] board;
        private int[,] saved_board;
        private int w, h;
        private int prevx, prevy; // prev position when playing
        private int draw_diameter; // block width of board


        private int max_path_size; // DFS search best path
        private Point[] best_path;
        private int best_path_size;
        private int best_path_combo;
        private int path_thres;
        private double search_time;

        private Random random;


        public Form1()
        {
            InitializeComponent();
            board_graphics = panel1.CreateGraphics();

            dp_colors = new Color[8];
            dp_colors[0] = Color.Red;
            dp_colors[1] = Color.Blue;
            dp_colors[2] = Color.Green;
            dp_colors[3] = Color.Yellow;
            dp_colors[4] = Color.MediumPurple;
            dp_colors[5] = Color.Fuchsia;
            dp_colors[6] = Color.Indigo;
            dp_colors[7] = Color.DimGray;

            comboBox1.Items.Add(4);
            comboBox1.Items.Add(5);
            comboBox1.Items.Add(6);
            comboBox1.Items.Add(7);
            comboBox1.Items.Add(8);

            comboBox1.SelectedItem = 6;


            setdp_color = 0;
            state = STATE.NORMAL;

            random = new Random();



            w = 6;
            h = 5;

            path_thres = 7;
            max_path_size = 32;
            best_path_size = 0;
            best_path = new Point[max_path_size];
            best_path_combo = 0;


            draw_diameter = panel1.Size.Width / w;
            board = new int[h, w];
            saved_board = new int[h, w];
            InitBoard();
            CaculateCombo(board, true, true, false);
            CopyBoard(board, saved_board);

        }
        private void DrawBestPath()
        {
            // black -> white = start -> end
            for (int i = 1; i < best_path_size; i++)
            {
                int x1 = best_path[i - 1].X * draw_diameter + draw_diameter / 2;
                int y1 = best_path[i - 1].Y * draw_diameter + draw_diameter / 2;

                int x2 = best_path[i].X * draw_diameter + draw_diameter / 2;
                int y2 = best_path[i].Y * draw_diameter + draw_diameter / 2;

                int rgb = (int)(255.0f * (float)i / (float)best_path_size);
                Color color = Color.FromArgb(rgb, rgb, rgb);

                board_graphics.DrawLine(new Pen(color,5.0f), x1, y1, x2, y2);
            }

            Thread.Sleep(3000);

            DrawAllBoard();


            //auto move animation
            for (int i = 1; i < best_path_size; i++)
            {
                Point cur = best_path[i - 1];
                Point next = best_path[i];
                for (int d = 0; d <= draw_diameter; d+=5)
                {

                    if (d < draw_diameter / 2)
                    {
                        board_graphics.FillRectangle(
                            new SolidBrush(Color.Black),
                            cur.X * draw_diameter,
                            cur.Y * draw_diameter,
                            draw_diameter, draw_diameter);
                    }
                    else
                    {
                        board_graphics.FillRectangle(
                            new SolidBrush(Color.Black),
                            cur.X * draw_diameter,
                            cur.Y * draw_diameter,
                            draw_diameter, draw_diameter);

                        board_graphics.FillRectangle(
                            new SolidBrush(Color.Black),
                            next.X * draw_diameter,
                            next.Y * draw_diameter,
                            draw_diameter, draw_diameter);

                        board_graphics.FillEllipse(
                            new SolidBrush(dp_colors[board[next.Y, next.X]]),
                            cur.X * draw_diameter + 2,
                            cur.Y * draw_diameter + 2,
                            draw_diameter - 4, draw_diameter - 4);
                    }


                    board_graphics.FillEllipse(
                       new SolidBrush(dp_colors[board[cur.Y, cur.X]]),
                       cur.X * draw_diameter + 2 + (next.X - cur.X) * d,
                       cur.Y * draw_diameter + 2 + (next.Y - cur.Y) * d,
                       draw_diameter - 4, draw_diameter - 4);


                    Thread.Sleep(1);
                }

                SwapBoard(board, cur.Y, cur.X, next.Y, next.X);



            }

        }

        private void FindBestPathBFS()
        {
            best_path_combo = 0;
            best_path_size = 0;

            Queue<PathState> queue = new Queue<PathState>();

            // add start points


            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                {
                    Point cur = new Point(i, j);
                    {
                        Point next = new Point(i, j - 1);
                        if (board_InRange(next.Y, next.X))
                        {
                            PathState ps = new PathState();
                            ps.path = new Point[max_path_size];
                            ps.path[0] = cur;
                            ps.path[1] = next;
                            ps.combo = 0;
                            ps.size = 2;
                            ps.board = new int[h, w];
                            CopyBoard(board, ps.board);
                            SwapBoard(ps.board, next.Y, next.X, cur.Y, cur.X);
                            queue.Enqueue(ps);
                        }

                    }

                    {
                        Point next = new Point(i, j + 1);
                        if (board_InRange(next.Y, next.X))
                        {
                            PathState ps = new PathState();
                            ps.path = new Point[max_path_size];
                            ps.path[0] = cur;
                            ps.path[1] = next;
                            ps.combo = 0;
                            ps.size = 2;
                            ps.board = new int[h, w];
                            CopyBoard(board, ps.board);
                            SwapBoard(ps.board, next.Y, next.X, cur.Y, cur.X);
                            queue.Enqueue(ps);
                        }

                    }

                    {
                        Point next = new Point(i - 1, j);
                        if (board_InRange(next.Y, next.X))
                        {
                            PathState ps = new PathState();
                            ps.path = new Point[max_path_size];
                            ps.path[0] = cur;
                            ps.path[1] = next;
                            ps.combo = 0;
                            ps.size = 2;
                            ps.board = new int[h, w];
                            CopyBoard(board, ps.board);
                            SwapBoard(ps.board, next.Y, next.X, cur.Y, cur.X);
                            queue.Enqueue(ps);
                        }

                    }

                    {
                        Point next = new Point(i + 1, j);
                        if (board_InRange(next.Y, next.X))
                        {
                            PathState ps = new PathState();
                            ps.path = new Point[max_path_size];
                            ps.path[0] = cur;
                            ps.path[1] = next;
                            ps.combo = 0;
                            ps.size = 2;
                            ps.board = new int[h, w];
                            CopyBoard(board, ps.board);
                            SwapBoard(ps.board, next.Y, next.X, cur.Y, cur.X);
                            queue.Enqueue(ps);
                        }

                    }

                }

            //evaluate path and add new path

            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (queue.Count > 0)
            {
                if (sw.Elapsed.TotalSeconds >= 30) {

                    break;
                }

                PathState ps = queue.Dequeue();
                int combo = CaculateCombo(ps.board, false, false, false);
                if (combo < ps.combo) continue;


                ps.combo = combo;
                if (ps.size >= max_path_size / 4 && ps.combo <= path_thres / 4) continue;
                if (ps.size >= max_path_size / 2 && ps.combo <= path_thres / 2) continue;
                if (ps.size >= max_path_size) return;

                if (combo > best_path_combo)
                {
                    for (int i = 0; i < ps.size; i++)
                    {
                        best_path[i] = ps.path[i];
                    }
                    best_path_size = ps.size;
                    best_path_combo = combo;

                    if (best_path_combo >= path_thres) break;
                }
                Point cur = ps.path[ps.size - 1];
                {
                    Point next = new Point(cur.X, cur.Y + 1);
                    if (next != ps.path[ps.size - 2] && board_InRange(next.Y, next.X))
                    {
                        PathState next_ps = new PathState();
                        next_ps.path = new Point[max_path_size];
                        for (int i = 0; i < ps.size; i++)
                        {
                            next_ps.path[i] = ps.path[i];
                        }
                        next_ps.path[ps.size] = next;
                        next_ps.size = ps.size + 1;
                        next_ps.combo = ps.combo;

                        next_ps.board = new int[h, w];
                        CopyBoard(ps.board, next_ps.board);
                        SwapBoard(next_ps.board, next.Y, next.X, cur.Y, cur.X);
                        queue.Enqueue(next_ps);
                    }
                }
                {
                    Point next = new Point(cur.X, cur.Y - 1);
                    if (next != ps.path[ps.size - 2] && board_InRange(next.Y, next.X))
                    {
                        PathState next_ps = new PathState();
                        next_ps.path = new Point[max_path_size];
                        for (int i = 0; i < ps.size; i++)
                        {
                            next_ps.path[i] = ps.path[i];
                        }
                        next_ps.path[ps.size] = next;
                        next_ps.size = ps.size + 1;
                        next_ps.combo = ps.combo;

                        next_ps.board = new int[h, w];
                        CopyBoard(ps.board, next_ps.board);
                        SwapBoard(next_ps.board, next.Y, next.X, cur.Y, cur.X);
                        queue.Enqueue(next_ps);
                    }
                }

                {
                    Point next = new Point(cur.X + 1, cur.Y);
                    if (next != ps.path[ps.size - 2] && board_InRange(next.Y, next.X))
                    {
                        PathState next_ps = new PathState();
                        next_ps.path = new Point[max_path_size];
                        for (int i = 0; i < ps.size; i++)
                        {
                            next_ps.path[i] = ps.path[i];
                        }
                        next_ps.path[ps.size] = next;
                        next_ps.size = ps.size + 1;
                        next_ps.combo = ps.combo;

                        next_ps.board = new int[h, w];
                        CopyBoard(ps.board, next_ps.board);
                        SwapBoard(next_ps.board, next.Y, next.X, cur.Y, cur.X);
                        queue.Enqueue(next_ps);
                    }
                }


                {
                    Point next = new Point(cur.X - 1, cur.Y);
                    if (next != ps.path[ps.size - 2] && board_InRange(next.Y, next.X))
                    {
                        PathState next_ps = new PathState();
                        next_ps.path = new Point[max_path_size];
                        for (int i = 0; i < ps.size; i++)
                        {
                            next_ps.path[i] = ps.path[i];
                        }
                        next_ps.path[ps.size] = next;
                        next_ps.size = ps.size + 1;
                        next_ps.combo = ps.combo;

                        next_ps.board = new int[h, w];
                        CopyBoard(ps.board, next_ps.board);
                        SwapBoard(next_ps.board, next.Y, next.X, cur.Y, cur.X);
                        queue.Enqueue(next_ps);
                    }
                }
            }
            search_time = sw.Elapsed.TotalSeconds;
            sw.Stop();
            queue.Clear();

        }

        private void DrawAllBoard()
        {

            // refresh screen
            board_graphics.Clear(Color.Black);
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    if (board[j, i] == -1)
                    {
                        board_graphics.FillRectangle(
                            new SolidBrush(Color.Black),
                            i * draw_diameter, j * draw_diameter,
                            draw_diameter, draw_diameter);
                    }
                    else
                    {
                        board_graphics.FillEllipse(
                            new SolidBrush(dp_colors[board[j, i]]),
                            i * draw_diameter + 2, j * draw_diameter + 2,
                            draw_diameter - 4, draw_diameter - 4);
                    }

                }
            }
        }

        private void InitBoard()
        {
            // random generate for board

            int ndp = (int)comboBox1.SelectedItem;
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    board[j, i] = random.Next() % ndp;
                }
            }

        }
        private void ReplaceRepeatCombos(int[,] combos, int repeat, int val)
        {
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (combos[j, i] == repeat)
                    {
                        combos[j, i] = val;
                    }
                }
            }
        }
        private void CopyBoard(int[,] a, int[,] b)
        {
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    b[j, i] = a[j, i];
                }
            }

        }
        private void SwapBoard(int[,] a, int j1, int i1, int j2, int i2)
        {
            int tmp = a[j1, i1];
            a[j1, i1] = a[j2, i2];
            a[j2, i2] = tmp;
        }

        private void DoroppuFall(int[,] board, int[,] fall_distance, bool draw)
        {
            // falling animation

            if (!draw)
            {
                for (int i = 0; i < w; i++)
                {
                    for (int j = h - 1; j >= 0; j--)
                    {
                        fall_distance[j, i] = 0;
                    }
                }
                return;
            }

            bool done = false;

            while (!done)
            {
                done = true;
                for (int d = 0; d < draw_diameter; d += 5)
                {
                    for (int i = 0; i < w; i++)
                    {
                        for (int j = h - 1; j >= 0; j--)
                        {
                            if (fall_distance[j, i] > 0 && j >= fall_distance[j, i] - 1)
                            {
                                board_graphics.FillRectangle(
                                    new SolidBrush(Color.Black),
                                    i * draw_diameter,
                                    j * draw_diameter + d - fall_distance[j, i] * draw_diameter,
                                    draw_diameter, draw_diameter);
                                if (board[j, i] >= 0)
                                {
                                    board_graphics.FillEllipse(
                                        new SolidBrush(dp_colors[board[j, i]]),
                                        i * draw_diameter + 2,
                                        j * draw_diameter + 2 + d + 5 - fall_distance[j, i] * draw_diameter,
                                        draw_diameter - 4, draw_diameter - 4);
                                }

                            }

                        }
                    }
                    Thread.Sleep(1);
                }


                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        if (fall_distance[j, i] > 0)
                        {
                            done = false;
                            fall_distance[j, i]--;
                        }
                    }
                }

            }
        }
        private void ClearCombos1(int[,] combos, int id, bool draw)
        {
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (combos[j, i] == id)
                    {
                        combos[j, i] = 0;

                        if (draw)
                        {
                            board_graphics.FillRectangle(
                                    new SolidBrush(Color.Black),
                                    i * draw_diameter,
                                    j * draw_diameter,
                                    draw_diameter, draw_diameter);
                        }

                    }

                }
            }
            if (draw)
            {
                Thread.Sleep(300);
            }
        }
        private int ClearCombos(int[,] combos, bool draw)
        {
            int combo_count = 0;
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {

                    if (combos[j, i] > 0)
                    {
                        combo_count++;
                        int id = combos[j, i];
                        ClearCombos1(combos, id, draw);
                    }
                }
            }
            return combo_count;
        }

        private int CaculateCombo(int[,] target, bool generate_new_doroppu, bool update_target, bool draw)
        {


            int[,] combos = new int[h, w];
            int[,] fall_distance = new int[h, w];
            int[,] copy = new int[h, w];
            int total_combo = 0;
            int combo_id = 1;
            int ndp = (int)comboBox1.SelectedItem;


            CopyBoard(target, copy);
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    combos[j, i] = 0;
                    fall_distance[j, i] = 0;
                }
            }

            while (combo_id > 0)
            {
                combo_id = 0;


                //row
                for (int j = 0; j < h; j++)
                {
                    int length = 1;
                    for (int i = 0; i < w; i++)
                    {
                        if (i == w - 1 || copy[j, i] != copy[j, i + 1])
                        {
                            if (length >= 3)
                            {
                                combo_id++;
                                for (int k = 0; k < length; k++)
                                {
                                    if (combos[j, i - k] > 0)
                                    {
                                        ReplaceRepeatCombos(combos, combos[j, i - k], combo_id);
                                    }
                                    combos[j, i - k] = combo_id;
                                }

                            }
                            length = 1;

                        }
                        else if (copy[j, i] == copy[j, i + 1])
                        {
                            length++;
                        }
                        if (copy[j, i] == -1) length = 1;
                    }
                }

                //col
                for (int i = 0; i < w; i++)
                {
                    int length = 1;
                    for (int j = 0; j < h; j++)
                    {

                        if (j == h - 1 || copy[j, i] != copy[j + 1, i])
                        {
                            if (length >= 3)
                            {
                                combo_id++;
                                for (int k = 0; k < length; k++)
                                {
                                    if (combos[j - k, i] > 0)
                                    {
                                        ReplaceRepeatCombos(combos, combos[j - k, i], combo_id);
                                    }
                                    combos[j - k, i] = combo_id;
                                }

                            }
                            length = 1;

                        }
                        else if (copy[j, i] == copy[j + 1, i])
                        {
                            length++;
                        }
                        if (copy[j, i] == -1) length = 1;
                    }
                }


                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        if (combos[j, i] > 0)
                        {



                            for (int k = j; k >= 1; k--)
                            {
                                fall_distance[k, i] = fall_distance[k - 1, i] + 1;
                                copy[k, i] = copy[k - 1, i];
                            }

                            if (generate_new_doroppu)
                                copy[0, i] = random.Next() % ndp;
                            else
                                copy[0, i] = -1;
                            fall_distance[0, i] += 1;

                        }
                    }
                }

                int combo_count = ClearCombos(combos, draw);



                DoroppuFall(copy, fall_distance, draw);

                total_combo += combo_count;
            }


            if (update_target)
            {
                CopyBoard(copy, target);
            }


            return total_combo;
        }
        private bool board_InRange(int j, int i)
        {
            return !(j < 0 || j >= h || i < 0 || i >= w);
        }

        private bool panel1_InRange(int y, int x)
        {
            return !(y < 0 || y >= panel1.Size.Height || x < 0 || x >= panel1.Size.Width);
        }

        private void buttonRANDOM_Click(object sender, EventArgs e)
        {
            InitBoard();
            CaculateCombo(board, true, true, false);
            DrawAllBoard();
            state = STATE.NORMAL;
            buttonRANDOM.Focus();



        }
        private void panel1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (state == STATE.NORMAL)
            {
                state = STATE.PLAY;
                prevx = e.X;
                prevy = e.Y;
            }
        }
        private void panel1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if (state == STATE.PLAY)
            {
                if (!panel1_InRange(e.Y, e.X))
                {
                    DrawAllBoard();
                    int c = CaculateCombo(board, true, true, true);
                    label3.Text = "Combos:" + c.ToString();
                    state = STATE.NORMAL;
                    return;
                }


                int j = e.Y / draw_diameter;
                int i = e.X / draw_diameter;


                int previ = prevx / draw_diameter;
                int prevj = prevy / draw_diameter;

                if (previ != i || prevj != j)
                {
                    int tmp = board[j, i];
                    board[j, i] = board[prevj, previ];
                    board[prevj, previ] = tmp;
                }

                board_graphics.FillEllipse(
                    new SolidBrush(Color.Black),
                    prevx - draw_diameter / 2 + 2, prevy - draw_diameter / 2 + 2,
                    draw_diameter - 4, draw_diameter - 4);


                for (int ni = i - 1; ni <= i + 1; ni++)
                {
                    for (int nj = j - 1; nj <= j + 1; nj++)
                    {
                        if (!board_InRange(nj, ni)) continue;
                        if (ni != i || nj != j)
                        {
                            board_graphics.FillEllipse(
                                new SolidBrush(dp_colors[board[nj, ni]]),
                                ni * draw_diameter + 2, nj * draw_diameter + 2,
                                draw_diameter - 4, draw_diameter - 4);
                        }

                    }

                }

                board_graphics.FillRectangle(
                    new SolidBrush(Color.Black),
                    i * draw_diameter, j * draw_diameter,
                    draw_diameter, draw_diameter);
                board_graphics.FillEllipse(
                    new SolidBrush(dp_colors[board[j, i]]),
                    e.X - draw_diameter / 2 + 2, e.Y - draw_diameter / 2 + 2,
                    draw_diameter - 4, draw_diameter - 4);

                prevx = e.X;
                prevy = e.Y;
            }
        }


        private void panel1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {


            if (state == STATE.SETDP)
            {
                if (!panel1_InRange(e.Y, e.X))
                {
                    return;
                }

                int j = e.Y / draw_diameter;
                int i = e.X / draw_diameter;
                board[j, i] = setdp_color;

                board_graphics.FillEllipse(
                    new SolidBrush(dp_colors[board[j, i]]),
                    i * draw_diameter + 2, j * draw_diameter + 2,
                    draw_diameter - 4, draw_diameter - 4);
            }

            if (state == STATE.PLAY)
            {
                DrawAllBoard();
                int c = CaculateCombo(board, true, true, true);
                label3.Text = "Combos:" + c.ToString();
                state = STATE.NORMAL;
            }


            //panel1.Invalidate();
        }

        private void buttonSETRED_Click(object sender, EventArgs e)
        {
            if (state == STATE.NORMAL || state == STATE.SETDP)
            {
                state = STATE.SETDP;
                buttonSETRED.Focus();
                setdp_color = 0;

            }

        }

        private void buttonSETBLUE_Click(object sender, EventArgs e)
        {
            if (state == STATE.NORMAL || state == STATE.SETDP)
            {
                state = STATE.SETDP;
                buttonSETBLUE.Focus();
                setdp_color = 1;
            }
        }

        private void buttonSETGREEN_Click(object sender, EventArgs e)
        {
            if (state == STATE.NORMAL || state == STATE.SETDP)
            {
                state = STATE.SETDP;
                buttonSETGREEN.Focus();
                setdp_color = 2;
            }
        }

        private void buttonSETYELLOW_Click(object sender, EventArgs e)
        {
            if (state == STATE.NORMAL || state == STATE.SETDP)
            {
                state = STATE.SETDP;
                buttonSETYELLOW.Focus();
                setdp_color = 3;
            }
        }

        private void buttonSETMPURPLE_Click(object sender, EventArgs e)
        {
            if (state == STATE.NORMAL || state == STATE.SETDP)
            {
                state = STATE.SETDP;
                buttonSETMPURPLE.Focus();
                setdp_color = 4;
            }
        }

        private void buttonSETFUCH_Click(object sender, EventArgs e)
        {
            if (state == STATE.NORMAL || state == STATE.SETDP)
            {
                state = STATE.SETDP;
                buttonSETFUCH.Focus();
                setdp_color = 5;
            }
        }

        private void buttonSETINDIGO_Click(object sender, EventArgs e)
        {
            if (state == STATE.NORMAL || state == STATE.SETDP)
            {
                state = STATE.SETDP;
                buttonSETINDIGO.Focus();
                setdp_color = 6;
            }
        }

        private void buttonSETGRAY_Click(object sender, EventArgs e)
        {
            if (state == STATE.NORMAL || state == STATE.SETDP)
            {
                state = STATE.SETDP;
                buttonSETGRAY.Focus();
                setdp_color = 7;
            }
        }

        private void buttonSAVELOAD_Click(object sender, EventArgs e)
        {
            if (state == STATE.PLAY || state == STATE.AUTO) return;
            if (state == STATE.NORMAL)
            {
                CopyBoard(saved_board, board);
                DrawAllBoard();
            }
            if (state == STATE.SETDP)
            {
                CopyBoard(board, saved_board);
            }

            state = STATE.NORMAL;
            buttonSAVELOAD.Focus();
        }



        private void buttonAUTO_Click(object sender, EventArgs e)
        {

            if (state == STATE.NORMAL)
            {
                state = STATE.AUTO;
                CopyBoard(board, saved_board);
               

                buttonAUTO.Focus();

                label3.Text = "Searching...";
                label3.Update();



                FindBestPathBFS();

                label3.Text = "Found Path with " + best_path_size + "steps, " + best_path_combo.ToString() + "combos in " + search_time + "seconds.";
                label3.Update();

                DrawBestPath();

                CaculateCombo(board, true, true, true);



                state = STATE.NORMAL;
            }

        }
    }
}
