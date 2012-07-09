
#region Imports (using)

using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections;

#endregion

namespace CPrintReportStringDemo
{


	#region public enums

	public enum CharsPerLine {CPL80, CPL96,CPL120,CPL160};
	public enum PrintOrientation {Portrait,Landscape};
	public enum PrintPreview {Print,Preview};
	public enum TitleStyle {Default,Bold,Italic,BoldItalic};
	public enum MarginExtender {None,OneTenthInch,OneQuarterInch,OneHalfInch,ThreeQuarterInch,OneInch};

	#endregion
	public class CPrintReportString
	{
		public  CPrintReportString()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Class Level Variables
		// Print directives allow you to change properites
		// dynamically within the print string.
		// <:CH1>Heading 1
		// <:CH2>Heading 2
		// <:CH3>Heading 3
		// <:CH4>Heading 4
		// <:NEWPAGE>
		// <:NOLINES>
		// <:SUBTITLE>New SubTitle Line
		// <:PAGENBR0>  resets the page count
   
		private string _ColHdr1;
		private string _ColHdr2;
		private string _ColHdr3;
		private string _ColHdr4;
		private string _SubTitle;
		private string _SubTitle2;
		private string _SubTitle3;
		private string _SubTitle4;
		private string _Title;
		private bool _SepLines=false;
		private Single _TitleFontSize;
		private TitleStyle _TitleFontStyle;
		private bool _PrintFooter=true;
		private bool _DrawBox;
		private MarginExtender _LeftMarginExtender;
		private MarginExtender _RightMarginExtender;
		private MarginExtender _TopMarginExtender;
		private MarginExtender _BottomMarginExtender;
		private PrintDocument PrintDoc;
		private string msRptString;
		private int miNL;
		private int mI;
		private int miChrPerLine;
		private int PageNbr;
		private bool Portrait=true;
		private ArrayList ColHdrArrayList=new System.Collections.ArrayList();
		private short ColHdrCount=0;
		private Single DetailFontSize;
		private string sFooter;
		const string DETAIL_FONT="Courier New";
		const int DETAIL_FONT_SIZE_80=10;
		const int DETAIL_FONT_SIZE_96=9;
		const int DETAIL_FONT_SIZE_120=8;
		const int DETAIL_FONT_SIZE_160=6;
		const bool DETAIL_FONT_BOLD=true;
		private TBMemoLine tbm;

		private Single LineHeight;
		private Single LineWidth;
		private Single xPos;
		private Single yPos;
		private Font PrintFont;
		private string sLine;
		private string sHdrLine;
		private Single CharWidth;
		private System.Drawing.SizeF sz;
		private Single TextWidth;
		private string sPageNbr;
		private Single x1;
		private Single y1;
		private Single x2;
		private Single y2;
		private Pen drPen = new Pen(Color.Black,1);
		private Single LeftMargin;
		private Single RightMargin;
		private Single TopMargin;
		private Single BottomMargin;
		#endregion

		#region Public Properties
		public string ColHdr1
		{
			get 
			{
				return _ColHdr1;
			}
			set 
			{
				_ColHdr1 = value;
			}
		}

		public string ColHdr2
		{
			get 
			{
				return _ColHdr2;
			}
			set 
			{
				_ColHdr2 = value;
			}
		}
		public string ColHdr3
		{
			get 
			{
				return _ColHdr3;
			}
			set 
			{
				_ColHdr3 = value;
			}
		}

		public string ColHdr4
		{
			get 
			{
				return _ColHdr4;
			}
			set 
			{
				_ColHdr4 = value;
			}
		}

		public string SubTitle 
		{
			get 
			{
				return _SubTitle;
			}
			set 
			{
				_SubTitle = value;
			}
		}

		public string SubTitle2
		{
			get 
			{
				return _SubTitle2;
			}
			set 
			{
				_SubTitle2 = value;
			}
		}

		public string SubTitle3
		{
			get 
			{
				return _SubTitle3;
			}
			set 
			{
				_SubTitle3 = value;
			}
		}

		public string SubTitle4
		{
			get 
			{
				return _SubTitle4;
			}
			set 
			{
				_SubTitle4 = value;
			}
		}

		public string Title
		{
			get 
			{
				return _Title;
			}
			set 
			{
				_Title = value;
			}
		}

