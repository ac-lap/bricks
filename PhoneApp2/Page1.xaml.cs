using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.Diagnostics;
using System.Windows.Threading;
using System.Threading;
using Microsoft.Phone.Tasks;
using Microsoft.Advertising.Mobile.UI;
namespace PhoneApp2
{
    class cons
    {
        public static int n_r ;
        public static int n_c ;
        public static int n = (n_r + 1) * n_c + (n_c + 1) * n_r;
        public static int n_boxes = n_r * n_c;
        public static int c_width;
        public static int c_height;
        public static int square_width;
        public static int square_height;
        public static int v_square_width;
        public static int v_square_height;
        public static int edge_height=75;
        public static int edge_width=18;
        public static int virtual_width = 30;
        public static int virtual_height = 00;
        public static ImageBrush user = new ImageBrush();
        public static ImageBrush comp = new ImageBrush();
        public static ImageBrush burst = new ImageBrush();
        public static ImageBrush bomb = new ImageBrush();
        public static ImageBrush b_edge = new ImageBrush();
        public static ImageBrush v_edge = new ImageBrush();
        public static ImageBrush tempImageBrush = new ImageBrush();
        public static bool userTurn = true;
        public static int broken = 2;
        public static Point sq_corner;
        public static int num_mines;
        public static int[] score={0,0};            //0-computer
        public static string[] term = { "n_r", "n_c", "n", "n_boxes","s1","s2","num","play","won","tie","per","music","sound","level" };
        public static int num_brick ;
        public static bool multi = false;
        public static bool pause_m = false;
        public static bool music = true;
        public static bool sound = true;
        public static string []pl_name = new string[2];
        public static int nameMaxLen = 10;
        public static int level = 1;
        public static Color colour1 = new Color();
        public static Color colour0 = new Color();
        public static Color[] playerNameColor = new Color[2];
        public static border lastEdge;
        public static bool end = false;
        static cons()
        {
            pl_name[0] = "ANUBHAV";
            pl_name[1] = "PLAYER 1";
            b_edge.ImageSource = new BitmapImage(new Uri("edge_h.png", UriKind.Relative));
            user.ImageSource = new BitmapImage(new Uri("brick_blue.png", UriKind.Relative));
            comp.ImageSource = new BitmapImage(new Uri("brick_red.png", UriKind.Relative));
            burst.ImageSource = new BitmapImage(new Uri("broken_1.png", UriKind.Relative));
            bomb.ImageSource = new BitmapImage(new Uri("burst.png", UriKind.Relative));
            v_edge.ImageSource = new BitmapImage(new Uri("black-painted-brick-wall-texture.jpg", UriKind.Relative));
            tempImageBrush.ImageSource = new BitmapImage(new Uri("edge_v.png", UriKind.Relative));
            colour1.A = 210;
            colour1.B=224;
            colour1.R=194;
            colour1.G=113;
            colour0.A = 225;
            colour0.B = 78;
            colour0.R = 238;
            colour0.G = 78;
            playerNameColor[0] = colour0;
            playerNameColor[1] = colour1;
        }

    }
    struct Player
    {
        public static int User = 1;
        public static int comp = 0;
        public static int currentPlayer = 1;
        public static int Player1 = 1;
        public static int Player2 = 0;
    };

    public class container
    {
        public int index { get; set; }
        public int left { get; set; }
        public int top { get; set; }
        public int[] ar { get; set; }
        public int count { get; set; }
        public bool mark { get; set; }
        public int num_e { get; set; }
        public int o { get; set; }
        public bool mine { get; set; }
        //       public Point corner { get; set; }

        public container()
        {
            ;
        }
        public container(border t)
        {
            index = t.index;
            left = t.left;
            top = t.top;
            o = t.orient;
            count = t.count;
            mark = t.mark;

            ar = new int[2];
            for (int i = 0; i < count; i++)
                ar[i] = t.sq_index[i];
        }
        public border conv_bor()
        {
            border t = new border();

            t.index = index;
            t.left = left;
            t.top = top;
            t.orient = o;
            t.count = count;
            t.mark = mark;

            t.sq_index = new int[2];
            for (int i = 0; i < count; i++)
                t.sq_index[i] = ar[i];
            return t;
        }

        public container(square t)
        {
            index = t.index;
            left = t.left;
            top = t.top;
            o = t.own;
            count = t.count;
            mark = t.mark;
            num_e = t.num_e;
            mine = t.mine;
            //corner = t.corner;

            ar = new int[4];
            for (int i = 0; i < count; i++)
                ar[i] = t.edge[i];
        }
        public square conv_sq()
        {
            square t = new square();

            t.index = index;
            t.left = left;
            t.top = top;
            t.own = o;
            t.count = count;
            t.mark = mark;
            t.num_e = num_e;
            t.mine = mine;
            //        t.corner = corner;

            t.edge = new int[4];
            for (int i = 0; i < count; i++)
                t.edge[i] = ar[i];
            return t;
        }
    }

    public class border
    {
        public int index;
        public int left, top;
        public int orient;

        public int[] sq_index;
        public int count;
        public bool mark;

        public Rectangle rect;
        public Rectangle v_rect;

        public border(int i)
        {
            index = i;
            sq_index=new int[2];
            count = 0;
            mark = false;
        }
        public border()
        {
            ;
        }

        public void add_s(int h,int i)
        {
            orient = h;
            sq_index[count++]=i;
        }

