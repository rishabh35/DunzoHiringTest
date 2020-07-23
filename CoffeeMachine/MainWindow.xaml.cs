using System;                       //Environment
using System.Collections.Generic;   //List
using System.Windows;               //Window, RoutedEventArgs
using System.Windows.Controls;      //Button

namespace CoffeeMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            json = "";
        }

        /// <summary>
        /// json string
        /// </summary>
        public string json { get; set; }

        /// <summary>
        /// Event handler for button click
        /// </summary>
        /// <param name="sender">Button on which it is clicked</param>
        /// <param name="e">The routed event generated on click</param>
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rb8.IsChecked == true)
                    json = tb.Text;
                if (json == "")
                {
                    MessageBox.Show("Empty JSON");
                    return;
                }

                //Initialise coffee machine
                CoffeeMachineMain cm = new CoffeeMachineMain(json);
                GenerateAndShowMessage(cm.CalculateWhatCanBeMade());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //Calcualte what all can be made and show message
        }

        /// <summary>
        /// Parse from the list into user readable format and show message
        /// </summary>
        /// <param name="prepStatus"></param>
        private void GenerateAndShowMessage(List<PreparationStatus> prepStatus)
        {
            string messageToShow = "";
            foreach (PreparationStatus prep in prepStatus)
            {
                messageToShow += prep.beverage.BeverageName;
                if (prep.isPrepared)
                    messageToShow += " is prepared";
                else
                {
                    messageToShow += " cannot be prepared because";
                    if (prep.reasonForError == ERROR_REASON.NO_FREE_OUTLETS)
                        messageToShow += " no free outlet";
                    else
                    {
                        foreach (Ingredient i in prep.statusList.Keys)
                        {
                            messageToShow += (" " + i.IngredientName + " is " + prep.statusList[i].ToString().ToLower());
                        }
                    }
                }
                messageToShow += Environment.NewLine;
            }
            MessageBox.Show(messageToShow, "Order Status");
        }

        /// <summary>
        /// Function to handle event radio button checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb_Checked(object sender, RoutedEventArgs e)
        {
            if (tb == null)
                return;
            tb.IsReadOnly = true;
            // On the basis of case, run test
            if (rb1.IsChecked == true)
                json = Properties.Resources.case1;
            else if (rb2.IsChecked == true)
                json = Properties.Resources.case2;
            else if (rb3.IsChecked == true)
                json = Properties.Resources.case3;
            else if (rb4.IsChecked == true)
                json = Properties.Resources.case4;
            else if (rb5.IsChecked == true)
                json = Properties.Resources.case5;
            else if (rb6.IsChecked == true)
                json = Properties.Resources.case6;
            else if (rb7.IsChecked == true)
                json = Properties.Resources.case7;
            else if (rb8.IsChecked == true)
                tb.IsReadOnly = false;
            tb.Text = json;
            tb.UpdateLayout();
        }

    }
}