		public  bool SepLines
		{
			get 
			{
				return _SepLines;
			}
			set 
			{
				_SepLines = value;
			}
		}

		public Single TitleFontSize 
		{
			get 
			{
				return _TitleFontSize;
			}
			set 
			{
				_TitleFontSize = value;
			}
		}

		public TitleStyle TitleFontStyle 
		{
			get 
			{
				return _TitleFontStyle;
			}
			set 
			{
				_TitleFontStyle = value;
			}
		}

		public bool PrintFooter 
		{
			get 
			{
				return _PrintFooter;
			}
			set 
			{
				_PrintFooter = value;
			}
		}

        public string Footer
        {
            get { return this.sFooter; }
            set { this.sFooter = value; }
        }

		public bool DrawBox 
		{
			get 
			{
				return _DrawBox;
			}
			set 
			{
				_DrawBox = value;
			}
		}

		public MarginExtender LeftMarginExtender
		{
			get
			{
				return _LeftMarginExtender;
			}
			set
			{
				_LeftMarginExtender=value;
			}
		}
		public MarginExtender RightMarginExtender
		{
			get
			{
				return _RightMarginExtender;
			}
			set
			{
				_RightMarginExtender=value;
			}
		}
		public MarginExtender TopMarginExtender
		{
			get
			{
				return _TopMarginExtender;
			}
			set
			{
				_TopMarginExtender=value;
			}
		}
		public MarginExtender BottomMarginExtender
		{
			get
			{
				return _BottomMarginExtender;
			}
			set
			{
				_BottomMarginExtender=value;
			}
		}
		#endregion

		#region Public Methods
		public void PrintOrPreview(CharsPerLine CPL,
			string PPrintBlock, string PTitle,
			string PSubTitle, PrintPreview PVOption,
			PrintOrientation Layout)
		{
			PrintOrPreview(CPL,PPrintBlock,PTitle,PSubTitle,PVOption,Layout,"","","","");
		}
		public void PrintOrPreview(CharsPerLine CPL,
			string PPrintBlock, string PTitle,
			string PSubTitle, PrintPreview PVOption,
			PrintOrientation Layout,string ColHdr1)
		{
			PrintOrPreview(CPL,PPrintBlock,PTitle,PSubTitle,PVOption,Layout,ColHdr1,"","","");
		}
		public void PrintOrPreview(CharsPerLine CPL,
			string PPrintBlock, string PTitle,
			string PSubTitle, PrintPreview PVOption,
			PrintOrientation Layout,string ColHdr1,string ColHdr2)
		{
			PrintOrPreview(CPL,PPrintBlock,PTitle,PSubTitle,PVOption,Layout,ColHdr1,ColHdr2,"","");
		}
		public void PrintOrPreview(CharsPerLine CPL,
			string PPrintBlock, string PTitle,
			string PSubTitle, PrintPreview PVOption,
			PrintOrientation Layout,string ColHdr1,string ColHdr2,
			string ColHdr3)
		{
			PrintOrPreview(CPL,PPrintBlock,PTitle,PSubTitle,PVOption,Layout,ColHdr1,ColHdr2,ColHdr3,"");
		}
		public void PrintOrPreview(CharsPerLine CPL,
			string PPrintBlock, string PTitle,
			string PSubTitle, PrintPreview PVOption,
			PrintOrientation Layout,string ColHdr1,string ColHdr2,
			string ColHdr3,string ColHdr4)
		{
			PrintPreviewDialog previewDialog;
			tbm = new TBMemoLine();
			Portrait =(Layout==PrintOrientation.Portrait);
			msRptString=PPrintBlock;
			_Title = PTitle;
			_SubTitle=PSubTitle;
			SetUpColHdrArray(ColHdr1,ColHdr2,ColHdr3,ColHdr4);
			miChrPerLine=(int) CPL;
			// create two memoline objects so that we can use 
			// nested calls to memoline w/o stepping
			// on each other, used only when wordwrap is on

            if (sFooter.Length == 0)
            {
                sFooter = "Printed on: " + DateTime.Now.ToString();
            }

			// chars per line will vary based on the margins
			switch (CPL)
			{
				case CharsPerLine.CPL80: 
					DetailFontSize = DETAIL_FONT_SIZE_80;
					break;
				case CharsPerLine.CPL96:
					DetailFontSize = DETAIL_FONT_SIZE_96;
					break;
				case CharsPerLine.CPL120:
					DetailFontSize = DETAIL_FONT_SIZE_120;
					break;
				case CharsPerLine.CPL160:
					DetailFontSize = DETAIL_FONT_SIZE_160;
					break;
				default:
					throw new System.Exception("Invalid CharsPerLine parameter");
			}

			// set up memoline
			miNL = tbm.MLCount(msRptString);
			if(miNL==0)
			{
				MessageBox.Show("No lines to print in report string.");
				return;
			}

			mI=0;
			PrintDoc = new PrintDocument();
			PrintDoc.PrintPage += new PrintPageEventHandler(this.OnPrintPage);
			PrintDoc.DefaultPageSettings.Landscape=(Layout == PrintOrientation.Landscape);
			PrintDoc.DocumentName=_Title;

			if(PVOption == PrintPreview.Preview)
			{
				previewDialog = new PrintPreviewDialog();
				previewDialog.Document = PrintDoc;
				previewDialog.ShowDialog();
				previewDialog.Dispose();
			}
			else
				PrintDoc.Print();
		}
		#endregion