        public void draw(Canvas can)
        {
            Array.Sort(sq_index,0,count);
            if (orient == 1)//vertical
            {
                top = (int)(Page1.brick[sq_index[0]].corner.Y+(cons.edge_width/2));
                if (count == 1)
                {
                    if (sq_index[0] % cons.n_c == 0)
                    {
                        left = (int)(Page1.brick[sq_index[0]].corner.X - (cons.edge_width / 2));
                    }
                    else
                    {
                        left = (int)(Page1.brick[sq_index[0]].corner.X + cons.square_width - (cons.edge_width / 2));
                    }
                }
                else
                {
                    left = (int)(Page1.brick[sq_index[0]].corner.X + cons.square_width - (cons.edge_width / 2));
                }
            }
            else
            {
                left=(int)(Page1.brick[sq_index[0]].corner.X+(cons.edge_width/2));
                if (count == 1)
                {
                    if (index < cons.n_c)
                    {
                        top = (int)(Page1.brick[sq_index[0]].corner.Y - (cons.edge_width / 2));
                    }
                    else
                    {
                        top = (int)(Page1.brick[sq_index[0]].corner.Y + cons.square_height - (cons.edge_width / 2));
                    }
                }
                else
                {
                    top = (int)(Page1.brick[sq_index[0]].corner.Y + cons.square_height - (cons.edge_width / 2));
                }
            }
            rect = new Rectangle();
            v_rect = new Rectangle();

            rect.Margin = new Thickness(left, top, 0, 0);
            int l, t;
            if (orient == 0)
            {
                rect.Height = cons.edge_width;
                rect.Width = cons.edge_height;
                l = left + (cons.edge_height - cons.virtual_height) / 2;
                t = top - (cons.virtual_width - cons.edge_width) / 2;
                v_rect.Height = cons.virtual_width;
                v_rect.Width = cons.virtual_height;
            }
            else
            {
                rect.Height = cons.edge_height;
                rect.Width = cons.edge_width;
                l = left - (cons.edge_height - cons.virtual_height) / 2;
                t = top + (cons.virtual_width - cons.edge_width) / 2;
                v_rect.Width = cons.virtual_width;
                v_rect.Height = cons.virtual_height;
            }
            v_rect.Margin = new Thickness(l, t, 0, 0);
            v_rect.Fill = new SolidColorBrush(Colors.Black);
            v_rect.Opacity = 0.0;
            rect.Fill = new SolidColorBrush(Colors.Gray);

            rect.MouseLeftButtonDown += new MouseButtonEventHandler(clicked);
            v_rect.MouseLeftButtonDown += new MouseButtonEventHandler(clicked);
            can.Children.Add(v_rect);
            can.Children.Add(rect);
        }

        public void load(border l, Canvas can)
        {
            index = l.index;
            left = l.left;
            top = l.top;
            orient = l.orient;
            count = l.count;
            mark = l.mark;

            for (int i = 0; i < count; i++)
                sq_index[i] = l.sq_index[i];

            can.Children.Remove(rect);
            rect = new Rectangle();

            rect.Margin = new Thickness(left, top, 0, 0);

            if (orient == 0)
            {
                rect.Height = cons.edge_width;
                rect.Width = cons.edge_height;
            }
            else
            {
                rect.Height = cons.edge_height;
                rect.Width = cons.edge_width;
            }

            if (mark == true)
            {
                //ImageBrush imgbrush = new ImageBrush();
                //imgbrush.ImageSource = new BitmapImage(new Uri("edge_h.png", UriKind.Relative));

                rect.Fill = cons.b_edge;
            }
            else
            {
                rect.Fill = new SolidColorBrush(Colors.Gray);

            }
            rect.MouseLeftButtonDown += new MouseButtonEventHandler(clicked);
            can.Children.Add(rect);
        }

        public void changeToOriginal()
        {
            rect.Fill = cons.b_edge;
        }
        
        public void change(int player)         //i -(0) computer turn
        {
            if (mark)
            {                
                cons.userTurn = true;
                return;
            }
            if(cons.sound)
                BrickClick.play();
            mark = true;

            //ImageBrush imgbrush = new ImageBrush();
            //imgbrush.ImageSource = new BitmapImage(new Uri("edge_h.png", UriKind.Relative));

           // rect.Fill = cons.b_edge;
            if (cons.lastEdge != null)
            {
                cons.lastEdge.changeToOriginal();
            }
            cons.lastEdge = this;
            rect.Fill = cons.tempImageBrush;
            if (!cons.userTurn)
                return;

            Page1.edge_click(index,player);
        }

        private void clicked(object sender, RoutedEventArgs e)
        {
            if (!cons.userTurn)
                return;
            if (cons.pause_m)
                return;
            change(1);
            
        }
        
    }

    public class square
    {
        public int index;
        public int left, top;

        public int[] edge;
        public int count;
        public int num_e;
        public bool mark;
        public int own;        //1- user   2-computer

        public bool mine;
        public Point corner;
        public Rectangle rect;
        public bool counted;

        public square(int i)
        {
            index = i;
            edge=new int[4];
            num_e = 0;
            mark=false;
            mine=false;
            own = -1;
            count = 0;
        }
        public square()
        {
            ;
        }
        public void setCorner(Point p)
        {
            corner = p;
            left = (int)p.X + cons.edge_width/2 + 1;
            top = (int)p.Y + cons.edge_width/2 + 1;
        }
        public void add_e(int i)
        {
            edge[count++]=i;
        }

        public bool inc_num(Canvas can,int owner)
        {
            if (num_e < 4)
                num_e++;
            else
                return true;

            if (num_e == 4)
            {
                own = owner;
                return draw(can);

            }
            return false;
        }

        public void blast(int owner)
        {
            if (mine == true)
                return;
            if (own == owner)
            {
                own = cons.broken;
                cons.score[owner]--;
                rect.Fill = cons.burst;
            }
        }

        public static void refresh()
        {
            Page1.p.t_c.Text = Convert.ToString(cons.score[0]);
            Page1.p.t_p.Text = Convert.ToString(cons.score[1]);
        }

        public bool draw(Canvas can)
        {
            mark = true;
            cons.num_brick++;
            bool flag = true;
            rect = new Rectangle();
            rect.Margin = new Thickness(left-1, top-1, 0, 0);
            rect.Height = rect.Width = cons.square_width-cons.edge_width;

            if (mine == true)
            {
                rect.Fill = cons.bomb;
                if(cons.sound)
                    MineBlast.play();
                Page1.bl_near(index);
                flag = false;
            }
            else if (own == Player.User)
            {
                if(cons.sound)
                    BlockCapture.play();
                cons.score[own]++;
                rect.Fill = cons.user;
            }
            else if (own == Player.comp)
            {
                if (cons.sound)
                    BlockCapture.play();
                cons.score[own]++;
                rect.Fill = cons.comp;
            }

            refresh();
            can.Children.Add(rect);

            if (Page1.game_end())
            {
                return false;
            }

            return flag;
        }
        public void load(square l, Canvas can)
        {

            index = l.index;
            left = l.left;
            top = l.top;
            count = l.count;
            num_e = l.num_e;
            mark = l.mark;
            own = l.own;        //1- user   2-computer
            mine = l.mine;
            corner = l.corner;
            rect = l.rect;

            for (int i = 0; i < count; i++)
                edge[i] = l.edge[i];

            can.Children.Remove(rect);
            rect = new Rectangle();
            rect.Margin = new Thickness(left, top, 0, 0);
            rect.Height = rect.Width = cons.square_width - cons.edge_width;

            if (mark == true)
            {
                if (mine == true)
                {
                    rect.Fill = cons.bomb;
                }
                else if (own == cons.broken)
                {
                    rect.Fill = cons.burst;
                }
                else if (own == Player.User)
                {
                    rect.Fill = cons.user;
                }
                else if (own == Player.comp)
                {
                    rect.Fill = cons.comp;
                }
                can.Children.Add(rect);
            }
            refresh();

        }
    }

