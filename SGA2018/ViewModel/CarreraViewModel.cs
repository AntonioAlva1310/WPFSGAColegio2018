using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using System.ComponentModel;
using System.Windows.Input;
using SGA2018.Model;
using System.Collections.ObjectModel;
using System.Data.Entity;


namespace SGA2018.ViewModel
{
    public class CarreraViewModel : INotifyPropertyChanged, ICommand {

        #region "Datos"
        private SGADataContext _db = new SGADataContext();
        #endregion
        #region "Campos"
        private Carrera _elemento;
        private IDialogCoordinator _dialogCoordinator;
        private string _titulo;
        private string _descripcion;
        private CarreraViewModel _instancia;
        private ObservableCollection<Carrera> _listaCarreras;
        private ACCION _accion = ACCION.NINGUNO;
        #endregion
        #region "Propiedades"
        public Carrera Elemento
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
                    this.Descripcion = value.Descripcion;
                }
                NotificarCambio("Elemento");

            }
        }
        public CarreraViewModel Instancia

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

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; NotificarCambio("Titulo"); }
        }

        public string Descripcion
        {
            get
            {
                return _descripcion;
            }
            set
            {
                _descripcion = value;
                NotificarCambio("Descripcion");
            }
        }
        public ObservableCollection<Carrera> ListaCarreras
        {
            get
            {
                if (_listaCarreras != null)
                {
                    return _listaCarreras;
                }
                else
                {
                    _listaCarreras = new ObservableCollection<Carrera>(_db.Carreras.ToList());
                    return _listaCarreras;
                }
            }
            set
            {
                _listaCarreras = value;
                NotificarCambio("ListaCarreras");
            }
        }
        #endregion 
        #region "Constructores"
        public CarreraViewModel(IDialogCoordinator dialogCoordinator)
        {
            this._dialogCoordinator = dialogCoordinator;
            this.Titulo = "Sistema SGA";

            this.Instancia = this;
        }
        #endregion
        #region "Metodos y funciones"
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;//los click que ejectuare

        public void NotificarCambio(string propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }

        //ejecutar instruccion con algún boton relacionado 
        public bool CanExecute(object parameter)
        {
            return true;//le decimos que si lo ejecute 
        }

        public async void Execute(object control)
        {
            if (control.Equals("Nuevo"))
            {
                //ActivarControles();
                this._accion = ACCION.NUEVO;
            }
            else if (control.Equals("Eliminar"))
            {
                if (Elemento != null)
                {
                    MessageDialogResult resultado = await this._dialogCoordinator.ShowMessageAsync(this
                    , "Eliminar Carrera"
                    , "Está seguro de eliminar el registro?"
                    , MessageDialogStyle.AffirmativeAndNegative);
                    if (resultado == MessageDialogResult.Affirmative)
                    {
                        try
                        {
                            _db.Carreras.Remove(Elemento);
                            _db.SaveChanges();
                            ListaCarreras.Remove(Elemento);
                            //LimpiarCampos();

                        }
                        catch (Exception ex)
                        {
                            await this._dialogCoordinator.ShowMessageAsync(
                            this
                            , "Eliminar Carrera"
                            , ex.Message);
                        }

                    }
                }
                else
                {
                    await this._dialogCoordinator.ShowMessageAsync(
                     this
                    , "Eliminar Carrera"
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
                            var registro = new Carrera
                            {
                                Descripcion = this.Descripcion

                            };
                            _db.Carreras.Add(registro);
                            _db.SaveChanges();
                            this.ListaCarreras.Add(registro);
                        }
                        catch (Exception ex)
                        {
                            await this._dialogCoordinator.ShowMessageAsync(
                            this
                            , "Guardar Carrera"
                            , ex.Message);
                        }
                        finally
                        {
                            // DesactivarControles();
                            this._accion = ACCION.NINGUNO;
                        }
                        break;
                    case ACCION.GUARDAR:
                        try
                        {
                            int posicion = ListaCarreras.IndexOf(Elemento);
                            var registro = _db.Carreras.Find(Elemento.CarreraId);
                            if (registro != null)
                            {
                                registro.Descripcion = this.Descripcion;


                                _db.Entry(registro).State = EntityState.Modified;
                                _db.SaveChanges();
                                ListaCarreras.RemoveAt(posicion);
                                ListaCarreras.Insert(posicion, registro);
                            }

                        }
                        catch (Exception ex)
                        {
                            await this._dialogCoordinator.ShowMessageAsync(
                            this
                            , "Editar Carrera"
                            , ex.Message);
                        }
                        finally
                        {
                            //DesactivarControles();
                            this._accion = ACCION.NINGUNO;
                        }
                        break;
                }

            }

            else if (control.Equals("Editar"))
            {
                if (Elemento != null)
                {
                  //  ActivarControles();
                 //   this.IsReadOnlyCarne = true;
                    this._accion = ACCION.GUARDAR;
                }
                else
                {
                    await this._dialogCoordinator.ShowMessageAsync(
                    this
                    , "Editar Carrera"
                    , "Debe seleccionar un elemento");
                }
            }
        }
        #endregion
    }
}
   


   