		#region Private Methods
		// Handles requests for print pages from PrintDoc object.
		private void OnPrintPage(object sender,PrintPageEventArgs e)
		{
			int iLineLen = miChrPerLine;
			// compute the margins after determining landscape or portrait
			PrintFont = new Font(DETAIL_FONT,DetailFontSize);
			sz = e.Graphics.MeasureString("M",PrintFont);
			CharWidth = sz.Width;

			// check for margin extenders
			Single lmExt = 0;
			Single rmExt = 0;
			Single tmExt = 0;
			Single bmExt = 0;

			GetMarginExtenders(ref lmExt,ref rmExt,ref tmExt,ref bmExt);

			// determine the print area
			System.Drawing.Rectangle rect = 
				new System.Drawing.Rectangle((int) (e.PageBounds.X + 15 + lmExt), 
				(int) (e.PageBounds.Y + 15 + tmExt), 
				(int) (e.PageBounds.Width - (80 + rmExt)),
				(int) (e.PageBounds.Height - (75 + bmExt)));
			RightMargin = rect.Right;
			LeftMargin = rect.Left;
			TopMargin = rect.Top;
			BottomMargin =  rect.Bottom;
			xPos =  LeftMargin;
			yPos=TopMargin;

			if(_DrawBox)
			{
				Pen drPen2 =  new Pen(Color.Black,2);
				e.Graphics.DrawRectangle(drPen2,rect);
			}

			// print the header on this page first thing
			// print the title
			PrintTitle(e);

			// print the subtitle1
			PrintAllExtantSubTitles(e);

			PrintFont = new Font(DETAIL_FONT,DetailFontSize,FontStyle.Bold);

			PrintExtantColumnHeaders(e);


			// we can now call memoline to get the print data
			bool bBlank;

			/* Print directives
			 <:CH1>Heading 1
			 <:CH2>Heading 2
			 <:CH3>Heading 3
			 <:CH4>Heading 4
			 <:NEWPAGE>
			 <:NOLINES>
			 <:LINES>
			 <:SUBTITLE>New SubTitle Line
			 <:SUBTITLE2>New SubTitle Line
			 <:SUBTITLE3>New SubTitle Line
			 <:SUBTITLE4>New SubTitle Line
			 <:PAGENBR0>  resets the page count
			*/
			for(int i=mI; i < miNL; i++)
			{
				// print a detail line
				sLine = tbm.MemoLine(i);

				// ck for print directives
				if(sLine.Length>0)
				{
					if(sLine.StartsWith("<:"))
					{
						short k=HandlePrintDirectives(sLine);
						switch(k)
						{
							case 1: continue;
							case 2: 
								EndPage(e);
								e.HasMorePages=true;
								mI= i + 1;
								return;
							default:break;
						}
					}
				}

				if(sLine==null || sLine.Length==0)
					// dont print blank line, just bump yos
					bBlank=true;
				else
				{
					LineHeight=PrintFont.GetHeight(e.Graphics);
					xPos=LeftMargin;
					bBlank=false;
				}

				// print the detail line
				PrintFont=new Font(DETAIL_FONT,DetailFontSize,FontStyle.Bold);
				e.Graphics.DrawString(sLine,PrintFont,Brushes.Black,xPos,yPos,new StringFormat());
				yPos += LineHeight;

				// ck to see if we underline every line
				if(_SepLines  && !bBlank)
					PrintSeparatorLine(e,ref xPos,ref yPos,LeftMargin);

				// ck to see if at end of the page
				if(yPos>=(BottomMargin-(2*LineHeight)))
				{
					// end of page, ck for more lines to print
					EndPage(e);

					// ck for more lines to print
					if(i < miNL)
					{
						e.HasMorePages=true;
						// set ptr to next line back
						mI= i + 1;
						return;
					}
					else
					{
						e.HasMorePages=false;
						PageNbr=0;// in chase called again from preview
						mI = 0;
						return;
					}
				}// end ck for more pages
			}// end for
			e.HasMorePages = false;
			mI = 0;
			PageNbr = 0; // in case called again from preview print
			//print a footer on the last page
			yPos = BottomMargin - LineHeight;
			x1 = LeftMargin;
			y1 = yPos;
			x2 = RightMargin;
			y2 = yPos;
			e.Graphics.DrawLine(drPen, x1, y1, x2, y2);
			PrintFont.Dispose();
			PrintFont = new Font("Arial", 10);
			xPos = LeftMargin;
			e.Graphics.DrawString(sFooter, 
				PrintFont, 
				Brushes.Black, 
				xPos, 
				yPos, 
				new StringFormat());

		}// end PrintPage