    public partial class Page1 : PhoneApplicationPage
    {
        static border[] edge;
        public static square[] brick;
        static Canvas can;
        static List<int>[] ar;
        public static Page1 p;
        public bool pause_flag;
        public static Canvas c;
        public static IsolatedStorageSettings save_g = IsolatedStorageSettings.ApplicationSettings;
        public static DispatcherTimer newTimer = new DispatcherTimer();
        public static DispatcherTimer game_T = new DispatcherTimer();

        public Page1()
        {
            
            InitializeComponent();
            if (!cons.multi)
            {
                cons.pl_name[0] = "Phone";
                cons.pl_name[1] = "Player";
            }
            cons.lastEdge = null;
            can = new Canvas();
            ar = new List<int>[4];
            for (int i = 0; i < 4; i++)
                ar[i] = new List<int>();
            p = this;
            cons.userTurn = true;
            cons.pause_m = false;
            pause_flag = false;
            if (cons.multi)
            {
                Player.currentPlayer = 0;
                t_turn.Text = cons.pl_name[Player.currentPlayer] + "'s  Turn";
            }
            else
            {
                p.t_turn.Foreground = new SolidColorBrush(cons.playerNameColor[Player.User]);
                p.t_turn.Text = cons.pl_name[Player.User] + "'s  Turn";
            }
            newTimer.Interval = TimeSpan.FromSeconds(1);
            newTimer.Tick += OnTimerTick;
            init_graphics();

            //AdControl adControl = new AdControl("test_client",   // ApplicationID
            //                       "Image480_80",   // AdUnitID
            //                       true);           // isAutoRefreshEnabled
            //adControl.Width = 480;
            //adControl.Height = 80;

            //Grid grid = (Grid)this.LayoutRoot;
            //grid.Children.Add(adControl);
            //adControl.Margin = new Thickness(0, 720, 0, 0);

        }

        public static void bl_near(int st)
        {
            if (st % cons.n_c != 0)
                brick[st - 1].blast(brick[st].own);
            if (st % cons.n_c != cons.n_c - 1)
                brick[st + 1].blast(brick[st].own);
            if (st / cons.n_c != 0)
                brick[st - cons.n_c].blast(brick[st].own);
            if (st / cons.n_c != cons.n_r - 1)
                brick[st + cons.n_c].blast(brick[st].own);
        }

        public void init_graphics()
        {
            cons.score[0] = cons.score[1] = 0;
            int t_n = cons.n - cons.n_c;
            int i, j,sq=0;
            cons.c_height = (int)ContentPanel.Height;
            cons.c_width = (int)ContentPanel.Width - 16;
            cons.square_height = (cons.c_height - cons.edge_width) / cons.n_r;
            cons.square_width = (cons.c_width - cons.edge_width) / cons.n_c;
            if (cons.square_height < cons.square_width)
                cons.square_width = cons.square_height;
            else cons.square_height = cons.square_width;
            cons.edge_height = cons.square_width - cons.edge_width;

        //virtual

            cons.v_square_height = (cons.c_height - cons.virtual_width) / cons.n_r;
            cons.v_square_width = (cons.c_width - cons.virtual_width) / cons.n_c;
            if (cons.v_square_height < cons.v_square_width)
                cons.v_square_width = cons.v_square_height;
            else cons.v_square_height = cons.v_square_width;
            cons.virtual_height = cons.v_square_width - cons.virtual_width;



            int x = (cons.c_width - (cons.n_c * cons.square_width)) / 2 + 8;
            int y = (cons.c_height - (cons.n_r * cons.square_width)) / 2; ;
            cons.sq_corner = new Point(x, y);
            Debug.WriteLine(cons.sq_corner);
            can.Width = cons.c_width+16;
            can.Height = cons.c_height;
            can.Margin=new Thickness(0,0,0,0);
            can.HorizontalAlignment = HorizontalAlignment.Center;
            can.VerticalAlignment = VerticalAlignment.Center;


            this.ContentPanel.Children.Add(can);

            edge = new border[cons.n];
            brick=new square[cons.n_boxes];
            
            for (i = 0; i < cons.n; i++)
            {
                edge[i] = new border(i);
            }
            for(i=0;i<cons.n_boxes;i++)
            {
                brick[i] = new square(i);
            }
            //int x, y;
            y = (int)cons.sq_corner.Y;
            for (i = 0; i < cons.n_r; i++)
            {
                x = (int)cons.sq_corner.X;
                for (j = 0; j < cons.n_c; j++)
                {
                    brick[i * cons.n_c + j].setCorner(new Point(x, y));
                    x += cons.square_width;
                }
                y += cons.square_height;
            }

            for (i = 0; i < cons.n_c; i++)
            {
                edge[i].add_s(0,i);
                brick[sq++].add_e(i);
            }

            //0=horizontal orientation
            sq = 0;
            int edge_index=0;
            for (i = 0; i < cons.n_r; i++)
            {
                sq = i * cons.n_c;
                for (j = 0; j < cons.n_c + 1; j++)
                {
                    edge_index = cons.n_c * (i + 1) + (cons.n_c+1) * i + j;

                    if (j == 0)
                    {
                        edge[edge_index].add_s(1, sq);
                        brick[sq].add_e(edge_index);
                    }
                    else if (j == cons.n_c) 
                    {
                        edge[edge_index].add_s(1, sq - 1);
                        brick[sq-1].add_e(edge_index);
                    }
                    else
                    {
                        edge[edge_index].add_s(1, sq);
                        brick[sq].add_e(edge_index);
                        edge[edge_index].add_s(1, sq - 1);
                        brick[sq-1].add_e(edge_index);
                    }
                    sq++;
                }
            }
            int squp, sqbelow;
            squp = 0;
            sqbelow = cons.n_c;
            for (i = 1; i < cons.n_r; i++)
            {
                
                for (j = 0; j < cons.n_c; j++)
                {
                    edge_index = cons.n_c * i + (cons.n_c + 1) * i+j;
                    edge[edge_index].add_s(0, squp);
                    brick[squp].add_e(edge_index);
                    edge[edge_index].add_s(0, sqbelow);
                    brick[sqbelow].add_e(edge_index);
                    squp++;
                    sqbelow++;
                }
            }

           // edge_index = 0;
            for (i = edge_index+(cons.n_c+1)+1; i < cons.n; i++)
            {
                edge[i].add_s(0, squp);
                brick[squp++].add_e(i);
            }
            

            for (i = 0; i < cons.n; i++)
                edge[i].draw(can);

            init_mines();
/*            Random rnd = new Random();
            int temp = rnd.Next(cons.n_boxes);
            List<int> l = new List<int>();
            for (i = 0; i < cons.num_mines; i++)
            {
                while (l.Contains(temp))
                {
                    temp = rnd.Next(cons.n_boxes);
                }
                l.Add(temp);
                brick[temp].mine = true;
            }                                   */
        }

