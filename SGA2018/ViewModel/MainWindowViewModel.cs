using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using SGA2018.View;
namespace SGA2018.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged, ICommand
    {
        public MainWindowViewModel Instancia { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public MainWindowViewModel()
        {
            this.Instancia = this;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object objeto)
        {
            if(objeto.Equals("Alumnos"))
            {
                AlumnoView ventana = new AlumnoView();
                ventana.ShowDialog();
            }
        }
    }
}