		private void EndPage(PrintPageEventArgs e)
		{
			yPos=BottomMargin-(2*LineHeight);
			yPos+=10;
			x1=LeftMargin;
			y1=yPos;
			x2=RightMargin;
			y2=yPos;

			if(_PrintFooter)
				PrintFooterLine(e,ref xPos,ref yPos,LeftMargin);
		}
		private void PrintSeparatorLine(PrintPageEventArgs e,ref Single xPos,ref Single yPos,Single LeftMargin)
		{
			// insert a print separator line
			yPos+=2;
			x1=LeftMargin;
			y1=yPos;
			x2=RightMargin;
			y2=yPos;
			e.Graphics.DrawLine(drPen,x1,y1,x2,y2);
			yPos+=2;
		}
		private void PrintFooterLine(PrintPageEventArgs e,ref Single xPos,ref Single yPos,Single LeftMargin)
		{
			e.Graphics.DrawLine(drPen,x1,y1,x2,y2);
			yPos+=4;
			PrintFont.Dispose();
			PrintFont=new Font("Arial",10);
			xPos=LeftMargin;
			e.Graphics.DrawString(sFooter,PrintFont,Brushes.Black,xPos,yPos,new StringFormat());
		}
		private void PrintExtantColumnHeaders(PrintPageEventArgs e)
		{
			float LineHeight;
			// if headings are extant print them
			if(ColHdrCount>0)
			{
				for(short p=0;p<=ColHdrCount-1;p++)
				{
					yPos +=2;
					if(ColHdrArrayList[p].ToString().Length>0)
					{
						xPos=LeftMargin;
						LineHeight=PrintFont.GetHeight(e.Graphics);
						sHdrLine=_SubTitle;
						e.Graphics.DrawString(ColHdrArrayList[p].ToString(),PrintFont,Brushes.Black,xPos,yPos,new StringFormat());
						yPos += LineHeight;
					}
				}
				// now draw a line after column headers
				yPos+=4;
				x1=LeftMargin;
				y1=yPos;
				x2=RightMargin;
				y2=yPos;
				e.Graphics.DrawLine(drPen,x1,y1,x2,y2);
				yPos+=4;
				// print a second(double)line
				x1 = LeftMargin;  
				y1 = yPos;
				x2 = RightMargin; 
				y2 = yPos;
				e.Graphics.DrawLine(drPen, x1, y1, x2, y2);
				yPos += 4;
			}
		}
		private void PrintTitle(PrintPageEventArgs e)
		{
			PageNbr++;
			if(_TitleFontStyle==TitleStyle.BoldItalic)
				PrintFont = new Font("Arial", _TitleFontSize,FontStyle.Italic | FontStyle.Bold);
			else if(_TitleFontStyle==TitleStyle.Bold)
				PrintFont = new Font("Arial",_TitleFontSize, FontStyle.Bold);
			else if(_TitleFontStyle == TitleStyle.Italic)
				PrintFont = new Font("Arial", _TitleFontSize,FontStyle.Italic);
			else
				PrintFont = new Font("Arial", _TitleFontSize);
      
			LineHeight = PrintFont.GetHeight(e.Graphics);
			sz = e.Graphics.MeasureString(_Title,PrintFont);
			TextWidth = sz.Width;
			LineWidth = RightMargin + LeftMargin;

			xPos = (LineWidth - TextWidth) / 2;
			e.Graphics.DrawString(_Title,PrintFont,Brushes.Black, 
				xPos, yPos,new StringFormat());
			yPos += LineHeight;
		}