        public void init_mines()
        {
            if (App.start_type == false)
                return;
            Random rnd = new Random();
            int []ar=new int[cons.num_mines];
            int temp,ind=0;
            ar[ind++] = rnd.Next(cons.n_boxes);
            brick[ar[0]].mine = true;

            for (int i=1;i<cons.num_mines;i++)
            {
                temp=rnd.Next(cons.n_boxes);

                while(true)
                {
                    for (int j=0;j<ind;j++)
                    {
                        if (Math.Abs(ar[j] - temp) <= 1)
                            goto repeat;
                        if (Math.Abs(temp - ar[j]) == cons.n_c)
                            goto repeat;
                    }
                    goto ok;

                repeat:
                    temp=rnd.Next(cons.n_boxes);
                }

            ok:
                ar[ind++] = temp;
                brick[temp].mine = true;
            }

        }

        internal static void edge_click(int index, int player)
        {
            bool flag = false;
            for (int j = 0; j < edge[index].count; j++)
            {
                //if (cons.multi)
                {
                    if (edge[index].count == 2)
                    {
                        if (brick[edge[index].sq_index[0]].mine && brick[edge[index].sq_index[0]].num_e==3)
                        {
                            brick[edge[index].sq_index[1]].inc_num(can, Player.currentPlayer);
                            brick[edge[index].sq_index[0]].inc_num(can, Player.currentPlayer);
                            flag=false;
                            j=2;
                        }
                        else if (brick[edge[index].sq_index[1]].mine && brick[edge[index].sq_index[1]].num_e==3)
                        {
                            brick[edge[index].sq_index[0]].inc_num(can, Player.currentPlayer);
                            brick[edge[index].sq_index[1]].inc_num(can, Player.currentPlayer);
                            flag = false;
                            j = 2;
                        }
                        else
                        {
                            flag = brick[edge[index].sq_index[1]].inc_num(can, Player.currentPlayer) || flag ;
                            flag = brick[edge[index].sq_index[0]].inc_num(can, Player.currentPlayer) || flag ;
                            j = 2;
                        }
                    }
                    else
                        flag = brick[edge[index].sq_index[j]].inc_num(can, Player.currentPlayer) || flag;
                }
                //else
                    //flag = brick[edge[index].sq_index[j]].inc_num(can, Player.currentPlayer) || flag;
            }

            if (flag)
            {
                cons.userTurn = true;
                return;
            }
            if (cons.multi)
            {
                if (Player.currentPlayer == 1)
                    Player.currentPlayer = 0;
                else
                    Player.currentPlayer = 1;
                p.t_turn.Foreground =new SolidColorBrush(cons.playerNameColor[Player.currentPlayer]);
                p.t_turn.Text = cons.pl_name[Player.currentPlayer] + "'s  Turn";
                return;
            }
            p.t_turn.Foreground = new SolidColorBrush(cons.playerNameColor[Player.comp]);
            p.t_turn.Text = cons.pl_name[Player.comp] + "'s  Turn";
            cons.userTurn = false;
            Player.currentPlayer = Player.comp;
            newTimer.Start();
        }

