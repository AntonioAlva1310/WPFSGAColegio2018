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
using System.Data.Entity;

namespace SGA2018.ViewModel
{
    enum ACCION
    {
        NINGUNO,
        NUEVO,
        GUARDAR
    };
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
        private bool _isEnableGuardar = false;
        private bool _isEnableCancelar = false;
        private bool _isEnableNuevo = true;
        private bool _isEnableEliminar = true;
        private bool _isEnableEditar = true;
        private bool _isReadOnlyCarne = true;
        private bool _isReadOnlyApellidos = true;
        private bool _isReadOnlyNombres = true;
        private bool _isEnableFechaNacimiento = false;
        private bool _isEnableCarrera = false;
        private ACCION _accion = ACCION.NINGUNO;
        #endregion
        #region "Propiedades"
        public bool IsEnableCarrera
        {
            get { return _isEnableCarrera; }
            set { _isEnableCarrera = value; NotificarCambio("IsEnableCarrera"); }
        }

        public bool IsEnableFechaNacimiento
        {
            get { return _isEnableFechaNacimiento; }
            set { _isEnableFechaNacimiento = value; NotificarCambio("IsEnableFechaNacimiento"); }
        }

        public bool IsReadOnlyApellidos
        {
            get { return _isReadOnlyApellidos; }
            set { _isReadOnlyApellidos = value; NotificarCambio("IsReadOnlyApellidos"); }
        }

        public bool IsReadOnlyNombres
        {
            get { return _isReadOnlyNombres; }
            set { _isReadOnlyNombres = value; NotificarCambio("IsReadOnlyNombres"); }
        }

        public bool IsReadOnlyCarne
        {
            get { return _isReadOnlyCarne; }
            set { _isReadOnlyCarne = value; NotificarCambio("IsReadOnlyCarne"); }
        }
        public bool IsEnableEditar
        {
            get { return _isEnableEditar; }
            set { _isEnableEditar = value; NotificarCambio("IsEnableEditar"); }
        }
        public bool IsEnableEliminar
        {
            get { return _isEnableEliminar; }
            set { _isEnableEliminar = value; NotificarCambio("IsEnableEliminar"); }
        }
        public bool IsEnableNuevo
        {
            get { return _isEnableNuevo; }
            set { _isEnableNuevo = value; NotificarCambio("IsEnableNuevo"); }
        }
        public bool IsEnableCancelar
        {
            get { return _isEnableCancelar; }
            set { _isEnableCancelar = value; NotificarCambio("IsEnableCancelar"); }
        }
        public bool IsEnableGuardar
        {
            get{ return _isEnableGuardar; }
            set{ _isEnableGuardar = value; NotificarCambio("IsEnableGuardar"); }
        }
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
                ActivarControles();
            } else if (control.Equals("Eliminar"))
            {
                if (Elemento != null)
                {
                    MessageDialogResult resultado = await this._dialogCoordinator.ShowMessageAsync(this
                    , "Eliminar Alumno"
                    , "Está seguro de eliminar el registro?"
                    , MessageDialogStyle.AffirmativeAndNegative);
                    if (resultado == MessageDialogResult.Affirmative)
                    {
                        try
                        {
                            _db.Alumnos.Remove(Elemento);
                            _db.SaveChanges();
                            ListaAlumnos.Remove(Elemento);
                            LimpiarCampos();

                        }
                        catch (Exception ex)
                        {
                            await this._dialogCoordinator.ShowMessageAsync(
                            this
                            , "Eliminar Alumno"
                            , ex.Message);
                        }

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
            else if (control.Equals("Guardar"))
            {
                switch (this._accion)
                {
                    case ACCION.NUEVO:
                        try
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
                        }
                        catch (Exception ex)
                        {
                            await this._dialogCoordinator.ShowMessageAsync(
                            this
                            , "Guardar Alumno"
                            , ex.Message);
                        }
                        finally
                        {
                            DesactivarControles();
                        }
                        break;
                      case ACCION.GUARDAR:
                        try
                        {
                            Elemento.Carrera = this.CarreraSeleccionada;
                            Elemento.Apellidos = this.Apellidos;
                            Elemento.Nombres = this.Nombres;
                            Elemento.FechaNacimiento = this.FechaNacimiento;
                            _db.Entry(Elemento).State = EntityState.Modified;
                            _db.Alumnos.Attach(Elemento);
                            _db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            await this._dialogCoordinator.ShowMessageAsync(
                            this
                            , "Editar Alumno"
                            , ex.Message);
                        }
                        break;
                }
            }
        }

        private void DesactivarControles()
        {
            this.IsEnableNuevo = true;
            this.IsEnableEliminar = true;
            this.IsEnableEditar = true;
            this.IsEnableGuardar = false;
            this.IsEnableCancelar = false;
            this.IsReadOnlyCarne = true;
            this.IsReadOnlyApellidos = true ;
            this.IsReadOnlyNombres = true;
            this.IsEnableCarrera = false;
            this.IsEnableFechaNacimiento = false;
            this._accion = ACCION.NINGUNO;
        }

        private void ActivarControles()
        {
            this.IsEnableNuevo = false;
            this.IsEnableEliminar = false;
            this.IsEnableEditar = false;
            this.IsEnableGuardar = true;
            this.IsEnableCancelar = true;
            this.IsReadOnlyCarne = false;
            this.IsReadOnlyApellidos = false;
            this.IsReadOnlyNombres = false;
            this.IsEnableCarrera = true;
            this.IsEnableFechaNacimiento = true;
            this._accion = ACCION.NUEVO;
        }

        private void LimpiarCampos()
        {
            this.Carne = string.Empty;
            this.Apellidos = string.Empty;
            this.Nombres = string.Empty;
            this.FechaNacimiento = DateTime.Now;
            this.CarreraSeleccionada = null;
        }
        #endregion
    }
}
