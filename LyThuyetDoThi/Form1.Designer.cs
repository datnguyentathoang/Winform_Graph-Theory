namespace LyThuyetDoThi
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            graphPanel = new Panel();
            btnKruskal = new Button();
            btnDijkstra = new Button();
            btnPrim = new Button();
            txtResult = new TextBox();
            btnAddVertex = new Button();
            btnAddEdge = new Button();
            btnDeleteVertex = new Button();
            btnDeleteEdge = new Button();
            btnReset = new Button();
            SuspendLayout();
            // 
            // graphPanel
            // 
            graphPanel.BorderStyle = BorderStyle.Fixed3D;
            graphPanel.Location = new Point(173, 13);
            graphPanel.Name = "graphPanel";
            graphPanel.Size = new Size(928, 436);
            graphPanel.TabIndex = 0;
            graphPanel.Paint += graphPanel_Paint;
            graphPanel.MouseClick += graphPanel_MouseClick;
            // 
            // btnKruskal
            // 
            btnKruskal.AutoSize = true;
            btnKruskal.Cursor = Cursors.Hand;
            btnKruskal.Location = new Point(691, 489);
            btnKruskal.Name = "btnKruskal";
            btnKruskal.Size = new Size(100, 30);
            btnKruskal.TabIndex = 1;
            btnKruskal.Text = "Kruskal";
            btnKruskal.UseVisualStyleBackColor = true;
            btnKruskal.Click += btnKruskal_Click;
            // 
            // btnDijkstra
            // 
            btnDijkstra.Cursor = Cursors.Hand;
            btnDijkstra.Location = new Point(986, 489);
            btnDijkstra.Name = "btnDijkstra";
            btnDijkstra.Size = new Size(100, 30);
            btnDijkstra.TabIndex = 2;
            btnDijkstra.Text = "Dijsktra";
            btnDijkstra.UseVisualStyleBackColor = true;
            btnDijkstra.Click += btnDijkstra_Click;
            // 
            // btnPrim
            // 
            btnPrim.Cursor = Cursors.Hand;
            btnPrim.Location = new Point(836, 489);
            btnPrim.Name = "btnPrim";
            btnPrim.Size = new Size(100, 30);
            btnPrim.TabIndex = 3;
            btnPrim.Text = "Prim";
            btnPrim.UseVisualStyleBackColor = true;
            btnPrim.Click += btnPrim_Click;
            // 
            // txtResult
            // 
            txtResult.BackColor = SystemColors.ActiveBorder;
            txtResult.Location = new Point(173, 463);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.Size = new Size(500, 100);
            txtResult.TabIndex = 4;
            // 
            // btnAddVertex
            // 
            btnAddVertex.Location = new Point(31, 64);
            btnAddVertex.Name = "btnAddVertex";
            btnAddVertex.Size = new Size(100, 30);
            btnAddVertex.TabIndex = 5;
            btnAddVertex.Text = "Thêm đỉnh";
            btnAddVertex.UseVisualStyleBackColor = true;
            btnAddVertex.Click += btnAddVertex_Click;
            // 
            // btnAddEdge
            // 
            btnAddEdge.Location = new Point(31, 122);
            btnAddEdge.Name = "btnAddEdge";
            btnAddEdge.Size = new Size(100, 30);
            btnAddEdge.TabIndex = 6;
            btnAddEdge.Text = "Thêm cạnh";
            btnAddEdge.UseVisualStyleBackColor = true;
            btnAddEdge.Click += btnAddEdge_Click;
            // 
            // btnDeleteVertex
            // 
            btnDeleteVertex.Location = new Point(31, 187);
            btnDeleteVertex.Name = "btnDeleteVertex";
            btnDeleteVertex.Size = new Size(100, 30);
            btnDeleteVertex.TabIndex = 7;
            btnDeleteVertex.Text = "Xóa đỉnh";
            btnDeleteVertex.UseVisualStyleBackColor = true;
            btnDeleteVertex.Click += btnDeleteVertex_Click;
            // 
            // btnDeleteEdge
            // 
            btnDeleteEdge.Location = new Point(31, 248);
            btnDeleteEdge.Name = "btnDeleteEdge";
            btnDeleteEdge.Size = new Size(100, 30);
            btnDeleteEdge.TabIndex = 8;
            btnDeleteEdge.Text = "Xóa cạnh";
            btnDeleteEdge.UseVisualStyleBackColor = true;
            btnDeleteEdge.Click += btnDeleteEdge_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(31, 320);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(100, 30);
            btnReset.TabIndex = 9;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1113, 575);
            Controls.Add(btnReset);
            Controls.Add(btnDeleteEdge);
            Controls.Add(btnDeleteVertex);
            Controls.Add(btnAddEdge);
            Controls.Add(btnAddVertex);
            Controls.Add(txtResult);
            Controls.Add(btnPrim);
            Controls.Add(btnDijkstra);
            Controls.Add(btnKruskal);
            Controls.Add(graphPanel);
            Name = "Form1";
            Text = "Graph Algorithms - design by DatAndKhanh";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel graphPanel;
        private Button btnKruskal;
        private Button btnDijkstra;
        private Button btnPrim;
        private TextBox txtResult;
        private Button btnAddVertex;
        private Button btnAddEdge;
        private Button btnDeleteVertex;
        private Button btnDeleteEdge;
        private Button btnReset;
    }
}