        void OnTimerTick(Object sender, EventArgs args)
        {

            if (cons.userTurn)
            {
                newTimer.Stop();
                return;
            }

            bool flag = false;
            flag = levelDivider();

            if (flag == false)
            {
                cons.userTurn = true;
                Player.currentPlayer = Player.User;
                p.t_turn.Foreground = new SolidColorBrush(cons.playerNameColor[Player.User]);
                p.t_turn.Text = cons.pl_name[Player.currentPlayer] + "'s  Turn";
                newTimer.Stop();
            }
        }
        private Boolean levelDivider()
        {
            if (cons.level==1)
                return comp_turnEasyAI();
            else if (cons.level==2)
                return comp_turnMediumAI();
            else if (cons.level==3)
                return comp_turnHardAI();
            return false;
        }
         private bool comp_turn()
        {
            int i;
            int count3, count2, count1, count0;
            count0 = 0;
            count1 = 0;
            count2 = 0;
            count3 = 0;
            bool flag = false;
            for (i = 0; i < 4; i++)
                ar[i].Clear();

            for (i = 0; i < cons.n_boxes; i++)
            {
                switch (brick[i].num_e)
                {
                    case 3:
                        ar[3].Add(i);
                        count3++;
                        break;
                    case 2:
                        ar[2].Add(i);
                        count2++;
                        break;
                    case 0:
                        ar[0].Add(i);
                        count0++;
                        break;
                    case 1:
                        ar[1].Add(i);
                        count1++;
                        break;
                }
            }

            int b, e;
            if (count3 != 0)
                flag = true;
            flag = false;
            Random rnd = new Random();
            while (count3 != 0)
            {
                b = ar[3][rnd.Next(count3)];
                ar[3].Remove(b);
                for (i = 0; i < 4; i++)
                {
                    e = brick[b].edge[i];
                    if (edge[e].mark == true)
                        continue;
                    edge[e].change(Player.comp);
                    for (int j = 0; j < edge[e].count; j++)
                    {
                        flag = brick[edge[e].sq_index[j]].inc_num(can, Player.comp) || flag;
                    }
                    break;
                }
                return flag;
            }
            if (flag)
                return true;

            int ind;
            if (count0 != 0)
            {
                ind = rnd.Next(count0);
                for ( ; ; )
                {
                    e = brick[ar[0][ind]].edge[rnd.Next(4)];
                    if (edge[e].mark == true)
                        continue;
                    edge[e].change(Player.comp);
                    for (int j = 0; j < edge[e].count; j++)
                    {
                        brick[edge[e].sq_index[j]].inc_num(can, Player.comp);
                    }
                    return false;
                }
                //return false;
            }
            if (count1 != 0)
            {
                ind = rnd.Next(count1);
                for ( ; ; )
                {
                    e = brick[ar[1][ind]].edge[rnd.Next(4)];
                    if (edge[e].mark == true)
                        continue;
                    edge[e].change(Player.comp);
                    for (int j = 0; j < edge[e].count; j++)
                    {
                        brick[edge[e].sq_index[j]].inc_num(can, Player.comp);
                    }
                    return false;
                }
                return false;
            }
            if (count2 != 0)
            {
                ind = rnd.Next(count2);
                for ( ; ; )
                {
                    e = brick[ar[2][ind]].edge[rnd.Next(4)];
                    if (edge[e].mark == true)
                        continue;
                    edge[e].change(Player.comp);
                    for (int j = 0; j < edge[e].count; j++)
                    {
                        brick[edge[e].sq_index[j]].inc_num(can, Player.comp);
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

         private bool comp_turnMediumAI()
         {
             int i,j;
             int count3, count2, count1, count0;
             count0 = 0;
             count1 = 0;
             count2 = 0;
             count3 = 0;
             bool flag = false;
             for (i = 0; i < 4; i++)
                 ar[i].Clear();

             for (i = 0; i < cons.n_boxes; i++)
             {
                 switch (brick[i].num_e)
                 {
                     case 3:
                         ar[3].Add(i);
                         count3++;
                         break;
                     case 2:
                         ar[2].Add(i);
                         count2++;
                         break;
                     case 0:
                         ar[0].Add(i);
                         count0++;
                         break;
                     case 1:
                         ar[1].Add(i);
                         count1++;
                         break;
                 }
             }

             int b, e;
             if (count3 != 0)
                 flag = true;
             flag = false;
             Random rnd = new Random();
             while (count3 != 0)
             {
                 b = ar[3][rnd.Next(count3)];
                 ar[3].Remove(b);
                 for (i = 0; i < 4; i++)
                 {
                     e = brick[b].edge[i];
                     if (edge[e].mark == true)
                         continue;
                     edge[e].change(Player.comp);
                     /*for (j = 0; j < edge[e].count; j++)
                     {
                         flag = brick[edge[e].sq_index[j]].inc_num(can, Player.comp) || flag;
                     }*/
                     flag = mineAndBrickCheck(e);
                     break;
                 }
                 return flag;
             }
             if (flag)
                 return true;

             int ind,edge_count,edge_index,maxBrickEdges=4,playing_edge=0;
             int[] edge_check={0,0,0,0};
             if (count0 != 0)
             {
                 ind = rnd.Next(count0);
                 edge_count = 0;
                 for (; edge_count<4; )
                 {
                     edge_index = rnd.Next(4);
                     if (edge_check[edge_index] == 1)
                         continue;
                     edge_check[edge_index]=1;
                     edge_count++;
                     e = brick[ar[0][ind]].edge[edge_index];
                     for (j = 0; j < edge[e].count; j++)
                     {
                         if (edge[e].sq_index[j]!=ar[0][ind] && brick[edge[e].sq_index[j]].num_e < maxBrickEdges)
                         {
                             maxBrickEdges = brick[edge[e].sq_index[j]].num_e;
                             playing_edge = e;
                         }
                     }
                 }
                 e=playing_edge;
                 edge[e].change(Player.comp);
                 for ( j = 0; j < edge[e].count; j++)
                 {
                     brick[edge[e].sq_index[j]].inc_num(can, Player.comp);
                 }
                 return false;
             }
             
             if (count1 != 0)
             {
                 ind = rnd.Next(count1);
                 for (; ; )
                 {
                     e = brick[ar[1][ind]].edge[rnd.Next(4)];
                     if (edge[e].mark == true)
                         continue;
                     edge[e].change(Player.comp);
                     for ( j = 0; j < edge[e].count; j++)
                     {
                         brick[edge[e].sq_index[j]].inc_num(can, Player.comp);
                     }
                     return false;
                 }
                 return false;
             }
             if (count2 != 0)
             {
                 ind = rnd.Next(count2);
                 for (; ; )
                 {
                     e = brick[ar[2][ind]].edge[rnd.Next(4)];
                     if (edge[e].mark == true)
                         continue;
                     edge[e].change(Player.comp);
                     for ( j = 0; j < edge[e].count; j++)
                     {
                         brick[edge[e].sq_index[j]].inc_num(can, Player.comp);
                     }
                     return false;
                 }
                 return false;
             }
             return false;
         }
        
        void end_pop(Object sender, EventArgs args)
        {
            game_T.Stop();
            cons.end = true;
            p.g_result.Visibility = Visibility.Visible;
            p.ContentPanel.Visibility = Visibility.Collapsed;
            ImageBrush img1 = new ImageBrush();
            string temp;

            if (cons.score[0] > cons.score[1])
            {
                img1.ImageSource = new BitmapImage(new Uri("red_w.png", UriKind.Relative));

                if (cons.multi == true)
                    temp = cons.pl_name[0]+" WINS !!";
                else
                    temp = "Phone WINS !!";
            }
            else if (cons.score[0] < cons.score[1])
            {
                img1.ImageSource = new BitmapImage(new Uri("blue_w.png", UriKind.Relative));

                if (cons.multi == true)
                    temp = cons.pl_name[1]+" WINS !!";
                else
                    temp = "You WIN !!";
            }
            else
            {
                temp = "Its a draw !!";
            }
            p.g_result.Background = img1;
            p.tb1.Text = temp;


        }

        public static bool game_end()
        {
            if (cons.num_brick == cons.n_boxes)
            {
                game_T.Interval = TimeSpan.FromSeconds(1);
                game_T.Tick += p.end_pop;
                p.t_turn.Visibility = Visibility.Collapsed;
                if (cons.multi == true)
                {
                    game_T.Start();
                    return true;
                }

                if (save_g.Contains(cons.term[7]))
                {
                    save_g[cons.term[7]] = (double)save_g[cons.term[7]] + 1;

                    if (cons.score[0] < cons.score[1])
                    {
                        save_g[cons.term[8]] = (double)save_g[cons.term[8]] + 1;
                    }
                    else if (cons.score[0] == cons.score[1])
                    {
                        save_g[cons.term[9]] = (int)save_g[cons.term[9]] + 1;
                    }

                    save_g[cons.term[10]] = ((double)save_g[cons.term[8]] / (double)save_g[cons.term[7]]) * 100.0;
                }
                else
                {
                    save_g.Add(cons.term[7], 1.0);
                    if (cons.score[0] < cons.score[1])
                    {
                        save_g.Add(cons.term[8], 1.0);
                    }
                    else
                        save_g.Add(cons.term[8], 0.0);
                    if (cons.score[0] == cons.score[1])
                    {
                        save_g.Add(cons.term[9], 1);
                    }
                    else
                        save_g.Add(cons.term[9], 0);

                    save_g.Add(cons.term[10], ((double)save_g[cons.term[8]] / (double)save_g[cons.term[7]]) * 100.0);
                }
                save_g.Save();
                game_T.Start();
                return true;
            }
            else
                return false;
        }

        private void load_game()
        {
            string t = "";

            if (Page1.save_g.Contains(cons.term[4]))
                cons.score[0] = (int)Page1.save_g[cons.term[4]];

            if (Page1.save_g.Contains(cons.term[5]))
                cons.score[1] = (int)Page1.save_g[cons.term[5]];

            if (Page1.save_g.Contains(cons.term[6]))
                cons.num_brick = (int)Page1.save_g[cons.term[6]];

            square.refresh();

            for (int i = 0; i < cons.n; i++)
            {
                t = Convert.ToString(i);
                if (save_g.Contains(t))
                {
                    container temp = (container)save_g[t];
                    edge[i].load(temp.conv_bor(), can);
                }
            }

            for (int i = 0; i < cons.n_boxes; i++)
            {
                t = Convert.ToString(i + cons.n);
                if (save_g.Contains(t))
                {
                    container temp=(container)save_g[t];
                    brick[i].load(temp.conv_sq(),can);
                }
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            cons.end = false;
            BackgroundMusic.pause();
            if(cons.music)
                GameMusic.play();
            newTimer = new DispatcherTimer();
            newTimer.Interval = TimeSpan.FromSeconds(1);
            newTimer.Tick += OnTimerTick;
            if (App.start_type == false)
            {
                load_game();
                App.start_type = true;
            }
            else
                cons.num_brick = 0;

        }
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (cons.music)
            {
                GameMusic.stop();
                BackgroundMusic.play();
            }
            base.OnNavigatedFrom(e);
        }
        private void save()
        {
            if (save_g.Contains(cons.term[0]))
            {
                save_g[cons.term[0]] = cons.n_r;
                save_g[cons.term[1]] = cons.n_c;
                save_g[cons.term[2]] = cons.n;
                save_g[cons.term[3]] = cons.n_boxes;
                save_g[cons.term[4]] = cons.score[0];
                save_g[cons.term[5]] = cons.score[1];
                save_g[cons.term[6]] = cons.num_brick;
            }
            else
            {
                save_g.Clear();

                save_g.Add(cons.term[0], cons.n_r);
                save_g.Add(cons.term[1], cons.n_c);
                save_g.Add(cons.term[2], cons.n);
                save_g.Add(cons.term[3], cons.n_boxes);
                save_g.Add(cons.term[4], cons.score[0]);
                save_g.Add(cons.term[5], cons.score[1]);
                save_g.Add(cons.term[6], cons.num_brick);
            }

            string t = "";
            container c;

            for (int i = 0; i < cons.n; i++)
            {
                t = Convert.ToString(i);
                c = new container(edge[i]);
                if (save_g.Contains(t))
                    save_g[t]=c;
                else
                    save_g.Add(t, c);
            }

            for (int i = 0; i < cons.n_boxes; i++)
            {
                t = Convert.ToString(i + cons.n);
                c = new container(brick[i]);

                if (save_g.Contains(t))
                    save_g[t] = c;
                else
                    save_g.Add(t, c);
            }
            save_g.Save();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            if (cons.end == false)
            {
                game_pause();
            }
            else
            {
                NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.Relative));
            }
        }

        public void game_pause()
        {
            if (pause_flag == true)
            {
                this.pop_up.Children.Remove(c);
                this.pop_up.Visibility = Visibility.Collapsed;
                cons.pause_m = false;
                pause_flag = false;
                t_save.Text = "";
            }
            else
            {
                this.pop_up.Visibility = Visibility.Visible;
                cons.pause_m = true;

                int a2, a3;

                c = new Canvas();
                Rectangle r1 = new Rectangle();
                Rectangle r2 = new Rectangle();
                Rectangle r3 = new Rectangle();

                r1.Height = r2.Height = r3.Height = 80;
                r1.Width = r2.Width = r3.Width = pop_up.Width;

                if (cons.multi == true)
                {
                    a2 = 130;
                    a3 = 280;
                }
                else
                {
                    ImageBrush img1 = new ImageBrush();
                    img1.ImageSource = new BitmapImage(new Uri("save.png", UriKind.Relative));
                    r1.Fill = img1;
                    r1.Margin = new Thickness(0, 90, 0, 0);
                    r1.MouseLeftButtonDown += new MouseButtonEventHandler(p_save);
                    c.Children.Add(r1);

                    a2 = 190;
                    a3 = 290;
                }

                ImageBrush img2 = new ImageBrush();
                img2.ImageSource = new BitmapImage(new Uri("reset.png", UriKind.Relative));
                r2.Fill = img2;
                r2.Margin = new Thickness(0, a2, 0, 0);
                r2.MouseLeftButtonDown += new MouseButtonEventHandler(p_reset);

                ImageBrush img3 = new ImageBrush();
                img3.ImageSource = new BitmapImage(new Uri("main_m.png", UriKind.Relative));
                r3.Fill = img3;
                r3.Margin = new Thickness(0, a3, 0, 0);
                r3.MouseLeftButtonDown += new MouseButtonEventHandler(p_main);

                c.Children.Add(r2);
                c.Children.Add(r3);
                this.pop_up.Children.Add(c);
                pause_flag = true;
            }
        }

        private void p_save(object sender, RoutedEventArgs e)
        {
            save();
            t_save.Text = "SAVED!";
        }
        private void p_reset(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page5.xaml", UriKind.Relative));
        }
        private void p_main(object sender, RoutedEventArgs e)
        {
            while (NavigationService.CanGoBack)
                NavigationService.RemoveBackEntry();

            NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.Relative));
        }

