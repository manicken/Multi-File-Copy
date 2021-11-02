namespace System.Windows.Forms
{
    class MyDataGridView : DataGridView
    {
        public event EventHandler VerticalScrollbarVisibleChanged;
        public event EventHandler HorizontalScrollbarVisibleChanged;
        public bool PreventUserClick = false;

        public MyDataGridView()
        {
            this.VerticalScrollBar.VisibleChanged += new EventHandler(VerticalScrollBar_VisibleChanged);
            this.HorizontalScrollBar.VisibleChanged += new EventHandler(HorizontalScrollBar_VisibleChanged);
           
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (PreventUserClick) return;
            
            base.OnMouseDown(e);
        }

        public void ScrollToSelectedRow()
        {
            if (this.Rows.Count == 0) return;
            if (this.SelectedRows.Count < 1) return;

            int dgvContentHeight = this.Height - this.ColumnHeadersHeight;

            if (HorizontalScrollBar.Visible)
                dgvContentHeight -= HorizontalScrollBar.Height;

            int visibleRowCount = dgvContentHeight / this.RowTemplate.Height;

            if (this.SelectedRows[0].Index < visibleRowCount)
                this.FirstDisplayedScrollingRowIndex = 0;
            else
                this.FirstDisplayedScrollingRowIndex = this.SelectedRows[0].Index - visibleRowCount + 1;
        }
        public ScrollBar ThisVerticalScrollBar
        {
            get { return this.VerticalScrollBar; }
        }
        public ScrollBar ThisHorizontalScrollBar
        {
            get { return this.HorizontalScrollBar; }
        }

        public bool VerticalScrollbarVisible
        {
            get { return VerticalScrollBar.Visible; }
        }
        public bool HorizontalScrollbarVisible
        {
            get { return HorizontalScrollBar.Visible; }
        }

        private void VerticalScrollBar_VisibleChanged(object sender, EventArgs e)
        {
            if (VerticalScrollbarVisibleChanged != null) VerticalScrollbarVisibleChanged(this, e);
        }

        private void HorizontalScrollBar_VisibleChanged(object sender, EventArgs e)
        {
            if (HorizontalScrollbarVisibleChanged != null) HorizontalScrollbarVisibleChanged(this, e);
        }
    }
}