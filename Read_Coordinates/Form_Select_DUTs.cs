/*Created: 26.08.2019; Maxi Schmid
 * 
 * Explanation:
 * 
 * 
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Read_Coordinates
{
    public partial class Form_Select_DUTs : Form
    {
        //********************************************************************************************************************
        //                                         Lokale     Variablen
        //********************************************************************************************************************

        private Movement_Infos MyMovement_New { get;  set; } = null;
       // private Measurement_Point_XYZA [] MyMovement_Old {  get;  set; } = null;

        private Movement_Infos MyMovement_Old { get; set; } = null;

        //********************************************************************************************************************
        //                                              Initalisierung
        //********************************************************************************************************************

        public Form_Select_DUTs(Movement_Infos input)
        {
            //Information übernehmen
            MyMovement_New = input;
            MyMovement_Old = input.CopyByValue();

            //Alten Stand ablegen (Copy by value)   
            /*
            MyMovement_Old = new Measurement_Point_XYZA[input.MyMeasurment_Point.Length];
            for (int i = 0; i < input.MyMeasurment_Point.Length; i++)
                MyMovement_Old[i] = new Measurement_Point_XYZA
                    (input.MyMeasurment_Point[i].X, 
                    input.MyMeasurment_Point[i].Y, 
                    input.MyMeasurment_Point[i].Angle, 
                    input.MyMeasurment_Point[i].Name, 
                    input.MyMeasurment_Point[i].IsSelected);
                    */
           
            //Form initialisieren
            InitializeComponent();

            //Feld mit LED-Namen auffüllen (Für jede Position eine LED)
            for (int i = 0; i < MyMovement_New.MyMeasurment_Point.Length; i++)
            {
                if (MyMovement_New.MyMeasurment_Point[i].IsSelected)
                    listBox_selected.Items.Add(MyMovement_New.MyMeasurment_Point[i].Name);

                else
                    listBox_NOT_selected.Items.Add(MyMovement_New.MyMeasurment_Point[i].Name);
            }

        }

        //********************************************************************************************************************
        //                                              Add & Delete
        //********************************************************************************************************************

        private void Popup_add_Click(object sender, EventArgs e)
        {
            //Alle ausgewählte abholen
            ListBox.SelectedIndexCollection indices = listBox_NOT_selected.SelectedIndices;

            //Für jeden Ausgewählten Index in selected Liste übertragen
            foreach (int index in indices)
            {
                //In selected Liste übertragen
                listBox_selected.Items.Add(listBox_NOT_selected.Items[index]);

                //Is selected Flag auf true
                for (int i = 0; i < MyMovement_New.MyMeasurment_Point.Length; i++)
                {
                    if (MyMovement_New.MyMeasurment_Point[i].Name == listBox_NOT_selected.Items[index].ToString())
                        MyMovement_New.MyMeasurment_Point[i].IsSelected = true;
                }
            }
            //Alle ausgewählten aus availabel Liste löschen
            while (indices.Count > 0)
            {
                listBox_NOT_selected.Items.RemoveAt(indices[0]);
            }
        }

        private void Popup_delete_Click(object sender, EventArgs e)
        {
            //Alle ausgewählte abholen
            ListBox.SelectedIndexCollection indices = listBox_selected.SelectedIndices;

            //Für jeden Ausgewählten Index in selected Liste übertragen
            foreach (int index in indices)
            {
                //In selected Liste übertragen
                listBox_NOT_selected.Items.Add(listBox_selected.Items[index]);

                //Is selected Flag auf true
                for (int i = 0; i < MyMovement_New.MyMeasurment_Point.Length; i++)
                {
                    if (MyMovement_New.MyMeasurment_Point[i].Name == listBox_selected.Items[index].ToString())
                        MyMovement_New.MyMeasurment_Point[i].IsSelected = false;
                }
            }
            //Alle ausgewählten aus availabel Liste löschen
            while (indices.Count > 0)
            {
                listBox_selected.Items.RemoveAt(indices[0]);
            }
        }

        private void Popup_add_all_Click(object sender, EventArgs e)
        {
            while (listBox_NOT_selected.Items.Count > 0)
            {
                listBox_selected.Items.Add(listBox_NOT_selected.Items[0]);
                listBox_NOT_selected.Items.Remove(listBox_NOT_selected.Items[0]);
            }

            //All Flags to true
            for (int i = 0; i < MyMovement_New.MyMeasurment_Point.Length; i++)
            {
                 MyMovement_New.MyMeasurment_Point[i].IsSelected = true;
            }
        }

        private void Popup_delete_all_Click(object sender, EventArgs e)
        {
            while (listBox_selected.Items.Count > 0)
            {
                listBox_NOT_selected.Items.Add(listBox_selected.Items[0]);
                listBox_selected.Items.Remove(listBox_selected.Items[0]);
            }

            //All Flags to true
            for (int i = 0; i < MyMovement_New.MyMeasurment_Point.Length; i++)
            {
                MyMovement_New.MyMeasurment_Point[i].IsSelected = false;
            }
        }

        //********************************************************************************************************************
        //                                              Close Window
        //********************************************************************************************************************


        private void Popup_ok_button_Click(object sender, EventArgs e)
        {
            //Fenster schließen
            this.Close();           
        }

        private void Popup_cancel_button_Click(object sender, EventArgs e)
        {
            //Alten Zustand wieder herstellen
            MyMovement_New.MyMeasurment_Point = MyMovement_Old.MyMeasurment_Point;

            //Fenster schließen
            this.Close();           
        }


    }
}