        int count2squares;
        private bool comp_turnHardAI()
        {
            int i, j;
            int count3, count2, count1, count0;
            count0 = 0;
            count1 = 0;
            count2 = 0;
            count3 = 0;
            bool flag = false;
            for (i = 0; i < 4; i++)
                ar[i].Clear();

            for (i = 0; i < cons.n_boxes; i++)
            {
                switch (brick[i].num_e)
                {
                    case 3:
                        ar[3].Add(i);
                        count3++;
                        break;
                    case 2:
                        ar[2].Add(i);
                        brick[i].counted = false;
                        count2++;
                        break;
                    case 0:
                        ar[0].Add(i);
                        count0++;
                        break;
                    case 1:
                        ar[1].Add(i);
                        count1++;
                        break;
                }
            }

            int b, e;
            if (count3 != 0)
                flag = true;
            flag = false;
            Random rnd = new Random();
            while (count3 != 0)
            {
                b = ar[3][rnd.Next(count3)];
                ar[3].Remove(b);
                for (i = 0; i < 4; i++)
                {
                    e = brick[b].edge[i];
                    if (edge[e].mark == true)
                        continue;
                    edge[e].change(Player.comp);
                    /*for (j = 0; j < edge[e].count; j++)
                    {
                        flag = brick[edge[e].sq_index[j]].inc_num(can, Player.comp) || flag;
                    }*/
                    flag = mineAndBrickCheck(e);
                    break;
                }
                return flag;
            }
            if (flag)
                return true;

            int ind, edge_count, edge_index, maxBrickEdges = 4, playing_edge = 0;
            int[] edge_check = { 0, 0, 0, 0 };
            if (count0 != 0)
            {
                ind = rnd.Next(count0);
                edge_count = 0;
                for (; edge_count < 4; )
                {
                    edge_index = rnd.Next(4);
                    if (edge_check[edge_index] == 1)
                        continue;
                    edge_check[edge_index] = 1;
                    edge_count++;
                    e = brick[ar[0][ind]].edge[edge_index];
                    if (edge[e].count == 1)
                    {
                        maxBrickEdges = 0;
                        playing_edge = e;
                        continue;
                    }
                    for (j = 0; j < edge[e].count; j++)
                    {
                        if (edge[e].sq_index[j] != ar[0][ind] && (brick[edge[e].sq_index[j]].num_e <= maxBrickEdges || brick[edge[e].sq_index[j]].num_e <= 1))
                        {
                            maxBrickEdges = brick[edge[e].sq_index[j]].num_e;
                            playing_edge = e;
                        }
                    }
                }
                e = playing_edge;
                edge[e].change(Player.comp);
                for (j = 0; j < edge[e].count; j++)
                {
                    brick[edge[e].sq_index[j]].inc_num(can, Player.comp);
                }
                return false;
            }
            //----------------------------------------------------------
            Debug.WriteLine("------------------------------------------------");
            if (count1 != 0)
            {
                int[] square_check = new int[count1];
                for (i = 0; i < count1; )
                {

                    ind = rnd.Next(count1);
                    Debug.WriteLine("1 ind="+ind);
                    if (square_check[ind] == 1)
                        continue;
                    i++;
                    square_check[ind] = 1;
                    edge_count = 0;
                    maxBrickEdges = 4;
                    edge_check[0] = 0;
                    edge_check[1] = 0;
                    edge_check[2] = 0;
                    edge_check[3] = 0;
                    for (; edge_count < 3; )
                    {
                        edge_index = rnd.Next(4);
                        Debug.WriteLine("edge ocountlopp");
                        if (edge_check[edge_index] == 1)
                            continue;
                        e = brick[ar[1][ind]].edge[edge_index];
                        if (edge[e].mark)
                            continue;
                        edge_check[edge_index] = 1;
                        edge_count++;
                        //Debug.WriteLine("ind="+ind+"     "+"edge index=" + edge_index);
                        
                        if (edge[e].count == 1)
                        {
                            maxBrickEdges = 0;
                            playing_edge = e;
                            continue;
                        }
                        for (j = 0; j < edge[e].count; j++)
                        {
                            if (edge[e].sq_index[j] != ar[1][ind] && (brick[edge[e].sq_index[j]].num_e <= maxBrickEdges || brick[edge[e].sq_index[j]].num_e <= 1))
                            {
                                maxBrickEdges = brick[edge[e].sq_index[j]].num_e;
                                playing_edge = e;
                            }
                        }
                    }
                    if (maxBrickEdges <= 1)
                    {
                        e = playing_edge;
                        edge[e].change(Player.comp);
                        for (j = 0; j < edge[e].count; j++)
                        {
                            brick[edge[e].sq_index[j]].inc_num(can, Player.comp);
                        }
                        return false;
                    }
                }
                e = playing_edge;
                edge[e].change(Player.comp);
                for (j = 0; j < edge[e].count; j++)
                {
                    brick[edge[e].sq_index[j]].inc_num(can, Player.comp);
                }
                return false;
            }
            Debug.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            if (count2 != 0)
            {
                int playing_square = 0, min = 99 ,count;
                count2squares = 0;
                for ( ; count2squares < count2; )
                {
                    ind = rnd.Next(count2);
                    if (brick[ar[2][ind]].counted)
                        continue;
                    count=getCount(ar[2][ind]);
                    Debug.WriteLine("cont="+count);
                    if (count < min)
                    {
                        min = count;
                        playing_square = ind;
                    }
                }

                ind = playing_square;
                for (; ; )
                {
                    e = brick[ar[2][ind]].edge[rnd.Next(4)];
                    if (edge[e].mark == true)
                        continue;
                    edge[e].change(Player.comp);
                    for (j = 0; j < edge[e].count; j++)
                    {
                        brick[edge[e].sq_index[j]].inc_num(can, Player.comp);
                    }
                    return false;
                }
                return false;
            }
            return false;
        }
        int getCount(int ind)
        {
            int e,count=0,j;
            if (brick[ind].counted) 
                return 0;
            if (brick[ind].num_e < 2)
                return 0;
            brick[ind].counted = true;
            count2squares++;
            for (int i = 0; i < 4; i++)
            {
                e=brick[ind].edge[i];
                if (edge[e].mark == true)
                    continue;
                for (j = 0; j < edge[e].count; j++)
                {
                    if (edge[e].sq_index[j] == ind)
                        continue;
                    count += getCount(edge[e].sq_index[j]);
                }
            }
            return 1+count;
        }

