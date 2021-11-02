using System;
using System.Drawing;
using System.Windows.Forms;

namespace Multi_File_Copy
{
    public class RichTextAppendArguments
    {
        public string Message;
        public bool AppendCrLf = true;
        public static bool ScrollToCaret = true;
        public static Color DefaultColorBackground = Color.White;
        public static Color DefaultColorForeground = Color.Black;
        public Color ColorBackground = Color.White;
        public Color ColorForeground = Color.Black;

        /// <summary> White background and black text.</summary>
        /// <param name="message"></param>
        /// <param name="appendCrLf"></param>
        public RichTextAppendArguments(string message, bool appendCrLf)
        {
            Message = message;
            AppendCrLf = appendCrLf;
        }

        public RichTextAppendArguments(string message, bool appendCrLf, Color backGround)
        {
            Message = message;
            AppendCrLf = appendCrLf;

            if (backGround.IsEmpty)
                ColorBackground = DefaultColorBackground;
            else
                ColorBackground = backGround;

            ColorForeground = DefaultColorForeground;
        }

        public RichTextAppendArguments(string message, bool appendCrLf, Color backGround, Color foreGround)
        {
            Message = message;
            AppendCrLf = appendCrLf;

            if (backGround.IsEmpty)
                ColorBackground = DefaultColorBackground;
            else
                ColorBackground = backGround;

            if (foreGround.IsEmpty)
                ColorForeground = DefaultColorForeground;
            else
                DefaultColorForeground = foreGround;
        }
    }

    public static class RichTextBoxExtensions
    {
        /// <summary>
        /// append text with specified background color,
        /// and then scroll to caret.
        /// </summary>
        /// <param name="box"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public static void AppendText(this RichTextBox box, string text, Color bgColor)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = bgColor;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
            box.ScrollToCaret();
        }
        public static void AppendTextLine(this RichTextBox box, string text, Color bgColor)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionBackColor = bgColor;
            box.AppendText(text + "\r\n");
            box.SelectionBackColor = box.BackColor;
            box.ScrollToCaret();
        }
        public static void AppendText(this RichTextBox box, RichTextAppendArguments args)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = args.ColorForeground;
            box.SelectionBackColor = args.ColorBackground;
            if (args.AppendCrLf)
                box.AppendText(args.Message + "\r\n");
            else
                box.AppendText(args.Message);

            box.SelectionColor = box.ForeColor;
            if (RichTextAppendArguments.ScrollToCaret)
                box.ScrollToCaret();
        }

        public class MyToolStripMenuItem<T> : ToolStripMenuItem
        {
            public delegate void MyClickEventHandler(T mySender);
            public event MyClickEventHandler MyClick;

            public T mySenderRef;

            public MyToolStripMenuItem(string text, T mySender)
            {
                this.Text = text;
                mySenderRef = mySender;
            }

            public MyToolStripMenuItem(string text, MyClickEventHandler onMyClick, T mySender)
            {
                this.Text = text;
                if (onMyClick != null)
                {
                    MyClick = onMyClick;
                }
                mySenderRef = mySender;
            }
            protected override void OnClick(EventArgs e)
            {
                if (MyClick != null) MyClick(mySenderRef);
                base.OnClick(e);
            }
        }

        public static void RichText_CMS_Copy(RichTextBox rtb) { rtb.Copy(); }
        public static void RichText_CMS_Paste(RichTextBox rtb) { rtb.Paste(); }
        public static void RichText_CMS_Clear(RichTextBox rtb) { rtb.SelectedText = ""; }
        public static void RichText_CMS_ClearAll(RichTextBox rtb) { rtb.SaveToDateTimeNamedFile("log_"); rtb.Clear(); }

        public static void RichText_CMS_Test(RichTextBox rtb) { rtb.AppendText("hello world\r\n"); }

        public static void SetStandardRightClickMenu(this RichTextBox thisRtxt)
        {
            if (thisRtxt.ContextMenuStrip != null) return; // we only set this once

            System.Windows.Forms.ContextMenuStrip cms_rtxtBox = new System.Windows.Forms.ContextMenuStrip();

            cms_rtxtBox.Items.Add(new MyToolStripMenuItem<RichTextBox>("print something", RichText_CMS_Test, thisRtxt));

            cms_rtxtBox.Items.Add<RichTextBox>("Copy", RichText_CMS_Copy, thisRtxt);
            cms_rtxtBox.Items.Add<RichTextBox>("Paste", RichText_CMS_Paste, thisRtxt);
            cms_rtxtBox.Items.Add<RichTextBox>("Clear", RichText_CMS_Clear, thisRtxt);
            cms_rtxtBox.Items.Add("-");
            cms_rtxtBox.Items.Add<RichTextBox>("Clear All", RichText_CMS_ClearAll, thisRtxt);
            thisRtxt.ContextMenuStrip = cms_rtxtBox;
            
        }

        public static void Add<T>(this ToolStripItemCollection thisTsic, string text, MyToolStripMenuItem<T>.MyClickEventHandler onMyClick, T mySender)
        {
            thisTsic.Add(new MyToolStripMenuItem<T>(text, onMyClick, mySender));
        }

        public static void SaveToDateTimeNamedFile(this RichTextBox thisRtxt, string fileNameHeader)
        {
            string filePath = System.IO.Directory.GetCurrentDirectory() + "\\" + fileNameHeader + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            if (System.IO.File.Exists(filePath + ".rtf")) // if we accidentally click to quick, or the mouse glitches
                filePath += "-" + DateTime.Now.Millisecond.ToString("D3"); // we add millisecond so that the previous log dont get overwritten.

            thisRtxt.SaveFile(filePath + ".rtf", RichTextBoxStreamType.RichText);
        }
    }
}
