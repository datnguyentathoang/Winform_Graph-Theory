using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace LyThuyetDoThi
{
    public partial class Form1 : Form
    {
        private List<Point> vertices = new List<Point>(); // Danh sách tọa độ đỉnh
        private List<(int Src, int Dest, int Weight)> edges = new List<(int, int, int)>(); // Danh sách cạnh
        private int selectedVertex1 = -1, selectedVertex2 = -1; // Đỉnh được chọn để thêm/xóa cạnh
        private bool isAddingVertex = false, isAddingEdge = false, isDeletingVertex = false, isDeletingEdge = false;
        private const int VertexRadius = 10;
        private List<(int Src, int Dest, int Weight)> mstEdges = new List<(int, int, int)>();
        private List<(int Src, int Dest, int Weight)> shortestPathEdges = new List<(int, int, int)>();
        public Form1()
        {
            InitializeComponent();
            graphPanel.Paint += graphPanel_Paint; // Đăng ký sự kiện Paint
            graphPanel.MouseClick += graphPanel_MouseClick; // Đăng ký sự kiện MouseClick

        }
        private void graphPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            // Vẽ tất cả các cạnh (màu đen)
            using (Pen pen = new Pen(Color.Black, 2))
            {
                foreach (var edge in edges)
                {
                    Point p1 = vertices[edge.Src];
                    Point p2 = vertices[edge.Dest];
                    g.DrawLine(pen, p1, p2);
                    Point mid = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                    g.DrawString(edge.Weight.ToString(), Font, Brushes.Red, mid);
                }
            }

            // Vẽ các cạnh trong MST (màu xanh lá cây)
            using (Pen mstPen = new Pen(Color.Green, 3))
            {
                foreach (var edge in mstEdges)
                {
                    Point p1 = vertices[edge.Src];
                    Point p2 = vertices[edge.Dest];
                    g.DrawLine(mstPen, p1, p2);
                    Point mid = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                    g.DrawString(edge.Weight.ToString(), Font, Brushes.Red, mid);
                }
            }

            // Vẽ các cạnh trong đường đi ngắn nhất (màu đỏ)
            using (Pen pathPen = new Pen(Color.Red, 3))
            {
                foreach (var edge in shortestPathEdges)
                {
                    Point p1 = vertices[edge.Src];
                    Point p2 = vertices[edge.Dest];
                    g.DrawLine(pathPen, p1, p2);
                    Point mid = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                    g.DrawString(edge.Weight.ToString(), Font, Brushes.Red, mid);
                }
            }

            // Vẽ các đỉnh
            using (Brush brush = new SolidBrush(Color.Blue))
            {
                for (int i = 0; i < vertices.Count; i++)
                {
                    Point p = vertices[i];
                    g.FillEllipse(brush, p.X - VertexRadius, p.Y - VertexRadius, 2 * VertexRadius, 2 * VertexRadius);
                    g.DrawString(i.ToString(), Font, Brushes.White, p.X - 5, p.Y - 5);
                }
            }
        }
        private async Task UpdateGraphAsync()
        {
            graphPanel.Invalidate();
            await Task.Delay(500); // Tạm dừng 0.5 giây
        }
        private void btnDeleteVertex_Click(object sender, EventArgs e)
        {
            isDeletingVertex = true;
            isAddingVertex = isAddingEdge = isDeletingEdge = false;
            txtResult.Text = "Chọn đỉnh để xóa.";
        }
        private int GetVertexAtPoint(int x, int y)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                Point p = vertices[i];
                if (Math.Sqrt(Math.Pow(p.X - x, 2) + Math.Pow(p.Y - y, 2)) <= VertexRadius)
                    return i;
            }
            return -1;
        }
        private void graphPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            int clickedVertex = GetVertexAtPoint(e.X, e.Y);

            if (isAddingVertex)
            {
                vertices.Add(new Point(e.X, e.Y));
                txtResult.Text = $"Đã thêm đỉnh {vertices.Count - 1}";
                graphPanel.Invalidate();
                isAddingVertex = false;
            }
            else if (isAddingEdge)
            {
                if (clickedVertex != -1)
                {
                    if (selectedVertex1 == -1)
                    {
                        selectedVertex1 = clickedVertex;
                        txtResult.Text = $"Đã chọn đỉnh {selectedVertex1}. Chọn đỉnh thứ hai.";
                    }
                    else if (selectedVertex1 != clickedVertex)
                    {
                        selectedVertex2 = clickedVertex;
                        string weightInput = Microsoft.VisualBasic.Interaction.InputBox("Nhập trọng số:", "Thêm cạnh", "1");
                        if (int.TryParse(weightInput, out int weight))
                        {
                            edges.Add((selectedVertex1, selectedVertex2, weight));
                            txtResult.Text = $"Đã thêm cạnh {selectedVertex1} -- {selectedVertex2} = {weight}";
                            graphPanel.Invalidate();
                        }
                        selectedVertex1 = selectedVertex2 = -1;
                        isAddingEdge = false;
                    }
                }
            }
            else if (isDeletingVertex && clickedVertex != -1)
            {
                vertices.RemoveAt(clickedVertex);
                edges.RemoveAll(edge => edge.Src == clickedVertex || edge.Dest == clickedVertex);
                for (int i = 0; i < edges.Count; i++)
                {
                    var edge = edges[i];
                    if (edge.Src > clickedVertex) edge.Src--;
                    if (edge.Dest > clickedVertex) edge.Dest--;
                    edges[i] = edge;
                }
                txtResult.Text = $"Đã xóa đỉnh {clickedVertex}";
                graphPanel.Invalidate();
                isDeletingVertex = false;
            }
            else if (isDeletingEdge)
            {
                if (clickedVertex != -1)
                {
                    if (selectedVertex1 == -1)
                    {
                        selectedVertex1 = clickedVertex;
                        txtResult.Text = $"Đã chọn đỉnh {selectedVertex1}. Chọn đỉnh thứ hai để xóa cạnh.";
                    }
                    else if (selectedVertex1 != clickedVertex)
                    {
                        selectedVertex2 = clickedVertex;
                        edges.RemoveAll(edge => (edge.Src == selectedVertex1 && edge.Dest == selectedVertex2) ||
                                               (edge.Src == selectedVertex2 && edge.Dest == selectedVertex1));
                        txtResult.Text = $"Đã xóa cạnh giữa {selectedVertex1} và {selectedVertex2}";
                        graphPanel.Invalidate();
                        selectedVertex1 = selectedVertex2 = -1;
                        isDeletingEdge = false;
                    }
                }
            }
        }


        private void btnAddVertex_Click(object sender, EventArgs e)
        {
            isAddingVertex = true;
            isAddingEdge = isDeletingVertex = isDeletingEdge = false;
            txtResult.Text = "Nhấp vào Panel để thêm đỉnh.";
        }

        private void btnAddEdge_Click(object sender, EventArgs e)
        {
            isAddingEdge = true;
            isAddingVertex = isDeletingVertex = isDeletingEdge = false;
            selectedVertex1 = selectedVertex2 = -1;
            txtResult.Text = "Chọn hai đỉnh để thêm cạnh.";
        }

        private void btnDeleteEdge_Click(object sender, EventArgs e)
        {
            isDeletingEdge = true;
            isAddingVertex = isAddingEdge = isDeletingVertex = false;
            selectedVertex1 = selectedVertex2 = -1;
            txtResult.Text = "Chọn hai đỉnh để xóa cạnh.";
        }

        private async void btnKruskal_Click(object sender, EventArgs e)
        {
            if (vertices.Count == 0 || edges.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm đỉnh và cạnh!");
                return;
            }

            int[] parent = new int[vertices.Count];
            for (int i = 0; i < vertices.Count; i++) parent[i] = -1;

            mstEdges.Clear(); // Xóa các cạnh MST cũ
            shortestPathEdges.Clear(); // Xóa các cạnh đường đi ngắn nhất
            edges.Sort((a, b) => a.Weight.CompareTo(b.Weight));

            int Find(int[] p, int i) => p[i] == -1 ? i : Find(p, p[i]);
            void Union(int[] p, int x, int y) => p[x] = y;

            txtResult.Text = "Đang chạy Kruskal...\r\n";
            foreach (var edge in edges)
            {
                int x = Find(parent, edge.Src);
                int y = Find(parent, edge.Dest);
                if (x != y)
                {
                    mstEdges.Add(edge);
                    Union(parent, x, y);
                    txtResult.Text += $"Thêm cạnh: {edge.Src} -- {edge.Dest} = {edge.Weight}\r\n";
                    await UpdateGraphAsync(); // Gọi phương thức này
                }
            }

            txtResult.Text += "Hoàn tất Kruskal!";
        }

        private async void btnPrim_Click(object sender, EventArgs e)
        {
            if (vertices.Count == 0 || edges.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm đỉnh và cạnh!");
                return;
            }

            int[,] graph = new int[vertices.Count, vertices.Count];
            foreach (var edge in edges)
            {
                graph[edge.Src, edge.Dest] = edge.Weight;
                graph[edge.Dest, edge.Src] = edge.Weight;
            }

            int[] parent = new int[vertices.Count];
            int[] key = new int[vertices.Count];
            bool[] mstSet = new bool[vertices.Count];

            for (int i = 0; i < vertices.Count; i++)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }

            key[0] = 0;
            parent[0] = -1;
            mstEdges.Clear(); // Xóa các cạnh MST cũ

            txtResult.Text = "Đang chạy Prim...\r\n";
            for (int count = 0; count < vertices.Count - 1; count++)
            {
                int u = -1, min = int.MaxValue;
                for (int v = 0; v < vertices.Count; v++)
                    if (!mstSet[v] && key[v] < min)
                    {
                        min = key[v];
                        u = v;
                    }

                if (u == -1) break;

                mstSet[u] = true;
                if (parent[u] != -1) // Bỏ qua đỉnh gốc
                {
                    mstEdges.Add((parent[u], u, graph[parent[u], u]));
                    txtResult.Text += $"Thêm cạnh: {parent[u]} -- {u} = {graph[parent[u], u]}\r\n";
                    await UpdateGraphAsync();
                }

                for (int v = 0; v < vertices.Count; v++)
                    if (graph[u, v] != 0 && !mstSet[v] && graph[u, v] < key[v])
                    {
                        parent[v] = u;
                        key[v] = graph[u, v];
                    }
            }

            txtResult.Text += "Hoàn tất Prim!";
        }

        private async void btnDijkstra_Click(object sender, EventArgs e)
        {
            if (vertices.Count == 0 || edges.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm đỉnh và cạnh!");
                return;
            }

            int[,] graph = new int[vertices.Count, vertices.Count];
            foreach (var edge in edges)
            {
                graph[edge.Src, edge.Dest] = edge.Weight;
                graph[edge.Dest, edge.Src] = edge.Weight;
            }

            int[] dist = new int[vertices.Count];
            bool[] sptSet = new bool[vertices.Count];
            int[] parent = new int[vertices.Count]; // Lưu đỉnh cha để tái tạo đường đi
            for (int i = 0; i < vertices.Count; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
                parent[i] = -1;
            }

            dist[0] = 0;
            mstEdges.Clear(); // Xóa các cạnh MST để tránh chồng lấn
            shortestPathEdges.Clear(); // Xóa các cạnh đường đi ngắn nhất

            txtResult.Text = "Đang chạy Dijkstra từ đỉnh 0...\r\n";
            for (int count = 0; count < vertices.Count - 1; count++)
            {
                int u = -1, min = int.MaxValue;
                for (int v = 0; v < vertices.Count; v++)
                    if (!sptSet[v] && dist[v] < min)
                    {
                        min = dist[v];
                        u = v;
                    }

                if (u == -1) break;

                sptSet[u] = true;
                for (int v = 0; v < vertices.Count; v++)
                    if (!sptSet[v] && graph[u, v] != 0 && dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                    {
                        dist[v] = dist[u] + graph[u, v];
                        parent[v] = u; // Cập nhật cha của v
                    }

                // Thêm cạnh vào đường đi ngắn nhất và cập nhật giao diện
                if (parent[u] != -1) // Bỏ qua đỉnh gốc (0)
                {
                    shortestPathEdges.Add((parent[u], u, graph[parent[u], u]));
                    txtResult.Text += $"Cập nhật đường đi tới {u}: {dist[u]}\r\n";
                    await UpdateGraphAsync();
                }
            }

            txtResult.Text += "Hoàn tất Dijkstra!\r\n";
            for (int i = 0; i < vertices.Count; i++)
                txtResult.Text += $"Đỉnh {i}: {dist[i]}\r\n";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            vertices.Clear();
            edges.Clear();
            mstEdges.Clear();
            shortestPathEdges.Clear();

            selectedVertex1 = -1;
            selectedVertex2 = -1;
            isAddingVertex = false;
            isAddingEdge = false;
            isDeletingVertex = false;
            isDeletingEdge = false;

            txtResult.Text = "Đã reset đồ thị!";
            graphPanel.Invalidate();
        }
    }
}