        private void rate(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var rateTask = new MarketplaceReviewTask();
                rateTask.Show();
            }
            catch
            {
                ;
            }
        }

        private bool comp_turnEasyAI()
        {
            int i;
            int count3, count2, count1, count0;
            count0 = 0;
            count1 = 0;
            count2 = 0;
            count3 = 0;
            bool flag = false;
            for (i = 0; i < 4; i++)
                ar[i].Clear();

            for (i = 0; i < cons.n_boxes; i++)
            {
                switch (brick[i].num_e)
                {
                    case 3:
                        ar[3].Add(i);
                        count3++;
                        break;
                    case 2:
                        ar[2].Add(i);
                        count2++;
                        break;
                    case 0:
                        ar[0].Add(i);
                        count0++;
                        break;
                    case 1:
                        ar[1].Add(i);
                        count1++;
                        break;
                }
            }

            int b, e;
            if (count3 != 0)
                flag = true;
            flag = false;
            Random rnd = new Random();
            while (count3 != 0)
            {
                b = ar[3][rnd.Next(count3)];
                ar[3].Remove(b);
                for (i = 0; i < 4; i++)
                {
                    e = brick[b].edge[i];
                    if (edge[e].mark == true)
                        continue;
                    edge[e].change(Player.comp);
                    //for (int j = 0; j < edge[e].count; j++)
                    {
                        //flag = brick[edge[e].sq_index[j]].inc_num(can, Player.comp) || flag;
                    }
                    flag = mineAndBrickCheck(e);
                    break;
                }
                return flag;
            }
            if (flag)
                return true;
            if (getLucky())
            {
                if (count2logic(count2))
                    return false;
            }
            if (count0logic(count0))
                return false;
            if (count1logic(count1))
                return false;
            count2logic(count2);
            return false;
        }

