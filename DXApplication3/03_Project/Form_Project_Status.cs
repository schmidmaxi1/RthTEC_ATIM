using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;

namespace ATIM_GUI._3_Project
{
    public partial class Form_Project_Status : Form
    {



        public Messung_Info neu1= new Messung_Info()
        {
            Name = "Hans",
            Kat1 = "0000Cycles",
            Kat2 = "SAC305",
            Kat3 = "Board_1",

            Anzahl_Prozent = 100,
            Error_Prozent = 0
        };
        public Messung_Info neu2 = new Messung_Info()
        {
            Name = "Hans",
            Kat1 = "0000Cycles",
            Kat2 = "SAC305",
            Kat3 = "Board_2",

            Anzahl_Prozent = 100,
            Error_Prozent = 0
        };
        public Messung_Info neu3 = new Messung_Info()
        {
            Name = "Hans",
            Kat1 = "0500Cycles",
            Kat2 = "SAC305",
            Kat3 = "Board_1",

            Anzahl_Prozent = 100,
            Error_Prozent = 50
        };
        public Messung_Info neu4 = new Messung_Info()
        {
            Name = "Hans",
            Kat1 = "0500Cycles",
            Kat2 = "SAC305",
            Kat3 = "Board_2",

            Anzahl_Prozent = 100,
            Error_Prozent = 0
        };
        public Messung_Info neu5 = new Messung_Info()
        {
            Name = "Hans",
            Kat1 = "0000Cycles",
            Kat2 = "SiBi",
            Kat3 = "Board_1",

            Anzahl_Prozent = 0,
            Error_Prozent = 0
        };





        public BindingList<Messung_Info> testListe;




        public Form_Project_Status()
        {
            testListe = new BindingList<Messung_Info>() { neu1, neu2, neu3, neu4, neu5 };



            InitializeComponent();

            gridControl1.DataSource = testListe;


            //GridColumnSummaryItem item1 = new GridColumnSummaryItem()


            gridView1.Columns[0].Group();
            gridView1.Columns[0].Caption = "Cycles";
            gridView1.Columns[1].Group();
            gridView1.Columns[1].Caption = "Paste";
            gridView1.Columns[2].Group();
            gridView1.Columns[2].Caption = "Board";

            gridView1.Columns[3].Visible = false;




            /*
            GridColumn firstGroupColumn = gridView1.SortInfo[6].Column;
            GroupSummarySortInfo[] groupSummaryToSort = { new GroupSummarySortInfo(summaryItemMaxOrderSum, firstGroupColumn, ColumnSortOrder.Ascending) };
            gridView1.GroupSummarySortInfo.ClearAndAddRange(groupSummaryToSort);
            */



            GridGroupSummaryItem item = new GridGroupSummaryItem();
            item.FieldName = "test";
            item.ShowInGroupColumnFooter = gridView1.Columns[5];
            item.SummaryType = DevExpress.Data.SummaryItemType.Average;
            //item.Collection.
            
            item.DisplayFormat = "AVR = {0:n2}";

            gridView1.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.True;




            gridView1.GroupSummary.Add(item);



        }
    }
    
}