		private void PrintAllExtantSubTitles(PrintPageEventArgs e)
		{
			PrintFont = new Font("Arial",10,FontStyle.Bold);
			xPos=LeftMargin;
			LineHeight=PrintFont.GetHeight(e.Graphics);
			sHdrLine=_SubTitle;
			e.Graphics.DrawString(_SubTitle,PrintFont,Brushes.Black, 
				xPos, yPos,new StringFormat());

			// print the page number on the right end of last subtitle line
			if(_SubTitle2.Length==0)
			{
				sPageNbr = PageNbr.ToString();
				xPos = RightMargin - (7 * CharWidth);
				e.Graphics.DrawString("Page: " + sPageNbr, PrintFont, Brushes.Black, xPos, yPos, new StringFormat());
			}
			yPos += LineHeight;

			// print subtitle2 if extant
			if(_SubTitle2.Length>0)
			{
				PrintFont=new Font("Arial",10,FontStyle.Bold);
				xPos = LeftMargin;
				LineHeight = PrintFont.GetHeight(e.Graphics);
				sHdrLine = _SubTitle2;
				e.Graphics.DrawString(sHdrLine,PrintFont,Brushes.Black,xPos,yPos,new StringFormat());

				if(_SubTitle3.Length==0)
				{
					sPageNbr = PageNbr.ToString();
					xPos = RightMargin - (7 * CharWidth);
					e.Graphics.DrawString("Page: " + sPageNbr,PrintFont,Brushes.Black,xPos,yPos,new StringFormat());
				}
				yPos += LineHeight;
			}

			// print subtitle3 if extant
			if(_SubTitle3.Length>0)
			{
				PrintFont=new Font("Arial",10,FontStyle.Bold);
				xPos = LeftMargin;
				LineHeight = PrintFont.GetHeight(e.Graphics);
				sHdrLine = _SubTitle3;
				e.Graphics.DrawString(sHdrLine,PrintFont,Brushes.Black,xPos,yPos,new StringFormat());

				if(_SubTitle4.Length==0)
				{
					sPageNbr = PageNbr.ToString();
					xPos = RightMargin - (7 * CharWidth);
					e.Graphics.DrawString("Page: " + sPageNbr,PrintFont,Brushes.Black,xPos,yPos,new StringFormat());
				}
				yPos += LineHeight;
			}

			// print subtitle4 if extant
			if(_SubTitle4.Length>0)
			{
				PrintFont=new Font("Arial",10,FontStyle.Bold);
				xPos = LeftMargin;
				LineHeight = PrintFont.GetHeight(e.Graphics);
				sHdrLine = _SubTitle4;
				e.Graphics.DrawString(sHdrLine,PrintFont,Brushes.Black,xPos,yPos,new StringFormat());

				sPageNbr = PageNbr.ToString();
				xPos = RightMargin - (7 * CharWidth);
				e.Graphics.DrawString("Page: " + sPageNbr,PrintFont,Brushes.Black,xPos,yPos,new StringFormat());
				yPos += LineHeight;
			}

			// now draw a line after a little space
			x1=LeftMargin;
			y1=yPos;
			x2=RightMargin;
			y2=yPos;
			e.Graphics.DrawLine(drPen,x1,y1,x2,y2);
		}

