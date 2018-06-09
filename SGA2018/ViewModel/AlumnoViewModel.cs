using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SGA2018.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls.Dialogs;
namespace SGA2018.ViewModel
{
    public class AlumnoViewModel : INotifyPropertyChanged, ICommand
    {
        #region "Datos"
        private SGADataContext _db = new SGADataContext();
        #endregion
        #region "Campos"
        private Alumno _elemento;
        private IDialogCoordinator _dialogCoordinator;
        private string _titulo;
        private string _carne;
        private ObservableCollection<Alumno> _listaAlumnos;
        private List<Carrera> _listaCarrera;
        private string _nombres;
        private string _apellidos;
        private DateTime _fechaNacimiento;
        private Carrera _carreraSeleccionada;
        private AlumnoViewModel _instancia;
        #endregion
        #region "Propiedades"
        public Alumno Elemento
        {
            get
            {
                return _elemento;
            }
            set
            {
                _elemento = value;
                if (value != null)
                {
                    this.Carne = value.Carne;
                    this.Apellidos = value.Apellidos;
                    this.Nombres = value.Nombres;
                    this.CarreraSeleccionada = value.Carrera;
                    this.FechaNacimiento = value.FechaNacimiento;
                }
                NotificarCambio("Elemento");
            }
        }
        public AlumnoViewModel Instancia
        {
            get
            {
                return _instancia;
            }
            set
            {
                _instancia = value;
                NotificarCambio("Instancia");
            }
        }
        public Carrera CarreraSeleccionada
        {
            get
            {
                return _carreraSeleccionada;
            }
            set
            {
                _carreraSeleccionada = value;
                NotificarCambio("CarreraSeleccionada");
            }

        }
        public DateTime FechaNacimiento
        {
            get
            {
                return _fechaNacimiento;
            }
            set
            {
                _fechaNacimiento = value;
                NotificarCambio("FechaNacimiento");
            }
        }
        public string Apellidos
        {
            get
            {
                return _apellidos;
            }
            set
            {
                _apellidos = value;
                NotificarCambio("Apellidos");
            }
        }
        public string Nombres
        {
            get
            {
                return _nombres;
            }
            set
            {
                _nombres = value;
                NotificarCambio("Nombres");
            }
        }
        public List<Carrera> ListaCarreras
        {
            get
            {
                if (_listaCarrera != null)
                {
                    return _listaCarrera;
                }
                else
                {
                    _listaCarrera = _db.Carreras.ToList();
                    return _listaCarrera;
                }
            }
            set
            {
                _listaCarrera = value;
                NotificarCambio("ListaCarreras");
            }
        }
        public string Carne
        {
            get
            {
                return _carne;
            }
            set
            {
                _carne = value;
                NotificarCambio("Carne");
            }
        }
        public string Titulo 
        {
            get { return _titulo; }
            set { _titulo = value; NotificarCambio("Titulo");  }
        }
        public ObservableCollection<Alumno> ListaAlumnos
        {
            get
            {
                if(_listaAlumnos != null)
                {
                    return _listaAlumnos;
                }
                else
                {
                    _listaAlumnos = new ObservableCollection<Alumno>(_db.Alumnos.ToList());
                    return _listaAlumnos;
                }
            }
            set
            {
                _listaAlumnos = value;
                NotificarCambio("ListaAlumnos");
            }
        }
        #endregion
        #region "Constructores"
        public AlumnoViewModel(IDialogCoordinator dialogCoordinator)
        {
            this._dialogCoordinator = dialogCoordinator;
            this.Titulo = "Sistema SGA";
            this.FechaNacimiento = DateTime.Now;
            this.Instancia = this;
        }
        #endregion
        #region "Metodos y funciones"
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public void NotificarCambio(string propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object control)
        {
            if (control.Equals("Nuevo"))
            {
                var registro = new Alumno
                {
                    Carne = this.Carne,
                    Nombres = this.Nombres,
                    Apellidos = this.Apellidos,
                    FechaNacimiento = this.FechaNacimiento,
                    Carrera = this.CarreraSeleccionada
                };
                _db.Alumnos.Add(registro);
                _db.SaveChanges();
                this.ListaAlumnos.Add(registro);
            }else if(control.Equals("Eliminar"))
            {
                if (Elemento != null)
                {
                    MessageDialogResult resultado = await this._dialogCoordinator.ShowMessageAsync(this
                    , "Eliminar Alumno"
                    , "Está seguro de eliminar el registro?"
                    , MessageDialogStyle.AffirmativeAndNegative);
                    if (resultado == MessageDialogResult.Affirmative)
                    {
                        _db.Alumnos.Remove(Elemento);
                        _db.SaveChanges();
                    }
                }
                else
                {
                    await this._dialogCoordinator.ShowMessageAsync(
                     this
                    , "Eliminar Alumno"
                    , "Debe seleccionar un elemento");
                }
            }
        }
        #endregion
    }
}
