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
            this.Instancia = this;//aqui relaciono todas las ventanas de mis modelos con la instancia creada de la ventana principal 
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

            }else if (objeto.Equals("Puestos"))
            {

                PuestoView ventana = new PuestoView();
                ventana.ShowDialog();

            }
            else if (objeto.Equals("Carreras"))
            {

                CarreraView ventana = new CarreraView();
                ventana.ShowDialog();

            }
        }
    }
}















