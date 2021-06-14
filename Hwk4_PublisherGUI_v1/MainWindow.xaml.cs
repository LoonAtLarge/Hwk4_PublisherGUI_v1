//******************************************************
// File: MainWindow.xaml.cs
//
// Purpose: Contains all code for event handling related to the Main Window GUI.
//
// Written By: Samson Fashakin
//
// Compiler: Visual Studio 2019
//
//******************************************************
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Fashakin_HW1_ClassLibrary;

/*accidentally named the file Hwk4_PublisherGUI_v1 even though this is our third homework. I was kinda just following the pdf
I tried renaming the whole thing but Visual Studio apparently doesn't refactor the whole project and it was very messy and files wouldn't open
so I just kept it
 */
namespace Hwk4_PublisherGUI_v1 
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenFileDialog op;
        private Publisher p;
       
        public MainWindow()
        {
            InitializeComponent();
            DataContext = p;
            
        }

        //****************************************************
        // Method: openFileBtn_Click
        //
        // Purpose: On Click Handler for the Open File Button.
        //****************************************************
        private void openFileBtn_Click(object sender, RoutedEventArgs e)
        {
            //clears the specified elements when a new file is chosen
            targetBookTxt.Clear();
            titleTxt.Clear();
            priceTxt.Clear();
            authorBox.DataContext = null;
            

            //opens file using File Dialog and reads data from file into new Publisher object
            op = new OpenFileDialog();
            op.Title = "Select a File";
            //op.InitialDirectory = ;
            op.ShowDialog();
            string filename = op.FileName;
            filenameTxt.Text = filename;
           
            try
            {
                FileStream reader = new FileStream(filename, FileMode.Open, FileAccess.Read);
                DataContractJsonSerializer inputSerializer;
                inputSerializer = new DataContractJsonSerializer(typeof(Publisher));
                p = (Publisher)inputSerializer.ReadObject(reader);
                reader.Dispose();
            }
            catch { }

            DataContext = p;



        }

        //****************************************************
        // Method: findBookBtn_Click
        //
        // Purpose: On Click handler for the Find Book Button.
        //****************************************************
        private void findBookBtn_Click(object sender, RoutedEventArgs e)
        {
            //finds specified Book by Title using findBook method and reads data into new Book object. Then sets DataContext of selected elements to new Book Object
            string bookName = targetBookTxt.Text.ToString();
            Book b = p.FindBook(bookName);
            titleTxt.DataContext = b;
            priceTxt.DataContext = b;
            authorBox.DataContext = b;
        }
    }
}