        private Boolean getLucky()
        {
            Random r = new Random();
            if (r.Next(10) < 3)
                return true;
            return false;
        }
        private Boolean count2logic(int count2)
        {
            int ind, e;
            Random rnd = new Random();
            if (count2 != 0)
            {
                ind = rnd.Next(count2);
                for (; ; )
                {
                    e = brick[ar[2][ind]].edge[rnd.Next(4)];
                    if (edge[e].mark == true)
                        continue;
                    edge[e].change(Player.comp);
                    for (int j = 0; j < edge[e].count; j++)
                    {
                        brick[edge[e].sq_index[j]].inc_num(can, Player.comp);
                    }
                    return true;
                }
            }
            return false;
        }
        private Boolean count1logic(int count1)
        {
            int ind, e;
            Random rnd = new Random();
            if (count1 != 0)
            {
                ind = rnd.Next(count1);
                for (; ; )
                {
                    e = brick[ar[1][ind]].edge[rnd.Next(4)];
                    if (edge[e].mark == true)
                        continue;
                    edge[e].change(Player.comp);
                    for (int j = 0; j < edge[e].count; j++)
                    {
                        brick[edge[e].sq_index[j]].inc_num(can, Player.comp);
                    }
                    return true;
                }
            }
            return false;
        }
        private Boolean count0logic(int count0)
        {
            int ind, e;
            Random rnd = new Random();
            if (count0 != 0)
            {
                ind = rnd.Next(count0);
                for (; ; )
                {
                    e = brick[ar[0][ind]].edge[rnd.Next(4)];
                    if (edge[e].mark == true)
                        continue;
                    edge[e].change(Player.comp);
                    for (int j = 0; j < edge[e].count; j++)
                    {
                        brick[edge[e].sq_index[j]].inc_num(can, Player.comp);
                    }
                    return true;
                }
                //return false;
            }
            return false;
        }
        private Boolean mineAndBrickCheck(int index)
        {
            Boolean flag = false;
            if (edge[index].count == 2)
            {
                if (brick[edge[index].sq_index[0]].mine && brick[edge[index].sq_index[0]].num_e == 3)
                {
                    brick[edge[index].sq_index[1]].inc_num(can, Player.currentPlayer);
                    brick[edge[index].sq_index[0]].inc_num(can, Player.currentPlayer);
                    flag = false;
                }
                else if (brick[edge[index].sq_index[1]].mine && brick[edge[index].sq_index[1]].num_e == 3)
                {
                    brick[edge[index].sq_index[0]].inc_num(can, Player.currentPlayer);
                    brick[edge[index].sq_index[1]].inc_num(can, Player.currentPlayer);
                    flag = false;
                }
                else
                {
                    flag = brick[edge[index].sq_index[1]].inc_num(can, Player.currentPlayer) || flag;
                    flag = brick[edge[index].sq_index[0]].inc_num(can, Player.currentPlayer) || flag;
                }
            }
            else
                flag = brick[edge[index].sq_index[0]].inc_num(can, Player.currentPlayer) || flag;
            return flag;
        }
    }
}