		private short HandlePrintDirectives(string sLine)
		{
			// return 0, if no goto action
			// return 1 if goto next line
			// return 2 if goto EndPage
			// we have a print diretive telling us to change something
			if(sLine.Substring(2,3)=="CH1")
			{
				// new col1 hdr
				ColHdrArrayList[0]=sLine.Substring(6);
				return 1;
			}
			else if(sLine.Substring(2,3)=="CH2")
			{
				ColHdrArrayList[1]=sLine.Substring(6);
				return 1;
			}
			else if (sLine.Substring(2,3)=="CH3")
			{
				ColHdrArrayList[2]=sLine.Substring(6);
				return 1;
			}
			else if(sLine.Substring(2,3)=="CH4")
			{
				ColHdrArrayList[3]=sLine.Substring(6);
				return 1;
			}
			else if(sLine.Substring(2,7).StartsWith("NEWPAGE"))
				return 2;
			else if(sLine.Substring(2,5).StartsWith("LINES"))
			{
				_SepLines=true;
				return 1;
			}
			else if(sLine.Substring(2,7)=="NOLINES")
			{
				_SepLines=false;
				return 1;
			}
			else if(sLine.Substring(2,8).StartsWith("PAGENBR0"))
			{
				PageNbr=0;
				return 1;
			}
			else if(sLine.Substring(2,9).StartsWith("SUBTITLE1"))
			{
				_SubTitle=sLine.Substring(12);
				return 1;
			}
			else if(sLine.Substring(2,9).StartsWith("SUBTITLE2"))
			{
				_SubTitle2=sLine.Substring(12);
				return 1;
			}
			else if(sLine.Substring(2,9).StartsWith("SUBTITLE3"))
			{
				_SubTitle3=sLine.Substring(12);
				return 1;
			}
			else if(sLine.Substring(2,9).StartsWith("SUBTITLE4"))
			{
				_SubTitle4=sLine.Substring(12);
				return 1;
			}
			return 1;
		}

		private void SetUpColHdrArray(string ColHdr1,string ColHdr2,string ColHdr3,string ColHdr4)
		{
			if(ColHdr1.Length>0)
			{
				ColHdrArrayList.Add(ColHdr1);
				ColHdrCount++;
			}
			if(ColHdr2.Length>0)
			{
				ColHdrArrayList.Add(ColHdr2);
				ColHdrCount++;
			}
			if(ColHdr3.Length>0)
			{
				ColHdrArrayList.Add(ColHdr3);
				ColHdrCount++;
			}
			if(ColHdr4.Length>0)
			{
				ColHdrArrayList.Add(ColHdr4);
				ColHdrCount++;
			}
		}
		private void GetMarginExtenders(ref Single lmExt,ref Single rmExt,ref Single tmExt,ref Single bmExt)
		{
			lmExt = GetOneMarginExtender(_LeftMarginExtender);
			rmExt = GetOneMarginExtender(_RightMarginExtender);
			tmExt = GetOneMarginExtender(_TopMarginExtender);
			bmExt = GetOneMarginExtender(_BottomMarginExtender);
		}
		private Single GetOneMarginExtender(MarginExtender ext)
		{
			const int oneTenth=5;
			const int oneQtr=15;
			const int oneHalf=30;
			const int threeQtr=45;
			const int oneInch=60;
			switch (ext)
			{
				case MarginExtender.None: 
					return 0;
				case MarginExtender.OneTenthInch: 
					return oneTenth;
				case MarginExtender.OneQuarterInch: 
					return oneQtr;
				case MarginExtender.OneHalfInch: 
					return oneHalf;
				case MarginExtender.ThreeQuarterInch: 
					return threeQtr;
				case MarginExtender.OneInch: 
					return oneInch;
				default: 
					return 0;
			}
		}
		#endregion
	}

	#region TBMemoLine Class


	public class TBMemoLine
	{
		private TextBox tb;
		//private string [] a;
		// returns the requeste line from the memo string
		public string MemoLine(int nL)
		{
			if(nL >= 0 && nL <= tb.Lines.Length)
				return tb.Lines[nL];
				//return a[nL];
			else
				return string.Empty;
		}

		// returns the number of lines in the memo string
		public int MLCount(string s)
		{
			tb = new TextBox();
			tb.Text = s;
			//a=tb.Lines;
			//return a.Length;
			return tb.Lines.Length;
		}
	}//end class TBMemoline
	#endregion
}// end namespace
