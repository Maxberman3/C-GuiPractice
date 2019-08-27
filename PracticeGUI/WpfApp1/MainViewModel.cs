using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfApp1
{
    class MainViewModel : INotifyPropertyChanged
    {
        public List<Staff> people { get; }
        public Staff[] picksix { get; }
        public string[] namelist { get; }
        public MyCommand resetcommand { get; set; }
        public RadioCommand radioselect1 { get; set; }
        public RadioCommand radioselect2 { get; set; }
        public RadioCommand radioselect3 { get; set; }
        public RadioCommand radioselect4 { get; set; }
        public RadioCommand radioselect5 { get; set; }
        public RadioCommand radioselect6 { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public string[] selections { get; set; }
        public MyCommand submitcommand { get; set; }
        public MainViewModel()
        {
            people = new List<Staff>();
            picksix = new Staff[6];
            namelist = new string[18];
            selections = new string[6];
            resetcommand = new MyCommand(OnReset, CanReset);
            radioselect1 = new RadioCommand(RadioSelect1, CanSelect);
            radioselect2 = new RadioCommand(RadioSelect2, CanSelect);
            radioselect3 = new RadioCommand(RadioSelect3, CanSelect);
            radioselect4 = new RadioCommand(RadioSelect4, CanSelect);
            radioselect5 = new RadioCommand(RadioSelect5, CanSelect);
            radioselect6 = new RadioCommand(RadioSelect6, CanSelect);
            submitcommand = new MyCommand(OnSubmit, CanSubmit);
            //testimage = "C:/Users/503147985/Downloads/disney characters/aladin.jfif";
            string[] files = Directory.GetFiles("C:/Users/503147985/Downloads/disney characters");
            int filecount = 0;
            foreach(string path in files)
            {
                people.Add(new Staff(path));
                filecount++;
            }
            OnReset();
        }
        private void OnReset()
        {

            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                picksix[i]= people.ElementAt(random.Next(0, people.Count));
            }
            int[] select = new int[6];
            select[0] = random.Next(0, 3);
            select[1] = random.Next(3, 6);
            select[2] = random.Next(6, 9);
            select[3] = random.Next(9, 12);
            select[4] = random.Next(12, 15);
            select[5] = random.Next(15, 18);
            int assigned = 0;
            for (int i = 0; i<18; i++)
            {
                if (select.Contains(i))
                {
                    namelist[i] = picksix[assigned].Name;
                    if (assigned < 5)
                    {
                        assigned++;
                    }
                }
                else
                {
                    string randomname= people.ElementAt(random.Next(0, people.Count)).Name;
                    while (picksix[assigned].Name.Equals(randomname)) {
                            randomname = people.ElementAt(random.Next(0, people.Count)).Name;
                        }
                    namelist[i] = randomname;
                }
            }
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("picksix"));
                PropertyChanged(this, new PropertyChangedEventArgs("namelist"));
            }
            Array.Clear(selections, 0, 6);
        }
        private bool CanReset()
        {
            return people.Any();
        }
        private void OnSubmit() {
            string message = "";
            string newline = Environment.NewLine;
            for(int i = 0;i < 6; i++)
            {
                if (!selections[i].Equals(picksix[i].Name))
                {
                    message += "The staff member in frame " + i + " is actually named " + picksix[i].Name+newline;
                }
            }
            if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("Good job! You remembered everyone's names. That shit is low-key kinda hard");
            }
            else
            {
                MessageBox.Show(message);
            }
            OnReset();
}       
        private bool CanSubmit()
        {
            for (int i = 0; i < 6; i++)
            {
                if (selections[i] == null)
                {
                    return false;
                }
            }
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("submitcommand"));
            }
            return true;
        }
        private bool CanSelect(object obj)
        {
            return true;
        }
        private void RadioSelect1(object obj)
        {
            selections[0] = obj.ToString();
            submitcommand.RaiseCanExecuteChanged();
        }
        private void RadioSelect2(object obj)
        {
            selections[1] = obj.ToString();
            submitcommand.RaiseCanExecuteChanged();
        }
        private void RadioSelect3(object obj)
        {
            selections[2] = obj.ToString();
            submitcommand.RaiseCanExecuteChanged();
        }
        private void RadioSelect4(object obj)
        {
            selections[3] = obj.ToString();
            submitcommand.RaiseCanExecuteChanged();
        }
        private void RadioSelect5(object obj)
        {
            selections[4] = obj.ToString();
            submitcommand.RaiseCanExecuteChanged();
        }
        private void RadioSelect6(object obj)
        {
            selections[5] = obj.ToString();
            submitcommand.RaiseCanExecuteChanged();
        }
    }
}
